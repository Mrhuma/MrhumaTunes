using BepInEx;
using System.IO;
using System.Linq;

namespace MrhumaTunes
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly string NewSongsFolder = Path.Combine(Paths.PluginPath, "Mrhuma-MrhumaTunes", "New Songs");
		private readonly string BoomboxMusicFolder = Path.Combine(Paths.BepInExRootPath, "Custom Songs", "Boombox Music");

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            if(!Directory.Exists(BoomboxMusicFolder)) Directory.CreateDirectory(BoomboxMusicFolder);
            if(!Directory.Exists(NewSongsFolder)) Directory.CreateDirectory(NewSongsFolder);

            if(Directory.GetFiles(NewSongsFolder).Length > 0) //If there are songs in the New Songs folder, replace the current songs with these new ones
            {
                DeleteOldSongs();
                AddNewSongs();
            }
        }

        private void DeleteOldSongs()
        {
            //Get all mp3, wav, and ogg files in the boombox folder
            string[] songs =
            [
                .. Directory.GetFiles(BoomboxMusicFolder, "*.mp3"),
                .. Directory.GetFiles(BoomboxMusicFolder, "*.wav"),
                .. Directory.GetFiles(BoomboxMusicFolder, "*.ogg"),
            ];

            foreach(string song in songs)
            {
                File.Delete(song);
            }
        }

        private void AddNewSongs()
        {
            //Get all mp3, wav, and ogg files in the temp folder
            string[] songs =
            [
                .. Directory.GetFiles(NewSongsFolder, "*.mp3"),
                .. Directory.GetFiles(NewSongsFolder, "*.wav"),
                .. Directory.GetFiles(NewSongsFolder, "*.ogg"),
            ];

            //Move all songs from the temp folder to the permanent boombox folder
            foreach(string song in songs)
            {
                File.Move(song, Path.Combine(BoomboxMusicFolder, Path.GetFileName(song)));
            }
        }
    }
}