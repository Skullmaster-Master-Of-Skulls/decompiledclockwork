using System;

namespace MailBee.DnsMX
{
	// Token: 0x0200056D RID: 1389
	[Flags]
	public enum DnsAutodetectOptions
	{
		// Token: 0x04001FBD RID: 8125
		None = 0,
		// Token: 0x04001FBE RID: 8126
		ConfigFiles = 1,
		// Token: 0x04001FBF RID: 8127
		NetInterface = 2,
		// Token: 0x04001FC0 RID: 8128
		Registry = 4,
		// Token: 0x04001FC1 RID: 8129
		Wmi = 8,
		// Token: 0x04001FC2 RID: 8130
		RootServers = 16,
		// Token: 0x04001FC3 RID: 8131
		AllowIPv6Servers = 32
	}
}
