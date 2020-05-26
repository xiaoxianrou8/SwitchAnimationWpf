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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp13
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Page1 Page1 = new Page1();
        public Page2 Page2 = new Page2();
        bool SwitchStatue = false;
        public MainWindow()
        {
            InitializeComponent();
            this.Nav.Children.Add(Page2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchStatue)
            {
                return;
            }
            
            if (this.Nav.Children.Count<1)
            {
                return;
            }
            var btn=sender as Button;
            if (btn==null)
            {
                return;
            }
            var pageStr = btn.Content;
            //记录当前page
            FrameworkElement currentPage = null;
            FrameworkElement nextPage = null;
            if (Nav.Children.Count!=0)
            {
                currentPage = Nav.Children[0] as FrameworkElement; 
            }
            else
            {
                return;
            }
            

            switch(pageStr)
            {
                case "page1":
                    {
                        nextPage = Page1;
                    }
                    break;
                case "page2":
                    {
                        nextPage = Page2;
                    }
                    break;
            }
            if (currentPage==nextPage)
            {
                return;
            }
            SwitchStatue = true;
            //if ()|| )
            //{
            //currentPage transformGroup
            if (!(currentPage.RenderTransform is TransformGroup))
            {
                TransformGroup cTransformGroup = new TransformGroup();
                var cScaleTransform = new ScaleTransform(0, 0);
                var cTranslateTransform = new TranslateTransform(0, 0);

                cTransformGroup.Children.Add(cScaleTransform);
                cTransformGroup.Children.Add(cTranslateTransform);
                Binding cBinding = new Binding("ScaleX");
                cBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
                BindingOperations.SetBinding(cScaleTransform, ScaleTransform.ScaleYProperty, cBinding);
                currentPage.RenderTransform = cTransformGroup;
            }



            if (!(nextPage.RenderTransform is TransformGroup))
            {
                TransformGroup nTransformGroup = new TransformGroup();
                var nScaleTransform = new ScaleTransform(0.5, 0);
                var nTranslateTransform = new TranslateTransform(0, 999999);

                nTransformGroup.Children.Add(nScaleTransform);
                nTransformGroup.Children.Add(nTranslateTransform);
                Binding nBinding = new Binding("ScaleX");
                nBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
                BindingOperations.SetBinding(nScaleTransform, ScaleTransform.ScaleYProperty, nBinding);
                nextPage.Opacity = 0;
                nextPage.RenderTransform = nTransformGroup;
            }
            currentPage.HorizontalAlignment = HorizontalAlignment.Center;
            currentPage.VerticalAlignment = VerticalAlignment.Center;
            currentPage.RenderTransformOrigin = new Point(0.5, 0.5);
            //nextPage transformGroup
            
            nextPage.HorizontalAlignment = HorizontalAlignment.Center;
            nextPage.VerticalAlignment = VerticalAlignment.Center;
            nextPage.RenderTransformOrigin = new Point(0.5, 0.5);
            var nextHeightBinding = new Binding();
            //var nextRelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor);
            //nextRelativeSource.AncestorType = typeof(Window);
            //nextHeightBinding.RelativeSource = nextRelativeSource;
            nextHeightBinding.ElementName = "myWindow";
            nextHeightBinding.Path = new PropertyPath("ActualHeight");
            var nextWidthBinding = new Binding();
            //nextWidthBinding.RelativeSource = nextRelativeSource;
            nextWidthBinding.ElementName="myWindow";
            nextWidthBinding.Path = new PropertyPath("ActualWidth");
            BindingOperations.SetBinding(nextPage, FrameworkElement.HeightProperty, nextHeightBinding);
            BindingOperations.SetBinding(nextPage, FrameworkElement.WidthProperty, nextWidthBinding);
            Nav.Children.Add(nextPage);
            //}



            Storyboard storyboard = new Storyboard();
            storyboard.FillBehavior=FillBehavior.HoldEnd;
            //currentPage消失
            DoubleAnimation scaleAnimation = new DoubleAnimation(1.0,0.5,TimeSpan.FromSeconds(0.4),FillBehavior.HoldEnd);
            CircleEase circleEase = new CircleEase();
            circleEase.EasingMode = EasingMode.EaseInOut;
            GravityEase gravityEase = new GravityEase();
           
            scaleAnimation.EasingFunction = gravityEase;
            DoubleAnimation opacityAnimation = new DoubleAnimation(1.0, 0, TimeSpan.FromSeconds(0.4), FillBehavior.HoldEnd);
            scaleAnimation.EasingFunction = circleEase;
            //nextPage出现
            DoubleAnimation nTranslateAnimation = new DoubleAnimation(Nav.ActualHeight, 0, TimeSpan.FromSeconds(0.4), FillBehavior.HoldEnd);
            nTranslateAnimation.BeginTime = TimeSpan.FromSeconds(0.15);
            nTranslateAnimation.EasingFunction = circleEase;
            DoubleAnimation nOpacityAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.4), FillBehavior.HoldEnd);
            nOpacityAnimation.BeginTime = TimeSpan.FromSeconds(0.15);
            nOpacityAnimation.EasingFunction = circleEase;
            DoubleAnimation nScaleAnimation = new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.4), FillBehavior.HoldEnd);
            nScaleAnimation.BeginTime = TimeSpan.FromSeconds(0.15);
            nScaleAnimation.EasingFunction = circleEase;



            Storyboard.SetTarget(opacityAnimation, currentPage);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(opacityAnimation);
            Storyboard.SetTargetProperty(scaleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(scaleAnimation, currentPage);
            storyboard.Children.Add(scaleAnimation);
            //nextPage 目标
            Storyboard.SetTarget(nTranslateAnimation, nextPage);
            Storyboard.SetTargetProperty(nTranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));
            storyboard.Children.Add(nTranslateAnimation);
            Storyboard.SetTarget(nOpacityAnimation, nextPage);
            Storyboard.SetTargetProperty(nOpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(nOpacityAnimation);
            Storyboard.SetTargetProperty(nScaleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            Storyboard.SetTarget(nScaleAnimation, nextPage);
            storyboard.Children.Add(nScaleAnimation);
            storyboard.Completed += Storyboard_Completed;
            storyboard.Begin();
           
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            SwitchStatue = false;
            Nav.Children.RemoveAt(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
