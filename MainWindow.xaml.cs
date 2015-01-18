using Microsoft.Win32;
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

namespace SketchpadTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            SketchpadApp.Instance.window = this;
        }

        private void rectangle1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SketchpadApp.Instance.Toolset.MouseDown(e.GetPosition(rectangle1));
        }

        private void rectangle1_MouseMove(object sender, MouseEventArgs e)
        {
            bool mousePressed = e.LeftButton == MouseButtonState.Pressed;

            if (mousePressed)
                SketchpadApp.Instance.Toolset.MouseMove(e.GetPosition(rectangle1));
        }

        private void rectangle1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SketchpadApp.Instance.Toolset.MouseUp(e.GetPosition(rectangle1));
        }

        private void rectangle1_MouseLeave(object sender, MouseEventArgs e)
        {
            bool mousePressed = e.LeftButton == MouseButtonState.Pressed;

            if (mousePressed)
                SketchpadApp.Instance.Toolset.MouseLeave(e.GetPosition(rectangle1));
        }

        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            SketchpadApp.Instance.Toolset.setCurrentTool(new Pencil());

            PencilButton.BorderThickness = new Thickness(4);
            EraserButton.BorderThickness = new Thickness(1);
        }

        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            SketchpadApp.Instance.Toolset.setCurrentTool(new Eraser());

            PencilButton.BorderThickness = new Thickness(1);
            EraserButton.BorderThickness = new Thickness(4);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            SketchpadApp.Instance.RawSketch.Segments = new List<Segment>();
            SketchpadApp.Instance.RawSketch.segmentUpdated();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML Files (.xml)|*.xml";
            openDialog.FilterIndex = 1;

            if (openDialog.ShowDialog().Value)
            {
                SketchpadApp.Instance.RawSketch.Segments = SketchpadApp.Instance.FileManager.Read(openDialog.FileName);
                SketchpadApp.Instance.RawSketch.segmentUpdated();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML Files (.xml)|*.xml";
            saveDialog.FilterIndex = 1;

            if (saveDialog.ShowDialog().Value)
            {
                List<Segment> content = SketchpadApp.Instance.RawSketch.Segments;
                SketchpadApp.Instance.FileManager.Write(saveDialog.FileName, content);
            }
        }
    }
}
