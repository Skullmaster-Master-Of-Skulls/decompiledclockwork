using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200001F RID: 31
	public class MultipartFileData
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00004F5C File Offset: 0x0000315C
		public MultipartFileData(HttpContentHeaders headers, string localFileName)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (localFileName == null)
			{
				throw Error.ArgumentNull("localFileName");
			}
			this.Headers = headers;
			this.LocalFileName = localFileName;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004F8E File Offset: 0x0000318E
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004F96 File Offset: 0x00003196
		public HttpContentHeaders Headers { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004F9F File Offset: 0x0000319F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00004FA7 File Offset: 0x000031A7
		public string LocalFileName { get; private set; }
	}
}
