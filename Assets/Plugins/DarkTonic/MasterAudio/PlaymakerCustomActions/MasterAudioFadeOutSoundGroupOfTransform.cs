using DarkTonic.MasterAudio;
using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
public class MasterAudioFadeOutSoundGroupOfTransform : FsmStateAction {
    [RequiredField]
    [Tooltip("The GameObject to use for sound position.")]
    public FsmOwnerDefault gameObject;

    [Tooltip("Name of Master Audio Sound Group")]
    public FsmString soundGroupName = null;

    [RequiredField]
    [HasFloatSlider(0, 10)]
    [Tooltip("Amount of time to complete fade (seconds)")]
    public FsmFloat fadeTime;
	
	public override void OnEnter() {
        FadeVolume();
		Finish();
	}

    public void FadeVolume() {
        Transform trans;
        if (gameObject.GameObject.Value != null) {
            trans = gameObject.GameObject.Value.transform;
        } else {
            trans = Owner.transform;
        }

        MasterAudio.FadeOutSoundGroupOfTransform(trans, soundGroupName.Value, fadeTime.Value);
    }

	public override void Reset() {
        fadeTime = null;
        gameObject = null;
        soundGroupName = null;
    }
}