using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using DevExpress.Xpf.Grid;
using System.Windows.Data;
using System.Windows.Media;

namespace E2019 {
    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();
            grid.ItemsSource = IssueList.GetData();
        }
        private void OnColumnsGenerated(object sender, RoutedEventArgs e) {
            foreach (GridColumn column in grid.Columns) {
                switch (column.FieldName) {
                    case "IssueName":
                        column.CellTemplate = Application.Current.MainWindow.Resources["IssueNameTemplate"] as DataTemplate;
                        column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        break;
                    case "IssueType":
                        column.CellTemplate = Application.Current.MainWindow.Resources["IssueTypeTemplate"] as DataTemplate;
                        break;
                    case "ID":
                        column.Visible = false;
                        break;
                }
            }
        }
        public class IssueList {
            static public List<IssueDataObject> GetData() {
                List<IssueDataObject> data = new List<IssueDataObject>();
                data.Add(new IssueDataObject() {
                    ID = 0,
                    IssueName = "Transaction History", IssueType = "Bug", IsPrivate = true
                });
                data.Add(new IssueDataObject() {
                    ID = 1,
                    IssueName = "Ledger: Inconsistency", IssueType = "Bug", IsPrivate = false
                });
                data.Add(new IssueDataObject() {
                    ID = 2,
                    IssueName = "Data Import", IssueType = "Request", IsPrivate = false
                });
                data.Add(new IssueDataObject() {
                    ID = 3,
                    IssueName = "Data Archiving", IssueType = "Request", IsPrivate = true
                });
                return data;
            }
        }
        public class IssueDataObject {
            public int ID { get; set; }
            public string IssueName { get; set; }
            public string IssueType { get; set; }
            public bool IsPrivate { get; set; }
        }
    }

    public class IssueTypeForegroundConverter : IValueConverter {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return null;

            string issueType = value.ToString();
            if (issueType == "Bug")
                return new SolidColorBrush(Colors.Red);

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new System.NotImplementedException();
        }
    }

}
