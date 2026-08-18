using System;
using System.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A46 RID: 2630
	[SuppressUnmanagedCodeSecurity]
	internal sealed class StoreCertificateHandle : CertificateHandle
	{
		// Token: 0x06006816 RID: 26646 RVA: 0x0018480A File Offset: 0x00182A0A
		private StoreCertificateHandle()
		{
			this.delete = true;
		}
	}
}
