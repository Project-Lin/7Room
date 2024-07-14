using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Stop all sound effects in Master Audio. Does not include Playlists.")]
public class MasterAudioMixerStop : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.StopMixer();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
