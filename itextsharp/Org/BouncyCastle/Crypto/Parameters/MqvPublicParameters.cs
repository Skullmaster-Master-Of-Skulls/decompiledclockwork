using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200011E RID: 286
	public class MqvPublicParameters : ICipherParameters
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x00037F57 File Offset: 0x00036F57
		public MqvPublicParameters(ECPublicKeyParameters staticPublicKey, ECPublicKeyParameters ephemeralPublicKey)
		{
			this.staticPublicKey = staticPublicKey;
			this.ephemeralPublicKey = ephemeralPublicKey;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00037F6D File Offset: 0x00036F6D
		public ECPublicKeyParameters StaticPublicKey
		{
			get
			{
				return this.staticPublicKey;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00037F75 File Offset: 0x00036F75
		public ECPublicKeyParameters EphemeralPublicKey
		{
			get
			{
				return this.ephemeralPublicKey;
			}
		}

		// Token: 0x0400087B RID: 2171
		private readonly ECPublicKeyParameters staticPublicKey;

		// Token: 0x0400087C RID: 2172
		private readonly ECPublicKeyParameters ephemeralPublicKey;
	}
}
