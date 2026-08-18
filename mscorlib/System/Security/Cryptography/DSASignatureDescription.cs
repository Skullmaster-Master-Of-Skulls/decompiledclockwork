using System;

namespace System.Security.Cryptography
{
	// Token: 0x020008B5 RID: 2229
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x060050F8 RID: 20728 RVA: 0x00122565 File Offset: 0x00121565
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
