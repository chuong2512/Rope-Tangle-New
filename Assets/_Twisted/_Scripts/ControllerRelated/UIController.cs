using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Twisted._Scripts.ControllerRelated
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public TextMeshProUGUI levelNumText,coinNumText;
        public GameObject HUD,targetDisplay,winPanel,failPanel,failPanelNonPowerup,targetBallDisplayPanel;

        public GameObject inapp;

        public void NextLevel()
        {
            if(GameDataManager.Instance.playerData.intDiamond>=50)
            {
                GameDataManager.Instance.playerData.SubDiamond(50);
                MainController.instance.SetActionType(GameState.Levelwin);
            }
            else
            {
                inapp.SetActive(true);
            }
        }

        private void Awake()
        {
            instance=this;
        }

        private void Start()
        {
            levelNumText.text="Level "+PlayerPrefs.GetInt("levelnumber",1);
        }

        private void OnEnable()
        {
            MainController.GameStateChanged+=GameManager_GameStateChanged;
        }

        private void OnDisable()
        {
            MainController.GameStateChanged-=GameManager_GameStateChanged;
        }

        void GameManager_GameStateChanged(GameState newState,GameState oldState)
        {
            if(newState==GameState.Levelwin)
            {
                DOVirtual.DelayedCall(
                2.5f,
                () =>
                {
                    winPanel.SetActive(true);
                    QuangCao.Instance.PhatQuangCao();
                    //if(ISManager.instance) ISManager.instance.ShowInterstitialAds();
                });
            }

            if(newState==GameState.Levelfail)
            {
                DOVirtual.DelayedCall(
                0.75f,
                () =>
                {
                    failPanelNonPowerup.SetActive(true);

                    QuangCao.Instance.PhatQuangCao();
                });
            }
        }

        public void UpdateCoinUi()
        {
            coinNumText.text=PlayerPrefs.GetInt("TotalCoins",0).ToString();
        }
    }
}
