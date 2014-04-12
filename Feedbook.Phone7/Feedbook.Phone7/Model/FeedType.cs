/*/
 *
 * Author: Faraz Masood Khan
 * 
 * Date: Monday, August 25, 2008 12:32 AM
 * 
 * Class: FeedType
 * 
 * Email: mk.faraz@gmail.com
 * 
 * Blogs: http://farazmasoodkhan.wordpress.com
 *
 * Website: http://www.linkedin.com/in/farazmasoodkhan
 *
 * Copyright: Faraz Masood Khan @ Copyright ©  2008
 * 
/*/

using System.ComponentModel;

namespace Feedbook.Phone7.Model
{
    public enum FeedType
    {
        [DescriptionAttribute("RSS 0.91")]
        RSS_091,

        [DescriptionAttribute("RSS 0.92")]
        RSS_092,

        [DescriptionAttribute("RSS 0.93")]
        RSS_093,

        [DescriptionAttribute("RSS 0.94")]
        RSS_094,

        [DescriptionAttribute("RSS 1.0")]
        RSS_100,

        [DescriptionAttribute("RSS 2.0")]
        RSS_200,

        [DescriptionAttribute("RSS 2.0.1")]
        RSS_201,

        [Description("Atom 1.0")]
        Atom_100,
    }
}