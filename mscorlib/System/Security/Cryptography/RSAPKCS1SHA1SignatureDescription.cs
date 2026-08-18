using System;

namespace System.Security.Cryptography
{
	// Token: 0x020008B4 RID: 2228
	internal class RSAPKCS1SHA1SignatureDescription : SignatureDescription
	{
		// Token: 0x060050F6 RID: 20726 RVA: 0x00122500 File Offset: 0x00121500
		public RSAPKCS1SHA1SignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.RSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x00122534 File Offset: 0x00121534
		public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(base.DeformatterAlgorithm);
			asymmetricSignatureDeformatter.SetKey(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm("SHA1");
			return asymmetricSignatureDeformatter;
		}
	}
}
