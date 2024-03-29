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

namespace Test_Task.Views
{
    /// <summary>
    /// Interaction logic for ItemGridView.xaml
    /// </summary>
    public partial class ItemGridView : Page
    {
        public ItemGridView()
        {
            InitializeComponent();
            GetData();
        }

        private void AddNewItemClick(object sender, RoutedEventArgs e)
        {

        }

        public void GetData()
        {
            System.Windows.Data.CollectionViewSource viewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeeViewViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // employeeViewViewSource.Source = [generic data source]
            Test_Task.Test_TaskDataSet test_TaskDataSet = ((Test_Task.Test_TaskDataSet)(this.FindResource("test_TaskDataSet")));
            // Load data into the table Item. You can modify this code as needed.
            Test_Task.Test_TaskDataSetTableAdapters.ItemTableAdapter test_TaskDataSetItemTableAdapter = new Test_Task.Test_TaskDataSetTableAdapters.ItemTableAdapter();
            test_TaskDataSetItemTableAdapter.Fill(test_TaskDataSet.Item);
            System.Windows.Data.CollectionViewSource itemViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("itemViewSource")));
            itemViewSource.View.MoveCurrentToFirst();
        }      
    }
}
