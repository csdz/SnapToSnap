using UnityEngine;
using System.Collections;
using VoiceChat.Networking;

namespace VoiceChat.UI
{
    public class VoiceChatUnityClient : MonoBehaviour
    {
        VoiceChatNetworkProxy proxy;

        string address = "127.0.0.1";
        string port = "25001";
        bool isConnected = false;

        void OnGUI()
        {

            if (!isConnected)
            {
                GUILayout.Label("\nIP_ADDRESS\n");
                address = GUILayout.TextField(address, 50);
                GUILayout.Label("\nPORT\n");
                port = GUILayout.TextField(port, 8);
                if (GUILayout.Button("\n  connect  \n"))
                {
                    Debug.Log(address + " " + port);
                    Network.Connect(address, int.Parse(port));
                }
            }
            else {
                GUILayout.Label("\nIP:" + address + "\n");
                GUILayout.Label("\nPORT:" + port.ToString() + "\n");
                if (GUILayout.Button("\n  close connect  \n"))
                {
                    Debug.Log(address + " " + port);
                    Network.Disconnect();
                    MonoBehaviour.Destroy(gameObject.GetComponent<VoiceChatUi>());
                }
            }
        }

        void OnConnectedToServer()
        {
            Debug.Log("OnConnectedToServer");
            proxy = VoiceChatNetworkUtils.CreateProxy();
            gameObject.AddComponent<VoiceChatUi>();
            isConnected = true;
        }

        void OnDisconnectedFromServer(NetworkDisconnection info)
        {
			MonoBehaviour.Destroy(gameObject.GetComponent<VoiceChatUi>());
            GameObject.Destroy(proxy.gameObject);
            isConnected = false;
        }
    } 
}