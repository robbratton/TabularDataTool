using System;
using System.Data;
using System.Windows;
using TabularDataHelper;
using TabularDataHelper.Parsers;

namespace TabularDataViewer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TabularFile _file;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _file = new TabularFile(new DelimitedFileParser(HasHeader.IsChecked.HasValue && HasHeader.IsChecked.Value));
            _file.Load(Filename.Text);

            Output.ItemsSource = _file.DataTable.AsDataView();
        }
    }
}