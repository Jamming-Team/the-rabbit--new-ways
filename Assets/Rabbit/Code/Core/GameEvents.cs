using System;
using UnityEngine;

namespace Rabbit {
    public static class GameEvents {

        public static class UI {
            public static Action<GC.UI.ButtonTypes> OnButtonPressed;
            



        }

        public static class Narrative {
            public static Action<INarrativeData> OnStartNarrative;
            public static Action OnEndNarrative;
        }
        
    }
}