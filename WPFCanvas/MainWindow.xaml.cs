using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

namespace WPFCanvas
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //const double RADIUS = 50; //반지름
        
        //public double CenterX { get; set; }
        //public double CenterY { get; set; }

        public Point _posCat { get; set; }

        public Point PosCat
        {
            get { return _posCat; }
            set
            {
                _posCat = value;
                OnPropertyChanged(nameof(PosCat));
                //CenterX = _pos.X - RADIUS;
                //CenterY = _pos.Y - RADIUS;
                //
                //OnPropertyChanged(nameof(CenterX));
                //OnPropertyChanged(nameof(CenterY));
            }
        }
        public Point _posAya { get; set; }

        public Point PosAya
        {
            get { return _posAya; }
            set
            {
                _posAya = value;
                OnPropertyChanged(nameof(PosAya));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            //DataContext 중요
            DataContext = this;
            //PosX=200;
            //PosY=200;
            PosCat = new Point(200, 200);
            PosAya = new Point(400, 400);

        }

        public event PropertyChangedEventHandler? PropertyChanged; //property 가 무엇인가? 여기서 PosX 와 PosY임
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //private void OnMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    //MessageBox.Show("마우스 버튼  클릭"); //원을 눌렀을때는 뜨는데 다른 곳은 안뜸, 배경 색 지정시 버튼 클릭이벤트 발생.
        //    //Pos X = e.GetPosition(this).X;
        //    //Pos Y = e.GetPosition(this).Y; //이걸로 변경이 되었는데 변경되었는지 모른다. OnpropertyChanged 함수 호출

        //    //OnPropertyChanged(nameof(PosX));
        //    //OnPropertyChanged("PosY");
        //    Pos = e.GetPosition(this);  
        //}
        private Boolean IsMoving = false;
        private Image? MovingImage = null;
        private Point PosDiff = new Point(0, 0); //사진을 중앙이 아닌 다른 쪽을 눌렀을때 차이로 이용하여 움직이게 하게 하기 위한 dx, dy를 구할 때 사용하는 변수
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
           if(!IsMoving) return;

            Point p = new Point(
                 e.GetPosition(this).X + Center.RADIUS - PosDiff.X,
                 e.GetPosition(this).Y + Center.RADIUS - PosDiff.Y);
            switch (MovingImage?.Name) {
                case "Cat":
                    PosCat = p;
                    break;
                case "Aya":
                    PosAya = p;
                    break;
            }

            
            
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MovingImage = sender as Image;
            IsMoving = true;
            PosDiff = e.GetPosition(MovingImage); //사진에서의위치를 불러오는 방법임 this(window)가 아닌 이미지 에서 위치
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MovingImage = null;
            IsMoving = false;
        }
    }
    public class Center : IValueConverter
    {
        public const double RADIUS = 50;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - RADIUS;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value + RADIUS;
        }
    }
}
