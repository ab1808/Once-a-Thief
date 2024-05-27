using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class PlatformUIControls : MonoBehaviour
    {
        [FormerlySerializedAs("MobileUI")] public GameObject[] mobileUI;
        [FormerlySerializedAs("WindowsUI")] public GameObject[] windowsUI;


        private void Start()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
			foreach (var ui in mobileUI)
			{
				ui.SetActive(false);
			}
			foreach (var ui in windowsUI)
			{
				ui.SetActive(true);
			}
#elif UNITY_ANDROID || UNITY_IOS
			foreach (var ui in mobileUI)
			{
				ui.SetActive(true);
			}
			foreach (var ui in windowsUI)
			{
				ui.SetActive(false);
			}
#endif
        }
        
    }
}
