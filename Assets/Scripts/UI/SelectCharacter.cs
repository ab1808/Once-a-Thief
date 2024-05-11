using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class SelectCharacter : MonoBehaviour
    {
        [FormerlySerializedAs("Player")] [SerializeField] GameObject player;
        [FormerlySerializedAs("Player_Green")] [SerializeField] GameObject playerGreen;
        [FormerlySerializedAs("Player_Red")] [SerializeField] GameObject playerRed;
    
        public void SelectDefaultPlayer()
        {
            player.SetActive(true);
            DontDestroyOnLoad(player);
            SceneManager.LoadScene("MainGame");
        }

        public void SelectGreenPlayer()
        {
            playerGreen.SetActive(true);
            DontDestroyOnLoad(playerGreen);
            SceneManager.LoadScene("MainGame");
        }

        public void SelectRedPlayer()
        {
            playerRed.SetActive(true);
            DontDestroyOnLoad(playerRed);
            SceneManager.LoadScene("MainGame");
        }
    }
}
