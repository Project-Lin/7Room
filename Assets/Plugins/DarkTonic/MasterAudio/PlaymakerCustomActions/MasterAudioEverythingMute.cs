using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Mute all sound effects and Playlists in Master Audio.")]
public class MasterAudioEverythingMute : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.MuteEverything();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
