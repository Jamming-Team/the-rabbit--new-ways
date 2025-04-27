using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;
using AYellowpaper.SerializedCollections;

namespace Rabbit {
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "Rabbit/GameDataSO", order = 0)]
    public class GameDataSO : ScriptableObject {

        public ContentData content;
        public AudioData audio;

        public Dictionary<MusicBundleType, MusicBundle> bundles;


    }
    
    [Serializable]
    public class AudioData {
        public float musicVolume;
        public float sfxVolume;
        public MusicData music;
    }
    
    [Serializable]
    public class MusicData {
        // public List<AudioClip> audioClips;
        [SerializedDictionary("Type", "Bundle")]
        public SerializedDictionary<MusicBundleType, MusicBundle> bundles;
        public float crossFadeTime = 2.0f;
    }
    
    [Serializable]
    public class MusicBundle {
        public List<AudioClip> audioClips;
        public bool shouldLoopFirstClip = false;
    }
    
    public enum MusicBundleType { MainMenu, Gameplay }

}