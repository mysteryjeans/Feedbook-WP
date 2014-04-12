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
using System.Windows.Data;

namespace Feedbook.Phone7.Helper
{
    public interface IDataContextChanged<T> where T : FrameworkElement
    {
        void DataContextChanged(T sender, DependencyPropertyChangedEventArgs e);
    }

    public static class DataContextChangedHelper<T> where T : FrameworkElement, IDataContextChanged<T>
    {
        private const string INTERNAL_CONTEXT = "InternalDataContext";

        public static readonly DependencyProperty InternalDataContextProperty =
            DependencyProperty.Register(INTERNAL_CONTEXT,
                                        typeof(Object),
                                        typeof(T),
                                        new PropertyMetadata(_DataContextChanged));

        private static void _DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            T control = (T)sender;
            control.DataContextChanged(control, e);
        }

        public static void Bind(T control)
        {
            control.SetBinding(InternalDataContextProperty, new Binding());
        }
    }

}
