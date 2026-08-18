using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Owin.ExceptionHandling;
using System.Web.Http.Owin.Properties;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000014 RID: 20
	public class HttpMessageHandlerAdapter : OwinMiddleware, IDisposable
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000034D4 File Offset: 0x000016D4
		public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandlerOptions options) : base(next)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this._messageHandler = options.MessageHandler;
			if (this._messageHandler == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"MessageHandler"
				}), "options");
			}
			this._messageInvoker = new HttpMessageInvoker(this._messageHandler);
			this._bufferPolicySelector = options.BufferPolicySelector;
			if (this._bufferPolicySelector == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"BufferPolicySelector"
				}), "options");
			}
			this._exceptionLogger = options.ExceptionLogger;
			if (this._exceptionLogger == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"ExceptionLogger"
				}), "options");
			}
			this._exceptionHandler = options.ExceptionHandler;
			if (this._exceptionHandler == null)
			{
				throw new ArgumentException(Error.Format(OwinResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(HttpMessageHandlerOptions).Name,
					"ExceptionHandler"
				}), "options");
			}
			this._appDisposing = options.AppDisposing;
			if (this._appDisposing.CanBeCanceled)
			{
				this._appDisposing.Register(new Action(this.OnAppDisposing));
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003664 File Offset: 0x00001864
		[Obsolete("Use the HttpMessageHandlerAdapter(OwinMiddleware, HttpMessageHandlerOptions) constructor instead.")]
		public HttpMessageHandlerAdapter(OwinMiddleware next, HttpMessageHandler messageHandler, IHostBufferPolicySelector bufferPolicySelector) : this(next, HttpMessageHandlerAdapter.CreateOptions(messageHandler, bufferPolicySelector))
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003674 File Offset: 0x00001874
		public HttpMessageHandler MessageHandler
		{
			get
			{
				return this._messageHandler;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000367C File Offset: 0x0000187C
		public IHostBufferPolicySelector BufferPolicySelector
		{
			get
			{
				return this._bufferPolicySelector;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003684 File Offset: 0x00001884
		public IExceptionLogger ExceptionLogger
		{
			get
			{
				return this._exceptionLogger;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000368C File Offset: 0x0000188C
		public IExceptionHandler ExceptionHandler
		{
			get
			{
				return this._exceptionHandler;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003694 File Offset: 0x00001894
		public CancellationToken AppDisposing
		{
			get
			{
				return this._appDisposing;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000369C File Offset: 0x0000189C
		public override Task Invoke(IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IOwinRequest request = context.Request;
			IOwinResponse response = context.Response;
			if (request == null)
			{
				throw Error.InvalidOperation(OwinResources.OwinContext_NullRequest, new object[0]);
			}
			if (response == null)
			{
				throw Error.InvalidOperation(OwinResources.OwinContext_NullResponse, new object[0]);
			}
			return this.InvokeCore(context, request, response);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003CE8 File Offset: 0x00001EE8
		private async Task InvokeCore(IOwinContext context, IOwinRequest owinRequest, IOwinResponse owinResponse)
		{
			CancellationToken cancellationToken = owinRequest.CallCancelled;
			bool bufferInput = this._bufferPolicySelector.UseBufferedInputStream(context);
			if (!bufferInput)
			{
				owinRequest.DisableBuffering();
			}
			HttpContent requestContent;
			if (!owinRequest.Body.CanSeek && bufferInput)
			{
				requestContent = await HttpMessageHandlerAdapter.CreateBufferedRequestContentAsync(owinRequest, cancellationToken);
			}
			else
			{
				requestContent = HttpMessageHandlerAdapter.CreateStreamedRequestContent(owinRequest);
			}
			HttpRequestMessage request = HttpMessageHandlerAdapter.CreateRequestMessage(owinRequest, requestContent);
			HttpMessageHandlerAdapter.MapRequestProperties(request, context);
			HttpMessageHandlerAdapter.SetPrincipal(owinRequest.User);
			HttpResponseMessage response = null;
			bool callNext;
			try
			{
				response = await this._messageInvoker.SendAsync(request, cancellationToken);
				if (response == null)
				{
					throw Error.InvalidOperation(OwinResources.SendAsync_ReturnedNull, new object[0]);
				}
				if (HttpMessageHandlerAdapter.IsSoftNotFound(request, response))
				{
					callNext = true;
				}
				else
				{
					callNext = false;
					if (response.Content == null || await this.ComputeContentLengthAsync(request, response, owinResponse, cancellationToken))
					{
						if (!this._bufferPolicySelector.UseBufferedOutputStream(response))
						{
							owinResponse.DisableBuffering();
						}
						else if (response.Content != null)
						{
							response = await this.BufferResponseContentAsync(request, response, cancellationToken);
						}
						if (await this.PrepareHeadersAsync(request, response, owinResponse, cancellationToken))
						{
							await this.SendResponseMessageAsync(request, response, owinResponse, cancellationToken);
						}
					}
				}
			}
			finally
			{
				request.DisposeRequestResources();
				request.Dispose();
				if (response != null)
				{
					response.Dispose();
				}
			}
			if (callNext && base.Next != null)
			{
				await base.Next.Invoke(context);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003D46 File Offset: 0x00001F46
		private static HttpContent CreateStreamedRequestContent(IOwinRequest owinRequest)
		{
			return new StreamContent(new NonOwnedStream(owinRequest.Body));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003F1C File Offset: 0x0000211C
		private static async Task<HttpContent> CreateBufferedRequestContentAsync(IOwinRequest owinRequest, CancellationToken cancellationToken)
		{
			int? contentLength = owinRequest.GetContentLength();
			MemoryStream buffer;
			if (contentLength == null)
			{
				buffer = new MemoryStream();
			}
			else
			{
				buffer = new MemoryStream(contentLength.Value);
			}
			cancellationToken.ThrowIfCancellationRequested();
			using (StreamContent copier = new StreamContent(owinRequest.Body))
			{
				await copier.CopyToAsync(buffer);
			}
			buffer.Position = 0L;
			owinRequest.Body = buffer;
			return new ByteArrayContent(buffer.GetBuffer(), 0, (int)buffer.Length);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F6C File Offset: 0x0000216C
		private static HttpRequestMessage CreateRequestMessage(IOwinRequest owinRequest, HttpContent requestContent)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(new HttpMethod(owinRequest.Method), owinRequest.Uri);
			try
			{
				httpRequestMessage.Content = requestContent;
				foreach (KeyValuePair<string, string[]> keyValuePair in owinRequest.Headers)
				{
					if (!httpRequestMessage.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value))
					{
						requestContent.Headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			catch
			{
				httpRequestMessage.Dispose();
				throw;
			}
			return httpRequestMessage;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004020 File Offset: 0x00002220
		private static void MapRequestProperties(HttpRequestMessage request, IOwinContext context)
		{
			request.SetOwinContext(context);
			HttpRequestContext context2 = new OwinHttpRequestContext(context, request);
			request.SetRequestContext(context2);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004043 File Offset: 0x00002243
		private static void SetPrincipal(IPrincipal user)
		{
			if (user != null)
			{
				Thread.CurrentPrincipal = user;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004050 File Offset: 0x00002250
		private static bool IsSoftNotFound(HttpRequestMessage request, HttpResponseMessage response)
		{
			bool flag;
			return response.StatusCode == HttpStatusCode.NotFound && request.Properties.TryGetValue(HttpPropertyKeys.NoRouteMatched, out flag) && flag;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000044D4 File Offset: 0x000026D4
		private async Task<HttpResponseMessage> BufferResponseContentAsync(HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				await response.Content.LoadIntoBufferAsync();
				return response;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception source)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(source);
			}
			ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferContent, request, response);
			await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			HttpResponseMessage errorResponse = await this._exceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			response.Dispose();
			HttpResponseMessage result;
			if (errorResponse == null)
			{
				exceptionInfo.Throw();
				result = null;
			}
			else
			{
				response = errorResponse;
				cancellationToken.ThrowIfCancellationRequested();
				Exception errorException;
				try
				{
					await response.Content.LoadIntoBufferAsync();
					return response;
				}
				catch (OperationCanceledException)
				{
					throw;
				}
				catch (Exception ex)
				{
					errorException = ex;
				}
				ExceptionContext errorExceptionContext = new ExceptionContext(errorException, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterBufferError, request, response);
				await this._exceptionLogger.LogAsync(errorExceptionContext, cancellationToken);
				response.Dispose();
				result = request.CreateResponse(HttpStatusCode.InternalServerError);
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004534 File Offset: 0x00002734
		private Task<bool> PrepareHeadersAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			HttpResponseHeaders headers = response.Headers;
			HttpContent content = response.Content;
			bool flag = headers.TransferEncodingChunked == true;
			HttpHeaderValueCollection<TransferCodingHeaderValue> transferEncoding = headers.TransferEncoding;
			if (content != null)
			{
				HttpContentHeaders headers2 = content.Headers;
				if (!flag)
				{
					return this.ComputeContentLengthAsync(request, response, owinResponse, cancellationToken);
				}
				headers2.ContentLength = null;
			}
			if (flag && transferEncoding.Count == 1)
			{
				transferEncoding.Clear();
			}
			return Task.FromResult<bool>(true);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000045B8 File Offset: 0x000027B8
		private Task<bool> ComputeContentLengthAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			HttpResponseHeaders headers = response.Headers;
			HttpContent content = response.Content;
			HttpContentHeaders headers2 = content.Headers;
			Exception exception;
			try
			{
				long? contentLength = headers2.ContentLength;
				return Task.FromResult<bool>(true);
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			return this.HandleTryComputeLengthExceptionAsync(exception, request, response, owinResponse, cancellationToken);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004738 File Offset: 0x00002938
		private async Task<bool> HandleTryComputeLengthExceptionAsync(Exception exception, HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			ExceptionContext exceptionContext = new ExceptionContext(exception, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterComputeContentLength, request, response);
			await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			owinResponse.StatusCode = 500;
			HttpMessageHandlerAdapter.SetHeadersForEmptyResponse(owinResponse.Headers);
			return false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000047A8 File Offset: 0x000029A8
		private Task SendResponseMessageAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			owinResponse.StatusCode = (int)response.StatusCode;
			owinResponse.ReasonPhrase = response.ReasonPhrase;
			IDictionary<string, string[]> headers = owinResponse.Headers;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in response.Headers)
			{
				headers[keyValuePair.Key] = keyValuePair.Value.AsArray<string>();
			}
			HttpContent content = response.Content;
			if (content == null)
			{
				HttpMessageHandlerAdapter.SetHeadersForEmptyResponse(headers);
				return TaskHelpers.Completed();
			}
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in content.Headers)
			{
				headers[keyValuePair2.Key] = keyValuePair2.Value.AsArray<string>();
			}
			return this.SendResponseContentAsync(request, response, owinResponse, cancellationToken);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000048A0 File Offset: 0x00002AA0
		private static void SetHeadersForEmptyResponse(IDictionary<string, string[]> headers)
		{
			headers["Content-Length"] = new string[]
			{
				"0"
			};
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004B24 File Offset: 0x00002D24
		private async Task SendResponseContentAsync(HttpRequestMessage request, HttpResponseMessage response, IOwinResponse owinResponse, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Exception exception;
			try
			{
				await response.Content.CopyToAsync(owinResponse.Body);
				return;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				exception = ex;
			}
			ExceptionContext exceptionContext = new ExceptionContext(exception, OwinExceptionCatchBlocks.HttpMessageHandlerAdapterStreamContent, request, response);
			await this._exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			await HttpMessageHandlerAdapter.AbortResponseAsync();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004B8B File Offset: 0x00002D8B
		private static Task AbortResponseAsync()
		{
			return TaskHelpers.Canceled();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004B94 File Offset: 0x00002D94
		private static HttpMessageHandlerOptions CreateOptions(HttpMessageHandler messageHandler, IHostBufferPolicySelector bufferPolicySelector)
		{
			if (messageHandler == null)
			{
				throw new ArgumentNullException("messageHandler");
			}
			if (bufferPolicySelector == null)
			{
				throw new ArgumentNullException("bufferPolicySelector");
			}
			return new HttpMessageHandlerOptions
			{
				MessageHandler = messageHandler,
				BufferPolicySelector = bufferPolicySelector,
				ExceptionLogger = new EmptyExceptionLogger(),
				ExceptionHandler = new DefaultExceptionHandler(),
				AppDisposing = CancellationToken.None
			};
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004BF3 File Offset: 0x00002DF3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.OnAppDisposing();
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004BFE File Offset: 0x00002DFE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004C0D File Offset: 0x00002E0D
		private void OnAppDisposing()
		{
			if (!this._disposed)
			{
				this._messageInvoker.Dispose();
				this._disposed = true;
			}
		}

		// Token: 0x04000017 RID: 23
		private readonly HttpMessageHandler _messageHandler;

		// Token: 0x04000018 RID: 24
		private readonly HttpMessageInvoker _messageInvoker;

		// Token: 0x04000019 RID: 25
		private readonly IHostBufferPolicySelector _bufferPolicySelector;

		// Token: 0x0400001A RID: 26
		private readonly IExceptionLogger _exceptionLogger;

		// Token: 0x0400001B RID: 27
		private readonly IExceptionHandler _exceptionHandler;

		// Token: 0x0400001C RID: 28
		private readonly CancellationToken _appDisposing;

		// Token: 0x0400001D RID: 29
		private bool _disposed;
	}
}
