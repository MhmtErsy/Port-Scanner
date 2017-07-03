using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;


namespace Odev2_Siber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int control = 0;
        private void btnTara_Click(object sender, EventArgs e)
        {
            lstPortlar.Text = "";
            control = int.Parse(txtBasPort.Text);
        etiket:
            try
            {
                TcpClient tcp = new TcpClient();
                control = control + 1;
                tcp.Connect(txtIP.Text, control);
                
                if (tcp.Connected)
                {
                    if (control != int.Parse(txtBitPort.Text)+1)
                    {

                        lstPortlar.Items.Add(control.ToString()+ " PORTU AÇIK");

                        goto etiket;

                    }
                }
            }
            catch
            {
                if (control != int.Parse(txtBitPort.Text)+1)
                {

                    lstPortlar.Items.Add(control.ToString() + " PORTU KAPALI");
                    
                    goto etiket;
                }
                else
                {
                }
            }
            
            
            
        }
        private void btnPing_Click(object sender, EventArgs e)
        {
            bool pinging = false;

            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(txtHostname.Text);
                pinging = reply.Status == IPStatus.Success;
            }
            catch (PingException pe)
            {
                MessageBox.Show(pe.Message.ToString());
            }
            if (pinging)
                lblDurum.Text = txtHostname.Text + " is Alive....";
            else
                lblDurum.Text = txtHostname.Text + " is Dead....";
        }

        private void btnMacArp_Click(object sender, EventArgs e)
        {
            string TEMP = "";
            foreach (NetworkInterface NI in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties IPPS = NI.GetIPProperties();
                TEMP += "CİHAZ BİLGİSİ: "+Environment.NewLine;
                foreach (UnicastIPAddressInformation ADRS in IPPS.UnicastAddresses)
                {
                   TEMP += ADRS.Address.ToString() + " MAC: "+NI.GetPhysicalAddress()+Environment.NewLine;
                   
                }
                TEMP += Environment.NewLine;
            }
            txtSonuc.Text = TEMP;
           

        }

        private void btnicmbps_Click(object sender, EventArgs e)
        {
        }
    }
}
