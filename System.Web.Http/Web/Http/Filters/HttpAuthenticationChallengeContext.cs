using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x02000048 RID: 72
	public class HttpAuthenticationChallengeContext
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00007B29 File Offset: 0x00005D29
		public HttpAuthenticationChallengeContext(HttpActionContext actionContext, IHttpActionResult result)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this.ActionContext = actionContext;
			this.Result = result;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007B5B File Offset: 0x00005D5B
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00007B63 File Offset: 0x00005D63
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00007B6C File Offset: 0x00005D6C
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00007B74 File Offset: 0x00005D74
		public IHttpActionResult Result
		{
			get
			{
				return this._result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._result = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007B8B File Offset: 0x00005D8B
		public HttpRequestMessage Request
		{
			get
			{
				return this.ActionContext.Request;
			}
		}

		// Token: 0x04000094 RID: 148
		private IHttpActionResult _result;
	}
}
