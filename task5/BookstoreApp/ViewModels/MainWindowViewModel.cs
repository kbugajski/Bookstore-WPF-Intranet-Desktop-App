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
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    #region TopAndSideMenuCommand

    public ICommand OpenDashBoardViewCommand { get => new BaseCommand(() => CreateView(new DashboardViewModel())); }
    public ICommand OpenBooksViewCommand { get => new BaseCommand(() => CreateView(new BooksViewModel())); }
    public ICommand OpenAuthorsViewCommand { get => new BaseCommand(() => CreateView(new AuthorsViewModel())); }
    public ICommand OpenCategoriesViewCommand { get => new BaseCommand(() => CreateView(new CategoriesViewModel())); }
    public ICommand OpenCountriesViewCommand { get => new BaseCommand(() => CreateView(new CountriesViewModel())); }
    public ICommand OpenAddCountryViewCommand { get => new BaseCommand(() => CreateView(new AddCountryViewModel())); }
    public ICommand OpenCustomersViewCommand { get => new BaseCommand(() => CreateView(new CustomersViewModel())); }
    public ICommand OpenPublishersViewCommand { get => new BaseCommand(() => CreateView(new PublishersViewModel())); }
    public ICommand OpenReviewsViewCommand { get => new BaseCommand(() => CreateView(new ReviewsViewModel())); }
    public ICommand OpenInvoicesViewCommand { get => new BaseCommand(() => CreateView(new InvoicesViewModel())); }
    public ICommand OpenPaymentMethodsViewCommand { get => new BaseCommand(() => CreateView(new PaymentMethodsViewModel())); }
    public ICommand OpenAddressesViewCommand { get => new BaseCommand(() => CreateView(new AddressesViewModel())); }
    public ICommand OpenAddBookViewCommand { get => new BaseCommand(() => CreateView(new AddBookViewModel())); }
    public ICommand OpenAddCategoryViewCommand{ get => new BaseCommand(() => CreateView(new AddCategoryViewModel())); }
    public ICommand OpenAddAuthorViewCommand { get => new BaseCommand(() => CreateView(new AddAuthorViewModel())); }
    public ICommand OpenAddCustomerViewCommand { get => new BaseCommand(() => CreateView(new AddCustomerViewModel())); }
    public ICommand OpenAddPublisherViewCommand { get => new BaseCommand(() => CreateView(new AddPublisherViewModel())); }
    public ICommand OpenAddReviewViewCommand { get => new BaseCommand(() => CreateView(new AddReviewViewModel())); }
    public ICommand OpenAddInvoiceViewCommand { get => new BaseCommand(() => CreateView(new AddInvoiceViewModel())); }
    public ICommand OpenAddAddressViewCommand { get => new BaseCommand(() => CreateView(new AddAddressViewModel()));  }


    


    #endregion


    public MainWindowViewModel()
    {
        Commands = new(CreateCommands());
        Workspaces = new();
        Workspaces.CollectionChanged += OnWorkspacesChanged;
        WeakReferenceMessenger.Default.Register<OpenViewMessage>(this, (recipent, message) => OpenView(message));
    }

    #region Buttons in side bar

    public ReadOnlyCollection<CommandViewModel> Commands { get; set; }

    private List<CommandViewModel> CreateCommands()
    {
        return new()
        {
            new ("DASHBOARD", OpenDashBoardViewCommand, "GreenYellow"), 
            new ("Books", OpenBooksViewCommand, "LightBlue"),
            new ("Add Book", OpenAddBookViewCommand, "CornflowerBlue"),
            new ("Authors", OpenAuthorsViewCommand, "LightBlue"),
            new ("Add Author", OpenAddAuthorViewCommand, "CornflowerBlue"),
            new ("Countries", OpenCountriesViewCommand, "LightBlue"),
            new ("Add Country", OpenAddCountryViewCommand, "CornflowerBlue"),
            new ("Categories", OpenCategoriesViewCommand, "LightBlue"),
            new ("Add Category", OpenAddCategoryViewCommand, "CornflowerBlue"),
            new ("Addresses", OpenAddressesViewCommand, "LightBlue"),
            new ("Add Address", OpenAddAddressViewCommand, "CornflowerBlue"),
            new ("Customers", OpenCustomersViewCommand, "LightBlue"),
            new ("Add Customer", OpenAddCustomerViewCommand, "CornflowerBlue"),
            new ("Publishers", OpenPublishersViewCommand, "LightBlue"),
            new ("Add Publisher", OpenAddPublisherViewCommand, "CornflowerBlue"),
            new ("Reviews", OpenReviewsViewCommand, "LightBlue"),
            new ("Add Reivew", OpenAddReviewViewCommand, "CornflowerBlue"),
            new ("Invoices", OpenInvoicesViewCommand, "LightBlue"),
            new ("Add Invoice", OpenAddInvoiceViewCommand, "CornflowerBlue"),
           // new ("Payment Methods", OpenPaymentMethodsViewCommand, "LightBlue")
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

    private void OpenView(OpenViewMessage message)
    {
        CreateView(message.ViewModelToBeOpened);
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
