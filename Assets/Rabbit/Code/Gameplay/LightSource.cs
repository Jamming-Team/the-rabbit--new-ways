using UnityEngine;

namespace Rabbit
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    public class LightSource : MonoBehaviour
    {
        [SerializeField] private Sprite _offSprite;
        [SerializeField] private Sprite _onSprite;

        private SpriteRenderer _spriteRenderer;

        public bool IsOn { get; private set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer not found!", this);
            }

            SetLightStatus(false);
        }

        public void TurnOn()
        {
            SetLightStatus(true);
        }

        public void TurnOff()
        {
            SetLightStatus(false);
        }

        private void SetLightStatus(bool isOn)
        {
            IsOn = isOn;
            _spriteRenderer.sprite = isOn ? _onSprite : _offSprite;
        }
    }
}