using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020005AE RID: 1454
	public class AsymmetricCipherKeyPair
	{
		// Token: 0x06003236 RID: 12854 RVA: 0x00138504 File Offset: 0x00137504
		public AsymmetricCipherKeyPair(AsymmetricKeyParameter publicParameter, AsymmetricKeyParameter privateParameter)
		{
			if (publicParameter.IsPrivate)
			{
				throw new ArgumentException("Expected a public key", "publicParameter");
			}
			if (!privateParameter.IsPrivate)
			{
				throw new ArgumentException("Expected a private key", "privateParameter");
			}
			this.publicParameter = publicParameter;
			this.privateParameter = privateParameter;
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x00138555 File Offset: 0x00137555
		public AsymmetricKeyParameter Public
		{
			get
			{
				return this.publicParameter;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x0013855D File Offset: 0x0013755D
		public AsymmetricKeyParameter Private
		{
			get
			{
				return this.privateParameter;
			}
		}

		// Token: 0x0400226B RID: 8811
		private readonly AsymmetricKeyParameter publicParameter;

		// Token: 0x0400226C RID: 8812
		private readonly AsymmetricKeyParameter privateParameter;
	}
}
