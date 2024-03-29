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
using System.Windows.Shapes;
using Test_Task.Model;

namespace Test_Task.Views
{
    /// <summary>
    /// Interaction logic for CreateNewItemView.xaml
    /// </summary>
    public partial class CreateNewItemView : Window
    {
        public Item item{ get; }                
        List<string> UnitOfMeasurement = new List<string> { "kg", "l", "m", "m²", "m³" };
        List<string> status = new List<string>() { "New", "Approve", "Reject"};

        public CreateNewItemView()
        {
            InitializeComponent();
            item = new Item();
            
            UnitTextBox.ItemsSource = UnitOfMeasurement;
            StatusTextBox.ItemsSource = status;
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
            DataContext dataContext = new DataContext();

            item.ItemGroup = ItemGroupTextBox.Text;
            item.UnitOfMeasurement = UnitTextBox.Text;
            item.Status = StatusTextBox.Text;
            item.Quantity = Convert.ToInt32(QuantityTextBox.Text);
            item.PriceWithVAT = Convert.ToInt32(PriceTextBox.Text);
            item.ContactPerson = ContactPersonTextBox.Text;
            item.StorageLocation = StorageLocationTextBox.Text;
            //item.Photo = PhotoTextBox.Text;

            dataContext.Item.Add(item);
            dataContext.SaveChanges();

            MessageBox.Show("Item created", "Success!");
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
