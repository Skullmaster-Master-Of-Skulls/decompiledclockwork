using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000546 RID: 1350
	internal class ReferenceMultiplier : ECMultiplier
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x0011F184 File Offset: 0x0011E184
		public ECPoint Multiply(ECPoint p, BigInteger k, PreCompInfo preCompInfo)
		{
			ECPoint ecpoint = p.Curve.Infinity;
			int bitLength = k.BitLength;
			for (int i = 0; i < bitLength; i++)
			{
				if (k.TestBit(i))
				{
					ecpoint = ecpoint.Add(p);
				}
				p = p.Twice();
			}
			return ecpoint;
		}
	}
}
