using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000CA RID: 202
	internal class RSAPKCS1SHA256SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00019827 File Offset: 0x00018827
		public RSAPKCS1SHA256SignatureDescription() : base("SHA256")
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00019834 File Offset: 0x00018834
		public sealed override HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName("http://www.w3.org/2001/04/xmlenc#sha256");
		}
	}
}
