using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace Rabbit {
    public class AudioManager : Singleton<AudioManager>, IVisitable {
        const string MUSIC_VOLUME_NAME = "MusicVolume";
        const string SFX_VOLUME_NAME = "SfxVolume";
        
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] AudioSource[] _musicSources = new AudioSource[2];
        [SerializeField] SoundData _testSound;
        [SerializeField] SoundManager _soundModel;


        [HideInInspector]
        public AudioData data { get; set; }
        
        MusicManager _music;

        bool _initialized = false;
        
        
        void Start() {
            GameManager.Instance.RequestData(this);

            AdjustMixerVolume();
            
            _music = new MusicManager(data.music, new MusicManager.MusicSourcesPair
            {
                sourceOne = _musicSources[0],
                sourceTwo = _musicSources[1]
            });


            GameEvents.Data.OnDataChanged += AdjustMixerVolume;

            _initialized = true;
            
            PlayMusic(MusicBundleType.MainMenu, true);


        }

        void OnDestroy() {
            GameEvents.Data.OnDataChanged -= AdjustMixerVolume;
        }

        void Update() {
            if (!_initialized)
                return;
            
            _music.CheckForCrossFade();

            // if (Input.GetKeyDown(KeyCode.Space)) {
            //     PlaySound(_testSound);
            // }
        }

        public SoundEmitter PlaySound(SoundData soundData, Transform playTransform = null) {
            if (!_soundModel.initialized)
                return null;
            
            var a = _soundModel.CreateSoundBuilder()
                .WithRandomPitch();
            if (playTransform != null) {
                a.WithPosition(playTransform.position);
            } 
            return a.Play(soundData);
        }
        
        void AdjustMixerVolume() {
            _mixer.SetFloat(SFX_VOLUME_NAME, data.sfxVolume.ToLogarithmicVolume());
            _mixer.SetFloat(MUSIC_VOLUME_NAME, data.musicVolume.ToLogarithmicVolume());
        }
        

        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }

        public void PlayMusic(MusicBundleType type, bool skipCheck = false) {
            if (!_initialized)
                return;
            
            var flag = _music.LoadBundle(type);
            
            
            if (!skipCheck && flag)
                return;
            
            _music.PlayNextTrack();
        }


    }
}