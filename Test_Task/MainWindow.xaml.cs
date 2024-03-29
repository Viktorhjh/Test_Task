using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test_Task.Views;

namespace Test_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeView employeeView = new EmployeeView();                              
            MainFrame.Navigate(employeeView);            

            CoordinatorButton.Visibility = Visibility.Collapsed;
            EmployeeButton.Visibility = Visibility.Collapsed;
        }

        private void OpenCoordinator(object sender, RoutedEventArgs e)
        {
            CoordinatorView coordinatorView = new CoordinatorView();
            MainFrame.Navigate(coordinatorView);


            CoordinatorButton.Visibility = Visibility.Collapsed;
            EmployeeButton.Visibility = Visibility.Collapsed;
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(null);

            CoordinatorButton.Visibility = Visibility.Visible;
            EmployeeButton.Visibility = Visibility.Visible;
        }
    }
}
