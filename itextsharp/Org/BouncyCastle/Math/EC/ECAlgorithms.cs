using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000471 RID: 1137
	public class ECAlgorithms
	{
		// Token: 0x060026C7 RID: 9927 RVA: 0x000EAB88 File Offset: 0x000E9B88
		public static ECPoint SumOfTwoMultiplies(ECPoint P, BigInteger a, ECPoint Q, BigInteger b)
		{
			ECCurve curve = P.Curve;
			if (!curve.Equals(Q.Curve))
			{
				throw new ArgumentException("P and Q must be on same curve");
			}
			return ECAlgorithms.ImplShamirsTrick(P, a, Q, b);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000EABBE File Offset: 0x000E9BBE
		public static ECPoint ShamirsTrick(ECPoint P, BigInteger k, ECPoint Q, BigInteger l)
		{
			if (!P.Curve.Equals(Q.Curve))
			{
				throw new ArgumentException("P and Q must be on same curve");
			}
			return ECAlgorithms.ImplShamirsTrick(P, k, Q, l);
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000EABE8 File Offset: 0x000E9BE8
		private static ECPoint ImplShamirsTrick(ECPoint P, BigInteger k, ECPoint Q, BigInteger l)
		{
			int num = Math.Max(k.BitLength, l.BitLength);
			ECPoint b = P.Add(Q);
			ECPoint ecpoint = P.Curve.Infinity;
			for (int i = num - 1; i >= 0; i--)
			{
				ecpoint = ecpoint.Twice();
				if (k.TestBit(i))
				{
					if (l.TestBit(i))
					{
						ecpoint = ecpoint.Add(b);
					}
					else
					{
						ecpoint = ecpoint.Add(P);
					}
				}
				else if (l.TestBit(i))
				{
					ecpoint = ecpoint.Add(Q);
				}
			}
			return ecpoint;
		}
	}
}
