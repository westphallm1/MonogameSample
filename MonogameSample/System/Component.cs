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
            AddToSystem();
        }

        public void Detach()
        {
            IsActive = false;
        }

        public virtual void PostAttach()
        {
            // no-op, runs after Entity is fully configured
        }

        public virtual void AddToSystem()
        {
            // no-op, might want to be moved externally
        }

    }
}
