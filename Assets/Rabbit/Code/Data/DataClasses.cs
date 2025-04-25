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
    }

    [Serializable]
    public class CutsceneStateData : IActionStateData {
        public ActionStateTypes type { get; set; } = ActionStateTypes.Cutscene;
        [SerializeReference] public INarrativeData narrative;
    }

    public enum ActionStateTypes {
        SetupScene,
        Gameplay,
        Cutscene
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
    public class CutsceneData : INarrativeData {
        public NarrativeTypes type { get; set; } = NarrativeTypes.Cutscene;

        public int cutscene;
    }
    
    
    public enum NarrativeTypes {
        Comics,
        Dialogue,
        Cutscene
    }
    
    #endregion

    [Serializable]
    public class ContentData {
        public List<GameBlockDataSO> gameBlocks;
        public int currentBlockNum = 0;
    }
    
}