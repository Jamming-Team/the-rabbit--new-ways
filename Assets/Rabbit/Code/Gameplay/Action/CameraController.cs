using System;
using UnityEngine;

namespace Rabbit {
    public class CameraController : MonoBehaviour {
        [SerializeField] Camera _camera;
        [SerializeField] LayerMask _layerInteractive;

        private float rotationSpeed = 70f; 
        private float maxRotationAngle = 80f;
        private float currentRotation = 0f;

        void Awake() {
            GameEvents.Gameplay.OnGameplayUpdate += OnGameplayUpdate;
        }

        void OnDestroy() {
            GameEvents.Gameplay.OnGameplayUpdate -= OnGameplayUpdate;
        }

        void OnGameplayUpdate(float deltaTime) {
            RotateCamera(deltaTime);
            DetectClick();
        }

        private void RotateCamera(float deltaTime) {
            float rotationInput = 0f;

            
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rotationInput -= 1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rotationInput += 1f;
            }
            else return;

            
            float rotationAmount = rotationInput * rotationSpeed * deltaTime;
            float targetRotation = Mathf.Clamp(currentRotation + rotationAmount, -maxRotationAngle, maxRotationAngle);

            float realRotationAmount = targetRotation - currentRotation;
            transform.Rotate(0f, realRotationAmount, 0f);

            currentRotation = targetRotation;
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
