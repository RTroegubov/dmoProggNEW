using Nazv_organizaciy.classes;
using Nazv_organizaciy.windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Nazv_organizaciy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private List<Service> _ServiceList;
        public List<Service> ServiceList
        {
            get { return _ServiceList; }
            set { _ServiceList = value; }
        }

        public MainWindow()
        {         
            InitializeComponent();
            this.DataContext = this;
            ServiceList = Core.DB.Service.ToList();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private Boolean _IsAdminMode = false;

        public event PropertyChangedEventHandler PropertyChanged;

        // публичный геттер, который меняет текущий режим (Админ/не Админ)
        public Boolean IsAdminMode
        {
            get { return _IsAdminMode; }
            set
            {
                _IsAdminMode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AdminModeCaption"));
                    PropertyChanged(this, new PropertyChangedEventArgs("AdminVisibility"));
                }
            }
        }
        // этот геттер возвращает текст для кнопки в зависимости от текущего режима
        public string AdminModeCaption
        {
            get
            {
                if (IsAdminMode) return "Выйти из режима\nАдминистратора";
                return "Войти в режим\nАдминистратора";
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            // если мы уже в режиме Администратора, то выходим из него 
            if (IsAdminMode) IsAdminMode = false;
            else
            {
                // создаем окно для ввода пароля
                var InputBox = new InputBoxWindow("Введите пароль Администратора");
                // и показываем его как диалог (модально)
                if ((bool)InputBox.ShowDialog())
                {
                    // если нажали кнопку "Ok", то включаем режим, если пароль введен верно
                    IsAdminMode = InputBox.InputText == "0000";
                }
            }
        }
        public string AdminVisibility
        {
            get
            {
                if (IsAdminMode) return "Visible";
                return "Collapsed";
            }
        }

    }
}
