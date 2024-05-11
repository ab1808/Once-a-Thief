using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
	public class PauseMenu : MonoBehaviour
	{
		public Canvas pauseMenuCanvas;

		private bool isPaused = false;
		private bool wasEscapePressed = false;
		private bool isPauseMenuCanvasNotNull;

		private void Start()
		{
			isPauseMenuCanvasNotNull = pauseMenuCanvas != null;
			// Ensure the pause menu canvas is initially disabled
			if (pauseMenuCanvas != null)
			{
				pauseMenuCanvas.enabled = false;
			}
			else
			{
				Debug.LogWarning("PauseMenuCanvas is not assigned!");
			}
		}

		private void Update()
		{
			bool isEscapePressed = Keyboard.current.escapeKey.isPressed || InputSystem.GetDevice<Keyboard>().escapeKey.isPressed;
		
			if (isEscapePressed && !wasEscapePressed)
			{
				TogglePause();
			}
			wasEscapePressed = isEscapePressed;
		}

		public void TogglePause()
		{
			// Toggle pause state
			isPaused = !isPaused;
		
			if (isPauseMenuCanvasNotNull)
			{
				pauseMenuCanvas.enabled = isPaused;
				Time.timeScale = isPaused ? 0f : 1f; // Freeze or resume time
			}
			else
			{
				Debug.LogWarning("PauseMenuCanvas is not assigned!");
			}
		}

 
    }
}