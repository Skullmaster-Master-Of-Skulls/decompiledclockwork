using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000010 RID: 16
	public class BatchDecryptor : IBatchDecryptor, IDisposable
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00005000 File Offset: 0x00003200
		~BatchDecryptor()
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000502C File Offset: 0x0000322C
		public void Dispose()
		{
			ICryptoTransform cryptoTransform = this.cryptoTransform;
			if (cryptoTransform != null)
			{
				cryptoTransform.Dispose();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005044 File Offset: 0x00003244
		public void Init(params object[] args)
		{
			this.key = (byte[])args[0];
			this.iv = (byte[])args[1];
			this.utf8encoder = new UTF8Encoding();
			this.tdesProvider = new TripleDESCryptoServiceProvider();
			this.cryptoTransform = this.tdesProvider.CreateDecryptor(this.key, this.iv);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000050A4 File Offset: 0x000032A4
		public string Decrypt(byte[] data)
		{
			bool flag = data == null || data.Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = this.DecryptMini(data, this.utf8encoder, this.tdesProvider, this.cryptoTransform);
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000050E8 File Offset: 0x000032E8
		private string DecryptMini(byte[] inputInBytes, UTF8Encoding utf8encoder, TripleDESCryptoServiceProvider tdesProvider, ICryptoTransform cryptoTransform)
		{
			return this.DecryptMini(inputInBytes, utf8encoder, tdesProvider, cryptoTransform, true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005108 File Offset: 0x00003308
		private string DecryptMini(byte[] inputInBytes, UTF8Encoding utf8encoder, TripleDESCryptoServiceProvider tdesProvider, ICryptoTransform cryptoTransform, bool tryRepairBadData)
		{
			string result;
			try
			{
				bool flag = inputInBytes == null;
				if (flag)
				{
					result = "";
				}
				else
				{
					MemoryStream memoryStream = new MemoryStream();
					CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
					cryptoStream.Write(inputInBytes, 0, inputInBytes.Length);
					cryptoStream.FlushFinalBlock();
					memoryStream.Position = 0L;
					result = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			catch (Exception ex)
			{
				try
				{
					result = this.TryDecryptNoPadding(inputInBytes);
				}
				catch
				{
					string text = tryRepairBadData ? this.TryRepairBadData(inputInBytes, (byte[] g) => this.DecryptMini(g, utf8encoder, tdesProvider, cryptoTransform, false)) : null;
					result = (text ?? ".?.");
				}
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000051EC File Offset: 0x000033EC
		private string TryRepairBadData(byte[] inputInBytes, Func<byte[], string> normalDecryptFunction)
		{
			bool flag = inputInBytes == null || inputInBytes.Length < 1 || inputInBytes.Length % 8 != 7;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					byte[] array = new byte[inputInBytes.Length + 1];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = inputInBytes[i];
					}
					result = normalDecryptFunction(array);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005264 File Offset: 0x00003464
		private string TryDecryptNoPadding(byte[] inputInBytes)
		{
			bool flag = inputInBytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				ICryptoTransform transform = new TripleDESCryptoServiceProvider
				{
					Padding = PaddingMode.None
				}.CreateDecryptor(this.key, this.iv);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
				cryptoStream.Write(inputInBytes, 0, inputInBytes.Length);
				cryptoStream.FlushFinalBlock();
				memoryStream.Position = 0L;
				result = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x04000025 RID: 37
		private UTF8Encoding utf8encoder;

		// Token: 0x04000026 RID: 38
		private TripleDESCryptoServiceProvider tdesProvider;

		// Token: 0x04000027 RID: 39
		private ICryptoTransform cryptoTransform;

		// Token: 0x04000028 RID: 40
		private byte[] key;

		// Token: 0x04000029 RID: 41
		private byte[] iv;
	}
}
