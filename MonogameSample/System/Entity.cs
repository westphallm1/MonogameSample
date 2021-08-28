using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System
{

    // the "type" of the entity
    enum EntityTag
    {
        PLAYER,
        SCENERY
    }

    class Entity
    {
        public List<Component> Components;
        public EntityTag Tag;
        public Entity(EntityTag tag) 
        {
            Tag = tag;
            Components = new List<Component>();
        }

        public Entity(EntityTag tag, params Component[] components) 
        {
            Tag = tag;
            Components = new List<Component>(components);
            for(int i = 0; i < components.Length; i++)
            {
                components[i].Attach(this);
            }
            for(int i = 0; i < components.Length; i++)
            {
                components[i].PostAttach();
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
