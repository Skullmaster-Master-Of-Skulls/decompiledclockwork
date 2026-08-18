using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000434 RID: 1076
	public class ElGamalEngine : IAsymmetricBlockCipher
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000DEDE4 File Offset: 0x000DDDE4
		public string AlgorithmName
		{
			get
			{
				return "ElGamal";
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000DEDEC File Offset: 0x000DDDEC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.key = (ElGamalKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.key = (ElGamalKeyParameters)parameters;
				this.random = new SecureRandom();
			}
			this.forEncryption = forEncryption;
			this.bitSize = this.key.Parameters.P.BitLength;
			if (forEncryption)
			{
				if (!(this.key is ElGamalPublicKeyParameters))
				{
					throw new ArgumentException("ElGamalPublicKeyParameters are required for encryption.");
				}
			}
			else if (!(this.key is ElGamalPrivateKeyParameters))
			{
				throw new ArgumentException("ElGamalPrivateKeyParameters are required for decryption.");
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000DEE93 File Offset: 0x000DDE93
		public int GetInputBlockSize()
		{
			if (this.forEncryption)
			{
				return (this.bitSize - 1) / 8;
			}
			return 2 * ((this.bitSize + 7) / 8);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000DEEB4 File Offset: 0x000DDEB4
		public int GetOutputBlockSize()
		{
			if (this.forEncryption)
			{
				return 2 * ((this.bitSize + 7) / 8);
			}
			return (this.bitSize - 1) / 8;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000DEED8 File Offset: 0x000DDED8
		public byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("ElGamal engine not initialised");
			}
			int num = this.forEncryption ? ((this.bitSize - 1 + 7) / 8) : this.GetInputBlockSize();
			if (length > num)
			{
				throw new DataLengthException("input too large for ElGamal cipher.\n");
			}
			BigInteger p = this.key.Parameters.P;
			byte[] array;
			if (this.key is ElGamalPrivateKeyParameters)
			{
				int num2 = length / 2;
				BigInteger bigInteger = new BigInteger(1, input, inOff, num2);
				BigInteger val = new BigInteger(1, input, inOff + num2, num2);
				ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)this.key;
				BigInteger bigInteger2 = bigInteger.ModPow(p.Subtract(BigInteger.One).Subtract(elGamalPrivateKeyParameters.X), p).Multiply(val).Mod(p);
				array = bigInteger2.ToByteArrayUnsigned();
			}
			else
			{
				BigInteger bigInteger3 = new BigInteger(1, input, inOff, length);
				if (bigInteger3.BitLength >= p.BitLength)
				{
					throw new DataLengthException("input too large for ElGamal cipher.\n");
				}
				ElGamalPublicKeyParameters elGamalPublicKeyParameters = (ElGamalPublicKeyParameters)this.key;
				BigInteger value = p.Subtract(BigInteger.Two);
				BigInteger bigInteger4;
				do
				{
					bigInteger4 = new BigInteger(p.BitLength, this.random);
				}
				while (bigInteger4.SignValue == 0 || bigInteger4.CompareTo(value) > 0);
				BigInteger g = this.key.Parameters.G;
				BigInteger bigInteger5 = g.ModPow(bigInteger4, p);
				BigInteger bigInteger6 = bigInteger3.Multiply(elGamalPublicKeyParameters.Y.ModPow(bigInteger4, p)).Mod(p);
				array = new byte[this.GetOutputBlockSize()];
				byte[] array2 = bigInteger5.ToByteArrayUnsigned();
				byte[] array3 = bigInteger6.ToByteArrayUnsigned();
				array2.CopyTo(array, array.Length / 2 - array2.Length);
				array3.CopyTo(array, array.Length - array3.Length);
			}
			return array;
		}

		// Token: 0x04001994 RID: 6548
		private ElGamalKeyParameters key;

		// Token: 0x04001995 RID: 6549
		private SecureRandom random;

		// Token: 0x04001996 RID: 6550
		private bool forEncryption;

		// Token: 0x04001997 RID: 6551
		private int bitSize;
	}
}
