using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000243 RID: 579
	public class DsaKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x000824D7 File Offset: 0x000814D7
		public DsaKeyGenerationParameters(SecureRandom random, DsaParameters parameters) : base(random, parameters.P.BitLength - 1)
		{
			this.parameters = parameters;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x000824F4 File Offset: 0x000814F4
		public DsaParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04000F55 RID: 3925
		private readonly DsaParameters parameters;
	}
}
