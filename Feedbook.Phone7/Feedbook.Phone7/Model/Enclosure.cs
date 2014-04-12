/*
 * Author: Faraz Masood Khan 
 * 
 * Date:  Saturday, July 31, 2010 2:39 AM
 * 
 * Class: Enclosure
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
using System.Collections.ObjectModel;


namespace Feedbook.Phone7.Model
{
    public class Enclosure : Entity
    {
        #region Declarations

        protected string url = default(string);

        protected string type = default(string);

        protected long? length = default(long?);

        protected string localpath = default(string);

        protected DateTime? downloadedon = default(DateTime?);

        protected bool isdownloaded = default(bool);

        #endregion

        #region Properties

        public string Url
        {
            get { return this.url; }
            set
            {
                if (this.url != value)
                {
                    this.url = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Url"));
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

        public long? Length
        {
            get { return this.length; }
            set
            {
                if (this.length != value)
                {
                    this.length = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Length"));
                }
            }
        }

        public string LocalPath
        {
            get { return this.localpath; }
            set
            {
                if (this.localpath != value)
                {
                    this.localpath = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("LocalPath"));
                }
            }
        }

        public DateTime? DownloadedOn
        {
            get { return this.downloadedon; }
            set
            {
                if (this.downloadedon != value)
                {
                    this.downloadedon = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("DownloadedOn"));
                }
            }
        }

        public bool IsDownloaded
        {
            get { return this.isdownloaded; }
            set
            {
                if (this.isdownloaded != value)
                {
                    this.isdownloaded = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("IsDownloaded"));
                }
            }
        }

        public ObservableCollection<Link> Links { get; set; }

        #endregion

        #region Methods

        public Enclosure()
        {
            this.Links = new ObservableCollection<Link>();
        }

        #endregion
    }
}
