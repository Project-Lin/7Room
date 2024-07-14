using DarkTonic.MasterAudio;
using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Get properties of a Sound Group Variation in Master Audio.")]
// ReSharper disable once CheckNamespace
public class MasterAudioVariationGetProps : FsmStateAction
{
    [RequiredField]
    [Tooltip("Name of Master Audio Sound Group")]
    public FsmString SoundGroupName;

    [RequiredField]
    [Tooltip("Name of a Variation in the Sound Group above")]
    public FsmString VariationName;

    [Tooltip("Name of Variable to store the Variation's current pitch in.")]
    [UIHint(UIHint.Variable)]
    public FsmFloat VariationPitch;

    [Tooltip("Name of Variable to store the Variation's current volume in.")]
    [UIHint(UIHint.Variable)]
    public FsmFloat VariationVolume;

    public override void OnEnter()
    {
        if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(SoundGroupName.Value))
        {
            Debug.LogError("You must enter the Sound Group Name");
            return;
        }

        if (string.IsNullOrEmpty(VariationName.Value))
        {
            Debug.LogError("You must enter the Variation Name");
            return;
        }

        var allVariations = MasterAudio.GetAllVariationsOfGroup(SoundGroupName.Value, false);
        if (allVariations == null)
        {
            Debug.LogError("The Sound Group '" + SoundGroupName.Value + "' was not found.");
            return;
        }

        MasterAudio.AudioInfo matchingVariation = null;

        for (var i = 0; i < allVariations.Count; i++)
        {
            var aVar = allVariations[i];
            if (aVar.Source.name.Equals(VariationName.Value))
            {
                matchingVariation = aVar;
                break;
            }
        }

        if (matchingVariation == null)
        {
            Debug.LogError("The Sound Group '" + SoundGroupName.Value + "' has no Variation named '" + VariationName.Value + "'.");
            return;
        }

        var aud = matchingVariation.Source;

        if (VariationPitch.UsesVariable)
        {
            VariationPitch.Value = aud.pitch;
        }

        if (VariationVolume.UsesVariable)
        {
            VariationVolume.Value = aud.volume;
        }

        Finish();
    }

    public override void Reset()
    {
        SoundGroupName = new FsmString(string.Empty);
        VariationName = new FsmString();
        VariationPitch = new FsmFloat();
        VariationVolume = new FsmFloat();
    }
}
