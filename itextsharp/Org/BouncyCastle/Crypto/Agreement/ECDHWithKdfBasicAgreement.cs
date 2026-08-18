using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Agreement
{
	// Token: 0x020002FE RID: 766
	public class ECDHWithKdfBasicAgreement : ECDHBasicAgreement
	{
		// Token: 0x06001C19 RID: 7193 RVA: 0x000A86C6 File Offset: 0x000A76C6
		public ECDHWithKdfBasicAgreement(string algorithm, IDerivationFunction kdf)
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

		// Token: 0x06001C1A RID: 7194 RVA: 0x000A86F8 File Offset: 0x000A76F8
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

		// Token: 0x06001C1B RID: 7195 RVA: 0x000A8760 File Offset: 0x000A7760
		private byte[] bigIntToBytes(BigInteger r)
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.privKey.Parameters.G.X);
			return X9IntegerConverter.IntegerToBytes(r, byteLength);
		}

		// Token: 0x04001351 RID: 4945
		private readonly string algorithm;

		// Token: 0x04001352 RID: 4946
		private readonly IDerivationFunction kdf;
	}
}
