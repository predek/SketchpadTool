using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SketchpadTool
{
    public class Segment
    {
        public Point p1;
        public Point p2;

        public Segment()
        {

        }

        //public bool IsNode()
        //{
        //    if (!isNodeDetermined) //determine if is node
        //    {
        //        isNodeDetermined = true;

        //        //calculate center:
        //        double sumX = 0;
        //        double sumY = 0;
        //        for (int i = 0; i < points.Count - 1; i++)
        //        {
        //            sumX += points[i].X;
        //            sumY += points[i].Y;
        //        }

        //        Point segmentCenter = new Point(sumX / points.Count, sumY / points.Count);
        //        //NotepadApp.Instance.Diagnostics.Write("isNode()");
        //        //NotepadApp.Instance.Diagnostics.Write("segmentCenter = " + segmentCenter.ToString());

        //        List<double> angles = new List<double>();
                
        //        for (int i = 0; i < points.Count; i++)
        //        {
        //            double angle = calculateAngle(segmentCenter, points[i]);
        //            angles.Add(angle);
        //            SketchpadApp.Instance.Diagnostics.Write(angle.ToString());
        //        }

        //        List<double> ds = new List<double>();
        //        for (int i = 0; i < angles.Count; i++)
        //        {
        //            double d = angles[(i + 1)%angles.Count] - angles[i];
        //            ds.Add(d);
        //            SketchpadApp.Instance.Diagnostics.Write(d.ToString());
        //        }

        //        //NotepadApp.Instance.Diagnostics.Write("--");

        //        //NotepadApp.Instance.Diagnostics.Write(Math.Sign(ds[0]).ToString());

        //        int numDifferences = 0;
        //        for (int i = 0; i < ds.Count; i++)
        //        {
        //            //NotepadApp.Instance.Diagnostics.Write(Math.Sign(ds[i]).ToString());

        //            if (Math.Sign(ds[(i + 1)%ds.Count]) != Math.Sign(ds[i]))
        //                numDifferences++;
        //        }

        //        SketchpadApp.Instance.Diagnostics.Write("numDifferences = " + numDifferences.ToString() + "/" + ds.Count);

        //        isNode = numDifferences < 2; //todo
        //    }

        //    return isNode;
        //}

        //public bool isLine()
        //{
        //    return false;  
        //}

        //private double calculateAngle(Point p1, Point p2)
        //{
        //    return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180 / Math.PI;
        //}
    }
}
