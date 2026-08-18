using System;
using System.Diagnostics.Tracing;

namespace System.Web
{
	// Token: 0x0200001B RID: 27
	[EventData(Name = "TargetFrameworkSet")]
	internal struct TargetFrameworkTelemetryData
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003C93 File Offset: 0x00001E93
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003C9B File Offset: 0x00001E9B
		public string AppID { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003CA4 File Offset: 0x00001EA4
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003CAC File Offset: 0x00001EAC
		public string TargetFramework { get; set; }
	}
}
