using System;
using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x020003D8 RID: 984
	public class DHKekGenerator : IDerivationFunction
	{
		// Token: 0x0600225B RID: 8795 RVA: 0x000D5BF9 File Offset: 0x000D4BF9
		public DHKekGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000D5C08 File Offset: 0x000D4C08
		public void Init(IDerivationParameters param)
		{
			DHKdfParameters dhkdfParameters = (DHKdfParameters)param;
			this.algorithm = dhkdfParameters.Algorithm;
			this.keySize = dhkdfParameters.KeySize;
			this.z = dhkdfParameters.GetZ();
			this.partyAInfo = dhkdfParameters.GetExtraInfo();
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000D5C4C File Offset: 0x000D4C4C
		public IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000D5C54 File Offset: 0x000D4C54
		public int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			if (outBytes.Length - len < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			long num = (long)len;
			int digestSize = this.digest.GetDigestSize();
			if (num > 8589934591L)
			{
				throw new ArgumentException("Output length too large");
			}
			int num2 = (int)((num + (long)digestSize - 1L) / (long)digestSize);
			byte[] array = new byte[this.digest.GetDigestSize()];
			int num3 = 1;
			for (int i = 0; i < num2; i++)
			{
				this.digest.BlockUpdate(this.z, 0, this.z.Length);
				DerSequence derSequence = new DerSequence(new Asn1Encodable[]
				{
					this.algorithm,
					new DerOctetString(this.integerToBytes(num3))
				});
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
				{
					derSequence
				});
				if (this.partyAInfo != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(true, 0, new DerOctetString(this.partyAInfo))
					});
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, new DerOctetString(this.integerToBytes(this.keySize)))
				});
				byte[] derEncoded = new DerSequence(asn1EncodableVector).GetDerEncoded();
				this.digest.BlockUpdate(derEncoded, 0, derEncoded.Length);
				this.digest.DoFinal(array, 0);
				if (len > digestSize)
				{
					Array.Copy(array, 0, outBytes, outOff, digestSize);
					outOff += digestSize;
					len -= digestSize;
				}
				else
				{
					Array.Copy(array, 0, outBytes, outOff, len);
				}
				num3++;
			}
			this.digest.Reset();
			return len;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000D5DF0 File Offset: 0x000D4DF0
		private byte[] integerToBytes(int keySize)
		{
			return new byte[]
			{
				(byte)(keySize >> 24),
				(byte)(keySize >> 16),
				(byte)(keySize >> 8),
				(byte)keySize
			};
		}

		// Token: 0x04001796 RID: 6038
		private readonly IDigest digest;

		// Token: 0x04001797 RID: 6039
		private DerObjectIdentifier algorithm;

		// Token: 0x04001798 RID: 6040
		private int keySize;

		// Token: 0x04001799 RID: 6041
		private byte[] z;

		// Token: 0x0400179A RID: 6042
		private byte[] partyAInfo;
	}
}
