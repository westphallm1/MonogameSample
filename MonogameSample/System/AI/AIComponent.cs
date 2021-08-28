using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System.AI
{
    abstract class AIComponent : Component
    {
        public override void AddToSystem()
        {
            AISystem.AIs.Add(this);
        }

        // do everything
        public abstract void Update();
    }
}
