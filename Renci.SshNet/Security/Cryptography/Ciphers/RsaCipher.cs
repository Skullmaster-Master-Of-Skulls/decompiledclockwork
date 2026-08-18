using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x0200008B RID: 139
	public class RsaCipher : AsymmetricCipher
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00018D28 File Offset: 0x00016F28
		public RsaCipher(RsaKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._key = key;
			this._isPrivate = !this._key.D.IsZero;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00018D6C File Offset: 0x00016F6C
		public override byte[] Encrypt(byte[] data, int offset, int length)
		{
			int bitLength = this._key.Modulus.BitLength;
			byte[] array = new byte[bitLength / 8 + ((bitLength % 8 > 0) ? 1 : 0) - 1];
			array[0] = 1;
			for (int i = 1; i < array.Length - length - 1; i++)
			{
				array[i] = byte.MaxValue;
			}
			Buffer.BlockCopy(data, offset, array, array.Length - length, length);
			return this.Transform(array);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00018DD8 File Offset: 0x00016FD8
		public override byte[] Decrypt(byte[] data)
		{
			byte[] array = this.Transform(data);
			if (array[0] != 1 && array[0] != 2)
			{
				throw new NotSupportedException("Only block type 01 or 02 are supported.");
			}
			int num = 1;
			while (num < array.Length && array[num] != 0)
			{
				num++;
			}
			num++;
			byte[] array2 = new byte[array.Length - num];
			Buffer.BlockCopy(array, num, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00018E34 File Offset: 0x00017034
		private byte[] Transform(byte[] data)
		{
			Array.Reverse(data);
			byte[] array = new byte[data.Length + 1];
			Buffer.BlockCopy(data, 0, array, 0, data.Length);
			BigInteger bigInteger = new BigInteger(array);
			BigInteger bigInteger4;
			if (this._isPrivate)
			{
				BigInteger bigInteger2 = BigInteger.One;
				BigInteger bigInteger3 = this._key.Modulus - 1;
				int bitLength = this._key.Modulus.BitLength;
				if (bigInteger3 < BigInteger.One)
				{
					throw new SshException("Invalid RSA key.");
				}
				while (bigInteger2 <= BigInteger.One || bigInteger2 >= bigInteger3)
				{
					bigInteger2 = BigInteger.Random(bitLength);
				}
				BigInteger dividend = BigInteger.PositiveMod(BigInteger.ModPow(bigInteger2, this._key.Exponent, this._key.Modulus) * bigInteger, this._key.Modulus);
				BigInteger left = BigInteger.ModPow(dividend % this._key.P, this._key.DP, this._key.P);
				BigInteger right = BigInteger.ModPow(dividend % this._key.Q, this._key.DQ, this._key.Q);
				BigInteger left2 = BigInteger.PositiveMod((left - right) * this._key.InverseQ, this._key.P) * this._key.Q + right;
				BigInteger right2 = BigInteger.ModInverse(bigInteger2, this._key.Modulus);
				bigInteger4 = BigInteger.PositiveMod(left2 * right2, this._key.Modulus);
			}
			else
			{
				bigInteger4 = BigInteger.ModPow(bigInteger, this._key.Exponent, this._key.Modulus);
			}
			return bigInteger4.ToByteArray().Reverse<byte>();
		}

		// Token: 0x040002B3 RID: 691
		private readonly bool _isPrivate;

		// Token: 0x040002B4 RID: 692
		private readonly RsaKey _key;
	}
}
