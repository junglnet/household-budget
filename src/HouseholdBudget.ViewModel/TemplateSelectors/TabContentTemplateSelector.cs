using System.Windows;
using System.Windows.Controls;

namespace HouseholdBudget.ViewModel
{
    public class TabContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (container is FrameworkElement element && item != null)
            {

                if (item is FundTabListViewModel)
                    return element.FindResource("FundListTemplate") as DataTemplate;
                
            }
            return null;
        }
    }
}
