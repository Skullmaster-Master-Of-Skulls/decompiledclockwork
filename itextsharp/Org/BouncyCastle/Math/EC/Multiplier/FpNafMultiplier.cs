using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000239 RID: 569
	internal class FpNafMultiplier : ECMultiplier
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x00081F04 File Offset: 0x00080F04
		public ECPoint Multiply(ECPoint p, BigInteger k, PreCompInfo preCompInfo)
		{
			BigInteger bigInteger = k.Multiply(BigInteger.Three);
			ECPoint ecpoint = p.Negate();
			ECPoint ecpoint2 = p;
			for (int i = bigInteger.BitLength - 2; i > 0; i--)
			{
				ecpoint2 = ecpoint2.Twice();
				bool flag = bigInteger.TestBit(i);
				bool flag2 = k.TestBit(i);
				if (flag != flag2)
				{
					ecpoint2 = ecpoint2.Add(flag ? p : ecpoint);
				}
			}
			return ecpoint2;
		}
	}
}
