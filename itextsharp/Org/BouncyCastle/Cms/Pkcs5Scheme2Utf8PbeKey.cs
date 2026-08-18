using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000435 RID: 1077
	public class Pkcs5Scheme2Utf8PbeKey : CmsPbeKey
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000DF092 File Offset: 0x000DE092
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2Utf8PbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000DF0A2 File Offset: 0x000DE0A2
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2Utf8PbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000DF0B1 File Offset: 0x000DE0B1
		public Pkcs5Scheme2Utf8PbeKey(char[] password, byte[] salt, int iterationCount) : base(password, salt, iterationCount)
		{
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000DF0BC File Offset: 0x000DE0BC
		public Pkcs5Scheme2Utf8PbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm) : base(password, keyDerivationAlgorithm)
		{
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000DF0C8 File Offset: 0x000DE0C8
		internal override KeyParameter GetEncoded(string algorithmOid)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();
			pkcs5S2ParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToUtf8Bytes(this.password), this.salt, this.iterationCount);
			return (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(algorithmOid, CmsEnvelopedHelper.Instance.GetKeySize(algorithmOid));
		}
	}
}
