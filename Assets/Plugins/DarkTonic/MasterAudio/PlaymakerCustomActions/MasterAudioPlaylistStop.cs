using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Stop current Playlist in Master Audio")]
public class MasterAudioPlaylistStop : FsmStateAction {
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
			MasterAudio.StopAllPlaylists();
		} else {
			if (string.IsNullOrEmpty(playlistControllerName.Value)) {
				MasterAudio.StopPlaylist();
			} else {
				MasterAudio.StopPlaylist(playlistControllerName.Value);
			}
		}
		
		Finish();
	}
	
	public override void Reset() {
		allPlaylistControllers = new FsmBool(false);
		playlistControllerName = new FsmString(string.Empty);
	}
}
