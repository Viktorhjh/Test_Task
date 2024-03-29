using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
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
using DataContext = Test_Task.Model.DataContext;

namespace Test_Task.Views
{
    /// <summary>
    /// Interaction logic for CoordinatorView.xaml
    /// </summary>
    public partial class CoordinatorView : Page
    {        
        bool isItemGrid = true;
        DataContext dataContext;
        public CoordinatorView()
        {
            InitializeComponent();
            dataContext = new DataContext();                        
        }

        private void OpenItemsGridClick(object sender, RoutedEventArgs e)
        {
            openItemGrid();
        }

        private void OpenRequestsGridClick(object sender, RoutedEventArgs e)
        {            
            openRequestGrid();
        }

        private void AddNewItemClick(object sender, RoutedEventArgs e)
        {                       
            CreateNewItemView createView = new CreateNewItemView();
            createView.Show();            
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if(isItemGrid)
            {                 
                ItemGridView itemGridView = new ItemGridView();
                DataRowView row = (DataRowView)itemGridView.itemDataGrid.SelectedItem;
                
                int id = Convert.ToInt32(row.Row[0]);

                var requestToDelete = dataContext.Request.Where(
                    request => request.ItemId == id
                    ).ToList();

                foreach( var request in requestToDelete)
                {
                    deleteRequest(request);
                }

                dataContext.Item.Remove(
                    dataContext.Item.FirstOrDefault(
                        item => item.ItemId == id
                        ));

                dataContext.SaveChanges();
                openItemGrid();
            }
            else
            {
                RequestGridView requestGridView = new RequestGridView(); 
                DataRowView row = (DataRowView)requestGridView.requestDataGrid.SelectedItem;
                int id = Convert.ToInt32(row.Row[0]);
                var requestToDelete = dataContext.Request.FirstOrDefault(
                    request => request.ItemId == id
                    );
                deleteRequest(requestToDelete);
                openRequestGrid();
            }

        }

        public void deleteRequest(Request req)
        {
            dataContext.Request.Remove(req);
            dataContext.SaveChanges();  
        }

        public void openItemGrid() 
        { 
            isItemGrid = true;
            ItemGridView itemGridView = new ItemGridView();
            MainFrame.Navigate(itemGridView);
        }

        public void openRequestGrid()
        {
            isItemGrid= false;
            RequestGridView requestGridView = new RequestGridView();
            MainFrame.Navigate(requestGridView);
        }
    }
}
