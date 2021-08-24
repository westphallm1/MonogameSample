using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{
    class Entity
    {
        public List<Component> Components;
        public Entity() 
        {
            Components = new List<Component>();
        }

        public Entity(params Component[] components) 
        {
            Components = new List<Component>(components);
            for(int i = 0; i < components.Length; i++)
            {
                components[i].Attach(this);
            }
        }

        public void Deactivate()
        {
            for(int i = 0; i < Components.Count; i++)
            {
                Components[i].Detach();
            }
        }

        public T GetComponent<T>() where T: Component
        {
            for(int i = 0; i < Components.Count; i++)
            {
                if(Components[i] is T component)
                {
                    return component;
                }
            }
            return default;
        }
    }
}
