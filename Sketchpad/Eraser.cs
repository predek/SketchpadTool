
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SketchpadTool
{
    class Eraser : Tool
    {
        private bool isMouseMoving;
        private Point prevErasedPoint;

        public Eraser()
        {
            
        }

        public void MouseDown(Point p)
        {
            erasePoint(p);

            isMouseMoving = true;

            prevErasedPoint = p;
        }

        public void MouseMove(Point p)
        {
            if (isMouseMoving && distance(p, prevErasedPoint) > 10)
            {
                erasePoint(p);
                prevErasedPoint = p;
            }
        }

        public void MouseUp(Point p)
        {
            if (isMouseMoving)
            {
                erasePoint(p);
                prevErasedPoint = p;
                isMouseMoving = false;
            }
        }

        public void MouseLeave(Point p)
        {
            if (isMouseMoving)
            {
                erasePoint(p);
                prevErasedPoint = p;
                isMouseMoving = false;
            }
        }

        private void erasePoint(Point p)
        {
            
            for (int i = 0; i < SketchpadApp.Instance.RawSketch.Segments.Count; i++)
            {
                Segment segment = SketchpadApp.Instance.RawSketch.Segments[i];

                Point middle = new Point((segment.p1.X + segment.p2.X) / 2, (segment.p1.Y + segment.p2.Y) / 2);
                if (distance(middle, p) < 15)
                {
                    SketchpadApp.Instance.RawSketch.Segments.Remove(segment);
                    SketchpadApp.Instance.RawSketch.segmentUpdated();
                }
            }            
        }

        public void closeTool()
        {
            if (isMouseMoving)
            {
                isMouseMoving = false;
            }
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
