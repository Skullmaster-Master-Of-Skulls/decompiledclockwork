using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000011 RID: 17
	public class BatchEncryptor : IBatchEncryptor, IDisposable
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000052F0 File Offset: 0x000034F0
		~BatchEncryptor()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000531C File Offset: 0x0000351C
		public void Dispose()
		{
			ICryptoTransform cryptoTransform = this.cryptoTransform;
			if (cryptoTransform != null)
			{
				cryptoTransform.Dispose();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005334 File Offset: 0x00003534
		public void Init(params object[] args)
		{
			this.key = (byte[])args[0];
			this.iv = (byte[])args[1];
			this.utf8encoder = new UTF8Encoding();
			this.tdesProvider = new TripleDESCryptoServiceProvider();
			this.cryptoTransform = this.tdesProvider.CreateEncryptor(this.key, this.iv);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005394 File Offset: 0x00003594
		public byte[] Encrypt(string data)
		{
			return this.EncryptMini(data ?? "", this.utf8encoder, this.tdesProvider, this.cryptoTransform);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000053C8 File Offset: 0x000035C8
		private byte[] EncryptMini(string inputString, UTF8Encoding utf8encoder, TripleDESCryptoServiceProvider tdesProvider, ICryptoTransform cryptoTransform)
		{
			bool flag = inputString == null || inputString.Trim().Length < 1;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] bytes = utf8encoder.GetBytes(inputString);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				memoryStream.Position = 0L;
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, (int)memoryStream.Length);
				cryptoStream.Close();
				result = array;
			}
			return result;
		}

		// Token: 0x0400002A RID: 42
		private UTF8Encoding utf8encoder;

		// Token: 0x0400002B RID: 43
		private TripleDESCryptoServiceProvider tdesProvider;

		// Token: 0x0400002C RID: 44
		private ICryptoTransform cryptoTransform;

		// Token: 0x0400002D RID: 45
		private byte[] key;

		// Token: 0x0400002E RID: 46
		private byte[] iv;
	}
}
