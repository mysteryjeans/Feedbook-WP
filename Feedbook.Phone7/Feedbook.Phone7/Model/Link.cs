/*
 * Author: Faraz Masood Khan 
 * 
 * Date:  Saturday, July 31, 2010 5:16 PM
 * 
 * Class: Link
 * 
 * Email: mk.faraz@gmail.com
 * 
 * Blogs: http://csharplive.wordpress.com, http://farazmasoodkhan.wordpress.com
 *
 * Website: http://www.linkedin.com/in/farazmasoodkhan
 *
 * Copyright: Faraz Masood Khan @ Copyright 2009
 *
/*/

using System;
using System.ComponentModel;
using WPCore.ValueTypeExtension;

namespace Feedbook.Phone7.Model
{
    public class Link : Entity
    {
        #region Declarations
                
        protected string title = default(string);

        protected string href = default(string);

        protected string type = default(string);

        protected string rel = default(string);

        #endregion

        #region Properties
        
        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public string HRef
        {
            get { return this.href; }
            set
            {
                if (this.href != value)
                {
                    this.href = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("HRef"));
                }
            }
        }

        public string Type
        {
            get { return this.type; }
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Type"));
                }
            }
        }

        public string Rel
        {
            get { return this.rel; }
            set
            {
                if (this.rel != value)
                {
                    this.rel = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Rel"));
                }
            }
        }

        public LinkRel LinkRel
        {
            get { return this.Rel.ToEnum<LinkRel>(); }
            set { this.Rel = value.ToString(); }
        }

        #endregion

        public static bool IsLinkEqual(Link left, Link right)
        {
            return (left != null && right != null && left.href == right.href);
        }
    }
}
