using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NPC
{
    public abstract class Npc : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer interactIcon;
        
        private const float InteractDistance = 2f;

        private Transform playerTransform;

        private void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if(Keyboard.current.eKey.wasPressedThisFrame)
            {
                // interact with the NPC {can be a shop, a character, etc.}
                Interact();
            }
            
            if (interactIcon.gameObject.activeSelf && !IsWithinInteractDistance())
            {
                // hide the interact icon if the player is not within interact distance
                interactIcon.gameObject.SetActive(false);
            }
            else if (!interactIcon.gameObject.activeSelf && IsWithinInteractDistance())
            {
                // show the interact icon if the player is within interact distance
                interactIcon.gameObject.SetActive(true);
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
