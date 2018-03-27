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
using System.Windows.Threading;

namespace WPF_Demo._3DDemo
{
    
    /// <summary>
    /// _3DDemoSample.xaml 的交互逻辑
    /// </summary>
    public partial class _3DDemoSample : Page
    {
        #region 字段 属性 事件 命令
        private DispatcherTimer _dispatcherTimer;  //移动时时间驱动对象
        private bool IsMoveView = true;           //是否运动视角
                                                   //private bool IsPoint3DAnimation = false;

        private int IsKeyDown = 0;  //记录按下方向键的数量
        private double _Off = 1;    //移动方向距离单位
        private int _plusX;         //移动X增向值-1 0 1
        private int _plusY;         //移动Y增向值-1 0 1
        private int _plusZ;         //移动Z增向值-1 0 1

        private Point3D originPosition;        //原始镜头坐标
        private Vector3D originLookDirection;  //原始镜头焦点方向
        private Vector3D originUpDirection;  //原始镜头焦点方向

        private Vector3D newLookDirection;     //移动镜头焦点后方向
        private Vector3D newUpDirection;     //移动摄像头正上方
        private Point3D newPosition = new Point3D();     //移动后镜头坐标


        private delegate void transformDelegate(Point3D p, Vector3D v);  //委托镜头执行移动
        public static RoutedCommand Fore = new RoutedCommand("Fore", typeof(string));

        #endregion
        public _3DDemoSample()
        {
            InitializeComponent();

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 4);  //可移动时每4毫秒移动镜头位置
            //_dispatcherTimer.Start();

            newPosition = camera.Position;
            newLookDirection = camera.LookDirection;
            newUpDirection = camera.UpDirection;

            originPosition = camera.Position;
            originLookDirection = camera.LookDirection;
            originUpDirection = camera.UpDirection;

            PositionAnimation(originPosition, originLookDirection);
        }
        //计时驱动 异步设置镜头新坐标
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdatePosition();
            //TransformCameraPosition();
            camera.Dispatcher.BeginInvoke(DispatcherPriority.Normal, //UI线程的委托方法
                new transformDelegate(PositionAnimation), newPosition, newLookDirection);
            //camera.BeginAnimation(PerspectiveCamera.PositionProperty, animationPoint3D);
        }
        //点击选择3D元素的中心为镜头聚焦点
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你好！");
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            //if (Mouse.LeftButton==MouseButtonState.Pressed)
            //{
            //    Point newMousePosition = e.GetPosition(this);
            //    if (mouseLeftPosition.X!=newMousePosition.X)
            //    {
            //        HorizontalTransform(mouseLeftPosition.X < newMousePosition.X, mouseDeltaFactor);//水平变化
            //    }
            //    if (mouseLeftPosition.Y<newMousePosition.Y)
            //    {
            //        VerticalTransform(mouseLeftPosition.Y > newMousePosition.Y, mouseDeltaFactor);//垂直变化
            //    }
            //    mouseLastPosition = newMousePosition;
            //}

        }
        //画面绕屏幕中心点（摄像头）垂直旋转
        private void VerticalTransform(bool upDown, double angleDeltaFactor)
        {
            Vector3D position = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z); //获取相对原点向量
            Vector3D rotateAxis = Vector3D.CrossProduct(position, camera.UpDirection);  //获取中心原地向量+正上方向量 的组合面 的垂直方向（屏幕左右方向）作为绕轴
            RotateTransform3D rt3d = new RotateTransform3D();
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (upDown ? -1 : 1));//初始化3D旋转对象
            rt3d.Rotation = rotate;
            Matrix3D martix = rt3d.Value;                                              //获取3*3矩阵对象
            Point3D newPosition = martix.Transform(camera.Position);                   //以源坐标（摄像头自身）通过矩阵转换为新坐标
            camera.Position = newPosition;
            camera.LookDirection = new Vector3D(-newPosition.X, -newPosition.Y, -newPosition.Z);

            //update the up direction;
            Vector3D newUpDirection = Vector3D.CrossProduct(camera.LookDirection, rotateAxis);  //相对原地方向+LookDirector方向的组合面 的垂直方向 就为新的UpDirector
            newUpDirection.Normalize();
            camera.UpDirection = newUpDirection;
        }
        //画面绕屏幕中心点（摄像头）水平旋转:为达到以摄像头自身为中心点（屏幕中点） 绕UpDirection轴（屏幕上下轴） 旋转自身水平角度（左右距离）  
        //获得的相对中心原点的摄像头模型信息（新位置、新UpDirection、新LookDirection)
        private void HorizontalTransform(bool leftRight, double angleDeltaFactor)
        {
            Vector3D position = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z);//相对原点向量
            Vector3D rotateAxis = camera.UpDirection;                               //摄像机的正上方作为旋转轴
            RotateTransform3D rt3d = new RotateTransform3D();                       //设置绕轴旋转对象（轴、转角）
            AxisAngleRotation3D rotate = new AxisAngleRotation3D(rotateAxis, angleDeltaFactor * (leftRight ? -1 : 1));
            rt3d.Rotation = rotate;
            Matrix3D martix = rt3d.Value;            //设置4*4的矩形转换对象
            Point3D newPosition = martix.Transform(camera.Position);  //矩形转换对象根据原坐标（摄像机位置）变换新坐标
            camera.Position = newPosition;
            camera.LookDirection = new Vector3D(-newPosition.X, -newPosition.Y, -newPosition.Z); //  。。。

            //update the up direction;
            Vector3D newUpDirection = Vector3D.CrossProduct(camera.LookDirection, rotateAxis);
            newUpDirection.Normalize();
            camera.UpDirection = newUpDirection;
        }

        //鼠标滚轮 摄像镜头方向画面缩放远近
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (camera.FieldOfView < 170)
                {
                    camera.FieldOfView += _Off;
                }
            }
            if (e.Delta < 0)
            {
                if (camera.FieldOfView > 10)
                {
                    camera.FieldOfView -= _Off;
                }
            }
            ShowCameraPosition();

            //double scaleFactor = 5;
            ////120 near,-120 far
            //System.Diagnostics.Debug.WriteLine(e.Delta.ToString());
            //Point3D currentPosition = camera.Position;
            //Vector3D lookDirection = camera.LookDirection;//new Vector3D(camera.LookDirection.X,Y,Z);
            //lookDirection.Normalize();

            //lookDirection *= scaleFactor;
            //if (e.Delta==120) //get near
            //{
            //    if ((currentPosition.X+lookDirection.X)*currentPosition.X>0)
            //    {
            //        currentPosition += lookDirection;
            //    }
            //}
            //if (e.Delta==-120) //getting far
            //{
            //    currentPosition -= lookDirection;
            //}

            //Point3DAnimation positionAnimation = new Point3DAnimation();
            //positionAnimation.BeginTime = new TimeSpan(0, 0, 0);
            //positionAnimation.Duration = TimeSpan.FromMilliseconds(100);
            //positionAnimation.To = currentPosition;
            //positionAnimation.From = camera.Position;
            ////positionAnimation.Completed += new EventHandler(positionAnimation_Completed);
            //camera.BeginAnimation(PerspectiveCamera.PositionProperty, positionAnimation, HandoffBehavior.Compose);            
        }


        //跟踪虚拟球按键的方向位移
        private void trackball_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsMoveView)
            {
                if (e.Key == Key.W)
                {
                    _plusZ = -1; //往屏幕里-z方向
                    IsKeyDown += 1;
                }
                if (e.Key == Key.S)
                {
                    _plusZ = 1;//往屏幕外+z方向
                    IsKeyDown += 1;
                }
                if (e.Key == Key.A)
                {
                    _plusX = -1;//往屏幕左-x方向
                    IsKeyDown += 1;
                }
                if (e.Key == Key.D)
                {
                    _plusX = 1;//往屏幕右+x方向
                    IsKeyDown += 1;
                }
                if (e.Key == Key.Q)
                {
                    _plusY = 1;//往屏幕上+Y方向
                    IsKeyDown += 1;
                }
                if (e.Key == Key.E)
                {
                    _plusY = -1;//往屏幕下-Y方向
                    IsKeyDown += 1;
                }
                if (IsKeyDown > 0)
                {
                    if (!_dispatcherTimer.IsEnabled)
                    {
                        _dispatcherTimer.Start(); //符合运动条件，启动移动线程方法
                    }
                }
            }
            else
            {
                if (_dispatcherTimer.IsEnabled)
                {
                    _dispatcherTimer.Stop();
                }
            }
            ShowCameraPosition();
        }
        //更新位移后新摄像头位置及新LookDirection值（相对方向向量未变）
        private void UpdatePosition()
        {
            //屏幕左右方向设置新坐标
            Vector3D vPosition = new Vector3D(newPosition.X, newPosition.Y, newPosition.Z);
            Vector3D vHorizontal = Vector3D.CrossProduct(newUpDirection, vPosition); //获取屏幕左右的方向（两向量面的垂直向）
            vHorizontal.Normalize();
            newPosition += _plusX * _Off * vHorizontal;    //因为摄像头的LookDirection总是指向固定焦点，所有作用移动就是绕焦点转圈

            //屏幕上下方向设置新坐标
            Vector3D vVertical = newUpDirection;
            vVertical.Normalize();
            newPosition += _plusY * _Off * vVertical;

            //屏幕前后方向设置新坐标
            Vector3D vLook = newLookDirection;   //屏幕前方
            vLook.Normalize();
            newPosition += -_plusZ * _Off * vLook;

            Vector3D vLookDirection = newLookDirection;
            vLookDirection.Normalize();
            Vector3D vUpDirection = newUpDirection;
            vUpDirection.Normalize();


            //TranslateTransform3D tt3D = new TranslateTransform3D(new Vector3D(_plusX,_plusY,_plusZ)*_intOff); //设置转移的方向的距离
            //Vector3D vOldPosition3D = new Vector3D(currentPosition.X, currentPosition.Y, currentPosition.Z); //设置源镜头位置的原地向量
            //Vector3D vTransform = Vector3D.CrossProduct(vOldPosition3D, currentLookDirection);//转换为向量叉乘积
            //Matrix3D matrix = tt3D.Value;
            ////newPosition = matrix.Transform(new Point3D(vTransform.X, vTransform.Y,vTransform.Z));
            ////newLookDirection = matrix.Transform(vTransform);
            //newPosition = matrix.Transform(currentPosition);  //使用Matrix3D确定源静态坐标转移后的相对坐标
            //newLookDirection = new Vector3D(-newPosition.X, -newPosition.Y, -newPosition.Z);


        }
       
        //设置摄像头最新位置
        private void PositionAnimation(Point3D p, Vector3D v)  //需由UI线程调用
        {
            camera.Position = p;
            //camera.LookDirection = v; //此处不需要更新镜头视角，由3Dtools设定总是对焦中心
            ShowCameraPosition();
        }
        //显示镜头位置信息
        private void ShowCameraPosition()
        {
            tbCameraPosition.Text = "X: " + camera.Position.X.ToString("f2") + " Y: " + camera.Position.Y.ToString("f2") + " Z: " + camera.Position.Z.ToString("f2");
            tbCameraLookDirection.Text = "X: " + camera.LookDirection.X.ToString("f2") + " Y: " + camera.LookDirection.Y.ToString("f2") + " Z: " + camera.LookDirection.Z.ToString("f2"); ;
            tbCameraUpDirection.Text = "X: " + camera.UpDirection.X.ToString("f2") + " Y: " + camera.UpDirection.Y.ToString("f2") + " Z: " + camera.UpDirection.Z.ToString("f2"); ;
            tbCameraFiledOfView.Text = "X: " + camera.FieldOfView.ToString();

            //tbKeyDown.Text = IsKeyDown.ToString();
        }
        //方向键弹起事件
        private void trackball_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsMoveView)
            {
                if (e.Key == Key.W)
                {
                    _plusZ = 0; //往屏幕里-z方向
                    IsKeyDown -= 1;
                }
                if (e.Key == Key.S)
                {
                    _plusZ = 0;//往屏幕外+z方向
                    IsKeyDown -= 1;
                }
                if (e.Key == Key.A)
                {
                    _plusX = 0;//往屏幕左-x方向
                    IsKeyDown -= 1;
                }
                if (e.Key == Key.D)
                {
                    _plusX = 0;//往屏幕右+x方向
                    IsKeyDown -= 1;
                }
                if (e.Key == Key.Q)
                {
                    _plusY = 0;//往屏幕上+Y方向
                    IsKeyDown -= 1;
                }
                if (e.Key == Key.E)
                {
                    _plusY = 0;//往屏幕下-Y方向
                    IsKeyDown -= 1;
                }
                if (_dispatcherTimer.IsEnabled)
                {
                    _dispatcherTimer.Stop();
                }
                //if (IsPoint3DAnimation)  //若有动画
                //{
                //    IsPoint3DAnimation = false;
                //}                  

            }
        }
        //设置初始摄像头信息
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IsMoveView = false;
            //回归中心原点
            PositionAnimation(originPosition, originLookDirection);
            camera.Position = originPosition;
            camera.LookDirection = originLookDirection;
            camera.UpDirection = originUpDirection;
            camera.FieldOfView = 100;
        }
        //设置摄像头为可以移动
        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            IsMoveView = true;
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


        private void trackball_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed |
                e.RightButton == MouseButtonState.Pressed)
            {
                ShowCameraPosition();
            }
        }
    }
}
