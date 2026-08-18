using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200015E RID: 350
	internal class HttpActionInvokerTracer : IHttpActionInvoker, IDecorator<IHttpActionInvoker>
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0001CF37 File Offset: 0x0001B137
		public HttpActionInvokerTracer(IHttpActionInvoker innerInvoker, ITraceWriter traceWriter)
		{
			this._innerInvoker = innerInvoker;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0001CF4D File Offset: 0x0001B14D
		public IHttpActionInvoker Inner
		{
			get
			{
				return this._innerInvoker;
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		Task<HttpResponseMessage> IHttpActionInvoker.InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}
			return this._traceWriter.TraceBeginEndAsync(actionContext.ControllerContext.Request, TraceCategories.ActionCategory, TraceLevel.Info, this._innerInvoker.GetType().Name, "InvokeActionAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionInvokeMessage, new object[]
				{
					FormattingUtilities.ActionInvokeToString(actionContext)
				});
			}, () => this._innerInvoker.InvokeActionAsync(actionContext, cancellationToken), delegate(TraceRecord tr, HttpResponseMessage result)
			{
				if (result != null)
				{
					tr.Status = result.StatusCode;
				}
			}, null);
		}

		// Token: 0x04000299 RID: 665
		private const string InvokeActionAsyncMethodName = "InvokeActionAsync";

		// Token: 0x0400029A RID: 666
		private readonly IHttpActionInvoker _innerInvoker;

		// Token: 0x0400029B RID: 667
		private readonly ITraceWriter _traceWriter;
	}
}
