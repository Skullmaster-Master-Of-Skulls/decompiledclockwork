using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000CB RID: 203
	internal class RSAPKCS1SHA384SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00019845 File Offset: 0x00018845
		public RSAPKCS1SHA384SignatureDescription() : base("SHA384")
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00019852 File Offset: 0x00018852
		public sealed override HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName("http://www.w3.org/2001/04/xmldsig-more#sha384");
		}
	}
}
