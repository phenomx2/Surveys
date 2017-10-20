using Prism.Mvvm;
using Prism.Navigation;

namespace Surveys.Core.ViewModel
{
    public abstract class ViewModelBase : BindableBase, INavigationAware
    {
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }


        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
