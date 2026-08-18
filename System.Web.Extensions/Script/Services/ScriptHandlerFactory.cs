using System;
using System.Security;
using System.Web.Services.Protocols;
using System.Web.SessionState;

namespace System.Web.Script.Services
{
	// Token: 0x020000F3 RID: 243
	internal class ScriptHandlerFactory : IHttpHandlerFactory
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x0002BB2E File Offset: 0x00029D2E
		public ScriptHandlerFactory()
		{
			this._restHandlerFactory = new RestHandlerFactory();
			this._webServiceHandlerFactory = new WebServiceHandlerFactory();
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002BB4C File Offset: 0x00029D4C
		[SecuritySafeCritical]
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			IHttpHandlerFactory httpHandlerFactory;
			if (RestHandlerFactory.IsRestRequest(context))
			{
				httpHandlerFactory = this._restHandlerFactory;
			}
			else
			{
				httpHandlerFactory = this._webServiceHandlerFactory;
			}
			IHttpHandler handler = httpHandlerFactory.GetHandler(context, requestType, url, pathTranslated);
			bool flag = handler is IRequiresSessionState;
			if (handler is IHttpAsyncHandler)
			{
				if (flag)
				{
					return new ScriptHandlerFactory.AsyncHandlerWrapperWithSession(handler, httpHandlerFactory);
				}
				return new ScriptHandlerFactory.AsyncHandlerWrapper(handler, httpHandlerFactory);
			}
			else
			{
				if (flag)
				{
					return new ScriptHandlerFactory.HandlerWrapperWithSession(handler, httpHandlerFactory);
				}
				return new ScriptHandlerFactory.HandlerWrapper(handler, httpHandlerFactory);
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002BBB4 File Offset: 0x00029DB4
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			((ScriptHandlerFactory.HandlerWrapper)handler).ReleaseHandler();
		}

		// Token: 0x04000396 RID: 918
		private IHttpHandlerFactory _restHandlerFactory;

		// Token: 0x04000397 RID: 919
		private IHttpHandlerFactory _webServiceHandlerFactory;

		// Token: 0x0200017A RID: 378
		internal class HandlerWrapper : IHttpHandler
		{
			// Token: 0x0600107D RID: 4221 RVA: 0x00038A95 File Offset: 0x00036C95
			internal HandlerWrapper(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory)
			{
				this._originalFactory = originalFactory;
				this._originalHandler = originalHandler;
			}

			// Token: 0x0600107E RID: 4222 RVA: 0x00038AAB File Offset: 0x00036CAB
			internal void ReleaseHandler()
			{
				this._originalFactory.ReleaseHandler(this._originalHandler);
			}

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x0600107F RID: 4223 RVA: 0x00038ABE File Offset: 0x00036CBE
			public bool IsReusable
			{
				get
				{
					return this._originalHandler.IsReusable;
				}
			}

			// Token: 0x06001080 RID: 4224 RVA: 0x00038ACB File Offset: 0x00036CCB
			public void ProcessRequest(HttpContext context)
			{
				this._originalHandler.ProcessRequest(context);
			}

			// Token: 0x0400051C RID: 1308
			protected IHttpHandler _originalHandler;

			// Token: 0x0400051D RID: 1309
			private IHttpHandlerFactory _originalFactory;
		}

		// Token: 0x0200017B RID: 379
		internal class HandlerWrapperWithSession : ScriptHandlerFactory.HandlerWrapper, IRequiresSessionState
		{
			// Token: 0x06001081 RID: 4225 RVA: 0x00038AD9 File Offset: 0x00036CD9
			internal HandlerWrapperWithSession(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory) : base(originalHandler, originalFactory)
			{
			}
		}

		// Token: 0x0200017C RID: 380
		private class AsyncHandlerWrapper : ScriptHandlerFactory.HandlerWrapper, IHttpAsyncHandler, IHttpHandler
		{
			// Token: 0x06001082 RID: 4226 RVA: 0x00038AD9 File Offset: 0x00036CD9
			internal AsyncHandlerWrapper(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory) : base(originalHandler, originalFactory)
			{
			}

			// Token: 0x06001083 RID: 4227 RVA: 0x00038AE3 File Offset: 0x00036CE3
			public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
			{
				return ((IHttpAsyncHandler)this._originalHandler).BeginProcessRequest(context, cb, extraData);
			}

			// Token: 0x06001084 RID: 4228 RVA: 0x00038AF8 File Offset: 0x00036CF8
			public void EndProcessRequest(IAsyncResult result)
			{
				((IHttpAsyncHandler)this._originalHandler).EndProcessRequest(result);
			}
		}

		// Token: 0x0200017D RID: 381
		private class AsyncHandlerWrapperWithSession : ScriptHandlerFactory.AsyncHandlerWrapper, IRequiresSessionState
		{
			// Token: 0x06001085 RID: 4229 RVA: 0x00038B0B File Offset: 0x00036D0B
			internal AsyncHandlerWrapperWithSession(IHttpHandler originalHandler, IHttpHandlerFactory originalFactory) : base(originalHandler, originalFactory)
			{
			}
		}
	}
}
