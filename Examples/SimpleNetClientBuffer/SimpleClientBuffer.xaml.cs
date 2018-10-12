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
using MyUtil;

namespace SimpleNetClientBuffer
{
    /// <summary>
    /// Interaction logic for SimpleClientBuffer.xaml
    /// </summary>
    public partial class SimpleClientBuffer : Window
    {
        NetClient client = new NetClient(NetConnection.ClientMode.bufferMode);
        bool isConnected = false;

        public SimpleClientBuffer()
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
            btnCheck.IsEnabled = false;
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
            btnCheck.IsEnabled = isConnected;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbMsg.Text.Length > 0)
            {
                client.Send(tbMsg.Text);
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (client.Available > 0)
            {
                byte[] data = client.ReadAll();
                String msg = Encoding.UTF8.GetString(data);
                tbLog.Text += String.Format("{0} bytes received : {1} \n", data.Count(), msg);
            }
        }
    }
}
