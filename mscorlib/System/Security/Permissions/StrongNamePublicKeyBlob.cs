using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000655 RID: 1621
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		// Token: 0x06003A77 RID: 14967 RVA: 0x000C52DA File Offset: 0x000C42DA
		internal StrongNamePublicKeyBlob()
		{
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000C52E2 File Offset: 0x000C42E2
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("PublicKey");
			}
			this.PublicKey = new byte[publicKey.Length];
			Array.Copy(publicKey, 0, this.PublicKey, 0, publicKey.Length);
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000C5317 File Offset: 0x000C4317
		internal StrongNamePublicKeyBlob(string publicKey)
		{
			this.PublicKey = Hex.DecodeHexString(publicKey);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000C532C File Offset: 0x000C432C
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000C535E File Offset: 0x000C435E
		internal bool Equals(StrongNamePublicKeyBlob blob)
		{
			return blob != null && StrongNamePublicKeyBlob.CompareArrays(this.PublicKey, blob.PublicKey);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000C5376 File Offset: 0x000C4376
		public override bool Equals(object obj)
		{
			return obj != null && obj is StrongNamePublicKeyBlob && this.Equals((StrongNamePublicKeyBlob)obj);
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000C5394 File Offset: 0x000C4394
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8 ^ (int)baData[i] ^ num >> 24);
			}
			return num;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000C53C4 File Offset: 0x000C43C4
		public override int GetHashCode()
		{
			return StrongNamePublicKeyBlob.GetByteArrayHashCode(this.PublicKey);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000C53D1 File Offset: 0x000C43D1
		public override string ToString()
		{
			return Hex.EncodeHexString(this.PublicKey);
		}

		// Token: 0x04001E5A RID: 7770
		internal byte[] PublicKey;
	}
}
