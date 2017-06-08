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

namespace MusicShop
{
    /// <summary>
    /// Interakční logika pro DetailPage.xaml
    /// </summary>
    public partial class DetailPage : Page
    {
        public DetailPage(Goods selectedgoods)
        {
            InitializeComponent();
            
            List<Goods> goods1 = GoodsDatabase.GetItemsNotDoneAsync().Result;


            Goods goods = new Goods();

            for (int i = 0; i < goods1.Count; i++)
            {
                if (selectedgoods.Name.Equals(goods1[i].Name))
                {
                    goods = goods1[i];
                }

            }
            var category = GoodsCategoryDatabase.GetItemAsync(goods.GoodsCategoryID).Result;
            var information = GoodsInformationDatabase.GetItemAsync(goods.GoodsInformationID).Result;
            var queryofcatefories = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
            var queryofimage = GoodsImageDatabase.GetItemAsync(goods.GoodsImageID).Result;
            ListViewOfCategories.ItemsSource = queryofcatefories;
            Name.Text = goods.Name;
            YearOfRealising.Text = information.YearOfRealising.ToString();
            Category.Text = category.Category;
            Type.Text = information.Type.ToString();
            Price.Text = goods.Price.ToString()+" Kc";
            Description.Text = information.Description.ToString();

            ImageOfAlbum.Source = new BitmapImage(new Uri($"{queryofimage.ImageName}", UriKind.Relative));
        }
     
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new ShopPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new CatalogPage());
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
    }
}
