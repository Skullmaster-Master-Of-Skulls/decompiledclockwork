using System;
using System.Diagnostics.Tracing;

namespace System.Web
{
	// Token: 0x0200001D RID: 29
	[EventData(Name = "ProviderInitialized")]
	internal struct ProviderTelemetryData
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003CD7 File Offset: 0x00001ED7
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003CDF File Offset: 0x00001EDF
		public string AppID { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003CE8 File Offset: 0x00001EE8
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public string ProviderType { get; set; }
	}
}
