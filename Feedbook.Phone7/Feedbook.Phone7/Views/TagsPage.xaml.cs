using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Feedbook.Phone7.Model;
using Microsoft.Phone.Shell;
using WPCore.RefTypeExtension;
using System.Windows.Controls.Primitives;

namespace Feedbook.Phone7.Views
{
    public partial class TagsPage : PhoneApplicationPage
    {
        public TagsPage()
        {
            InitializeComponent();
            this.ApplicationBar.Buttons.Cast<ApplicationBarIconButton>().ForEach(b => b.IconUri = Helper.HelperFunction.GetThemeIcon(b.IconUri.OriginalString));
            this.LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = new List<CategoryFeed>();
            var feeds = (from channel in Storage.Channels
                         from feed in channel.Feeds
                         from category in feed.Categories
                         select new { Category = category, Feed = feed }).Distinct();

            foreach (var feed in feeds)
            {
                var categoryFeed = categories.FirstOrDefault(c => string.Equals(feed.Category.Name, c.Category, StringComparison.OrdinalIgnoreCase));
                if (categoryFeed == null)
                {
                    categoryFeed = new CategoryFeed { Category = feed.Category.Name };
                    categories.Add(categoryFeed);
                }

                categoryFeed.IsPinned = Storage.Favorites.Any(c => string.Equals(c.Category, categoryFeed.Category, StringComparison.OrdinalIgnoreCase));
                categoryFeed.Feeds.Add(feed.Feed);
            }

            this.Categories.ItemsSource = categories.OrderBy(c => c.Category);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            var button = sender as ApplicationBarIconButton;
            if (button != null && button.Text == "Done")
            {
                foreach (var checkBox in this.Categories.GetVisualDescendants().OfType<CheckBox>())
                {
                    var category = checkBox.DataContext as CategoryFeed;
                    var favorite = Storage.Favorites.FirstOrDefault(c => string.Equals(c.Category, category.Category, StringComparison.OrdinalIgnoreCase));
                    if (checkBox.IsChecked == true)
                    {
                        if (favorite == null)
                        {
                            favorite = new CategoryFeed() { Category = category.Category };
                            favorite.Feeds.AddRange((from channel in Storage.Channels
                                                     from feed in channel.Feeds
                                                     from tag in feed.Categories
                                                     where string.Equals(tag.Name, favorite.Category, StringComparison.OrdinalIgnoreCase)
                                                     select feed).Distinct());

                            Storage.Favorites.Add(favorite);

                        }
                    }
                    else
                    {
                        if (favorite != null)
                            Storage.Favorites.Remove(favorite);
                    }
                }
            }

            this.NavigationService.GoBack();
        }
    }
}