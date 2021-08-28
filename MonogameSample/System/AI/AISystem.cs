using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameSample.System.AI
{
    class AISystem
    {
        public static List<AIComponent> AIs = new List<AIComponent>();
        public static void Update()
        {
            for(int i = 0; i < AIs.Count; i++)
            {
                AIs[i].Update();
            }
        }
    }
}
