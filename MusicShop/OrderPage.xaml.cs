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
            GoodsInCart = GoodsInCart1;
            GoodsName = GoodsName1;
            Piece = PieceOfGoods;
            List<Transport> TransportToCheckBox = new List<Transport>();
            TransportToCheckBox = Transport();
            CheckBoxList.ItemsSource = TransportToCheckBox;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            PotentialCustomer Customer = new PotentialCustomer();
            Customer.Name = Name.Text;
            Customer.Surname = Surname.Text;
            Customer.Mail = Mail.Text;
            Customer.Phone = int.Parse(Phone.Text);
            Customer.Street = Street.Text;
            Customer.Town = Town.Text;
            Customer.PostCode = PostCode.Text;

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new FinishPage(Customer, TransportToSend, GoodsName, GoodsInCart, Piece));
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

        public List<Transport> Transport()
        {
            Transport transport = new Transport();
            transport.Name = "Osobni odber";
            transport.Price = 0;
            transport.IsSelected = false;
            Transport transport1 = new Transport();
            transport1.Name = "Balik na postu";
            transport1.Price = 100;
            transport1.IsSelected = false;
            Transport transport2 = new Transport();
            transport2.Name = "Balik do ruky";
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
