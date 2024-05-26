using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI UIPrompt;

    public GameObject player;

    public List<string> prompts;

    private int currentPrompt = 0;

    public GameObject buttonNextScene;

    public GameObject enemy;

    private float timer;

    void Start()
    {
        UIPrompt.text = prompts[0];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 4f && currentPrompt == 0)
        {
            currentPrompt++;
            UIPrompt.text = prompts[currentPrompt];
        }

        else if(player.GetComponent<Controller>().jumpPressed && currentPrompt == 1)
        {
            currentPrompt++;
            UIPrompt.text = prompts[currentPrompt];
        }

        else if(player.GetComponent<Controller>().sprintPressed && currentPrompt == 2)
        {
            currentPrompt++;
            UIPrompt.text = prompts[currentPrompt];
        }

        else if (enemy == null && currentPrompt == 3)
        {
            currentPrompt++;
            UIPrompt.text = prompts[currentPrompt];
        }

        else if(currentPrompt == 4)
        {
            buttonNextScene.SetActive(true);
            
        }

    }

    
}
