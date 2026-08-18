using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement.Srp
{
	// Token: 0x02000478 RID: 1144
	public class Srp6Utilities
	{
		// Token: 0x060026F7 RID: 9975 RVA: 0x000EC4C4 File Offset: 0x000EB4C4
		public static BigInteger CalculateK(IDigest digest, BigInteger N, BigInteger g)
		{
			return Srp6Utilities.HashPaddedPair(digest, N, N, g);
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000EC4CF File Offset: 0x000EB4CF
		public static BigInteger CalculateU(IDigest digest, BigInteger N, BigInteger A, BigInteger B)
		{
			return Srp6Utilities.HashPaddedPair(digest, N, A, B);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000EC4DC File Offset: 0x000EB4DC
		public static BigInteger CalculateX(IDigest digest, BigInteger N, byte[] salt, byte[] identity, byte[] password)
		{
			byte[] array = new byte[digest.GetDigestSize()];
			digest.BlockUpdate(identity, 0, identity.Length);
			digest.Update(58);
			digest.BlockUpdate(password, 0, password.Length);
			digest.DoFinal(array, 0);
			digest.BlockUpdate(salt, 0, salt.Length);
			digest.BlockUpdate(array, 0, array.Length);
			digest.DoFinal(array, 0);
			return new BigInteger(1, array).Mod(N);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000EC54C File Offset: 0x000EB54C
		public static BigInteger GeneratePrivateValue(IDigest digest, BigInteger N, BigInteger g, SecureRandom random)
		{
			int num = Math.Min(256, N.BitLength / 2);
			BigInteger min = BigInteger.One.ShiftLeft(num - 1);
			BigInteger max = N.Subtract(BigInteger.One);
			return BigIntegers.CreateRandomInRange(min, max, random);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000EC58E File Offset: 0x000EB58E
		public static BigInteger ValidatePublicValue(BigInteger N, BigInteger val)
		{
			val = val.Mod(N);
			if (val.Equals(BigInteger.Zero))
			{
				throw new CryptoException("Invalid public value: 0");
			}
			return val;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000EC5B4 File Offset: 0x000EB5B4
		private static BigInteger HashPaddedPair(IDigest digest, BigInteger N, BigInteger n1, BigInteger n2)
		{
			int length = (N.BitLength + 7) / 8;
			byte[] padded = Srp6Utilities.GetPadded(n1, length);
			byte[] padded2 = Srp6Utilities.GetPadded(n2, length);
			digest.BlockUpdate(padded, 0, padded.Length);
			digest.BlockUpdate(padded2, 0, padded2.Length);
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return new BigInteger(1, array).Mod(N);
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000EC614 File Offset: 0x000EB614
		private static byte[] GetPadded(BigInteger n, int length)
		{
			byte[] array = BigIntegers.AsUnsignedByteArray(n);
			if (array.Length < length)
			{
				byte[] array2 = new byte[length];
				Array.Copy(array, 0, array2, length - array.Length, array.Length);
				array = array2;
			}
			return array;
		}
	}
}
