using System;
using System.Diagnostics.Tracing;

namespace System.Web
{
	// Token: 0x0200001C RID: 28
	[EventData(Name = "HandlerMapped")]
	internal struct HttpHandlerTelemetryData
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003CB5 File Offset: 0x00001EB5
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003CBD File Offset: 0x00001EBD
		public string AppID { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003CC6 File Offset: 0x00001EC6
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00003CCE File Offset: 0x00001ECE
		public string HttpHandlerType { get; set; }
	}
}
