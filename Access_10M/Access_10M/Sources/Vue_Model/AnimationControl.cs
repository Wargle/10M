using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Access_10M.Sources.Vue_Model
{
    public class AnimationControl
    {
        private bool isExe;

        private List<FrameworkElement> targets = new List<FrameworkElement>();
        private List<Storyboard> shows = new List<Storyboard>(), hides = new List<Storyboard>();
        private int Count;

        public AnimationControl(bool isShow = false)
        {
            this.isExe = isShow;
        }

        public void AddAnimation(FrameworkElement target, Storyboard show, Storyboard hide)
        {
            if (target == null || show == null || hide == null)
                throw new NullReferenceException();

            targets.Add(target);
            hides.Add(hide);
            shows.Add(show);

            Count++;
        }

        public void Execute()
        {
            for (int i = 0; i < Count; i++)
                shows.ElementAt(i).Begin(targets.ElementAt(i));

            isExe = true;
        }

        public void Back()
        {
            for (int i = 0; i < Count; i++)
                hides.ElementAt(i).Begin(targets.ElementAt(i));

            isExe = false;
        }

        public bool IsExecuted { get { return isExe; } }

        public void Toggle()
        {
            if (isExe)
                Back();
            else
                Execute();
        }
    }
}
