using System;

namespace System.IdentityModel
{
	// Token: 0x02000089 RID: 137
	internal enum CertificateEncoding
	{
		// Token: 0x040003E5 RID: 997
		Zero,
		// Token: 0x040003E6 RID: 998
		X509AsnEncoding,
		// Token: 0x040003E7 RID: 999
		X509NdrEncoding,
		// Token: 0x040003E8 RID: 1000
		Pkcs7AsnEncoding = 65536,
		// Token: 0x040003E9 RID: 1001
		Pkcs7NdrEncoding = 131072,
		// Token: 0x040003EA RID: 1002
		AnyAsnEncoding = 65537
	}
}
