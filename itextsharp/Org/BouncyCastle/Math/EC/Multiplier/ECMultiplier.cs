using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000081 RID: 129
	internal interface ECMultiplier
	{
		// Token: 0x0600041A RID: 1050
		ECPoint Multiply(ECPoint p, BigInteger k, PreCompInfo preCompInfo);
	}
}
