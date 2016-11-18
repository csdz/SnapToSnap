using UnityEngine;
using System.Collections;

namespace VoiceChat.UI
{
    public class VoiceChatUnityServer : MonoBehaviour
    {
        string port = "25001";
        int MaxConnections = 8;
        bool startServer = false;

        void OnGUI()
        {
            if (!startServer)
            {
                GUILayout.Label("\nPORT\n");
                port = GUILayout.TextField(port, 8);
                if (GUILayout.Button("\n  start server  \n"))
                {
                    Network.InitializeServer(MaxConnections, int.Parse(port), false);
                    MonoBehaviour.Destroy(GetComponent<VoiceChatUnityClient>());
                    gameObject.AddComponent<VoiceChatServerUi>();
                    startServer = true;
                }
            }
            else {
                GUILayout.Label("\nPORT: " + port + "\n");
                GUILayout.Label("PLAYERS: " + Network.connections.Length.ToString() + "\n");
                if (GUILayout.Button("\n  close server  \n"))
                {
                    Network.Disconnect();
                    MonoBehaviour.Destroy(gameObject.GetComponent<VoiceChatServerUi>());
                    startServer = false;
                }
            }
        }

        void OnPlayerConnected(NetworkPlayer player)
        {
            Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
        }

        void OnPlayerDisconnected(NetworkPlayer player)
        {
            Network.RemoveRPCs(player);
            Network.DestroyPlayerObjects(player);
        }
    } 
}
