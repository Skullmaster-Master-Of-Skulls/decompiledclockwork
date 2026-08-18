using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C9 RID: 201
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x000197A7 File Offset: 0x000187A7
		public RSAPKCS1SignatureDescription(string hashAlgorithmName)
		{
			base.KeyAlgorithm = "System.Security.Cryptography.RSA";
			base.DigestAlgorithm = hashAlgorithmName;
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000197D8 File Offset: 0x000187D8
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = new RSAPKCS1SHA2Deformatter();
			asymmetricSignatureDeformatter.SetKey(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(base.DigestAlgorithm);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00019800 File Offset: 0x00018800
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = new RSAPKCS1SHA2Formatter();
			asymmetricSignatureFormatter.SetKey(key);
			asymmetricSignatureFormatter.SetHashAlgorithm(base.DigestAlgorithm);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x06000505 RID: 1285
		public abstract override HashAlgorithm CreateDigest();
	}
}
