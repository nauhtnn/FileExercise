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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExercise
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
            // Configure open file dialog box
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            ////dlg.FileName = "Document"; // Default file name
            ////dlg.DefaultExt = ".txt"; // Default file extension
            ////dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            //// Show open file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            //// Process open file dialog box results
            //if (result == true)
            //{
            //    // Open document
            //    //output.Text += dlg.FileName;
            //}
        }

        

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var explorer = new TreeView();
            
            for(int i = 0; i < 10; ++i)
            {
                var it = new TreeViewItem();
                var f = new StackPanel();
                f.Orientation = Orientation.Horizontal;
                f.Children.Add(StockIconGetter.GetImage());
                var text = new TextBlock();
                text.Text = " hello " + i;
                f.Children.Add(text);
                it.Header = f;
                var pad = new Thickness(4);
                it.Padding = pad;
                for (int j = 0; j < 3; ++j)
                {
                    var ite = new TreeViewItem();
                    var folder = new StackPanel();
                    folder.Orientation = Orientation.Horizontal;
                    folder.Children.Add(StockIconGetter.GetImage());
                    var folderName = new TextBlock();
                    folderName.Text = " hello " + j;
                    folder.Children.Add(folderName);
                    ite.Header = folder;
                    var padding = new Thickness(12);
                    ite.Padding = padding;
                    it.Items.Add(ite);
                }
                explorer.Items.Add(it);
            }
            canvas.Children.Add(explorer);
        }
    }
}
