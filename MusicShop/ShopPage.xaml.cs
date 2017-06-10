using MusicShop.Database;
using MusicShop.Model;
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
using MusicShop.Class;

namespace MusicShop
{
    /// <summary>
    /// Interakční logika pro ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        List<string> GoodsName = new List<string>();
        List<int> Piece = new List<int>();
        List<CartGoods> CartGoodsList = new List<CartGoods>();
        public ShopPage(List<CartGoods> cartgoods, List<string> goodsname)
        {
            InitializeComponent();
            GoodsName = goodsname;
            CartGoodsList = cartgoods;
            listView.ItemsSource = CartGoodsList;
        }

        public ShopPage()
        {
            InitializeComponent();
        }

        public ShopPage(List<string> GoodsNameFromCart, List<CartGoods> GoodsFromCart, List<int> GoodsPiece)
        {
            InitializeComponent();
            GoodsName = GoodsNameFromCart;
            CartGoodsList = GoodsFromCart;
            Piece = GoodsPiece;
            listView.ItemsSource = CartGoodsList;

        }

        public ShopPage(List<string> GoodsNameFromCatalog, List<int> GoodsPiece)
        {
            InitializeComponent();
            GoodsName = GoodsNameFromCatalog;
            Piece = GoodsPiece;
            for (int i = 0; i < GoodsName.Count; i++)
            {
                Goods goods = GoodsDatabase.GetItemAsync(GoodsName[i]).Result;
                CartGoods CartGoods = new CartGoods();
                CartGoods.GoodsID = goods.GoodsID;
                CartGoods.Name = goods.Name;
                CartGoods.Price = goods.Price;
                CartGoods.GoodsQauntity = Piece[i];
                CartGoods.TotalPrice = Piece[i] * goods.Price;
                CartGoodsList.Add(CartGoods);
            }
            listView.ItemsSource = CartGoodsList;
            
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

        private static GoodsCategoryDatabase _goodscategorydatabase;
        public static GoodsCategoryDatabase GoodsCategoryDatabase
        {
            get
            {
                if (_goodscategorydatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _goodscategorydatabase = new GoodsCategoryDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _goodscategorydatabase;
            }
        }

        private static GoodsInformationDatabase _goodsinformationdatabase;
        public static GoodsInformationDatabase GoodsInformationDatabase
        {
            get
            {
                if (_goodsinformationdatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _goodsinformationdatabase = new GoodsInformationDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _goodsinformationdatabase;
            }
        }

        private static GoodsImageDatabase _goodsimagedatabase;
        public static GoodsImageDatabase GoodsImageDatabase
        {
            get
            {
                if (_goodsimagedatabase == null)
                {
                    var fileHelper = new FileHelper();
                    _goodsimagedatabase = new GoodsImageDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _goodsimagedatabase;
            }
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new ShopPage(CartGoodsList, GoodsName));
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new OrderPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new CatalogPage(GoodsName, CartGoodsList, Piece));
        }

        private void DeleteFormCart_Button_Click(object sender, RoutedEventArgs e)
        {
            string item = (e.Source as Button).Tag.ToString();
            GoodsName.Remove(item);

            string a = item;
            
                    var itemToRemove = CartGoodsList.SingleOrDefault(r => r.Name == item);
                    if (itemToRemove != null)
                    {                   
                        CartGoodsList.Remove(itemToRemove);
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new ShopPage(CartGoodsList, GoodsName));
            }

            List<CartGoods> CartGoodsList1 = new List<CartGoods>();
            CartGoodsList1 = CartGoodsList;
            listView.ItemsSource = CartGoodsList;
            Login.Text = a.ToString();
        }


        private void PieceOfGoods_TextChanged(object sender, TextChangedEventArgs e)
        {
            string item = (e.Source as TextBox).Tag.ToString();
            string itemText = (e.Source as TextBox).Text.ToString();

            int n;
            bool isNumeric = int.TryParse(itemText, out n);
            Login.Text = itemText;
            if (isNumeric)
            {
                var itemToCheck = CartGoodsList.SingleOrDefault(r => r.Name == item);

                if (itemToCheck != null)
                {
                     for (int i = 0; i < GoodsName.Count; i++)
                     {
                         if (item.Equals(GoodsName[i]))
                         {
                             // Potreba osetrit
                             Piece[i] += n;

                         }
                     }

                    itemToCheck.GoodsQauntity += n - itemToCheck.GoodsQauntity;
                    itemToCheck.TotalPrice = itemToCheck.GoodsQauntity * itemToCheck.Price;
                    NavigationService ns = NavigationService.GetNavigationService(this);
                    ns.Navigate(new ShopPage(CartGoodsList, GoodsName));
                }
                Login.Text = itemText;
                List<CartGoods> CartGoodsList1 = new List<CartGoods>();
                CartGoodsList1 = CartGoodsList;
                listView.ItemsSource = CartGoodsList;

            }
        }
    }
}
