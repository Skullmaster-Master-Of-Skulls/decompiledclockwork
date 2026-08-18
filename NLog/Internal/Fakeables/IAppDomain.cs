using System;
using System.Collections.Generic;

namespace NLog.Internal.Fakeables
{
	// Token: 0x02000081 RID: 129
	public interface IAppDomain
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000432 RID: 1074
		string BaseDirectory { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000433 RID: 1075
		string ConfigurationFile { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000434 RID: 1076
		IEnumerable<string> PrivateBinPath { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000435 RID: 1077
		string FriendlyName { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000436 RID: 1078
		int Id { get; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000437 RID: 1079
		// (remove) Token: 0x06000438 RID: 1080
		event EventHandler<EventArgs> ProcessExit;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000439 RID: 1081
		// (remove) Token: 0x0600043A RID: 1082
		event EventHandler<EventArgs> DomainUnload;
	}
}
