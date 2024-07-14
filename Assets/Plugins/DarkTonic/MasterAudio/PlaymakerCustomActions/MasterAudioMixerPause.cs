using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Pause all sound effects in Master Audio. Does not include Playlists.")]
public class MasterAudioMixerPause : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.PauseMixer();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
