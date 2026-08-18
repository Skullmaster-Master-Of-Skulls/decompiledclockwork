using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Http.WebHost.Properties;
using System.Web.Http.WebHost.Routing;
using System.Web.Routing;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000024 RID: 36
	public class HttpControllerHandler : HttpTaskAsyncHandler
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00004E78 File Offset: 0x00003078
		public HttpControllerHandler(RouteData routeData) : this(routeData, GlobalConfiguration.DefaultServer)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004E86 File Offset: 0x00003086
		public HttpControllerHandler(RouteData routeData, HttpMessageHandler handler)
		{
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			if (handler == null)
			{
				throw Error.ArgumentNull("handler");
			}
			this._routeData = new HostedHttpRouteData(routeData);
			this._server = new HttpMessageInvoker(handler);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004EC2 File Offset: 0x000030C2
		public override Task ProcessRequestAsync(HttpContext context)
		{
			return this.ProcessRequestAsyncCore(new HttpContextWrapper(context));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000515C File Offset: 0x0000335C
		internal async Task ProcessRequestAsyncCore(HttpContextBase contextBase)
		{
			HttpRequestMessage request = contextBase.GetHttpRequestMessage() ?? HttpControllerHandler.ConvertRequest(contextBase);
			request.SetRouteData(this._routeData);
			CancellationToken cancellationToken = contextBase.Response.GetClientDisconnectedTokenWhenFixed();
			HttpResponseMessage response = null;
			try
			{
				response = await this._server.SendAsync(request, cancellationToken);
				await HttpControllerHandler.CopyResponseAsync(contextBase, request, response, HttpControllerHandler._exceptionLogger.Value, HttpControllerHandler._exceptionHandler.Value, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				contextBase.Request.Abort();
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
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000051AC File Offset: 0x000033AC
		private static void CopyHeaders(HttpHeaders from, HttpContextBase to)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in from)
			{
				string key = keyValuePair.Key;
				foreach (string value in keyValuePair.Value)
				{
					to.Response.AppendHeader(key, value);
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005240 File Offset: 0x00003440
		private static void AddHeaderToHttpRequestMessage(HttpRequestMessage httpRequestMessage, string headerName, string[] headerValues)
		{
			if (!httpRequestMessage.Headers.TryAddWithoutValidation(headerName, headerValues))
			{
				httpRequestMessage.Content.Headers.TryAddWithoutValidation(headerName, headerValues);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005448 File Offset: 0x00003648
		internal static async Task CopyResponseAsync(HttpContextBase httpContextBase, HttpRequestMessage request, HttpResponseMessage response, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler, CancellationToken cancellationToken)
		{
			if (response == null)
			{
				HttpControllerHandler.SetEmptyErrorResponse(httpContextBase.Response);
			}
			else if (await HttpControllerHandler.CopyResponseStatusAndHeadersAsync(httpContextBase, request, response, exceptionLogger, cancellationToken))
			{
				if (response.Headers.CacheControl == null)
				{
					httpContextBase.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				}
				if (response.Content != null)
				{
					await HttpControllerHandler.WriteResponseContentAsync(httpContextBase, request, response, exceptionLogger, exceptionHandler, cancellationToken);
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000054B8 File Offset: 0x000036B8
		internal static HttpRequestMessage ConvertRequest(HttpContextBase httpContextBase)
		{
			return HttpControllerHandler.ConvertRequest(httpContextBase, HttpControllerHandler._bufferPolicySelector.Value);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000054F0 File Offset: 0x000036F0
		internal static HttpRequestMessage ConvertRequest(HttpContextBase httpContextBase, IHostBufferPolicySelector policySelector)
		{
			HttpRequestBase requestBase = httpContextBase.Request;
			HttpMethod httpMethod = HttpMethodHelper.GetHttpMethod(requestBase.HttpMethod);
			Uri url = requestBase.Url;
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, url);
			bool bufferInput = policySelector == null || policySelector.UseBufferedInputStream(httpContextBase);
			httpRequestMessage.Content = HttpControllerHandler.GetStreamContent(requestBase, bufferInput);
			foreach (object obj in requestBase.Headers)
			{
				string text = (string)obj;
				string[] values = requestBase.Headers.GetValues(text);
				HttpControllerHandler.AddHeaderToHttpRequestMessage(httpRequestMessage, text, values);
			}
			httpRequestMessage.SetHttpContext(httpContextBase);
			HttpRequestContext context = new WebHostHttpRequestContext(httpContextBase, requestBase, httpRequestMessage);
			httpRequestMessage.SetRequestContext(context);
			IDictionary items = httpContextBase.Items;
			if (items != null && items.Contains(HttpControllerHandler.OwinEnvironmentHttpContextKey))
			{
				httpRequestMessage.Properties.Add(HttpControllerHandler.OwinEnvironmentKey, items[HttpControllerHandler.OwinEnvironmentHttpContextKey]);
			}
			httpRequestMessage.Properties.Add(HttpPropertyKeys.RetrieveClientCertificateDelegateKey, HttpControllerHandler._retrieveClientCertificate);
			httpRequestMessage.Properties.Add(HttpPropertyKeys.IsLocalKey, new Lazy<bool>(() => requestBase.IsLocal));
			httpRequestMessage.Properties.Add(HttpPropertyKeys.IncludeErrorDetailKey, new Lazy<bool>(() => !httpContextBase.IsCustomErrorEnabled));
			return httpRequestMessage;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005804 File Offset: 0x00003A04
		private static HttpContent GetStreamContent(HttpRequestBase requestBase, bool bufferInput)
		{
			if (bufferInput)
			{
				return new HttpControllerHandler.LazyStreamContent(delegate()
				{
					if (requestBase.ReadEntityBodyMode == ReadEntityBodyMode.None)
					{
						return new SeekableBufferedRequestStream(requestBase);
					}
					if (requestBase.ReadEntityBodyMode == ReadEntityBodyMode.Classic)
					{
						requestBase.InputStream.Position = 0L;
						return requestBase.InputStream;
					}
					if (requestBase.ReadEntityBodyMode != ReadEntityBodyMode.Buffered)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SRResources.RequestBodyAlreadyReadInMode, new object[]
						{
							ReadEntityBodyMode.Bufferless
						}));
					}
					if (requestBase.GetBufferedInputStream().Position > 0L)
					{
						requestBase.InputStream.Position = 0L;
						return requestBase.InputStream;
					}
					return new SeekableBufferedRequestStream(requestBase);
				});
			}
			return new HttpControllerHandler.LazyStreamContent(delegate()
			{
				if (requestBase.ReadEntityBodyMode == ReadEntityBodyMode.None)
				{
					return requestBase.GetBufferlessInputStream();
				}
				if (requestBase.ReadEntityBodyMode == ReadEntityBodyMode.Classic)
				{
					throw new InvalidOperationException(SRResources.RequestStreamCannotBeReadBufferless);
				}
				if (requestBase.ReadEntityBodyMode != ReadEntityBodyMode.Bufferless)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SRResources.RequestBodyAlreadyReadInMode, new object[]
					{
						ReadEntityBodyMode.Buffered
					}));
				}
				Stream bufferlessInputStream = requestBase.GetBufferlessInputStream();
				if (bufferlessInputStream.Position > 0L)
				{
					throw new InvalidOperationException(SRResources.RequestBodyAlreadyRead);
				}
				return bufferlessInputStream;
			});
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005852 File Offset: 0x00003A52
		internal static void EnsureSuppressFormsAuthenticationRedirect(HttpContextBase httpContextBase)
		{
			if (httpContextBase.Response.StatusCode == 401)
			{
				HttpControllerHandler._suppressRedirectAction.Value(httpContextBase);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005878 File Offset: 0x00003A78
		private static Task WriteResponseContentAsync(HttpContextBase httpContextBase, HttpRequestMessage request, HttpResponseMessage response, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler, CancellationToken cancellationToken)
		{
			HttpResponseBase response2 = httpContextBase.Response;
			HttpContent content = response.Content;
			HttpControllerHandler.CopyHeaders(content.Headers, httpContextBase);
			if (!response2.BufferOutput)
			{
				return HttpControllerHandler.WriteStreamedResponseContentAsync(httpContextBase, request, response, exceptionLogger, cancellationToken);
			}
			return HttpControllerHandler.WriteBufferedResponseContentAsync(httpContextBase, request, response, exceptionLogger, exceptionHandler, cancellationToken);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005AD8 File Offset: 0x00003CD8
		internal static async Task WriteStreamedResponseContentAsync(HttpContextBase httpContextBase, HttpRequestMessage request, HttpResponseMessage response, IExceptionLogger exceptionLogger, CancellationToken cancellationToken)
		{
			Exception exception = null;
			cancellationToken.ThrowIfCancellationRequested();
			try
			{
				await response.Content.CopyToAsync(httpContextBase.Response.OutputStream);
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
			ExceptionContextCatchBlock catchBlock = WebHostExceptionCatchBlocks.HttpControllerHandlerStreamContent;
			ExceptionContext exceptionContext = new ExceptionContext(exception, catchBlock, request, response);
			await exceptionLogger.LogAsync(exceptionContext, cancellationToken);
			httpContextBase.Request.Abort();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005D5C File Offset: 0x00003F5C
		internal static async Task WriteBufferedResponseContentAsync(HttpContextBase httpContextBase, HttpRequestMessage request, HttpResponseMessage response, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler, CancellationToken cancellationToken)
		{
			HttpResponseBase httpResponseBase = httpContextBase.Response;
			cancellationToken.ThrowIfCancellationRequested();
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				await response.Content.CopyToAsync(httpResponseBase.OutputStream);
				return;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception source)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(source);
			}
			ExceptionContextCatchBlock catchBlock = WebHostExceptionCatchBlocks.HttpControllerHandlerBufferContent;
			if (!(await HttpControllerHandler.CopyErrorResponseAsync(catchBlock, httpContextBase, request, response, exceptionInfo.SourceException, exceptionLogger, exceptionHandler, cancellationToken)))
			{
				exceptionInfo.Throw();
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006134 File Offset: 0x00004334
		internal static async Task<bool> CopyErrorResponseAsync(ExceptionContextCatchBlock catchBlock, HttpContextBase httpContextBase, HttpRequestMessage request, HttpResponseMessage response, Exception exception, IExceptionLogger exceptionLogger, IExceptionHandler exceptionHandler, CancellationToken cancellationToken)
		{
			HttpResponseBase httpResponseBase = httpContextBase.Response;
			HttpResponseMessage errorResponse = null;
			HttpResponseException responseException = exception as HttpResponseException;
			HttpControllerHandler.ClearContentAndHeaders(httpResponseBase);
			if (responseException != null)
			{
				errorResponse = responseException.Response;
			}
			else
			{
				ExceptionContext exceptionContext = new ExceptionContext(exception, catchBlock, request)
				{
					Response = response
				};
				await exceptionLogger.LogAsync(exceptionContext, cancellationToken);
				errorResponse = await exceptionHandler.HandleAsync(exceptionContext, cancellationToken);
				if (errorResponse == null)
				{
					return false;
				}
			}
			bool result;
			if (!(await HttpControllerHandler.CopyResponseStatusAndHeadersAsync(httpContextBase, request, errorResponse, exceptionLogger, cancellationToken)))
			{
				result = true;
			}
			else if (errorResponse.Content == null)
			{
				errorResponse.Dispose();
				result = true;
			}
			else
			{
				HttpControllerHandler.CopyHeaders(errorResponse.Content.Headers, httpContextBase);
				await HttpControllerHandler.WriteErrorResponseContentAsync(httpResponseBase, request, errorResponse, cancellationToken, exceptionLogger);
				result = true;
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000063F8 File Offset: 0x000045F8
		private static async Task WriteErrorResponseContentAsync(HttpResponseBase httpResponseBase, HttpRequestMessage request, HttpResponseMessage errorResponse, CancellationToken cancellationToken, IExceptionLogger exceptionLogger)
		{
			try
			{
				Exception exception = null;
				cancellationToken.ThrowIfCancellationRequested();
				try
				{
					await errorResponse.Content.CopyToAsync(httpResponseBase.OutputStream);
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
				ExceptionContext exceptionContext = new ExceptionContext(exception, WebHostExceptionCatchBlocks.HttpControllerHandlerBufferError, request, errorResponse);
				await exceptionLogger.LogAsync(exceptionContext, cancellationToken);
				HttpControllerHandler.SetEmptyErrorResponse(httpResponseBase);
			}
			finally
			{
				errorResponse.Dispose();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000065CC File Offset: 0x000047CC
		private static async Task<bool> CopyResponseStatusAndHeadersAsync(HttpContextBase httpContextBase, HttpRequestMessage request, HttpResponseMessage response, IExceptionLogger exceptionLogger, CancellationToken cancellationToken)
		{
			HttpResponseBase httpResponseBase = httpContextBase.Response;
			httpResponseBase.StatusCode = (int)response.StatusCode;
			httpResponseBase.StatusDescription = response.ReasonPhrase;
			httpResponseBase.TrySkipIisCustomErrors = true;
			HttpControllerHandler.EnsureSuppressFormsAuthenticationRedirect(httpContextBase);
			bool result;
			if (!(await HttpControllerHandler.PrepareHeadersAsync(httpResponseBase, request, response, exceptionLogger, cancellationToken)))
			{
				result = false;
			}
			else
			{
				HttpControllerHandler.CopyHeaders(response.Headers, httpContextBase);
				result = true;
			}
			return result;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000689C File Offset: 0x00004A9C
		internal static async Task<bool> PrepareHeadersAsync(HttpResponseBase responseBase, HttpRequestMessage request, HttpResponseMessage response, IExceptionLogger exceptionLogger, CancellationToken cancellationToken)
		{
			HttpResponseHeaders responseHeaders = response.Headers;
			HttpContent content = response.Content;
			bool isTransferEncodingChunked = responseHeaders.TransferEncodingChunked == true;
			HttpHeaderValueCollection<TransferCodingHeaderValue> transferEncoding = responseHeaders.TransferEncoding;
			if (content != null)
			{
				HttpContentHeaders contentHeaders = content.Headers;
				if (isTransferEncodingChunked)
				{
					contentHeaders.ContentLength = null;
				}
				else
				{
					Exception exception = null;
					try
					{
						long? contentLength = contentHeaders.ContentLength;
					}
					catch (Exception ex)
					{
						exception = ex;
					}
					if (exception != null)
					{
						ExceptionContext exceptionContext = new ExceptionContext(exception, WebHostExceptionCatchBlocks.HttpControllerHandlerComputeContentLength, request, response);
						await exceptionLogger.LogAsync(exceptionContext, cancellationToken);
						HttpControllerHandler.SetEmptyErrorResponse(responseBase);
						return false;
					}
				}
				bool isBuffered = HttpControllerHandler._bufferPolicySelector.Value == null || HttpControllerHandler._bufferPolicySelector.Value.UseBufferedOutputStream(response);
				responseBase.BufferOutput = isBuffered;
			}
			if (isTransferEncodingChunked && transferEncoding.Count == 1)
			{
				transferEncoding.Clear();
				responseBase.BufferOutput = false;
			}
			return true;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006903 File Offset: 0x00004B03
		private static void ClearContentAndHeaders(HttpResponseBase httpResponseBase)
		{
			httpResponseBase.Clear();
			httpResponseBase.ClearHeaders();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006911 File Offset: 0x00004B11
		private static void SetEmptyErrorResponse(HttpResponseBase httpResponseBase)
		{
			HttpControllerHandler.ClearContentAndHeaders(httpResponseBase);
			httpResponseBase.StatusCode = 500;
			httpResponseBase.SuppressContent = true;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000692C File Offset: 0x00004B2C
		private static X509Certificate2 RetrieveClientCertificate(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			X509Certificate2 result = null;
			HttpContextBase httpContext = request.GetHttpContext();
			if (httpContext != null && httpContext.Request.ClientCertificate.Certificate != null && httpContext.Request.ClientCertificate.Certificate.Length > 0)
			{
				result = new X509Certificate2(httpContext.Request.ClientCertificate.Certificate);
			}
			return result;
		}

		// Token: 0x0400003B RID: 59
		internal static readonly string OwinEnvironmentHttpContextKey = "owin.Environment";

		// Token: 0x0400003C RID: 60
		internal static readonly string OwinEnvironmentKey = "MS_OwinEnvironment";

		// Token: 0x0400003D RID: 61
		private static readonly Lazy<Action<HttpContextBase>> _suppressRedirectAction = new Lazy<Action<HttpContextBase>>(delegate()
		{
			if (!SuppressFormsAuthRedirectHelper.GetEnabled(WebConfigurationManager.AppSettings))
			{
				return delegate(HttpContextBase httpContext)
				{
				};
			}
			return delegate(HttpContextBase httpContext)
			{
				httpContext.Response.SuppressFormsAuthenticationRedirect = true;
			};
		});

		// Token: 0x0400003E RID: 62
		private static readonly Lazy<IHostBufferPolicySelector> _bufferPolicySelector = new Lazy<IHostBufferPolicySelector>(() => GlobalConfiguration.Configuration.Services.GetHostBufferPolicySelector());

		// Token: 0x0400003F RID: 63
		private static readonly Lazy<IExceptionHandler> _exceptionHandler = new Lazy<IExceptionHandler>(() => ExceptionServices.GetHandler(GlobalConfiguration.Configuration));

		// Token: 0x04000040 RID: 64
		private static readonly Lazy<IExceptionLogger> _exceptionLogger = new Lazy<IExceptionLogger>(() => ExceptionServices.GetLogger(GlobalConfiguration.Configuration));

		// Token: 0x04000041 RID: 65
		private static readonly Func<HttpRequestMessage, X509Certificate2> _retrieveClientCertificate = new Func<HttpRequestMessage, X509Certificate2>(HttpControllerHandler.RetrieveClientCertificate);

		// Token: 0x04000042 RID: 66
		private readonly IHttpRouteData _routeData;

		// Token: 0x04000043 RID: 67
		private readonly HttpMessageInvoker _server;

		// Token: 0x02000025 RID: 37
		private class DelegatingStreamContent : StreamContent
		{
			// Token: 0x06000111 RID: 273 RVA: 0x00006AF2 File Offset: 0x00004CF2
			public DelegatingStreamContent(Stream stream) : base(stream)
			{
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00006AFB File Offset: 0x00004CFB
			public Task WriteToStreamAsync(Stream stream, TransportContext context)
			{
				return this.SerializeToStreamAsync(stream, context);
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00006B05 File Offset: 0x00004D05
			public bool TryCalculateLength(out long length)
			{
				return this.TryComputeLength(out length);
			}

			// Token: 0x06000114 RID: 276 RVA: 0x00006B0E File Offset: 0x00004D0E
			public Task<Stream> GetContentReadStreamAsync()
			{
				return this.CreateContentReadStreamAsync();
			}
		}

		// Token: 0x02000026 RID: 38
		private class LazyStreamContent : HttpContent
		{
			// Token: 0x06000115 RID: 277 RVA: 0x00006B16 File Offset: 0x00004D16
			public LazyStreamContent(Func<Stream> getStream)
			{
				this._getStream = getStream;
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000116 RID: 278 RVA: 0x00006B25 File Offset: 0x00004D25
			private HttpControllerHandler.DelegatingStreamContent StreamContent
			{
				get
				{
					if (this._streamContent == null)
					{
						this._streamContent = new HttpControllerHandler.DelegatingStreamContent(this._getStream());
					}
					return this._streamContent;
				}
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00006B4B File Offset: 0x00004D4B
			protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
			{
				return this.StreamContent.WriteToStreamAsync(stream, context);
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00006B5A File Offset: 0x00004D5A
			protected override Task<Stream> CreateContentReadStreamAsync()
			{
				return this.StreamContent.GetContentReadStreamAsync();
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00006B67 File Offset: 0x00004D67
			protected override bool TryComputeLength(out long length)
			{
				return this.StreamContent.TryCalculateLength(out length);
			}

			// Token: 0x0400004A RID: 74
			private readonly Func<Stream> _getStream;

			// Token: 0x0400004B RID: 75
			private HttpControllerHandler.DelegatingStreamContent _streamContent;
		}
	}
}
