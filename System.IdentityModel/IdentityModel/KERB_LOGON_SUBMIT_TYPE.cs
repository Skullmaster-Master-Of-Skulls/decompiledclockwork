using System;

namespace System.IdentityModel
{
	// Token: 0x0200005D RID: 93
	internal enum KERB_LOGON_SUBMIT_TYPE
	{
		// Token: 0x04000302 RID: 770
		KerbInteractiveLogon = 2,
		// Token: 0x04000303 RID: 771
		KerbSmartCardLogon = 6,
		// Token: 0x04000304 RID: 772
		KerbWorkstationUnlockLogon,
		// Token: 0x04000305 RID: 773
		KerbSmartCardUnlockLogon,
		// Token: 0x04000306 RID: 774
		KerbProxyLogon,
		// Token: 0x04000307 RID: 775
		KerbTicketLogon,
		// Token: 0x04000308 RID: 776
		KerbTicketUnlockLogon,
		// Token: 0x04000309 RID: 777
		KerbS4ULogon,
		// Token: 0x0400030A RID: 778
		KerbCertificateLogon,
		// Token: 0x0400030B RID: 779
		KerbCertificateS4ULogon,
		// Token: 0x0400030C RID: 780
		KerbCertificateUnlockLogon
	}
}
