using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchpadTool
{
    public class ShapesDetector
    {
        public ShapesDetector()
        {

        }

        public void shapesUpdated()
        {
            //ShapesDetector notified about updated shapes in RawSketch
            SketchpadApp.Instance.Diagnostics.Write("shapesUpdated");
        }

        public void registerRectangle(List<Segment> rectangle)
        {
            throw new NotImplementedException();
        }
    }
}
