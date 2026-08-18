using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000084 RID: 132
	public class MqvPrivateParameters : ICipherParameters
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x000163D2 File Offset: 0x000153D2
		public MqvPrivateParameters(ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey) : this(staticPrivateKey, ephemeralPrivateKey, null)
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000163DD File Offset: 0x000153DD
		public MqvPrivateParameters(ECPrivateKeyParameters staticPrivateKey, ECPrivateKeyParameters ephemeralPrivateKey, ECPublicKeyParameters ephemeralPublicKey)
		{
			this.staticPrivateKey = staticPrivateKey;
			this.ephemeralPrivateKey = ephemeralPrivateKey;
			this.ephemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000163FA File Offset: 0x000153FA
		public ECPrivateKeyParameters StaticPrivateKey
		{
			get
			{
				return this.staticPrivateKey;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00016402 File Offset: 0x00015402
		public ECPrivateKeyParameters EphemeralPrivateKey
		{
			get
			{
				return this.ephemeralPrivateKey;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001640A File Offset: 0x0001540A
		public ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x0400021B RID: 539
		private readonly ECPrivateKeyParameters staticPrivateKey;

		// Token: 0x0400021C RID: 540
		private readonly ECPrivateKeyParameters ephemeralPrivateKey;

		// Token: 0x0400021D RID: 541
		private readonly ECPublicKeyParameters ephemeralPublicKey;
	}
}
