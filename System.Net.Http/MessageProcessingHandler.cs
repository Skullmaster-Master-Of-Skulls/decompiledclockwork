using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000018 RID: 24
	[__DynamicallyInvokable]
	public abstract class MessageProcessingHandler : DelegatingHandler
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000062D6 File Offset: 0x000044D6
		[__DynamicallyInvokable]
		protected MessageProcessingHandler()
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000062DE File Offset: 0x000044DE
		[__DynamicallyInvokable]
		protected MessageProcessingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
		{
		}

		// Token: 0x06000157 RID: 343
		[__DynamicallyInvokable]
		protected abstract HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken);

		// Token: 0x06000158 RID: 344
		[__DynamicallyInvokable]
		protected abstract HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken);

		// Token: 0x06000159 RID: 345 RVA: 0x000062E8 File Offset: 0x000044E8
		[__DynamicallyInvokable]
		protected internal sealed override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request", SR.net_http_handler_norequest);
			}
			TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
			try
			{
				HttpRequestMessage request2 = this.ProcessRequest(request, cancellationToken);
				Task<HttpResponseMessage> task2 = base.SendAsync(request2, cancellationToken);
				task2.ContinueWithStandard(delegate(Task<HttpResponseMessage> task)
				{
					if (task.IsFaulted)
					{
						tcs.TrySetException(task.Exception.GetBaseException());
						return;
					}
					if (task.IsCanceled)
					{
						tcs.TrySetCanceled();
						return;
					}
					if (task.Result == null)
					{
						tcs.TrySetException(new InvalidOperationException(SR.net_http_handler_noresponse));
						return;
					}
					try
					{
						HttpResponseMessage result = this.ProcessResponse(task.Result, cancellationToken);
						tcs.TrySetResult(result);
					}
					catch (OperationCanceledException e2)
					{
						MessageProcessingHandler.HandleCanceledOperations(cancellationToken, tcs, e2);
					}
					catch (Exception exception2)
					{
						tcs.TrySetException(exception2);
					}
				});
			}
			catch (OperationCanceledException e)
			{
				MessageProcessingHandler.HandleCanceledOperations(cancellationToken, tcs, e);
			}
			catch (Exception exception)
			{
				tcs.TrySetException(exception);
			}
			return tcs.Task;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000063A8 File Offset: 0x000045A8
		private static void HandleCanceledOperations(CancellationToken cancellationToken, TaskCompletionSource<HttpResponseMessage> tcs, OperationCanceledException e)
		{
			if (cancellationToken.IsCancellationRequested && e.CancellationToken == cancellationToken)
			{
				tcs.TrySetCanceled();
				return;
			}
			tcs.TrySetException(e);
		}
	}
}
