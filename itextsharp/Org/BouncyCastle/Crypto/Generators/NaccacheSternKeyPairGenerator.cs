using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000616 RID: 1558
	public class NaccacheSternKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06003502 RID: 13570 RVA: 0x00148E18 File Offset: 0x00147E18
		public void Init(KeyGenerationParameters parameters)
		{
			this.param = (NaccacheSternKeyGenerationParameters)parameters;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x00148E28 File Offset: 0x00147E28
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			int strength = this.param.Strength;
			SecureRandom random = this.param.Random;
			int certainty = this.param.Certainty;
			bool isDebug = this.param.IsDebug;
			if (isDebug)
			{
				Console.WriteLine("Fetching first " + this.param.CountSmallPrimes + " primes.");
			}
			ArrayList arrayList = NaccacheSternKeyPairGenerator.findFirstPrimes(this.param.CountSmallPrimes);
			arrayList = NaccacheSternKeyPairGenerator.permuteList(arrayList, random);
			BigInteger bigInteger = BigInteger.One;
			BigInteger bigInteger2 = BigInteger.One;
			for (int i = 0; i < arrayList.Count / 2; i++)
			{
				bigInteger = bigInteger.Multiply((BigInteger)arrayList[i]);
			}
			for (int j = arrayList.Count / 2; j < arrayList.Count; j++)
			{
				bigInteger2 = bigInteger2.Multiply((BigInteger)arrayList[j]);
			}
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger2);
			int num = strength - bigInteger3.BitLength - 48;
			BigInteger bigInteger4 = NaccacheSternKeyPairGenerator.generatePrime(num / 2 + 1, certainty, random);
			BigInteger bigInteger5 = NaccacheSternKeyPairGenerator.generatePrime(num / 2 + 1, certainty, random);
			long num2 = 0L;
			if (isDebug)
			{
				Console.WriteLine("generating p and q");
			}
			BigInteger val = bigInteger4.Multiply(bigInteger).ShiftLeft(1);
			BigInteger val2 = bigInteger5.Multiply(bigInteger2).ShiftLeft(1);
			BigInteger bigInteger6;
			BigInteger bigInteger7;
			BigInteger bigInteger8;
			BigInteger bigInteger9;
			for (;;)
			{
				num2 += 1L;
				bigInteger6 = NaccacheSternKeyPairGenerator.generatePrime(24, certainty, random);
				bigInteger7 = bigInteger6.Multiply(val).Add(BigInteger.One);
				if (bigInteger7.IsProbablePrime(certainty))
				{
					for (;;)
					{
						bigInteger8 = NaccacheSternKeyPairGenerator.generatePrime(24, certainty, random);
						if (!bigInteger6.Equals(bigInteger8))
						{
							bigInteger9 = bigInteger8.Multiply(val2).Add(BigInteger.One);
							if (bigInteger9.IsProbablePrime(certainty))
							{
								break;
							}
						}
					}
					if (!bigInteger3.Gcd(bigInteger6.Multiply(bigInteger8)).Equals(BigInteger.One))
					{
						Console.WriteLine(string.Concat(new object[]
						{
							"sigma.gcd(_p.mult(_q)) != 1!\n _p: ",
							bigInteger6,
							"\n _q: ",
							bigInteger8
						}));
					}
					else
					{
						if (bigInteger7.Multiply(bigInteger9).BitLength >= strength)
						{
							break;
						}
						if (isDebug)
						{
							Console.WriteLine(string.Concat(new object[]
							{
								"key size too small. Should be ",
								strength,
								" but is actually ",
								bigInteger7.Multiply(bigInteger9).BitLength
							}));
						}
					}
				}
			}
			if (isDebug)
			{
				Console.WriteLine("needed " + num2 + " tries to generate p and q.");
			}
			BigInteger bigInteger10 = bigInteger7.Multiply(bigInteger9);
			BigInteger bigInteger11 = bigInteger7.Subtract(BigInteger.One).Multiply(bigInteger9.Subtract(BigInteger.One));
			num2 = 0L;
			if (isDebug)
			{
				Console.WriteLine("generating g");
			}
			BigInteger bigInteger12;
			for (;;)
			{
				ArrayList arrayList2 = new ArrayList();
				for (int num3 = 0; num3 != arrayList.Count; num3++)
				{
					BigInteger val3 = (BigInteger)arrayList[num3];
					BigInteger exponent = bigInteger11.Divide(val3);
					do
					{
						num2 += 1L;
						bigInteger12 = NaccacheSternKeyPairGenerator.generatePrime(strength, certainty, random);
					}
					while (bigInteger12.ModPow(exponent, bigInteger10).Equals(BigInteger.One));
					arrayList2.Add(bigInteger12);
				}
				bigInteger12 = BigInteger.One;
				for (int k = 0; k < arrayList.Count; k++)
				{
					BigInteger bigInteger13 = (BigInteger)arrayList2[k];
					BigInteger val4 = (BigInteger)arrayList[k];
					bigInteger12 = bigInteger12.Multiply(bigInteger13.ModPow(bigInteger3.Divide(val4), bigInteger10)).Mod(bigInteger10);
				}
				bool flag = false;
				for (int l = 0; l < arrayList.Count; l++)
				{
					if (bigInteger12.ModPow(bigInteger11.Divide((BigInteger)arrayList[l]), bigInteger10).Equals(BigInteger.One))
					{
						if (isDebug)
						{
							Console.WriteLine(string.Concat(new object[]
							{
								"g has order phi(n)/",
								arrayList[l],
								"\n g: ",
								bigInteger12
							}));
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (bigInteger12.ModPow(bigInteger11.ShiftRight(2), bigInteger10).Equals(BigInteger.One))
					{
						if (isDebug)
						{
							Console.WriteLine("g has order phi(n)/4\n g:" + bigInteger12);
						}
					}
					else if (bigInteger12.ModPow(bigInteger11.Divide(bigInteger6), bigInteger10).Equals(BigInteger.One))
					{
						if (isDebug)
						{
							Console.WriteLine("g has order phi(n)/p'\n g: " + bigInteger12);
						}
					}
					else if (bigInteger12.ModPow(bigInteger11.Divide(bigInteger8), bigInteger10).Equals(BigInteger.One))
					{
						if (isDebug)
						{
							Console.WriteLine("g has order phi(n)/q'\n g: " + bigInteger12);
						}
					}
					else if (bigInteger12.ModPow(bigInteger11.Divide(bigInteger4), bigInteger10).Equals(BigInteger.One))
					{
						if (isDebug)
						{
							Console.WriteLine("g has order phi(n)/a\n g: " + bigInteger12);
						}
					}
					else
					{
						if (!bigInteger12.ModPow(bigInteger11.Divide(bigInteger5), bigInteger10).Equals(BigInteger.One))
						{
							break;
						}
						if (isDebug)
						{
							Console.WriteLine("g has order phi(n)/b\n g: " + bigInteger12);
						}
					}
				}
			}
			if (isDebug)
			{
				Console.WriteLine("needed " + num2 + " tries to generate g");
				Console.WriteLine();
				Console.WriteLine("found new NaccacheStern cipher variables:");
				Console.WriteLine("smallPrimes: " + Arrays.ToString(arrayList.ToArray()));
				Console.WriteLine(string.Concat(new object[]
				{
					"sigma:...... ",
					bigInteger3,
					" (",
					bigInteger3.BitLength,
					" bits)"
				}));
				Console.WriteLine("a:.......... " + bigInteger4);
				Console.WriteLine("b:.......... " + bigInteger5);
				Console.WriteLine("p':......... " + bigInteger6);
				Console.WriteLine("q':......... " + bigInteger8);
				Console.WriteLine("p:.......... " + bigInteger7);
				Console.WriteLine("q:.......... " + bigInteger9);
				Console.WriteLine("n:.......... " + bigInteger10);
				Console.WriteLine("phi(n):..... " + bigInteger11);
				Console.WriteLine("g:.......... " + bigInteger12);
				Console.WriteLine();
			}
			return new AsymmetricCipherKeyPair(new NaccacheSternKeyParameters(false, bigInteger12, bigInteger10, bigInteger3.BitLength), new NaccacheSternPrivateKeyParameters(bigInteger12, bigInteger10, bigInteger3.BitLength, arrayList, bigInteger11));
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x001494C6 File Offset: 0x001484C6
		private static BigInteger generatePrime(int bitLength, int certainty, SecureRandom rand)
		{
			return new BigInteger(bitLength, certainty, rand);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x001494D0 File Offset: 0x001484D0
		private static ArrayList permuteList(ArrayList arr, SecureRandom rand)
		{
			ArrayList arrayList = new ArrayList(arr.Count);
			foreach (object value in arr)
			{
				int index = rand.Next(arrayList.Count + 1);
				arrayList.Insert(index, value);
			}
			return arrayList;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x00149540 File Offset: 0x00148540
		private static ArrayList findFirstPrimes(int count)
		{
			ArrayList arrayList = new ArrayList(count);
			for (int num = 0; num != count; num++)
			{
				arrayList.Add(BigInteger.ValueOf((long)NaccacheSternKeyPairGenerator.smallPrimes[num]));
			}
			return arrayList;
		}

		// Token: 0x0400237A RID: 9082
		private static readonly int[] smallPrimes = new int[]
		{
			3,
			5,
			7,
			11,
			13,
			17,
			19,
			23,
			29,
			31,
			37,
			41,
			43,
			47,
			53,
			59,
			61,
			67,
			71,
			73,
			79,
			83,
			89,
			97,
			101,
			103,
			107,
			109,
			113,
			127,
			131,
			137,
			139,
			149,
			151,
			157,
			163,
			167,
			173,
			179,
			181,
			191,
			193,
			197,
			199,
			211,
			223,
			227,
			229,
			233,
			239,
			241,
			251,
			257,
			263,
			269,
			271,
			277,
			281,
			283,
			293,
			307,
			311,
			313,
			317,
			331,
			337,
			347,
			349,
			353,
			359,
			367,
			373,
			379,
			383,
			389,
			397,
			401,
			409,
			419,
			421,
			431,
			433,
			439,
			443,
			449,
			457,
			461,
			463,
			467,
			479,
			487,
			491,
			499,
			503,
			509,
			521,
			523,
			541,
			547,
			557
		};

		// Token: 0x0400237B RID: 9083
		private NaccacheSternKeyGenerationParameters param;
	}
}
