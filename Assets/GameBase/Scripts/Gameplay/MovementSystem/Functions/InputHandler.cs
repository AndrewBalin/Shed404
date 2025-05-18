using System;
using Dialogs.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using Interactive;

namespace GameBase.Scripts.Gameplay.MovementSystem.Functions
{
    public class InputHandler
    {
        private Interactable _hovered;
        private LayerMask _groundMask;
        private LayerMask _interactableMask;

        public event Action<Vector3> MoveRequested;
        public event Action<Interactable> InteractRequested;
        public event Action<Interactable> Hover;
        public event Action<Interactable> Unhover;

        public InputHandler(LayerMask groundMask, LayerMask interactableMask)
        {
            _groundMask = groundMask;
            _interactableMask = interactableMask;
        }

        public void ProcessInput(Camera cam)
        {
            if (EventSystem.current.IsPointerOverGameObject() || DialogUI.instance != null && DialogUI.instance.IsOpen)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hitInt, 100f, _interactableMask))
                {
                    if (hitInt.collider.TryGetComponent(out Interactable interactable))
                    {
                        if (interactable != null)
                            InteractRequested?.Invoke(interactable);
                    }
                }
                else if (Physics.Raycast(ray, out var hitGr, 100f, _groundMask))
                {
                    MoveRequested?.Invoke(hitGr.point);
                }
            }
        }

        public void ProcessHover(Camera cam)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100f, _interactableMask))
            {
                if (hit.collider.TryGetComponent(out Interactable interactable))
                {
                    if (interactable != _hovered)
                    {
                        if (_hovered != null)
                            Unhover?.Invoke(_hovered);

                        _hovered = interactable;
                        Hover?.Invoke(interactable);
                    }
                }
            }
            else if (_hovered != null)
            {
                Unhover?.Invoke(_hovered);
                _hovered = null;
            }
        }
    }
}