using DarkTonic.MasterAudio;
using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Unpause all Audio in a Bus in Master Audio")]
public class MasterAudioBusUnpause : FsmStateAction {
	[Tooltip("Check this to perform action on all Buses")]
	public FsmBool allBuses;	

    [Tooltip("Name of Master Audio Bus")]
	public FsmString busName;
	
	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		if (!allBuses.Value && string.IsNullOrEmpty(busName.Value)) {
			Debug.LogError("You must either check 'All Buses' or enter the Bus Name");
			return;
		}

		if (allBuses.Value) {
			var busNames = MasterAudio.RuntimeBusNames;
			for (var i = 0; i < busNames.Count; i++) {
				MasterAudio.UnpauseBus(busNames[i]);
			}
		} else {
			MasterAudio.UnpauseBus(busName.Value);
		}
		
		Finish();
	}
	
	public override void Reset() {
		allBuses = new FsmBool(false);
		busName = new FsmString(string.Empty);
	}
}
