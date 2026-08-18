using System;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x02000007 RID: 7
	public interface IUpdateExecuter
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35
		// (set) Token: 0x06000024 RID: 36
		string Name { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37
		int ExecutionOrder { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000026 RID: 38
		// (set) Token: 0x06000027 RID: 39
		ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		IExternalLogManager ExternalLogManager { get; set; }

		// Token: 0x0600002A RID: 42
		ExecuteUpdatesResp ExecuteUpdate();
	}
}
