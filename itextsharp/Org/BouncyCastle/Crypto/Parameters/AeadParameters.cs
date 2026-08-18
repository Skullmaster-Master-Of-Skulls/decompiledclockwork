using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200018C RID: 396
	public class AeadParameters : ICipherParameters
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x00058C58 File Offset: 0x00057C58
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.key = key;
			this.nonce = nonce;
			this.macSize = macSize;
			this.associatedText = associatedText;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00058C7D File Offset: 0x00057C7D
		public virtual KeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00058C85 File Offset: 0x00057C85
		public virtual int MacSize
		{
			get
			{
				return this.macSize;
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00058C8D File Offset: 0x00057C8D
		public virtual byte[] GetAssociatedText()
		{
			return this.associatedText;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00058C95 File Offset: 0x00057C95
		public virtual byte[] GetNonce()
		{
			return this.nonce;
		}

		// Token: 0x04000B27 RID: 2855
		private readonly byte[] associatedText;

		// Token: 0x04000B28 RID: 2856
		private readonly byte[] nonce;

		// Token: 0x04000B29 RID: 2857
		private readonly KeyParameter key;

		// Token: 0x04000B2A RID: 2858
		private readonly int macSize;
	}
}
