using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;

namespace WpfApp1.Models.ViewModels;

class MainWindowViewModel : BindableBase, INotifyPropertyChanged
{
    #region Properties

    private List<User> _users;
    public List<User> Users
    {
        get { return _users; }
        set {  SetProperty(ref _users, value); }
    }

    private User _selectedUser;
    public User SelectedUser
    {
        get { return _selectedUser; }
        set { SetProperty(ref _selectedUser, value); }
    }

    private bool _isLoadData;
    public bool IsLoadData
    {
        get { return _isLoadData; }
        set { SetProperty(ref _isLoadData, value); }
    }

    private string _responseMessage = "Welcome to REST API Tutorials";
    public string ResponseMessage
    {
        get { return _responseMessage; }
        set { SetProperty(ref _responseMessage, value); }
    }
    #endregion

    private bool _isShowForm;

    public bool IsShowForm
    {
        get { return _isShowForm; }
        set { SetProperty(ref _isShowForm, value); }
    }

    private string _showPostMessage = "Fill the form to register an employee!";

    public string ShowPostMessage
    {
        get { return _showPostMessage; }
        set { SetProperty(ref _showPostMessage, value); }
    }

    #region ICommands
    public DelegateCommand GetButtonClicked { get; set; }
    public DelegateCommand ShowRegistrationForm { get; set; }
    public DelegateCommand PostButtonClick { get; set; }
    public DelegateCommand<User> PutButtonClicked { get; set; }
    public DelegateCommand<User> DeleteButtonClicked { get; set; }
    #endregion

    public MainWindowViewModel()
    {
        GetButtonClicked = new DelegateCommand(async () => await GetUsers());
        //PutButtonClicked = new DelegateCommand<Employee>(UpdateEmployeeDetails);
        //DeleteButtonClicked = new DelegateCommand<Employee>(DeleteEmployeeDetails);
        //PostButtonClick = new DelegateCommand(CreateNewEmployee);
        //ShowRegistrationForm = new DelegateCommand(RegisterEmployee);
    }

    #region CRUD

    private void RegisterUser()
    {
        IsShowForm = true;
    }

    private async Task GetUsers()
    {
        var API_BASE_URL = ConfigurationManager.AppSettings["API_BASE_URL"];

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(API_BASE_URL + "/User/GetUsers");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiResult<List<User>>>(jsonResponse);

                Users = result.Result;

                IsLoadData = true;
            }
            else
            {
                Console.WriteLine($"Hata Kodu: {response.StatusCode}");
                Console.WriteLine($"Hata Mesajı: {response.ReasonPhrase}");
            }
        }
    }

    #endregion
}
