using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Batch
{
	// Token: 0x02000025 RID: 37
	public class DefaultHttpBatchHandler : HttpBatchHandler
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00005084 File Offset: 0x00003284
		public DefaultHttpBatchHandler(HttpServer httpServer) : base(httpServer)
		{
			this.ExecutionOrder = BatchExecutionOrder.Sequential;
			this.SupportedContentTypes = new List<string>
			{
				"multipart/mixed"
			};
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000050B7 File Offset: 0x000032B7
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000050BF File Offset: 0x000032BF
		public BatchExecutionOrder ExecutionOrder
		{
			get
			{
				return this._executionOrder;
			}
			set
			{
				if (!Enum.IsDefined(typeof(BatchExecutionOrder), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BatchExecutionOrder));
				}
				this._executionOrder = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000050F5 File Offset: 0x000032F5
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000050FD File Offset: 0x000032FD
		public IList<string> SupportedContentTypes { get; private set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x00005108 File Offset: 0x00003308
		public virtual Task<HttpResponseMessage> CreateResponseMessageAsync(IList<HttpResponseMessage> responses, HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (responses == null)
			{
				throw Error.ArgumentNull("responses");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			MultipartContent multipartContent = new MultipartContent("mixed");
			foreach (HttpResponseMessage httpResponse in responses)
			{
				multipartContent.Add(new HttpMessageContent(httpResponse));
			}
			HttpResponseMessage httpResponseMessage = request.CreateResponse();
			httpResponseMessage.Content = multipartContent;
			return Task.FromResult<HttpResponseMessage>(httpResponseMessage);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005458 File Offset: 0x00003658
		public override async Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.ValidateRequest(request);
			IList<HttpRequestMessage> subRequests = await this.ParseBatchRequestsAsync(request, cancellationToken);
			HttpResponseMessage result;
			try
			{
				IList<HttpResponseMessage> responses = await this.ExecuteRequestMessagesAsync(subRequests, cancellationToken);
				result = await this.CreateResponseMessageAsync(responses, request, cancellationToken);
			}
			finally
			{
				IEnumerator<HttpRequestMessage> enumerator = subRequests.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						HttpRequestMessage httpRequestMessage = enumerator.Current;
						request.RegisterForDispose(httpRequestMessage.GetResourcesForDisposal());
						request.RegisterForDispose(httpRequestMessage);
					}
				}
				finally
				{
					bool flag;
					if (flag && enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000583C File Offset: 0x00003A3C
		public virtual async Task<IList<HttpResponseMessage>> ExecuteRequestMessagesAsync(IEnumerable<HttpRequestMessage> requests, CancellationToken cancellationToken)
		{
			if (requests == null)
			{
				throw Error.ArgumentNull("requests");
			}
			List<HttpResponseMessage> responses = new List<HttpResponseMessage>();
			try
			{
				switch (this.ExecutionOrder)
				{
				case BatchExecutionOrder.Sequential:
					using (IEnumerator<HttpRequestMessage> enumerator = requests.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							HttpRequestMessage request2 = enumerator.Current;
							responses.Add(await base.Invoker.SendAsync(request2, cancellationToken));
						}
						goto IL_273;
					}
					break;
				case BatchExecutionOrder.NonSequential:
					break;
				default:
					goto IL_273;
				}
				responses.AddRange(await Task.WhenAll<HttpResponseMessage>(from request in requests
				select this.Invoker.SendAsync(request, cancellationToken)));
				IL_273:;
			}
			catch
			{
				List<HttpResponseMessage>.Enumerator enumerator2 = responses.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						HttpResponseMessage httpResponseMessage = enumerator2.Current;
						if (httpResponseMessage != null)
						{
							httpResponseMessage.Dispose();
						}
					}
				}
				finally
				{
					bool flag;
					if (flag)
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				throw;
			}
			return responses;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005ADC File Offset: 0x00003CDC
		public virtual async Task<IList<HttpRequestMessage>> ParseBatchRequestsAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			List<HttpRequestMessage> requests = new List<HttpRequestMessage>();
			cancellationToken.ThrowIfCancellationRequested();
			MultipartStreamProvider streamProvider = await request.Content.ReadAsMultipartAsync();
			foreach (HttpContent httpContent in streamProvider.Contents)
			{
				cancellationToken.ThrowIfCancellationRequested();
				HttpRequestMessage innerRequest = await httpContent.ReadAsHttpRequestMessageAsync();
				innerRequest.CopyBatchRequestProperties(request);
				requests.Add(innerRequest);
			}
			return requests;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005B34 File Offset: 0x00003D34
		public virtual void ValidateRequest(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (request.Content == null)
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BatchRequestMissingContent));
			}
			MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
			if (contentType == null)
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BatchContentTypeMissing));
			}
			if (!this.SupportedContentTypes.Contains(contentType.MediaType, StringComparer.OrdinalIgnoreCase))
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, Error.Format(SRResources.BatchMediaTypeNotSupported, new object[]
				{
					contentType.MediaType
				})));
			}
		}

		// Token: 0x04000047 RID: 71
		private const string MultiPartContentSubtype = "mixed";

		// Token: 0x04000048 RID: 72
		private const string MultiPartMixed = "multipart/mixed";

		// Token: 0x04000049 RID: 73
		private BatchExecutionOrder _executionOrder;
	}
}
