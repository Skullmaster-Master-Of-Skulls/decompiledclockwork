using System;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200008C RID: 140
	internal class AsyncSessionlessHandler : SyncSessionlessHandler, IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00012A87 File Offset: 0x00011A87
		internal AsyncSessionlessHandler(ServerProtocol protocol) : base(protocol)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012A90 File Offset: 0x00011A90
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object asyncState)
		{
			TraceMethod caller = Tracing.On ? new TraceMethod(this, "BeginProcessRequest", new object[0]) : null;
			if (Tracing.On)
			{
				Tracing.Enter("IHttpAsyncHandler.BeginProcessRequest", caller, Tracing.Details(context.Request));
			}
			IAsyncResult result = base.BeginCoreProcessRequest(callback, asyncState);
			if (Tracing.On)
			{
				Tracing.Exit("IHttpAsyncHandler.BeginProcessRequest", caller);
			}
			return result;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012AF4 File Offset: 0x00011AF4
		public void EndProcessRequest(IAsyncResult asyncResult)
		{
			TraceMethod caller = Tracing.On ? new TraceMethod(this, "EndProcessRequest", new object[0]) : null;
			if (Tracing.On)
			{
				Tracing.Enter("IHttpAsyncHandler.EndProcessRequest", caller);
			}
			base.EndCoreProcessRequest(asyncResult);
			if (Tracing.On)
			{
				Tracing.Exit("IHttpAsyncHandler.EndProcessRequest", caller);
			}
		}
	}
}
