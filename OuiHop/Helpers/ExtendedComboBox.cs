using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OuiHop.Helpers
{
    public class ExtendedComboBox : ComboBox
    {
        private ScrollViewer _scrollViewer;

        public ExtendedComboBox()
        {
            DefaultStyleKey = typeof(ComboBox);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _scrollViewer = GetTemplateChild("ScrollViewer") as ScrollViewer;
            if (_scrollViewer != null)
            {
                _scrollViewer.Loaded += OnScrollViewerLoaded;
            }
        }

        private void OnScrollViewerLoaded(object sender, RoutedEventArgs e)
        {
            _scrollViewer.Loaded -= OnScrollViewerLoaded;
            _scrollViewer.ChangeView(null, 0, null);
        }
    }
}