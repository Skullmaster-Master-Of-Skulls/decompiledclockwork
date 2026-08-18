using System;
using System.Net.Http;
using System.Threading;

namespace Google.Apis.Http
{
	// Token: 0x02000032 RID: 50
	public class HandleUnsuccessfulResponseArgs
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004336 File Offset: 0x00002536
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000433E File Offset: 0x0000253E
		public HttpRequestMessage Request { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004347 File Offset: 0x00002547
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000434F File Offset: 0x0000254F
		public HttpResponseMessage Response { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004358 File Offset: 0x00002558
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004360 File Offset: 0x00002560
		public int TotalTries { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004369 File Offset: 0x00002569
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004371 File Offset: 0x00002571
		public int CurrentFailedTry { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000437A File Offset: 0x0000257A
		public bool SupportsRetry
		{
			get
			{
				return this.TotalTries - this.CurrentFailedTry > 0;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000438C File Offset: 0x0000258C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00004394 File Offset: 0x00002594
		public CancellationToken CancellationToken { get; set; }
	}
}
