using System;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000609 RID: 1545
	public class PgpKeyPair
	{
		// Token: 0x060034A3 RID: 13475 RVA: 0x00147AF0 File Offset: 0x00146AF0
		public PgpKeyPair(PublicKeyAlgorithmTag algorithm, AsymmetricCipherKeyPair keyPair, DateTime time) : this(algorithm, keyPair.Public, keyPair.Private, time)
		{
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x00147B06 File Offset: 0x00146B06
		public PgpKeyPair(PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, AsymmetricKeyParameter privKey, DateTime time)
		{
			this.pub = new PgpPublicKey(algorithm, pubKey, time);
			this.priv = new PgpPrivateKey(privKey, this.pub.KeyId);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x00147B34 File Offset: 0x00146B34
		public PgpKeyPair(PgpPublicKey pub, PgpPrivateKey priv)
		{
			this.pub = pub;
			this.priv = priv;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x00147B4A File Offset: 0x00146B4A
		public long KeyId
		{
			get
			{
				return this.pub.KeyId;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x00147B57 File Offset: 0x00146B57
		public PgpPublicKey PublicKey
		{
			get
			{
				return this.pub;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060034A8 RID: 13480 RVA: 0x00147B5F File Offset: 0x00146B5F
		public PgpPrivateKey PrivateKey
		{
			get
			{
				return this.priv;
			}
		}

		// Token: 0x0400235E RID: 9054
		private readonly PgpPublicKey pub;

		// Token: 0x0400235F RID: 9055
		private readonly PgpPrivateKey priv;
	}
}
