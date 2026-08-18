using System;
using System.Net.Http;
using Google.Apis.Http;

namespace Google.Apis.Upload
{
	// Token: 0x02000009 RID: 9
	public sealed class ResumableUploadOptions
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002B83 File Offset: 0x00000D83
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002B8B File Offset: 0x00000D8B
		public HttpClient HttpClient { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002B94 File Offset: 0x00000D94
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002B9C File Offset: 0x00000D9C
		public Action<HttpRequestMessage> ModifySessionInitiationRequest { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002BA5 File Offset: 0x00000DA5
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002BAD File Offset: 0x00000DAD
		public ISerializer Serializer { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002BB6 File Offset: 0x00000DB6
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002BBE File Offset: 0x00000DBE
		public string ServiceName { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002BC7 File Offset: 0x00000DC7
		internal ConfigurableHttpClient ConfigurableHttpClient
		{
			get
			{
				return this.HttpClient as ConfigurableHttpClient;
			}
		}
	}
}
