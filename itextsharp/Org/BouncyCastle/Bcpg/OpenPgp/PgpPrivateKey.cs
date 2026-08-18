using System;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000606 RID: 1542
	public class PgpPrivateKey
	{
		// Token: 0x06003498 RID: 13464 RVA: 0x001478F9 File Offset: 0x001468F9
		public PgpPrivateKey(AsymmetricKeyParameter privateKey, long keyId)
		{
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("Expected a private key", "privateKey");
			}
			this.privateKey = privateKey;
			this.keyId = keyId;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x00147927 File Offset: 0x00146927
		public long KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x0014792F File Offset: 0x0014692F
		public AsymmetricKeyParameter Key
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x04002356 RID: 9046
		private readonly long keyId;

		// Token: 0x04002357 RID: 9047
		private readonly AsymmetricKeyParameter privateKey;
	}
}
