using System;
using System.Net;

namespace Common.Web
{
	// Token: 0x02000002 RID: 2
	internal class MyClient : WebClient
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public bool HeadOnly { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = base.GetWebRequest(address);
			bool flag = this.HeadOnly && webRequest.Method == "GET";
			if (flag)
			{
				webRequest.Method = "HEAD";
			}
			return webRequest;
		}
	}
}
