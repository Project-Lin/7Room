using DarkTonic.MasterAudio;
using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
public class MasterAudioFadeSoundGroupOfTransform : FsmStateAction {
    [RequiredField]
    [Tooltip("The GameObject to use for sound position.")]
    public FsmOwnerDefault gameObject;

    [Tooltip("Check this to fade all Sound Groups")]
    public FsmBool allSoundGroups;

    [Tooltip("Name of Master Audio Sound Group")]
    public FsmString soundGroupName = null;

    [RequiredField]
    [HasFloatSlider(0, 10)]
    [Tooltip("Amount of time to complete fade (seconds)")]
    public FsmFloat fadeTime;

    [RequiredField]
    [HasFloatSlider(0, 1)]
    [Tooltip("Volume After Fade")]
    public FsmFloat newVolume;
	
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

        var fadeAll = allSoundGroups != null && allSoundGroups.Value == true;

        var playingVars = MasterAudio.GetAllPlayingVariationsOfTransform(trans);
        for (var i = 0; i < playingVars.Count; i++) {
            var aVar = playingVars[i];
            if (fadeAll || aVar.ParentGroup.name == soundGroupName.Value) {
                aVar.FadeToVolume(newVolume.Value, fadeTime.Value); 
            }
        }
    }

	public override void Reset() {
        fadeTime = null;
        newVolume = null;
        gameObject = null;
        soundGroupName = null;
        allSoundGroups = null;
    }
}