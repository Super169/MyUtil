using MyUtil;
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


namespace SimpleNetClient
{
    /// <summary>
    /// Interaction logic for SimpleClient.xaml
    /// </summary>
    public partial class SimpleClient : Window
    {

        NetClient client = new NetClient();
        bool isConnected = false;

        public SimpleClient()
        {
            InitializeComponent();
            client.OnConnect += client_OnConnect;
            client.OnDataReceived += client_OnDataReceived;
            client.OnDataSend += client_OnDataSend;
            client.OnDisconnect += client_OnDisconnect;
        }

        private void client_OnDisconnect(object sender, NetConnection connection)
        {
            // It seems that it will be triggered again after server confirm for disconnect
            if (connection.RemoteEndPoint != null) tbLog.Text += "Disconnect from " + connection.RemoteEndPoint + "\n";
            btnConnect.Content = "Connect";
            btnSend.IsEnabled = false;
        }

        private void client_OnDataReceived(object sender, NetConnection connection, byte[] e)
        {
            tbLog.Text += "Message from " + connection.RemoteEndPoint + " : " + Encoding.UTF8.GetString(e) + "\n";
        }

        private void client_OnDataSend(object sender, NetConnection connection, byte[] e)
        {
            tbLog.Text += "Message to " + connection.RemoteEndPoint + " : " + Encoding.UTF8.GetString(e) + "\n";
        }

        private void client_OnConnect(object sender, NetConnection connection)
        {
            tbLog.Text += "Connected to " + connection.RemoteEndPoint + "\n";
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                client.Disconnect();
                isConnected = false;
                tbLog.Text += "Server disconnected\n\n";
            }
            else
            {
                client.Connect("localhost", 55555);
                if (client.Connected)
                {
                    isConnected = true;
                    tbLog.Text += "Server connected\n";
                }
                else
                {
                    tbLog.Text += "Fail connecting to server at localhost:55555\n";
                }
            }
            btnConnect.Content = (isConnected ? "Disconnect" : "Connect");
            btnSend.IsEnabled = isConnected;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbMsg.Text.Length > 0)
            {
                client.Send(tbMsg.Text);
            }
        }
    }
}
