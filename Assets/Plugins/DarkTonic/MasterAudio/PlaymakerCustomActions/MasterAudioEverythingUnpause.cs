using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Unpause all sound effects and Playlists in Master Audio.")]
public class MasterAudioEverythingUnpause : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.UnpauseEverything();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
