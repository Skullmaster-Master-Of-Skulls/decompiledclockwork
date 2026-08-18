using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x02000064 RID: 100
	internal class HttpUnsortedResponse
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000E160 File Offset: 0x0000C360
		public HttpUnsortedResponse()
		{
			this.HttpHeaders = new HttpUnsortedHeaders();
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000E173 File Offset: 0x0000C373
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000E17B File Offset: 0x0000C37B
		public Version Version { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000E184 File Offset: 0x0000C384
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000E18C File Offset: 0x0000C38C
		public HttpStatusCode StatusCode { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000E195 File Offset: 0x0000C395
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000E19D File Offset: 0x0000C39D
		public string ReasonPhrase { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000E1AE File Offset: 0x0000C3AE
		public HttpHeaders HttpHeaders { get; private set; }
	}
}
