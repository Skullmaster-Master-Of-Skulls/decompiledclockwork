using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020004C5 RID: 1221
	internal class DHKeyGeneratorHelper
	{
		// Token: 0x0600299E RID: 10654 RVA: 0x000FD306 File Offset: 0x000FC306
		private DHKeyGeneratorHelper()
		{
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000FD310 File Offset: 0x000FC310
		internal BigInteger CalculatePrivate(DHParameters dhParams, SecureRandom random)
		{
			int l = dhParams.L;
			if (l != 0)
			{
				return new BigInteger(l, random).SetBit(l - 1);
			}
			BigInteger min = BigInteger.Two;
			int m = dhParams.M;
			if (m != 0)
			{
				min = BigInteger.One.ShiftLeft(m - 1);
			}
			BigInteger max = dhParams.P.Subtract(BigInteger.Two);
			BigInteger q = dhParams.Q;
			if (q != null)
			{
				max = q.Subtract(BigInteger.Two);
			}
			return BigIntegers.CreateRandomInRange(min, max, random);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000FD387 File Offset: 0x000FC387
		internal BigInteger CalculatePublic(DHParameters dhParams, BigInteger x)
		{
			return dhParams.G.ModPow(x, dhParams.P);
		}

		// Token: 0x04001D07 RID: 7431
		internal static readonly DHKeyGeneratorHelper Instance = new DHKeyGeneratorHelper();
	}
}
