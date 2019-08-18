using System.Windows;
using System.Windows.Controls;
using HouseholdBudget.ViewModel;

namespace HouseholdBudget.TemplateSelectors
{
    public class TabContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (container is FrameworkElement element && item != null)
            {

                if (item is FundTabViewModel)
                    return element.FindResource("FundListTemplate") as DataTemplate;
                
            }
            return null;
        }
    }
}
