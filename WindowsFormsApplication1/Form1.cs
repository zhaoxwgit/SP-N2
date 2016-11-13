using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSMBS;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        WSMBS.WSMBSControl WsmbsRun = new WSMBS.WSMBSControl();
        IPEndPoint ServerIp = new IPEndPoint(IPAddress.Parse("0.0.0.0"),0);
        IPEndPoint LocalIp = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);

        TcpClient client = null;
        TcpListener listener = null;

        NetworkStream streamToServer = null;
        public static int ServerPort = 2000;//服务器端端口
        public static Socket serverSocket;//服务器端
        public static int ClientCount_Max = 20;//客户端的最大数量

        public Form1()
        {
            InitializeComponent();
            WsmbsRun.LicenseKey("2222222222222222222222222F3AA");
        }

        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSMBS.Result Result;
            Result = WsmbsRun.Open();
            if (Result != WSMBS.Result.SUCCESS)
                MessageBox.Show(WsmbsRun.GetLastErrorString());

            try
            {
                TcpListener listener = new TcpListener(LocalIp.Address, LocalIp.Port);
                listener.Start();
               
                Thread acceptThread = new Thread(acceptClientConnect);
                acceptThread.Start();
                //listBox1.Items.Add(client.Client.LocalEndPoint + " ---> " + client.Client.RemoteEndPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 通讯参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comm commset = new Comm();
            commset.wsmbsControlinit = WsmbsRun;
            commset.setServerIp = ServerIp;
            commset.setLocalIp = LocalIp;
            commset.ShowDialog();
        }

        private void 断开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WsmbsRun.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        string msg = @"Welcome To TraceFact.Net";
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(ServerIp.Address.ToString() + ":" + ServerIp.Port.ToString());
            listBox1.Items.Add(LocalIp.Address.ToString() + ":" + LocalIp.Port.ToString());

            streamToServer = client.GetStream();
            byte[] buffer = Encoding.Unicode.GetBytes(msg);     // 获得缓存
            streamToServer.Write(buffer, 0, buffer.Length);     // 发往服务器
        }

        // 接受请求  
        private void acceptClientConnect()
        {
            Thread.Sleep(1000);
            try
            {
                TcpClient  rclient = listener.AcceptTcpClient();
                if (listener != null)
                {
                    NetworkStream  rstreamToServer = rclient.GetStream();
                    byte[] buffer = new byte[100];
                    int bytesRead = rstreamToServer.Read(buffer, 0, 100);

                    string msg = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                    listBox1.Items.Add(msg);
                }
            }
            catch
            {
                Thread.Sleep(1000);
            }
        }
    }
}
