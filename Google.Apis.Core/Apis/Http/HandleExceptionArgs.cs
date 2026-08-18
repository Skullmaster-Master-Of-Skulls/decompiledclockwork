using System;
using System.Net.Http;
using System.Threading;

namespace Google.Apis.Http
{
	// Token: 0x0200002F RID: 47
	public class HandleExceptionArgs
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000042CF File Offset: 0x000024CF
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000042D7 File Offset: 0x000024D7
		public HttpRequestMessage Request { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000042E0 File Offset: 0x000024E0
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000042E8 File Offset: 0x000024E8
		public Exception Exception { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000042F1 File Offset: 0x000024F1
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000042F9 File Offset: 0x000024F9
		public int TotalTries { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004302 File Offset: 0x00002502
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000430A File Offset: 0x0000250A
		public int CurrentFailedTry { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004313 File Offset: 0x00002513
		public bool SupportsRetry
		{
			get
			{
				return this.TotalTries - this.CurrentFailedTry > 0;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004325 File Offset: 0x00002525
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000432D File Offset: 0x0000252D
		public CancellationToken CancellationToken { get; set; }
	}
}
