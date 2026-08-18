using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x02000063 RID: 99
	internal class HttpUnsortedRequest
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000E109 File Offset: 0x0000C309
		public HttpUnsortedRequest()
		{
			this.HttpHeaders = new HttpUnsortedHeaders();
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000E11C File Offset: 0x0000C31C
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000E124 File Offset: 0x0000C324
		public HttpMethod Method { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000E12D File Offset: 0x0000C32D
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000E135 File Offset: 0x0000C335
		public string RequestUri { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000E13E File Offset: 0x0000C33E
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000E146 File Offset: 0x0000C346
		public Version Version { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000E14F File Offset: 0x0000C34F
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000E157 File Offset: 0x0000C357
		public HttpHeaders HttpHeaders { get; private set; }
	}
}
