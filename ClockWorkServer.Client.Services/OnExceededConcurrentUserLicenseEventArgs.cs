using System;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.Services
{
	// Token: 0x02000003 RID: 3
	public class OnExceededConcurrentUserLicenseEventArgs : EventArgs
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000025DC File Offset: 0x000007DC
		public AuthenticationSessionInfoDTO SessionInfo { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000025E5 File Offset: 0x000007E5
		public OnExceededConcurrentUserLicenseEventArgs(AuthenticationSessionInfoDTO sessionInfo)
		{
			this.SessionInfo = sessionInfo;
		}
	}
}
