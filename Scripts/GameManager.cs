using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string hasKey;
    public string hasFlyingBoots;
    public string hasFireSword;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (!PlayerPrefs.HasKey("FireSword"))
        {
            PlayerPrefs.SetString("FireSword", "False");
            PlayerPrefs.SetString("FlyingBoots", "False");
            PlayerPrefs.SetString("Key", "False");
        }
    }
    void Start()
    {
        hasFireSword = PlayerPrefs.GetString("FireSword");
        hasFlyingBoots = PlayerPrefs.GetString("FlyingBoots");
        hasKey = PlayerPrefs.GetString("Key");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updatePurchases()
    {
        PlayerPrefs.SetString("FireSword", hasFireSword);
        PlayerPrefs.SetString("FlyingBoots", hasFlyingBoots);
        PlayerPrefs.SetString("Key", hasKey);
    }
}
