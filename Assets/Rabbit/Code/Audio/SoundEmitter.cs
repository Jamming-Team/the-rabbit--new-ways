using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rabbit {
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour {
        public SoundData Data { get; private set; }
        public LinkedListNode<SoundEmitter> Node { get; set; }

        AudioSource audioSource;
        Coroutine playingCoroutine;
        SoundManager _manager;

        void Awake() {
            audioSource = gameObject.GetOrAdd<AudioSource>();
        }

        public void Initialize(SoundData data, SoundManager manager) {
            _manager = manager;
            
            Data = data;
            // audioSource.clip = data.clip;
            audioSource.outputAudioMixerGroup = data.mixerGroup;
            audioSource.playOnAwake = data.playOnAwake;
            
            audioSource.mute = data.mute;
            audioSource.bypassEffects = data.bypassEffects;
            audioSource.bypassListenerEffects = data.bypassListenerEffects;
            audioSource.bypassReverbZones = data.bypassReverbZones;
            
            audioSource.priority = data.priority;
            audioSource.volume = data.volume;
            audioSource.pitch = data.pitch;
            audioSource.panStereo = data.panStereo;
            audioSource.spatialBlend = data.spatialBlend;
            audioSource.reverbZoneMix = data.reverbZoneMix;
            audioSource.dopplerLevel = data.dopplerLevel;
            audioSource.spread = data.spread;
            
            audioSource.minDistance = data.minDistance;
            audioSource.maxDistance = data.maxDistance;
            
            audioSource.ignoreListenerVolume = data.ignoreListenerVolume;
            audioSource.ignoreListenerPause = data.ignoreListenerPause;
            
            audioSource.rolloffMode = data.rolloffMode;

            audioSource.loop = data.shouldPlayOnLoop;
        }

        public void Play() {
            if (playingCoroutine != null) {
                StopCoroutine(playingCoroutine);
            }

            if (Data.clips.Count > 0)
            {
                audioSource.clip = Data.clips[Random.Range(0, Data.clips.Count)];
            }
            audioSource.Play();
            playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        IEnumerator WaitForSoundToEnd() {
            yield return new WaitWhile(() => audioSource.isPlaying);
            Stop();
        }

        public void Stop() {
            if (playingCoroutine != null) {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }
            
            audioSource.Stop();
            _manager.ReturnToPool(this);
        }

        public void WithRandomPitch(float min = -0.1f, float max = 0.1f) {
            audioSource.pitch += Random.Range(min, max);
        }
    }
}