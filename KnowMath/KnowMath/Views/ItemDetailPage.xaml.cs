using System.ComponentModel;
using Xamarin.Forms;
using KnowMath.ViewModels;

namespace KnowMath.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}