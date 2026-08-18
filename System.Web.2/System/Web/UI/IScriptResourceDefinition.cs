using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000269 RID: 617
	internal interface IScriptResourceDefinition
	{
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001D4B RID: 7499
		string Path { get; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001D4C RID: 7500
		string DebugPath { get; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001D4D RID: 7501
		string CdnPath { get; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001D4E RID: 7502
		string CdnDebugPath { get; }

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001D4F RID: 7503
		string CdnPathSecureConnection { get; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001D50 RID: 7504
		string CdnDebugPathSecureConnection { get; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001D51 RID: 7505
		string ResourceName { get; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001D52 RID: 7506
		Assembly ResourceAssembly { get; }
	}
}
