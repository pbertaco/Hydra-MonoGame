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

        internal void play(string someMusicName)
        {
#if DEBUG
            return;
#endif
            if (musicName == someMusicName) { return; }

            contentManager.Unload();

            musicName = someMusicName;

            Song song = Song(someMusicName);

            if (song != null)
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
            } else {
                Microsoft.Xna.Framework.Media.MediaPlayer.Stop();
            }
        }

        Song Song(string assetName)
        {
            Song song;

            try
            {
                song = contentManager.Load<Song>("Song/" + assetName);
            }
            catch (Exception)
            {
                song = null;
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
