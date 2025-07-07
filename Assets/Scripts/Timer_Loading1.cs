using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer_Loading1 : MonoBehaviour
{
    public float timeValue = 3f;
    public TextMeshProUGUI timerText;
    string scene;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timeValue > 0) {
            timeValue -= Time.deltaTime;
        } else {
            scene = "Level 1";
            SceneManager.LoadScene(scene);
        }
        DisplayText();
    }

    void DisplayText ()
    {
        if (timeValue <= 3)
        {
            timerText.text = "ì̷̧̩̙̱̘̯̻͔̹̝͙̂̅͑̐̿̃͌̀̏̒̒͆̚ ̸̮̩̜̟̭̤̣̘̜̹̼̈́͜͝͝ḑ̶̥̘̖͍̫̒̐͝o̷̢̻̞͚̖͇̖̰̖̘̜͂̈́͜n̶̨̞̱̯͖͕͎̠̘̅͋͂͊̍̓͘̕͜͝'̵̧̗͓̂t̵̛̫̖͕̙̖̯̝̬̝͓̭͍͚̅̊͊̐͊́̈́̇̽͊͋͋͝͠ͅ ̶̢̻̭̺̰͚͖̮̫̺͎͕̟͂̀̒͆̈́̍͐̋́̈́͘͘͠ͅͅţ̶̪̤͓͚̲̦̈́̈́͋̀̓̅͜ͅh̸̡̡̙̬̯̱͈̘͔̻̱̥̜̼̅̅̀͜ḯ̴̛͍͙͑̋ņ̷̛̮͔̜͇̞͕͎̔̐͆͂͒͛͆͊͠k̷̡̮͓̬͖̝͐̇̽̈́̋͝ ̸̧̛͕̙̝͕̻̖̗̯̰̩̜̺͋̾̓̈́̒͋̀͒̅ÿ̵͖͙̖͔̭̩͔̲̩́̾̃́̈́̓ơ̵̩͚̘͎̘̥̹̬͔͚̺̰̺͈͛͛͗̾̎̔͑̾́̿́̀͝ͅu̴͙̫͚͔̳̣͈͙̯͂͑̄͗̒́͊͠ ̸̢̮̪̞͙̮͓͎̻̂́̈́̊̋̃͂̀͒̔̂̅͝á̷͓͚̬̥̻̪͇̩̟̪ŗ̶̠̞̠̪̺̺̤̰̖̳͍̀̋̓͜ͅę̷̡͕̝͔̳̳͕̼̪̣̮̣̀͛͋";
        }
    }
}
