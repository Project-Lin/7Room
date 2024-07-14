using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Stop all sound effects and Playlists in Master Audio.")]
public class MasterAudioEverythingStop : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.StopEverything();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
