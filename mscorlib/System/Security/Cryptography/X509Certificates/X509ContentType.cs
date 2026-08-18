using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008C0 RID: 2240
	[ComVisible(true)]
	public enum X509ContentType
	{
		// Token: 0x04002A0B RID: 10763
		Unknown,
		// Token: 0x04002A0C RID: 10764
		Cert,
		// Token: 0x04002A0D RID: 10765
		SerializedCert,
		// Token: 0x04002A0E RID: 10766
		Pfx,
		// Token: 0x04002A0F RID: 10767
		Pkcs12 = 3,
		// Token: 0x04002A10 RID: 10768
		SerializedStore,
		// Token: 0x04002A11 RID: 10769
		Pkcs7,
		// Token: 0x04002A12 RID: 10770
		Authenticode
	}
}
