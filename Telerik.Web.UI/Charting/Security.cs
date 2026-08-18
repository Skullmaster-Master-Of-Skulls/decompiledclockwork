using System;
using System.IO;
using System.Security.Cryptography;

namespace Telerik.Charting
{
	// Token: 0x02001746 RID: 5958
	internal static class Security
	{
		// Token: 0x0600E8AF RID: 59567 RVA: 0x00343BF4 File Offset: 0x00341DF4
		internal static byte[] encryptStringToBytes_AES(string plainText, byte[] Key, byte[] IV)
		{
			if (plainText == null || plainText.Length <= 0)
			{
				throw new ArgumentNullException("plainText");
			}
			if (Key == null || Key.Length <= 0)
			{
				throw new ArgumentNullException("Key");
			}
			if (IV == null || IV.Length <= 0)
			{
				throw new ArgumentNullException("Key");
			}
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamWriter streamWriter = null;
			AesCryptoServiceProvider aesCryptoServiceProvider = null;
			try
			{
				aesCryptoServiceProvider = new AesCryptoServiceProvider();
				aesCryptoServiceProvider.Key = Key;
				aesCryptoServiceProvider.IV = IV;
				ICryptoTransform transform = aesCryptoServiceProvider.CreateEncryptor(aesCryptoServiceProvider.Key, aesCryptoServiceProvider.IV);
				memoryStream = new MemoryStream();
				cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				streamWriter = new StreamWriter(cryptoStream);
				streamWriter.Write(plainText);
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (aesCryptoServiceProvider != null)
				{
					aesCryptoServiceProvider.Clear();
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600E8B0 RID: 59568 RVA: 0x00343CCC File Offset: 0x00341ECC
		internal static string decryptStringFromBytes_AES(byte[] cipherText, byte[] Key, byte[] IV)
		{
			if (cipherText == null || cipherText.Length <= 0)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (Key == null || Key.Length <= 0)
			{
				throw new ArgumentNullException("Key");
			}
			if (IV == null || IV.Length <= 0)
			{
				throw new ArgumentNullException("Key");
			}
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			StreamReader streamReader = null;
			AesCryptoServiceProvider aesCryptoServiceProvider = null;
			string result = null;
			try
			{
				aesCryptoServiceProvider = new AesCryptoServiceProvider();
				aesCryptoServiceProvider.Key = Key;
				aesCryptoServiceProvider.IV = IV;
				ICryptoTransform transform = aesCryptoServiceProvider.CreateDecryptor(aesCryptoServiceProvider.Key, aesCryptoServiceProvider.IV);
				memoryStream = new MemoryStream(cipherText);
				cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
				streamReader = new StreamReader(cryptoStream);
				result = streamReader.ReadToEnd();
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
				if (cryptoStream != null)
				{
					cryptoStream.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
				if (aesCryptoServiceProvider != null)
				{
					aesCryptoServiceProvider.Clear();
				}
			}
			return result;
		}

		// Token: 0x040042B1 RID: 17073
		internal static byte[] chartIV = new byte[]
		{
			232,
			157,
			31,
			216,
			12,
			78,
			234,
			46,
			212,
			236,
			131,
			52,
			211,
			170,
			178,
			149
		};

		// Token: 0x040042B2 RID: 17074
		internal static byte[] chartKey = new byte[]
		{
			208,
			86,
			89,
			14,
			5,
			34,
			94,
			40,
			1,
			110,
			45,
			23,
			201,
			131,
			101,
			239,
			106,
			24,
			91,
			246,
			110,
			133,
			127,
			17,
			174,
			110,
			200,
			91,
			202,
			249,
			160,
			141
		};
	}
}
