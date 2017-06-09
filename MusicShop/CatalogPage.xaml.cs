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
    /// Interakční logika pro CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        string[] GoodsName;
        string[] Category;
        List<Goods> GoodsNameToCart = new List<Goods>();
        List<string> GoodsNameCart = new List<string>();
        public CatalogPage()
        {
            InitializeComponent();
            AddGoodsCategory();
            AddGoodsInformation();
            AddGoodsImage();
            AddGoodsToCatalog();
            DisplayDatabase();
        }

        public CatalogPage(List<Goods> GoodsInCart, List<string> GoodsNameCart1)
        {
            InitializeComponent();
            GoodsNameToCart = GoodsInCart;
            GoodsNameCart = GoodsNameCart1;
            AddGoodsCategory();
            AddGoodsInformation();
            AddGoodsImage();
            AddGoodsToCatalog();
            DisplayDatabase();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new ShopPage());
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GoodsListview.SelectedItem != null)
            {
                var item = GoodsListview.SelectedItem;



                for (int i = 0; i < GoodsName.Length; i++)
                {
                    if (item.ToString().Contains(GoodsName[i]))
                    {
                        var goods = GoodsDatabase.GetItemAsync(GoodsName[i]).Result;
                        NavigationService ns = NavigationService.GetNavigationService(this);
                        ns.Navigate(new DetailPage(goods));
                    }
                }

            }
        }
        

        private void DisplayDatabase()
        {
            List<Goods> goods1 = GoodsDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

            var query = from goods in goods1
                        join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                        join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                        join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                        select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };
     
            var queryofcategories = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;

            Debug.WriteLine(goods1.Count);
            foreach (Goods Item in goods1)
            {
                Debug.WriteLine(Item);
            }
            GoodsListview.ItemsSource = query;
            ListViewOfCategories.ItemsSource = queryofcategories;
        }

        public void AddGoodsToCatalog()
        {
            //DisplayDatabase();
            GoodsName = new string[] { "Blink-182 - Enema of the State", "Simple Plan - Still Not Getting Any...", "Good Charlotte - The Young and the Hopeless", "Sum 41 - All Killer No Filler", "Anti-Flag - Underground Network", "Green day - American Idiot", "Boxcar Racer - Boxcar Racer" };
            int[] GoodsPrice = new int[] { 449, 329, 249, 229, 319, 399, 239 };
            int[] GoodsCategory = new int[] { 1, 1, 2, 2, 1, 1, 2};
            int[] GoodsInformation = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            int[] GoodsImage = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            for (int i = 0; i < GoodsName.Length; i++)
            {
                Goods goods = new Goods();
                goods.Name = GoodsName[i];
                goods.Price = GoodsPrice[i];
                goods.GoodsCategoryID = GoodsCategory[i];
                goods.GoodsInformationID = GoodsInformation[i];
                goods.GoodsImageID = GoodsImage[i];
                int count = 0;
                foreach (Goods item in GoodsDatabase.GetItemsAsync().Result)
                {
                    if (item.Name.Equals(GoodsName[i]))
                    {
                        count += 1;
                    }
                }
                if (count == 0)
                {
                    GoodsDatabase.SaveItemAsync(goods);
                }
            }
        }

        public void AddGoodsCategory()
        {
            //DisplayDatabase();
            Category = new string[] { "Pop", "Punk-Rock", "Rock", "Metal" };

            for (int i = 0; i < Category.Length; i++)
            {
                GoodsCategory category = new GoodsCategory();
                category.Category = Category[i];
                int count = 0;
                foreach (GoodsCategory item in GoodsCategoryDatabase.GetItemsAsync().Result)
                {
                    if (item.Category.Equals(Category[i]))
                    {
                        count += 1;
                    }
                }
                if (count == 0)
                {
                    GoodsCategoryDatabase.SaveItemAsync(category);
                }
            }
        }

        public void AddGoodsInformation()
        {
            //DisplayDatabase();
            int[] Year = new int[] { 1999, 2000, 2001, 2001, 2002, 2003, 2002, 2005};
            string[] Type = new string[] { "CD", "CD", "CD+DVD", "CD+LP", "CD", "CD", "CD+DVD audio", "CD+Blu-ray" };
            string[] Description = new string[] { "Cras pede libero, dapibus nec, pretium sit amet, tempor quis. Nulla non lectus sed nisl molestie malesuada. Quisque tincidunt scelerisque libero.", "Phasellus rhoncus. Quisque tincidunt scelerisque libero. Fusce dui leo, imperdiet in, aliquam sit amet, feugiat eu, orci. Nullam at arcu a est sollicitudin euismod.", "Etiam ligula pede, sagittis quis, interdum ultricies, scelerisque eu. Aenean fermentum risus id tortor. ", "Integer tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam dui sem, fermentum vitae, sagittis id, malesuada in, quam. Aenean vel massa quis mauris vehicula lacinia. Etiam posuere lacus quis dolor.", "Integer tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam dui sem, fermentum vitae, sagittis id, malesuada in, quam. Aenean vel massa quis mauris vehicula lacinia. Etiam posuere lacus quis dolor.", "Nulla non arcu lacinia neque faucibus fringilla. Duis sapien nunc, commodo et, interdum suscipit, sollicitudin et, dolor. Fusce wisi.", "Proin mattis lacinia justo. Et harum quidem rerum facilis est et expedita distinctio. Sed vel lectus. Donec odio tempus molestie, porttitor ut, iaculis quis, sem.", "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Fusce dui leo, imperdiet in, aliquam sit amet, feugiat eu, orci. Nullam at arcu a est sollicitudin euismod. Nunc auctor. Aliquam erat volutpat. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?" };

            for (int i = 0; i < Year.Length; i++)
            {
                GoodsInformation information = new GoodsInformation();
                information.YearOfRealising = Year[i];
                information.Type = Type[i];
                information.Description = Description[i];

                GoodsInformationDatabase.SaveItemAsync(information);
            }
        }

        public void AddGoodsImage()
        {
            //DisplayDatabase();
            string[] ImageName = new string[] { "Assets/AntiFlag-UndergroundNetwork.jpg", "Assets/Blink182-EnemaOfTheState.jpg", "Assets/BoxCarRacer-Album.jpg", "Assets/GoodCharlote-TheYoungAndTheHopeless.jpg", "Assets/GreenDay-AmericanIdiot.jpg", "Assets/SimplePlan-StillNotGetAny.jpg", "Assets/Sum41-AllKillerNoFiller.jpg" };

            for (int i = 0; i < ImageName.Length; i++)
            {
                GoodsImage image = new GoodsImage();
                image.ImageName = ImageName[i];
                int count = 0;
                foreach (GoodsImage item in GoodsImageDatabase.GetItemsAsync().Result)
                {
                    if (item.ImageName.Equals(ImageName[i]))
                    {
                        count += 1;
                    }
                }
                if (count == 0)
                {
                    GoodsImageDatabase.SaveItemAsync(image);
                }
            }
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string SearchWord = Search.Text.ToString();           
            var queryofsearchingword = GoodsDatabase.GetSearchWord(SearchWord).Result;
            List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

            var query = from goods in queryofsearchingword
                        join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                        join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                        join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                        select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };

            GoodsListview.ItemsSource = query;
        }

        private void SearchByLetter_Click(object sender, RoutedEventArgs e)
        {
            var keyword = (e.Source as Button).Content.ToString();
            string SearchWord = keyword;
            var queryofsearchingword = GoodsDatabase.GetSearchWord(SearchWord).Result;
            List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

            var query = from goods in queryofsearchingword
                        join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                        join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                        join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                        select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };

            GoodsListview.ItemsSource = query;
        }

        private void ListViewOfCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ListViewOfCategories.SelectedItem != null)
            {
                var item = ListViewOfCategories.SelectedItem as GoodsCategory;
                

                List<Goods> goods1 = GoodsDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

                var query = from goods in goods1
                            join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                            where goodscategory.Category == item.Category
                            join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                            join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                            select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };

                GoodsListview.ItemsSource = query;

            }
        }

        private void OrderBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = OrderBox.SelectedItem.ToString();

            ComboBoxItem cbi = (ComboBoxItem)OrderBox.SelectedItem;
            string selectedText = cbi.Content.ToString();

            if (selectedText.Equals("dle nazvu"))
            {
                GetJoinedDatatable(selectedText);
            }

            if (selectedText.Equals("od nejlevnejsiho"))
            {
                GetJoinedDatatable(selectedText);
            }

            if (selectedText.Equals("od nejdrazsiho"))
            {
                GetJoinedDatatable(selectedText);
            }

            if (selectedText.Equals("od nejnovejsiho"))
            {
                List<Goods> goods1 = GoodsDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

                var query = from goods in goods1
                            join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                            join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                            join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                            orderby goodsinformation.YearOfRealising descending
                            select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };

                var queryofcategories = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;

                Debug.WriteLine(goods1.Count);
                foreach (Goods Item in goods1)
                {
                    Debug.WriteLine(Item);
                }
                GoodsListview.ItemsSource = query;
                ListViewOfCategories.ItemsSource = queryofcategories;
            }

            if (selectedText.Equals("od nejstarsiho"))
            {
                List<Goods> goods1 = GoodsDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
                List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

                var query = from goods in goods1
                            join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                            join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                            join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                            orderby goodsinformation.YearOfRealising
                            select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };

                var queryofcategories = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;

                Debug.WriteLine(goods1.Count);
                foreach (Goods Item in goods1)
                {
                    Debug.WriteLine(Item);
                }
                GoodsListview.ItemsSource = query;
                ListViewOfCategories.ItemsSource = queryofcategories;
            }
        }

        public void GetJoinedDatatable(string selectedtext)
        {
            List<Goods> goods1 = new List<Goods>();
            if (selectedtext.Equals("dle nazvu"))
            {
                goods1 = GoodsDatabase.GetGoodsByName().Result;
            } else if(selectedtext.Equals("od nejlevnejsiho"))
            {
                goods1 = GoodsDatabase.GetGoodsByLowestPrice().Result;
            } else if(selectedtext.Equals("od nejdrazsiho"))
            {
                goods1 = GoodsDatabase.GetGoodsByHighestPrice().Result;
            }

            List<GoodsCategory> goodscategory1 = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsInformation> goodsinformation1 = GoodsInformationDatabase.GetItemsNotDoneAsync().Result;
            List<GoodsImage> goodsimage1 = GoodsImageDatabase.GetItemsNotDoneAsync().Result;

            var query = from goods in goods1
                        join goodscategory in goodscategory1 on goods.GoodsCategoryID equals goodscategory.GoodsCategoryID
                        join goodsinformation in goodsinformation1 on goods.GoodsInformationID equals goodsinformation.GoodsInformationID
                        join goodsimage in goodsimage1 on goods.GoodsImageID equals goodsimage.ImageID
                        select new { Name = goods.Name, Price = goods.Price, Category = goodscategory.Category, Type = goodsinformation.Type, ImageName = goodsimage.ImageName };

            var queryofcategories = GoodsCategoryDatabase.GetItemsNotDoneAsync().Result;

            Debug.WriteLine(goods1.Count);
            foreach (Goods Item in goods1)
            {
                Debug.WriteLine(Item);
            }
            GoodsListview.ItemsSource = query;
            ListViewOfCategories.ItemsSource = queryofcategories;
        }

        private void Buy_Button_Click(object sender, RoutedEventArgs e)
        {

                 string item = (e.Source as Button).Tag.ToString();

                 for (int i = 0; i < GoodsName.Length; i++)
                 {
                     if (item.ToString().Contains(GoodsName[i]))
                     {

                         Goods goods = GoodsDatabase.GetItemAsync(GoodsName[i]).Result;
                    GoodsNameToCart.Add(goods);
                    GoodsNameCart.Add(item);
                         NavigationService ns = NavigationService.GetNavigationService(this);
                         ns.Navigate(new ShopPage(GoodsNameToCart, GoodsNameCart));
                     }
                 }
             
              
        }
    }
}
