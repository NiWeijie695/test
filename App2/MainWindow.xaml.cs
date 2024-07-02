using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App2
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // 设置窗口大小
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            // 设置窗口大小
            SetWindowSize(1360, 955);

            Button1_Click(Button1, null);
            Button_Click_PF(null, null);
        }

        private void SetWindowSize(int width, int height)
        {
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                appWindow.Resize(new SizeInt32(width, height));
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            CreateButtonsForMiddlePanel(new string[] { "Picture Format", "Brightness", "Contrast", "Resolution", "Input", "PIP/PBP" });
            Button_Click_PF(null, null);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            CreateButtonsForMiddlePanel(new string[] { "Color Temperature", "Display Gamma", "Smart Image", "Application Preset Mode" });
            Button_Click_CT(null, null);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            CreateButtonsForMiddlePanel(new string[] { "Audio" });
            Button_Click_Audio(null, null);
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            CreateButtonsForMiddlePanel(new string[] { "Power Schedule", "Power LED" });
            Button_Click_PS(null, null);
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            CreateButtonsForMiddlePanel(new string[] { "Smart Desktop" });
            Button_Click_SD(null, null);
        }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            CreateButtonsForMiddlePanel(new string[] { "Multi Monitor", "Screen Rotate", "Reset", "Information", "About", "Version Upgrade", "Language", "Remote Control" });
            Button_Click_MM(null, null);
        }

        private void Button_Click_PF(object sender, RoutedEventArgs e)////////////////////////////////Picture
        {
            RightPanel.Children.Clear();
            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Picture Format.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            imageGrid.Children.Add(image);

            //控件布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            string[] optionContents = { "Wide", "4:3", "1:1", "Movie1", "Movie2", "16:9" };
            int row = 2; // 从第三行开始放置RadioButton
            int column = -1;

            foreach (var content in optionContents)
            {
                var radioButton = new RadioButton()
                {
                    Content = content,
                    Margin = new Thickness(0, 150, 0, 0) // 设置每个控件之间的间距
                };
                Grid.SetRow(radioButton, row);
                Grid.SetColumn(radioButton, column + 1); // 放在中间列
                controlGrid.Children.Add(radioButton);

                column++;
                if (column > 1)
                {
                    column = -1;
                    row++;
                }
            }

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_Brightness(object sender, RoutedEventArgs e)////////////////////////////////Picture
        {
            RightPanel.Children.Clear();
            Grid grid = new Grid();

            // 定义行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 滑动条行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 按钮行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Brightness.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 1); // 设置在第二行
            Grid.SetColumn(image, 0); // 设置在中间列
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 创建滑动条和显示值
            Slider progressBar = new Slider() { Minimum = 0, Maximum = 100, Value = 0, Width = 500, Margin = new Thickness(0, 500, 0, 0) };
            TextBlock progressValue = new TextBlock() { Text = "0", Margin = new Thickness(0, 500, 0, 0) };

            progressBar.ValueChanged += (s, ev) =>
            {
                progressValue.Text = progressBar.Value.ToString();
            };

            StackPanel progressPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            progressPanel.Children.Add(progressBar);
            progressPanel.Children.Add(progressValue);

            Grid.SetRow(progressPanel, 2); // 设置在第三行
            Grid.SetColumn(progressPanel, 1); // 设置在中间列
            controlGrid.Children.Add(progressPanel);

            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Reset",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 20, 0)
            };
            Grid.SetRow(buttonReset, 3); // 设置在第四行
            Grid.SetColumn(buttonReset, 1); // 设置在中间列
            grid.Children.Add(buttonReset);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_Contrast(object sender, RoutedEventArgs e)////////////////////////////////Picture
        {
            RightPanel.Children.Clear();
            Grid grid = new Grid();

            // 定义行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 滑动条行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 按钮行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Contrast.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 1); // 设置在第二行
            Grid.SetColumn(image, 0); // 设置在中间列
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 创建滑动条和显示值
            Slider progressBar = new Slider() { Minimum = 0, Maximum = 100, Value = 0, Width = 500, Margin = new Thickness(0, 500, 0, 0) };
            TextBlock progressValue = new TextBlock() { Text = "0", Margin = new Thickness(0, 500, 0, 0) };

            progressBar.ValueChanged += (s, ev) =>
            {
                progressValue.Text = progressBar.Value.ToString();
            };

            StackPanel progressPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            progressPanel.Children.Add(progressBar);
            progressPanel.Children.Add(progressValue);

            Grid.SetRow(progressPanel, 2); // 设置在第三行
            Grid.SetColumn(progressPanel, 1); // 设置在中间列
            controlGrid.Children.Add(progressPanel);

            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Reset",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 20, 0)
            };
            Grid.SetRow(buttonReset, 3); // 设置在第四行
            Grid.SetColumn(buttonReset, 1); // 设置在中间列
            grid.Children.Add(buttonReset);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_Resolution(object sender, RoutedEventArgs e)////////////////////////////////Picture
        {
            RightPanel.Children.Clear();
            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 选择类控件行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 文本和组合框行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Resolution.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 1); // 设置在第一行
            Grid.SetColumn(image, 0); // 设置在第一列
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 创建选择类控件（带小正方形）
            CheckBox checkBox = new CheckBox() { Content = "Resolution Notification", Margin = new Thickness(0, 140, 0, 10) };
            Grid.SetRow(checkBox, 2); // 设置在第二行
            Grid.SetColumn(checkBox, 0); // 设置在第一列
            controlGrid.Children.Add(checkBox);

            // 创建文本和组合框
            TextBlock textBlock = new TextBlock() { Text = "Resolution", Margin = new Thickness(0, 5, 0, 5) };
            ComboBox comboBox = new ComboBox() { Margin = new Thickness(0, 0, 0, 10) };
            string[] comboBoxItems = { "2560x1440-60Hz", "2560x1440-59Hz", "2048x1152-59Hz", "2048x1152-60Hz", "1920x1080-59Hz", "1920x1080-60Hz" };
            foreach (var item in comboBoxItems)
            {
                comboBox.Items.Add(new ComboBoxItem() { Content = item });
            }

            Grid.SetRow(textBlock, 3); // 设置在第三行
            Grid.SetColumn(textBlock, 0); // 设置在第一列
            grid.Children.Add(textBlock);

            Grid.SetRow(comboBox, 5); // 设置在第三行
            Grid.SetColumn(comboBox, 0); // 设置在第一列
            grid.Children.Add(comboBox);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_Input(object sender, RoutedEventArgs e)////////////////////////////////Picture
        {
            RightPanel.Children.Clear();

            Grid grid = new Grid();
            for (int i = 0; i < 10; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Input.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 1);
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            for (int i = 0; i < 10; i++)
            {
                controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            string[] contents = { "Auto", "DP1", "DP2", "HDMI1", "HDMI2", "HDMI3", "VGA1", "DSub", "DVI", "USBC1", "USBC2" };

            // 创建选择类控件
            for (int i = 0; i < contents.Length; i++)
            {
                Control control;
                if (i == 0)
                {
                    control = new CheckBox() { Content = contents[i], Margin = new Thickness(0, 140, 0, 0) };
                }
                else
                {
                    control = new RadioButton() { Content = contents[i], Margin = new Thickness(0, 10, 0, 0) };
                }

                if (i < 9)
                {
                    Grid.SetRow(control, i + 1);
                    Grid.SetColumn(control, 0);
                }
                else
                {
                    Grid.SetRow(control, i - 7);
                    Grid.SetColumn(control, 1);
                }

                controlGrid.Children.Add(control);
            }

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }
        private void Button_Click_PP(object sender, RoutedEventArgs e)////////////////////////////////Picture
        {
            RightPanel.Children.Clear();

            Grid grid = new Grid();
            for (int i = 0; i < 8; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/sample_image.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            for (int i = 0; i < 7; i++)
            {
                controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            // 添加PIP/PBP文本
            TextBlock pipPbpText = new TextBlock() { Text = "PIP/PBP", FontSize = 24, Margin = new Thickness(0, 150, 0, 10) };
            Grid.SetRow(pipPbpText, 1);
            Grid.SetColumnSpan(pipPbpText, 2);
            controlGrid.Children.Add(pipPbpText);

            // 创建选择类控件
            string[] contents = { "OFF", "PIP", "PBP_1", "PBP_2", "PBP_3" };
            for (int i = 0; i < contents.Length; i++)
            {
                RadioButton radioButton = new RadioButton() { Content = contents[i], Margin = new Thickness(0, 10, 0, 10) };
                Grid.SetRow(radioButton, i + 2);
                Grid.SetColumn(radioButton, 0);
                grid.Children.Add(radioButton);
            }

            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Apply",
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 30, 230, 0)
            };
            Grid.SetRow(buttonReset, 8); // 设置在第四行
            Grid.SetColumn(buttonReset, 0); // 设置在中间列
            grid.Children.Add(buttonReset);

            // 创建并添加右下角按钮
            Button buttonReset1 = new Button()
            {
                Content = "Swap",
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 30, 100, 0)
            };
            Grid.SetRow(buttonReset1, 8); // 设置在第四行
            Grid.SetColumn(buttonReset1, 0); // 设置在中间列
            grid.Children.Add(buttonReset1);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }


        private void Button_Click_CT(object sender, RoutedEventArgs e)/////////////////////////Color
        {
            RightPanel.Children.Clear();

            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Color Temperature.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            imageGrid.Children.Add(image);

            //控件布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            string[] optionContents = { "sRGB", "5000K", "6500K", "7500K", "8200K", "9300K", "11500K", "User Define" };
            int row = 2; // 从第三行开始放置RadioButton
            int column = -1;

            foreach (var content in optionContents)
            {
                var radioButton = new RadioButton()
                {
                    Content = content,
                    Margin = new Thickness(0, 150, 0, 0) // 设置每个控件之间的间距
                };
                Grid.SetRow(radioButton, row);
                Grid.SetColumn(radioButton, column + 1); // 放在中间列
                controlGrid.Children.Add(radioButton);

                column++;
                if (column > 1)
                {
                    column = -1;
                    row++;
                }
            }
            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_DG(object sender, RoutedEventArgs e)/////////////////////////Color
        {
            RightPanel.Children.Clear();

            Grid grid = new Grid();
            for (int i = 0; i < 6; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Display Gamma.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 1);
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            for (int i = 0; i < 10; i++)
            {
                controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            string[] contents = { "1.8", "2.0", "2.2", "2.4", "2.6" };
            for (int i = 0; i < contents.Length; i++)
            {
                RadioButton radioButton = new RadioButton() { Content = contents[i], Margin = new Thickness(0, 10, 0, 10) };
                Grid.SetRow(radioButton, i + 1);
                Grid.SetColumn(radioButton, 0);
                grid.Children.Add(radioButton);
            }

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_SI(object sender, RoutedEventArgs e)/////////////////////////Color
        {
            RightPanel.Children.Clear();

            Grid grid = new Grid();
            for (int i = 0; i < 10; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Smart Image.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 1);
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            for (int i = 0; i < 10; i++)
            {
                controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            string[] contents = { "Standard", "FPS", "Gamer1", "Gamer2", "Racing", "RTS", "LowBlueMode", "EasyRead", "Smart Uniformity" };
            for (int i = 0; i < contents.Length; i++)
            {
                RadioButton radioButton = new RadioButton() { Content = contents[i], Margin = new Thickness(0, 10, 0, 10) };
                Grid.SetRow(radioButton, i + 1);
                Grid.SetColumn(radioButton, 0);
                grid.Children.Add(radioButton);
            }

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_APM(object sender, RoutedEventArgs e)/////////////////////////Color
        {
            RightPanel.Visibility = Visibility.Visible;

            RightPanel.Children.Clear();

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            Grid buttonGrid = new Grid();
            buttonGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            Button button = new Button
            {
                Content = "+ Add an application",
                Margin = new Thickness(0,50,400,0),
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray),
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White)
            };
            Grid.SetRow(button, 0);
            Grid.SetColumn(button, 0);
            buttonGrid.Children.Add(button);

            // 将按钮Grid添加到主Grid
            Grid.SetRow(buttonGrid, 1);
            grid.Children.Add(buttonGrid);

            // 添加一个10行3列的空白表格
            Grid tableGrid = new Grid
            {
                Margin = new Thickness(0), // 移除外层Grid的Margin，防止影响边框线
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray)
            };

            // 定义行和列
            for (int i = 0; i < 10; i++)
            {
                tableGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
            for (int j = 0; j < 3; j++)
            {
                tableGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // 设置第一行的内容
            Border applicationBorder = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Black),
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray),
                Child = new TextBlock
                {
                    Text = "Application",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                }
            };
            Grid.SetRow(applicationBorder, 0);
            Grid.SetColumn(applicationBorder, 0);
            tableGrid.Children.Add(applicationBorder);

            Border presetModeBorder = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Black),
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray),
                Child = new TextBlock
                {
                    Text = "Application Preset Mode",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                }
            };
            Grid.SetRow(presetModeBorder, 0);
            Grid.SetColumn(presetModeBorder, 1);
            tableGrid.Children.Add(presetModeBorder);

            Border emptyBorder = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Black),
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray)
            };
            Grid.SetRow(emptyBorder, 0);
            Grid.SetColumn(emptyBorder, 2);
            tableGrid.Children.Add(emptyBorder);

            // 合并下面的单元格
            for (int row = 1; row < 10; row++)
            {
                Border border = new Border
                {
                    BorderThickness = new Thickness(10),
                    BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.White),
                    Background = new SolidColorBrush(Microsoft.UI.Colors.White) // 设置背景颜色为白色
                };
                Grid.SetRow(border, row);
                Grid.SetColumnSpan(border, 3); // 合并该行的所有列
                tableGrid.Children.Add(border);
            }

            // 将表格Grid添加到带黑色边框的外层Border
            Border outerBorder = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Microsoft.UI.Colors.Black),
                Child = tableGrid
            };

            // 将带边框的表格添加到主Grid
            Grid.SetRow(outerBorder, 2);
            grid.Children.Add(outerBorder);

            // 将主Grid添加到RightPanel
            RightPanel.Children.Add(grid);
        }

        private void Button_Click_Audio(object sender, RoutedEventArgs e)/////////////////Audio
        {
            RightPanel.Children.Clear();
            Grid grid = new Grid();

            // 定义行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 滑动条行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 按钮行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Audio.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 1); // 设置在第二行
            Grid.SetColumn(image, 0); // 设置在中间列
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 创建选择类控件（带小正方形）
            CheckBox checkBox = new CheckBox() { Content = "Mute", Margin = new Thickness(0, 400, 0, 10) };
            Grid.SetRow(checkBox, 2); // 设置在第二行
            Grid.SetColumn(checkBox, 0); // 设置在第一列
            controlGrid.Children.Add(checkBox);
            // 创建滑动条和显示值
            Slider progressBar = new Slider() { Minimum = 0, Maximum = 100, Value = 0, Width = 500, Margin = new Thickness(0, 500, 0, 0) };
            TextBlock progressValue = new TextBlock() { Text = "0", Margin = new Thickness(0, 500, 0, 0) };

            progressBar.ValueChanged += (s, ev) =>
            {
                progressValue.Text = progressBar.Value.ToString();
            };

            StackPanel progressPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            progressPanel.Children.Add(progressBar);
            progressPanel.Children.Add(progressValue);

            Grid.SetRow(progressPanel, 2); // 设置在第三行
            Grid.SetColumn(progressPanel, 1); // 设置在中间列
            controlGrid.Children.Add(progressPanel);

            TextBlock text = new TextBlock() { Text = "Volume", Margin = new Thickness(20, 490, 0, 0) };
            Grid.SetRow(text, 2); // 设置在第三行
            Grid.SetColumn(text, 0); // 设置在第一列
            controlGrid.Children.Add(text);

            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Reset",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 20, 0)
            };
            Grid.SetRow(buttonReset, 3); // 设置在第四行
            Grid.SetColumn(buttonReset, 1); // 设置在中间列
            grid.Children.Add(buttonReset);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_PS(object sender, RoutedEventArgs e)////////////////ECO
        {
            RightPanel.Children.Clear();

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件行
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Power Schedule.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 设置在第一行
            Grid.SetColumn(image, 0); // 设置在第一列
            imageGrid.Children.Add(image);

            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//选择类控件
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//文本
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//滑动条
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//文本
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//滑动条
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//选择类控件
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//文本
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 为RadioButton添加一个新行
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//为button添加一个新行
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 创建选择类控件（带小正方形）
            CheckBox checkBox1 = new CheckBox() { Content = "Power Saving", Margin = new Thickness(0, 110, 0, 0) };
            Grid.SetRow(checkBox1, 1); // 设置在第二行
            Grid.SetColumn(checkBox1, 0); // 设置在第一列
            controlGrid.Children.Add(checkBox1);

            TextBlock label = new TextBlock() { Text = "Idle Timer/(Minute)", Margin = new Thickness(30, 0, 0, 0), FontSize = 12 };
            Grid.SetRow(label, 2); // 设置在第三行
            Grid.SetColumn(label, 0); // 设置在中间列
            controlGrid.Children.Add(label);
            // 创建滑动条和显示值
            Slider progressBar1 = new Slider() { Minimum = 0, Maximum = 100, Value = 0, Width = 500, Margin = new Thickness(0, -10, 0, 0) };
            TextBlock progressValue1 = new TextBlock() { Text = "0", Margin = new Thickness(0, -10, 0, 0) };

            progressBar1.ValueChanged += (s, ev) =>
            {
                progressValue1.Text = progressBar1.Value.ToString();
            };

            StackPanel progressPanel1 = new StackPanel() { Orientation = Orientation.Horizontal };
            progressPanel1.Children.Add(progressBar1);
            progressPanel1.Children.Add(progressValue1);

            Grid.SetRow(progressPanel1, 3); // 设置在第四行
            Grid.SetColumn(progressPanel1, 0); // 设置在中间列
            controlGrid.Children.Add(progressPanel1);

            TextBlock labe2 = new TextBlock() { Text = "Power Off Timer/(Minute)", Margin = new Thickness(30, -10, 0, 0), FontSize = 12 };
            Grid.SetRow(labe2, 4); // 设置在第三行
            Grid.SetColumn(labe2, 0); // 设置在中间列
            controlGrid.Children.Add(labe2);
            // 创建滑动条和显示值
            Slider progressBar2 = new Slider() { Minimum = 0, Maximum = 100, Value = 0, Width = 500, Margin = new Thickness(0, -10, 0, 0) };
            TextBlock progressValue2 = new TextBlock() { Text = "0", Margin = new Thickness(0, -10, 0, 0) };

            progressBar2.ValueChanged += (s, ev) =>
            {
                progressValue2.Text = progressBar2.Value.ToString();
            };

            StackPanel progressPanel2 = new StackPanel() { Orientation = Orientation.Horizontal };
            progressPanel2.Children.Add(progressBar2);
            progressPanel2.Children.Add(progressValue2);

            Grid.SetRow(progressPanel2, 5); // 设置在第四行
            Grid.SetColumn(progressPanel2, 0); // 设置在中间列
            controlGrid.Children.Add(progressPanel2);

            // 创建选择类控件（带小正方形）
            CheckBox checkBox2 = new CheckBox() { Content = "Power off schedule", Margin = new Thickness(0, -10, 0, 0) };
            Grid.SetRow(checkBox2, 6); // 设置在第二行
            Grid.SetColumn(checkBox2, 0); // 设置在第一列
            controlGrid.Children.Add(checkBox2);
            // 创建文本和组合框
            TextBlock textBlock = new TextBlock() { Text = "Power Off At Time", Margin = new Thickness(0, 0, 0, 0) };
            ComboBox comboBox = new ComboBox() { Margin = new Thickness(0, 0, 0, 0) };
            string[] comboBoxItems = { "10s" };
            foreach (var item in comboBoxItems)
            {
                comboBox.Items.Add(new ComboBoxItem() { Content = item });
            }

            Grid.SetRow(textBlock, 7); // 设置在第三行
            Grid.SetColumn(textBlock, 0); // 设置在第一列
            controlGrid.Children.Add(textBlock);

            Grid.SetRow(comboBox, 8); // 设置在第三行
            Grid.SetColumn(comboBox, 0); // 设置在第一列
            controlGrid.Children.Add(comboBox);

            // 创建RadioButton并放置在ComboBox下方
            Grid radioButtonGrid = new Grid();
            radioButtonGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            radioButtonGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            radioButtonGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            radioButtonGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            radioButtonGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            string[] optionContents = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int row = 0; // 从第三行开始放置RadioButton
            int column = 0;

            foreach (var content in optionContents)
            {
                var radioButton = new RadioButton()
                {
                    Content = content,
                    Margin = new Thickness(0, 0, 0, 0), // 设置每个控件之间的间距
                    FontSize = 12
                };
                Grid.SetRow(radioButton, row);
                Grid.SetColumn(radioButton, column); // 放在中间列
                radioButtonGrid.Children.Add(radioButton);

                row++;
                if (row > 2)
                {
                    row = 0;
                    column++;
                }
            }
            Grid.SetRow(radioButtonGrid, 9); // 设置在ComboBox的下一行
            Grid.SetColumn(radioButtonGrid, 0); // 设置在第一列
            controlGrid.Children.Add(radioButtonGrid);

            // 创建并添加右下角按钮
            Grid buttonGrid = new Grid();
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            Button buttonReset = new Button()
            {
                Content = "Stand by Now",
                Margin = new Thickness(10)
            };
            Grid.SetRow(buttonReset, 0); // 设置在第十行
            Grid.SetColumn(buttonReset, 1); // 设置在中间列
            buttonGrid.Children.Add(buttonReset);

            Button buttonReset1 = new Button()
            {
                Content = "Turn Power Off Now",
                Margin = new Thickness(10)
            };
            Grid.SetRow(buttonReset1, 0); // 设置在第十行
            Grid.SetColumn(buttonReset1, 2); // 设置在中间列
            buttonGrid.Children.Add(buttonReset1);

            Grid.SetRow(buttonGrid, 10); // 设置在RadioButton的下一行
            Grid.SetColumn(buttonGrid, 0); // 设置在第一列
            controlGrid.Children.Add(buttonGrid);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }


        private void Button_Click_PLED(object sender, RoutedEventArgs e)////////////////ECO
        {
            RightPanel.Children.Clear();

            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Power LED.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            imageGrid.Children.Add(image);

            //控件布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            TextBlock label = new TextBlock() {
                Text = "There are four brightness settings for the Power LED indicator ranging from 0 (off) to 4 (high) that you can adjust according to your preference.",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 130, 0, 0),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 500 // 设置宽度以触发换行
            };
            Grid.SetRow(label, 1); // 设置在第三行
            Grid.SetColumn(label, 0); // 设置在中间列
            controlGrid.Children.Add(label);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);
            RightPanel.Children.Add(grid);
        }

        private void Button_Click_SD(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();

            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 文本Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });//控件
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Smart Desktop.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            imageGrid.Children.Add(image);

            //文本布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            TextBlock label = new TextBlock()
            {
                Text = "Smart Desktop",
                Margin = new Thickness(0, 130, 0, 0),
                
            };
            Grid.SetRow(label, 1); // 设置在第三行
            Grid.SetColumn(label, 0); // 设置在中间列
            controlGrid.Children.Add(label);
            TextBlock labe2 = new TextBlock()
            {
                Text = "Smart Desktop is a window manager utility for arranging and snapping windows into efficient layouts to improve the speed of your workflow and restore layouts quickly.",
                Margin = new Thickness(0, 30, 0, 0),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 540 // 设置宽度以触发换行
            };
            Grid.SetRow(labe2, 2); // 设置在第三行
            Grid.SetColumn(labe2, 0); // 设置在中间列
            controlGrid.Children.Add(labe2);
            TextBlock labe3 = new TextBlock()
            {
                Text = "Please download before using this utility.",
                Margin = new Thickness(0, 30, 0, 0),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 550 // 设置宽度以触发换行
            };
            Grid.SetRow(labe3, 3); // 设置在第三行
            Grid.SetColumn(labe3, 0); // 设置在中间列
            controlGrid.Children.Add(labe3);

            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Download",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 200, 20, 0)
            };
            Grid.SetRow(buttonReset, 4); // 设置在第四行
            Grid.SetColumn(buttonReset, 0); // 设置在中间列
            controlGrid.Children.Add(buttonReset);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);
            
            RightPanel.Children.Add(grid);
        }

        private void Button_Click_MM(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Multi Monitor.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            imageGrid.Children.Add(image);

            //控件布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 创建文本和组合框
            TextBlock textBlock = new TextBlock() { Text = "Multi Monitor", Margin = new Thickness(0, 5, 0, 5) };
            ComboBox comboBox = new ComboBox() { Margin = new Thickness(0, 20, 0, 10) };
            string[] comboBoxItems = { "0-PHLC301_27M2C5500W" };
            foreach (var item in comboBoxItems)
            {
                comboBox.Items.Add(new ComboBoxItem() { Content = item });
            }

            Grid.SetRow(textBlock, 1); // 设置在第三行
            Grid.SetColumn(textBlock, 0); // 设置在第一列
            grid.Children.Add(textBlock);

            Grid.SetRow(comboBox, 2); // 设置在第三行
            Grid.SetColumn(comboBox, 0); // 设置在第一列
            grid.Children.Add(comboBox);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_SR(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Screen Rotate.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            imageGrid.Children.Add(image);

            //控件布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            
            TextBlock labe2 = new TextBlock()
            {
                Text = "For monitors that are equipped with a Height Adjustable Stand (HAS), you are able to rotate the screen according to your preference.",
                Margin = new Thickness(0, 130, 0, 0),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 540 // 设置宽度以触发换行
            };
            Grid.SetRow(labe2, 1); // 设置在第三行
            Grid.SetColumn(labe2, 0); // 设置在中间列
            controlGrid.Children.Add(labe2);

            // 创建选择类控件（带小正方形）
            CheckBox checkBox = new CheckBox() { Content = "Auto", Margin = new Thickness(0, 0, 0, 10) };
            Grid.SetRow(checkBox, 2); // 设置在第二行
            Grid.SetColumn(checkBox, 0); // 设置在第一列
            controlGrid.Children.Add(checkBox);

            Grid radioControlGrid = new Grid();
            radioControlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            radioControlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            radioControlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            radioControlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            radioControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            radioControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            radioControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            radioControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            string[] optionContents = { "0°", "90°", "180°", "270°" };
            int row = 2; // 从第三行开始放置RadioButton
            int column = -1;

            foreach (var content in optionContents)
            {
                var radioButton = new RadioButton()
                {
                    Content = content,
                    Margin = new Thickness(0, 50, 0, 0) // 设置每个控件之间的间距
                };
                Grid.SetRow(radioButton, row);
                Grid.SetColumn(radioButton, column + 1); // 放在中间列
                radioControlGrid.Children.Add(radioButton);
                column++;
                if (column > 0)
                {
                    column = -1;
                    row++;
                }
            }

            Grid.SetRow(radioControlGrid, 3); // 设置在RadioButton的下一行
            Grid.SetColumn(radioControlGrid, 0); // 设置在第一列
            controlGrid.Children.Add(radioControlGrid);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);
            RightPanel.Children.Add(grid);
        }
        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Screen Rotate.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 图片设置在第二行
            Grid.SetColumn(image, 0); // 图片设置在中间列
            grid.Children.Add(image);
            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Reset",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 400, 80, 0)
            };
            Grid.SetRow(buttonReset, 1); // 设置在第四行
            Grid.SetColumn(buttonReset, 0); // 设置在中间列
            grid.Children.Add(buttonReset);

            RightPanel.Children.Add(grid);
        }
        private void Button_Click_Information(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock labe1 = new TextBlock()
            {
                Text = "Manufacturer",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe1, 0); // 设置在第三行
            Grid.SetColumn(labe1, 0); // 设置在中间列
            grid.Children.Add(labe1);
            TextBlock labe1_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 50, 0, 0),
            };
            Grid.SetRow(labe1_1, 0); // 设置在第三行
            Grid.SetColumn(labe1_1, 0); // 设置在中间列
            grid.Children.Add(labe1_1);

            TextBlock labe2 = new TextBlock()
            {
                Text = "Monitor Name",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe2, 1); // 设置在第三行
            Grid.SetColumn(labe2, 0); // 设置在中间列
            grid.Children.Add(labe2);
            TextBlock labe2_2 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe2_2, 1); // 设置在第三行
            Grid.SetColumn(labe2_2, 0); // 设置在中间列
            grid.Children.Add(labe2_2);

            TextBlock labe3 = new TextBlock()
            {
                Text = "Plug and Play ID",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe3, 2); // 设置在第三行
            Grid.SetColumn(labe3, 0); // 设置在中间列
            grid.Children.Add(labe3);
            TextBlock labe3_3 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe3_3, 2); // 设置在第三行
            Grid.SetColumn(labe3_3, 0); // 设置在中间列
            grid.Children.Add(labe3_3);

            TextBlock labe4 = new TextBlock()
            {
                Text = "Serial Number",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe4, 3); // 设置在第三行
            Grid.SetColumn(labe4, 0); // 设置在中间列
            grid.Children.Add(labe4);
            TextBlock labe4_4 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe4_4, 3); // 设置在第三行
            Grid.SetColumn(labe4_4, 0); // 设置在中间列
            grid.Children.Add(labe4_4);

            TextBlock labe5 = new TextBlock()
            {
                Text = "Manufacture Date",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe5, 4); // 设置在第三行
            Grid.SetColumn(labe5, 0); // 设置在中间列
            grid.Children.Add(labe5);
            TextBlock labe5_5 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe5_5, 4); // 设置在第三行
            Grid.SetColumn(labe5_5, 0); // 设置在中间列
            grid.Children.Add(labe5_5);

            TextBlock labe6 = new TextBlock()
            {
                Text = "EDID Revision",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe6, 5); // 设置在第三行
            Grid.SetColumn(labe6, 0); // 设置在中间列
            grid.Children.Add(labe6);
            TextBlock labe6_6 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe6_6, 5); // 设置在第三行
            Grid.SetColumn(labe6_6, 0); // 设置在中间列
            grid.Children.Add(labe6_6);

            TextBlock labe7 = new TextBlock()
            {
                Text = "Display Type and Signal",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe7, 6); // 设置在第三行
            Grid.SetColumn(labe7, 0); // 设置在中间列
            grid.Children.Add(labe7);
            TextBlock labe7_7 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe7_7, 6); // 设置在第三行
            Grid.SetColumn(labe7_7, 0); // 设置在中间列
            grid.Children.Add(labe7_7);

            TextBlock labe8 = new TextBlock()
            {
                Text = "DDCCI",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe8, 7); // 设置在第三行
            Grid.SetColumn(labe8, 0); // 设置在中间列
            grid.Children.Add(labe8);
            TextBlock labe8_8 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe8_8, 7); // 设置在第三行
            Grid.SetColumn(labe8_8, 0); // 设置在中间列
            grid.Children.Add(labe8_8);

            TextBlock labe9 = new TextBlock()
            {
                Text = "Native Resolution",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe9, 0); // 设置在第三行
            Grid.SetColumn(labe9, 1); // 设置在中间列
            grid.Children.Add(labe9);
            TextBlock labe9_9 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 50, 0, 0),
            };
            Grid.SetRow(labe9_9, 0); // 设置在第三行
            Grid.SetColumn(labe9_9, 1); // 设置在中间列
            grid.Children.Add(labe9_9);

            TextBlock labe10 = new TextBlock()
            {
                Text = "Screen Size",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe10, 1); // 设置在第三行
            Grid.SetColumn(labe10, 1); // 设置在中间列
            grid.Children.Add(labe10);
            TextBlock labe10_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe10_1, 1); // 设置在第三行
            Grid.SetColumn(labe10_1, 1); // 设置在中间列
            grid.Children.Add(labe10_1);

            TextBlock labe11 = new TextBlock()
            {
                Text = "Display Gamma",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe11, 2); // 设置在第三行
            Grid.SetColumn(labe11, 1); // 设置在中间列
            grid.Children.Add(labe11);
            TextBlock labe11_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe11_1, 2); // 设置在第三行
            Grid.SetColumn(labe11_1, 1); // 设置在中间列
            grid.Children.Add(labe11_1);

            TextBlock labe12 = new TextBlock()
            {
                Text = "Red Chromaticity",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe12, 3); // 设置在第三行
            Grid.SetColumn(labe12, 1); // 设置在中间列
            grid.Children.Add(labe12);
            TextBlock labe12_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe12_1, 3); // 设置在第三行
            Grid.SetColumn(labe12_1, 1); // 设置在中间列
            grid.Children.Add(labe12_1);

            TextBlock labe13 = new TextBlock()
            {
                Text = "Green Chromaticity",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe13, 4); // 设置在第三行
            Grid.SetColumn(labe13, 1); // 设置在中间列
            grid.Children.Add(labe13);
            TextBlock labe13_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe13_1, 4); // 设置在第三行
            Grid.SetColumn(labe13_1, 1); // 设置在中间列
            grid.Children.Add(labe13_1);

            TextBlock labe14 = new TextBlock()
            {
                Text = "Blue Chromaticity",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe14, 5); // 设置在第三行
            Grid.SetColumn(labe14, 1); // 设置在中间列
            grid.Children.Add(labe14);
            TextBlock labe14_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe14_1, 5); // 设置在第三行
            Grid.SetColumn(labe14_1, 1); // 设置在中间列
            grid.Children.Add(labe14_1);

            TextBlock labe15 = new TextBlock()
            {
                Text = "White Point",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe15, 6); // 设置在第三行
            Grid.SetColumn(labe15, 1); // 设置在中间列
            grid.Children.Add(labe15);
            TextBlock labe15_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe15_1, 6); // 设置在第三行
            Grid.SetColumn(labe15_1, 1); // 设置在中间列
            grid.Children.Add(labe15_1);

            TextBlock labe16 = new TextBlock()
            {
                Text = "SmartControl Version",
                Margin = new Thickness(0, 10, 0, 0),
            };
            Grid.SetRow(labe16, 7); // 设置在第三行
            Grid.SetColumn(labe16, 1); // 设置在中间列
            grid.Children.Add(labe16);
            TextBlock labe16_1 = new TextBlock()
            {
                Text = "11111",
                Margin = new Thickness(0, 30, 0, 0),
            };
            Grid.SetRow(labe16_1, 7); // 设置在第三行
            Grid.SetColumn(labe16_1, 1); // 设置在中间列
            grid.Children.Add(labe16_1);



            RightPanel.Children.Add(grid);
        }
        private void Button_Click_About(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            // 主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 用于直线
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            // 图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 用于直线
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/About.png")),
                Width = 75,
                Height = 75,
                Margin = new Thickness(0, 30, 480, 0)
            };
            Grid.SetRow(image, 0); // 设置在第一行
            Grid.SetColumn(image, 0); // 设置在第一列
            imageGrid.Children.Add(image);

            // 添加文本块
            TextBlock textBlock = new TextBlock()
            {
                Text = "Philips SmartControl",
                Margin = new Thickness(100, 35, 0, 0),
                FontSize = 20
            };
            Grid.SetRow(textBlock, 0); // 设置在第一行
            Grid.SetColumn(textBlock, 0); // 设置在第一列
            imageGrid.Children.Add(textBlock);

            TextBlock textBlock1 = new TextBlock()
            {
                Text = "Release: version 6.22.5",
                Margin = new Thickness(100, 65, 0, 0)
            };
            Grid.SetRow(textBlock1, 0); // 设置在第一行
            Grid.SetColumn(textBlock1, 0); // 设置在第一列
            imageGrid.Children.Add(textBlock1);

            // 添加直线
            Rectangle line = new Rectangle
            {
                Height = 1,
                Width = 500,
                Fill = new SolidColorBrush(Microsoft.UI.Colors.Gray),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 30, 0, 0)
            };
            Grid.SetRow(line, 1); // 设置在第二行
            Grid.SetColumn(line, 0); // 设置在第一列
            imageGrid.Children.Add(line);

            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image1 = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/1.png")),
                Width = 50,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 30, 0, 0)
            };
            Grid.SetRow(image1, 2); // 设置在第一行
            Grid.SetColumn(image1, 0); // 设置在第一列
            controlGrid.Children.Add(image1);

            TextBlock text1 = new TextBlock() { Text = "2024-5-15", Margin = new Thickness(80, 45, 0, 0) };
            Grid.SetRow(text1, 2); // 设置在第一行
            Grid.SetColumn(text1, 0); // 设置在第一列
            controlGrid.Children.Add(text1);

            // 添加图片
            Image image2 = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/2.png")),
                Width = 50,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 10, 0, 0)
            };
            Grid.SetRow(image2, 3); // 设置在第一行
            Grid.SetColumn(image2, 0); // 设置在第一列
            controlGrid.Children.Add(image2);
            TextBlock text2 = new TextBlock() { Text = "Go to www.philips.com/support.to download the latest version of Smartcontrol software",
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 320, // 设置宽度以触发换行
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(80, 25, 0, 0) };
            Grid.SetRow(text2, 3); // 设置在第一行
            Grid.SetColumn(text2, 0); // 设置在第一列
            controlGrid.Children.Add(text2);

            // 添加图片
            Image image3 = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/3.png")),
                Width = 70,
                Height = 70,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(430, 0, 0, 0)
            };
            Grid.SetRow(image3, 3); // 设置在第一行
            Grid.SetColumn(image3, 0); // 设置在第一列
            controlGrid.Children.Add(image3);

            

            TextBlock text3 = new TextBlock()
            {
                Text = "© MMD Monitors & Displays B.V. 2023. All rights are reserved. Reproduction or transmission in whole or in part, in any form or by any means, electronic, mechanical,or otherwise, is prohibited without the prior written consent of the copyright owner.Copyrights and allother proprietary rights in any software and related documentation(\"Software\") made available to you rest exclusively with Philips or its licensors. No titleof ownership of the Software is conferred to you. Use of the Software is subject to the end-user license conditions as are available on " +
                "request.\n To the maximum extent permitted by law, you shall not decompile and/or reverse engineer the software or any part thereof.",
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 500, // 设置宽度以触发换行
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 12,
                Margin = new Thickness(0, 60, 0, 0)
            };
            Grid.SetRow(text3, 4); // 设置在第一行
            Grid.SetColumn(text3, 0); // 设置在第一列
            controlGrid.Children.Add(text3);
            TextBlock text4 = new TextBlock()
            {
                Text = "Philips and the Philips Shield Emblem are registered trademarks of Koninklijke Philips N.V. and are used under license.",
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 500, // 设置宽度以触发换行
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 12,
                Margin = new Thickness(0, 20, 0, 0)
            };
            Grid.SetRow(text4, 5); // 设置在第一行
            Grid.SetColumn(text4, 0); // 设置在第一列
            controlGrid.Children.Add(text4);



            Grid.SetRow(imageGrid, 1); // 将 imageGrid 放在主 grid 的第二行
            Grid.SetRow(controlGrid, 2); // 将 controlGrid 放在主 grid 的第三行
            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);
            RightPanel.Children.Add(grid);
        }


        private void Button_Click_VU(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            //主布局
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 控件Grid
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Version Upgrade.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 设置在第一行
            Grid.SetColumn(image, 0); // 设置在第一列
            grid.Children.Add(image);

            TextBlock labe2 = new TextBlock()
            {
                Text = "To update the firmware, please connect a USB (USB-C/C, USB-C/A, or USB-A/B) upstream cable to your 27 M2C5500W device.",
                Margin = new Thickness(0, 0, 0, 0),
                FontSize = 12,
                TextWrapping = TextWrapping.Wrap, // 启用自动换行
                Width = 540 // 设置宽度以触发换行
            };
            Grid.SetRow(labe2, 1); // 设置在第三行
            Grid.SetColumn(labe2, 0); // 设置在中间列
            grid.Children.Add(labe2);
            RightPanel.Children.Add(grid);
        }
        private void Button_Click_Language(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();

            // 主布局
            Grid grid = new Grid();
            for (int i = 0; i < 17; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            // 图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock textBlock = new TextBlock()
            {
                Text = "Language",
                Margin = new Thickness(0, 50, 0, 0)
            };
            Grid.SetRow(textBlock, 0);
            Grid.SetColumn(textBlock, 0);
            imageGrid.Children.Add(textBlock);

            // 控件布局
            Grid controlGrid = new Grid();
            for (int i = 0; i < 17; i++)
            {
                controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            string[] optionContents = { "English", "Deutsch", "Espanol", "EAAnVlká", "Français", "Italiano", "Magyarország", "Nederlands",
        "Português", "Português brasileiro", "Polski", "русский язык", "Svenska", "Suomalainen", "Türkçe", "Čeština",
        "українська мова", "简体中文", "繁體中文", "日本语", "한국어"};

            int row = 1; // 从第二行开始放置RadioButton
            int column = 0;

            foreach (var content in optionContents)
            {
                var radioButton = new RadioButton()
                {
                    Content = content,
                    Margin = new Thickness(0, 0, 0, 0) // 设置每个控件之间的间距
                };
                Grid.SetRow(radioButton, row);
                Grid.SetColumn(radioButton, column); // 放在第一列
                controlGrid.Children.Add(radioButton);

                row++;
                if (row > 16)
                {
                    row = 1;
                    column++;
                }
            }

            Grid.SetRow(imageGrid, 0);
            Grid.SetColumn(imageGrid, 0);
            grid.Children.Add(imageGrid);

            Grid.SetRow(controlGrid, 1);
            Grid.SetColumn(controlGrid, 0);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);
        }

        private void Button_Click_RC(object sender, RoutedEventArgs e)
        {
            RightPanel.Children.Clear();
            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 图片行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 选择类控件行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto }); // 文本和组合框行
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间

            //图片布局
            Grid imageGrid = new Grid();
            imageGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            imageGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            // 添加图片
            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/Remote Control.png")),
                Width = 60,
                Height = 60,
                Margin = new Thickness(0, 50, 480, 0)
            };
            Grid.SetRow(image, 0); // 设置在第一行
            Grid.SetColumn(image, 0); // 设置在第一列
            imageGrid.Children.Add(image);

            //控件布局
            Grid controlGrid = new Grid();
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            controlGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // 占据剩余空间
            // 创建文本和组合框
            TextBlock textBlock = new TextBlock() { Text = "Server IP", Margin = new Thickness(0, 130, 0, 5) };
            Grid.SetRow(textBlock, 1); // 设置在第三行
            Grid.SetColumn(textBlock, 0); // 设置在第一列
            controlGrid.Children.Add(textBlock);

            TextBox textBox = new TextBox() { Width = 170, Margin = new Thickness(0, 0, 0, 0),HorizontalAlignment = HorizontalAlignment.Left  };
            Grid.SetRow(textBox, 2); // 设置在第四行
            Grid.SetColumn(textBox, 0); // 设置在第一列
            controlGrid.Children.Add(textBox);
            TextBox textBox1 = new TextBox() { Width = 150, Margin = new Thickness(0, 0, 0, 0),HorizontalAlignment = HorizontalAlignment.Left };
            Grid.SetRow(textBox1, 2); // 设置在第四行
            Grid.SetColumn(textBox1, 1); // 设置在第一列
            controlGrid.Children.Add(textBox1);

            // 创建并添加右下角按钮
            Button buttonReset = new Button()
            {
                Content = "Save",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 0)
            };
            Grid.SetRow(buttonReset, 2); // 设置在第四行
            Grid.SetColumn(buttonReset, 2); // 设置在中间列
            controlGrid.Children.Add(buttonReset);

            // 创建选择类控件（带小正方形）
            CheckBox checkBox = new CheckBox() { Content = "Remote Control On", Margin = new Thickness(0, 50, 0, 0) };
            Grid.SetRow(checkBox, 3); // 设置在第二行
            Grid.SetColumn(checkBox, 0); // 设置在第一列
            controlGrid.Children.Add(checkBox);

            grid.Children.Add(imageGrid);
            grid.Children.Add(controlGrid);

            RightPanel.Children.Add(grid);


        }






        private void CreateButtonsForMiddlePanel(string[] buttonContents)
        {
            MiddlePanel.Children.Clear();
            foreach (var content in buttonContents)
            {
                var button = new Button
                {
                    Content = content,
                    Width = 250, // 设置按钮宽度
                    Height = 70, // 设置按钮高度
                    Margin = new Thickness(5) // 设置按钮外边距
                };
                if (content == "Picture Format")
                {
                    button.Click += Button_Click_PF; // 为按钮添加点击事件处理
                }
                if (content == "Brightness")
                {
                    button.Click += Button_Click_Brightness; // 为按钮添加点击事件处理
                }
                if (content == "Contrast")
                {
                    button.Click += Button_Click_Contrast; // 为按钮添加点击事件处理
                }
                if (content == "Resolution")
                {
                    button.Click += Button_Click_Resolution; // 为按钮添加点击事件处理
                }
                if (content == "Input")
                {
                    button.Click += Button_Click_Input; // 为按钮添加点击事件处理
                }
                if (content == "PIP/PBP")
                {
                    button.Click += Button_Click_PP; // 为按钮添加点击事件处理
                }
                if (content == "Color Temperature")
                {
                    button.Click += Button_Click_CT; // 为按钮添加点击事件处理
                }
                if (content == "Display Gamma")
                {
                    button.Click += Button_Click_DG; // 为按钮添加点击事件处理
                }
                if (content == "Smart Image")
                {
                    button.Click += Button_Click_SI; // 为按钮添加点击事件处理
                }
                if (content == "Application Preset Mode")
                {
                    button.Click += Button_Click_APM; // 为按钮添加点击事件处理
                }
                if (content == "Audio")
                {
                    button.Click += Button_Click_Audio; // 为按钮添加点击事件处理
                }
                if (content == "Power Schedule")
                {
                    button.Click += Button_Click_PS; // 为按钮添加点击事件处理
                }
                if (content == "Power LED")
                {
                    button.Click += Button_Click_PLED; // 为按钮添加点击事件处理
                }
                if (content == "Smart Desktop")
                {
                    button.Click += Button_Click_SD; // 为按钮添加点击事件处理
                }
                if (content == "Multi Monitor")
                {
                    button.Click += Button_Click_MM; // 为按钮添加点击事件处理
                }
                if (content == "Screen Rotate")
                {
                    button.Click += Button_Click_SR; // 为按钮添加点击事件处理
                }
                if (content == "Reset")
                {
                    button.Click += Button_Click_Reset; // 为按钮添加点击事件处理
                }
                if (content == "Information")
                {
                    button.Click += Button_Click_Information; // 为按钮添加点击事件处理
                }
                if (content == "About")
                {
                    button.Click += Button_Click_About; // 为按钮添加点击事件处理
                }
                if (content == "Version Upgrade")
                {
                    button.Click += Button_Click_VU; // 为按钮添加点击事件处理
                }
                if (content == "Language")
                {
                    button.Click += Button_Click_Language; // 为按钮添加点击事件处理
                }
                if (content == "Remote Control")
                {
                    button.Click += Button_Click_RC; // 为按钮添加点击事件处理
                }
                MiddlePanel.Children.Add(button);
            }
        }
        private void CreateButtonsForRightPanel(string[] buttonContents)
        {
            RightPanel.Children.Clear();
            foreach (var content in buttonContents)
            {
                var button = new Button
                {
                    Content = content,
                    Width = 200, // 设置按钮宽度
                    Height = 30, // 设置按钮高度
                    Margin = new Thickness(0, 50, 0, 0) // 设置按钮外边距
                };
                RightPanel.Children.Add(button);
            }
        }
    }
}

        