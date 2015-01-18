using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SketchpadTool
{
    class Pencil : Tool
    {
        private Segment currentSegment;
        private bool isMouseMoving = false;
        private Point initPoint;

        public Pencil()
        {
            
        }

        public void MouseDown(Point p)
        {
            initPoint = p;

            currentSegment = new Segment();
            currentSegment.p1 = p;
            currentSegment.p2 = p;
            SketchpadApp.Instance.RawSketch.Segments.Add(currentSegment);
            SketchpadApp.Instance.RawSketch.segmentUpdated();

            isMouseMoving = true;
        }

        public void MouseMove(Point p)
        {
            //SketchpadApp.Instance.Diagnostics.Write("MouseMove");
            if (isMouseMoving)
            {
                if (distance(p, currentSegment.p1) > 10)
                {
                    currentSegment.p2 = p;
                    currentSegment = new Segment();
                    currentSegment.p1 = p;
                    currentSegment.p2 = p;
                    SketchpadApp.Instance.RawSketch.Segments.Add(currentSegment);
                    SketchpadApp.Instance.RawSketch.segmentUpdated();
                }
                else
                {
                    //currentSegment.p2 = p;
                }
            }
        }

        public void MouseUp(Point p)
        {
            SketchpadApp.Instance.Diagnostics.Write("MouseUp");

            if (isMouseMoving)
            {
                currentSegment.p2 = p;
                SketchpadApp.Instance.RawSketch.segmentUpdated();
                checkForShapes(p);
            }

            isMouseMoving = false;
        }

        public void MouseLeave(Point p)
        {
            if (isMouseMoving)
            {
                currentSegment.p2 = p;
                SketchpadApp.Instance.RawSketch.segmentUpdated();
                checkForShapes(p);
            }
            isMouseMoving = false;
        }

        private void checkForShapes(Point p)
        {
            if (distance(initPoint, p) < 20)
            {
                //closed movement == cycle == mayby rect
                //SketchpadApp.Instance.Diagnostics.Write("cycle");

                Point prevPoint = p;
                List<Segment> cycleSegments = SketchpadApp.Instance.RawSketch.Segments.SkipWhile(s => !s.p1.Equals(initPoint)).ToList<Segment>();

                //SketchpadApp.Instance.Diagnostics.Write(cycleSegments.Count.ToString());

                //Segment minX = cycleSegments.OrderBy<Segment>(s => s.p1.X).FirstOrDefault().toList();

                //items.Aggregate((c, d) => c.Score < d.Score ? c : d)
                //min x z p1
                //List<Point> points = cycleSegments.Select<Point>(cycleSegments, s => s.p1);

                List<Point> cyclePoints = cycleSegments.Select(s => s.p1).ToList<Point>();
                cyclePoints.Add(p);

                List<Point> orderedByX = cyclePoints.OrderBy(x => x.X).ToList<Point>();
                double minX = Math.Round(orderedByX.First<Point>().X);
                double maxX = Math.Round(orderedByX.Last<Point>().X);
                //SketchpadApp.Instance.Diagnostics.Write(minX.ToString());
                //SketchpadApp.Instance.Diagnostics.Write(maxX.ToString());
                
                List<Point> orderedByY = cyclePoints.OrderBy(x => x.Y).ToList<Point>();
                double minY = Math.Round(orderedByY.First<Point>().Y);
                double maxY = Math.Round(orderedByY.Last<Point>().Y);
                //SketchpadApp.Instance.Diagnostics.Write(minY.ToString());
                //SketchpadApp.Instance.Diagnostics.Write(maxY.ToString());
                
                double w = maxX - minX;
                double h = maxY - minY;

                //all cycle point boudary
                Rect bounds = new Rect(new Point(minX, minY), new Point(maxX, maxY));
                //SketchpadApp.Instance.Diagnostics.Write(bounds.ToString());

                //bool b = cyclePoints.Any(x => !bounds.Contains(x));
                //SketchpadApp.Instance.Diagnostics.Write(b.ToString());

                double f = 5;
                //smile rect inside bounds to make sure shape reminds rectangle
                Rect excludedBounds = new Rect(new Point(minX + h/f, minY + h/f), new Point(maxX - h/f, maxY - h/f));

                bool anyInsideExcludedBouds = cyclePoints.Any(x => excludedBounds.Contains(x));
                SketchpadApp.Instance.Diagnostics.Write("anyInsideExcludedBouds = " + anyInsideExcludedBouds.ToString());
                
                double ratio = w / h;
                bool correctRatio = ratio > 0.25 && ratio < 4;

                SketchpadApp.Instance.Diagnostics.Write("ratio = " + ratio.ToString());

                bool isRectangle = !anyInsideExcludedBouds && correctRatio && w > 30 && h > 30;

                SketchpadApp.Instance.Diagnostics.Write("isRectangle = " + isRectangle.ToString());

                if(isRectangle)
                {
                    //redraw rectangle:

                    Segment s1 = new Segment();
                    s1.p1 = new Point(minX, minY);
                    s1.p2 = new Point(maxX, minY);

                    Segment s2 = new Segment();
                    s2.p1 = new Point(maxX, minY);
                    s2.p2 = new Point(maxX, maxY);

                    Segment s3 = new Segment();
                    s3.p1 = new Point(maxX, maxY);
                    s3.p2 = new Point(minX, maxY);

                    Segment s4 = new Segment();
                    s4.p1 = new Point(minX, maxY);
                    s4.p2 = new Point(minX, minY);

                    int limit = 20;
                    List<Segment> ls1 = divideSegment(s1, limit);   //divided top edge
                    List<Segment> ls2 = divideSegment(s2, limit);
                    List<Segment> ls3 = divideSegment(s3, limit);
                    List<Segment> ls4 = divideSegment(s4, limit);

                    //remove drawn segments
                    SketchpadApp.Instance.RawSketch.Segments = SketchpadApp.Instance.RawSketch.Segments.TakeWhile(s => !s.p1.Equals(initPoint)).ToList<Segment>();

                    List<Segment> redrawnRectangle = new List<Segment>();
                    redrawnRectangle.AddRange(ls1);
                    redrawnRectangle.AddRange(ls2);
                    redrawnRectangle.AddRange(ls3);
                    redrawnRectangle.AddRange(ls4);

                    //shake it
                    Random random = new Random();
                    int range = 1;
                    for (int i = 0; i < redrawnRectangle.Count; i++ )
                    {
                        Point movedPoint = new Point();
                        movedPoint.X = redrawnRectangle[i].p2.X + random.Next(-range, range);
                        movedPoint.Y = redrawnRectangle[i].p2.Y + random.Next(-range, range);

                        redrawnRectangle[i].p2 = redrawnRectangle[(i + 1) % redrawnRectangle.Count].p1 = movedPoint;
                    }

                    SketchRectangle sketchRectangle = new SketchRectangle();
                    sketchRectangle.segments = redrawnRectangle;
                    sketchRectangle.bounds = bounds;

                    SketchpadApp.Instance.ShapesManager.registerRectangle(sketchRectangle);

                    SketchpadApp.Instance.RawSketch.Segments.AddRange(redrawnRectangle);
                    SketchpadApp.Instance.RawSketch.segmentUpdated();
                    

                    //List<Segment> cycleSegments = SketchpadApp.Instance.RawSketch.Segments.SkipWhile(s => !s.p1.Equals(initPoint)).ToList<Segment>();
                    
                }
                
                if(SketchpadApp.Instance.Diagnostics.debug && false)
                {
                    Line l1 = new Line();
                    l1.X1 = minX;
                    l1.Y1 = minY;
                    l1.X2 = maxX;
                    l1.Y2 = maxY;
                    l1.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0000"));

                    l1.HorizontalAlignment = HorizontalAlignment.Left;
                    l1.VerticalAlignment = VerticalAlignment.Center;
                    l1.StrokeThickness = 0.5;
                    SketchpadApp.Instance.window.rectangle1.Children.Add(l1);
                }
            }
            else if (distance(initPoint, p) > 40)
            {
                double sumAngle = calculateAngle(initPoint, p);

                List<Segment> freshSegments = SketchpadApp.Instance.RawSketch.Segments.SkipWhile(s => !s.p1.Equals(initPoint)).ToList<Segment>();

                bool isLine = !freshSegments.Any(s => angleDifference(sumAngle, calculateAngle(s.p1, s.p2)) > 40);

                SketchpadApp.Instance.Diagnostics.Write("isLine = " + isLine.ToString());

                if(isLine)
                {
                    Segment s1 = new Segment();
                    s1.p1 = initPoint;
                    s1.p2 = p;
                    int limit = 20;
                    List<Segment> l = divideSegment(s1, limit);

                    SketchpadApp.Instance.RawSketch.Segments = SketchpadApp.Instance.RawSketch.Segments.TakeWhile(s => !s.p1.Equals(initPoint)).ToList<Segment>();


                    SketchpadApp.Instance.RawSketch.Segments.AddRange(l);
                    SketchpadApp.Instance.RawSketch.segmentUpdated();
                }


            }
        }

        public void closeTool()
        {
            isMouseMoving = false;
        }

        //

        private List<Segment> divideSegment(Segment s, int limit)
        {
            List<Segment> result = new List<Segment>();
            if(distance(s.p1, s.p2) < limit)
            {
                result.Add(s);
            }
            else
            {
                Point midpoint = new Point((s.p1.X + s.p2.X) / 2, (s.p1.Y + s.p2.Y) / 2);

                Segment s1 = new Segment();
                s1.p1 = s.p1;
                s1.p2 = midpoint;
                result.AddRange(divideSegment(s1, limit));

                Segment s2 = new Segment();
                s2.p1 = midpoint;
                s2.p2 = s.p2;
                result.AddRange(divideSegment(s2, limit));
            }
            return result;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private double calculateAngle(Point p1, Point p2)
        {
            //calculate angle created by 2 points
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180 / Math.PI;
        }

        private double angleDifference(double a1, double a2)
        {
            double d = Math.Abs(a1 - a2) % 360;
            double result = d > 180 ? 360 - d : d;
            return result;
        }
    }
}
