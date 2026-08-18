using System;
using System.Web.Mvc.Properties;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x0200012E RID: 302
	internal static class HttpHandlerUtil
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x000157FC File Offset: 0x000139FC
		public static IHttpHandler WrapForServerExecute(IHttpHandler httpHandler)
		{
			IHttpAsyncHandler httpAsyncHandler = httpHandler as IHttpAsyncHandler;
			if (httpAsyncHandler == null)
			{
				return new HttpHandlerUtil.ServerExecuteHttpHandlerWrapper(httpHandler);
			}
			return new HttpHandlerUtil.ServerExecuteHttpHandlerAsyncWrapper(httpAsyncHandler);
		}

		// Token: 0x0200012F RID: 303
		internal class ServerExecuteHttpHandlerWrapper : Page
		{
			// Token: 0x060007F5 RID: 2037 RVA: 0x00015820 File Offset: 0x00013A20
			public ServerExecuteHttpHandlerWrapper(IHttpHandler httpHandler)
			{
				this._httpHandler = httpHandler;
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001582F File Offset: 0x00013A2F
			internal IHttpHandler InnerHandler
			{
				get
				{
					return this._httpHandler;
				}
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x00015858 File Offset: 0x00013A58
			public override void ProcessRequest(HttpContext context)
			{
				HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.Wrap(delegate()
				{
					this._httpHandler.ProcessRequest(context);
				});
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x000158A0 File Offset: 0x00013AA0
			protected static void Wrap(Action action)
			{
				HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.Wrap<object>(delegate()
				{
					action();
					return null;
				});
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x000158CC File Offset: 0x00013ACC
			protected static TResult Wrap<TResult>(Func<TResult> func)
			{
				TResult result;
				try
				{
					result = func();
				}
				catch (HttpException ex)
				{
					if (ex.GetHttpCode() == 500)
					{
						throw;
					}
					HttpException ex2 = new HttpException(500, MvcResources.ViewPageHttpHandlerWrapper_ExceptionOccurred, ex);
					throw ex2;
				}
				return result;
			}

			// Token: 0x0400023A RID: 570
			private readonly IHttpHandler _httpHandler;
		}

		// Token: 0x02000130 RID: 304
		private sealed class ServerExecuteHttpHandlerAsyncWrapper : HttpHandlerUtil.ServerExecuteHttpHandlerWrapper, IHttpAsyncHandler, IHttpHandler
		{
			// Token: 0x060007FA RID: 2042 RVA: 0x00015918 File Offset: 0x00013B18
			public ServerExecuteHttpHandlerAsyncWrapper(IHttpAsyncHandler httpHandler) : base(httpHandler)
			{
				this._httpHandler = httpHandler;
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x00015954 File Offset: 0x00013B54
			public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
			{
				return HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.Wrap<IAsyncResult>(() => this._httpHandler.BeginProcessRequest(context, cb, extraData));
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x000159B4 File Offset: 0x00013BB4
			public void EndProcessRequest(IAsyncResult result)
			{
				HttpHandlerUtil.ServerExecuteHttpHandlerWrapper.Wrap(delegate()
				{
					this._httpHandler.EndProcessRequest(result);
				});
			}

			// Token: 0x0400023B RID: 571
			private readonly IHttpAsyncHandler _httpHandler;
		}
	}
}
