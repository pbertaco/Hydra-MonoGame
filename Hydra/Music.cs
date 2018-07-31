using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hydra
{
    public class Music
    {
        internal static Music sharedInstance = new Music();

        internal ContentManager contentManager;

        string musicName = "";

        internal void play(string musicName)
        {
            if (this.musicName == musicName) { return; }

            contentManager.Unload();

            this.musicName = musicName;

            Microsoft.Xna.Framework.Media.MediaPlayer.Play(Song(musicName));
        }

        Song Song(string assetName)
        {
            Song song;

            try
            {
                song = contentManager.Load<Song>("Song/" + assetName);
            }
            catch (ContentLoadException)
            {
                song = contentManager.Load<Song>("Song/null");
            }

            return song;
        }

        internal void pause()
        {
            Microsoft.Xna.Framework.Media.MediaPlayer.Pause();
        }

        internal void stop()
        {
            Microsoft.Xna.Framework.Media.MediaPlayer.Stop();
        }
    }
}
