using System;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200024B RID: 587
	public class DsaParametersGenerator
	{
		// Token: 0x06001684 RID: 5764 RVA: 0x00082BEE File Offset: 0x00081BEE
		public void Init(int size, int certainty, SecureRandom random)
		{
			if (!DsaParametersGenerator.IsValidDsaStrength(size))
			{
				throw new ArgumentException("size must be from 512 - 1024 and a multiple of 64", "size");
			}
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00082C20 File Offset: 0x00081C20
		private static void Add(byte[] a, byte[] b, int value)
		{
			int num = (int)(b[b.Length - 1] & byte.MaxValue) + value;
			a[b.Length - 1] = (byte)num;
			num = (int)((uint)num >> 8);
			for (int i = b.Length - 2; i >= 0; i--)
			{
				num += (int)(b[i] & byte.MaxValue);
				a[i] = (byte)num;
				num = (int)((uint)num >> 8);
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00082C70 File Offset: 0x00081C70
		public DsaParameters GenerateParameters()
		{
			byte[] array = new byte[20];
			byte[] array2 = new byte[20];
			byte[] array3 = new byte[20];
			byte[] array4 = new byte[20];
			Sha1Digest sha1Digest = new Sha1Digest();
			int num = (this.size - 1) / 160;
			byte[] array5 = new byte[this.size / 8];
			BigInteger bigInteger = null;
			BigInteger bigInteger2 = null;
			int i = 0;
			bool flag = false;
			while (!flag)
			{
				do
				{
					this.random.NextBytes(array);
					sha1Digest.BlockUpdate(array, 0, array.Length);
					sha1Digest.DoFinal(array2, 0);
					Array.Copy(array, 0, array3, 0, array.Length);
					DsaParametersGenerator.Add(array3, array, 1);
					sha1Digest.BlockUpdate(array3, 0, array3.Length);
					sha1Digest.DoFinal(array3, 0);
					for (int num2 = 0; num2 != array4.Length; num2++)
					{
						array4[num2] = (array2[num2] ^ array3[num2]);
					}
					byte[] array6 = array4;
					int num3 = 0;
					array6[num3] |= 128;
					byte[] array7 = array4;
					int num4 = 19;
					array7[num4] |= 1;
					bigInteger = new BigInteger(1, array4);
				}
				while (!bigInteger.IsProbablePrime(this.certainty));
				i = 0;
				int num5 = 2;
				while (i < 4096)
				{
					for (int j = 0; j < num; j++)
					{
						DsaParametersGenerator.Add(array2, array, num5 + j);
						sha1Digest.BlockUpdate(array2, 0, array2.Length);
						sha1Digest.DoFinal(array2, 0);
						Array.Copy(array2, 0, array5, array5.Length - (j + 1) * array2.Length, array2.Length);
					}
					DsaParametersGenerator.Add(array2, array, num5 + num);
					sha1Digest.BlockUpdate(array2, 0, array2.Length);
					sha1Digest.DoFinal(array2, 0);
					Array.Copy(array2, array2.Length - (array5.Length - num * array2.Length), array5, 0, array5.Length - num * array2.Length);
					byte[] array8 = array5;
					int num6 = 0;
					array8[num6] |= 128;
					BigInteger bigInteger3 = new BigInteger(1, array5);
					BigInteger bigInteger4 = bigInteger3.Mod(bigInteger.ShiftLeft(1));
					bigInteger2 = bigInteger3.Subtract(bigInteger4.Subtract(BigInteger.One));
					if (bigInteger2.TestBit(this.size - 1) && bigInteger2.IsProbablePrime(this.certainty))
					{
						flag = true;
						break;
					}
					i++;
					num5 += num + 1;
				}
			}
			BigInteger exponent = bigInteger2.Subtract(BigInteger.One).Divide(bigInteger);
			BigInteger bigInteger6;
			for (;;)
			{
				BigInteger bigInteger5 = new BigInteger(this.size, this.random);
				if (bigInteger5.CompareTo(BigInteger.One) > 0 && bigInteger5.CompareTo(bigInteger2.Subtract(BigInteger.One)) < 0)
				{
					bigInteger6 = bigInteger5.ModPow(exponent, bigInteger2);
					if (bigInteger6.CompareTo(BigInteger.One) > 0)
					{
						break;
					}
				}
			}
			return new DsaParameters(bigInteger2, bigInteger, bigInteger6, new DsaValidationParameters(array, i));
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00082F36 File Offset: 0x00081F36
		private static bool IsValidDsaStrength(int strength)
		{
			return strength >= 512 && strength <= 1024 && strength % 64 == 0;
		}

		// Token: 0x04000F69 RID: 3945
		private int size;

		// Token: 0x04000F6A RID: 3946
		private int certainty;

		// Token: 0x04000F6B RID: 3947
		private SecureRandom random;
	}
}
