using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.ViewModels.Many;
using BookstoreApp.ViewModels;
using BookstoreApp.Views.Many;
using System.Windows;

namespace BookstoreApp.Helpers
{
    public static class WindowManager
    {
        public static void OpenWindow(WorkspaceViewModel workspaceView)
        {
            Window window = new Window()
            {
                Owner = App.Current.MainWindow,
                Height = 400,
                Width = 600,
            };
            if (workspaceView.GetType() == typeof(CountriesWithCallbackViewModel))
            {
                window.Content = new CountriesView()
                {
                    DataContext = workspaceView
                };
            }
            if (workspaceView.GetType() == typeof(CustomersWithCallbackViewModel))
            {
                window.Content = new CustomersView()
                {
                    DataContext = workspaceView
                };
            }
            if (workspaceView.GetType() == typeof(BooksWithCallbackViewModel))
            {
                window.Content = new BooksView()
                {
                    DataContext = workspaceView
                };
            }

            workspaceView.RequestClose += (sender, e) =>
            {
                window.Close();
            };
            App.Current.MainWindow.Opacity = 0.5;

            window.Closing += (sender, e) =>
            {
                App.Current.MainWindow.Opacity = 1;
            };
            window.ShowDialog();
        }
    }
}
