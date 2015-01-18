using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SketchpadTool
{
    public class RawSketch
    {
        private List<Segment> segments;

        private MainWindow window;
        //private string sketchBackgroundColor = "#3C9DCF";
        private string sketchColor = "#0557CE";

        public RawSketch(MainWindow window)
        {
            this.window = window;
            //this.window.rectangle1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(sketchBackgroundColor));
            segments = new List<Segment>();
        }

        public List<Segment> Segments
        {
            get
            {
                return segments;
            }

            set
            {
                segments = value;
            }
        }

        public void segmentUpdated()
        {
            //SketchpadApp.Instance.Diagnostics.Write("segmentUpdated");
            //NotepadApp.Instance.Diagnostics.Write(segments.Count.ToString());
            SketchpadApp.Instance.ShapesManager.shapesUpdated();
            redrawSegments();
        }

        private void segmentFinished()
        {
            //SketchpadApp.Instance.ShapeDetector.shapesUpdated();
        }

        private void redrawSegments()
        {
            window.rectangle1.Children.Clear();
            
            for (int i = 0; i < SketchpadApp.Instance.RawSketch.Segments.Count; i++)
            {
                Segment segment = SketchpadApp.Instance.RawSketch.Segments[i];
                Line line = new Line();
                line.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(sketchColor));
                line.X1 = segment.p1.X;
                line.X2 = segment.p2.X;
                line.Y1 = segment.p1.Y;
                line.Y2 = segment.p2.Y;
                    
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.VerticalAlignment = VerticalAlignment.Center;
                line.StrokeThickness = 0.5;
                window.rectangle1.Children.Add(line);
             }
        }
    }
}
