using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class WeaponSelection : MonoBehaviour
    {
        [FormerlySerializedAs("Weapons")] [SerializeField] public List<GameObject> weapons;
        [HideInInspector] public GameObject currentWeapon;
        [HideInInspector] public GameObject newWeapon;
        private GameObject player;
        private Transform weaponPose;
        Transform childFound = null;


        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            weaponPose = CustomFindChild("WeaponPose", player.transform);
            for (int i = 0; i < weaponPose.childCount; i++)
            {
                weapons.Add(weaponPose.GetChild(i).gameObject);
            }
        }
        void Start()
        {
            currentWeapon = weapons[0];
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateWeapon(string weaponName)
        {
            for(int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].name == weaponName)
                {
                    //set new weapon
                    newWeapon = weapons[i];

                    //disable old weapon and enable new weapon
                    currentWeapon.SetActive(false);
                    newWeapon.SetActive(true);

                    //set current weapon to variable
                    currentWeapon = newWeapon;                
                }
            }
        }

        Transform CustomFindChild(string key, Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child.name == key)
                {
                    childFound = child;
                }
                else
                {
                    if (child.childCount > 0)
                    {
                        if (childFound == null)
                        {
                            CustomFindChild(key, child);
                        }
                    }
                }
            }

            return childFound;
        }
    }
}
