using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

namespace Rabbit {
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "Rabbit/GameDataSO", order = 0)]
    public class GameDataSO : ScriptableObject {

        public ContentData content;

    }
}