using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

namespace Rabbit {
    
    [CreateAssetMenu(fileName = "GameBlockSO", menuName = "Rabbit/GameBlockSO", order = 0)]
    public class GameBlockDataSO : ScriptableObject {
        [SerializeReference] public List<IActionStateData> states;
    }
    

}