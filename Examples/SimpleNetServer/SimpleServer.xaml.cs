using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MyUtil;


namespace SimpleNetServer
{
    /// <summary>
    /// Interaction logic for SimpleServer.xaml
    /// </summary>
    public partial class SimpleServer : Window
    {
        NetServer server = new NetServer();
        bool serverRunning = false;

        public SimpleServer()
        {
            InitializeComponent();
            server.OnConnect += server_OnConnect;
            server.OnDataReceived += server_OnDataReceived;
            server.OnDataSend += server_OnDataSend;
            server.OnDisconnect += server_OnDisconnect;
            server.OnKillClient += server_OnKillClient;
        }

        private void server_OnDisconnect(object sender, NetConnection connection)
        {
            // No need to show if connection is killed by server (in this case, connection has no client)
            if (connection.RemoteEndPoint != null) tbLog.Text += "Disconnect from " + connection.RemoteEndPoint + "\n";
            RefreshClientList();
        }

        private void server_OnDataReceived(object sender, NetConnection connection, byte[] e)
        {
            if (e.Length > 0)
                tbLog.Text += "Message from " + connection.RemoteEndPoint + " : " + Encoding.UTF8.GetString(e) + "\n";
        }

        private void server_OnDataSend(object sender, NetConnection connection, byte[] e)
        {
            if (e.Length > 0)
                tbLog.Text += "Message to " + connection.RemoteEndPoint + " : " + Encoding.UTF8.GetString(e) + "\n";
        }

        private void server_OnConnect(object sender, NetConnection connection)
        {
            tbLog.Text += "Connection from " + connection.RemoteEndPoint + "\n";
            RefreshClientList();
        }

        private void server_OnKillClient(object sender, NetConnection connection)
        {
            tbLog.Text += "Kill client connection to " + connection.RemoteEndPoint + "\n";
            RefreshClientList();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (serverRunning)
            {
                server.Stop();
                serverRunning = false;
                tbLog.Text += "Server stopped\n\n";
            }
            else
            {
                server.Start(55555);
                serverRunning = true;
                tbLog.Text += "Server started\n";
            }
            btnStart.Content = (serverRunning ? "Stop" : "Start");
            btnBroadcast.IsEnabled = serverRunning;
        }

        private void btnBroadcast_Click(object sender, RoutedEventArgs e)
        {
            if (tbMsg.Text.Length > 0)
            {
                server.Send(tbMsg.Text);
            }
        }

        private void RefreshClientList()
        {
            cboClient.Items.Clear();
            List<NetClient> clients = server.GetClientList();
            foreach (NetClient client in clients)
            {
                // In some rare case, when stopping connection, it will refresh the list many time, sometime the client is reset during refresh
                EndPoint ep = client.RemoteEndPoint;
                if (ep != null) cboClient.Items.Add(client.RemoteEndPoint);
            }
            btnSend.IsEnabled = (cboClient.Items.Count > 0);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbMsg.Text.Length == 0) return;
            EndPoint ep = (EndPoint)cboClient.SelectedItem;
            if (ep == null) return;
            List<NetClient> clients = server.GetClientList();
            NetClient client = clients.Find(x => x.RemoteEndPoint == ep);
            if (client == null) return;
            client.Send(tbMsg.Text);
        }
    }
}
