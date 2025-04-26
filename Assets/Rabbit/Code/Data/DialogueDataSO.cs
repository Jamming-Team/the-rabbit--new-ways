using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEngine;
using Alchemy.Serialization;

namespace Rabbit {
    [CreateAssetMenu(fileName = "DialogueDataSO", menuName = "Rabbit/DialogueDataSO", order = 0)]
    public class DialogueDataSO : ScriptableObject {
        [SerializeReference] public List<IDialogueDataAction> actions;
    }

    public interface IDialogueDataAction {
        public DialogueDataType type { get; set; }
    }

    [Serializable]
    public class DialogueHeadAction : IDialogueDataAction {
        public DialogueDataType type { get; set; } = DialogueDataType.UpdateHead;

        [HelpBox("Check the box and leave the sprite field empty to remove head")]
        public bool updateLeftSprite;
        public Sprite leftSprite;
        
        public bool updateRightSprite;
        public Sprite rightSprite;
    }
    
    [Serializable]
    public class DialogueTextAction : IDialogueDataAction {
        public DialogueDataType type { get; set; } = DialogueDataType.UpdateText;

        [TextAreaAttribute(minLines: 4, maxLines: 8)]
        public string text;
    }

    public enum DialogueDataType {
        UpdateHead,
        UpdateText,
    }
}