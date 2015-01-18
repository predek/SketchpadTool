using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace SketchpadTool
{
    public class Diagnostics
    {
        public bool debug = true;
        private MainWindow window;

        public Diagnostics(MainWindow window)
        {
            this.window = window;
        }

        public void Write(String s)
        {
            if (debug)
                Debug.WriteLine(s);
                //window.label1.Content = s; 
        }
    }
}
