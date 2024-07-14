using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Play a random clip in a Playlist in Master Audio")]
public class MasterAudioPlaylistClipRandom : FsmStateAction {
	[Tooltip("Check this to perform action on all Playlist Controllers")]
	public FsmBool allPlaylistControllers;	

	[Tooltip("Name of Playlist Controller containing the Playlist. Not required if you only have one Playlist Controller.")]
	public FsmString playlistControllerName;

	public override void OnEnter() {
		if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
		{
			return;
		}

		if (allPlaylistControllers.Value) {
            MasterAudio.TriggerRandomClipAllPlaylists();
		}
		else {
			if (string.IsNullOrEmpty(playlistControllerName.Value)) {
				MasterAudio.TriggerRandomPlaylistClip();
			} else {
				MasterAudio.TriggerRandomPlaylistClip(playlistControllerName.Value);
			}
		}
		
		Finish();
	}
	
	public override void Reset() {
		allPlaylistControllers = new FsmBool(false);
		playlistControllerName = new FsmString(string.Empty);
	}
}
