using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000CC RID: 204
	internal class RSAPKCS1SHA512SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x00019863 File Offset: 0x00018863
		public RSAPKCS1SHA512SignatureDescription() : base("SHA512")
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00019870 File Offset: 0x00018870
		public sealed override HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName("http://www.w3.org/2001/04/xmlenc#sha512");
		}
	}
}
