using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TabControl tbControl;
        public MainWindow()
        {
            InitializeComponent();
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#99FFFFFF");
         
            for (int i = 0; i < 30; i++)
            {
                var border = new Border { Background =(Brush)Resources["Secondary"] ,CornerRadius = new CornerRadius(10) , Width=250,Height=250 , Margin = new Thickness { Top = 0, Bottom = 20, Right = 0, Left = 0 }};
                var view = new StackPanel { Orientation=System.Windows.Controls.Orientation.Vertical,  Width = 220, Height = 215, Background = (Brush)Resources["Secondary"] };
                var btn = new Button { Width=100,Height=50 , Content = "Abrir" , Style=(Style)Resources["BtnStyle"]};
                var text = new TextBlock { HorizontalAlignment=System.Windows.HorizontalAlignment.Center, Text = "Nombre del proyecto", FontSize = 20, Foreground = (Brush)Resources["Font2"] };
                var dir = new TextBlock { Margin = new Thickness { Top = 3, Bottom = 3, Right = 0, Left = 0 }, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, Text = "C:/Directorio", FontSize = 11, Foreground = (Brush)Resources["Font4"] };
                border.Child = view;
                view.Children.Add(text);
                view.Children.Add(new Image() { Margin = new Thickness {Top=10,Bottom=1,Right=10,Left=10}, Source = new BitmapImage(new Uri("/assets/img/image-preview.png", UriKind.Relative)), Width = 100 ,Height=100 });
                view.Children.Add(dir);
                view.Children.Add(btn);
                Items.Add(border);
            }
        }

        private ObservableCollection<Border> items = new ObservableCollection<Border>();
        public ObservableCollection<Border> Items
        {
            get { return items; }
            set { items = value; }
        }
        private void tbCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            tbControl = (sender as TabControl);
        }
       

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var nameTab = "";
            var closeMargin = new Thickness();
            closeMargin.Bottom = 0;
            closeMargin.Left = 10;
            closeMargin.Right = 0;
            closeMargin.Top = 0;
            nameTab = txtProject.Text == "" ? "New Tab" : txtProject.Text;

            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#99FFFFFF");
            TabItem newTabItem = new TabItem
            {
                Name = "Test",
                Foreground = brush
            };
            //// Header tab 
            var stack = new StackPanel() { Orientation = Orientation.Horizontal };
            stack.Children.Add(new TextBlock() { Text = nameTab });
            stack.Children.Add(new Image() { Margin = closeMargin, Source = new BitmapImage(new Uri("/assets/img/close.png", UriKind.Relative)), Width = 10 });
            newTabItem.Header = stack;
            /////////
            
            ////// Grid content of tab
            var content = new Grid { Background = (Brush)Resources["Primary"] };
            content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) });
            content.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            content.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            content.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var nav = ContainerNav();
            content.Children.Add(nav);
            var cont1 = ContainerImage();
            content.Children.Add(cont1);
            var cont2 = ContainerRight();
            content.Children.Add(cont2);
            Grid.SetColumn(cont1, 0);
            Grid.SetRow(cont1, 1);
            Grid.SetColumn(cont2, 1);
            Grid.SetRow(cont2, 1);
            Grid.SetColumnSpan(nav,2);
            Grid.SetRow(nav, 0);

            newTabItem.Content = content;

            tbControl.Items.Add(newTabItem);
            var index = tbControl.Items.Count - 2;
            var tabItem = tbControl.Items[index];
            tbControl.Items.RemoveAt(index);
            tbControl.Items.Insert(tbControl.Items.Count, tabItem);
            txtProject.Text = "";
            //((TabItem)tabItem).IsSelected = true;
            // tbControl.SelectedIndex += 2;
            //tbControl.Items.Remove(tbControl.Items.GetItemAt(tbControl.SelectedIndex - 1)); --Delete 
        }

        private StackPanel ContainerImage()
        {
            
            var view = new StackPanel { Orientation = Orientation.Vertical, Background = (Brush)Resources["Secondary"] };
            return view;
        }

        private Grid ContainerRight()
        {
            var contentGrid = new Grid { Background = (Brush)Resources["Primary"] };
            contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            var CG1 = new Button { Content = "Contenedor 1" };
            var CG2 = new Button { Content = "Contenedor 2" };
            contentGrid.Children.Add(CG1);
            contentGrid.Children.Add(CG2);
            Grid.SetRow(CG1, 0);
            Grid.SetRow(CG2, 1);
            return contentGrid;
        }

        private StackPanel ContainerNav()
        {
            var view = new StackPanel {Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Stretch, Background = (Brush)Resources["btnColor"] };
            var cmb = new ComboBox { Width = 200  };
            cmb.Items.Insert(0,"Agregar");
            cmb.Items.Insert(1, "no");
            cmb.SelectedIndex = 0;
            var btnGuardar = new Button { Content = "Guardar",Width=200 };
            var btnImportar = new Button { Content = "Importar", Width=200 };
            view.Children.Add(cmb);
            view.Children.Add(btnImportar);
            view.Children.Add(btnGuardar);
            return view;
        }
    }

}
