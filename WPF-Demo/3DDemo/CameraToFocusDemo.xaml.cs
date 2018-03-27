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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Demo._3DDemo
{
    /// <summary>
    /// CameraToFocusDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CameraToFocusDemo : Page
    {
        public CameraToFocusDemo()
        {
            InitializeComponent();

            originPosition = camera.Position;
            originLookDirection = camera.LookDirection;
            originUpDirection = camera.UpDirection;

            FocusPosition = new Point3D(0, 0, 0);
            Vector3D vDistance = FocusPosition - camera.Position;
            DistanceToFocus = vDistance.Length;
        }
        #region 字段 属性 委托
        private Point mouseLastPosition;
        double mouseDeltaFactor = 0.8;     //鼠标移动距离对应摄像头移动角度单位
        double keyDeltaFactor = 2;         //方向键按压对应摄像头移动距离单位
        double wheelMoveFactor = 2;        //滚轮前后移动单位距离
        private Point3D _focusPosition;    //焦点坐标（依赖摄像头位置）
        private double _distanceToFocus;   //焦点与摄像头间距

        bool IsMoveFocus = true;              //是否可用方向、移动键移动焦点
        private Point3D originPosition;       //原始镜头坐标
        private Vector3D originLookDirection; //原始镜头焦点方向
        private Vector3D originUpDirection;   //原始镜头正上方向

        /// <summary>
        /// 焦点中心坐标（摄像头前方固定距离）
        /// </summary>
        public Point3D FocusPosition
        {
            get { return _focusPosition; }
            set
            {
                _focusPosition = value;
                if (_focusPosition.Y < 0)
                {
                    _focusPosition.Y = 0;
                }
            }
        }
        public double DistanceToFocus
        {
            get { return _distanceToFocus; }
            set
            {
                _distanceToFocus = value;
            }
        }
        #endregion

        //public static RoutedCommand Fore = new RoutedCommand("Fore", typeof(string));
        //可移动时设置镜头新坐标
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你好！");
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseLastPosition = e.GetPosition(this);
        }
        //鼠标移动对应摄像头位置转换
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point newMousePosition = e.GetPosition(this);
                if (mouseLastPosition.X != newMousePosition.X)
                {
                    HorizontalTransform(mouseLastPosition.X < newMousePosition.X, mouseDeltaFactor);//水平变化
                }
                if (mouseLastPosition.Y != newMousePosition.Y)
                {
                    VerticalTransform(mouseLastPosition.Y > newMousePosition.Y, mouseDeltaFactor);//垂直变化
                }
                mouseLastPosition = newMousePosition;

                ShowCameraPosition();
            }

        }
        //画面绕屏幕平面中心点（焦点）的摄像头垂直转换
        private void VerticalTransform(bool upDown, double angleDeltaFactor)
        {
            Vector3D position = camera.Position - FocusPosition;
            //Vector3D position = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z); //获取相对原点向量
            Vector3D rotateAxis = Vector3D.CrossProduct(position, camera.UpDirection);  //获取中心原地向量+正上方向量 的组合面 的垂直方向（屏幕左右方向）作为绕轴
            RotateTransform3D rt3d = new RotateTransform3D();
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (upDown ? -1 : 1));//初始化3D旋转对象
            rt3d.Rotation = rotate;
            Matrix3D martix = rt3d.Value;                                              //获取3*3矩阵对象
            Point3D newPosition = martix.Transform(camera.Position);                   //以源坐标（摄像头自身）通过矩阵转换为新坐标
            camera.Position = newPosition;
            //camera.LookDirection = new Vector3D(-newPosition.X, -newPosition.Y, -newPosition.Z);
            camera.LookDirection = FocusPosition - newPosition;

            //update the up direction;
            Vector3D newUpDirection = Vector3D.CrossProduct(camera.LookDirection, rotateAxis);  //相对原地方向+LookDirector方向的组合面 的垂直方向 就为新的UpDirector
            newUpDirection.Normalize();
            camera.UpDirection = newUpDirection;
            ShowCameraPosition();

        }
        //画面绕屏幕平面中心点（焦点）的摄像头水平转换
        private void HorizontalTransform(bool leftRight, double angleDeltaFactor)
        {
            Vector3D position = camera.Position - FocusPosition;
            //Vector3D position = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z);//相对原点向量
            Vector3D rotateAxis = camera.UpDirection;                               //摄像机的正上方作为旋转轴
            RotateTransform3D rt3d = new RotateTransform3D();                       //设置绕轴旋转对象（轴、转角）
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (leftRight ? -1 : 1));
            rt3d.Rotation = rotate;
            Matrix3D martix = rt3d.Value;            //设置4*4的矩形转换对象
            Point3D newPosition = martix.Transform(camera.Position);  //矩形转换对象根据（摄像机位置）变换新坐标
            camera.Position = newPosition;
            //camera.LookDirection = new Vector3D(-newPosition.X, -newPosition.Y, -newPosition.Z); //  摄像头方向重新指向焦点
            camera.LookDirection = FocusPosition - newPosition;

            ShowCameraPosition();

            //update the up direction;
            //Vector3D newUpDirection = Vector3D.CrossProduct(camera.LookDirection, rotateAxis);
            //newUpDirection.Normalize();
            //camera.UpDirection = newUpDirection;
        }
        //鼠标滚轮 摄像镜头方向画面缩放远近
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0) //get near
            {
                DistanceToFocus -= wheelMoveFactor;
                if (DistanceToFocus < 5)  //确定摄像头与焦点距离不小于5
                {
                    return;
                }
                CameraToFocus();
                //camera.FieldOfView -= wheelMoveFactor;
            }

            if (e.Delta < 0) //get near
            {
                DistanceToFocus += wheelMoveFactor;
                if (DistanceToFocus > 1000)  //确定摄像头与焦点间距不大于100
                {
                    return;
                }
                CameraToFocus();
                //camera.FieldOfView += wheelMoveFactor;
            }
        }

        //更新位移后新摄像头位置及新LookDirection值（相对方向向量未变）
        private void ForeBackTransformPosition(bool foreBack, double _Off)
        {
            Vector3D vLook = camera.LookDirection;   //屏幕前方
            vLook.Normalize();
            FocusPosition += (foreBack ? -1 : 1) * _Off * vLook;
            camera.Position = FocusPosition - DistanceToFocus * vLook; //摄像头固定距离在聚焦点的后方
            CameraToFocus();
        }
        //更新左右移后新摄像头位置及新LookDirection值（相对方向向量未变）
        private void HorizontalTransformPosition(bool leftRight, double _Off)
        {
            Vector3D vPosition = camera.Position - FocusPosition;
            Vector3D vHorizontal = Vector3D.CrossProduct(camera.UpDirection, vPosition); //获取屏幕左右的方向（两向量面的垂直向）
            vHorizontal.Normalize();
            FocusPosition += (leftRight ? -1 : 1) * _Off * vHorizontal;
            CameraToFocus();
        }
        //更新位移后新摄像头位置及新LookDirection值（相对方向向量未变）
        private void VerticalTransformPosition(bool upDown, double _Off)
        {
            Vector3D vVertical = camera.UpDirection;  //屏幕上方
            vVertical.Normalize();
            FocusPosition += (upDown ? -1 : 1) * _Off * vVertical;
            CameraToFocus();

        }
        //设置摄像头在焦点后的坐标
        private void CameraToFocus()
        {
            Vector3D vLook = camera.LookDirection;   //屏幕前方
            vLook.Normalize();
            camera.Position = FocusPosition - DistanceToFocus * vLook; //摄像头设置在焦点的后方
            ShowCameraPosition();
        }
        //按住方向键及移动键后持续转换摄像头位置
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsMoveFocus)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left: HorizontalTransform(false, keyDeltaFactor); break;
                case Key.Right: HorizontalTransform(true, keyDeltaFactor); break;
                case Key.Up: VerticalTransform(true, keyDeltaFactor); break;
                case Key.Down: VerticalTransform(false, keyDeltaFactor); break;
                case Key.A: HorizontalTransformPosition(true, keyDeltaFactor); break;
                case Key.D: HorizontalTransformPosition(false, keyDeltaFactor); break;
                case Key.W: ForeBackTransformPosition(false, keyDeltaFactor); break;
                case Key.S: ForeBackTransformPosition(true, keyDeltaFactor); break;
                case Key.Q: VerticalTransformPosition(false, keyDeltaFactor); break;
                case Key.E: VerticalTransformPosition(true, keyDeltaFactor); break;
            }
            ShowCameraPosition();
        }
        //显示镜头位置信息
        private void ShowCameraPosition()
        {
            tbCameraPosition.Text = "X: " + camera.Position.X.ToString("f2") + " Y: " + camera.Position.Y.ToString("f2") + " Z: " + camera.Position.Z.ToString("f2");
            tbCameraLookDirection.Text = "X: " + camera.LookDirection.X.ToString("f2") + " Y: " + camera.LookDirection.Y.ToString("f2") + " Z: " + camera.LookDirection.Z.ToString("f2"); ;
            tbCameraUpDirection.Text = "X: " + camera.UpDirection.X.ToString("f2") + " Y: " + camera.UpDirection.Y.ToString("f2") + " Z: " + camera.UpDirection.Z.ToString("f2"); ;

            //tbKeyDown.Text = IsKeyDown.ToString();
        }
        //回归中心原点
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            camera.Position = originPosition;
            camera.LookDirection = originLookDirection;
            camera.UpDirection = originUpDirection;
            FocusPosition = new Point3D(0, 0, 0);
            IsMoveFocus = false;
        }
        //设置可运动焦点
        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            IsMoveFocus = true;
        }
        //显示箭头
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            myPolyline.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            myPolyline.Visibility = Visibility.Hidden;
        }
    }
}
