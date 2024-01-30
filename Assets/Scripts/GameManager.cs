using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int savedLives = 5;
    int savedCoins = 0;
    public static GameManager gM;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI coinsText;
    // Start is called before the first frame update

    void Awake()
    {
        if (gM == null)
        {
            gM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        
    }

    public int GetSavedLives()
    {
        return savedLives;
    }
    public int GetSavedCoins()
    {
        return savedCoins;
    }

    public void SaveData(int lives, int coins)
    {
        savedLives = lives;
        savedCoins = coins;
        livesText.text = "x " + savedLives.ToString();
        coinsText.text = "x " + savedCoins.ToString();
    }
}
