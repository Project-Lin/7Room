using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Turn music ducking on or off in Master Audio.")]
public class MasterAudioDuckingToggle : FsmStateAction {
    [RequiredField]
    [Tooltip("Check this to enable ducking, uncheck it to disable ducking.")]
	public FsmBool enableDucking;
	
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.Instance.EnableMusicDucking = enableDucking.Value;
		
		Finish();
	}
	
	public override void Reset() {
		enableDucking = new FsmBool(false);
	}
}
