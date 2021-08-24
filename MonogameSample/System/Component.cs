using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    abstract class Component
    {
        public Entity Entity { get; set; }

        public bool IsActive { get; set; }

        public void Attach(Entity entity)
        {
            Entity = entity;
            IsActive = true;
        }

        public void Detach()
        {
            IsActive = false;
        }

    }
}
