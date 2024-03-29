using System.Windows;
using System.Windows.Controls;
using Test_Task.Views;

namespace Test_Task
{
    
    public partial class EmployeeView : Page
    {

        public EmployeeView()
        {
            InitializeComponent();                   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ItemGridView itemGridView = new ItemGridView();
            MainFrame.Navigate(itemGridView);
        }

        private void CreateNewOrder(object sender, RoutedEventArgs e)
        {
            CreateNewOrderView createNewOrderView = new CreateNewOrderView();
            createNewOrderView.Show();
            
        }
    }
}
