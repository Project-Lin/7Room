using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Unpause all sound effects in Master Audio. Does not include Playlists.")]
public class MasterAudioMixerUnpause : FsmStateAction {
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.UnpauseMixer();
		
		Finish();
	}
	
	public override void Reset() {
	}
}
