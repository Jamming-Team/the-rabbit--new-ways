using System.Collections.Generic;
using UnityEngine;

namespace Rabbit
{
    [RequireComponent(typeof(BoxCollider))]
    public class LightSource : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _offSprite;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] Light _light;
        [SerializeField] Color[] _statesColors = new Color[2];
        
        [SerializeField] SoundData _onSound;
        [SerializeField] SoundData _offSound;

        [SerializeField] List<ShadowController> _affectedShadows;

        
        public bool IsOn { get; private set; }

        private void Awake()
        {
            // _spriteRenderer = GetComponent<SpriteRenderer>();
            //
            // if (_spriteRenderer == null)
            // {
            //     Debug.LogError("SpriteRenderer not found!", this);
            // }

            SetLightStatus(false);
        }

        public void TurnOn()
        {
            _affectedShadows.ForEach(x => {
                x.StopShadowGrowth();
                x.SetCanGrow(false);
            });
            SetLightStatus(true);
            
            AudioManager.Instance.PlaySound(_onSound, transform);
        }

        public void TurnOff()
        {
            SetLightStatus(false);
            
            _affectedShadows.ForEach(x => {
                // x.StopShadowGrowth();
                x.SetCanGrow(true);
            });
            
            AudioManager.Instance.PlaySound(_offSound, transform);
        }

        private void SetLightStatus(bool isOn)
        {
            IsOn = isOn;

            switch (isOn) {
                case true: {
                    _spriteRenderer.sprite = _onSprite;
                    _spriteRenderer.color = _statesColors[0];
                    _light.gameObject.SetActive(true);
                    break;
                }
                case false: {
                    _spriteRenderer.sprite = _offSprite;
                    _spriteRenderer.color = _statesColors[1];
                    _light.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}