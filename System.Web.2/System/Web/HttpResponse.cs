using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.Routing;
using System.Web.UI;
using System.Web.Util;
using Microsoft.Win32.SafeHandles;

namespace System.Web
{
	// Token: 0x020000B0 RID: 176
	public sealed class HttpResponse
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000B2A RID: 2858 RVA: 0x0001B848 File Offset: 0x00019A48
		// (remove) Token: 0x06000B2B RID: 2859 RVA: 0x0001B87C File Offset: 0x00019A7C
		internal static event EventHandler Redirecting;

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001B8AF File Offset: 0x00019AAF
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0001B8B7 File Offset: 0x00019AB7
		internal HttpContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001B8C0 File Offset: 0x00019AC0
		internal HttpRequest Request
		{
			get
			{
				if (this._context == null)
				{
					return null;
				}
				return this._context.Request;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001B8D7 File Offset: 0x00019AD7
		internal HttpResponse(HttpWorkerRequest wr, HttpContext context)
		{
			this._wr = wr;
			this._context = context;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001B90A File Offset: 0x00019B0A
		public HttpResponse(TextWriter writer)
		{
			this._wr = null;
			this._httpWriter = null;
			this._writer = writer;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001B944 File Offset: 0x00019B44
		private bool UsingHttpWriter
		{
			get
			{
				return this._httpWriter != null && this._writer == this._httpWriter;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001B95E File Offset: 0x00019B5E
		internal void SetAllocatorProvider(IAllocatorProvider allocator)
		{
			if (this._httpWriter != null)
			{
				this._httpWriter.AllocatorProvider = allocator;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001B974 File Offset: 0x00019B74
		internal void Dispose()
		{
			if (this._httpWriter != null)
			{
				this._httpWriter.RecycleBuffers();
			}
			if (this._cacheDependencyForResponse != null)
			{
				this._cacheDependencyForResponse.Dispose();
				this._cacheDependencyForResponse = null;
			}
			if (this._userAddedDependencies != null)
			{
				foreach (CacheDependency cacheDependency in this._userAddedDependencies)
				{
					cacheDependency.Dispose();
				}
				this._userAddedDependencies = null;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0001B9DC File Offset: 0x00019BDC
		internal void InitResponseWriter()
		{
			if (this._httpWriter == null)
			{
				this._httpWriter = new HttpWriter(this);
				this._writer = this._httpWriter;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0001BA00 File Offset: 0x00019C00
		private void AppendHeader(HttpResponseHeader h)
		{
			if (this._customHeaders == null)
			{
				this._customHeaders = new ArrayList();
			}
			this._customHeaders.Add(h);
			if (this._cachePolicy != null && StringUtil.EqualsIgnoreCase("Set-Cookie", h.Name))
			{
				this._cachePolicy.SetHasSetCookieHeader();
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001BA52 File Offset: 0x00019C52
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001BA5A File Offset: 0x00019C5A
		public bool HeadersWritten
		{
			get
			{
				return this._headersWritten;
			}
			internal set
			{
				this._headersWritten = value;
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001BA64 File Offset: 0x00019C64
		internal ArrayList GenerateResponseHeadersIntegrated(bool forCache)
		{
			ArrayList arrayList = new ArrayList();
			HttpHeaderCollection httpHeaderCollection = this.Headers as HttpHeaderCollection;
			foreach (object obj in httpHeaderCollection)
			{
				string text = (string)obj;
				int knownResponseHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(text);
				if (knownResponseHeaderIndex < 0 || !forCache || (knownResponseHeaderIndex != 26 && knownResponseHeaderIndex != 27 && knownResponseHeaderIndex != 0 && knownResponseHeaderIndex != 18 && knownResponseHeaderIndex != 19 && knownResponseHeaderIndex != 22 && knownResponseHeaderIndex != 28))
				{
					if (knownResponseHeaderIndex >= 0)
					{
						arrayList.Add(new HttpResponseHeader(knownResponseHeaderIndex, httpHeaderCollection[text]));
					}
					else
					{
						arrayList.Add(new HttpResponseHeader(text, httpHeaderCollection[text]));
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001BB30 File Offset: 0x00019D30
		internal void GenerateResponseHeadersForCookies()
		{
			if (this._cookies == null || (this._cookies.Count == 0 && !this._cookies.Changed))
			{
				return;
			}
			HttpHeaderCollection httpHeaderCollection = this.Headers as HttpHeaderCollection;
			bool flag = false;
			if (!this._cookies.Changed)
			{
				for (int i = 0; i < this._cookies.Count; i++)
				{
					HttpCookie httpCookie = this._cookies[i];
					if (httpCookie.Added)
					{
						bool flag2 = true;
						if (AppSettings.AvoidDuplicatedSetCookie)
						{
							if (!httpCookie.IsInResponseHeader)
							{
								httpCookie.IsInResponseHeader = true;
							}
							else
							{
								flag2 = false;
							}
						}
						if (flag2)
						{
							HttpResponseHeader setCookieHeader = httpCookie.GetSetCookieHeader(this._context);
							httpHeaderCollection.SetHeader(setCookieHeader.Name, setCookieHeader.Value, false);
						}
						httpCookie.Added = false;
						httpCookie.Changed = false;
					}
					else if (httpCookie.Changed)
					{
						flag = true;
						break;
					}
				}
			}
			if (this._cookies.Changed || flag)
			{
				httpHeaderCollection.Remove("Set-Cookie");
				for (int j = 0; j < this._cookies.Count; j++)
				{
					HttpCookie httpCookie = this._cookies[j];
					HttpResponseHeader setCookieHeader = httpCookie.GetSetCookieHeader(this._context);
					httpHeaderCollection.SetHeader(setCookieHeader.Name, setCookieHeader.Value, false);
					httpCookie.Added = false;
					httpCookie.Changed = false;
					if (AppSettings.AvoidDuplicatedSetCookie)
					{
						httpCookie.IsInResponseHeader = true;
					}
				}
				this._cookies.Changed = false;
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		internal void GenerateResponseHeadersForHandler()
		{
			if (!(this._wr is IIS7WorkerRequest))
			{
				return;
			}
			string value = null;
			if (!this._headersWritten && !this._handlerHeadersGenerated)
			{
				try
				{
					RuntimeConfig lkgconfig = RuntimeConfig.GetLKGConfig(this._context);
					HttpRuntimeSection httpRuntime = lkgconfig.HttpRuntime;
					if (httpRuntime != null)
					{
						value = httpRuntime.VersionHeader;
						this._sendCacheControlHeader = httpRuntime.SendCacheControlHeader;
					}
					OutputCacheSection outputCache = lkgconfig.OutputCache;
					if (outputCache != null)
					{
						this._sendCacheControlHeader &= outputCache.SendCacheControlHeader;
					}
					if (this.SuppressDefaultCacheControlHeader)
					{
						this._sendCacheControlHeader = false;
					}
					if (this._sendCacheControlHeader && !this._cacheControlHeaderAdded)
					{
						this.Headers.Set("Cache-Control", "private");
					}
					if (!string.IsNullOrEmpty(value))
					{
						this.Headers.Set("X-AspNet-Version", value);
					}
					this._contentTypeSetByManagedHandler = true;
				}
				finally
				{
					this._handlerHeadersGenerated = true;
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001BD88 File Offset: 0x00019F88
		internal ArrayList GenerateResponseHeaders(bool forCache)
		{
			ArrayList arrayList = new ArrayList();
			bool flag = true;
			if (!forCache && !this._versionHeaderSent)
			{
				string value = null;
				RuntimeConfig lkgconfig = RuntimeConfig.GetLKGConfig(this._context);
				HttpRuntimeSection httpRuntime = lkgconfig.HttpRuntime;
				if (httpRuntime != null)
				{
					value = httpRuntime.VersionHeader;
					flag = httpRuntime.SendCacheControlHeader;
				}
				OutputCacheSection outputCache = lkgconfig.OutputCache;
				if (outputCache != null)
				{
					flag &= outputCache.SendCacheControlHeader;
				}
				if (!string.IsNullOrEmpty(value))
				{
					arrayList.Add(new HttpResponseHeader("X-AspNet-Version", value));
				}
				this._versionHeaderSent = true;
			}
			if (this._customHeaders != null)
			{
				int count = this._customHeaders.Count;
				for (int i = 0; i < count; i++)
				{
					arrayList.Add(this._customHeaders[i]);
				}
			}
			if (this._redirectLocation != null)
			{
				arrayList.Add(new HttpResponseHeader(23, this._redirectLocation));
			}
			if (!forCache)
			{
				if (this._cookies != null)
				{
					int count2 = this._cookies.Count;
					for (int j = 0; j < count2; j++)
					{
						arrayList.Add(this._cookies[j].GetSetCookieHeader(this.Context));
					}
				}
				if (this._cachePolicy != null && this._cachePolicy.IsModified())
				{
					this._cachePolicy.GetHeaders(arrayList, this);
				}
				else
				{
					if (this._cacheHeaders != null)
					{
						arrayList.AddRange(this._cacheHeaders);
					}
					if (!this._cacheControlHeaderAdded && flag)
					{
						arrayList.Add(new HttpResponseHeader(0, "private"));
					}
				}
			}
			if (this._statusCode != 204 && this._contentType != null)
			{
				string value2 = this.AppendCharSetToContentType(this._contentType);
				arrayList.Add(new HttpResponseHeader(12, value2));
			}
			return arrayList;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001BF38 File Offset: 0x0001A138
		internal string AppendCharSetToContentType(string contentType)
		{
			string result = contentType;
			if ((this._customCharSet || (this._httpWriter != null && this._httpWriter.ResponseEncodingUsed)) && contentType.IndexOf("charset=", StringComparison.Ordinal) < 0)
			{
				string charset = this.Charset;
				if (charset.Length > 0)
				{
					result = contentType + "; charset=" + charset;
				}
			}
			return result;
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001BF91 File Offset: 0x0001A191
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0001BF99 File Offset: 0x0001A199
		internal bool UseAdaptiveError
		{
			get
			{
				return this._useAdaptiveError;
			}
			set
			{
				this._useAdaptiveError = value;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		private void WriteHeaders()
		{
			if (this._wr == null)
			{
				return;
			}
			if (this._context != null && this._context.ApplicationInstance != null)
			{
				this._context.ApplicationInstance.RaiseOnPreSendRequestHeaders();
			}
			if (this.UseAdaptiveError)
			{
				int statusCode = this.StatusCode;
				if (statusCode >= 400 && statusCode < 600)
				{
					this.StatusCode = 200;
				}
			}
			ArrayList arrayList = this.GenerateResponseHeaders(false);
			this._wr.SendStatus(this.StatusCode, this.StatusDescription);
			this._wr.SetHeaderEncoding(this.HeaderEncoding);
			int num = (arrayList != null) ? arrayList.Count : 0;
			for (int i = 0; i < num; i++)
			{
				HttpResponseHeader httpResponseHeader = arrayList[i] as HttpResponseHeader;
				httpResponseHeader.Send(this._wr);
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001C072 File Offset: 0x0001A272
		internal int GetBufferedLength()
		{
			if (this._httpWriter == null)
			{
				return 0;
			}
			return Convert.ToInt32(this._httpWriter.GetBufferedLength());
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001C090 File Offset: 0x0001A290
		private void Flush(bool finalFlush, bool async = false)
		{
			if (this._completed || this._flushing)
			{
				return;
			}
			if (this._httpWriter == null)
			{
				this._writer.Flush();
				return;
			}
			this._flushing = true;
			bool flag = false;
			try
			{
				IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
				if (iis7WorkerRequest != null)
				{
					this.GenerateResponseHeadersForHandler();
					this.UpdateNativeResponse(true);
					if (!async)
					{
						try
						{
							iis7WorkerRequest.ExplicitFlush();
						}
						finally
						{
							this._headersWritten = true;
						}
					}
				}
				else
				{
					long num = 0L;
					if (!this._headersWritten)
					{
						if (!this._suppressHeaders && !this._clientDisconnected)
						{
							this.EnsureSessionStateIfNecessary();
							if (finalFlush)
							{
								num = this._httpWriter.GetBufferedLength();
								if (!this._contentLengthSet && num == 0L && this._httpWriter != null)
								{
									this._contentType = null;
								}
								this.SuppressCachingCookiesIfNecessary();
								this.WriteHeaders();
								num = this._httpWriter.GetBufferedLength();
								if (!this._contentLengthSet && this._statusCode != 304)
								{
									this._wr.SendCalculatedContentLength(num);
								}
							}
							else
							{
								if (!this._contentLengthSet && !this._transferEncodingSet && this._statusCode == 200)
								{
									string httpVersion = this._wr.GetHttpVersion();
									if (httpVersion != null && httpVersion.Equals("HTTP/1.1"))
									{
										this.AppendHeader(new HttpResponseHeader(6, "chunked"));
										this._chunked = true;
									}
									num = this._httpWriter.GetBufferedLength();
								}
								this.WriteHeaders();
							}
						}
						this._headersWritten = true;
					}
					else
					{
						num = this._httpWriter.GetBufferedLength();
					}
					if (!this._filteringCompleted)
					{
						this._httpWriter.Filter(false);
						num = this._httpWriter.GetBufferedLength();
					}
					if (!this._suppressContentSet && this.Request != null && this.Request.HttpVerb == HttpVerb.HEAD)
					{
						this._suppressContent = true;
					}
					if (this._suppressContent || this._ended)
					{
						this._httpWriter.ClearBuffers();
						num = 0L;
					}
					if (!this._clientDisconnected)
					{
						if (this._context != null && this._context.ApplicationInstance != null)
						{
							this._context.ApplicationInstance.RaiseOnPreSendRequestContent();
						}
						if (this._chunked)
						{
							if (num > 0L)
							{
								byte[] bytes = Encoding.ASCII.GetBytes(Convert.ToString(num, 16) + "\r\n");
								this._wr.SendResponseFromMemory(bytes, bytes.Length);
								this._httpWriter.Send(this._wr);
								this._wr.SendResponseFromMemory(HttpResponse.s_chunkSuffix, HttpResponse.s_chunkSuffix.Length);
							}
							if (finalFlush)
							{
								this._wr.SendResponseFromMemory(HttpResponse.s_chunkEnd, HttpResponse.s_chunkEnd.Length);
							}
						}
						else
						{
							this._httpWriter.Send(this._wr);
						}
						if (!async)
						{
							flag = !finalFlush;
							this._wr.FlushResponse(finalFlush);
						}
						this._wr.UpdateResponseCounters(finalFlush, (int)num);
					}
				}
			}
			finally
			{
				this._flushing = false;
				if (finalFlush && this._headersWritten)
				{
					this._completed = true;
				}
				if (flag)
				{
					this._httpWriter.ClearBuffers();
				}
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		internal void FinalFlushAtTheEndOfRequestProcessing()
		{
			this.FinalFlushAtTheEndOfRequestProcessing(false);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001C3B5 File Offset: 0x0001A5B5
		internal void FinalFlushAtTheEndOfRequestProcessing(bool needPipelineCompletion)
		{
			this.Flush(true, false);
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001C3BF File Offset: 0x0001A5BF
		public bool SupportsAsyncFlush
		{
			get
			{
				return this._wr != null && this._wr.SupportsAsyncFlush;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		public IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			if (this._completed)
			{
				throw new HttpException(SR.GetString("Cannot_flush_completed_response"));
			}
			if (this._wr != null && this._wr.SupportsAsyncFlush && !this._context.IsInCancellablePeriod)
			{
				this.Flush(false, true);
				return this._wr.BeginFlush(callback, state);
			}
			FlushAsyncResult flushAsyncResult = new FlushAsyncResult(callback, state);
			try
			{
				this.Flush(false, false);
			}
			catch (Exception error)
			{
				flushAsyncResult.SetError(error);
			}
			flushAsyncResult.Complete(0, 0, IntPtr.Zero, true);
			return flushAsyncResult;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001C470 File Offset: 0x0001A670
		public void EndFlush(IAsyncResult asyncResult)
		{
			if (this._wr != null && this._wr.SupportsAsyncFlush && !this._context.IsInCancellablePeriod)
			{
				this._headersWritten = true;
				if (!(this._wr is IIS7WorkerRequest))
				{
					this._httpWriter.ClearBuffers();
				}
				this._wr.EndFlush(asyncResult);
				return;
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			FlushAsyncResult flushAsyncResult = asyncResult as FlushAsyncResult;
			if (flushAsyncResult == null)
			{
				throw new ArgumentException(null, "asyncResult");
			}
			flushAsyncResult.ReleaseWaitHandleWhenSignaled();
			if (flushAsyncResult.Error != null)
			{
				flushAsyncResult.Error.Throw();
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001C508 File Offset: 0x0001A708
		public Task FlushAsync()
		{
			return Task.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginFlush), new Action<IAsyncResult>(this.EndFlush), null);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001C530 File Offset: 0x0001A730
		internal string SetupKernelCaching(string originalCacheUrl)
		{
			if (this._cookies != null && this._cookies.Count != 0)
			{
				this._cachePolicy.SetHasSetCookieHeader();
				return null;
			}
			bool enableKernelCacheForVaryByStar = this.IsKernelCacheEnabledForVaryByStar();
			if (!this._cachePolicy.IsKernelCacheable(this.Request, enableKernelCacheForVaryByStar))
			{
				return null;
			}
			HttpRuntimeSection httpRuntime = RuntimeConfig.GetLKGConfig(this._context).HttpRuntime;
			if (httpRuntime == null || !httpRuntime.EnableKernelOutputCache)
			{
				return null;
			}
			double totalSeconds = (this._cachePolicy.UtcGetAbsoluteExpiration() - DateTime.UtcNow).TotalSeconds;
			if (totalSeconds <= 0.0)
			{
				return null;
			}
			int secondsToLive = (totalSeconds < 2147483647.0) ? ((int)totalSeconds) : int.MaxValue;
			string text = this._wr.SetupKernelCaching(secondsToLive, originalCacheUrl, enableKernelCacheForVaryByStar);
			if (text != null)
			{
				this._cachePolicy.SetNoMaxAgeInCacheControl();
			}
			return text;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001C5FE File Offset: 0x0001A7FE
		public void DisableKernelCache()
		{
			if (this._wr == null)
			{
				return;
			}
			this._wr.DisableKernelCache();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001C614 File Offset: 0x0001A814
		public void DisableUserCache()
		{
			if (this._wr == null)
			{
				return;
			}
			this._wr.DisableUserCache();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001C62C File Offset: 0x0001A82C
		private bool IsKernelCacheEnabledForVaryByStar()
		{
			OutputCacheSection outputCache = RuntimeConfig.GetAppConfig().OutputCache;
			return this._cachePolicy.IsVaryByStar && outputCache.EnableKernelCacheForVaryByStar;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001C65C File Offset: 0x0001A85C
		internal void FilterOutput()
		{
			if (this._filteringCompleted)
			{
				return;
			}
			try
			{
				if (this.UsingHttpWriter)
				{
					IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
					if (iis7WorkerRequest != null)
					{
						this._httpWriter.FilterIntegrated(true, iis7WorkerRequest);
					}
					else
					{
						this._httpWriter.Filter(true);
					}
				}
			}
			finally
			{
				this._filteringCompleted = true;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		internal void IgnoreFurtherWrites()
		{
			if (this.UsingHttpWriter)
			{
				this._httpWriter.IgnoreFurtherWrites();
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001C6D5 File Offset: 0x0001A8D5
		internal bool IsBuffered()
		{
			return !this._headersWritten && this.UsingHttpWriter;
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001C6E7 File Offset: 0x0001A8E7
		public HttpCookieCollection Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					this._cookies = new HttpCookieCollection(this, false);
				}
				return this._cookies;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001C704 File Offset: 0x0001A904
		internal bool ContainsNonShareableCookies()
		{
			if (this._cookies != null)
			{
				for (int i = 0; i < this._cookies.Count; i++)
				{
					if (!this._cookies[i].Shareable)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001C745 File Offset: 0x0001A945
		internal HttpCookieCollection GetCookiesNoCreate()
		{
			return this._cookies;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0001C750 File Offset: 0x0001A950
		public NameValueCollection Headers
		{
			get
			{
				if (!(this._wr is IIS7WorkerRequest))
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				if (this._headers == null)
				{
					this._headers = new HttpHeaderCollection(this._wr, this, 16);
				}
				return this._headers;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001C79C File Offset: 0x0001A99C
		public void AddFileDependency(string filename)
		{
			this._fileDependencyList.AddDependency(filename, "filename");
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001C7AF File Offset: 0x0001A9AF
		public void AddFileDependencies(ArrayList filenames)
		{
			this._fileDependencyList.AddDependencies(filenames, "filenames");
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001C7C2 File Offset: 0x0001A9C2
		public void AddFileDependencies(string[] filenames)
		{
			this._fileDependencyList.AddDependencies(filenames, "filenames");
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001C7D5 File Offset: 0x0001A9D5
		internal void AddVirtualPathDependencies(string[] virtualPaths)
		{
			this._virtualPathDependencyList.AddDependencies(virtualPaths, "virtualPaths", false, this.Request.Path);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		internal void AddFileDependencies(string[] filenames, DateTime utcTime)
		{
			this._fileDependencyList.AddDependencies(filenames, "filenames", false, utcTime);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001C809 File Offset: 0x0001AA09
		public void AddCacheItemDependency(string cacheKey)
		{
			this._cacheItemDependencyList.AddDependency(cacheKey, "cacheKey");
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001C81C File Offset: 0x0001AA1C
		public void AddCacheItemDependencies(ArrayList cacheKeys)
		{
			this._cacheItemDependencyList.AddDependencies(cacheKeys, "cacheKeys");
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001C82F File Offset: 0x0001AA2F
		public void AddCacheItemDependencies(string[] cacheKeys)
		{
			this._cacheItemDependencyList.AddDependencies(cacheKeys, "cacheKeys");
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001C844 File Offset: 0x0001AA44
		public void AddCacheDependency(params CacheDependency[] dependencies)
		{
			if (dependencies == null)
			{
				throw new ArgumentNullException("dependencies");
			}
			if (dependencies.Length == 0)
			{
				return;
			}
			if (this._cacheDependencyForResponse != null)
			{
				throw new InvalidOperationException(SR.GetString("Invalid_operation_cache_dependency"));
			}
			if (this._userAddedDependencies == null)
			{
				this._userAddedDependencies = (CacheDependency[])dependencies.Clone();
			}
			else
			{
				CacheDependency[] array = new CacheDependency[this._userAddedDependencies.Length + dependencies.Length];
				int i;
				for (i = 0; i < this._userAddedDependencies.Length; i++)
				{
					array[i] = this._userAddedDependencies[i];
				}
				for (int j = 0; j < dependencies.Length; j++)
				{
					array[i + j] = dependencies[j];
				}
				this._userAddedDependencies = array;
			}
			this.Cache.SetDependencies(true);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001C8F2 File Offset: 0x0001AAF2
		public static void RemoveOutputCacheItem(string path)
		{
			HttpResponse.RemoveOutputCacheItem(path, null);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		public static void RemoveOutputCacheItem(string path, string providerName)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (StringUtil.StringStartsWith(path, "\\\\") || path.IndexOf(':') >= 0 || !UrlPath.IsRooted(path))
			{
				throw new ArgumentException(SR.GetString("Invalid_path_for_remove", new object[]
				{
					path
				}));
			}
			string key = OutputCacheModule.CreateOutputCachedItemKey(path, HttpVerb.GET, null, null);
			if (providerName == null)
			{
				OutputCache.Remove(key, null);
			}
			else
			{
				OutputCache.RemoveFromProvider(key, providerName);
			}
			key = OutputCacheModule.CreateOutputCachedItemKey(path, HttpVerb.POST, null, null);
			if (providerName == null)
			{
				OutputCache.Remove(key, null);
				return;
			}
			OutputCache.RemoveFromProvider(key, providerName);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001C98A File Offset: 0x0001AB8A
		internal bool HasFileDependencies()
		{
			return this._fileDependencyList.HasDependencies();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001C997 File Offset: 0x0001AB97
		internal bool HasCacheItemDependencies()
		{
			return this._cacheItemDependencyList.HasDependencies();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		internal CacheDependency CreateCacheDependencyForResponse()
		{
			if (this._cacheDependencyForResponse == null)
			{
				CacheDependency cacheDependency = this._cacheItemDependencyList.CreateCacheDependency(CacheDependencyType.CacheItems, null);
				cacheDependency = this._fileDependencyList.CreateCacheDependency(CacheDependencyType.Files, cacheDependency);
				cacheDependency = this._virtualPathDependencyList.CreateCacheDependency(CacheDependencyType.VirtualPaths, cacheDependency);
				if (this._userAddedDependencies != null)
				{
					AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
					aggregateCacheDependency.Add(this._userAddedDependencies);
					if (cacheDependency != null)
					{
						aggregateCacheDependency.Add(new CacheDependency[]
						{
							cacheDependency
						});
					}
					this._userAddedDependencies = null;
					this._cacheDependencyForResponse = aggregateCacheDependency;
				}
				else
				{
					this._cacheDependencyForResponse = cacheDependency;
				}
			}
			return this._cacheDependencyForResponse;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001CA30 File Offset: 0x0001AC30
		internal HttpRawResponse GetSnapshot()
		{
			int statusCode = 200;
			string statusDescription = null;
			ArrayList headers = null;
			ArrayList buffers = null;
			bool hasSubstBlocks = false;
			if (!this.IsBuffered())
			{
				throw new HttpException(SR.GetString("Cannot_get_snapshot_if_not_buffered"));
			}
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (!this._suppressContent)
			{
				if (iis7WorkerRequest != null)
				{
					buffers = this._httpWriter.GetIntegratedSnapshot(out hasSubstBlocks, iis7WorkerRequest);
				}
				else
				{
					buffers = this._httpWriter.GetSnapshot(out hasSubstBlocks);
				}
			}
			if (!this._suppressHeaders)
			{
				statusCode = this._statusCode;
				statusDescription = this._statusDescription;
				if (iis7WorkerRequest != null)
				{
					headers = this.GenerateResponseHeadersIntegrated(true);
				}
				else
				{
					headers = this.GenerateResponseHeaders(true);
				}
			}
			return new HttpRawResponse(statusCode, statusDescription, headers, buffers, hasSubstBlocks);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		internal void UseSnapshot(HttpRawResponse rawResponse, bool sendBody)
		{
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_use_snapshot_after_headers_sent"));
			}
			if (this._httpWriter == null)
			{
				throw new HttpException(SR.GetString("Cannot_use_snapshot_for_TextWriter"));
			}
			this.ClearAll();
			this.StatusCode = rawResponse.StatusCode;
			this.StatusDescription = rawResponse.StatusDescription;
			ArrayList headers = rawResponse.Headers;
			int num = (headers != null) ? headers.Count : 0;
			for (int i = 0; i < num; i++)
			{
				HttpResponseHeader httpResponseHeader = (HttpResponseHeader)headers[i];
				this.AppendHeader(httpResponseHeader.Name, httpResponseHeader.Value);
			}
			this.SetResponseBuffers(rawResponse.Buffers);
			this._suppressContent = !sendBody;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001CB84 File Offset: 0x0001AD84
		internal void SetResponseBuffers(ArrayList buffers)
		{
			if (this._httpWriter == null)
			{
				throw new HttpException(SR.GetString("Cannot_use_snapshot_for_TextWriter"));
			}
			this._httpWriter.UseSnapshot(buffers);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001CBAA File Offset: 0x0001ADAA
		internal void CloseConnectionAfterError()
		{
			this._closeConnectionAfterError = true;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
		private void WriteErrorMessage(Exception e, bool dontShowSensitiveErrors)
		{
			CultureInfo cultureInfo = null;
			CultureInfo currentUICulture = null;
			bool flag = false;
			if (this._context.DynamicUICulture != null)
			{
				cultureInfo = this._context.DynamicUICulture;
			}
			else
			{
				GlobalizationSection globalization = RuntimeConfig.GetLKGConfig(this._context).Globalization;
				if (globalization != null && !string.IsNullOrEmpty(globalization.UICulture))
				{
					try
					{
						cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(globalization.UICulture);
					}
					catch
					{
					}
				}
			}
			this.GenerateResponseHeadersForHandler();
			if (cultureInfo != null)
			{
				currentUICulture = Thread.CurrentThread.CurrentUICulture;
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
				flag = true;
			}
			try
			{
				try
				{
					ErrorFormatter errorFormatter = this.GetErrorFormatter(e);
					if (dontShowSensitiveErrors && !errorFormatter.CanBeShownToAllUsers)
					{
						errorFormatter = new GenericApplicationErrorFormatter(this.Request.IsLocal);
					}
					if (ErrorFormatter.RequiresAdaptiveErrorReporting(this.Context))
					{
						this._writer.Write(errorFormatter.GetAdaptiveErrorMessage(this.Context, dontShowSensitiveErrors));
					}
					else
					{
						this._writer.Write(errorFormatter.GetHtmlErrorMessage(dontShowSensitiveErrors));
						if (!dontShowSensitiveErrors && HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
						{
							this._writer.Write("<!-- \r\n");
							this.WriteExceptionStack(e);
							this._writer.Write("-->");
						}
						if (!dontShowSensitiveErrors && !this.Request.IsLocal)
						{
							this._writer.Write("<!-- \r\n");
							this._writer.Write(SR.GetString("Information_Disclosure_Warning"));
							this._writer.Write("-->");
						}
					}
					if (this._closeConnectionAfterError)
					{
						this.Flush();
						this.Close();
					}
				}
				finally
				{
					if (flag)
					{
						Thread.CurrentThread.CurrentUICulture = currentUICulture;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001CD90 File Offset: 0x0001AF90
		internal void SetOverrideErrorFormatter(ErrorFormatter errorFormatter)
		{
			this._overrideErrorFormatter = errorFormatter;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001CD9C File Offset: 0x0001AF9C
		internal ErrorFormatter GetErrorFormatter(Exception e)
		{
			if (this._overrideErrorFormatter != null)
			{
				return this._overrideErrorFormatter;
			}
			ErrorFormatter errorFormatter = HttpException.GetErrorFormatter(e);
			if (errorFormatter == null)
			{
				ConfigurationException ex = e as ConfigurationException;
				if (ex != null && !string.IsNullOrEmpty(ex.Filename))
				{
					errorFormatter = new ConfigErrorFormatter(ex);
				}
			}
			if (errorFormatter == null)
			{
				if (this._statusCode == 404)
				{
					errorFormatter = new PageNotFoundErrorFormatter(this.Request.Path);
				}
				else if (this._statusCode == 403)
				{
					errorFormatter = new PageForbiddenErrorFormatter(this.Request.Path);
				}
				else if (e is SecurityException)
				{
					errorFormatter = new SecurityErrorFormatter(e);
				}
				else
				{
					errorFormatter = new UnhandledErrorFormatter(e);
				}
			}
			ConfigErrorFormatter configErrorFormatter = errorFormatter as ConfigErrorFormatter;
			if (configErrorFormatter != null)
			{
				configErrorFormatter.AllowSourceCode = this.Request.IsLocal;
			}
			return errorFormatter;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001CE5C File Offset: 0x0001B05C
		private void WriteOneExceptionStack(Exception e)
		{
			Exception innerException = e.InnerException;
			if (innerException != null)
			{
				this.WriteOneExceptionStack(innerException);
			}
			string text = "[" + e.GetType().Name + "]";
			if (e.Message != null && e.Message.Length > 0)
			{
				text = text + ": " + HttpUtility.HtmlEncode(e.Message);
			}
			this._writer.WriteLine(text);
			if (e.StackTrace != null)
			{
				this._writer.WriteLine(e.StackTrace);
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		private void WriteExceptionStack(Exception e)
		{
			ConfigurationErrorsException ex = e as ConfigurationErrorsException;
			if (ex == null)
			{
				this.WriteOneExceptionStack(e);
				return;
			}
			this.WriteOneExceptionStack(e);
			ICollection errors = ex.Errors;
			if (errors.Count > 1)
			{
				bool flag = false;
				foreach (object obj in errors)
				{
					ConfigurationException e2 = (ConfigurationException)obj;
					if (!flag)
					{
						flag = true;
					}
					else
					{
						this._writer.WriteLine("---");
						this.WriteOneExceptionStack(e2);
					}
				}
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001CF84 File Offset: 0x0001B184
		internal void ReportRuntimeError(Exception e, bool canThrow, bool localExecute)
		{
			CustomErrorsSection customErrorsSection = null;
			bool flag = false;
			int num = -1;
			if (this._completed)
			{
				return;
			}
			if (this._wr != null)
			{
				this._wr.TrySkipIisCustomErrors = true;
			}
			if (!localExecute)
			{
				num = HttpException.GetHttpCodeForException(e);
				if (num != 404)
				{
					WebBaseEvent.RaiseRuntimeError(e, this);
				}
				customErrorsSection = CustomErrorsSection.GetSettings(this._context, canThrow);
				flag = (customErrorsSection == null || customErrorsSection.CustomErrorsEnabled(this.Request));
			}
			if (!this._headersWritten)
			{
				if (num == -1)
				{
					num = HttpException.GetHttpCodeForException(e);
				}
				if (num == 401 && !this._context.IsClientImpersonationConfigured)
				{
					num = 500;
				}
				if (this._context.TraceIsEnabled)
				{
					this._context.Trace.StatusCode = num;
				}
				if (!localExecute && flag)
				{
					string url = (customErrorsSection != null) ? customErrorsSection.GetRedirectString(num) : null;
					HttpResponse.RedirectToErrorPageStatus redirectToErrorPageStatus = this.RedirectToErrorPage(url, customErrorsSection.RedirectMode);
					if (redirectToErrorPageStatus != HttpResponse.RedirectToErrorPageStatus.NotAttempted)
					{
						if (redirectToErrorPageStatus == HttpResponse.RedirectToErrorPageStatus.Success)
						{
							return;
						}
						if (!customErrorsSection.AllowNestedErrors)
						{
							this.ClearAll();
							this.StatusCode = 500;
							HttpException ex = new HttpException();
							ex.SetFormatter(new CustomErrorFailedErrorFormatter());
							this.WriteErrorMessage(ex, true);
							return;
						}
					}
					this.ClearAll();
					this.StatusCode = num;
					this.WriteErrorMessage(e, true);
					return;
				}
				this.ClearAll();
				this.StatusCode = num;
				this.WriteErrorMessage(e, false);
				return;
			}
			else
			{
				this.Clear();
				if (this._contentType != null && this._contentType.Equals("text/html"))
				{
					this.Write("\r\n\r\n</pre></table></table></table></table></table>");
					this.Write("</font></font></font></font></font>");
					this.Write("</i></i></i></i></i></b></b></b></b></b></u></u></u></u></u>");
					this.Write("<p>&nbsp;</p><hr>\r\n\r\n");
				}
				this.WriteErrorMessage(e, flag);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001D126 File Offset: 0x0001B326
		internal void SynchronizeStatus(int statusCode, int subStatusCode, string description)
		{
			this._statusCode = statusCode;
			this._subStatusCode = subStatusCode;
			this._statusDescription = description;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001D140 File Offset: 0x0001B340
		internal void SynchronizeHeader(int knownHeaderIndex, string name, string value)
		{
			HttpHeaderCollection httpHeaderCollection = this.Headers as HttpHeaderCollection;
			httpHeaderCollection.SynchronizeHeader(name, value);
			if (knownHeaderIndex < 0)
			{
				return;
			}
			bool headersWritten = this.HeadersWritten;
			this.HeadersWritten = false;
			try
			{
				if (knownHeaderIndex <= 12)
				{
					if (knownHeaderIndex != 0)
					{
						if (knownHeaderIndex == 12)
						{
							this._contentType = value;
						}
					}
					else
					{
						this._cacheControlHeaderAdded = true;
					}
				}
				else if (knownHeaderIndex != 23)
				{
					if (knownHeaderIndex == 27)
					{
						if (value != null)
						{
							HttpCookie httpCookie = HttpRequest.CreateCookieFromString(value, false);
							if (AppSettings.FixCookieDefaults)
							{
								HttpCookie.TryParseFlags(value, value.IndexOf(';'), httpCookie);
							}
							httpCookie.IsInResponseHeader = true;
							this.Cookies.Set(httpCookie);
							httpCookie.Changed = false;
							httpCookie.Added = false;
						}
					}
				}
				else
				{
					this._redirectLocation = value;
					this._redirectLocationSet = false;
				}
			}
			finally
			{
				this.HeadersWritten = headersWritten;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001D210 File Offset: 0x0001B410
		internal void SyncStatusIntegrated()
		{
			if (!this._headersWritten && this._statusSet)
			{
				this._wr.SendStatus(this._statusCode, this._subStatusCode, this.StatusDescription);
				this._statusSet = false;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001D246 File Offset: 0x0001B446
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0001D24E File Offset: 0x0001B44E
		public int StatusCode
		{
			get
			{
				return this._statusCode;
			}
			set
			{
				if (this._headersWritten)
				{
					throw new HttpException(SR.GetString("Cannot_set_status_after_headers_sent"));
				}
				if (this._statusCode != value)
				{
					this._statusCode = value;
					this._subStatusCode = 0;
					this._statusDescription = null;
					this._statusSet = true;
				}
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001D28D File Offset: 0x0001B48D
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
		public int SubStatusCode
		{
			get
			{
				if (!(this._wr is IIS7WorkerRequest))
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				return this._subStatusCode;
			}
			set
			{
				if (!(this._wr is IIS7WorkerRequest))
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				if (this._headersWritten)
				{
					throw new HttpException(SR.GetString("Cannot_set_status_after_headers_sent"));
				}
				this._subStatusCode = value;
				this._statusSet = true;
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001D304 File Offset: 0x0001B504
		internal void SetStatusCode(int statusCode, int subStatus = -1)
		{
			this.StatusCode = statusCode;
			if (subStatus >= 0 && this._wr is IIS7WorkerRequest)
			{
				this.SubStatusCode = subStatus;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0001D325 File Offset: 0x0001B525
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0001D348 File Offset: 0x0001B548
		public string StatusDescription
		{
			get
			{
				if (this._statusDescription == null)
				{
					this._statusDescription = HttpWorkerRequest.GetStatusDescription(this._statusCode);
				}
				return this._statusDescription;
			}
			set
			{
				if (this._headersWritten)
				{
					throw new HttpException(SR.GetString("Cannot_set_status_after_headers_sent"));
				}
				if (value != null && value.Length > 512)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._statusDescription = value;
				this._statusSet = true;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001D396 File Offset: 0x0001B596
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0001D3AD File Offset: 0x0001B5AD
		public bool TrySkipIisCustomErrors
		{
			get
			{
				return this._wr != null && this._wr.TrySkipIisCustomErrors;
			}
			set
			{
				if (this._wr != null)
				{
					this._wr.TrySkipIisCustomErrors = value;
				}
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001D3C3 File Offset: 0x0001B5C3
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0001D3CB File Offset: 0x0001B5CB
		public bool SuppressFormsAuthenticationRedirect { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		public bool SuppressDefaultCacheControlHeader { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0001D3E5 File Offset: 0x0001B5E5
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0001D3ED File Offset: 0x0001B5ED
		public bool BufferOutput
		{
			get
			{
				return this._bufferOutput;
			}
			set
			{
				if (this._bufferOutput != value)
				{
					this._bufferOutput = value;
					if (this._httpWriter != null)
					{
						this._httpWriter.UpdateResponseBuffering();
					}
				}
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001D414 File Offset: 0x0001B614
		internal string GetHttpHeaderContentEncoding()
		{
			string result = null;
			if (this._wr is IIS7WorkerRequest)
			{
				if (this._headers != null)
				{
					result = this._headers["Content-Encoding"];
				}
			}
			else if (this._customHeaders != null)
			{
				int count = this._customHeaders.Count;
				for (int i = 0; i < count; i++)
				{
					HttpResponseHeader httpResponseHeader = (HttpResponseHeader)this._customHeaders[i];
					if (httpResponseHeader.Name == "Content-Encoding")
					{
						result = httpResponseHeader.Value;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0001D499 File Offset: 0x0001B699
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0001D4A1 File Offset: 0x0001B6A1
		public string ContentType
		{
			get
			{
				return this._contentType;
			}
			set
			{
				if (!this._headersWritten)
				{
					this._contentTypeSetByManagedCaller = true;
					this._contentType = value;
					return;
				}
				if (this._contentType == value)
				{
					return;
				}
				throw new HttpException(SR.GetString("Cannot_set_content_type_after_headers_sent"));
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x0001D4F9 File Offset: 0x0001B6F9
		public string Charset
		{
			get
			{
				if (this._charSet == null)
				{
					this._charSet = this.ContentEncoding.WebName;
				}
				return this._charSet;
			}
			set
			{
				if (this._headersWritten)
				{
					throw new HttpException(SR.GetString("Cannot_set_content_type_after_headers_sent"));
				}
				if (value != null)
				{
					this._charSet = value;
				}
				else
				{
					this._charSet = string.Empty;
				}
				this._customCharSet = true;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001D534 File Offset: 0x0001B734
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0001D584 File Offset: 0x0001B784
		public Encoding ContentEncoding
		{
			get
			{
				if (this._encoding == null)
				{
					GlobalizationSection globalization = RuntimeConfig.GetLKGConfig(this._context).Globalization;
					if (globalization != null)
					{
						this._encoding = globalization.ResponseEncoding;
					}
					if (this._encoding == null)
					{
						this._encoding = Encoding.Default;
					}
				}
				return this._encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._encoding == null || !this._encoding.Equals(value))
				{
					this._encoding = value;
					this._encoder = null;
					if (this._httpWriter != null)
					{
						this._httpWriter.UpdateResponseEncoding();
					}
				}
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0001D638 File Offset: 0x0001B838
		public Encoding HeaderEncoding
		{
			get
			{
				if (this._headerEncoding == null)
				{
					GlobalizationSection globalization = RuntimeConfig.GetLKGConfig(this._context).Globalization;
					if (globalization != null)
					{
						this._headerEncoding = globalization.ResponseHeaderEncoding;
					}
					if (this._headerEncoding == null || this._headerEncoding.Equals(Encoding.Unicode))
					{
						this._headerEncoding = Encoding.UTF8;
					}
				}
				return this._headerEncoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Equals(Encoding.Unicode))
				{
					throw new HttpException(SR.GetString("Invalid_header_encoding", new object[]
					{
						value.WebName
					}));
				}
				if (this._headerEncoding == null || !this._headerEncoding.Equals(value))
				{
					if (this._headersWritten)
					{
						throw new HttpException(SR.GetString("Cannot_set_header_encoding_after_headers_sent"));
					}
					this._headerEncoding = value;
				}
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		internal Encoder ContentEncoder
		{
			get
			{
				if (this._encoder == null)
				{
					Encoding contentEncoding = this.ContentEncoding;
					this._encoder = contentEncoding.GetEncoder();
					if (!contentEncoding.Equals(Encoding.UTF8))
					{
						bool flag = false;
						GlobalizationSection globalization = RuntimeConfig.GetLKGConfig(this._context).Globalization;
						if (globalization != null)
						{
							flag = globalization.EnableBestFitResponseEncoding;
						}
						if (!flag)
						{
							this._encoder.Fallback = new EncoderReplacementFallback();
						}
					}
				}
				return this._encoder;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001D71F File Offset: 0x0001B91F
		public HttpCachePolicy Cache
		{
			get
			{
				if (this._cachePolicy == null)
				{
					this._cachePolicy = new HttpCachePolicy();
				}
				return this._cachePolicy;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0001D73A File Offset: 0x0001B93A
		internal bool HasCachePolicy
		{
			get
			{
				return this._cachePolicy != null;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0001D745 File Offset: 0x0001B945
		public bool IsClientConnected
		{
			get
			{
				if (this._clientDisconnected)
				{
					return false;
				}
				if (this._wr != null && !this._wr.IsClientConnected())
				{
					this._clientDisconnected = true;
					return false;
				}
				return true;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0001D770 File Offset: 0x0001B970
		public CancellationToken ClientDisconnectedToken
		{
			get
			{
				IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
				CancellationToken result;
				if (iis7WorkerRequest != null && iis7WorkerRequest.TryGetClientDisconnectedCancellationToken(out result))
				{
					return result;
				}
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_75_Integrated"));
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001D7A7 File Offset: 0x0001B9A7
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0001D7AF File Offset: 0x0001B9AF
		public bool IsRequestBeingRedirected
		{
			get
			{
				return this._isRequestBeingRedirected;
			}
			internal set
			{
				this._isRequestBeingRedirected = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public string RedirectLocation
		{
			get
			{
				return this._redirectLocation;
			}
			set
			{
				if (this._headersWritten)
				{
					throw new HttpException(SR.GetString("Cannot_append_header_after_headers_sent"));
				}
				this._redirectLocation = value;
				this._redirectLocationSet = true;
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
		public void Close()
		{
			if (!this._clientDisconnected && !this._completed && this._wr != null)
			{
				this._wr.CloseConnection();
				this._clientDisconnected = true;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0001D814 File Offset: 0x0001BA14
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0001D81C File Offset: 0x0001BA1C
		public TextWriter Output
		{
			get
			{
				return this._writer;
			}
			set
			{
				this._writer = value;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001D828 File Offset: 0x0001BA28
		internal TextWriter SwitchWriter(TextWriter writer)
		{
			TextWriter writer2 = this._writer;
			this._writer = writer;
			return writer2;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0001D844 File Offset: 0x0001BA44
		public Stream OutputStream
		{
			get
			{
				if (!this.UsingHttpWriter)
				{
					throw new HttpException(SR.GetString("OutputStream_NotAvail"));
				}
				return this._httpWriter.OutputStream;
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0001D869 File Offset: 0x0001BA69
		public void BinaryWrite(byte[] buffer)
		{
			this.OutputStream.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0001D87B File Offset: 0x0001BA7B
		public void Pics(string value)
		{
			this.AppendHeader("PICS-Label", value);
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0001D889 File Offset: 0x0001BA89
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		public Stream Filter
		{
			get
			{
				if (this.UsingHttpWriter)
				{
					return this._httpWriter.GetCurrentFilter();
				}
				return null;
			}
			set
			{
				if (!this.UsingHttpWriter)
				{
					throw new HttpException(SR.GetString("Filtering_not_allowed"));
				}
				this._httpWriter.InstallFilter(value);
				IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
				if (iis7WorkerRequest != null)
				{
					iis7WorkerRequest.ResponseFilterInstalled();
					return;
				}
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0001D8E7 File Offset: 0x0001BAE7
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0001D8EF File Offset: 0x0001BAEF
		public bool SuppressContent
		{
			get
			{
				return this._suppressContent;
			}
			set
			{
				this._suppressContent = value;
				this._suppressContentSet = true;
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0001D900 File Offset: 0x0001BB00
		public void AppendHeader(string name, string value)
		{
			bool flag = false;
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_append_header_after_headers_sent"));
			}
			int knownResponseHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(name);
			if (knownResponseHeaderIndex <= 11)
			{
				if (knownResponseHeaderIndex != 0)
				{
					if (knownResponseHeaderIndex == 6)
					{
						this._transferEncodingSet = true;
						goto IL_8C;
					}
					if (knownResponseHeaderIndex != 11)
					{
						goto IL_8C;
					}
					this._contentLengthSet = true;
					goto IL_8C;
				}
				else
				{
					this._cacheControlHeaderAdded = true;
				}
			}
			else
			{
				if (knownResponseHeaderIndex == 12)
				{
					this.ContentType = value;
					return;
				}
				switch (knownResponseHeaderIndex)
				{
				case 18:
				case 19:
				case 22:
					break;
				case 20:
				case 21:
					goto IL_8C;
				case 23:
					this.RedirectLocation = value;
					return;
				default:
					if (knownResponseHeaderIndex != 28)
					{
						goto IL_8C;
					}
					break;
				}
			}
			flag = true;
			IL_8C:
			if (this._wr is IIS7WorkerRequest)
			{
				this.Headers.Add(name, value);
				return;
			}
			if (flag)
			{
				if (this._cacheHeaders == null)
				{
					this._cacheHeaders = new ArrayList();
				}
				this._cacheHeaders.Add(new HttpResponseHeader(knownResponseHeaderIndex, value));
				return;
			}
			HttpResponseHeader h;
			if (knownResponseHeaderIndex >= 0)
			{
				h = new HttpResponseHeader(knownResponseHeaderIndex, value);
			}
			else
			{
				h = new HttpResponseHeader(name, value);
			}
			this.AppendHeader(h);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0001D9FB File Offset: 0x0001BBFB
		public void AppendCookie(HttpCookie cookie)
		{
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_append_cookie_after_headers_sent"));
			}
			this.Cookies.AddCookie(cookie, true);
			this.OnCookieAdd(cookie);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001DA29 File Offset: 0x0001BC29
		public void SetCookie(HttpCookie cookie)
		{
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_append_cookie_after_headers_sent"));
			}
			this.Cookies.AddCookie(cookie, false);
			this.OnCookieCollectionChange();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0001DA56 File Offset: 0x0001BC56
		internal void BeforeCookieCollectionChange()
		{
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_modify_cookies_after_headers_sent"));
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001DA70 File Offset: 0x0001BC70
		internal void OnCookieAdd(HttpCookie cookie)
		{
			this.Request.AddResponseCookie(cookie);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0001DA7E File Offset: 0x0001BC7E
		internal void OnCookieCollectionChange()
		{
			this.Request.ResetCookies();
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		public void ClearHeaders()
		{
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_clear_headers_after_headers_sent"));
			}
			this.StatusCode = 200;
			this._subStatusCode = 0;
			this._statusDescription = null;
			this._contentType = "text/html";
			this._contentTypeSetByManagedCaller = false;
			this._charSet = null;
			this._customCharSet = false;
			this._contentLengthSet = false;
			this._redirectLocation = null;
			this._redirectLocationSet = false;
			this._isRequestBeingRedirected = false;
			this._customHeaders = null;
			if (this._headers != null)
			{
				this._headers.ClearInternal();
			}
			this._transferEncodingSet = false;
			this._chunked = false;
			if (this._cookies != null)
			{
				this._cookies.Reset();
				this.Request.ResetCookies();
			}
			if (this._cachePolicy != null)
			{
				this._cachePolicy.Reset();
			}
			this._cacheControlHeaderAdded = false;
			this._cacheHeaders = null;
			this._suppressHeaders = false;
			this._suppressContent = false;
			this._suppressContentSet = false;
			this._expiresInMinutes = 0;
			this._expiresInMinutesSet = false;
			this._expiresAbsolute = DateTime.MinValue;
			this._expiresAbsoluteSet = false;
			this._cacheControl = null;
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				this.ClearNativeResponse(false, true, iis7WorkerRequest);
				if (this._handlerHeadersGenerated && this._sendCacheControlHeader)
				{
					this.Headers.Set("Cache-Control", "private");
				}
				this._handlerHeadersGenerated = false;
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001DBED File Offset: 0x0001BDED
		public void ClearContent()
		{
			this.Clear();
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public void Clear()
		{
			if (this.UsingHttpWriter)
			{
				this._httpWriter.ClearBuffers();
			}
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest != null)
			{
				this.ClearNativeResponse(true, false, iis7WorkerRequest);
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001DC30 File Offset: 0x0001BE30
		internal void ClearAll()
		{
			if (!this._headersWritten)
			{
				this.ClearHeaders();
			}
			this.Clear();
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001DC46 File Offset: 0x0001BE46
		public void Flush()
		{
			if (this._completed)
			{
				throw new HttpException(SR.GetString("Cannot_flush_completed_response"));
			}
			this.Flush(false, false);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001DC68 File Offset: 0x0001BE68
		public ISubscriptionToken AddOnSendingHeaders(Action<HttpContext> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (!(this._wr is IIS7WorkerRequest))
			{
				throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
			}
			if (this.HeadersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_call_method_after_headers_sent_generic"));
			}
			return this._onSendingHeadersSubscriptionQueue.Enqueue(callback);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001DCC4 File Offset: 0x0001BEC4
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public void AppendToLog(string param)
		{
			if (this._wr is ISAPIWorkerRequest)
			{
				((ISAPIWorkerRequest)this._wr).AppendLogParameter(param);
				return;
			}
			if (this._wr is IIS7WorkerRequest)
			{
				this._context.Request.AppendToLogQueryString(param);
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001DD03 File Offset: 0x0001BF03
		public void Redirect(string url)
		{
			this.Redirect(url, true, false);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001DD0E File Offset: 0x0001BF0E
		public void Redirect(string url, bool endResponse)
		{
			this.Redirect(url, endResponse, false);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001DD19 File Offset: 0x0001BF19
		public void RedirectToRoute(object routeValues)
		{
			this.RedirectToRoute(new RouteValueDictionary(routeValues));
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001DD27 File Offset: 0x0001BF27
		public void RedirectToRoute(string routeName)
		{
			this.RedirectToRoute(routeName, null, false);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001DD32 File Offset: 0x0001BF32
		public void RedirectToRoute(RouteValueDictionary routeValues)
		{
			this.RedirectToRoute(null, routeValues, false);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0001DD3D File Offset: 0x0001BF3D
		public void RedirectToRoute(string routeName, object routeValues)
		{
			this.RedirectToRoute(routeName, new RouteValueDictionary(routeValues), false);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0001DD4D File Offset: 0x0001BF4D
		public void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
		{
			this.RedirectToRoute(routeName, routeValues, false);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0001DD58 File Offset: 0x0001BF58
		private void RedirectToRoute(string routeName, RouteValueDictionary routeValues, bool permanent)
		{
			string text = null;
			VirtualPathData virtualPath = RouteTable.Routes.GetVirtualPath(this.Request.RequestContext, routeName, routeValues);
			if (virtualPath != null)
			{
				text = virtualPath.VirtualPath;
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException(SR.GetString("No_Route_Found_For_Redirect"));
			}
			this.Redirect(text, false, permanent);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001DDAA File Offset: 0x0001BFAA
		public void RedirectToRoutePermanent(object routeValues)
		{
			this.RedirectToRoutePermanent(new RouteValueDictionary(routeValues));
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public void RedirectToRoutePermanent(string routeName)
		{
			this.RedirectToRoute(routeName, null, true);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
		public void RedirectToRoutePermanent(RouteValueDictionary routeValues)
		{
			this.RedirectToRoute(null, routeValues, true);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		public void RedirectToRoutePermanent(string routeName, object routeValues)
		{
			this.RedirectToRoute(routeName, new RouteValueDictionary(routeValues), true);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001DDDE File Offset: 0x0001BFDE
		public void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
		{
			this.RedirectToRoute(routeName, routeValues, true);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001DDE9 File Offset: 0x0001BFE9
		public void RedirectPermanent(string url)
		{
			this.Redirect(url, true, true);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		public void RedirectPermanent(string url, bool endResponse)
		{
			this.Redirect(url, endResponse, true);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001DE00 File Offset: 0x0001C000
		internal void Redirect(string url, bool endResponse, bool permanent)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.IndexOf('\n') >= 0)
			{
				throw new ArgumentException(SR.GetString("Cannot_redirect_to_newline"));
			}
			if (this._headersWritten)
			{
				throw new HttpException(SR.GetString("Cannot_redirect_after_headers_sent"));
			}
			Page page = this._context.Handler as Page;
			if (page != null && page.IsCallback)
			{
				throw new ApplicationException(SR.GetString("Redirect_not_allowed_in_callback"));
			}
			url = this.ApplyRedirectQueryStringIfRequired(url);
			url = this.ApplyAppPathModifier(url);
			url = this.ConvertToFullyQualifiedRedirectUrlIfRequired(url);
			url = this.UrlEncodeRedirect(url);
			this.Clear();
			if (page != null && page.IsPostBack && page.SmartNavigation && this.Request["__smartNavPostBack"] == "true")
			{
				this.Write("<BODY><ASP_SMARTNAV_RDIR url=\"");
				this.Write(HttpUtility.HtmlEncode(url));
				this.Write("\"></ASP_SMARTNAV_RDIR>");
				this.Write("</BODY>");
			}
			else
			{
				if (HttpRuntime.UseIntegratedPipeline)
				{
					this.ContentType = "text/html";
				}
				this.StatusCode = (permanent ? 301 : 302);
				this.RedirectLocation = url;
				if (UriUtil.IsSafeScheme(url))
				{
					url = HttpUtility.HtmlAttributeEncode(url);
				}
				else
				{
					url = HttpUtility.HtmlAttributeEncode(HttpUtility.UrlEncode(url));
				}
				this.Write("<html><head><title>Object moved</title></head><body>\r\n");
				this.Write("<h2>Object moved to <a href=\"" + url + "\">here</a>.</h2>\r\n");
				this.Write("</body></html>\r\n");
			}
			this._isRequestBeingRedirected = true;
			EventHandler redirecting = HttpResponse.Redirecting;
			if (redirecting != null)
			{
				redirecting(this, EventArgs.Empty);
			}
			if (endResponse)
			{
				this.End();
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		internal string ApplyRedirectQueryStringIfRequired(string url)
		{
			if (this.Request == null || this.Request.Browser["requiresPostRedirectionHandling"] != "true")
			{
				return url;
			}
			Page page = this._context.Handler as Page;
			if (page != null && !page.IsPostBack)
			{
				return url;
			}
			int num = url.IndexOf(HttpResponse.RedirectQueryStringAssignment, StringComparison.Ordinal);
			if (num == -1)
			{
				num = url.IndexOf('?');
				if (num >= 0)
				{
					url = url.Insert(num + 1, HttpResponse._redirectQueryStringInline);
				}
				else
				{
					url += HttpResponse._redirectQueryString;
				}
			}
			return url;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E034 File Offset: 0x0001C234
		internal HttpResponse.RedirectToErrorPageStatus RedirectToErrorPage(string url, CustomErrorsRedirectMode redirectMode)
		{
			try
			{
				if (string.IsNullOrEmpty(url))
				{
					return HttpResponse.RedirectToErrorPageStatus.NotAttempted;
				}
				if (this._headersWritten)
				{
					return HttpResponse.RedirectToErrorPageStatus.NotAttempted;
				}
				if (this.Request.QueryString["aspxerrorpath"] != null)
				{
					return HttpResponse.RedirectToErrorPageStatus.Failed;
				}
				if (redirectMode == CustomErrorsRedirectMode.ResponseRewrite)
				{
					this.Context.Server.Execute(url);
				}
				else
				{
					if (url.IndexOf('?') < 0)
					{
						url = url + "?aspxerrorpath=" + HttpEncoderUtility.UrlEncodeSpaces(this.Request.Path);
					}
					this.Redirect(url, false);
				}
			}
			catch
			{
				return HttpResponse.RedirectToErrorPageStatus.Failed;
			}
			return HttpResponse.RedirectToErrorPageStatus.Success;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		internal bool CanExecuteUrlForEntireResponse
		{
			get
			{
				return !this._headersWritten && this._wr != null && this._wr.SupportsExecuteUrl && this.UsingHttpWriter && this._httpWriter.GetBufferedLength() == 0L && !this._httpWriter.FilterInstalled && (this._cachePolicy == null || !this._cachePolicy.IsModified());
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001E148 File Offset: 0x0001C348
		internal IAsyncResult BeginExecuteUrlForEntireResponse(string pathOverride, NameValueCollection requestHeaders, AsyncCallback cb, object state)
		{
			string name;
			string authType;
			if (this._context != null && this._context.User != null)
			{
				name = this._context.User.Identity.Name;
				authType = this._context.User.Identity.AuthenticationType;
			}
			else
			{
				name = string.Empty;
				authType = string.Empty;
			}
			string url = this.Request.RewrittenUrl;
			if (pathOverride != null)
			{
				url = pathOverride;
			}
			string headers = null;
			if (requestHeaders != null)
			{
				int count = requestHeaders.Count;
				if (count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < count; i++)
					{
						stringBuilder.Append(requestHeaders.GetKey(i));
						stringBuilder.Append(": ");
						stringBuilder.Append(requestHeaders.Get(i));
						stringBuilder.Append("\r\n");
					}
					headers = stringBuilder.ToString();
				}
			}
			byte[] entity = null;
			if (this._context != null && this._context.Request != null)
			{
				entity = this._context.Request.EntityBody;
			}
			IAsyncResult result = this._wr.BeginExecuteUrl(url, null, headers, true, true, this._wr.GetUserToken(), name, authType, entity, cb, state);
			this._headersWritten = true;
			this._ended = true;
			return result;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001E27F File Offset: 0x0001C47F
		internal void EndExecuteUrlForEntireResponse(IAsyncResult result)
		{
			this._wr.EndExecuteUrl(result);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001E28D File Offset: 0x0001C48D
		public void Write(string s)
		{
			this._writer.Write(s);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001E29B File Offset: 0x0001C49B
		public void Write(object obj)
		{
			this._writer.Write(obj);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001E2A9 File Offset: 0x0001C4A9
		public void Write(char ch)
		{
			this._writer.Write(ch);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001E2B7 File Offset: 0x0001C4B7
		public void Write(char[] buffer, int index, int count)
		{
			this._writer.Write(buffer, index, count);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		public void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			if (callback.Target != null && callback.Target is Control)
			{
				throw new ArgumentException(SR.GetString("Invalid_substitution_callback"), "callback");
			}
			if (this.UsingHttpWriter)
			{
				this._httpWriter.WriteSubstBlock(callback, this._wr as IIS7WorkerRequest);
			}
			else
			{
				this._writer.Write(callback(this._context));
			}
			if (this._cachePolicy != null && this._cachePolicy.GetCacheability() == HttpCacheability.Public)
			{
				this._cachePolicy.SetCacheability(HttpCacheability.Server);
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001E35C File Offset: 0x0001C55C
		private void WriteStreamAsText(Stream f, long offset, long size)
		{
			if (size < 0L)
			{
				size = f.Length - offset;
			}
			if (size > 0L)
			{
				if (offset > 0L)
				{
					f.Seek(offset, SeekOrigin.Begin);
				}
				byte[] array = new byte[(int)size];
				int count = f.Read(array, 0, (int)size);
				this._writer.Write(Encoding.Default.GetChars(array, 0, count));
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		internal void WriteVirtualFile(VirtualFile vf)
		{
			using (Stream stream = vf.Open())
			{
				if (this.UsingHttpWriter)
				{
					long length = stream.Length;
					if (length > 0L)
					{
						byte[] buffer = new byte[(int)length];
						int count = stream.Read(buffer, 0, (int)length);
						this._httpWriter.WriteBytes(buffer, 0, count);
					}
				}
				else
				{
					this.WriteStreamAsText(stream, 0L, -1L);
				}
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001E42C File Offset: 0x0001C62C
		private string GetNormalizedFilename(string fn)
		{
			if (!UrlPath.IsAbsolutePhysicalPath(fn))
			{
				if (this.Request != null)
				{
					fn = this.Request.MapPath(fn);
				}
				else
				{
					fn = HostingEnvironment.MapPath(fn);
				}
			}
			return fn;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001E457 File Offset: 0x0001C657
		public void WriteFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			this.WriteFile(filename, false);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001E470 File Offset: 0x0001C670
		public void WriteFile(string filename, bool readIntoMemory)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			filename = this.GetNormalizedFilename(filename);
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				if (this.UsingHttpWriter)
				{
					long length = fileStream.Length;
					if (length > 0L)
					{
						if (readIntoMemory)
						{
							byte[] buffer = new byte[(int)length];
							int count = fileStream.Read(buffer, 0, (int)length);
							this._httpWriter.WriteBytes(buffer, 0, count);
						}
						else
						{
							fileStream.Close();
							fileStream = null;
							this._httpWriter.WriteFile(filename, 0L, length);
						}
					}
				}
				else
				{
					this.WriteStreamAsText(fileStream, 0L, -1L);
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001E51C File Offset: 0x0001C71C
		public void TransmitFile(string filename)
		{
			this.TransmitFile(filename, 0L, -1L);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001E52C File Offset: 0x0001C72C
		public void TransmitFile(string filename, long offset, long length)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (offset < 0L)
			{
				throw new ArgumentException(SR.GetString("Invalid_range"), "offset");
			}
			if (length < -1L)
			{
				throw new ArgumentException(SR.GetString("Invalid_range"), "length");
			}
			filename = this.GetNormalizedFilename(filename);
			using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				long length2 = fileStream.Length;
				if (length == -1L)
				{
					length = length2 - offset;
				}
				if (length2 < offset)
				{
					throw new ArgumentException(SR.GetString("Invalid_range"), "offset");
				}
				if (length2 - offset < length)
				{
					throw new ArgumentException(SR.GetString("Invalid_range"), "length");
				}
				if (!this.UsingHttpWriter)
				{
					this.WriteStreamAsText(fileStream, offset, length);
					return;
				}
			}
			if (length > 0L)
			{
				bool supportsLongTransmitFile = this._wr != null && this._wr.SupportsLongTransmitFile;
				this._httpWriter.TransmitFile(filename, offset, length, this._context.IsClientImpersonationConfigured || HttpRuntime.IsOnUNCShareInternal, supportsLongTransmitFile);
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001E644 File Offset: 0x0001C844
		private void ValidateFileRange(string filename, long offset, long length)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				long length2 = fileStream.Length;
				if (length == -1L)
				{
					length = length2 - offset;
				}
				if (offset < 0L || length > length2 - offset)
				{
					throw new HttpException(SR.GetString("Invalid_range"));
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		public void WriteFile(string filename, long offset, long size)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (size == 0L)
			{
				return;
			}
			filename = this.GetNormalizedFilename(filename);
			this.ValidateFileRange(filename, offset, size);
			if (this.UsingHttpWriter)
			{
				InternalSecurityPermissions.FileReadAccess(filename).Demand();
				this._httpWriter.WriteFile(filename, offset, size);
				return;
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				this.WriteStreamAsText(fileStream, offset, size);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0001E72C File Offset: 0x0001C92C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			if (size <= 0L)
			{
				return;
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(new SafeFileHandle(fileHandle, false), FileAccess.Read);
				if (this.UsingHttpWriter)
				{
					long length = fileStream.Length;
					if (size == -1L)
					{
						size = length - offset;
					}
					if (offset < 0L || size > length - offset)
					{
						throw new HttpException(SR.GetString("Invalid_range"));
					}
					if (offset > 0L)
					{
						fileStream.Seek(offset, SeekOrigin.Begin);
					}
					byte[] buffer = new byte[(int)size];
					int count = fileStream.Read(buffer, 0, (int)size);
					this._httpWriter.WriteBytes(buffer, 0, count);
				}
				else
				{
					this.WriteStreamAsText(fileStream, offset, size);
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0001E7DC File Offset: 0x0001C9DC
		public void PushPromise(string path)
		{
			this.PushPromise(path, "GET", null);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		public void PushPromise(string path, string method, NameValueCollection headers)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			string queryString = string.Empty;
			int num = path.IndexOf('?');
			if (num >= 0)
			{
				if (num < path.Length - 1)
				{
					queryString = path.Substring(num + 1);
				}
				path = path.Substring(0, num);
			}
			if (string.IsNullOrEmpty(path) || !UrlPath.IsValidVirtualPathWithoutProtocol(path))
			{
				throw new ArgumentException(SR.GetString("Invalid_path_for_push_promise", new object[]
				{
					path
				}));
			}
			VirtualPath virtualPath = this.Request.FilePathObject.Combine(VirtualPath.Create(path));
			try
			{
				if (!HttpRuntime.UseIntegratedPipeline)
				{
					throw new PlatformNotSupportedException(SR.GetString("Requires_Iis_Integrated_Mode"));
				}
				IIS7WorkerRequest iis7WorkerRequest = (IIS7WorkerRequest)this._wr;
				iis7WorkerRequest.PushPromise(virtualPath.VirtualPathString, queryString, method, headers);
			}
			catch (PlatformNotSupportedException errorInfo)
			{
				if (this.Context.TraceIsEnabled)
				{
					this.Context.Trace.Write("aspx", "Push promise is not supported", errorInfo);
				}
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0001E92C File Offset: 0x0001CB2C
		public string Status
		{
			get
			{
				return this.StatusCode.ToString(NumberFormatInfo.InvariantInfo) + " " + this.StatusDescription;
			}
			set
			{
				int statusCode = 200;
				string statusDescription = "OK";
				try
				{
					int num = value.IndexOf(' ');
					statusCode = int.Parse(value.Substring(0, num), CultureInfo.InvariantCulture);
					statusDescription = value.Substring(num + 1);
				}
				catch
				{
					throw new HttpException(SR.GetString("Invalid_status_string"));
				}
				this.StatusCode = statusCode;
				this.StatusDescription = statusDescription;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0001E99C File Offset: 0x0001CB9C
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		public bool Buffer
		{
			get
			{
				return this.BufferOutput;
			}
			set
			{
				this.BufferOutput = value;
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001E9AD File Offset: 0x0001CBAD
		public void AddHeader(string name, string value)
		{
			this.AppendHeader(name, value);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		public void End()
		{
			if (this._context.IsInCancellablePeriod)
			{
				HttpResponse.AbortCurrentThread();
				return;
			}
			this._endRequiresObservation = true;
			if (!this._flushing)
			{
				this.Flush();
				this._ended = true;
				if (this._context.ApplicationInstance != null)
				{
					this._context.ApplicationInstance.CompleteRequest();
				}
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001EA11 File Offset: 0x0001CC11
		internal void ObserveResponseEndCalled()
		{
			if (this._endRequiresObservation)
			{
				this._endRequiresObservation = false;
				HttpResponse.AbortCurrentThread();
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001EA27 File Offset: 0x0001CC27
		[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
		private static void AbortCurrentThread()
		{
			Thread.CurrentThread.Abort(new HttpApplication.CancelModuleException(false));
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001EA39 File Offset: 0x0001CC39
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0001EA44 File Offset: 0x0001CC44
		public int Expires
		{
			get
			{
				return this._expiresInMinutes;
			}
			set
			{
				if (!this._expiresInMinutesSet || value < this._expiresInMinutes)
				{
					this._expiresInMinutes = value;
					this.Cache.SetExpires(this._context.Timestamp + new TimeSpan(0, this._expiresInMinutes, 0));
				}
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0001EA91 File Offset: 0x0001CC91
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0001EA99 File Offset: 0x0001CC99
		public DateTime ExpiresAbsolute
		{
			get
			{
				return this._expiresAbsolute;
			}
			set
			{
				if (!this._expiresAbsoluteSet || value < this._expiresAbsolute)
				{
					this._expiresAbsolute = value;
					this.Cache.SetExpires(this._expiresAbsolute);
				}
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0001EAC9 File Offset: 0x0001CCC9
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		public string CacheControl
		{
			get
			{
				if (this._cacheControl == null)
				{
					return "private";
				}
				return this._cacheControl;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this._cacheControl = null;
					this.Cache.SetCacheability(HttpCacheability.NoCache);
					return;
				}
				if (StringUtil.EqualsIgnoreCase(value, "private"))
				{
					this._cacheControl = value;
					this.Cache.SetCacheability(HttpCacheability.Private);
					return;
				}
				if (StringUtil.EqualsIgnoreCase(value, "public"))
				{
					this._cacheControl = value;
					this.Cache.SetCacheability(HttpCacheability.Public);
					return;
				}
				if (StringUtil.EqualsIgnoreCase(value, "no-cache"))
				{
					this._cacheControl = value;
					this.Cache.SetCacheability(HttpCacheability.NoCache);
					return;
				}
				throw new ArgumentException(SR.GetString("Invalid_value_for_CacheControl", new object[]
				{
					value
				}));
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0001EB88 File Offset: 0x0001CD88
		internal void SetAppPathModifier(string appPathModifier)
		{
			if (appPathModifier != null && (appPathModifier.Length == 0 || appPathModifier[0] == '/' || appPathModifier[appPathModifier.Length - 1] == '/'))
			{
				throw new ArgumentException(SR.GetString("InvalidArgumentValue", new object[]
				{
					"appPathModifier"
				}));
			}
			this._appPathModifier = appPathModifier;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public string ApplyAppPathModifier(string virtualPath)
		{
			object cookielessHelper = this._context.CookielessHelper;
			if (virtualPath == null)
			{
				return null;
			}
			if (UrlPath.IsRelativeUrl(virtualPath))
			{
				virtualPath = UrlPath.Combine(this.Request.ClientBaseDir.VirtualPathString, virtualPath);
			}
			else
			{
				if (!UrlPath.IsRooted(virtualPath) || virtualPath.StartsWith("//", StringComparison.Ordinal))
				{
					return virtualPath;
				}
				virtualPath = UrlPath.Reduce(virtualPath);
			}
			if (AppSettings.DisableAppPathModifier || this._appPathModifier == null || virtualPath.IndexOf(this._appPathModifier, StringComparison.Ordinal) >= 0)
			{
				return virtualPath;
			}
			string appDomainAppVirtualPathString = HttpRuntime.AppDomainAppVirtualPathString;
			int num = appDomainAppVirtualPathString.Length;
			bool flag = virtualPath.Length == appDomainAppVirtualPathString.Length - 1;
			if (flag)
			{
				num--;
			}
			if (virtualPath.Length < num)
			{
				return virtualPath;
			}
			if (!StringUtil.EqualsIgnoreCase(virtualPath, 0, appDomainAppVirtualPathString, 0, num))
			{
				return virtualPath;
			}
			if (flag)
			{
				virtualPath += "/";
			}
			if (virtualPath.Length == appDomainAppVirtualPathString.Length)
			{
				virtualPath = virtualPath.Substring(0, appDomainAppVirtualPathString.Length) + this._appPathModifier + "/";
			}
			else
			{
				virtualPath = virtualPath.Substring(0, appDomainAppVirtualPathString.Length) + this._appPathModifier + "/" + virtualPath.Substring(appDomainAppVirtualPathString.Length);
			}
			return virtualPath;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001ED10 File Offset: 0x0001CF10
		internal string RemoveAppPathModifier(string virtualPath)
		{
			if (string.IsNullOrEmpty(this._appPathModifier))
			{
				return virtualPath;
			}
			int num = virtualPath.IndexOf(this._appPathModifier, StringComparison.Ordinal);
			if (num <= 0 || virtualPath[num - 1] != '/')
			{
				return virtualPath;
			}
			if (!AppSettings.RestoreAggressiveCookielessPathRemoval && (virtualPath.Length < num + this._appPathModifier.Length + 1 || virtualPath[num + this._appPathModifier.Length] != '/'))
			{
				return virtualPath;
			}
			return virtualPath.Substring(0, num - 1) + virtualPath.Substring(num + this._appPathModifier.Length);
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0001EDA6 File Offset: 0x0001CFA6
		internal bool UsePathModifier
		{
			get
			{
				return !string.IsNullOrEmpty(this._appPathModifier);
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		private string ConvertToFullyQualifiedRedirectUrlIfRequired(string url)
		{
			HttpRuntimeSection httpRuntimeSection = this._context.IsRuntimeErrorReported ? RuntimeConfig.GetLKGConfig(this._context).HttpRuntime : RuntimeConfig.GetConfig(this._context).HttpRuntime;
			if (httpRuntimeSection.UseFullyQualifiedRedirectUrl || (this.Request != null && this.Request.Browser["requiresFullyQualifiedRedirectUrl"] == "true"))
			{
				return new Uri(this.Request.Url, url).AbsoluteUri;
			}
			return url;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001EE40 File Offset: 0x0001D040
		private string UrlEncodeIDNSafe(string url)
		{
			string str;
			string str2;
			string str3;
			bool flag = UriUtil.TrySplitUriForPathEncode(url, out str, out str2, out str3, true);
			if (flag)
			{
				return str + HttpEncoderUtility.UrlEncodeSpaces(HttpUtility.UrlEncodeNonAscii(str2, Encoding.UTF8)) + str3;
			}
			return HttpEncoderUtility.UrlEncodeSpaces(HttpUtility.UrlEncodeNonAscii(url, Encoding.UTF8));
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001EE88 File Offset: 0x0001D088
		private string UrlEncodeRedirect(string url)
		{
			int num = url.IndexOf('?');
			if (num >= 0)
			{
				Encoding e = (this.Request != null) ? this.Request.ContentEncoding : this.ContentEncoding;
				url = this.UrlEncodeIDNSafe(url.Substring(0, num)) + HttpUtility.UrlEncodeNonAscii(url.Substring(num), e);
			}
			else
			{
				url = this.UrlEncodeIDNSafe(url);
			}
			return url;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		internal void UpdateNativeResponse(bool sendHeaders)
		{
			IIS7WorkerRequest iis7WorkerRequest = this._wr as IIS7WorkerRequest;
			if (iis7WorkerRequest == null)
			{
				return;
			}
			if ((this._suppressContent && this.Request != null && this.Request.HttpVerb != HttpVerb.HEAD) || this._ended)
			{
				this.Clear();
			}
			bool flag = false;
			long bufferedLength = this._httpWriter.GetBufferedLength();
			if (!this._headersWritten)
			{
				if (this.UseAdaptiveError)
				{
					int statusCode = this.StatusCode;
					if (statusCode >= 400 && statusCode < 600)
					{
						this.StatusCode = 200;
					}
				}
				if (sendHeaders && !this._onSendingHeadersSubscriptionQueue.IsEmpty)
				{
					this._onSendingHeadersSubscriptionQueue.FireAndComplete(delegate(Action<HttpContext> cb)
					{
						cb(this.Context);
					});
				}
				if (this._statusSet)
				{
					this._wr.SendStatus(this.StatusCode, this.SubStatusCode, this.StatusDescription);
					this._statusSet = false;
				}
				if (!this._suppressHeaders && !this._clientDisconnected)
				{
					if (sendHeaders)
					{
						this.EnsureSessionStateIfNecessary();
					}
					if (this._redirectLocation != null && this._redirectLocationSet)
					{
						HttpHeaderCollection httpHeaderCollection = this.Headers as HttpHeaderCollection;
						httpHeaderCollection.Set("Location", this._redirectLocation);
						this._redirectLocationSet = false;
					}
					bool flag2 = bufferedLength > 0L || iis7WorkerRequest.IsResponseBuffered();
					if (this._contentType != null && (this._contentTypeSetByManagedCaller || (this._contentTypeSetByManagedHandler && flag2)))
					{
						HttpHeaderCollection httpHeaderCollection2 = this.Headers as HttpHeaderCollection;
						string value = this.AppendCharSetToContentType(this._contentType);
						httpHeaderCollection2.Set("Content-Type", value);
					}
					this.GenerateResponseHeadersForCookies();
					if (sendHeaders)
					{
						this.SuppressCachingCookiesIfNecessary();
						if (this._cachePolicy != null && this._cachePolicy.IsModified())
						{
							ArrayList arrayList = new ArrayList();
							this._cachePolicy.GetHeaders(arrayList, this);
							HttpHeaderCollection httpHeaderCollection3 = this.Headers as HttpHeaderCollection;
							foreach (object obj in arrayList)
							{
								HttpResponseHeader httpResponseHeader = (HttpResponseHeader)obj;
								httpHeaderCollection3.Set(httpResponseHeader.Name, httpResponseHeader.Value);
							}
						}
						flag = true;
					}
				}
			}
			if (this._flushing && !this._filteringCompleted)
			{
				this._httpWriter.FilterIntegrated(false, iis7WorkerRequest);
				bufferedLength = this._httpWriter.GetBufferedLength();
			}
			if (!this._clientDisconnected && (bufferedLength > 0L || flag))
			{
				if (bufferedLength == 0L && this._httpWriter.IgnoringFurtherWrites)
				{
					return;
				}
				this._httpWriter.Send(this._wr);
				iis7WorkerRequest.PushResponseToNative();
				this._httpWriter.DisposeIntegratedBuffers();
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001F18C File Offset: 0x0001D38C
		private void ClearNativeResponse(bool clearEntity, bool clearHeaders, IIS7WorkerRequest wr)
		{
			wr.ClearResponse(clearEntity, clearHeaders);
			if (clearEntity)
			{
				this._httpWriter.ClearSubstitutionBlocks();
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		private void SuppressCachingCookiesIfNecessary()
		{
			if (!this.Request.IsSecureConnection && this.ContainsNonShareableCookies() && this.Cache.GetCacheability() == HttpCacheability.Public)
			{
				this.Cache.SetCacheability(HttpCacheability.NoCache, "Set-Cookie");
			}
			if (this._cachePolicy != null && this._cookies != null && this._cookies.Count != 0)
			{
				this._cachePolicy.SetHasSetCookieHeader();
				this.DisableKernelCache();
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001F213 File Offset: 0x0001D413
		private void EnsureSessionStateIfNecessary()
		{
			if (AppSettings.EnsureSessionStateLockedOnFlush)
			{
				this._context.EnsureSessionStateIfNecessary();
			}
		}

		// Token: 0x04000424 RID: 1060
		private HttpWorkerRequest _wr;

		// Token: 0x04000425 RID: 1061
		private HttpContext _context;

		// Token: 0x04000426 RID: 1062
		private HttpWriter _httpWriter;

		// Token: 0x04000427 RID: 1063
		private TextWriter _writer;

		// Token: 0x04000428 RID: 1064
		private HttpHeaderCollection _headers;

		// Token: 0x04000429 RID: 1065
		private bool _headersWritten;

		// Token: 0x0400042A RID: 1066
		private bool _completed;

		// Token: 0x0400042B RID: 1067
		private bool _ended;

		// Token: 0x0400042C RID: 1068
		private bool _endRequiresObservation;

		// Token: 0x0400042D RID: 1069
		private bool _flushing;

		// Token: 0x0400042E RID: 1070
		private bool _clientDisconnected;

		// Token: 0x0400042F RID: 1071
		private bool _filteringCompleted;

		// Token: 0x04000430 RID: 1072
		private bool _closeConnectionAfterError;

		// Token: 0x04000431 RID: 1073
		private int _statusCode = 200;

		// Token: 0x04000432 RID: 1074
		private string _statusDescription;

		// Token: 0x04000433 RID: 1075
		private bool _bufferOutput = true;

		// Token: 0x04000434 RID: 1076
		private string _contentType = "text/html";

		// Token: 0x04000435 RID: 1077
		private string _charSet;

		// Token: 0x04000436 RID: 1078
		private bool _customCharSet;

		// Token: 0x04000437 RID: 1079
		private bool _contentLengthSet;

		// Token: 0x04000438 RID: 1080
		private string _redirectLocation;

		// Token: 0x04000439 RID: 1081
		private bool _redirectLocationSet;

		// Token: 0x0400043A RID: 1082
		private Encoding _encoding;

		// Token: 0x0400043B RID: 1083
		private Encoder _encoder;

		// Token: 0x0400043C RID: 1084
		private Encoding _headerEncoding;

		// Token: 0x0400043D RID: 1085
		private bool _cacheControlHeaderAdded;

		// Token: 0x0400043E RID: 1086
		private HttpCachePolicy _cachePolicy;

		// Token: 0x0400043F RID: 1087
		private ArrayList _cacheHeaders;

		// Token: 0x04000440 RID: 1088
		private bool _suppressHeaders;

		// Token: 0x04000441 RID: 1089
		private bool _suppressContentSet;

		// Token: 0x04000442 RID: 1090
		private bool _suppressContent;

		// Token: 0x04000443 RID: 1091
		private string _appPathModifier;

		// Token: 0x04000444 RID: 1092
		private bool _isRequestBeingRedirected;

		// Token: 0x04000445 RID: 1093
		private bool _useAdaptiveError;

		// Token: 0x04000446 RID: 1094
		private bool _handlerHeadersGenerated;

		// Token: 0x04000447 RID: 1095
		private bool _sendCacheControlHeader;

		// Token: 0x04000448 RID: 1096
		private ArrayList _customHeaders;

		// Token: 0x04000449 RID: 1097
		private HttpCookieCollection _cookies;

		// Token: 0x0400044A RID: 1098
		private ResponseDependencyList _fileDependencyList;

		// Token: 0x0400044B RID: 1099
		private ResponseDependencyList _virtualPathDependencyList;

		// Token: 0x0400044C RID: 1100
		private ResponseDependencyList _cacheItemDependencyList;

		// Token: 0x0400044D RID: 1101
		private CacheDependency[] _userAddedDependencies;

		// Token: 0x0400044E RID: 1102
		private CacheDependency _cacheDependencyForResponse;

		// Token: 0x0400044F RID: 1103
		private ErrorFormatter _overrideErrorFormatter;

		// Token: 0x04000450 RID: 1104
		private int _expiresInMinutes;

		// Token: 0x04000451 RID: 1105
		private bool _expiresInMinutesSet;

		// Token: 0x04000452 RID: 1106
		private DateTime _expiresAbsolute;

		// Token: 0x04000453 RID: 1107
		private bool _expiresAbsoluteSet;

		// Token: 0x04000454 RID: 1108
		private string _cacheControl;

		// Token: 0x04000455 RID: 1109
		private bool _statusSet;

		// Token: 0x04000456 RID: 1110
		private int _subStatusCode;

		// Token: 0x04000457 RID: 1111
		private bool _versionHeaderSent;

		// Token: 0x04000458 RID: 1112
		private bool _contentTypeSetByManagedCaller;

		// Token: 0x04000459 RID: 1113
		private bool _contentTypeSetByManagedHandler;

		// Token: 0x0400045A RID: 1114
		private bool _transferEncodingSet;

		// Token: 0x0400045B RID: 1115
		private bool _chunked;

		// Token: 0x0400045C RID: 1116
		private SubscriptionQueue<Action<HttpContext>> _onSendingHeadersSubscriptionQueue;

		// Token: 0x0400045D RID: 1117
		internal static readonly string RedirectQueryStringVariable = "__redir";

		// Token: 0x0400045E RID: 1118
		internal static readonly string RedirectQueryStringValue = "1";

		// Token: 0x0400045F RID: 1119
		internal static readonly string RedirectQueryStringAssignment = HttpResponse.RedirectQueryStringVariable + "=" + HttpResponse.RedirectQueryStringValue;

		// Token: 0x04000460 RID: 1120
		private static readonly string _redirectQueryString = "?" + HttpResponse.RedirectQueryStringAssignment;

		// Token: 0x04000461 RID: 1121
		private static readonly string _redirectQueryStringInline = HttpResponse.RedirectQueryStringAssignment + "&";

		// Token: 0x04000463 RID: 1123
		private static byte[] s_chunkSuffix = new byte[]
		{
			13,
			10
		};

		// Token: 0x04000464 RID: 1124
		private static byte[] s_chunkEnd = new byte[]
		{
			48,
			13,
			10,
			13,
			10
		};

		// Token: 0x020008E4 RID: 2276
		internal enum RedirectToErrorPageStatus
		{
			// Token: 0x0400364F RID: 13903
			NotAttempted,
			// Token: 0x04003650 RID: 13904
			Success,
			// Token: 0x04003651 RID: 13905
			Failed
		}
	}
}
