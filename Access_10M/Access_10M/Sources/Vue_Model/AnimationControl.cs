using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Access_10M.Sources.Vue_Model
{
    /// <summary>
    /// Classe qui permet d'englober l'utilisation de StoryBoard dans des vues.
    /// </summary>
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

        /// <summary>
        /// Ajoute une Animation à executer.
        /// </summary>
        /// <param name="target">Le Control ciblé par l'animation</param>
        /// <param name="show">Le StoryBoard d'éxécution</param>
        /// <param name="hide">Le StoryBoard d'inversement</param>
        public void AddAnimation(FrameworkElement target, Storyboard show, Storyboard hide)
        {
            if (target == null || show == null || hide == null)
                throw new NullReferenceException();

            targets.Add(target);
            hides.Add(hide);
            shows.Add(show);

            Count++;
        }

        /// <summary>
        /// Execute toutes les animations ajoutées.
        /// </summary>
        public void Execute()
        {
            for (int i = 0; i < Count; i++)
                shows.ElementAt(i).Begin(targets.ElementAt(i));

            isExe = true;
        }

        /// <summary>
        /// Inverse toutes les animations ajoutées.
        /// </summary>
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
