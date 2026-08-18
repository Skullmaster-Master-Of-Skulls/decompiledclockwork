using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020005A8 RID: 1448
	public class DHKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x060031FE RID: 12798 RVA: 0x0013779B File Offset: 0x0013679B
		public DHKeyGenerationParameters(SecureRandom random, DHParameters parameters) : base(random, DHKeyGenerationParameters.GetStrength(parameters))
		{
			this.parameters = parameters;
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x001377B1 File Offset: 0x001367B1
		public DHParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x001377B9 File Offset: 0x001367B9
		internal static int GetStrength(DHParameters parameters)
		{
			if (parameters.L == 0)
			{
				return parameters.P.BitLength;
			}
			return parameters.L;
		}

		// Token: 0x04002257 RID: 8791
		private readonly DHParameters parameters;
	}
}
