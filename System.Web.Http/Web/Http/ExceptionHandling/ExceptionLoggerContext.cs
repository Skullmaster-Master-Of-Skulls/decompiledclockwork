using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000045 RID: 69
	public class ExceptionLoggerContext
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000079C9 File Offset: 0x00005BC9
		public ExceptionLoggerContext(ExceptionContext exceptionContext)
		{
			if (exceptionContext == null)
			{
				throw new ArgumentNullException("exceptionContext");
			}
			this._exceptionContext = exceptionContext;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000079E6 File Offset: 0x00005BE6
		public ExceptionContext ExceptionContext
		{
			get
			{
				return this._exceptionContext;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000079EE File Offset: 0x00005BEE
		public Exception Exception
		{
			get
			{
				return this._exceptionContext.Exception;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000079FB File Offset: 0x00005BFB
		public ExceptionContextCatchBlock CatchBlock
		{
			get
			{
				return this._exceptionContext.CatchBlock;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007A08 File Offset: 0x00005C08
		public HttpRequestMessage Request
		{
			get
			{
				return this._exceptionContext.Request;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007A15 File Offset: 0x00005C15
		public HttpRequestContext RequestContext
		{
			get
			{
				return this._exceptionContext.RequestContext;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007A24 File Offset: 0x00005C24
		public bool CallsHandler
		{
			get
			{
				ExceptionContextCatchBlock catchBlock = this._exceptionContext.CatchBlock;
				return catchBlock.CallsHandler;
			}
		}

		// Token: 0x04000092 RID: 146
		private readonly ExceptionContext _exceptionContext;
	}
}
