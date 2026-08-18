using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000078 RID: 120
	public class AsyncUploadResult : IAsyncUploadResult
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public string FileName { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000C7F1 File Offset: 0x0000A9F1
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000C7F9 File Offset: 0x0000A9F9
		public string ContentType { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000C802 File Offset: 0x0000AA02
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000C80A File Offset: 0x0000AA0A
		public long ContentLength { get; set; }
	}
}
