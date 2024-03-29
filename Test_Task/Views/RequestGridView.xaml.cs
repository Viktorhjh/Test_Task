using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
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
    /// Interaction logic for RequestGridView.xaml
    /// </summary>
    public partial class RequestGridView : Page
    {
        public RequestGridView()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {            
            CollectionViewSource viewSource = (CollectionViewSource)this.FindResource("employeeViewViewSource");
            // Load data by setting the CollectionViewSource.Source property:
            // employeeViewViewSource.Source = [generic data source]
            Test_Task.Test_TaskDataSet test_TaskDataSet = ((Test_Task.Test_TaskDataSet)(this.FindResource("test_TaskDataSet")));
            // Load data into the table Item. You can modify this code as needed.
            Test_Task.Test_TaskDataSetTableAdapters.RequestTableAdapter test_TaskDataSetItemTableAdapter = new Test_Task.Test_TaskDataSetTableAdapters.RequestTableAdapter();
            test_TaskDataSetItemTableAdapter.Fill(test_TaskDataSet.Request);
            System.Windows.Data.CollectionViewSource itemViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("requestViewSource")));
            itemViewSource.View.MoveCurrentToFirst();
        }

        private void requestDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)requestDataGrid.SelectedItem;
            int id = Convert.ToInt32(row.Row[0]);

            CreateNewOrderView createNewOrderView = new CreateNewOrderView(id);
            createNewOrderView.Show();
        }
    }
}
