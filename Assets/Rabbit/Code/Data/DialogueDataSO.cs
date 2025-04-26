using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEngine;
using Alchemy.Serialization;

namespace Rabbit {
    [CreateAssetMenu(fileName = "DialogueDataSO", menuName = "Rabbit/DialogueDataSO", order = 0)]
    public class DialogueDataSO : ScriptableObject {
        public List<DialoguePart> parts;
    }

    public class DialoguePart {
        public SpeakerType type;
        
        // public string speakerName;
        [TextAreaAttribute(minLines: 4, maxLines: 8)]
        public string text;
    }

    public enum SpeakerType {
        Player,
        Shadow
    }

    // public interface IDialogueDataAction {
    //     public DialogueDataType type { get; set; }
    // }
    //
    // [Serializable]
    // public class DialogueHeadAction : IDialogueDataAction {
    //     public DialogueDataType type { get; set; } = DialogueDataType.UpdateHead;
    //
    //     [HelpBox("Check the box and leave the sprite field empty to remove head")]
    //     public bool updateLeftSprite;
    //     public Sprite leftSprite;
    //     
    //     public bool updateRightSprite;
    //     public Sprite rightSprite;
    // }
    //
    // [Serializable]
    // public class DialogueTextAction : IDialogueDataAction {
    //     public DialogueDataType type { get; set; } = DialogueDataType.UpdateText;
    //
    //     public string speakerName;
    //     [TextAreaAttribute(minLines: 4, maxLines: 8)]
    //     public string text;
    // }
    //
    // public enum DialogueDataType {
    //     UpdateHead,
    //     UpdateText,
    // }
    
    
}