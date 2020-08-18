using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text totalDiamondsinShop;
    public Text currentDiamondsInPlayMode;
    public Text TotalDiamondsInPlayMode;
    public static UIManager instance;
    public GameObject cannotBuyPanel;
    public GameObject MobileControllers;
    public GameObject pausePanel;
    public List<Image> healthBar = new List<Image>();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if(SystemInfo.deviceType == DeviceType.Desktop){
            // MobileControllers.SetActive(false);
        }
    }
    void Update()
    {
        if (PlayerController.Playing)
        {
            currentDiamondsInPlayMode.text = PlayerController.collectedDiamonds.ToString() + "G";
            TotalDiamondsInPlayMode.text = PlayerPrefs.GetInt("TotalDiamonds").ToString() + "G";
            totalDiamondsinShop.text = PlayerPrefs.GetInt("TotalDiamonds").ToString() + "G";
        }
    }

    public void cannotPurchase()
    {
        cannotBuyPanel.SetActive(true);
        Invoke("hideUI", 2f);
    }

    void hideUI()
    {
        cannotBuyPanel.SetActive(false);
    }

    public void decreaseHealth(int lessHealth)
    {
        for (int i = 1; i <= lessHealth; i++)
        {
            healthBar[healthBar.Count - 1].enabled = false;
            healthBar.RemoveAt(healthBar.Count - 1);
        }
    }

    public void hidePausePanel(){
        pausePanel.SetActive(false);
    }
    public void showPausePanel(){
        pausePanel.SetActive(true);
    }
}
