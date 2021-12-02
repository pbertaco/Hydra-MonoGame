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
    class Music
    {
        internal static Music sharedInstance = new Music();

        internal ContentManager contentManager;

        string musicName = "";

        internal void play(string someMusicName)
        {
            if (musicName == someMusicName) { return; }

            contentManager.Unload();

            musicName = someMusicName;

            Song song = Song(someMusicName);

            if (song != null)
            {
                MediaPlayer.Play(song);
            }
            else
            {
                MediaPlayer.Stop();
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
            MediaPlayer.Pause();
        }

        internal void stop()
        {
            MediaPlayer.Stop();
        }
    }
}
