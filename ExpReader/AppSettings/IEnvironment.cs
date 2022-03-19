using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ExpReader.AppSettings
{
   public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
