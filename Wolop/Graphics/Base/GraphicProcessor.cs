using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolop.Graphics.Base
{
    class GraphicProcessor
    {
        private BackgroundWorker worker;
        public GraphicProcessor(FrameBuffer frameBuffer)
        {
            //Pass Delegate here?
             worker = new BackgroundWorker();
            worker.DoWork += (sender, e) =>
            {
                while (true)
                {
                    frameBuffer.Draw();
                }
            };
        }

        public void Run()
        {
            worker.RunWorkerAsync();
        }

        public void Stop()
        {
            worker.CancelAsync();
        }
    }
}
