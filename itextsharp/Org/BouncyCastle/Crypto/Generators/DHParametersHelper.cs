using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200008E RID: 142
	internal class DHParametersHelper
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00017CD4 File Offset: 0x00016CD4
		static DHParametersHelper()
		{
			for (int i = 0; i < DHParametersHelper.primeLists.Length; i++)
			{
				int[] array = DHParametersHelper.primeLists[i];
				int num = 1;
				for (int j = 0; j < array.Length; j++)
				{
					num *= array[j];
				}
				DHParametersHelper.primeProducts[i] = num;
				DHParametersHelper.PrimeProducts[i] = BigInteger.ValueOf((long)num);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000181D4 File Offset: 0x000171D4
		internal static BigInteger[] GenerateSafePrimes(int size, int certainty, SecureRandom random)
		{
			int num = size - 1;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			if (size <= 32)
			{
				for (;;)
				{
					bigInteger = new BigInteger(num, 2, random);
					bigInteger2 = bigInteger.ShiftLeft(1).Add(BigInteger.One);
					if (bigInteger2.IsProbablePrime(certainty))
					{
						if (certainty <= 2)
						{
							break;
						}
						if (bigInteger.IsProbablePrime(certainty))
						{
							break;
						}
					}
				}
			}
			else
			{
				for (;;)
				{
					bigInteger = new BigInteger(num, 0, random);
					for (;;)
					{
						IL_4B:
						for (int i = 0; i < DHParametersHelper.primeLists.Length; i++)
						{
							int num2 = bigInteger.Remainder(DHParametersHelper.PrimeProducts[i]).IntValue;
							if (i == 0)
							{
								int num3 = num2 % 3;
								if (num3 != 2)
								{
									int num4 = 2 * num3 + 2;
									bigInteger = bigInteger.Add(BigInteger.ValueOf((long)num4));
									num2 = (num2 + num4) % DHParametersHelper.primeProducts[i];
								}
							}
							foreach (int num5 in DHParametersHelper.primeLists[i])
							{
								int num6 = num2 % num5;
								if (num6 == 0 || num6 == num5 >> 1)
								{
									bigInteger = bigInteger.Add(DHParametersHelper.Six);
									goto IL_4B;
								}
							}
						}
						break;
					}
					if (bigInteger.BitLength == num && bigInteger.RabinMillerTest(2, random))
					{
						bigInteger2 = bigInteger.ShiftLeft(1).Add(BigInteger.One);
						if (bigInteger2.RabinMillerTest(certainty, random) && (certainty <= 2 || bigInteger.RabinMillerTest(certainty - 2, random)))
						{
							break;
						}
					}
				}
			}
			return new BigInteger[]
			{
				bigInteger2,
				bigInteger
			};
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00018334 File Offset: 0x00017334
		internal static BigInteger SelectGenerator(BigInteger p, BigInteger q, SecureRandom random)
		{
			BigInteger max = p.Subtract(BigInteger.Two);
			BigInteger bigInteger;
			do
			{
				bigInteger = BigIntegers.CreateRandomInRange(BigInteger.Two, max, random);
			}
			while (bigInteger.ModPow(BigInteger.Two, p).Equals(BigInteger.One) || bigInteger.ModPow(q, p).Equals(BigInteger.One));
			return bigInteger;
		}

		// Token: 0x04000232 RID: 562
		private static readonly int[][] primeLists = new int[][]
		{
			new int[]
			{
				3,
				5,
				7,
				11,
				13,
				17,
				19,
				23
			},
			new int[]
			{
				29,
				31,
				37,
				41,
				43
			},
			new int[]
			{
				47,
				53,
				59,
				61,
				67
			},
			new int[]
			{
				71,
				73,
				79,
				83
			},
			new int[]
			{
				89,
				97,
				101,
				103
			},
			new int[]
			{
				107,
				109,
				113,
				127
			},
			new int[]
			{
				131,
				137,
				139,
				149
			},
			new int[]
			{
				151,
				157,
				163,
				167
			},
			new int[]
			{
				173,
				179,
				181,
				191
			},
			new int[]
			{
				193,
				197,
				199,
				211
			},
			new int[]
			{
				223,
				227,
				229
			},
			new int[]
			{
				233,
				239,
				241
			},
			new int[]
			{
				251,
				257,
				263
			},
			new int[]
			{
				269,
				271,
				277
			},
			new int[]
			{
				281,
				283,
				293
			},
			new int[]
			{
				307,
				311,
				313
			},
			new int[]
			{
				317,
				331,
				337
			},
			new int[]
			{
				347,
				349,
				353
			},
			new int[]
			{
				359,
				367,
				373
			},
			new int[]
			{
				379,
				383,
				389
			},
			new int[]
			{
				397,
				401,
				409
			},
			new int[]
			{
				419,
				421,
				431
			},
			new int[]
			{
				433,
				439,
				443
			},
			new int[]
			{
				449,
				457,
				461
			},
			new int[]
			{
				463,
				467,
				479
			},
			new int[]
			{
				487,
				491,
				499
			},
			new int[]
			{
				503,
				509,
				521
			},
			new int[]
			{
				523,
				541,
				547
			},
			new int[]
			{
				557,
				563,
				569
			},
			new int[]
			{
				571,
				577,
				587
			},
			new int[]
			{
				593,
				599,
				601
			},
			new int[]
			{
				607,
				613,
				617
			},
			new int[]
			{
				619,
				631,
				641
			},
			new int[]
			{
				643,
				647,
				653
			},
			new int[]
			{
				659,
				661,
				673
			},
			new int[]
			{
				677,
				683,
				691
			},
			new int[]
			{
				701,
				709,
				719
			},
			new int[]
			{
				727,
				733,
				739
			},
			new int[]
			{
				743,
				751,
				757
			},
			new int[]
			{
				761,
				769,
				773
			},
			new int[]
			{
				787,
				797,
				809
			},
			new int[]
			{
				811,
				821,
				823
			},
			new int[]
			{
				827,
				829,
				839
			},
			new int[]
			{
				853,
				857,
				859
			},
			new int[]
			{
				863,
				877,
				881
			},
			new int[]
			{
				883,
				887,
				907
			},
			new int[]
			{
				911,
				919,
				929
			},
			new int[]
			{
				937,
				941,
				947
			},
			new int[]
			{
				953,
				967,
				971
			},
			new int[]
			{
				977,
				983,
				991
			},
			new int[]
			{
				997,
				1009,
				1013
			},
			new int[]
			{
				1019,
				1021,
				1031
			}
		};

		// Token: 0x04000233 RID: 563
		private static readonly BigInteger Six = BigInteger.ValueOf(6L);

		// Token: 0x04000234 RID: 564
		private static readonly int[] primeProducts = new int[DHParametersHelper.primeLists.Length];

		// Token: 0x04000235 RID: 565
		private static readonly BigInteger[] PrimeProducts = new BigInteger[DHParametersHelper.primeLists.Length];
	}
}
