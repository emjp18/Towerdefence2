using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towerdefence
{
    internal class Timer
    {
        private double m_currentTime = 0.0;
        public void ResetAndStart(double delay)
        {
            m_currentTime = delay;
        }
        public bool IsDone()
        {
            return m_currentTime <= 0.0;
        }
        public void Update(double deltaTime)
        {
            m_currentTime -= deltaTime;
        }
        public double GetTime() { return m_currentTime; }
    }
}
