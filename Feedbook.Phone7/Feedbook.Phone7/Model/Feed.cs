/*
 * Author: Faraz Masood Khan 
 * 
 * Date:  Saturday, July 31, 2010 2:45 AM
 * 
 * Class: Feed
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
using System.Net;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Feedbook.Phone7.Model
{
    public class Feed : Entity
    {
        #region Declarations

        protected string guid = default(string);

        protected string title = default(string);

        protected string commenturl = default(string);

        protected int like = default(int);

        protected bool isliked = default(bool);

        protected bool isprotected = default(bool);

        protected DateTime updated = default(DateTime);

        protected bool isreaded = default(bool);

        protected Link link;

        #endregion

        #region Properties

        public string Guid
        {
            get { return this.guid; }
            set
            {
                if (this.guid != value)
                {
                    this.guid = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Guid"));
                }
            }
        }

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

        public string CommentUrl
        {
            get { return this.commenturl; }
            set
            {
                if (this.commenturl != value)
                {
                    this.commenturl = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("CommentUrl"));
                }
            }
        }

        public int Likes
        {
            get { return this.like; }
            set
            {
                if (this.like != value)
                {
                    this.like = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Likes"));
                }
            }
        }

        public bool IsLiked
        {
            get { return this.isliked; }
            set
            {
                if (this.isliked != value)
                {
                    this.isliked = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("IsLiked"));
                }
            }
        }

        public bool IsProtected
        {
            get { return this.isprotected; }
            set
            {
                if (this.isprotected != value)
                {
                    this.isprotected = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("IsProtected"));
                }
            }
        }

        public DateTime Updated
        {
            get { return this.updated; }
            set
            {
                if (this.updated != value)
                {
                    this.updated = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Updated"));
                }
            }
        }

        public bool IsReaded
        {
            get { return this.isreaded; }
            set
            {
                if (this.isreaded != value)
                {
                    this.isreaded = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("IsReaded"));
                }
            }
        }

        public Link Link
        {
            get { return this.link; }
            set
            {
                if (this.link != value)
                {
                    this.link = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Link"));
                }
            }
        }

        public Person Author
        {
            get
            {
                var author = this.People.FirstOrDefault(p => p.PersonRole == PersonRole.Author);
                return author != null ? author : this.People.FirstOrDefault();
            }
        }

        private string textDescription;

        private string encodedDescription;

        public string TextDescription
        {
            get
            {
                if (this.textDescription == null)
                {
                    try
                    {
                        if (this.Descriptions.Count(content => content.Type == Content.TEXT) > 0)
                            this.textDescription = HttpUtility.HtmlDecode(this.Descriptions.First(content => content.Type == Content.TEXT).Value);
                        else if (this.Descriptions.Count(content => content.Type == Content.XHTML) > 0)
                        {
                            this.textDescription = this.Descriptions.First(content => content.Type == Content.XHTML).ToPlainText;
                            this.textDescription = HttpUtility.HtmlDecode(this.textDescription);
                        }
                        else if (this.Descriptions.Count > 0)
                            this.textDescription = HttpUtility.HtmlDecode(this.Descriptions[0].Value);
                    }
                    catch { }
                }

                return this.textDescription;
            }
        }

        public string EncodedDescription
        {
            get
            {
                if (this.encodedDescription == null)
                {
                    if (this.Descriptions.Count(content => content.Type == Content.XHTML) > 0)
                        this.encodedDescription = this.Descriptions.Where(content => content.Type == Content.XHTML).First().Value;
                    else if (this.Descriptions.Count(content => content.Type == Content.HTML) > 0)
                        this.encodedDescription = this.Descriptions.Where(content => content.Type == Content.HTML).First().Value;
                    else if (this.Descriptions.Count > 0)
                        this.encodedDescription = this.Descriptions[0].Value;
                }

                return this.encodedDescription;
                //return  "<div style=\"font-family:san-sarif;\">" + this.encodedDescription + "</div>";
            }
        }  

        public ObservableCollection<Comment> Comments { get; set; }

        public ObservableCollection<Enclosure> Enclosures { get; set; }

        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<Content> Descriptions { get; set; }

        public ObservableCollection<Person> People { get; set; }

        #endregion

        #region Methods

        public Feed()
        {
            this.Comments = new ObservableCollection<Comment>();
            this.Enclosures = new ObservableCollection<Enclosure>();
            this.Categories = new ObservableCollection<Category>();
            this.Descriptions = new ObservableCollection<Content>();
            this.People = new ObservableCollection<Person>();
        }

        #endregion
    }
}
