using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x02000479 RID: 1145
	public class ECMqvWithKdfBasicAgreement : ECMqvBasicAgreement
	{
		// Token: 0x060026FF RID: 9983 RVA: 0x000EC650 File Offset: 0x000EB650
		public ECMqvWithKdfBasicAgreement(string algorithm, IDerivationFunction kdf)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (kdf == null)
			{
				throw new ArgumentNullException("kdf");
			}
			this.algorithm = algorithm;
			this.kdf = kdf;
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000EC684 File Offset: 0x000EB684
		public override BigInteger CalculateAgreement(ICipherParameters pubKey)
		{
			BigInteger r = base.CalculateAgreement(pubKey);
			int defaultKeySize = GeneratorUtilities.GetDefaultKeySize(this.algorithm);
			DHKdfParameters parameters = new DHKdfParameters(new DerObjectIdentifier(this.algorithm), defaultKeySize, this.bigIntToBytes(r));
			this.kdf.Init(parameters);
			byte[] array = new byte[defaultKeySize / 8];
			this.kdf.GenerateBytes(array, 0, array.Length);
			return new BigInteger(1, array);
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000EC6EC File Offset: 0x000EB6EC
		private byte[] bigIntToBytes(BigInteger r)
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.privParams.StaticPrivateKey.Parameters.G.X);
			return X9IntegerConverter.IntegerToBytes(r, byteLength);
		}

		// Token: 0x04001AC8 RID: 6856
		private readonly string algorithm;

		// Token: 0x04001AC9 RID: 6857
		private readonly IDerivationFunction kdf;
	}
}
