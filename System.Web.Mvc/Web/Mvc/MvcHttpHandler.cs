using System;
using System.Web.Mvc.Async;
using System.Web.Routing;
using System.Web.SessionState;

namespace System.Web.Mvc
{
	// Token: 0x020001C9 RID: 457
	public class MvcHttpHandler : UrlRoutingHandler, IHttpAsyncHandler, IHttpHandler, IRequiresSessionState
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x000239B0 File Offset: 0x00021BB0
		protected virtual IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
		{
			HttpContextBase httpContext2 = new HttpContextWrapper(httpContext);
			return this.BeginProcessRequest(httpContext2, callback, state);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00023A00 File Offset: 0x00021C00
		protected internal virtual IAsyncResult BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, object state)
		{
			IHttpHandler httpHandler = MvcHttpHandler.GetHttpHandler(httpContext);
			IHttpAsyncHandler httpAsyncHandler = httpHandler as IHttpAsyncHandler;
			if (httpAsyncHandler != null)
			{
				BeginInvokeDelegate<IHttpAsyncHandler> beginDelegate = (AsyncCallback asyncCallback, object asyncState, IHttpAsyncHandler innerHandler) => innerHandler.BeginProcessRequest(HttpContext.Current, asyncCallback, asyncState);
				EndInvokeVoidDelegate<IHttpAsyncHandler> endDelegate = delegate(IAsyncResult asyncResult, IHttpAsyncHandler innerHandler)
				{
					innerHandler.EndProcessRequest(asyncResult);
				};
				return AsyncResultWrapper.Begin<IHttpAsyncHandler>(callback, state, beginDelegate, endDelegate, httpAsyncHandler, MvcHttpHandler._processRequestTag, -1, null);
			}
			Action action = delegate()
			{
				httpHandler.ProcessRequest(HttpContext.Current);
			};
			return AsyncResultWrapper.BeginSynchronous(callback, state, action, MvcHttpHandler._processRequestTag);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00023AA5 File Offset: 0x00021CA5
		protected internal virtual void EndProcessRequest(IAsyncResult asyncResult)
		{
			AsyncResultWrapper.End(asyncResult, MvcHttpHandler._processRequestTag);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00023AB4 File Offset: 0x00021CB4
		private static IHttpHandler GetHttpHandler(HttpContextBase httpContext)
		{
			MvcHttpHandler.DummyHttpHandler dummyHttpHandler = new MvcHttpHandler.DummyHttpHandler();
			dummyHttpHandler.PublicProcessRequest(httpContext);
			return dummyHttpHandler.HttpHandler;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00023AD4 File Offset: 0x00021CD4
		protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext)
		{
			if (httpHandler == null)
			{
				throw new ArgumentNullException("httpHandler");
			}
			httpHandler.ProcessRequest(HttpContext.Current);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00023AEF File Offset: 0x00021CEF
		IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			return this.BeginProcessRequest(context, cb, extraData);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00023AFA File Offset: 0x00021CFA
		void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
		{
			this.EndProcessRequest(result);
		}

		// Token: 0x0400037C RID: 892
		private static readonly object _processRequestTag = new object();

		// Token: 0x020001CA RID: 458
		private sealed class DummyHttpHandler : UrlRoutingHandler
		{
			// Token: 0x06000D87 RID: 3463 RVA: 0x00023B17 File Offset: 0x00021D17
			public void PublicProcessRequest(HttpContextBase httpContext)
			{
				this.ProcessRequest(httpContext);
			}

			// Token: 0x06000D88 RID: 3464 RVA: 0x00023B20 File Offset: 0x00021D20
			protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext)
			{
				this.HttpHandler = httpHandler;
			}

			// Token: 0x0400037F RID: 895
			public IHttpHandler HttpHandler;
		}
	}
}
