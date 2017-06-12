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
    /// Interakční logika pro OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<CartGoods> GoodsInCart = new List<CartGoods>();
        List<string> GoodsName = new List<string>();
        List<int> Piece = new List<int>();
        Transport TransportToSend = new Transport();
        
        public OrderPage(List<CartGoods> GoodsInCart1, List<string> GoodsName1, List<int> PieceOfGoods)
        {
            InitializeComponent();
            GoodsName = GoodsName1;
            GoodsInCart = GoodsInCart1;
            Piece = PieceOfGoods;
            List<Transport> TransportToCheckBox = new List<Transport>();
            TransportToCheckBox = Transport();
            CheckBoxList.ItemsSource = TransportToCheckBox;
            GetValueForShopCartInfo(GoodsInCart);
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            Warning.Visibility = Visibility.Hidden;
            int number = 0;
            int postcode = 0;
            if (Name.Text != "" && Surname.Text != "" && Mail.Text != "" && int.TryParse(Phone.Text, out number) && Street.Text != "" && Town.Text != "" && int.TryParse(PostCode.Text, out postcode))
            {
            PotentialCustomer Customer = new PotentialCustomer();
            Customer.Name = Name.Text;
            Customer.Surname = Surname.Text;
            Customer.Mail = Mail.Text;
            Customer.Phone = number;
            Customer.Street = Street.Text;
            Customer.Town = Town.Text;
            Customer.PostCode = postcode;

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new FinishPage(Customer, TransportToSend, GoodsName, GoodsInCart, Piece));
            } else
            {
                Warning.Visibility = Visibility.Visible;
            }
        }

        public void GetValueForShopCartInfo(List<CartGoods> GoodsFromCart)
        {
            int TotalPrice = GetTotalPriceOfSelectedGoods(GoodsFromCart);
            PriceOFSelectedGoods.Text = TotalPrice.ToString() + " " + "Kč";
            int TotalPiece = GetTotalPieceOfSelectedGoods(GoodsFromCart);
            PieceOFSelectedGoods.Text = TotalPiece.ToString() + " " + "Položek";
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
            ns.Navigate(new ShopPage(GoodsName, GoodsInCart, Piece));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new ShopPage(GoodsName, GoodsInCart, Piece));
        }

        private void OrderView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new OrderView(GoodsName, GoodsInCart, Piece));
        }

        private void NameOfShop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new CatalogPage(GoodsName, GoodsInCart, Piece));
        }

        public List<Transport> Transport()
        {
            Transport transport = new Transport();
            transport.Name = "Osobní odběr";
            transport.Price = 0;
            transport.IsSelected = false;
            Transport transport1 = new Transport();
            transport1.Name = "Balík na poštu";
            transport1.Price = 100;
            transport1.IsSelected = false;
            Transport transport2 = new Transport();
            transport2.Name = "Balík do ruky";
            transport2.Price = 120;
            transport2.IsSelected = false;
            Transport transport3 = new Transport();
            transport3.Name = "Zásilková společnost Geis ";
            transport3.Price = 110;
            transport3.IsSelected = false;
            Transport transport4 = new Transport();
            transport4.Name = "Zásilková společnost DPD ";
            transport4.Price = 110;
            transport4.IsSelected = false;

            List<Transport> Transport = new List<Transport>();
            Transport.Add(transport);
            Transport.Add(transport1);
            Transport.Add(transport2);
            Transport.Add(transport3);
            Transport.Add(transport4);

            return Transport;
        }

        private void TransportCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkZone = (CheckBox)sender;
            TransportToSend.Name = chkZone.Content.ToString();
            TransportToSend.Price = int.Parse(chkZone.Tag.ToString());
        }
    }
}
