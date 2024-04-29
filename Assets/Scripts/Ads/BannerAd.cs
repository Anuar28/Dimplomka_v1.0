using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] private string _androidAdUnitId = "BannerAndroid";

    string _adUnitId = null;
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    void Start()
    {
#if UNITY_IOS
    _adUnitId=_iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        LoadBannerAd();
    }

    public void LoadBannerAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Banner.SetPosition(_bannerPosition);
            BannerLoadOptions options = new BannerLoadOptions()
            {
                loadCallback = OnBannerLoad,
                errorCallback = OnBannerError
            };
            Advertisement.Banner.Load(_adUnitId, options);
        }
        
    }

    private void OnBannerError(string message)
    {
        Debug.Log("Banner Error");
    }

    private void OnBannerLoad()
    {
        Advertisement.Banner.Show(_adUnitId);
    }
}
