using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x0200061A RID: 1562
	public class ECDHKekGenerator : IDerivationFunction
	{
		// Token: 0x06003524 RID: 13604 RVA: 0x0014A2AE File Offset: 0x001492AE
		public ECDHKekGenerator(IDigest digest)
		{
			this.kdf = new Kdf2BytesGenerator(digest);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x0014A2C4 File Offset: 0x001492C4
		public void Init(IDerivationParameters param)
		{
			DHKdfParameters dhkdfParameters = (DHKdfParameters)param;
			this.algorithm = dhkdfParameters.Algorithm;
			this.keySize = dhkdfParameters.KeySize;
			this.z = dhkdfParameters.GetZ();
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x0014A2FC File Offset: 0x001492FC
		public IDigest Digest
		{
			get
			{
				return this.kdf.Digest;
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x0014A30C File Offset: 0x0014930C
		public int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			DerSequence derSequence = new DerSequence(new Asn1Encodable[]
			{
				new AlgorithmIdentifier(this.algorithm, DerNull.Instance),
				new DerTaggedObject(true, 2, new DerOctetString(this.integerToBytes(this.keySize)))
			});
			this.kdf.Init(new KdfParameters(this.z, derSequence.GetDerEncoded()));
			return this.kdf.GenerateBytes(outBytes, outOff, len);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x0014A380 File Offset: 0x00149380
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

		// Token: 0x0400238B RID: 9099
		private readonly IDerivationFunction kdf;

		// Token: 0x0400238C RID: 9100
		private DerObjectIdentifier algorithm;

		// Token: 0x0400238D RID: 9101
		private int keySize;

		// Token: 0x0400238E RID: 9102
		private byte[] z;
	}
}
