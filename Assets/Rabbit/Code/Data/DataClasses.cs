using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

namespace Rabbit {
    
    #region ActionStates
    
    public interface IActionStateData {
        public ActionStateTypes type { get; set; }
    }

    [Serializable]
    public class LevelSetupStateData : IActionStateData {
        public ActionStateTypes type { get; set; } = ActionStateTypes.SetupScene;

        public GameObject levelPrefab;
    }

    [Serializable]
    public class GameplayStateData : IActionStateData {
        public ActionStateTypes type { get; set; } = ActionStateTypes.Gameplay;

        public float timeTillNextAction;
    }

    [Serializable]
    public class NarrativeStateData : IActionStateData {
        public ActionStateTypes type { get; set; } = ActionStateTypes.Narrative;
        [SerializeReference] public INarrativeData narrative;
    }
    
    [Serializable]
    public class DoStuffStateData : IActionStateData {
        public ActionStateTypes type { get; set; } = ActionStateTypes.DoStuff;
        public DoStuffTypes stuffType;
    }

    public enum ActionStateTypes {
        SetupScene,
        Gameplay,
        Narrative,
        DoStuff
    }
    
    public enum DoStuffTypes {
        UpDifficulty
    }
    
    #endregion
    
    #region NarrativeRefs
    
    public interface INarrativeData {
        public NarrativeTypes type { get; set; }
    }
    
    [Serializable]
    public class ComicsData : INarrativeData {
        public NarrativeTypes type { get; set; } = NarrativeTypes.Comics;

        public GameObject comicsPrefab;
    }
    
    [Serializable]
    public class UIAnimationData : INarrativeData {
        public NarrativeTypes type { get; set; } = NarrativeTypes.UIAnimation;

        public string animationTriggerName;
    }
    
    [Serializable]
    public class OtherNarrativeData : INarrativeData {
        public NarrativeTypes type { get; set; } = NarrativeTypes.Other;

        public OtherNarrativeTypes otherType;
    }
    
    [Serializable]
    public class DialogueNarrativeData : INarrativeData {
        public NarrativeTypes type { get; set; } = NarrativeTypes.Dialogue;

        public DialogueDataSO dialogueData;
    }

    public enum OtherNarrativeTypes {
        HideNarrativeViews,
    }
    
    
    public enum NarrativeTypes {
        Comics,
        Dialogue,
        UIAnimation,
        Other
    }
    
    #endregion

    [Serializable]
    public class ContentData {
        public List<GameBlockDataSO> gameBlocks;
        [SerializeReference] public List<IActionStateData> defeatBlock;
        public int currentBlockNum = 0;

        public List<DifficultySet> difficulties;

        // public float timeTillEnd = 300;
        //
        // public class TimeConvertedData {
        //     public float hours;
        //     public float minutes;
        // }



    }

    [Serializable]
    public class DifficultySet {
        public float timeTillNextShadow = 3f;
        public int maxShadows = 2;
    }
    
}