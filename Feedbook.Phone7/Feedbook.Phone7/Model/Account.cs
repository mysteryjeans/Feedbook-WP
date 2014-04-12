/*
 * Author: Faraz Masood Khan 
 * 
 * Date:  Saturday, July 31, 2010 2:15 AM
 * 
 * Class: Account
 * 
 * Email: mk.faraz@gmail.com
 * 
 * Blogs: http://csharplive.wordpress.com, http://farazmasoodkhan.wordpress.com
 *
 * Website: http://www.linkedin.com/in/farazmasoodkhan
 *
 * Copyright: Faraz Masood Khan @ Copyright 2010
 *
/*/

using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Feedbook.Phone7.Model
{
    public class Account : Entity
    {
        #region Declarations

        protected string fullname = default(string);

        protected string username = default(string);

        protected AccountType accounttype = default(AccountType);

        protected string password = default(string);

        protected string imageurl = default(string);

        protected string token = default(string);

        protected string tokensecret = default(string);

        #endregion

        #region Properties

        public string FullName
        {
            get { return this.fullname; }
            set
            {
                if (this.fullname != value)
                {
                    this.fullname = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
                }
            }
        }

        public string UserName
        {
            get { return this.username; }
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
                }
            }
        }

        public AccountType AccountType
        {
            get { return this.accounttype; }
            set
            {
                if (this.accounttype != value)
                {
                    this.accounttype = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("AccountType"));
                }
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Password"));
                }
            }
        }

        public string ImageUrl
        {
            get { return this.imageurl; }
            set
            {
                if (this.imageurl != value)
                {
                    this.imageurl = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("ImageUrl"));
                }
            }
        }

        public string Token
        {
            get { return this.token; }
            set
            {
                if (this.token != value)
                {
                    this.token = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Token"));
                }
            }
        }

        public string TokenSecret
        {
            get { return this.tokensecret; }
            set
            {
                if (this.tokensecret != value)
                {
                    this.tokensecret = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("TokenSecret"));
                }
            }
        }

        public ObservableCollection<Channel> Channels { get; set; }

        #endregion

        public Account()
        {
            this.Channels = new ObservableCollection<Channel>();
        }
    }
}
