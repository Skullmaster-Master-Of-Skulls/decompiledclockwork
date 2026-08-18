using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000041 RID: 65
	public class ExceptionHandlerContext
	{
		// Token: 0x0600017A RID: 378 RVA: 0x000077AF File Offset: 0x000059AF
		public ExceptionHandlerContext(ExceptionContext exceptionContext)
		{
			if (exceptionContext == null)
			{
				throw new ArgumentNullException("exceptionContext");
			}
			this._exceptionContext = exceptionContext;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000077CC File Offset: 0x000059CC
		public ExceptionContext ExceptionContext
		{
			get
			{
				return this._exceptionContext;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000077D4 File Offset: 0x000059D4
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000077DC File Offset: 0x000059DC
		public IHttpActionResult Result { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000077E5 File Offset: 0x000059E5
		public Exception Exception
		{
			get
			{
				return this._exceptionContext.Exception;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000077F2 File Offset: 0x000059F2
		public ExceptionContextCatchBlock CatchBlock
		{
			get
			{
				return this._exceptionContext.CatchBlock;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000077FF File Offset: 0x000059FF
		public HttpRequestMessage Request
		{
			get
			{
				return this._exceptionContext.Request;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000780C File Offset: 0x00005A0C
		public HttpRequestContext RequestContext
		{
			get
			{
				return this._exceptionContext.RequestContext;
			}
		}

		// Token: 0x0400008C RID: 140
		private readonly ExceptionContext _exceptionContext;
	}
}
