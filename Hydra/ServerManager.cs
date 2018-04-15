using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Lidgren.Network;
using System.Threading.Tasks;

namespace Hydra
{
    public class ServerManager
    {
        internal static ServerManager current;

        NetPeerConfiguration peerConfiguration;

        NetServer server;
        NetClient client;

        public ServerManager(string appIdentifier, int port = 8900, int maximumConnections = 32)
        {
            peerConfiguration = new NetPeerConfiguration(appIdentifier)
            {
                MaximumConnections = maximumConnections,
                Port = port,
                AutoFlushSendQueue = false
            };
            current = this;
        }

        internal async void startBrowsingForPeers() // Start Server
        {
            if (client != null)
            {
                client.Disconnect("bye");
                while (client.Status != NetPeerStatus.NotRunning)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }

            server = new NetServer(peerConfiguration);
            server.Start();

            while (server.Status != NetPeerStatus.Running)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        internal async void startAdvertisingPeer() // Connect to server
        {
            if (server != null)
            {
                server.Shutdown("bye");
                while (server.Status != NetPeerStatus.NotRunning)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }

            client = new NetClient(peerConfiguration);
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            client.RegisterReceivedCallback(new SendOrPostCallback(HandleSendOrPostCallback));
            client.Start();

            while (client.Status != NetPeerStatus.Running)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            client.DiscoverLocalPeers(peerConfiguration.Port);
        }

        void HandleSendOrPostCallback(object peer)
        {
            NetIncomingMessage incomingMessage;

            while ((incomingMessage = client.ReadMessage()) != null)
            {
                Console.WriteLine(incomingMessage.ReadString());

                switch (incomingMessage.MessageType)
                {
                    case NetIncomingMessageType.Error:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        statusChanged(incomingMessage);
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        break;
                    case NetIncomingMessageType.Data:
                        break;
                    case NetIncomingMessageType.Receipt:
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.NatIntroductionSuccess:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                }

                client.Recycle(incomingMessage);
            }
        }

        void statusChanged(NetIncomingMessage incomingMessage)
        {
            NetConnectionStatus connectionStatus = (NetConnectionStatus)incomingMessage.ReadByte();

            switch (connectionStatus)
            {
                case NetConnectionStatus.None:
                    break;
                case NetConnectionStatus.InitiatedConnect:
                    break;
                case NetConnectionStatus.ReceivedInitiation:
                    break;
                case NetConnectionStatus.RespondedAwaitingApproval:
                    break;
                case NetConnectionStatus.RespondedConnect:
                    break;
                case NetConnectionStatus.Connected:
                    break;
                case NetConnectionStatus.Disconnecting:
                    break;
                case NetConnectionStatus.Disconnected:
                    break;
            }
        }
    }


}
