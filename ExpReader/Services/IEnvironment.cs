using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ExpReader.Services
{
   public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
