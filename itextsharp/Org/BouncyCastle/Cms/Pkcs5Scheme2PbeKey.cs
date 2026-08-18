using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200061B RID: 1563
	public class Pkcs5Scheme2PbeKey : CmsPbeKey
	{
		// Token: 0x06003529 RID: 13609 RVA: 0x0014A3B1 File Offset: 0x001493B1
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2PbeKey(string password, byte[] salt, int iterationCount) : this(password.ToCharArray(), salt, iterationCount)
		{
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x0014A3C1 File Offset: 0x001493C1
		[Obsolete("Use version taking 'char[]' instead")]
		public Pkcs5Scheme2PbeKey(string password, AlgorithmIdentifier keyDerivationAlgorithm) : this(password.ToCharArray(), keyDerivationAlgorithm)
		{
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x0014A3D0 File Offset: 0x001493D0
		public Pkcs5Scheme2PbeKey(char[] password, byte[] salt, int iterationCount) : base(password, salt, iterationCount)
		{
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x0014A3DB File Offset: 0x001493DB
		public Pkcs5Scheme2PbeKey(char[] password, AlgorithmIdentifier keyDerivationAlgorithm) : base(password, keyDerivationAlgorithm)
		{
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0014A3E8 File Offset: 0x001493E8
		internal override KeyParameter GetEncoded(string algorithmOid)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();
			pkcs5S2ParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(this.password), this.salt, this.iterationCount);
			return (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedParameters(algorithmOid, CmsEnvelopedHelper.Instance.GetKeySize(algorithmOid));
		}
	}
}
