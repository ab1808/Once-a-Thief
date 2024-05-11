using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class PlayerHUD : MonoBehaviour
    {
        private GameObject menu;
        private bool menuEnabled = false;

        [FormerlySerializedAs("MobileUI")] public GameObject mobileUI;
        [FormerlySerializedAs("WindowsUI")] public GameObject windowsUI;


        private void Start()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            menu = windowsUI.transform.Find("WeaponMenu").gameObject;
            mobileUI.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
        menu = MobileUI.transform.Find("WeaponMenu").gameObject;
        MobileUI.SetActive(true);
#endif
        }
        public void OnWeaponMenu()
        {
            menu.SetActive(!menuEnabled);
            menuEnabled = !menuEnabled;
        }
    }
}
