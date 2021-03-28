using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace COP_DatabindingInWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<ListItem> theList = new ObservableCollection<ListItem>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            theList.Add(new ListItem("Stephen Watson", 12345));
            theList.Add(new ListItem("Jane Doe", 4356345));
            theList.Add(new ListItem("Mr Name", 943855));

            TheList.ItemsSource = theList;
            
            
            


        }

        // One Way Binding (To Target) - Random Number Generator
        public int randomNumber { get; set; } = 7;
        private void GenerateNumber(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            this.randomNumber = random.Next(0,100);
            OnPropertyChanged("randomNumber");
        }

        // One Way Binding (To Source) - Message Box Number Generator
        public string message { get; set; }

        private void GenerateMessage(object sender, RoutedEventArgs e) {
            MessageBox.Show(this.message);
        }

        // Two Way Binding - Number Slider
        // This can also be used to demonstrate triggers by changing UpdateSourceTrigger between PropertyChanged and LostFocus
        private int _sliderNumber;
        public int sliderNumber {

            get { return _sliderNumber; }
            set
            {
                if (_sliderNumber != value)
                {
                    _sliderNumber = value;
                    OnPropertyChanged();
                }
            }

        }


        // Code behind
        private void CreateControl(object sender, RoutedEventArgs e)
        {
            var label = new Label()
            {
                Name = "CreatedLabel",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(409,278,0,0),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 137,
                Content = "Hello, world!"
            };

            var textbox = new TextBox()
            {
                Name = "CreatedTextBox",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(409, 319, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 137,
                TextWrapping = TextWrapping.Wrap,

            };

            if (MyUserInterface.FindName(label.Name) == null)
            {
                this.MyUserInterface.Children.Add(label);
                this.MyUserInterface.RegisterName(label.Name, label);
                this.MyUserInterface.Children.Add(textbox);
                this.MyUserInterface.RegisterName(textbox.Name, textbox);
            }

            var binding = new Binding("Text");
            binding.Source = textbox;
            label.SetBinding(Label.ContentProperty, binding);



        }


        private void ClearControl(object sender, RoutedEventArgs e)
        {
            this.MyUserInterface.Children.Remove((UIElement)MyUserInterface.FindName("CreatedLabel"));
            this.MyUserInterface.UnregisterName("CreatedLabel");

            this.MyUserInterface.Children.Remove((UIElement)MyUserInterface.FindName("CreatedTextBox"));
            this.MyUserInterface.UnregisterName("CreatedTextBox");
        }

        // Dealing with collections: Time to look at Data templates (Uncomment the commented xaml)
        // Different data structures
        public class ListItem
        {
            public string title { get; set; }
            public int id { get; set; }

            public ListItem(string title, int id)
            {
                this.title = title;
                this.id = id;
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            theList.Add(new ListItem("COP is awesome ", 1234567890));
        }


        // Allow the UI to change when a variable is changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            OnPropertyChanged("numberMessage");
        }

        
    }


    

}
