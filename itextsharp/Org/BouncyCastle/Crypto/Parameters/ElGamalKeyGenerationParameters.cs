using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002F1 RID: 753
	public class ElGamalKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06001BB6 RID: 7094 RVA: 0x000A5F61 File Offset: 0x000A4F61
		public ElGamalKeyGenerationParameters(SecureRandom random, ElGamalParameters parameters) : base(random, ElGamalKeyGenerationParameters.GetStrength(parameters))
		{
			this.parameters = parameters;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x000A5F77 File Offset: 0x000A4F77
		public ElGamalParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000A5F7F File Offset: 0x000A4F7F
		internal static int GetStrength(ElGamalParameters parameters)
		{
			if (parameters.L == 0)
			{
				return parameters.P.BitLength;
			}
			return parameters.L;
		}

		// Token: 0x04001303 RID: 4867
		private readonly ElGamalParameters parameters;
	}
}
