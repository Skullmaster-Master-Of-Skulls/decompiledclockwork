using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x020000D3 RID: 211
	public class HttpResponseException : Exception
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00010DBC File Offset: 0x0000EFBC
		public HttpResponseException(HttpStatusCode statusCode) : this(new HttpResponseMessage(statusCode))
		{
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00010DCA File Offset: 0x0000EFCA
		public HttpResponseException(HttpResponseMessage response) : base(SRResources.HttpResponseExceptionMessage)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			this.Response = response;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00010DEC File Offset: 0x0000EFEC
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		public HttpResponseMessage Response { get; private set; }
	}
}
