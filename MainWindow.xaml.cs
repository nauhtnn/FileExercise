﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExercise
{
    enum FILE_EXERCISE_FMT
    {
        PROBLEM_DESC,
        FOLDER_COUNT
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Problem problem;

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

        private TreeView Fixed10()
        {
            var explorer = new TreeView();

            for (int i = 0; i < 10; ++i)
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
                    //ite.IsExpanded = true;
                    it.Items.Add(ite);
                }
                it.IsExpanded = true;
                explorer.Items.Add(it);
            }
            return explorer;
        }

        private TreeView GenerateTreeView()
        {
            FolderModel folderModel = FolderModelFactory.Generate(null, 0, 4, 0, 4);
            var root = new TreeViewItem();
            root.Header = folderModel.Name;
            GenerateTreeViewItem(folderModel, ref root);
            var explorer = new TreeView();
            explorer.Items.Add(root);
            return explorer;
        }

        private void GenerateTreeViewItem(FolderModel parentFolder, ref TreeViewItem parentView)
        {
            foreach (FolderModel childFolder in parentFolder.Children)
            {
                var it = new TreeViewItem();
                var f = new StackPanel();
                f.Orientation = Orientation.Horizontal;
                f.Children.Add(StockIconGetter.GetImage());
                var text = new TextBlock();
                text.Text = childFolder.Name;
                f.Children.Add(text);
                it.Header = f;
                var pad = new Thickness(4);
                it.Padding = pad;
                it.IsExpanded = true;
                GenerateTreeViewItem(childFolder, ref it);
                parentView.Items.Add(it);
            }
        }

        private void NextProblem()
        {
            problem.Next();

            ProblemDesc.Text = problem.Desc[FILE_EXERCISE_FMT.PROBLEM_DESC.ToString()];
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var explorer = GenerateTreeView();
            canvas.Children.Add(explorer);

            problem = new Problem("FileExercise");
            problem.ReadMap();
            problem.LoadID();

            NextProblem();
        }
    }
}
