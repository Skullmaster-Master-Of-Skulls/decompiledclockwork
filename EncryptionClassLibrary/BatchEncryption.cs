using System;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000005 RID: 5
	public class BatchEncryption : IBatchEncryptor, IDisposable, IBatchDecryptor
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000314E File Offset: 0x0000134E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00003156 File Offset: 0x00001356
		protected ICryptoTransform CryptoTransform { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000315F File Offset: 0x0000135F
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00003167 File Offset: 0x00001367
		protected Encoding Encoder { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x00003170 File Offset: 0x00001370
		public BatchEncryption(ICryptoTransform cryptoTransform, Encoding encoder)
		{
			this.CryptoTransform = cryptoTransform;
			this.Encoder = encoder;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000318C File Offset: 0x0000138C
		public string Decrypt(byte[] data)
		{
			byte[] bytes = this.CryptoTransform.TransformFinalBlock(data, 0, data.Length);
			return this.Encoder.GetString(bytes);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000031BC File Offset: 0x000013BC
		public byte[] Encrypt(string data)
		{
			byte[] bytes = this.Encoder.GetBytes(data);
			return this.CryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031EB File Offset: 0x000013EB
		public void Dispose()
		{
			ICryptoTransform cryptoTransform = this.CryptoTransform;
			if (cryptoTransform != null)
			{
				cryptoTransform.Dispose();
			}
		}
	}
}
