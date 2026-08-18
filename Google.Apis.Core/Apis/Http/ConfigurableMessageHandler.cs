using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Logging;
using Google.Apis.Testing;
using Google.Apis.Util;

namespace Google.Apis.Http
{
	// Token: 0x02000026 RID: 38
	public class ConfigurableMessageHandler : DelegatingHandler
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003B18 File Offset: 0x00001D18
		[Obsolete("Use AddUnsuccessfulResponseHandler or RemoveUnsuccessfulResponseHandler instead.")]
		public IList<IHttpUnsuccessfulResponseHandler> UnsuccessfulResponseHandlers
		{
			get
			{
				return this.unsuccessfulResponseHandlers;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003B20 File Offset: 0x00001D20
		public void AddUnsuccessfulResponseHandler(IHttpUnsuccessfulResponseHandler handler)
		{
			object obj = this.unsuccessfulResponseHandlersLock;
			lock (obj)
			{
				this.unsuccessfulResponseHandlers.Add(handler);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003B68 File Offset: 0x00001D68
		public void RemoveUnsuccessfulResponseHandler(IHttpUnsuccessfulResponseHandler handler)
		{
			object obj = this.unsuccessfulResponseHandlersLock;
			lock (obj)
			{
				this.unsuccessfulResponseHandlers.Remove(handler);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003BB0 File Offset: 0x00001DB0
		[Obsolete("Use AddExceptionHandler or RemoveExceptionHandler instead.")]
		public IList<IHttpExceptionHandler> ExceptionHandlers
		{
			get
			{
				return this.exceptionHandlers;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public void AddExceptionHandler(IHttpExceptionHandler handler)
		{
			object obj = this.exceptionHandlersLock;
			lock (obj)
			{
				this.exceptionHandlers.Add(handler);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003C00 File Offset: 0x00001E00
		public void RemoveExceptionHandler(IHttpExceptionHandler handler)
		{
			object obj = this.exceptionHandlersLock;
			lock (obj)
			{
				this.exceptionHandlers.Remove(handler);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003C48 File Offset: 0x00001E48
		[Obsolete("Use AddExecuteInterceptor or RemoveExecuteInterceptor instead.")]
		public IList<IHttpExecuteInterceptor> ExecuteInterceptors
		{
			get
			{
				return this.executeInterceptors;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003C50 File Offset: 0x00001E50
		public void AddExecuteInterceptor(IHttpExecuteInterceptor interceptor)
		{
			object obj = this.executeInterceptorsLock;
			lock (obj)
			{
				this.executeInterceptors.Add(interceptor);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003C98 File Offset: 0x00001E98
		public void RemoveExecuteInterceptor(IHttpExecuteInterceptor interceptor)
		{
			object obj = this.executeInterceptorsLock;
			lock (obj)
			{
				this.executeInterceptors.Remove(interceptor);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003CE0 File Offset: 0x00001EE0
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003CE8 File Offset: 0x00001EE8
		internal ILogger InstanceLogger
		{
			get
			{
				return this._instanceLogger;
			}
			set
			{
				this._instanceLogger = value.ForType<ConfigurableMessageHandler>();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003CF6 File Offset: 0x00001EF6
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003CFE File Offset: 0x00001EFE
		public int NumTries
		{
			get
			{
				return this.numTries;
			}
			set
			{
				if (value > 20 || value < 1)
				{
					throw new ArgumentOutOfRangeException("NumTries");
				}
				this.numTries = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003D1B File Offset: 0x00001F1B
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003D23 File Offset: 0x00001F23
		public int NumRedirects
		{
			get
			{
				return this.numRedirects;
			}
			set
			{
				if (value > 20 || value < 1)
				{
					throw new ArgumentOutOfRangeException("NumRedirects");
				}
				this.numRedirects = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003D40 File Offset: 0x00001F40
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003D48 File Offset: 0x00001F48
		public bool FollowRedirect { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003D51 File Offset: 0x00001F51
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003D59 File Offset: 0x00001F59
		public bool IsLoggingEnabled { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003D62 File Offset: 0x00001F62
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003D6A File Offset: 0x00001F6A
		public ConfigurableMessageHandler.LogEventType LogEvents { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003D73 File Offset: 0x00001F73
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003D7B File Offset: 0x00001F7B
		public string ApplicationName { get; set; }

		// Token: 0x060000E0 RID: 224 RVA: 0x00003D84 File Offset: 0x00001F84
		public ConfigurableMessageHandler(HttpMessageHandler httpMessageHandler) : base(httpMessageHandler)
		{
			this.FollowRedirect = true;
			this.IsLoggingEnabled = true;
			this.LogEvents = (ConfigurableMessageHandler.LogEventType.RequestUri | ConfigurableMessageHandler.LogEventType.ResponseStatus | ConfigurableMessageHandler.LogEventType.ResponseAbnormal);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003E0C File Offset: 0x0000200C
		private void LogHeaders(string initialText, HttpHeaders headers1, HttpHeaders headers2)
		{
			List<KeyValuePair<string, IEnumerable<string>>> list = (headers1 ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>()).Concat(headers2 ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>()).ToList<KeyValuePair<string, IEnumerable<string>>>();
			object[] array = new object[list.Count * 2];
			StringBuilder stringBuilder = new StringBuilder(list.Count * 32);
			stringBuilder.Append(initialText);
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append(string.Format("\n  [{{{0}}}] '{{{1}}}'", i * 2, 1 + i * 2));
				array[i * 2] = list[i].Key;
				stringBuilder2.Clear();
				array[1 + i * 2] = string.Join("; ", list[i].Value);
			}
			this.InstanceLogger.Debug(stringBuilder.ToString(), array);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003EF8 File Offset: 0x000020F8
		private async Task LogBody(string fmtText, HttpContent content)
		{
			byte[] array;
			if (content != null)
			{
				array = await content.ReadAsByteArrayAsync();
			}
			else
			{
				array = new byte[0];
			}
			byte[] array2 = array;
			char[] array3 = new char[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				array3[i] = (char)((b >= 32 && b <= 126) ? b : 46);
			}
			this.InstanceLogger.Debug(fmtText, new object[]
			{
				new string(array3)
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F50 File Offset: 0x00002150
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			bool loggable = this.IsLoggingEnabled && this.InstanceLogger.IsDebugEnabled;
			string loggingRequestId = "";
			if (loggable)
			{
				loggingRequestId = Interlocked.Increment(ref this._loggingRequestId).ToString("X8");
			}
			int triesRemaining = this.NumTries;
			int redirectRemaining = this.NumRedirects;
			Exception lastException = null;
			string value = ((this.ApplicationName == null) ? "" : (this.ApplicationName + " ")) + ConfigurableMessageHandler.UserAgentSuffix;
			request.Headers.Add("User-Agent", value);
			HttpResponseMessage response = null;
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (response != null)
				{
					response.Dispose();
					response = null;
				}
				lastException = null;
				object obj = this.executeInterceptorsLock;
				IEnumerable<IHttpExecuteInterceptor> enumerable;
				lock (obj)
				{
					enumerable = this.executeInterceptors.ToList<IHttpExecuteInterceptor>();
				}
				foreach (IHttpExecuteInterceptor httpExecuteInterceptor in enumerable)
				{
					await httpExecuteInterceptor.InterceptAsync(request, cancellationToken).ConfigureAwait(false);
				}
				IEnumerator<IHttpExecuteInterceptor> enumerator = null;
				if (loggable)
				{
					if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.RequestUri) != ConfigurableMessageHandler.LogEventType.None)
					{
						this.InstanceLogger.Debug("Request[{0}] (triesRemaining={1}) URI: '{2}'", new object[]
						{
							loggingRequestId,
							triesRemaining,
							request.RequestUri
						});
					}
					if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.RequestHeaders) != ConfigurableMessageHandler.LogEventType.None)
					{
						string initialText = string.Format("Request[{0}] Headers:", loggingRequestId);
						HttpHeaders headers = request.Headers;
						HttpContent content = request.Content;
						this.LogHeaders(initialText, headers, (content != null) ? content.Headers : null);
					}
					if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.RequestBody) != ConfigurableMessageHandler.LogEventType.None)
					{
						await this.LogBody(string.Format("Request[{0}] Body: '{{0}}'", loggingRequestId), request.Content);
					}
				}
				try
				{
					response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
				}
				catch (Exception lastException)
				{
				}
				if (response == null || response.StatusCode >= HttpStatusCode.BadRequest || response.StatusCode < HttpStatusCode.OK)
				{
					int num = triesRemaining;
					triesRemaining = num - 1;
				}
				if (response == null)
				{
					bool flag2 = false;
					obj = this.exceptionHandlersLock;
					IEnumerable<IHttpExceptionHandler> enumerable2;
					lock (obj)
					{
						enumerable2 = this.exceptionHandlers.ToList<IHttpExceptionHandler>();
					}
					foreach (IHttpExceptionHandler httpExceptionHandler in enumerable2)
					{
						bool flag3 = flag2;
						bool flag = await httpExceptionHandler.HandleExceptionAsync(new HandleExceptionArgs
						{
							Request = request,
							Exception = lastException,
							TotalTries = this.NumTries,
							CurrentFailedTry = this.NumTries - triesRemaining,
							CancellationToken = cancellationToken
						}).ConfigureAwait(false);
						flag2 = (flag3 || flag);
					}
					IEnumerator<IHttpExceptionHandler> enumerator2 = null;
					if (!flag2)
					{
						break;
					}
					if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
					{
						this.InstanceLogger.Debug("Response[{0}] Exception {1} was thrown, but it was handled by an exception handler", new object[]
						{
							loggingRequestId,
							lastException.Message
						});
					}
				}
				else
				{
					if (loggable)
					{
						if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseStatus) != ConfigurableMessageHandler.LogEventType.None)
						{
							this.InstanceLogger.Debug("Response[{0}] Response status: {1} '{2}'", new object[]
							{
								loggingRequestId,
								response.StatusCode,
								response.ReasonPhrase
							});
						}
						if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseHeaders) != ConfigurableMessageHandler.LogEventType.None)
						{
							string initialText2 = string.Format("Response[{0}] Headers:", loggingRequestId);
							HttpHeaders headers2 = response.Headers;
							HttpContent content2 = response.Content;
							this.LogHeaders(initialText2, headers2, (content2 != null) ? content2.Headers : null);
						}
						if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseBody) != ConfigurableMessageHandler.LogEventType.None)
						{
							await this.LogBody(string.Format("Response[{0}] Body: '{{0}}'", loggingRequestId), response.Content);
						}
					}
					if (response.IsSuccessStatusCode)
					{
						triesRemaining = 0;
					}
					else
					{
						bool flag4 = false;
						obj = this.unsuccessfulResponseHandlersLock;
						IEnumerable<IHttpUnsuccessfulResponseHandler> enumerable3;
						lock (obj)
						{
							enumerable3 = this.unsuccessfulResponseHandlers.ToList<IHttpUnsuccessfulResponseHandler>();
						}
						foreach (IHttpUnsuccessfulResponseHandler httpUnsuccessfulResponseHandler in enumerable3)
						{
							bool flag3 = flag4;
							bool flag = await httpUnsuccessfulResponseHandler.HandleResponseAsync(new HandleUnsuccessfulResponseArgs
							{
								Request = request,
								Response = response,
								TotalTries = this.NumTries,
								CurrentFailedTry = this.NumTries - triesRemaining,
								CancellationToken = cancellationToken
							}).ConfigureAwait(false);
							flag4 = (flag3 || flag);
						}
						IEnumerator<IHttpUnsuccessfulResponseHandler> enumerator3 = null;
						if (!flag4)
						{
							if (this.FollowRedirect && this.HandleRedirect(response))
							{
								int num = redirectRemaining;
								redirectRemaining = num - 1;
								if (num == 0)
								{
									triesRemaining = 0;
								}
								flag4 = true;
								if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
								{
									this.InstanceLogger.Debug("Response[{0}] Redirect response was handled successfully. Redirect to {1}", new object[]
									{
										loggingRequestId,
										response.Headers.Location
									});
								}
							}
							else
							{
								if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
								{
									this.InstanceLogger.Debug("Response[{0}] An abnormal response wasn't handled. Status code is {1}", new object[]
									{
										loggingRequestId,
										response.StatusCode
									});
								}
								triesRemaining = 0;
							}
						}
						else if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
						{
							this.InstanceLogger.Debug("Response[{0}] An abnormal response was handled by an unsuccessful response handler. Status Code is {1}", new object[]
							{
								loggingRequestId,
								response.StatusCode
							});
						}
					}
				}
				if (triesRemaining <= 0)
				{
					goto Block_39;
				}
			}
			this.InstanceLogger.Error(lastException, "Response[{0}] Exception was thrown while executing a HTTP request and it wasn't handled", new object[]
			{
				loggingRequestId
			});
			throw lastException;
			Block_39:
			if (response == null)
			{
				this.InstanceLogger.Error(lastException, "Request[{0}] Exception was thrown while executing a HTTP request", new object[]
				{
					loggingRequestId
				});
				throw lastException;
			}
			if (!response.IsSuccessStatusCode && loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
			{
				this.InstanceLogger.Debug("Response[{0}] Abnormal response is being returned. Status Code is {1}", new object[]
				{
					loggingRequestId,
					response.StatusCode
				});
			}
			return response;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003FA8 File Offset: 0x000021A8
		private bool HandleRedirect(HttpResponseMessage message)
		{
			Uri location = message.Headers.Location;
			if (!message.IsRedirectStatusCode() || location == null)
			{
				return false;
			}
			HttpRequestMessage requestMessage = message.RequestMessage;
			requestMessage.RequestUri = new Uri(requestMessage.RequestUri, location);
			if (message.StatusCode == HttpStatusCode.SeeOther)
			{
				requestMessage.Method = HttpMethod.Get;
			}
			requestMessage.Headers.Remove("Authorization");
			requestMessage.Headers.IfMatch.Clear();
			requestMessage.Headers.IfNoneMatch.Clear();
			requestMessage.Headers.IfModifiedSince = null;
			requestMessage.Headers.IfUnmodifiedSince = null;
			requestMessage.Headers.Remove("If-Range");
			return true;
		}

		// Token: 0x04000043 RID: 67
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ConfigurableMessageHandler>();

		// Token: 0x04000044 RID: 68
		[VisibleForTestOnly]
		public const int MaxAllowedNumTries = 20;

		// Token: 0x04000045 RID: 69
		private static readonly string ApiVersion = Utilities.GetLibraryVersion();

		// Token: 0x04000046 RID: 70
		private static readonly string UserAgentSuffix = "google-api-dotnet-client/" + ConfigurableMessageHandler.ApiVersion + " (gzip)";

		// Token: 0x04000047 RID: 71
		private readonly object unsuccessfulResponseHandlersLock = new object();

		// Token: 0x04000048 RID: 72
		private readonly object exceptionHandlersLock = new object();

		// Token: 0x04000049 RID: 73
		private readonly object executeInterceptorsLock = new object();

		// Token: 0x0400004A RID: 74
		private readonly IList<IHttpUnsuccessfulResponseHandler> unsuccessfulResponseHandlers = new List<IHttpUnsuccessfulResponseHandler>();

		// Token: 0x0400004B RID: 75
		private readonly IList<IHttpExceptionHandler> exceptionHandlers = new List<IHttpExceptionHandler>();

		// Token: 0x0400004C RID: 76
		private readonly IList<IHttpExecuteInterceptor> executeInterceptors = new List<IHttpExecuteInterceptor>();

		// Token: 0x0400004D RID: 77
		private int _loggingRequestId;

		// Token: 0x0400004E RID: 78
		private ILogger _instanceLogger = ConfigurableMessageHandler.Logger;

		// Token: 0x0400004F RID: 79
		private int numTries = 3;

		// Token: 0x04000050 RID: 80
		private int numRedirects = 10;

		// Token: 0x02000048 RID: 72
		[Flags]
		public enum LogEventType
		{
			// Token: 0x040000A8 RID: 168
			None = 0,
			// Token: 0x040000A9 RID: 169
			RequestUri = 1,
			// Token: 0x040000AA RID: 170
			RequestHeaders = 2,
			// Token: 0x040000AB RID: 171
			RequestBody = 4,
			// Token: 0x040000AC RID: 172
			ResponseStatus = 8,
			// Token: 0x040000AD RID: 173
			ResponseHeaders = 16,
			// Token: 0x040000AE RID: 174
			ResponseBody = 32,
			// Token: 0x040000AF RID: 175
			ResponseAbnormal = 64
		}
	}
}
