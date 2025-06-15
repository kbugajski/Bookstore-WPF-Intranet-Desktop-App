using BookstoreApp.Helpers;
using BookstoreApp.ViewModels;
using BookstoreApp.ViewModels.Many;
using BookstoreApp.ViewModels.Single;
using BookstoreApp.Views.Single;
using BookstoreApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookstoreApp.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    #region TopAndSideMenuCommand

    public ICommand OpenBooksViewCommand { get => new BaseCommand(() => CreateView(new BooksViewModel())); }
    public ICommand OpenAuthorsViewCommand { get => new BaseCommand(() => CreateView(new AuthorsViewModel())); }
    public ICommand OpenAddBookViewCommand { get => new BaseCommand(() => CreateView(new AddBookViewModel())); }
    public ICommand OpenAddAuthorViewCommand { get => new BaseCommand(() => CreateView(new AddAuthorViewModel())); }

    #endregion


    public MainWindowViewModel()
    {
        Commands = new(CreateCommands());
        Workspaces = new();
        Workspaces.CollectionChanged += OnWorkspacesChanged;
    }

    #region Buttons in side bar

    public ReadOnlyCollection<CommandViewModel> Commands { get; set; }

    private List<CommandViewModel> CreateCommands()
    {
        return new()
        {
            new ("Books", OpenBooksViewCommand, "LightBlue"),
            new ("Authors", OpenAuthorsViewCommand, "LightBlue"),
            new ("Add Book", OpenAddBookViewCommand, "CornflowerBlue"),
            new ("Add Author", OpenAddAuthorViewCommand, "CornflowerBlue"),
        };
    }

    #endregion

    #region Tabs

    public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }
    private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null && e.NewItems.Count != 0)
            foreach (WorkspaceViewModel workspace in e.NewItems)
                workspace.RequestClose += onWorkspaceRequestClose;

        if (e.OldItems != null && e.OldItems.Count != 0)
            foreach (WorkspaceViewModel workspace in e.OldItems)
                workspace.RequestClose -= onWorkspaceRequestClose;
    }

    private void onWorkspaceRequestClose(object sender, EventArgs e)
    {
        WorkspaceViewModel? workspace = sender as WorkspaceViewModel;
        if (workspace != null)
        {
            Workspaces.Remove(workspace);
        }
    }

    #endregion

    #region Helepers

    public void CreateView(WorkspaceViewModel workspace)
    {
        Workspaces.Add(workspace);
        SetActiveWorkspace(workspace);
    }

    private void CreateListView<WorkspaceViewModelType>() where WorkspaceViewModelType : WorkspaceViewModel, new()
    {
        WorkspaceViewModel? workspace = Workspaces.FirstOrDefault(vm => vm is WorkspaceViewModelType);
        if (workspace == null)
        {
            workspace = new WorkspaceViewModelType();
            Workspaces.Add(workspace);
        }
        SetActiveWorkspace(workspace);
    }

    private void SetActiveWorkspace(WorkspaceViewModel workspace)
    {
        Debug.Assert(Workspaces.Contains(workspace));

        ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
        if (collectionView != null)
            collectionView.MoveCurrentTo(workspace);
    }

    #endregion
}
