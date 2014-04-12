using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Feedbook.Phone7.Model
{
    public enum PersonRole
    {
        [Description("Managing Editor")]
        ManagingEditor,

        [Description("Webmaster")]
        WebMaster,

        [Description("Author")]
        Author,

        [Description("Contributor")]
        Contributor
    }
}
