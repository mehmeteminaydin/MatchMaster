using UnityEngine;
using System.IO;
using System.Text;

namespace SNG.Save
{
    public static class FileManager
    {
        private const int XorKey = 96;
        private static string s_dataPath = Path.Combine(Application.persistentDataPath, "SngDataMatchMaster");

        /// <summary>
        /// Constructor. Creates a directory to save files
        /// </summary>
        static FileManager()
        {
            if (!Directory.Exists(s_dataPath))
            {
                Directory.CreateDirectory(s_dataPath);
            }
            
        }

        /// <summary>
        /// Saves given object to a given location
        /// </summary>
        /// <param name="filename">File name to save as</param>
        /// <param name="objectToSave">Object to save</param>
        public static void Save(string filename, object objectToSave)
        {
            string newPath = Path.Combine(s_dataPath, filename);
            File.WriteAllBytes(newPath, Encrypt(JsonUtility.ToJson(objectToSave)));
        }

        /// <summary>
        /// Loads given file
        /// </summary>
        /// <param name="filename">File name to load</param>
        /// <returns>Loaded object</returns>
        public static T Load<T>(string filename)
        {
            string path = Path.Combine(s_dataPath, filename);
            if (File.Exists(path))
                return JsonUtility.FromJson<T>(Decrypt(File.ReadAllBytes(path)));
            return default;
        }

        /// <summary>
        /// Applies xor encryption
        /// </summary>
        /// <param name="str">String to encrypt</param>
        /// <returns>Encrypted byte array</returns>
        private static byte[] Encrypt(string str)
        {
            return Encoding.ASCII.GetBytes(XOREncryptDecrypt(str));
        }

        /// <summary>
        /// Decrypts given parameter
        /// </summary>
        /// <param name="bytes">Byte array to decrypt</param>
        /// <returns>Decrypted string</returns>
        private static string Decrypt(byte[] bytes)
        {
            return XOREncryptDecrypt(Encoding.ASCII.GetString(bytes));
        }

        private static string XOREncryptDecrypt(string textToEncrypt)
        {
            StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
            for (int i = 0; i < textToEncrypt.Length; i++)
                outSb.Append((char)(textToEncrypt[i] ^ XorKey));
            return outSb.ToString();
        }
    }
}
