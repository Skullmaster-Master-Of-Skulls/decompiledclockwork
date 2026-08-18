using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x0200003F RID: 63
	public class ExceptionContext
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00007547 File Offset: 0x00005747
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
			if (catchBlock == null)
			{
				throw new ArgumentNullException("catchBlock");
			}
			this.CatchBlock = catchBlock;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000757C File Offset: 0x0000577C
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock, HttpActionContext actionContext) : this(exception, catchBlock)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			this.ActionContext = actionContext;
			HttpControllerContext controllerContext = actionContext.ControllerContext;
			if (controllerContext == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpActionContext).Name,
					"ControllerContext"
				}), "actionContext");
			}
			this.ControllerContext = controllerContext;
			HttpRequestContext requestContext = controllerContext.RequestContext;
			this.RequestContext = requestContext;
			HttpRequestMessage request = controllerContext.Request;
			if (request == null)
			{
				throw new ArgumentException(Error.Format(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpControllerContext).Name,
					"Request"
				}), "actionContext");
			}
			this.Request = request;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007648 File Offset: 0x00005848
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock, HttpRequestMessage request) : this(exception, catchBlock)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.Request = request;
			this.RequestContext = request.GetRequestContext();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007674 File Offset: 0x00005874
		public ExceptionContext(Exception exception, ExceptionContextCatchBlock catchBlock, HttpRequestMessage request, HttpResponseMessage response) : this(exception, catchBlock)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.Request = request;
			this.RequestContext = request.GetRequestContext();
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.Response = response;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000076C1 File Offset: 0x000058C1
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000076C9 File Offset: 0x000058C9
		public Exception Exception { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000076D2 File Offset: 0x000058D2
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000076DA File Offset: 0x000058DA
		public ExceptionContextCatchBlock CatchBlock { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000076E3 File Offset: 0x000058E3
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000076EB File Offset: 0x000058EB
		public HttpRequestMessage Request { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000076F4 File Offset: 0x000058F4
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000076FC File Offset: 0x000058FC
		public HttpRequestContext RequestContext { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007705 File Offset: 0x00005905
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000770D File Offset: 0x0000590D
		public HttpControllerContext ControllerContext { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007716 File Offset: 0x00005916
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000771E File Offset: 0x0000591E
		public HttpActionContext ActionContext { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00007727 File Offset: 0x00005927
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000772F File Offset: 0x0000592F
		public HttpResponseMessage Response { get; set; }
	}
}
