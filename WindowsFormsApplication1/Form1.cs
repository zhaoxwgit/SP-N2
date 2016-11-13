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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        WSMBS.WSMBSControl WsmbsRun = new WSMBS.WSMBSControl();
        IPEndPoint ServerIp = new IPEndPoint(IPAddress.Parse("0.0.0.0"),0);
        IPEndPoint LocalIp = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
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

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(ServerIp.Address.ToString() + ":" + ServerIp.Port.ToString());
            listBox1.Items.Add(LocalIp.Address.ToString() + ":" + LocalIp.Port.ToString());
        }
    }
}
