using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000012 RID: 18
	[__DynamicallyInvokable]
	public class HttpClient : HttpMessageInvoker
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000048EF File Offset: 0x00002AEF
		[__DynamicallyInvokable]
		public HttpRequestHeaders DefaultRequestHeaders
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.defaultRequestHeaders == null)
				{
					this.defaultRequestHeaders = new HttpRequestHeaders();
				}
				return this.defaultRequestHeaders;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000490A File Offset: 0x00002B0A
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004914 File Offset: 0x00002B14
		[__DynamicallyInvokable]
		public Uri BaseAddress
		{
			[__DynamicallyInvokable]
			get
			{
				return this.baseAddress;
			}
			[__DynamicallyInvokable]
			set
			{
				HttpClient.CheckBaseAddress(value, "value");
				this.CheckDisposedOrStarted();
				if (Logging.On)
				{
					TraceSource http = Logging.Http;
					string str = "BaseAddress: '";
					Uri uri = this.baseAddress;
					Logging.PrintInfo(http, this, str + ((uri != null) ? uri.ToString() : null) + "'");
				}
				this.baseAddress = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000496C File Offset: 0x00002B6C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004974 File Offset: 0x00002B74
		[__DynamicallyInvokable]
		public TimeSpan Timeout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.timeout;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != HttpClient.infiniteTimeout && (value <= TimeSpan.Zero || value > HttpClient.maxTimeout))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckDisposedOrStarted();
				this.timeout = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000049C0 File Offset: 0x00002BC0
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000049C8 File Offset: 0x00002BC8
		[__DynamicallyInvokable]
		public long MaxResponseContentBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxResponseContentBufferSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value <= 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", value, string.Format(CultureInfo.InvariantCulture, SR.net_http_content_buffersize_limit, new object[]
					{
						2147483647L
					}));
				}
				this.CheckDisposedOrStarted();
				this.maxResponseContentBufferSize = value;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004A2F File Offset: 0x00002C2F
		[__DynamicallyInvokable]
		public HttpClient() : this(new HttpClientHandler())
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004A3C File Offset: 0x00002C3C
		[__DynamicallyInvokable]
		public HttpClient(HttpMessageHandler handler) : this(handler, true)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004A48 File Offset: 0x00002C48
		[__DynamicallyInvokable]
		public HttpClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, ".ctor", handler);
			}
			this.timeout = HttpClient.defaultTimeout;
			this.maxResponseContentBufferSize = 2147483647L;
			this.pendingRequestsCts = new CancellationTokenSource();
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, ".ctor", null);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004AAF File Offset: 0x00002CAF
		[__DynamicallyInvokable]
		public Task<string> GetStringAsync(string requestUri)
		{
			return this.GetStringAsync(this.CreateUri(requestUri));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004ABE File Offset: 0x00002CBE
		[__DynamicallyInvokable]
		public Task<string> GetStringAsync(Uri requestUri)
		{
			return this.GetContentAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, string.Empty, (HttpContent content) => content.ReadAsStringAsync());
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004AEC File Offset: 0x00002CEC
		[__DynamicallyInvokable]
		public Task<byte[]> GetByteArrayAsync(string requestUri)
		{
			return this.GetByteArrayAsync(this.CreateUri(requestUri));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004AFB File Offset: 0x00002CFB
		[__DynamicallyInvokable]
		public Task<byte[]> GetByteArrayAsync(Uri requestUri)
		{
			return this.GetContentAsync<byte[]>(requestUri, HttpCompletionOption.ResponseContentRead, HttpUtilities.EmptyByteArray, (HttpContent content) => content.ReadAsByteArrayAsync());
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004B29 File Offset: 0x00002D29
		[__DynamicallyInvokable]
		public Task<Stream> GetStreamAsync(string requestUri)
		{
			return this.GetStreamAsync(this.CreateUri(requestUri));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004B38 File Offset: 0x00002D38
		[__DynamicallyInvokable]
		public Task<Stream> GetStreamAsync(Uri requestUri)
		{
			return this.GetContentAsync<Stream>(requestUri, HttpCompletionOption.ResponseHeadersRead, Stream.Null, (HttpContent content) => content.ReadAsStreamAsync());
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004B68 File Offset: 0x00002D68
		private Task<T> GetContentAsync<T>(Uri requestUri, HttpCompletionOption completionOption, T defaultValue, Func<HttpContent, Task<T>> readAs)
		{
			TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
			Action<Task<T>> <>9__1;
			this.GetAsync(requestUri, completionOption).ContinueWithStandard(delegate(Task<HttpResponseMessage> requestTask)
			{
				if (HttpClient.HandleRequestFaultsAndCancelation<T>(requestTask, tcs))
				{
					return;
				}
				HttpResponseMessage result = requestTask.Result;
				if (result.Content == null)
				{
					tcs.TrySetResult(defaultValue);
					return;
				}
				try
				{
					Task<T> task = readAs(result.Content);
					Action<Task<T>> continuation;
					if ((continuation = <>9__1) == null)
					{
						continuation = (<>9__1 = delegate(Task<T> contentTask)
						{
							if (!HttpUtilities.HandleFaultsAndCancelation<T>(contentTask, tcs))
							{
								tcs.TrySetResult(contentTask.Result);
							}
						});
					}
					task.ContinueWithStandard(continuation);
				}
				catch (Exception exception)
				{
					tcs.TrySetException(exception);
				}
			});
			return tcs.Task;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004BBA File Offset: 0x00002DBA
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(string requestUri)
		{
			return this.GetAsync(this.CreateUri(requestUri));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004BC9 File Offset: 0x00002DC9
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(Uri requestUri)
		{
			return this.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004BD3 File Offset: 0x00002DD3
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
		{
			return this.GetAsync(this.CreateUri(requestUri), completionOption);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004BE3 File Offset: 0x00002DE3
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
		{
			return this.GetAsync(requestUri, completionOption, CancellationToken.None);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004BF2 File Offset: 0x00002DF2
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
		{
			return this.GetAsync(this.CreateUri(requestUri), cancellationToken);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004C02 File Offset: 0x00002E02
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return this.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004C0D File Offset: 0x00002E0D
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return this.GetAsync(this.CreateUri(requestUri), completionOption, cancellationToken);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004C1E File Offset: 0x00002E1E
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri), completionOption, cancellationToken);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004C33 File Offset: 0x00002E33
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
		{
			return this.PostAsync(this.CreateUri(requestUri), content);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004C43 File Offset: 0x00002E43
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
		{
			return this.PostAsync(requestUri, content, CancellationToken.None);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004C52 File Offset: 0x00002E52
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.PostAsync(this.CreateUri(requestUri), content, cancellationToken);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004C64 File Offset: 0x00002E64
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004C8C File Offset: 0x00002E8C
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
		{
			return this.PutAsync(this.CreateUri(requestUri), content);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004C9C File Offset: 0x00002E9C
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
		{
			return this.PutAsync(requestUri, content, CancellationToken.None);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004CAB File Offset: 0x00002EAB
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.PutAsync(this.CreateUri(requestUri), content, cancellationToken);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004CBC File Offset: 0x00002EBC
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = content
			}, cancellationToken);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004CE4 File Offset: 0x00002EE4
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> DeleteAsync(string requestUri)
		{
			return this.DeleteAsync(this.CreateUri(requestUri));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004CF3 File Offset: 0x00002EF3
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
		{
			return this.DeleteAsync(requestUri, CancellationToken.None);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004D01 File Offset: 0x00002F01
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
		{
			return this.DeleteAsync(this.CreateUri(requestUri), cancellationToken);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004D11 File Offset: 0x00002F11
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
		{
			return this.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri), cancellationToken);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004D25 File Offset: 0x00002F25
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return this.SendAsync(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004D34 File Offset: 0x00002F34
		[__DynamicallyInvokable]
		public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return this.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004D3F File Offset: 0x00002F3F
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
		{
			return this.SendAsync(request, completionOption, CancellationToken.None);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004D50 File Offset: 0x00002F50
		[__DynamicallyInvokable]
		public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.CheckDisposed();
			HttpClient.CheckRequestMessage(request);
			this.SetOperationStarted();
			this.PrepareRequestMessage(request);
			CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.pendingRequestsCts.Token);
			this.SetTimeout(linkedCts);
			TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
			base.SendAsync(request, linkedCts.Token).ContinueWithStandard(delegate(Task<HttpResponseMessage> task)
			{
				try
				{
					this.DisposeRequestContent(request);
					if (task.IsFaulted)
					{
						this.SetTaskFaulted(request, linkedCts, tcs, task.Exception.GetBaseException());
					}
					else if (task.IsCanceled)
					{
						this.SetTaskCanceled(request, linkedCts, tcs);
					}
					else
					{
						HttpResponseMessage result = task.Result;
						if (result == null)
						{
							this.SetTaskFaulted(request, linkedCts, tcs, new InvalidOperationException(SR.net_http_handler_noresponse));
						}
						else if (result.Content == null || completionOption == HttpCompletionOption.ResponseHeadersRead)
						{
							this.SetTaskCompleted(request, linkedCts, tcs, result);
						}
						else
						{
							this.StartContentBuffering(request, linkedCts, tcs, result);
						}
					}
				}
				catch (Exception ex)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.Http, this, "SendAsync", ex);
					}
					tcs.TrySetException(ex);
				}
			});
			return tcs.Task;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004E10 File Offset: 0x00003010
		[__DynamicallyInvokable]
		public void CancelPendingRequests()
		{
			this.CheckDisposed();
			if (Logging.On)
			{
				Logging.Enter(Logging.Http, this, "CancelPendingRequests", "");
			}
			CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.pendingRequestsCts, new CancellationTokenSource());
			cancellationTokenSource.Cancel();
			cancellationTokenSource.Dispose();
			if (Logging.On)
			{
				Logging.Exit(Logging.Http, this, "CancelPendingRequests", "");
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004E78 File Offset: 0x00003078
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				this.pendingRequestsCts.Cancel();
				this.pendingRequestsCts.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004EB0 File Offset: 0x000030B0
		private void DisposeRequestContent(HttpRequestMessage request)
		{
			HttpContent content = request.Content;
			if (content != null)
			{
				content.Dispose();
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004ED0 File Offset: 0x000030D0
		private void StartContentBuffering(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs, HttpResponseMessage response)
		{
			response.Content.LoadIntoBufferAsync(this.maxResponseContentBufferSize).ContinueWithStandard(delegate(Task contentTask)
			{
				try
				{
					bool isCancellationRequested = cancellationTokenSource.Token.IsCancellationRequested;
					if (contentTask.IsFaulted)
					{
						response.Dispose();
						if (isCancellationRequested && contentTask.Exception.GetBaseException() is HttpRequestException)
						{
							this.SetTaskCanceled(request, cancellationTokenSource, tcs);
						}
						else
						{
							this.SetTaskFaulted(request, cancellationTokenSource, tcs, contentTask.Exception.GetBaseException());
						}
					}
					else if (contentTask.IsCanceled)
					{
						response.Dispose();
						this.SetTaskCanceled(request, cancellationTokenSource, tcs);
					}
					else
					{
						this.SetTaskCompleted(request, cancellationTokenSource, tcs, response);
					}
				}
				catch (Exception ex)
				{
					response.Dispose();
					tcs.TrySetException(ex);
					if (Logging.On)
					{
						Logging.Exception(Logging.Http, this, "SendAsync", ex);
					}
				}
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004F2F File Offset: 0x0000312F
		private void SetOperationStarted()
		{
			if (!this.operationStarted)
			{
				this.operationStarted = true;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004F44 File Offset: 0x00003144
		private void CheckDisposedOrStarted()
		{
			this.CheckDisposed();
			if (this.operationStarted)
			{
				throw new InvalidOperationException(SR.net_http_operation_started);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004F61 File Offset: 0x00003161
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004F7E File Offset: 0x0000317E
		private static void CheckRequestMessage(HttpRequestMessage request)
		{
			if (!request.MarkAsSent())
			{
				throw new InvalidOperationException(SR.net_http_client_request_already_sent);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004F94 File Offset: 0x00003194
		private void PrepareRequestMessage(HttpRequestMessage request)
		{
			Uri uri = null;
			if (request.RequestUri == null && this.baseAddress == null)
			{
				throw new InvalidOperationException(SR.net_http_client_invalid_requesturi);
			}
			if (request.RequestUri == null)
			{
				uri = this.baseAddress;
			}
			else if (!request.RequestUri.IsAbsoluteUri)
			{
				if (this.baseAddress == null)
				{
					throw new InvalidOperationException(SR.net_http_client_invalid_requesturi);
				}
				uri = new Uri(this.baseAddress, request.RequestUri);
			}
			if (uri != null)
			{
				request.RequestUri = uri;
			}
			if (this.defaultRequestHeaders != null)
			{
				request.Headers.AddHeaders(this.defaultRequestHeaders);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005042 File Offset: 0x00003242
		private static void CheckBaseAddress(Uri baseAddress, string parameterName)
		{
			if (baseAddress == null)
			{
				return;
			}
			if (!baseAddress.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.net_http_client_absolute_baseaddress_required, parameterName);
			}
			if (!HttpUtilities.IsHttpUri(baseAddress))
			{
				throw new ArgumentException(SR.net_http_client_http_baseaddress_required, parameterName);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005076 File Offset: 0x00003276
		private void SetTaskFaulted(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs, Exception e)
		{
			this.LogSendError(request, cancellationTokenSource, "SendAsync", e);
			tcs.TrySetException(e);
			cancellationTokenSource.Dispose();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005096 File Offset: 0x00003296
		private void SetTaskCanceled(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs)
		{
			this.LogSendError(request, cancellationTokenSource, "SendAsync", null);
			tcs.TrySetCanceled(cancellationTokenSource.Token);
			cancellationTokenSource.Dispose();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000050BC File Offset: 0x000032BC
		private void SetTaskCompleted(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs, HttpResponseMessage response)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Http, this, string.Format(CultureInfo.InvariantCulture, SR.net_http_client_send_completed, new object[]
				{
					Logging.GetObjectLogHash(request),
					Logging.GetObjectLogHash(response),
					response
				}));
			}
			tcs.TrySetResult(response);
			cancellationTokenSource.Dispose();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005117 File Offset: 0x00003317
		private void SetTimeout(CancellationTokenSource cancellationTokenSource)
		{
			if (this.timeout != HttpClient.infiniteTimeout)
			{
				cancellationTokenSource.CancelAfter(this.timeout);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005138 File Offset: 0x00003338
		private void LogSendError(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, string method, Exception e)
		{
			if (cancellationTokenSource.IsCancellationRequested)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Http, this, method, string.Format(CultureInfo.InvariantCulture, SR.net_http_client_send_canceled, new object[]
					{
						Logging.GetObjectLogHash(request)
					}));
					return;
				}
			}
			else if (Logging.On)
			{
				Logging.PrintError(Logging.Http, this, method, string.Format(CultureInfo.InvariantCulture, SR.net_http_client_send_error, new object[]
				{
					Logging.GetObjectLogHash(request),
					e
				}));
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000051B5 File Offset: 0x000033B5
		private Uri CreateUri(string uri)
		{
			if (string.IsNullOrEmpty(uri))
			{
				return null;
			}
			return new Uri(uri, UriKind.RelativeOrAbsolute);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000051C8 File Offset: 0x000033C8
		private static bool HandleRequestFaultsAndCancelation<T>(Task<HttpResponseMessage> task, TaskCompletionSource<T> tcs)
		{
			if (HttpUtilities.HandleFaultsAndCancelation<T>(task, tcs))
			{
				return true;
			}
			HttpResponseMessage result = task.Result;
			if (!result.IsSuccessStatusCode)
			{
				if (result.Content != null)
				{
					result.Content.Dispose();
				}
				tcs.TrySetException(new HttpRequestException(string.Format(CultureInfo.InvariantCulture, SR.net_http_message_not_success_statuscode, new object[]
				{
					(int)result.StatusCode,
					result.ReasonPhrase
				})));
				return true;
			}
			return false;
		}

		// Token: 0x0400008F RID: 143
		private static readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(100.0);

		// Token: 0x04000090 RID: 144
		private static readonly TimeSpan maxTimeout = TimeSpan.FromMilliseconds(2147483647.0);

		// Token: 0x04000091 RID: 145
		private static readonly TimeSpan infiniteTimeout = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x04000092 RID: 146
		private const HttpCompletionOption defaultCompletionOption = HttpCompletionOption.ResponseContentRead;

		// Token: 0x04000093 RID: 147
		private volatile bool operationStarted;

		// Token: 0x04000094 RID: 148
		private volatile bool disposed;

		// Token: 0x04000095 RID: 149
		private CancellationTokenSource pendingRequestsCts;

		// Token: 0x04000096 RID: 150
		private HttpRequestHeaders defaultRequestHeaders;

		// Token: 0x04000097 RID: 151
		private Uri baseAddress;

		// Token: 0x04000098 RID: 152
		private TimeSpan timeout;

		// Token: 0x04000099 RID: 153
		private long maxResponseContentBufferSize;
	}
}
