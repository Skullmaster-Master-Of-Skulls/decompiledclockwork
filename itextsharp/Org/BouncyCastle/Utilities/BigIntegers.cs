using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020003CA RID: 970
	public sealed class BigIntegers
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x000CCECE File Offset: 0x000CBECE
		private BigIntegers()
		{
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000CCED6 File Offset: 0x000CBED6
		public static byte[] AsUnsignedByteArray(BigInteger n)
		{
			return n.ToByteArrayUnsigned();
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000CCEE0 File Offset: 0x000CBEE0
		public static BigInteger CreateRandomInRange(BigInteger min, BigInteger max, SecureRandom random)
		{
			int num = min.CompareTo(max);
			if (num >= 0)
			{
				if (num > 0)
				{
					throw new ArgumentException("'min' may not be greater than 'max'");
				}
				return min;
			}
			else
			{
				if (min.BitLength > max.BitLength / 2)
				{
					return BigIntegers.CreateRandomInRange(BigInteger.Zero, max.Subtract(min), random).Add(min);
				}
				for (int i = 0; i < 1000; i++)
				{
					BigInteger bigInteger = new BigInteger(max.BitLength, random);
					if (bigInteger.CompareTo(min) >= 0 && bigInteger.CompareTo(max) <= 0)
					{
						return bigInteger;
					}
				}
				return new BigInteger(max.Subtract(min).BitLength - 1, random).Add(min);
			}
		}

		// Token: 0x04001745 RID: 5957
		private const int MaxIterations = 1000;
	}
}
