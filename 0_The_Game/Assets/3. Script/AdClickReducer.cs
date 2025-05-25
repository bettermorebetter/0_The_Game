// using UnityEngine;
// using UnityEngine.Advertisements;

// public class AdClickReducer : MonoBehaviour,
//     IUnityAdsInitializationListener,
//     IUnityAdsLoadListener,
//     IUnityAdsShowListener
// {
//     [SerializeField] string _androidGameId = "YOUR_ANDROID_GAME_ID";
//     [SerializeField] string _adUnitId     = "Rewarded_Android";
//     [SerializeField] bool   _testMode     = true;

//     public int clicksRemaining;

//     void Awake() {
//         Advertisement.Initialize(_androidGameId, _testMode, this);
//     }

//     // 초기화 리스너
//     public void OnInitializationComplete() => Advertisement.Load(_adUnitId, this);
//     public void OnInitializationFailed(UnityAdsInitializationError e, string msg)
//         => Debug.LogError($"Ads init 실패: {e} {msg}");

//     // 로드 리스너
//     public void OnUnityAdsAdLoaded(string placementId) { }
//     public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError e, string msg)
//         => Debug.LogError($"Ad load 실패: {e} {msg}");

//     // 버튼에서 호출
//     public void ShowRewardedAd()
//     {
//         if (Advertisement.IsReady(_adUnitId))
//             Advertisement.Show(_adUnitId, this);
//     }

//     // 재생 리스너
//     public void OnUnityAdsShowFailure(string id, UnityAdsShowError e, string msg)
//         => Debug.LogError($"Ad show 실패: {e} {msg}");
//     public void OnUnityAdsShowStart(string id) { }
//     public void OnUnityAdsShowClick(string id) { }
//     public void OnUnityAdsShowComplete(string id, UnityAdsShowCompletionState state)
//     {
//         if (id == _adUnitId && state == UnityAdsShowCompletionState.COMPLETED)
//             ApplyHalvedClicks();
//     }

//     void ApplyHalvedClicks()
//     {
//         if (clicksRemaining > 1)
//             clicksRemaining = Mathf.CeilToInt(clicksRemaining / 2f);
//     }
// }
