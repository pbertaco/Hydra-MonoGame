﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using Realms;

namespace Hydra
{
    public class MemoryCard
    {
        internal static MemoryCard current;

        internal PlayerData playerData;

        Realm realm;
        Transaction transaction;

        public MemoryCard()
        {
            current = this;

            RealmConfiguration realmConfiguration = new RealmConfiguration()
            {
                ShouldDeleteIfMigrationNeeded = true
            };

            realm = Realm.GetInstance(realmConfiguration);
        }

        internal void newGame()
        {
            Console.WriteLine("newGame");

            transaction = realm.BeginWrite();

            playerData = new PlayerData();
            realm.Add(playerData);

            saveGame();
        }

        internal void saveGame(bool willTerminate = false)
        {
            Console.WriteLine("saveGame");
            transaction.Commit();

            if (willTerminate)
            {
                transaction.Dispose();
                realm.Dispose();
            }
            else
            {
                transaction = realm.BeginWrite();
            }
        }

        internal void loadGame()
        {
            Console.WriteLine("loadGame");
            if (playerData == null)
            {
                var data = realm.All<PlayerData>();

                if (data.Count() > 0)
                {
                    playerData = data.Last();
                    transaction = realm.BeginWrite();
                }
                else
                {
                    newGame();
                }
            }
        }

        internal void resetGame()
        {
            Console.WriteLine("reset");
            realm.RemoveAll();
            transaction.Commit();
            playerData = null;
            newGame();
        }
    }
}