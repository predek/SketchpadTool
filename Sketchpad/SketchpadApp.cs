using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchpadTool
{
    using System;

    public class SketchpadApp
    {
        public MainWindow window;

        private static SketchpadApp instance;
        private Diagnostics diagnostics;
        private Toolset toolset;
        private RawSketch rawSketch;
        private ShapesManager shapesManager;
        private StructsDetector structDetector;
        private FileManager fileManager;

        private SketchpadApp() { }

        public static SketchpadApp Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SketchpadApp();
                }
                return instance;
            }
        }

        public Diagnostics Diagnostics
        {
            get
            {
                if (diagnostics == null)
                    diagnostics = new Diagnostics(window);
                return diagnostics;
            }
        }

        public Toolset Toolset
        {
            get
            {
                if (toolset == null)
                    toolset = new Toolset(window);
                return toolset;
            }
        }

        public RawSketch RawSketch
        {
            get
            {
                if (rawSketch == null)
                    rawSketch = new RawSketch(window);
                return rawSketch;
            }
        }

        public ShapesManager ShapesManager
        {
            get
            {
                if (shapesManager == null)
                    shapesManager = new ShapesManager();
                return shapesManager;
            }
        }

        public StructsDetector StructDetector
        {
            get
            {
                if (structDetector == null)
                    structDetector = new StructsDetector();
                return structDetector;
            }
        }

        public FileManager FileManager
        {
            get
            {
                if (fileManager == null)
                    fileManager = new FileManager();
                return fileManager;
            }
        }
    }
}
