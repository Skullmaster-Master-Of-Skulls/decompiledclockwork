using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000033 RID: 51
	public class HttpActionContext
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006F04 File Offset: 0x00005104
		public HttpActionContext(HttpControllerContext controllerContext, HttpActionDescriptor actionDescriptor)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			this._controllerContext = controllerContext;
			this._actionDescriptor = actionDescriptor;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006F57 File Offset: 0x00005157
		public HttpActionContext()
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006F75 File Offset: 0x00005175
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006F7D File Offset: 0x0000517D
		public HttpControllerContext ControllerContext
		{
			get
			{
				return this._controllerContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerContext = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006F8F File Offset: 0x0000518F
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006F97 File Offset: 0x00005197
		public HttpActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionDescriptor = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006FA9 File Offset: 0x000051A9
		public ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006FB1 File Offset: 0x000051B1
		public Dictionary<string, object> ActionArguments
		{
			get
			{
				return this._operationArguments;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006FB9 File Offset: 0x000051B9
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006FC1 File Offset: 0x000051C1
		public HttpResponseMessage Response { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006FCA File Offset: 0x000051CA
		public HttpRequestMessage Request
		{
			get
			{
				if (this._controllerContext == null)
				{
					return null;
				}
				return this._controllerContext.Request;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006FE1 File Offset: 0x000051E1
		public HttpRequestContext RequestContext
		{
			get
			{
				if (this._controllerContext == null)
				{
					return null;
				}
				return this._controllerContext.RequestContext;
			}
		}

		// Token: 0x04000076 RID: 118
		private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

		// Token: 0x04000077 RID: 119
		private readonly Dictionary<string, object> _operationArguments = new Dictionary<string, object>();

		// Token: 0x04000078 RID: 120
		private HttpActionDescriptor _actionDescriptor;

		// Token: 0x04000079 RID: 121
		private HttpControllerContext _controllerContext;
	}
}
