using System;
using System.IO;
using System.Net.Security;
using MailBee.Security;

namespace a.j
{
	// Token: 0x020001A1 RID: 417
	internal class aq : SslStream
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00038786 File Offset: 0x00037786
		public aq(Stream A_0, bool A_1, RemoteCertificateValidationCallback A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x0400096E RID: 2414
		public CertificateValidationFlags a;
	}
}
