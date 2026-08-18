using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000876 RID: 2166
	internal class HttpPipelineCancellationTokenSource : CancellationTokenSource
	{
		// Token: 0x06005214 RID: 21012 RVA: 0x0012E200 File Offset: 0x0012C400
		public HttpPipelineCancellationTokenSource(HttpRequestContext httpRequestContext)
		{
			this.httpRequestContext = httpRequestContext;
			base.Token.Register(HttpPipelineCancellationTokenSource.onCancelled, this);
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x0012E230 File Offset: 0x0012C430
		private static void OnCancelled(object obj)
		{
			HttpPipelineCancellationTokenSource httpPipelineCancellationTokenSource = (HttpPipelineCancellationTokenSource)obj;
			httpPipelineCancellationTokenSource.HandleCancelCallBack();
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0012E24A File Offset: 0x0012C44A
		private void HandleCancelCallBack()
		{
			this.httpRequestContext.Abort();
		}

		// Token: 0x0400323A RID: 12858
		private static Action<object> onCancelled = Fx.ThunkCallback<object>(new Action<object>(HttpPipelineCancellationTokenSource.OnCancelled));

		// Token: 0x0400323B RID: 12859
		private HttpRequestContext httpRequestContext;
	}
}
