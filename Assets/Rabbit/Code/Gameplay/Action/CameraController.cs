using System;
using UnityEngine;

namespace Rabbit {
    public class CameraController : MonoBehaviour {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _layerInteractive;


        void Awake() {
            GameEvents.Gameplay.OnGameplayUpdate += OnGameplayUpdate;
        }

        void OnDestroy() {
            GameEvents.Gameplay.OnGameplayUpdate -= OnGameplayUpdate;
        }

        void OnGameplayUpdate(float obj) {
            DetectClick();
        }

        private void DetectClick() 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
                
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerInteractive)) {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactive"))
                    {
                        LightInteractor interactor = hit.collider.GetComponent<LightInteractor>();
                        if (interactor != null)
                        {
                            interactor.Interact();
                        }
                    }
                }
            }
        }
        
    }
}