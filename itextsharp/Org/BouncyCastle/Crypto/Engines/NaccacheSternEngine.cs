using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000192 RID: 402
	public class NaccacheSternEngine : IAsymmetricBlockCipher
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00059D85 File Offset: 0x00058D85
		public string AlgorithmName
		{
			get
			{
				return "NaccacheStern";
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00059D8C File Offset: 0x00058D8C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.key = (NaccacheSternKeyParameters)parameters;
			if (!this.forEncryption)
			{
				if (this.debug)
				{
					Console.WriteLine("Constructing lookup Array");
				}
				NaccacheSternPrivateKeyParameters naccacheSternPrivateKeyParameters = (NaccacheSternPrivateKeyParameters)this.key;
				ArrayList smallPrimes = naccacheSternPrivateKeyParameters.SmallPrimes;
				this.lookup = new ArrayList[smallPrimes.Count];
				for (int i = 0; i < smallPrimes.Count; i++)
				{
					BigInteger bigInteger = (BigInteger)smallPrimes[i];
					int intValue = bigInteger.IntValue;
					this.lookup[i] = new ArrayList(intValue);
					this.lookup[i].Add(BigInteger.One);
					if (this.debug)
					{
						Console.WriteLine("Constructing lookup ArrayList for " + intValue);
					}
					BigInteger bigInteger2 = BigInteger.Zero;
					for (int j = 1; j < intValue; j++)
					{
						bigInteger2 = bigInteger2.Add(naccacheSternPrivateKeyParameters.PhiN);
						BigInteger exponent = bigInteger2.Divide(bigInteger);
						this.lookup[i].Add(naccacheSternPrivateKeyParameters.G.ModPow(exponent, naccacheSternPrivateKeyParameters.Modulus));
					}
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00059EC0 File Offset: 0x00058EC0
		public bool Debug
		{
			set
			{
				this.debug = value;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00059EC9 File Offset: 0x00058EC9
		public int GetInputBlockSize()
		{
			if (this.forEncryption)
			{
				return (this.key.LowerSigmaBound + 7) / 8 - 1;
			}
			return this.key.Modulus.BitLength / 8 + 1;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00059EF9 File Offset: 0x00058EF9
		public int GetOutputBlockSize()
		{
			if (this.forEncryption)
			{
				return this.key.Modulus.BitLength / 8 + 1;
			}
			return (this.key.LowerSigmaBound + 7) / 8 - 1;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00059F2C File Offset: 0x00058F2C
		public byte[] ProcessBlock(byte[] inBytes, int inOff, int length)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("NaccacheStern engine not initialised");
			}
			if (length > this.GetInputBlockSize() + 1)
			{
				throw new DataLengthException("input too large for Naccache-Stern cipher.\n");
			}
			if (!this.forEncryption && length < this.GetInputBlockSize())
			{
				throw new InvalidCipherTextException("BlockLength does not match modulus for Naccache-Stern cipher.\n");
			}
			BigInteger bigInteger = new BigInteger(1, inBytes, inOff, length);
			if (this.debug)
			{
				Console.WriteLine("input as BigInteger: " + bigInteger);
			}
			byte[] result;
			if (this.forEncryption)
			{
				result = this.Encrypt(bigInteger);
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				NaccacheSternPrivateKeyParameters naccacheSternPrivateKeyParameters = (NaccacheSternPrivateKeyParameters)this.key;
				ArrayList smallPrimes = naccacheSternPrivateKeyParameters.SmallPrimes;
				for (int i = 0; i < smallPrimes.Count; i++)
				{
					BigInteger bigInteger2 = bigInteger.ModPow(naccacheSternPrivateKeyParameters.PhiN.Divide((BigInteger)smallPrimes[i]), naccacheSternPrivateKeyParameters.Modulus);
					ArrayList arrayList2 = this.lookup[i];
					if (this.lookup[i].Count != ((BigInteger)smallPrimes[i]).IntValue)
					{
						if (this.debug)
						{
							Console.WriteLine(string.Concat(new object[]
							{
								"Prime is ",
								smallPrimes[i],
								", lookup table has size ",
								arrayList2.Count
							}));
						}
						throw new InvalidCipherTextException(string.Concat(new object[]
						{
							"Error in lookup Array for ",
							((BigInteger)smallPrimes[i]).IntValue,
							": Size mismatch. Expected ArrayList with length ",
							((BigInteger)smallPrimes[i]).IntValue,
							" but found ArrayList of length ",
							this.lookup[i].Count
						}));
					}
					int num = arrayList2.IndexOf(bigInteger2);
					if (num == -1)
					{
						if (this.debug)
						{
							Console.WriteLine("Actual prime is " + smallPrimes[i]);
							Console.WriteLine("Decrypted value is " + bigInteger2);
							Console.WriteLine(string.Concat(new object[]
							{
								"LookupList for ",
								smallPrimes[i],
								" with size ",
								this.lookup[i].Count,
								" is: "
							}));
							for (int j = 0; j < this.lookup[i].Count; j++)
							{
								Console.WriteLine(this.lookup[i][j]);
							}
						}
						throw new InvalidCipherTextException("Lookup failed");
					}
					arrayList.Add(BigInteger.ValueOf((long)num));
				}
				BigInteger bigInteger3 = NaccacheSternEngine.chineseRemainder(arrayList, smallPrimes);
				result = bigInteger3.ToByteArray();
			}
			return result;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0005A208 File Offset: 0x00059208
		public byte[] Encrypt(BigInteger plain)
		{
			byte[] array = new byte[this.key.Modulus.BitLength / 8 + 1];
			byte[] array2 = this.key.G.ModPow(plain, this.key.Modulus).ToByteArray();
			Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
			if (this.debug)
			{
				Console.WriteLine("Encrypted value is:  " + new BigInteger(array));
			}
			return array;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0005A284 File Offset: 0x00059284
		public byte[] AddCryptedBlocks(byte[] block1, byte[] block2)
		{
			if (this.forEncryption)
			{
				if (block1.Length > this.GetOutputBlockSize() || block2.Length > this.GetOutputBlockSize())
				{
					throw new InvalidCipherTextException("BlockLength too large for simple addition.\n");
				}
			}
			else if (block1.Length > this.GetInputBlockSize() || block2.Length > this.GetInputBlockSize())
			{
				throw new InvalidCipherTextException("BlockLength too large for simple addition.\n");
			}
			BigInteger bigInteger = new BigInteger(1, block1);
			BigInteger bigInteger2 = new BigInteger(1, block2);
			BigInteger bigInteger3 = bigInteger.Multiply(bigInteger2);
			bigInteger3 = bigInteger3.Mod(this.key.Modulus);
			if (this.debug)
			{
				Console.WriteLine("c(m1) as BigInteger:....... " + bigInteger);
				Console.WriteLine("c(m2) as BigInteger:....... " + bigInteger2);
				Console.WriteLine("c(m1)*c(m2)%n = c(m1+m2)%n: " + bigInteger3);
			}
			byte[] array = new byte[this.key.Modulus.BitLength / 8 + 1];
			byte[] array2 = bigInteger3.ToByteArray();
			Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
			return array;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0005A378 File Offset: 0x00059378
		public byte[] ProcessData(byte[] data)
		{
			if (this.debug)
			{
				Console.WriteLine();
			}
			if (data.Length > this.GetInputBlockSize())
			{
				int inputBlockSize = this.GetInputBlockSize();
				int outputBlockSize = this.GetOutputBlockSize();
				if (this.debug)
				{
					Console.WriteLine("Input blocksize is:  " + inputBlockSize + " bytes");
					Console.WriteLine("Output blocksize is: " + outputBlockSize + " bytes");
					Console.WriteLine("Data has length:.... " + data.Length + " bytes");
				}
				int i = 0;
				int num = 0;
				byte[] array = new byte[(data.Length / inputBlockSize + 1) * outputBlockSize];
				while (i < data.Length)
				{
					byte[] array2;
					if (i + inputBlockSize < data.Length)
					{
						array2 = this.ProcessBlock(data, i, inputBlockSize);
						i += inputBlockSize;
					}
					else
					{
						array2 = this.ProcessBlock(data, i, data.Length - i);
						i += data.Length - i;
					}
					if (this.debug)
					{
						Console.WriteLine("new datapos is " + i);
					}
					if (array2 == null)
					{
						if (this.debug)
						{
							Console.WriteLine("cipher returned null");
						}
						throw new InvalidCipherTextException("cipher returned null");
					}
					array2.CopyTo(array, num);
					num += array2.Length;
				}
				byte[] array3 = new byte[num];
				Array.Copy(array, 0, array3, 0, num);
				if (this.debug)
				{
					Console.WriteLine("returning " + array3.Length + " bytes");
				}
				return array3;
			}
			if (this.debug)
			{
				Console.WriteLine("data size is less then input block size, processing directly");
			}
			return this.ProcessBlock(data, 0, data.Length);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0005A504 File Offset: 0x00059504
		private static BigInteger chineseRemainder(ArrayList congruences, ArrayList primes)
		{
			BigInteger bigInteger = BigInteger.Zero;
			BigInteger bigInteger2 = BigInteger.One;
			for (int i = 0; i < primes.Count; i++)
			{
				bigInteger2 = bigInteger2.Multiply((BigInteger)primes[i]);
			}
			for (int j = 0; j < primes.Count; j++)
			{
				BigInteger bigInteger3 = (BigInteger)primes[j];
				BigInteger bigInteger4 = bigInteger2.Divide(bigInteger3);
				BigInteger val = bigInteger4.ModInverse(bigInteger3);
				BigInteger bigInteger5 = bigInteger4.Multiply(val);
				bigInteger5 = bigInteger5.Multiply((BigInteger)congruences[j]);
				bigInteger = bigInteger.Add(bigInteger5);
			}
			return bigInteger.Mod(bigInteger2);
		}

		// Token: 0x04000B49 RID: 2889
		private bool forEncryption;

		// Token: 0x04000B4A RID: 2890
		private NaccacheSternKeyParameters key;

		// Token: 0x04000B4B RID: 2891
		private ArrayList[] lookup;

		// Token: 0x04000B4C RID: 2892
		private bool debug;
	}
}
