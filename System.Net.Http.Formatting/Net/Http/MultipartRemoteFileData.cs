using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000016 RID: 22
	public class MultipartRemoteFileData
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004554 File Offset: 0x00002754
		public MultipartRemoteFileData(HttpContentHeaders headers, string location, string fileName)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (location == null)
			{
				throw Error.ArgumentNull("location");
			}
			if (fileName == null)
			{
				throw Error.ArgumentNull("fileName");
			}
			this.FileName = fileName;
			this.Headers = headers;
			this.Location = location;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000045A6 File Offset: 0x000027A6
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000045AE File Offset: 0x000027AE
		public string FileName { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000045B7 File Offset: 0x000027B7
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000045BF File Offset: 0x000027BF
		public HttpContentHeaders Headers { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000045C8 File Offset: 0x000027C8
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000045D0 File Offset: 0x000027D0
		public string Location { get; private set; }
	}
}
