using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfApp13
{
    public class GravityEase : EasingFunctionBase
    {
        protected override Freezable CreateInstanceCore()
        {
            return new GravityEase();
        }

        protected override double EaseInCore(double normalizedTime)
        {
            var g = 9.8;
            return 0.5 * g * normalizedTime * normalizedTime;
        }
    }
}
