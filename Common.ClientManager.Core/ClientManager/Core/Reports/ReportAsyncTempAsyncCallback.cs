using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;

namespace TechnoPro.Common.ClientManager.Core.Reports
{
	// Token: 0x02000028 RID: 40
	public class ReportAsyncTempAsyncCallback
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006883 File Offset: 0x00004A83
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000688B File Offset: 0x00004A8B
		public AsyncCallback Callback { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006894 File Offset: 0x00004A94
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000689C File Offset: 0x00004A9C
		public object AsyncState { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000068A5 File Offset: 0x00004AA5
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000068AD File Offset: 0x00004AAD
		public ReportAsyncClientManager ReportAsyncClientManager { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000068B6 File Offset: 0x00004AB6
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000068BE File Offset: 0x00004ABE
		public ExecuteReportReq OriginalRequest { get; set; }
	}
}
