using DarkTonic.MasterAudio;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Master Audio")]
[Tooltip("Change the Playlist that's loaded by a Playlist Controller in Master Audio")]
public class MasterAudioPlaylistChange : FsmStateAction {
    [Tooltip("Name of Playlist Controller to use. Not required if you only have one.")]
	public FsmString playlistControllerName;

    [RequiredField]
    [Tooltip("Name of playlist to load.")]
	public FsmString playlistName;

    [RequiredField]
    [Tooltip("Check this if you want the first song to play after loaded.")]
    public FsmBool playFirstSong;

	public override void OnEnter() {
        if (MasterAudio.AppIsShuttingDown || MasterAudio.SafeInstance == null)
        {
            return;
        }
        
        if (string.IsNullOrEmpty(playlistControllerName.Value)) {
            MasterAudio.ChangePlaylistByName(playlistName.Value, playFirstSong.Value);
		} else {
            MasterAudio.ChangePlaylistByName(playlistControllerName.Value, playlistName.Value, playFirstSong.Value);
        }

        Finish();
	}
	
	public override void Reset() {
        playlistName = new FsmString(string.Empty);
        playFirstSong = new FsmBool();
		playlistControllerName = new FsmString(string.Empty);
	}
}
