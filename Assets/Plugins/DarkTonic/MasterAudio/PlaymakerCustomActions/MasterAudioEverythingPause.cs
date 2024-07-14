using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Pause all sound effects and Playlists in Master Audio.")]
public class MasterAudioEverythingPause : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.PauseEverything();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
