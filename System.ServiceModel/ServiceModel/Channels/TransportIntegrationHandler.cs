using System;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200086A RID: 2154
	internal class TransportIntegrationHandler : DelegatingHandler
	{
		// Token: 0x06005159 RID: 20825 RVA: 0x0012B689 File Offset: 0x00129889
		public TransportIntegrationHandler(HttpMessageHandler innerChannel) : base(innerChannel)
		{
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x0012B694 File Offset: 0x00129894
		public Task<HttpResponseMessage> ProcessPipelineAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(delegate(Task<HttpResponseMessage> task)
			{
				HttpResponseMessage httpResponseMessage;
				if (task.IsFaulted)
				{
					if (Fx.IsFatal(task.Exception))
					{
						throw task.Exception;
					}
					FxTrace.Exception.AsError<FaultException>(task.Exception);
					httpResponseMessage = TransportIntegrationHandler.TraceFaultAndGetResponseMessasge(request);
				}
				else if (task.IsCanceled)
				{
					HttpPipeline httpPipeline = HttpPipeline.GetHttpPipeline(request);
					if (TD.HttpPipelineTimeoutExceptionIsEnabled())
					{
						TD.HttpPipelineTimeoutException((httpPipeline != null) ? httpPipeline.EventTraceActivity : null);
					}
					FxTrace.Exception.AsError(new TimeoutException(SR.GetString("HttpPipelineOperationCanceledError")));
					httpPipeline.Cancel();
					httpResponseMessage = null;
				}
				else
				{
					httpResponseMessage = task.Result;
					if (httpResponseMessage == null)
					{
						FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("HttpPipelineNotSupportNullResponseMessage", new object[]
						{
							typeof(DelegatingHandler).Name,
							typeof(HttpResponseMessage).Name
						})));
						httpResponseMessage = TransportIntegrationHandler.TraceFaultAndGetResponseMessasge(request);
					}
				}
				return httpResponseMessage;
			}, TaskContinuationOptions.ExecuteSynchronously);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x0012B6D4 File Offset: 0x001298D4
		private static HttpResponseMessage TraceFaultAndGetResponseMessasge(HttpRequestMessage request)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
			httpResponseMessage.RequestMessage = request;
			if (TD.HttpPipelineFaultedIsEnabled())
			{
				HttpPipeline httpPipeline = HttpPipeline.GetHttpPipeline(request);
				TD.HttpPipelineFaulted((httpPipeline != null) ? httpPipeline.EventTraceActivity : null);
			}
			return httpResponseMessage;
		}
	}
}
