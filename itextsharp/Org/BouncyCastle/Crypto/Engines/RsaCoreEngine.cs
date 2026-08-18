using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200034F RID: 847
	internal class RsaCoreEngine
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x000B66CC File Offset: 0x000B56CC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (!(parameters is RsaKeyParameters))
			{
				throw new InvalidKeyException("Not an RSA key");
			}
			this.key = (RsaKeyParameters)parameters;
			this.forEncryption = forEncryption;
			this.bitSize = this.key.Modulus.BitLength;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000B672A File Offset: 0x000B572A
		public int GetInputBlockSize()
		{
			if (this.forEncryption)
			{
				return (this.bitSize - 1) / 8;
			}
			return (this.bitSize + 7) / 8;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000B6749 File Offset: 0x000B5749
		public int GetOutputBlockSize()
		{
			if (this.forEncryption)
			{
				return (this.bitSize + 7) / 8;
			}
			return (this.bitSize - 1) / 8;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000B6768 File Offset: 0x000B5768
		public BigInteger ConvertInput(byte[] inBuf, int inOff, int inLen)
		{
			int num = (this.bitSize + 7) / 8;
			if (inLen > num)
			{
				throw new DataLengthException("input too large for RSA cipher.");
			}
			BigInteger bigInteger = new BigInteger(1, inBuf, inOff, inLen);
			if (bigInteger.CompareTo(this.key.Modulus) >= 0)
			{
				throw new DataLengthException("input too large for RSA cipher.");
			}
			return bigInteger;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000B67BC File Offset: 0x000B57BC
		public byte[] ConvertOutput(BigInteger result)
		{
			byte[] array = result.ToByteArrayUnsigned();
			if (this.forEncryption)
			{
				int outputBlockSize = this.GetOutputBlockSize();
				if (array.Length < outputBlockSize)
				{
					byte[] array2 = new byte[outputBlockSize];
					array.CopyTo(array2, array2.Length - array.Length);
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000B6800 File Offset: 0x000B5800
		public BigInteger ProcessBlock(BigInteger input)
		{
			if (this.key is RsaPrivateCrtKeyParameters)
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)this.key;
				BigInteger p = rsaPrivateCrtKeyParameters.P;
				BigInteger q = rsaPrivateCrtKeyParameters.Q;
				BigInteger dp = rsaPrivateCrtKeyParameters.DP;
				BigInteger dq = rsaPrivateCrtKeyParameters.DQ;
				BigInteger qinv = rsaPrivateCrtKeyParameters.QInv;
				BigInteger bigInteger = input.Remainder(p).ModPow(dp, p);
				BigInteger bigInteger2 = input.Remainder(q).ModPow(dq, q);
				BigInteger bigInteger3 = bigInteger.Subtract(bigInteger2);
				bigInteger3 = bigInteger3.Multiply(qinv);
				bigInteger3 = bigInteger3.Mod(p);
				BigInteger bigInteger4 = bigInteger3.Multiply(q);
				return bigInteger4.Add(bigInteger2);
			}
			return input.ModPow(this.key.Exponent, this.key.Modulus);
		}

		// Token: 0x0400151F RID: 5407
		private RsaKeyParameters key;

		// Token: 0x04001520 RID: 5408
		private bool forEncryption;

		// Token: 0x04001521 RID: 5409
		private int bitSize;
	}
}
