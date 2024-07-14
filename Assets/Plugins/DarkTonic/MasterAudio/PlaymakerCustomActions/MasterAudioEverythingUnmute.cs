using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Unmute all sound effects and Playlists in Master Audio.")]
public class MasterAudioEverythingUnmute : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.UnmuteEverything();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
