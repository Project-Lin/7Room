using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Remove a Sound Group in Master Audio from the list of sounds that cause music ducking.")]
public class MasterAudioDuckingRemoveGroup : FsmStateAction {
    [RequiredField]
    [Tooltip("Name of Master Audio Sound Group")]
	public FsmString soundGroupName;
	
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		MasterAudio.RemoveSoundGroupFromDuckList(soundGroupName.Value);
		
		Finish();
	}
	
	public override void Reset() {
		soundGroupName = new FsmString(string.Empty);
	}
}
