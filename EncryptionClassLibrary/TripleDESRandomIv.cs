using System;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionClassLibrary
{
	// Token: 0x02000015 RID: 21
	public class TripleDESRandomIv : TripleDES
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005794 File Offset: 0x00003994
		public TripleDESRandomIv()
		{
			this.iv = this.GetRandomIv();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000057AA File Offset: 0x000039AA
		public TripleDESRandomIv(byte[] _key)
		{
			this.key = _key;
			this.iv = this.GetRandomIv();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000057C8 File Offset: 0x000039C8
		public new byte[] Encrypt(string plainText)
		{
			byte[] bytes = base.Encoder.GetBytes(plainText);
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			byte[] randomIv = this.GetRandomIv();
			int num = randomIv.Length;
			ICryptoTransform transform = tripleDESCryptoServiceProvider.CreateEncryptor(this.key, randomIv);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			memoryStream.Position = 0L;
			byte[] array = new byte[(long)num + memoryStream.Length];
			memoryStream.Read(array, num, (int)memoryStream.Length);
			cryptoStream.Close();
			for (int i = 0; i < num; i++)
			{
				array[i] = randomIv[i];
			}
			return array;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000588C File Offset: 0x00003A8C
		private byte[] GetRandomIv()
		{
			Random random = new Random();
			byte[] array = new byte[8];
			random.NextBytes(array);
			return array;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000058B4 File Offset: 0x00003AB4
		public new string Decrypt(byte[] InputInBytes)
		{
			bool flag = InputInBytes == null || InputInBytes.Length < 9;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				byte[] array = new byte[8];
				for (int i = 0; i < 8; i++)
				{
					array[i] = InputInBytes[i];
				}
				byte[] array2 = new byte[InputInBytes.Length - 8];
				for (int j = 8; j < InputInBytes.Length; j++)
				{
					array2[j - 8] = InputInBytes[j];
				}
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				ICryptoTransform transform = tripleDESCryptoServiceProvider.CreateDecryptor(this.key, array);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				cryptoStream.Write(array2, 0, array2.Length);
				cryptoStream.FlushFinalBlock();
				memoryStream.Position = 0L;
				string @string = base.Encoder.GetString(memoryStream.ToArray());
				result = @string;
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005994 File Offset: 0x00003B94
		public new IBatchDecryptor GetBatchDecryptor()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005994 File Offset: 0x00003B94
		public new IBatchEncryptor GetBatchEncryptor()
		{
			throw new NotSupportedException();
		}
	}
}
