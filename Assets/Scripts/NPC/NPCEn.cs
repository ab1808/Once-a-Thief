using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NPCE
{
    public abstract class NPCEn : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer interactIcon;

        private const float InteractDistance = 2f;

        private Transform playerTransform;

        private bool engage;
        private void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if(engage) Interact();

            if (interactIcon.gameObject.activeSelf && !IsWithinInteractDistance())
            {
                // hide the interact icon if the player is not within interact distance
                interactIcon.gameObject.SetActive(false);
                engage = false;
            }
            else if (!interactIcon.gameObject.activeSelf && IsWithinInteractDistance())
            {
                // show the interact icon if the player is within interact distance
                interactIcon.gameObject.SetActive(true);
                engage = true;
            }
        }

        public abstract void Interact();

        private bool IsWithinInteractDistance()
        {
            if (Vector2.Distance(playerTransform.position, transform.position) <= InteractDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
