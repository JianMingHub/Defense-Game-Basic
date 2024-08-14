using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace COHENLI.DefenseBasic
{
    public class GUIManager : MonoBehaviour
    {
        public static GUIManager Ins;

        public GameObject homeGUI;
        public GameObject gameGUI;
        public Dialog gameoverDialog;
        public Text mainCoinTxt;
        public Text gameplayCoinTxt;
        
        public void Awake()
        {
            Ins = this;
        }
        public void ShowGameGUI(bool isShow)
        {
            if (gameGUI)
                gameGUI.SetActive(isShow);
            if (homeGUI)
                homeGUI.SetActive(!isShow);
        }
        public void UpdateMainCoins()
        {
            if (mainCoinTxt)
                mainCoinTxt.text = Pref.coins.ToString();
        }
        public void UpdateGameplayCoins()
        {
            if (gameplayCoinTxt)
                gameplayCoinTxt.text = Pref.coins.ToString();
        }
    }
}
