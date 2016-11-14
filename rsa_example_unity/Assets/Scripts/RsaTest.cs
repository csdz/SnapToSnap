// RsaTest.cs

using UnityEngine;
using System.Collections;
using crypt;
using System.Text;
using System.IO;

public class RsaTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // (0) 加载公钥与私钥
        string pub = Path.Combine(Application.streamingAssetsPath, "public.pem");
        string pri = Path.Combine(Application.streamingAssetsPath, "private.pem");

        CryptModule cmodule = new CryptModule();
        cmodule.LoadRSAPriKey(pri);
        cmodule.LoadRSAPubKey(pub);

        string text = "<pub_encrypt & pri_decrypt> Hello this is tab_space"; // 原文

        // (1)使用公钥加密， 得到byEncryptText
        byte[] byEncryptText = cmodule.RSAPubKeyEncrypt(Encoding.UTF8.GetBytes(text));
        Debug.Log("Pub Encrypted length:" + byEncryptText.Length);

        // (2)使用私钥解密， 得到byEncryptText
        byte[] byDecryptText = cmodule.RSAPriKeyDecrypt(byEncryptText);
        Debug.Log("Pri Decrypted length:" + byDecryptText.Length.ToString());

        string strDecryptText = Encoding.UTF8.GetString(byDecryptText);
        Debug.Log("Pri Decrypted Text:" + strDecryptText);

        Debug.Log("/*----------------------------------------------------------*/");

        text = "<pri_encrypt & pub_decrypt> Hello this is tab_space"; // 原文

        // (1)使用私钥加密， 得到byEncryptText
        byEncryptText = cmodule.RSAPriKeyEncrypt(Encoding.UTF8.GetBytes(text));
        Debug.Log("Pri Encrypted length:" + byEncryptText.Length);

        // (2)使用公钥解密， 得到byEncryptText
        byDecryptText = cmodule.RSAPubKeyDecrypt(byEncryptText);
        Debug.Log("Pub Decrypted length:" + byDecryptText.Length.ToString());

        strDecryptText = Encoding.UTF8.GetString(byDecryptText);
        Debug.Log("Pub Decrypted Text:" + strDecryptText);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
