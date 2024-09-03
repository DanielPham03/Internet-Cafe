﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        public ClientManager clientManager;
        const int USECLIENT = 101;
        const int MEMBERLOGIN = 102;
        const int PAYMENT = 103;
        int hour = 0;
        int min = 0;
        int sec = 0;
        double money = 0;
        TimeSpan total;
        TimeSpan use;
        TimeSpan remain;
        string userName = "";
        public ClientForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            clientManager = new ClientManager();
            money = clientManager.clientPrice;
            ClientManager.requestServer = -1;
            timerProgram.Enabled = true;
            timerProgram.Interval = 1000;
            timerProgram.Start();
            this.Enabled = false;
        }
        public ClientForm(ClientManager x)
        {
            this.clientManager = x;
            InitializeComponent();

        }
        private void MainFormClient_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }

        private void grbUser_Enter(object sender, EventArgs e)
        {

        }

        private void timerProgram_Tick(object sender, EventArgs e)
        {
            try
            {
                
                if (ClientManager.requestServer != -1)
                {
                    if (ClientManager.requestServer == MEMBERLOGIN)
                    {
                        this.Enabled = true;
                        grbUser.Text = "Người dùng: " + clientManager.userName;
                        txtCurrentMoney.Text = currencyFormat(clientManager.totalMoney);
                        userName = clientManager.userName;
                        ClientManager.requestServer = -2;
                        min = 0;
                        ResetTime();
                    }
                    TimeCount();
                    if (ClientManager.requestServer == -2)
                    {
                        use = TimeSpan.Parse(txtUsedTime.Text.ToString());
                        double remainingMoney = clientManager.totalMoney - money;
                        txtRemainingMoney.Text = currencyFormat(remainingMoney);
                        txtUseTimeFee.Text = "0";
                        clientManager.updateMoney(userName, Math.Round(remainingMoney, 0, MidpointRounding.AwayFromZero), use);
                    }
                    MoneyCount(txtUsedTime.Text.ToString());
                }
            }catch{
                Application.Exit();
            }
        }
        private void ResetTime()
        {
            min = 0;
            sec = 0;
            hour = 0;
            money = 0;
        }
        
        private void MoneyCount(String useTime)
        {
            int time = ChangeUseTimeToMinutes(useTime);
            txtUseTimeFee.Text = currencyFormat(money);
            money += clientManager.clientPrice / 3600;
        }
        private int ChangeUseTimeToMinutes(String useTime)
        {
            int minutes = 0;
            string[] arrListStr = useTime.Split(':');
            if (int.Parse(arrListStr[1]) > 0)
            {
                minutes = minutes + int.Parse(arrListStr[1]);
            }

            if (int.Parse(arrListStr[0]) > 0)
            {
                minutes = minutes + int.Parse(arrListStr[0]) * 60;
            }

            return minutes;
        }
        private void TimeCount()
        {
            txtUsedTime.Text = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
            sec++;
            if (sec > 59)
            {
                min++;
                sec = 00;
            }

            if (min > 59)
            {
                hour++;
                min = 00;
            }

            if (hour > 99)
            {
                timerProgram.Stop();
                timerProgram.Enabled = true;
            }

        }

        private void pnlLogout_Click(object sender, EventArgs e)
        {
            
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn đăng xuất.", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timerProgram.Start();
                txtCurrentMoney.Clear();
                txtRemainingMoney.Clear();
                txtServiceFee.Clear();
                txtUsedTime.Clear();
                txtUseTimeFee.Clear();
                grbUser.Text = "Username";
                ClientManager.requestServer = -1;
                clientManager.LogoutMember(userName);
            }
    }
        private string currencyFormat(double money) => string.Format(new CultureInfo("vi-VN"), "{0:C}", money);
        

        private void chat_Click(object sender, EventArgs e)
        {

        }

        private void addMoney_Click(object sender, EventArgs e)
        {

        }

        private void menu_Click(object sender, EventArgs e)
        {

        }

        private void changePass_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(this, this.clientManager);
            changePasswordForm.ShowDialog();
        }
    }
}
