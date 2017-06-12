using MusicShop.Class;
using MusicShop.Database;
using MusicShop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MusicShop
{
    /// <summary>
    /// Interakční logika pro OrderView.xaml
    /// </summary>
    public partial class OrderView : Page
    {
        List<int> Piece = new List<int>();
        List<string> GoodsNameCart = new List<string>();
        List<CartGoods> GoodsCartList = new List<CartGoods>();
        public OrderView(List<string> GoodsName, List<CartGoods> GoodsInCart, List<int> PieceList)
        {
            InitializeComponent();
            ListCreate(GoodsName, GoodsInCart, PieceList);
        }

        private void SearchOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int SearchNumber = int.Parse(SearchWordText.Text);
            List<Order> orderlist = new List<Order>();
            var orderlist1 = OrderDatabase.GetItemAsync(SearchNumber).Result;
            orderlist.Add(orderlist1);

            List<OrderGoods> ordergoods1 = new List<OrderGoods>();
            ordergoods1 = OrderGoodsDatabase.GetItemsAsync().Result;

            var query_where1 = from a in ordergoods1 where a.OrderID.Equals(orderlist[0].OrderID) select a;

            List<OrderGoods> ordergoods2 = new List<OrderGoods>();

            foreach (var a in query_where1)
            {
                ordergoods2.Add(a);
            }

            List<Goods> goodslist1 = new List<Goods>();
            goodslist1 = GetGoodsToList(ordergoods2);

            var query = from ordergoods in ordergoods2 join goodslist in goodslist1 on ordergoods.GoodsID equals goodslist.GoodsID select new { Name = goodslist.Name, Price = (goodslist.Price * ordergoods.GoodsQauntity), GoodsQauntity = ordergoods.GoodsQauntity };
            ListViewOfOrder.ItemsSource = query;

            List<OrderTransport> transport = new List<OrderTransport>();
            transport = OrderTransportDatabase.GetItemsAsync().Result;

            var query_where4 = from a in transport where a.TransportID.Equals(orderlist[0].TransportID) select a;

            foreach (var a in query_where4)
            {
                transport.Add(a);
            }

            List<Customer> customer = new List<Customer>();
            var query_where = CustomerDatabase.GetItemAsync(orderlist[0].CustomerID).Result;
            customer.Add(query_where);

            List<Address> address = new List<Address>();
            var query_address = AddressDatabase.GetItemAsync(orderlist[0].CustomerID).Result;
            address.Add(query_address);

            List<ContactInformation> contactinformation = new List<ContactInformation>();
            var query_contactinformation = ContactInformationDatabase.GetItemAsync(orderlist[0].CustomerID).Result;
            contactinformation.Add(query_contactinformation);

            Name.Text = customer[0].Name + " " + customer[0].Surname;
            Address.Text = address[0].Street + ", " + address[0].Town + ", " + address[0].PostNumber.ToString();
            Transport.Text = transport[0].TypeOfTransport;
            Phone.Text = contactinformation[0].Phone.ToString();
            Mail.Text = contactinformation[0].Email.ToString();
            Price.Text = orderlist[0].OrderPrice.ToString();
        }

        public void ListCreate(List<string> GoodsName, List<CartGoods> GoodsInCart, List<int> PieceList)
        {
            GoodsNameCart = GoodsName;
            GoodsCartList = GoodsInCart;
            Piece = PieceList;
        }

        public List<Goods> GetGoodsToList(List<OrderGoods> ordergoods)
        {
            List<Goods> goodslist = new List<Goods>();
            for (int i = 0; i < ordergoods.Count; i++)
            {
                var goods = GoodsDatabase.GetItemAsyncByID(ordergoods[i].GoodsID).Result;
                goodslist.Add(goods);
            }

            return goodslist;
        }

        public void GetValueForShopCartInfo(List<CartGoods> GoodsFromCart)
        {
            int TotalPrice = GetTotalPriceOfSelectedGoods(GoodsFromCart);
            PriceOFSelectedGoods.Text = TotalPrice.ToString() + " " + "Kc";
            int TotalPiece = GetTotalPieceOfSelectedGoods(GoodsFromCart);
            PieceOFSelectedGoods.Text = TotalPiece.ToString() + " " + "Polozek";
        }

        public int GetTotalPriceOfSelectedGoods(List<CartGoods> cartgoods)
        {
            int TotalPrice = 0;
            for (int i = 0; i < cartgoods.Count; i++)
            {
                TotalPrice += cartgoods[i].Price;
            }
            return TotalPrice;
        }

        public int GetTotalPieceOfSelectedGoods(List<CartGoods> cartgoods)
        {
            int TotalPiece = 0;
            for (int i = 0; i < cartgoods.Count; i++)
            {
                TotalPiece += cartgoods[i].GoodsQauntity;
            }
            return TotalPiece;
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new ShopPage(GoodsNameCart, GoodsCartList, Piece));
        }

        private void OrderView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new OrderView(GoodsNameCart, GoodsCartList, Piece));
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

