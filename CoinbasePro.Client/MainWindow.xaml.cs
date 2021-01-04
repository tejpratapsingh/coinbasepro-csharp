using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using CoinbasePro.Network.Authentication;
using Newtonsoft.Json;

namespace CoinbasePro.Client
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfigManager c = new ConfigManager();
                var config = c.GetConfig();

                //create an authenticator with your apiKey, apiSecret and passphrase
                var authenticator = new Authenticator(config.ApiKey, config.ApiSecret, config.PassPhrase);

                //create the CoinbasePro client
                var coinbaseProClient = new CoinbasePro.CoinbaseProClient(authenticator);

                //use one of the services 
                var allAccounts = await coinbaseProClient.AccountsService.GetAllAccountsAsync();
                File.WriteAllText("accounts".ToPath(config), JsonConvert.SerializeObject(allAccounts, Formatting.Indented));

                var payments = await coinbaseProClient.PaymentsService.GetAllPaymentMethodsAsync();
                File.WriteAllText("payments".ToPath(config), JsonConvert.SerializeObject(payments, Formatting.Indented));

                var products = await coinbaseProClient.ProductsService.GetAllProductsAsync();
                File.WriteAllText("products".ToPath(config), JsonConvert.SerializeObject(products, Formatting.Indented));

                var currencies = await coinbaseProClient.CurrenciesService.GetAllCurrenciesAsync();
                File.WriteAllText("currencies".ToPath(config), JsonConvert.SerializeObject(currencies, Formatting.Indented));

                var users = await coinbaseProClient.UserAccountService.GetTrailingVolumeAsync();
                File.WriteAllText("users".ToPath(config), JsonConvert.SerializeObject(users, Formatting.Indented));

                var fees = await coinbaseProClient.FeesService.GetCurrentFeesAsync();
                File.WriteAllText("fees".ToPath(config), JsonConvert.SerializeObject(fees, Formatting.Indented));

                var profiles = await coinbaseProClient.ProfilesService.GetAllProfilesAsync();
                File.WriteAllText("profiles".ToPath(config), JsonConvert.SerializeObject(profiles, Formatting.Indented));

                var fills = await coinbaseProClient.FillsService.GetAllFillsAsync();
                File.WriteAllText("fills".ToPath(config), JsonConvert.SerializeObject(fills, Formatting.Indented));

                var fundings = await coinbaseProClient.FundingsService.GetAllFundingsAsync();
                File.WriteAllText("fundings".ToPath(config), JsonConvert.SerializeObject(fundings, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
