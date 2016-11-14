using System;
using System.Security.Cryptography;
using System.IO;

namespace crypt
{
    public class CryptModule
    {
        private RSACryptoServiceProvider m_oPriRsa;
        private RSACryptoServiceProvider m_oPubRsa;

        private bool m_bOAEP = false;

        public bool LoadRSAPriKey(string strFilePath)
        {   
            rsakey.LoadProviderFromFile(strFilePath, ref m_oPriRsa);
            return m_oPriRsa != null;
        }

        public bool LoadRSAPubKey(string strFilePath)
        {
            rsakey.LoadProviderFromFile(strFilePath, ref m_oPubRsa);
            return m_oPubRsa != null;
        }

        public byte[] RSAPubKeyEncrypt(byte[] byPlainText)
        {
            if (m_oPubRsa == null)
            {
                return null;
            }
            return m_oPubRsa.Encrypt(byPlainText, m_bOAEP);
        }

        public byte[] RSAPriKeyDecrypt(byte[] byPlainText)
        {
            if (m_oPriRsa == null)
            {
                return null;
            }
            return m_oPriRsa.Decrypt(byPlainText, m_bOAEP);
        }

        public byte[] RSAPriKeyEncrypt(byte[] byPlainText)
        {
            if (m_oPriRsa == null)
            {
                return null;
            }
            return m_oPriRsa.Encrypt(byPlainText, m_bOAEP);
        }

        public byte[] RSAPubKeyDecrypt(byte[] byPlainText)
        {
            if (m_oPubRsa == null)
            {
                return null;
            }
            return m_oPriRsa.Decrypt(byPlainText, m_bOAEP);
        }
    }

}