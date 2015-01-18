using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchpadTool
{
    public class ShapesManager
    {
        List<SketchRectangle> sketchRectangles;

        public ShapesManager()
        {
            sketchRectangles = new List<SketchRectangle>();
        }

        public void shapesUpdated()
        {
            //ShapesDetector notified about updated shapes in RawSketch
            SketchpadApp.Instance.Diagnostics.Write("shapesUpdated");

            foreach(SketchRectangle sketchRectangle in sketchRectangles)
            {
                //sketchRectangle.segments.Any(s => SketchpadApp.Instance.RawSketch.Segments.Contain(s));
            }

        }

        public void registerRectangle(SketchRectangle rectangle)
        {
            sketchRectangles.Add(rectangle);
        }

        private void unregisterRectangle(SketchRectangle rectangle)
        {
            sketchRectangles.Remove(rectangle);
        }
    }
}
