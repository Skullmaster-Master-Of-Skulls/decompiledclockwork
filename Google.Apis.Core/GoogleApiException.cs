using System;
using System.Net;
using Google.Apis.Requests;
using Google.Apis.Util;

namespace Google
{
	// Token: 0x02000003 RID: 3
	public class GoogleApiException : Exception
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		public GoogleApiException(string serviceName, string message, Exception inner) : base(message, inner)
		{
			serviceName.ThrowIfNull("serviceName");
			this.serviceName = serviceName;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B1 File Offset: 0x000002B1
		public GoogleApiException(string serviceName, string message) : this(serviceName, message, null)
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		public RequestError Error { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020CD File Offset: 0x000002CD
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020D5 File Offset: 0x000002D5
		public HttpStatusCode HttpStatusCode { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x000020DE File Offset: 0x000002DE
		public override string ToString()
		{
			return string.Format("The service {1} has thrown an exception: {0}", base.ToString(), this.serviceName);
		}

		// Token: 0x04000002 RID: 2
		private readonly string serviceName;
	}
}
