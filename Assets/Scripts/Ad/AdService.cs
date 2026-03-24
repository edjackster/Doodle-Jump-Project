using System;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Zenject;

public class AdService : IRewardedVideoAdListener, IDisposable
{
    private const string AppKey = "d7328bd925cb3438791db153757c454df425728429341837";

    private SignalBus _signalBus;
    private int _adType = AppodealAdType.RewardedVideo;

    public AdService(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<ResumeButtonSignal>(ShowAd);
        
        Appodeal.SetTesting(true);
        Appodeal.SetRewardedVideoCallbacks(this);

        Appodeal.Initialize(AppKey, _adType);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ResumeButtonSignal>(ShowAd);
    }

    public void OnRewardedVideoFinished(double amount, string currency)
    {
        _signalBus.Fire<ResumeAdWatchedSignal>();
    }

    private void ShowAd()
    {
        bool canShow = Appodeal.CanShow(_adType);
        bool isPrecache = Appodeal.IsPrecache(_adType);
        bool isLoaded = Appodeal.IsLoaded(_adType);

        if (isLoaded && canShow && !isPrecache)
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
    }

    public void OnRewardedVideoLoaded(bool isPrecache) { }

    public void OnRewardedVideoFailedToLoad() { }

    public void OnRewardedVideoShowFailed() { }

    public void OnRewardedVideoShown() { }

    public void OnRewardedVideoClosed(bool finished) {}

    public void OnRewardedVideoExpired() {}

    public void OnRewardedVideoClicked() {}
}