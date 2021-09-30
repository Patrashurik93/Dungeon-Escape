using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
  public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandlesShowResult
            };

            Advertisement.Show("Rewarded_Android", options);
        }
    }

    void HandlesShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.UpdateGemCount(GameManager.Instance.Player.diamonds);
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Video failed");
                break;
        }
    }
}
