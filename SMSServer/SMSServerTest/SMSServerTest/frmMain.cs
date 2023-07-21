using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using SMSExchange;


namespace SMSServerTest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEF GHIJKLMNOPQR STUVWXYZ 0123456789 abcde fghijklmnopqr stuvwxyz ABCDEFGH IJKLMNO PQRSTUVWXYZ abcd efghijklmn opqrstu vwxyz";
            int intT = chars.Length;
            string strT = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(20, length)]).ToArray());
        }

        SMSClient _SMSClient;
        private void btnClientConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_SMSClient == null)
                {
                    _SMSClient = new SMSClient(txtClientCode.Text);
                }
                if (_SMSClient.Connect(txtServerIp.Text, (int)nudPort.Value))
                {
                    EndPoint epT = _SMSClient.LocalEndpoint;
                    this.Text = "LocalEndpoint - " + epT.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Connection fault", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _SMSClient = new SMSClient(txtClientCode.Text);
            sMS4JsonBS.DataSource = _SMSClient.OutgoingSMS;
            sMS4JsonBS.ResetBindings(false);

            responseBS.DataSource = _SMSClient.ResponseList;
            responseBS.ResetBindings(false);
        }

        private void btnClientSend_Click(object sender, EventArgs e)
        {
            SMS4Json sMS4Json = new SMS4Json();
            sMS4Json.PhoneNumber = txtPhoneNumber.Text;
            if (string.IsNullOrEmpty(txtText.Text)) sMS4Json.Text = "{" + sMS4Json.OriginalId.ToString() + "}" + RandomString(100);
            else sMS4Json.Text= txtText.Text;   

            _SMSClient.SendSMS(sMS4Json);
        }

        private void btnTest_1_50_Click(object sender, EventArgs e)
        {
            List<SMS4Json> listSMS4Json = new List<SMS4Json>();
            for (int i=0; i<50; i++)
            {
                SMS4Json sMS4Json = new SMS4Json();
                sMS4Json.PhoneNumber = txtPhoneNumber.Text;
                sMS4Json.Text = "{" + sMS4Json.OriginalId.ToString() + "}" + RandomString(100);
                listSMS4Json.Add(sMS4Json);
            }
            _SMSClient.SendSMS(listSMS4Json);
        }

        private async void btnTest_2_25_Click(object sender, EventArgs e)
        {
            var tasks = new List<Task<List<SMS4Json>>>();
            tasks.Add(Task.Run(Test_2_25));
            tasks.Add(Task.Run(Test_2_25));
            var waiter = Task.WhenAll(tasks);

            await waiter;
            foreach (Task<List<SMS4Json>> taskT in tasks)
            {
                List<SMS4Json> listSMS4Json=taskT.Result;
                foreach (SMS4Json sT in listSMS4Json)
                {
                    _SMSClient.OutgoingSMS.Insert(0, sT);
                }
            }
        }
        private List<SMS4Json> Test_2_25()
        {
            List<SMS4Json> listSMS4Json = new List<SMS4Json>();
            for (int i = 0; i < 25; i++)
            {
                SMS4Json sMS4Json = new SMS4Json();
                sMS4Json.PhoneNumber = txtPhoneNumber.Text;
                sMS4Json.Text = "{" + sMS4Json.OriginalId.ToString() + "}" + RandomString(100);
                listSMS4Json.Add(sMS4Json);
            }
            _SMSClient.SendSMS_Test(listSMS4Json);
            return listSMS4Json;
        }

        private List<SMS4Json> Test_10_5()
        {
            List<SMS4Json> listSMS4Json = new List<SMS4Json>();
            for (int i = 0; i < 5; i++)
            {
                SMS4Json sMS4Json = new SMS4Json();
                sMS4Json.PhoneNumber = txtPhoneNumber.Text;
                sMS4Json.Text = "{" + sMS4Json.OriginalId.ToString() + "}" + RandomString(100);
                listSMS4Json.Add(sMS4Json);
            }
            _SMSClient.SendSMS_Test(listSMS4Json);
            return listSMS4Json;
        }
    }
}
