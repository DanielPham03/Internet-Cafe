﻿using Server.BLL;
using Server.DTO;
using Server.socket_configure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.GUI
{
    public partial class AddMoneyRequestItem : UserControl
    {
        private AddMoneyTransaction transaction;
        private ProcessClient processClient = new ProcessClient();
        private ProcessAddMoney processAddMoney = new ProcessAddMoney();    
        private ServerManager serverManager;  
        public AddMoneyRequestItem(ServerManager serverManager)
        {
            InitializeComponent();
            this.serverManager = serverManager;
        }
        public AddMoneyTransaction AddMoneyTransaction
        {
            get { return transaction; }
            set
            {
                transaction = value;
                lblClientName.Text = processClient.getClientName(transaction.ClientIP);
                lblUserName.Text = transaction.MemberName;
                lblMoney.Text = transaction.AddMoney.ToString();
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (processAddMoney.checkDone(this.transaction))
            {
                ServerManager.addMoney = 0;
            }
        }
    }
}
