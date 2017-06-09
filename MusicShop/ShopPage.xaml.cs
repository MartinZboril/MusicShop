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

namespace MusicShop
{
    /// <summary>
    /// Interakční logika pro ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        List<Goods> GoodsInCart = new List<Goods>();
        List<string> GoodsName = new List<string>();
        public ShopPage()
        {
            InitializeComponent();

        }

        public ShopPage(List<Goods> GoodsNameToCart, List<string> GoodsNameFromCatalog)
        {
            InitializeComponent();
            listView.ItemsSource = GoodsNameToCart;
            GoodsInCart = GoodsNameToCart;
            GoodsName = GoodsNameFromCatalog;
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
            ns.Navigate(new ShopPage());
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new OrderPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new CatalogPage(GoodsInCart, GoodsName));
        }

        private void DeleteFormCart_Button_Click(object sender, RoutedEventArgs e)
        {
            string item = (e.Source as Button).Tag.ToString();

            int a = 0;
            for (int i = 0; i < GoodsName.Count; i++)
            {
                if (item.ToString().Contains(GoodsName[i]))
                {
                    //GoodsInCart.RemoveAt(i);
                    var itemToRemove = GoodsInCart.SingleOrDefault(r => r.Name == GoodsName[i]);
                    if (itemToRemove != null)
                    {
                        GoodsInCart.Remove(itemToRemove);
                    }
                }

            }
            listView.ItemsSource = GoodsInCart;
            Login.Text = a.ToString();
        }
    }
}
