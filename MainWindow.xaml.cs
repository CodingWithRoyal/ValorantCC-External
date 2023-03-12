using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace ValorantCC_Custom_WPF
{
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifyIcon;
        private System.Windows.Media.SolidColorBrush _crosshairColor;

        private static string[] crosshairs = new string[] { "Circle", "Plus" };
        private static string[] colors = new string[] { "White", "Black", "Red", "Cyan", "Lime", "Navy", "Orange", "Tomato", "Orchid", "Magenta" };

        // Read user settings or load default values
        private string _crosshair = Properties.Settings.Default.user_crosshair; // cricle / plus
        private int _crosshairSize = Properties.Settings.Default.user_crosshairSize;
        private int _crosshairThickness = Properties.Settings.Default.user_crosshairThickness;
        private string _crosshairColorString = Properties.Settings.Default.user_crosshairColor;

        public MainWindow()
        {
            InitializeComponent();

            // Set the window properties
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            ShowInTaskbar = false;
            AllowsTransparency = true;
            Background = System.Windows.Media.Brushes.Transparent;
            Topmost = true;

            // Create the notify icon and add it to the system tray
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new System.Drawing.Icon("assets/appicon.ico");
            _notifyIcon.Visible = true;
            // Create the context menu for the notify icon
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem typeMenuItem = new ToolStripMenuItem("Crosshair Type");
            contextMenuStrip.Items.Add(typeMenuItem);
            for (int i = 0; i < crosshairs.Length; i++)
            {
                ToolStripMenuItem typeItem = new ToolStripMenuItem(crosshairs[i]);
                typeItem.Checked = crosshairs[i] == _crosshair;
                typeItem.Click += TypeItem_Click;
                typeMenuItem.DropDownItems.Add(typeItem);
            }

            ToolStripMenuItem thicknessMenuItem = new ToolStripMenuItem("Crosshair Thickness");
            contextMenuStrip.Items.Add(thicknessMenuItem);
            for (int i = 1; i <= 10; i += 1)
            {
                ToolStripMenuItem thicknessItem = new ToolStripMenuItem(i.ToString() + "px");
                thicknessItem.Checked = i == _crosshairSize;
                thicknessItem.Click += ThicknessItem_Click;
                thicknessMenuItem.DropDownItems.Add(thicknessItem);
            }

            ToolStripMenuItem sizeMenuItem = new ToolStripMenuItem("Crosshair Size");
            contextMenuStrip.Items.Add(sizeMenuItem);
            for (int i = 4; i <= 40; i += 4)
            {
                ToolStripMenuItem sizeItem = new ToolStripMenuItem(i.ToString() + "px");
                sizeItem.Checked = i == _crosshairSize;
                sizeItem.Click += SizeItem_Click;
                sizeMenuItem.DropDownItems.Add(sizeItem);
            }

            ToolStripMenuItem colorMenuItem = new ToolStripMenuItem("Crosshair Color");
            contextMenuStrip.Items.Add(colorMenuItem);
            for (int i = 0; i < colors.Length; i++)
            {
                ToolStripMenuItem colorItem = new ToolStripMenuItem(colors[i]);
                colorItem.Checked = colors[i] == _crosshairColorString;
                colorItem.Click += ColorItem_Click;
                colorMenuItem.DropDownItems.Add(colorItem);
            }

            // EXIT
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;
            contextMenuStrip.Items.Add(exitMenuItem);

            _notifyIcon.ContextMenuStrip = contextMenuStrip;

            // Draw the crosshair
            DrawCrosshair();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Close();
        }

        private void SizeItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in clickedItem.Owner.Items)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
            _crosshairSize = int.Parse(clickedItem.Text.Replace("px", ""));
            
            // Save user settings
            Properties.Settings.Default.user_crosshairSize = _crosshairSize;
            Properties.Settings.Default.Save();

            Debug.WriteLine("Crosshair Size Changed:" + clickedItem.Text);
            DrawCrosshair();
        }

        private void ThicknessItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in clickedItem.Owner.Items)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
            _crosshairThickness = int.Parse(clickedItem.Text.Replace("px", ""));

            // Save user settings
            Properties.Settings.Default.user_crosshairThickness = _crosshairThickness;
            Properties.Settings.Default.Save();

            Debug.WriteLine("Crosshair Thickness Changed:" + clickedItem.Text);
            DrawCrosshair();
        }

        private void TypeItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in clickedItem.Owner.Items)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
            _crosshair = clickedItem.Text;

            // Save user settings
            Properties.Settings.Default.user_crosshair = _crosshair;
            Properties.Settings.Default.Save();

            Debug.WriteLine("Crosshair Type Changed:" + clickedItem.Text);
            DrawCrosshair();
        }

        private void updateCrosshairColor() {
            switch (_crosshairColorString)
            {
                case "Red":
                    _crosshairColor = System.Windows.Media.Brushes.Red;
                    break;
                case "Navy":
                    _crosshairColor = System.Windows.Media.Brushes.Navy;
                    break;
                case "Orange":
                    _crosshairColor = System.Windows.Media.Brushes.Orange;
                    break;
                case "Tomato":
                    _crosshairColor = System.Windows.Media.Brushes.OrangeRed;
                    break;
                case "Lime":
                    _crosshairColor = System.Windows.Media.Brushes.Lime;
                    break;
                case "Orchid":
                    _crosshairColor = System.Windows.Media.Brushes.Orchid;
                    break;
                case "Magenta":
                    _crosshairColor = System.Windows.Media.Brushes.Magenta;
                    break;
                case "White":
                    _crosshairColor = System.Windows.Media.Brushes.White;
                    break;
                case "Black":
                    _crosshairColor = System.Windows.Media.Brushes.Black;
                    break;
                case "Cyan":
                    _crosshairColor = System.Windows.Media.Brushes.Cyan;
                    break;
            }
        }

        private void ColorItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in clickedItem.Owner.Items)
            {
                item.Checked = false;
            }
            clickedItem.Checked = true;
            _crosshairColorString = clickedItem.Text;

            // Save user settings
            Properties.Settings.Default.user_crosshairColor = _crosshairColorString;
            Properties.Settings.Default.Save();

            Debug.WriteLine("Crosshair Color Changed:" + clickedItem.Text);

            DrawCrosshair();
        }

        private void DrawCrosshair() {
            Debug.WriteLine("Crosshair Type:" + _crosshair);
            Debug.WriteLine("Crosshair Size:" + _crosshairSize);
            
            updateCrosshairColor(); // update brush color
            switch (_crosshair) {
                case "Circle":
                    DrawCircle();
                    break;
                case "Plus":
                    DrawPlus();
                    break;
            }
        }

        private void DrawCircle()
        {
            Debug.WriteLine("Drawing Circle");
            canvas.Children.Clear();
            Ellipse circle = new Ellipse();
            circle.Stroke = _crosshairColor;
            circle.StrokeThickness = _crosshairThickness;
            circle.Width = _crosshairSize;
            circle.Height = _crosshairSize;
            double left = (SystemParameters.PrimaryScreenWidth - _crosshairSize) / 2;
            double top = (SystemParameters.PrimaryScreenHeight - _crosshairSize) / 2;
            Canvas.SetLeft(circle, left);
            Canvas.SetTop(circle, top);
            canvas.Children.Add(circle);

            // Set the window position to center the circle on the monitor
            Left = left - (ActualWidth - circle.Width) / 2;
            Top = top - (ActualHeight - circle.Height) / 2;
        }

        private void DrawPlus()
        {
            Debug.WriteLine("Drawing Plus");
            canvas.Children.Clear();
            double thickness = _crosshairThickness; // Math.Max(_crosshairSize / 10, 1);
            double halfWidth = _crosshairSize / 2;
            double halfThickness = thickness / 2;
            Rect verticalRect = new Rect(halfWidth - halfThickness, 0, thickness, _crosshairSize);
            Rect horizontalRect = new Rect(0, halfWidth - halfThickness, _crosshairSize, thickness);
            System.Windows.Media.PathGeometry geometry = new System.Windows.Media.PathGeometry();
            geometry.AddGeometry(new System.Windows.Media.RectangleGeometry(verticalRect));
            geometry.AddGeometry(new System.Windows.Media.RectangleGeometry(horizontalRect));
            Path path = new Path();
            path.Stroke = _crosshairColor;
            path.StrokeThickness = thickness;
            path.Data = geometry;

            // Set the window position to center the plus sign on the monitor
            double left = (SystemParameters.PrimaryScreenWidth - _crosshairSize) / 2;
            double top = (SystemParameters.PrimaryScreenHeight - _crosshairSize) / 2;
            Canvas.SetLeft(path, left);
            Canvas.SetTop(path, top);
            canvas.Children.Add(path);
            Left = left - (ActualWidth - _crosshairSize) / 2;
            Top = top - (ActualHeight - _crosshairSize) / 2;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawCrosshair();
        }
    }
}