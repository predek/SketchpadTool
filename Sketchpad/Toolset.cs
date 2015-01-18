using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SketchpadTool
{
    public class Toolset
    {
        private MainWindow window;
        private Tool currentTool;

        private Tool defaultTool = new Pencil();

        public Toolset(MainWindow window)
        {
            this.window = window;
            this.currentTool = defaultTool;
            //NotepadApp.Instance.Diagnostics.Write("Toolset()");
        }

        public Tool getCurrentTool()
        {
            return currentTool;
        }

        public void setCurrentTool(Tool tool)
        {
            currentTool.closeTool();
            currentTool = tool;
        }

        public void MouseDown(Point p)
        {
            currentTool.MouseDown(p);
        }

        public void MouseMove(Point p)
        {
            currentTool.MouseMove(p);
        }

        public void MouseUp(Point p)
        {
            currentTool.MouseUp(p);
        }

        public void MouseLeave(Point p)
        {
            currentTool.MouseLeave(p);
        }
    }
}
