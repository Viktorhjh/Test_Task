using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CreateNewOrderView.xaml
    /// </summary>
    public partial class CreateNewOrderView : Window
    {
        public Request request { get; }
        List<string> itemGroup;
        DataContext dataContext;
        List<Item> items;
        List<string> UnitOfMeasurement = new List<string> { "kg", "l", "m", "m²", "m³"};
        List<string> statuses = new List<string> { "New", "Approved", "Reject"};
        bool isUpdate;
        int oldId;

        public CreateNewOrderView()
        {
            InitializeComponent();
            dataContext = new DataContext();
            request = new Request();

            items = dataContext.Item.ToList();
            itemGroup = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                itemGroup.Add(items[i].ItemGroup);
            }
            
            ItemNameTextBox.ItemsSource = itemGroup;
            UnitTextBox.ItemsSource = UnitOfMeasurement;
            StatusTextBox.ItemsSource = statuses;
            StatusTextBox.SelectedIndex = 0;
            StatusTextBox.IsEnabled = false;
            isUpdate = false;
        }

        public CreateNewOrderView(int id)
        {
            InitializeComponent();
            dataContext = new DataContext();
            request = new Request();

            items = dataContext.Item.ToList();
            itemGroup = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                itemGroup.Add(items[i].ItemGroup);
            }

            ItemNameTextBox.ItemsSource = itemGroup;
            UnitTextBox.ItemsSource = UnitOfMeasurement;
            StatusTextBox.ItemsSource = statuses;   

            request = dataContext.Request.FirstOrDefault(request => request.RequestId == id);
            oldId = request.RequestId;
            string itemName = dataContext.Item.FirstOrDefault(item => item.ItemId == request.ItemId).ItemGroup;
            ItemNameTextBox.SelectedItem = itemName;
            PriceTextBox.Text = request.PriceWithVAT.ToString();
            UnitTextBox.Text = request.UnitOfMeasurement.ToString();
            QuantityTextBox.Text = request.Quantity.ToString();
            CommentTextBox.Text = request.Comment.ToString();
            StatusTextBox.Text = request.Status;
            StatusTextBox.IsEnabled = true;
            isUpdate = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Check())
            {
                MessageBox.Show("Error data!");
                return;
            }
                
            request.ItemId = items[ItemNameTextBox.SelectedIndex].ItemId;
            request.EmployeeName = "Employee";
            request.Comment = CommentTextBox.Text;
            request.PriceWithVAT = Convert.ToInt32(PriceTextBox.Text);
            request.UnitOfMeasurement = UnitTextBox.Text;
            request.Quantity = Convert.ToInt32(QuantityTextBox.Text);
            request.Status = StatusTextBox.Text;
            
            if (!isUpdate)
            {
                request.RequestId = dataContext.Request.Count() + 1;                
            }
            else
            {
                request.RequestId = oldId;
                if (request.Status == "Approved")
                {
                    Item itemQuantity = dataContext.Item.FirstOrDefault(item => item.ItemId == request.ItemId);
                    itemQuantity.Quantity -= request.Quantity;
                    dataContext.Item.AddOrUpdate(itemQuantity);
                }               
            }
            
            dataContext.Request.AddOrUpdate(request);
            dataContext.SaveChanges();
            MessageBox.Show("Request created", "Success!");
      
            this.Close();            
        }

        public bool Check()
        {
            string pattern = @"^\d+$";
            if (ItemNameTextBox.SelectedIndex == -1) return false;
            if(!Regex.IsMatch(PriceTextBox.Text, pattern)) return false;
            if (UnitTextBox.SelectedIndex == -1) return false;
            if(!Regex.IsMatch(QuantityTextBox.Text, pattern)) return false;

            return true;
        }

    }
}
