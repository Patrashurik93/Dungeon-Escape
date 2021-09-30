using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        { 
            if (instance == null)
            {
                Debug.LogError("UI Manger is null");
            }
        return instance;
        }
    }

    public TextMeshProUGUI playerGemCountText;
    public Image selectionImage;
    public Text gemCountText;
    public Image[] healthBars;
    

    private void Awake()
    {
        instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = " " + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = " " + count;
        var gems = Convert.ToInt32(gemCountText.text);
        //playerGemCountText.text = " " + count + "G";
        playerGemCountText.DOCounter(gems - count, gems, 1f);
    }

    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MainMenu()
    {
        Application.Quit();
    }
}