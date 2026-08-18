using System;
using System.IO;
using System.Web.Configuration;
using System.Web.Handlers;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000D7 RID: 215
	internal class ImplicitAsyncPreloadModule
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x00027B0C File Offset: 0x00025D0C
		internal void GetEventHandlers(out BeginEventHandler beginHandler, out EndEventHandler endHandler)
		{
			beginHandler = new BeginEventHandler(this.OnEnter);
			endHandler = new EndEventHandler(this.OnLeave);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00027B2A File Offset: 0x00025D2A
		private void Reset()
		{
			if (this._inputStream != null)
			{
				this._inputStream.Close();
				this._inputStream = null;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00027B48 File Offset: 0x00025D48
		private IAsyncResult OnEnter(object sender, EventArgs e, AsyncCallback cb, object state)
		{
			this._app = (HttpApplication)sender;
			HttpContext context = this._app.Context;
			HttpRequest request = context.Request;
			HttpWorkerRequest workerRequest = context.WorkerRequest;
			HttpAsyncResult httpAsyncResult = new HttpAsyncResult(cb, state);
			AsyncPreloadModeFlags asyncPreloadMode = context.AsyncPreloadMode;
			bool flag;
			bool flag2;
			if (asyncPreloadMode == AsyncPreloadModeFlags.None || request.ReadEntityBodyMode != ReadEntityBodyMode.None || workerRequest == null || !workerRequest.SupportsAsyncRead || !workerRequest.HasEntityBody() || workerRequest.IsEntireEntityBodyIsPreloaded() || context.Handler == null || context.Handler is TransferRequestHandler || context.Handler is DefaultHttpHandler || request.ContentLength > RuntimeConfig.GetConfig(context).HttpRuntime.MaxRequestLengthBytes || ((flag = StringUtil.StringStartsWithIgnoreCase(request.ContentType, "application/x-www-form-urlencoded")) && (asyncPreloadMode & AsyncPreloadModeFlags.Form) != AsyncPreloadModeFlags.Form) || ((flag2 = StringUtil.StringStartsWithIgnoreCase(request.ContentType, "multipart/form-data")) && (asyncPreloadMode & AsyncPreloadModeFlags.FormMultiPart) != AsyncPreloadModeFlags.FormMultiPart) || (!flag && !flag2 && (asyncPreloadMode & AsyncPreloadModeFlags.NonForm) != AsyncPreloadModeFlags.NonForm))
			{
				httpAsyncResult.Complete(true, null, null);
				return httpAsyncResult;
			}
			try
			{
				if (this._callback == null)
				{
					this._callback = new AsyncCallback(this.OnAsyncCompletion);
				}
				this._inputStream = request.GetBufferedInputStream();
				byte[] entityBuffer = this._app.EntityBuffer;
				for (;;)
				{
					IAsyncResult asyncResult = this._inputStream.BeginRead(entityBuffer, 0, entityBuffer.Length, this._callback, httpAsyncResult);
					if (!asyncResult.CompletedSynchronously)
					{
						break;
					}
					if (this._inputStream.EndRead(asyncResult) == 0)
					{
						goto Block_19;
					}
				}
				return httpAsyncResult;
				Block_19:;
			}
			catch
			{
				this.Reset();
				throw;
			}
			httpAsyncResult.Complete(true, null, null);
			return httpAsyncResult;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00027CF8 File Offset: 0x00025EF8
		private void OnLeave(IAsyncResult httpAsyncResult)
		{
			this.Reset();
			((HttpAsyncResult)httpAsyncResult).End();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00027D0C File Offset: 0x00025F0C
		private void OnAsyncCompletion(IAsyncResult readAsyncResult)
		{
			if (readAsyncResult.CompletedSynchronously)
			{
				return;
			}
			HttpAsyncResult httpAsyncResult = readAsyncResult.AsyncState as HttpAsyncResult;
			Exception error = null;
			try
			{
				int num = this._inputStream.EndRead(readAsyncResult);
				byte[] entityBuffer = this._app.EntityBuffer;
				while (num != 0)
				{
					readAsyncResult = this._inputStream.BeginRead(entityBuffer, 0, entityBuffer.Length, this._callback, httpAsyncResult);
					if (!readAsyncResult.CompletedSynchronously)
					{
						return;
					}
					num = this._inputStream.EndRead(readAsyncResult);
				}
			}
			catch (Exception ex)
			{
				error = ex;
			}
			httpAsyncResult.Complete(false, null, error);
		}

		// Token: 0x04000526 RID: 1318
		private HttpApplication _app;

		// Token: 0x04000527 RID: 1319
		private AsyncCallback _callback;

		// Token: 0x04000528 RID: 1320
		private Stream _inputStream;
	}
}
