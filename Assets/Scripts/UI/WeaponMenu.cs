using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class WeaponMenu : MonoBehaviour
    {
        [FormerlySerializedAs("Menu")] public GameObject menu;
        [FormerlySerializedAs("MobileUI")] public GameObject mobileUI;
        [FormerlySerializedAs("WindowsUI")] public GameObject windowsUI;
        private bool menuEnabled = false;

        private void Start()
        {
#if UNITY_STANDALONE
            windowsUI.SetActive(true);
#elif UNITY_ANDROID || UNITY_IOS
        MobileUI.SetActive(true);
#endif
        }


    }
}
