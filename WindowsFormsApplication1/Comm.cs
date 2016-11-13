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
    public partial class Comm : Form
    {
        public WSMBSControl wsmbsControlinit { get; set; }
        public IPEndPoint setServerIp { get; set; }
        public IPEndPoint setLocalIp { get; set; }

        public Comm()
        {
            InitializeComponent();
        }

        private void Comm_Load(object sender, EventArgs e)
        {
            comboBox2.Text = wsmbsControlinit.PortName;
            comboBox1.Text = wsmbsControlinit.BaudRate.ToString();
            if ( wsmbsControlinit.Mode == WSMBS.Mode.RTU)
            {
                radioButton1.Checked = true;
            }
            else {
                radioButton2.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("参数设置完成，点击确认退出", "设置提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                setServerIp.Address = IPAddress.Parse(textBox1.Text.Trim());
                setServerIp.Port = Convert.ToInt32(textBox2.Text.Trim());

                setLocalIp.Address = IPAddress.Parse(textBox3.Text.Trim());
                setLocalIp.Port = Convert.ToInt32(textBox4.Text.Trim());

                wsmbsControlinit.DataBits = 8;
                wsmbsControlinit.DTREnable = false;
                wsmbsControlinit.Parity = Parity.None;
                wsmbsControlinit.RemoveEcho = false;
                wsmbsControlinit.ResponseTimeout = 1000;
                wsmbsControlinit.RTSEnable = false;
                wsmbsControlinit.StopBits = 1;
                wsmbsControlinit.PortName = comboBox2.Text.Trim();
                wsmbsControlinit.BaudRate = Convert.ToInt32(comboBox1.Text.Trim());
                if (radioButton1.Checked)
                {
                    wsmbsControlinit.Mode = WSMBS.Mode.RTU;
                }
                else
                {
                    wsmbsControlinit.Mode = WSMBS.Mode.ASCII;
                }
                this.Close();
            }
        }
    }
}
