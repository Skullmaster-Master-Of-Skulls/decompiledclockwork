using System;
using System.Collections.Generic;
using System.Text;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet
{
	// Token: 0x02000011 RID: 17
	internal interface IConnectionInfo
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BE RID: 190
		IDictionary<string, RequestInfo> ChannelRequests { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BF RID: 191
		Encoding Encoding { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C0 RID: 192
		int RetryAttempts { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C1 RID: 193
		TimeSpan Timeout { get; }

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000C2 RID: 194
		// (remove) Token: 0x060000C3 RID: 195
		event EventHandler<AuthenticationBannerEventArgs> AuthenticationBanner;
	}
}
