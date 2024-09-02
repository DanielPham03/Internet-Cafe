﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTO
{
    public class Member
    {
        private string accountName;
        private string password;
        private string groupUser;
        private float currentMoney;
        private string statusAccount;
        
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string GroupUser
        {
            get { return groupUser; }
            set { groupUser = value; }
        }
        
        public float CurrentMoney
        {
            get { return currentMoney; }
            set { currentMoney = value; }
        }
        public string StatusAccount
        {
            get { return statusAccount; }
            set { statusAccount = value; }
        }
        
    }
}
