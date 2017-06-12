using MusicShop.Class;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MusicShop.Model;
using MusicShop.Database;
using System.Diagnostics;

namespace MusicShop
{
    /// <summary>
    /// Interakční logika pro FinishPage.xaml
    /// </summary>
    public partial class FinishPage : Page
    {
        List<CartGoods> GoodsInCart = new List<CartGoods>();
        List<string> GoodsName = new List<string>();
        List<int> Piece = new List<int>();
        int n;

        public FinishPage(PotentialCustomer Customer1, Transport TransportToSend, List<string> GoodsName, List<CartGoods> GoodsInCart, List<int> Piece1)
        {
            InitializeComponent();
            Piece = Piece1;
            Address Address = new Address();
            Address.Street = Customer1.Street;
            Address.Town = Customer1.Town;
            Address.PostNumber = int.Parse(Customer1.PostCode.ToString());
            AddressDatabase.SaveItemAsync(Address);
            DebugMethod();

            ContactInformation Contact = new ContactInformation();
            Contact.Email = Customer1.Mail;
            Contact.Phone = Customer1.Phone;
            ContactInformationDatabase.SaveItemAsync(Contact);
            DebugMethod();

            Customer Customer = new Customer();
            Customer.AddressID = Address.AddressID;
            Customer.ContactInformationID = Contact.ContactInformationID;
            Customer.Name = Customer1.Name;
            Customer.Surname = Customer1.Surname;
            CustomerDatabase.SaveItemAsync(Customer);
            DebugMethod();

            OrderTransport Transport = new OrderTransport();
            Transport.TypeOfTransport = TransportToSend.Name;
            Transport.Price = TransportToSend.Price;
            OrderTransportDatabase.SaveItemAsync(Transport);
            DebugMethod();

            Random r = new Random();
            int rnd = r.Next();

            int TotalPrice = 0;

            for (int i = 0; i < GoodsInCart.Count; i++)
            {
                TotalPrice += GoodsInCart[i].TotalPrice;
            }

            Order Order = new Order();
            Order.CustomerID = Customer.CustomerID;
            Order.TransportID = Transport.TransportID;
            
            Order.OrderNumber = rnd;
            Order.OrderPrice = TotalPrice;//n = Order.OrderID;
            OrderDatabase.SaveItemAsync(Order);
            DebugMethod();

            Number.Text = Order.OrderNumber.ToString();

            List<Customer> clist = new List<Customer>();
            clist = CustomerDatabase.GetItemsAsync().Result;
            CutomerListView.ItemsSource = clist;

            for (int i = 0; i < GoodsName.Count; i++)
            {
                OrderGoods OrderGoods = new OrderGoods();              
                OrderGoods.OrderID = Order.OrderID;
                Goods goods = GoodsDatabase.GetItemAsync(GoodsName[i]).Result;
                OrderGoods.GoodsQauntity = GoodsInCart[i].GoodsQauntity;
                OrderGoods.GoodsID = goods.GoodsID;               
                OrderGoodsDatabase.SaveItemAsync(OrderGoods);
                DebugMethod();
            }

            var ordergoods = OrderGoodsDatabase.GetItemsAsync().Result;
            var order = OrderDatabase.GetItemsAsync().Result;
            OrderListView.ItemsSource = ordergoods;
            OrderListView1.ItemsSource = order;
        }

        public void DebugMethod()
        {
            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("");
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new CatalogPage());
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new ShopPage(GoodsName, GoodsInCart, Piece));
        }

        private void OrderView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new OrderView(GoodsName, GoodsInCart, Piece));
        }

        private static GoodsDatabase _goodsdatabase;
        public static GoodsDatabase GoodsDatabase
        {
            get
            {
                if (_goodsdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _goodsdatabase = new GoodsDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _goodsdatabase;
            }
        }

        private static CustomerDatabase _customerdatabase;
        public static CustomerDatabase CustomerDatabase
        {
            get
            {
                if (_customerdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _customerdatabase = new CustomerDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _customerdatabase;
            }
        }

        private static AddressDatabase _addressdatabase;
        public static AddressDatabase AddressDatabase
        {
            get
            {
                if (_addressdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _addressdatabase = new AddressDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _addressdatabase;
            }
        }

        private static ContactInformationDatabase _contactinformationdatabase;
        public static ContactInformationDatabase ContactInformationDatabase
        {
            get
            {
                if (_contactinformationdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _contactinformationdatabase = new ContactInformationDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _contactinformationdatabase;
            }
        }

        private static OrderTransportDatabase _ordertransportdatabase;
        public static OrderTransportDatabase OrderTransportDatabase
        {
            get
            {
                if (_ordertransportdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _ordertransportdatabase = new OrderTransportDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _ordertransportdatabase;
            }
        }

        private static OrderDatabase _orderdatabase;
        public static OrderDatabase OrderDatabase
        {
            get
            {
                if (_orderdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _orderdatabase = new OrderDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _orderdatabase;
            }
        }

        private static OrderGoodsDatabase _ordergoodsdatabase;
        public static OrderGoodsDatabase OrderGoodsDatabase
        {
            get
            {
                if (_ordergoodsdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _ordergoodsdatabase = new OrderGoodsDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _ordergoodsdatabase;
            }
        }
    }
}
