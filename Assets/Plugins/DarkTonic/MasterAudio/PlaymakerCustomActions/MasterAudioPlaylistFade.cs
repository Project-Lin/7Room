using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Fade the Playlist volume in Master Audio to a specific volume over X seconds.")]
public class MasterAudioPlaylistFade : FsmStateAction {
	[Tooltip("Check this to perform action on all Playlist Controllers")]
	public FsmBool allPlaylistControllers;	

	[Tooltip("Name of Playlist Controller to use. Not required if you only have one.")]
	public FsmString playlistControllerName;

	[RequiredField]
	[HasFloatSlider(0, 1)]
	[Tooltip("Target Playlist Volume")]
	public FsmFloat targetVolume;
	
    [RequiredField]
	[HasFloatSlider(0, 10)]
	[Tooltip("Amount of time to complete fade (seconds)")]
	public FsmFloat fadeTime;
	
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		if (allPlaylistControllers.Value) {
            MasterAudio.FadeAllPlaylistsToVolume(targetVolume.Value, fadeTime.Value);
		}
		else {
			if (string.IsNullOrEmpty(playlistControllerName.Value)) {
				MasterAudio.FadePlaylistToVolume(targetVolume.Value, fadeTime.Value);
			} else {
				MasterAudio.FadePlaylistToVolume(playlistControllerName.Value, targetVolume.Value, fadeTime.Value);
			}
		}
		
		Finish();
	}
	
	public override void Reset() {
		allPlaylistControllers = new FsmBool(false);
		playlistControllerName = new FsmString(string.Empty);
		targetVolume = new FsmFloat();
		fadeTime = new FsmFloat();
	}
}
