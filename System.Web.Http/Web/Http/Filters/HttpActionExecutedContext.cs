using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C3 RID: 195
	public class HttpActionExecutedContext
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0000E57B File Offset: 0x0000C77B
		public HttpActionExecutedContext(HttpActionContext actionContext, Exception exception)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			this.Exception = exception;
			this._actionContext = actionContext;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000E59F File Offset: 0x0000C79F
		public HttpActionExecutedContext()
		{
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000E5A7 File Offset: 0x0000C7A7
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000E5AF File Offset: 0x0000C7AF
		public HttpActionContext ActionContext
		{
			get
			{
				return this._actionContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionContext = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000E5C1 File Offset: 0x0000C7C1
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		public Exception Exception { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000E5D2 File Offset: 0x0000C7D2
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000E5E9 File Offset: 0x0000C7E9
		public HttpResponseMessage Response
		{
			get
			{
				if (this.ActionContext == null)
				{
					return null;
				}
				return this.ActionContext.Response;
			}
			set
			{
				this.ActionContext.Response = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000E5F7 File Offset: 0x0000C7F7
		public HttpRequestMessage Request
		{
			get
			{
				if (this.ActionContext == null || this.ActionContext.ControllerContext == null)
				{
					return null;
				}
				return this.ActionContext.ControllerContext.Request;
			}
		}

		// Token: 0x04000155 RID: 341
		private HttpActionContext _actionContext;
	}
}
