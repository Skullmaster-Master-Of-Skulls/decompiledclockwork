using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Util;
using System.Web.WebSockets;

namespace System.Web
{
	// Token: 0x02000048 RID: 72
	internal sealed class WebSocketPipeline : IDisposable, ISyncContext
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x000072D1 File Offset: 0x000054D1
		public WebSocketPipeline(RootedObjects root, HttpContext httpContext, Func<AspNetWebSocketContext, Task> userFunc, string subProtocol)
		{
			this._root = root;
			this._httpContext = httpContext;
			this._userFunc = userFunc;
			this._subProtocol = subProtocol;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00006164 File Offset: 0x00004364
		public void Dispose()
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000072F8 File Offset: 0x000054F8
		public void ProcessRequest()
		{
			Task<AspNetWebSocket> task3 = this.ProcessRequestImplAsync();
			Task task2 = task3.ContinueWith<Task>(delegate(Task<AspNetWebSocket> task)
			{
				if (task.Result == null)
				{
					return null;
				}
				return task.Result.AbortAsync();
			}, TaskContinuationOptions.ExecuteSynchronously).Unwrap();
			task2.ContinueWith<int>((Task _) => UnsafeIISMethods.MgdPostCompletion(this._root.WorkerRequest.RequestContext, RequestNotificationStatus.Continue), TaskContinuationOptions.ExecuteSynchronously);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00007354 File Offset: 0x00005554
		private ExceptionDispatchInfo DoFlush()
		{
			ExceptionDispatchInfo result;
			try
			{
				this._root.WorkerRequest.FlushResponse(true);
				this._root.WorkerRequest.ExplicitFlush();
				result = null;
			}
			catch (Exception source)
			{
				result = ExceptionDispatchInfo.Capture(source);
			}
			return result;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000073A4 File Offset: 0x000055A4
		private Task<AspNetWebSocket> ProcessRequestImplAsync()
		{
			WebSocketPipeline.<ProcessRequestImplAsync>d__9 <ProcessRequestImplAsync>d__;
			<ProcessRequestImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AspNetWebSocket>.Create();
			<ProcessRequestImplAsync>d__.<>4__this = this;
			<ProcessRequestImplAsync>d__.<>1__state = -1;
			<ProcessRequestImplAsync>d__.<>t__builder.Start<WebSocketPipeline.<ProcessRequestImplAsync>d__9>(ref <ProcessRequestImplAsync>d__);
			return <ProcessRequestImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000073E7 File Offset: 0x000055E7
		HttpContext ISyncContext.HttpContext
		{
			get
			{
				if (!this._isProcessingComplete)
				{
					return this._httpContext;
				}
				return null;
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000073FC File Offset: 0x000055FC
		ISyncContextLock ISyncContext.Enter()
		{
			ThreadContext threadContext = new ThreadContext(this._httpContext);
			threadContext.AssociateWithCurrentThread(this._httpContext.UsesImpersonation);
			return threadContext;
		}

		// Token: 0x0400013E RID: 318
		private readonly RootedObjects _root;

		// Token: 0x0400013F RID: 319
		private readonly HttpContext _httpContext;

		// Token: 0x04000140 RID: 320
		private volatile bool _isProcessingComplete;

		// Token: 0x04000141 RID: 321
		private Func<AspNetWebSocketContext, Task> _userFunc;

		// Token: 0x04000142 RID: 322
		private readonly string _subProtocol;
	}
}
