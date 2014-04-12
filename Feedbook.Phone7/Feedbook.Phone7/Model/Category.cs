/*
 * Author: Faraz Masood Khan 
 * 
 * Date:  Saturday, July 31, 2010 2:15 AM
 * 
 * Class: Category
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
using System.ComponentModel;

namespace Feedbook.Phone7.Model
{
    public class Category : Entity
    {
        #region Declarations

        protected string name = default(string);

        protected string domain = default(string);

        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public string Domain
        {
            get { return this.domain; }
            set
            {
                if (this.domain != value)
                {

                    this.domain = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Domain"));
                }
            }
        }

        #endregion
    }
}
