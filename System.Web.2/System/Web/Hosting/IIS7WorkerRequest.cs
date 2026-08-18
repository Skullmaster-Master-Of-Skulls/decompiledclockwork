using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Security;
using System.Web.Util;
using System.Web.WebSockets;
using Microsoft.Win32.SafeHandles;

namespace System.Web.Hosting
{
	// Token: 0x020007B1 RID: 1969
	internal sealed class IIS7WorkerRequest : HttpWorkerRequest
	{
		// Token: 0x06005DBA RID: 23994 RVA: 0x00144E04 File Offset: 0x00143004
		internal IIS7WorkerRequest(IntPtr requestContext, bool etwProviderEnabled)
		{
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_TOTAL);
			if (IntPtr.Zero == requestContext)
			{
				throw new ArgumentNullException("requestContext");
			}
			this._context = requestContext;
			this._traceEnabled = etwProviderEnabled;
			if (this._traceEnabled)
			{
				EtwTrace.TraceEnableCheck(EtwTraceConfigType.IIS7_INTEGRATED, requestContext);
				UnsafeIISMethods.MgdGetRequestTraceGuid(this._context, out this._traceId);
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_APPDOMAIN_ENTER, this, Thread.GetDomain().FriendlyName);
				}
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x00144E95 File Offset: 0x00143095
		internal IntPtr RequestContext
		{
			get
			{
				if (this._context == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this._context;
			}
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x00144EB8 File Offset: 0x001430B8
		internal unsafe void ReadRequestBasics()
		{
			IntPtr ip;
			int num;
			IntPtr ip2;
			int num2;
			IntPtr intPtr;
			IntPtr pCookedUrl;
			int hresult = UnsafeIISMethods.MgdGetRequestBasics(this._context, out this._contentType, out this._contentTotalLength, out ip, out num, out ip2, out num2, out intPtr, out pCookedUrl);
			Misc.ThrowIfFailedHr(hresult);
			this._cacheUrl = ((num2 <= 0) ? null : StringUtil.StringFromWCharPtr(ip2, num2));
			this._pathTranslated = ((num <= 0) ? string.Empty : StringUtil.StringFromWCharPtr(ip, num));
			if (intPtr != IntPtr.Zero)
			{
				this._httpVerb = new string((sbyte*)((void*)intPtr));
			}
			this._pCookedUrl = pCookedUrl;
			this._path = this.GetUriPathInternal(true, false);
			this._filePath = this.GetUriPathInternal(false, false);
			int num3 = this._path.Length - this._filePath.Length;
			if (num3 > 0)
			{
				this._pathInfo = this._path.Substring(this._filePath.Length);
				int num4 = this._pathTranslated.Length - num3;
				if (num4 > 0)
				{
					this._pathTranslated = this._pathTranslated.Substring(0, num4);
				}
			}
			else
			{
				this._filePath = this._path;
				this._pathInfo = string.Empty;
			}
			this._queryString = this.GetQueryString();
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x00144FEC File Offset: 0x001431EC
		internal static IIS7WorkerRequest CreateWorkerRequest(IntPtr requestContext, bool etwProviderEnabled)
		{
			IIS7WorkerRequest iis7WorkerRequest = new IIS7WorkerRequest(requestContext, etwProviderEnabled);
			if (iis7WorkerRequest != null)
			{
				iis7WorkerRequest.Initialize();
			}
			return iis7WorkerRequest;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x0014500C File Offset: 0x0014320C
		internal void InitAppVars()
		{
			IntPtr ip;
			int length;
			IntPtr ip2;
			int length2;
			int num = UnsafeIISMethods.MgdGetApplicationInfo(this._context, out ip, out length, out ip2, out length2);
			if (num < 0)
			{
				throw new HttpException(SR.GetString("Cannot_retrieve_request_data"));
			}
			this._appPath = StringUtil.StringFromWCharPtr(ip, length);
			this._appPathTranslated = StringUtil.StringFromWCharPtr(ip2, length2);
			if (this._appPathTranslated != null && this._appPathTranslated.Length > 2 && !StringUtil.StringEndsWith(this._appPathTranslated, '\\'))
			{
				this._appPathTranslated += "\\";
			}
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x00145098 File Offset: 0x00143298
		internal void Initialize()
		{
			this.ReadRequestBasics();
			this.InitAppVars();
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x001450A8 File Offset: 0x001432A8
		internal void Dispose()
		{
			object disposeLockObj = this._disposeLockObj;
			IntPtr context;
			CancellationTokenHelper clientDisconnectTokenHelper;
			lock (disposeLockObj)
			{
				context = this._context;
				this._context = IntPtr.Zero;
				this._pCookedUrl = IntPtr.Zero;
				clientDisconnectTokenHelper = this._clientDisconnectTokenHelper;
			}
			if (clientDisconnectTokenHelper != null)
			{
				if (context != IntPtr.Zero)
				{
					bool flag2;
					int num = UnsafeIISMethods.MgdConfigureAsyncDisconnectNotification(context, false, out flag2);
				}
				clientDisconnectTokenHelper.Dispose();
			}
			if (this._channelBindingToken != null && !this._channelBindingToken.IsInvalid)
			{
				this._channelBindingToken.Dispose();
			}
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x00145148 File Offset: 0x00143348
		internal override void RaiseTraceEvent(IntegratedTraceType traceType, string eventData)
		{
			if (this._traceEnabled && this._context != IntPtr.Zero)
			{
				int flag = (traceType < IntegratedTraceType.DiagCritical) ? 4 : 2;
				if (EtwTrace.IsTraceEnabled(EtwTrace.InferVerbosity(traceType), flag))
				{
					string eventData2 = string.IsNullOrEmpty(eventData) ? string.Empty : eventData;
					UnsafeIISMethods.MgdEmitSimpleTrace(this._context, (int)traceType, eventData2);
				}
			}
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x001451A8 File Offset: 0x001433A8
		internal override void RaiseTraceEvent(WebBaseEvent webEvent)
		{
			if (this._traceEnabled && this._context != IntPtr.Zero && EtwTrace.IsTraceEnabled(webEvent.InferEtwTraceVerbosity(), 1))
			{
				int webEventType;
				int fieldCount;
				string[] fieldNames;
				int[] fieldTypes;
				string[] fieldData;
				webEvent.DeconstructWebEvent(out webEventType, out fieldCount, out fieldNames, out fieldTypes, out fieldData);
				UnsafeIISMethods.MgdEmitWebEventTrace(this._context, webEventType, fieldCount, fieldNames, fieldTypes, fieldData);
			}
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x00145200 File Offset: 0x00143400
		internal string GetUriPathInternal(bool includePathInfo, bool useParentContext)
		{
			string result = string.Empty;
			IntPtr ip;
			int num2;
			int num = UnsafeIISMethods.MgdGetUriPath(this._context, out ip, out num2, includePathInfo, useParentContext);
			if (num < 0)
			{
				throw new HttpException(SR.GetString("Cannot_retrieve_request_data"));
			}
			if (num2 > 0)
			{
				result = StringUtil.StringFromWCharPtr(ip, num2);
			}
			return result;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x00145246 File Offset: 0x00143446
		public override string GetUriPath()
		{
			return this._path;
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x00145250 File Offset: 0x00143450
		private unsafe IntPtr GetQueryStringPtr(out int length)
		{
			HTTP_COOKED_URL* cookedUrl = this.GetCookedUrl();
			if (cookedUrl->pQueryString != null)
			{
				int num = (int)(cookedUrl->QueryStringLength / 2);
				if (num > 1)
				{
					length = num - 1;
					return (IntPtr)((void*)(cookedUrl->pQueryString + 1));
				}
			}
			length = 0;
			return IntPtr.Zero;
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x00145298 File Offset: 0x00143498
		public override string GetQueryString()
		{
			int length;
			IntPtr queryStringPtr = this.GetQueryStringPtr(out length);
			if (queryStringPtr == IntPtr.Zero)
			{
				return string.Empty;
			}
			return StringUtil.StringFromWCharPtr(queryStringPtr, length);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x001452C8 File Offset: 0x001434C8
		internal string GetCacheUrl()
		{
			return this._cacheUrl;
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x001452D0 File Offset: 0x001434D0
		public override string GetRawUrl()
		{
			return HttpWorkerRequest.GetRawUrlHelper(this._cacheUrl);
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x001452EA File Offset: 0x001434EA
		public override string GetHttpVerbName()
		{
			return this._httpVerb;
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x001452F2 File Offset: 0x001434F2
		public override string GetHttpVersion()
		{
			return this.GetServerVariable("SERVER_PROTOCOL");
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x001452FF File Offset: 0x001434FF
		public override string GetRemoteAddress()
		{
			return this.GetServerVariable("REMOTE_ADDR");
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x0014530C File Offset: 0x0014350C
		public override string GetRemoteName()
		{
			return this.GetServerVariable("REMOTE_HOST");
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x00145319 File Offset: 0x00143519
		public override int GetRemotePort()
		{
			return UnsafeIISMethods.MgdGetRemotePort(this._context);
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x00145326 File Offset: 0x00143526
		public override string GetLocalAddress()
		{
			return this.GetServerVariable("LOCAL_ADDR");
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x00145333 File Offset: 0x00143533
		public override int GetLocalPort()
		{
			return UnsafeIISMethods.MgdGetLocalPort(this._context);
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x00145340 File Offset: 0x00143540
		public override string GetServerName()
		{
			return this.GetServerVariable("SERVER_NAME");
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x0014534D File Offset: 0x0014354D
		internal override string GetLocalPortAsString()
		{
			return this.GetServerVariable("SERVER_PORT");
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x0014535C File Offset: 0x0014355C
		public override bool IsSecure()
		{
			string serverVariable = this.GetServerVariable("HTTPS");
			return serverVariable != null && serverVariable.Equals("on");
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x00145385 File Offset: 0x00143585
		public override string GetFilePath()
		{
			return this._filePath;
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x0014538D File Offset: 0x0014358D
		public override string GetFilePathTranslated()
		{
			return this._pathTranslated;
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x00145395 File Offset: 0x00143595
		public override string GetPathInfo()
		{
			return this._pathInfo;
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x0014539D File Offset: 0x0014359D
		public override string GetAppPath()
		{
			return this._appPath;
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x001453A5 File Offset: 0x001435A5
		public override string GetAppPathTranslated()
		{
			return this._appPathTranslated;
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x001453B0 File Offset: 0x001435B0
		public override int GetPreloadedEntityBodyLength()
		{
			if (!this._preloadedLengthRead)
			{
				int preloadedLength = 0;
				int hresult = UnsafeIISMethods.MgdGetPreloadedSize(this._context, out preloadedLength);
				Misc.ThrowIfFailedHr(hresult);
				this._preloadedLength = preloadedLength;
				this._preloadedLengthRead = true;
			}
			return this._preloadedLength;
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x001453F0 File Offset: 0x001435F0
		public override byte[] GetPreloadedEntityBody()
		{
			if (!this._preloadedContentRead)
			{
				int preloadedEntityBodyLength = this.GetPreloadedEntityBodyLength();
				if (preloadedEntityBodyLength > 0)
				{
					byte[] array = new byte[preloadedEntityBodyLength];
					this.GetPreloadedContentInternal(array, 0, preloadedEntityBodyLength);
					this._preloadedContent = array;
				}
				this._preloadedContentRead = true;
			}
			return this._preloadedContent;
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x00145435 File Offset: 0x00143635
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return this.GetTotalEntityBodyLength() == this.GetPreloadedEntityBodyLength();
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x00145445 File Offset: 0x00143645
		public override int GetTotalEntityBodyLength()
		{
			return this._contentTotalLength;
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x00145450 File Offset: 0x00143650
		private int ReadEntityCoreSync(byte[] buffer, int offset, int size)
		{
			bool fAsync = false;
			int num = 0;
			int num2 = 0;
			try
			{
				base.IsInReadEntitySync = true;
				IntPtr intPtr;
				num2 = UnsafeIISMethods.MgdReadEntityBody(this._context, buffer, offset, size, fAsync, out num, out intPtr);
			}
			finally
			{
				base.IsInReadEntitySync = false;
			}
			if (num2 < 0)
			{
				this.RaiseCommunicationError(num2, false);
			}
			if (num > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, num);
			}
			return num;
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x001454B4 File Offset: 0x001436B4
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			if (size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.ReadEntityCoreSync(buffer, 0, size);
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x001454D0 File Offset: 0x001436D0
		public override int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			if (offset < 0 || buffer.Length - offset < size)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			return this.ReadEntityCoreSync(buffer, offset, size);
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x001454F2 File Offset: 0x001436F2
		public override long GetBytesRead()
		{
			throw new HttpException(SR.GetString("Not_supported"));
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsAsyncFlush
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x00145504 File Offset: 0x00143704
		public override IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			if (this._context == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			FlushAsyncResult flushAsyncResult = new FlushAsyncResult(callback, state);
			if (Interlocked.CompareExchange<AsyncResultBase>(ref this._asyncResultBase, flushAsyncResult, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Async_operation_pending"));
			}
			bool async = true;
			bool flag;
			int num = UnsafeIISMethods.MgdExplicitFlush(this._context, async, out flag);
			if (num < 0)
			{
				this._asyncResultBase = null;
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), num);
			}
			if (flag)
			{
				this._asyncResultBase = null;
				flushAsyncResult.Complete(0, 0, IntPtr.Zero, true);
				return flushAsyncResult;
			}
			return flushAsyncResult;
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x001455A0 File Offset: 0x001437A0
		public override void EndFlush(IAsyncResult asyncResult)
		{
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
			if (flushAsyncResult.HResult < 0)
			{
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), flushAsyncResult.HResult);
			}
			this._headersSent = true;
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06005DE3 RID: 24035 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsAsyncRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x00145604 File Offset: 0x00143804
		internal void OnAsyncCompletion(int bytesCompleted, int hresult, IntPtr pAsyncCompletionContext)
		{
			AsyncResultBase asyncResultBase = this._asyncResultBase;
			this._asyncResultBase = null;
			asyncResultBase.Complete(bytesCompleted, hresult, pAsyncCompletionContext, false);
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0014562C File Offset: 0x0014382C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("InvalidOffsetOrCount", new object[]
				{
					"offset",
					"count"
				}));
			}
			ReadAsyncResult readAsyncResult = new ReadAsyncResult(callback, state, buffer, offset, count, true);
			if (count == 0)
			{
				readAsyncResult.Complete(0, 0, IntPtr.Zero, true);
				return readAsyncResult;
			}
			if (Interlocked.CompareExchange<AsyncResultBase>(ref this._asyncResultBase, readAsyncResult, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Async_operation_pending"));
			}
			bool fAsync = true;
			int bytesCompleted = 0;
			IntPtr intPtr;
			int num = UnsafeIISMethods.MgdReadEntityBody(this._context, null, 0, count, fAsync, out bytesCompleted, out intPtr);
			if (num < 0)
			{
				this._asyncResultBase = null;
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), num);
			}
			if (intPtr != IntPtr.Zero)
			{
				this._asyncResultBase = null;
				readAsyncResult.Complete(bytesCompleted, 0, intPtr, true);
				return readAsyncResult;
			}
			return readAsyncResult;
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x00145734 File Offset: 0x00143934
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ReadAsyncResult readAsyncResult = asyncResult as ReadAsyncResult;
			if (readAsyncResult == null)
			{
				throw new ArgumentException(null, "asyncResult");
			}
			readAsyncResult.ReleaseWaitHandleWhenSignaled();
			if (readAsyncResult.HResult < 0)
			{
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), readAsyncResult.HResult);
			}
			return readAsyncResult.BytesRead;
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x00145796 File Offset: 0x00143996
		private void IncrementRequestsDisconnected()
		{
			if (!this._disconnected)
			{
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_DISCONNECTED);
				this._disconnected = true;
			}
		}

		// Token: 0x06005DE8 RID: 24040 RVA: 0x001457B0 File Offset: 0x001439B0
		internal Guid GetRequestCorrelationId()
		{
			IntPtr intPtr;
			ushort num;
			bool flag;
			int errorCode = UnsafeIISMethods.MgdGetCorrelationIdHeader(this._context, out intPtr, out num, out flag);
			Marshal.ThrowExceptionForHR(errorCode);
			try
			{
				if (intPtr != IntPtr.Zero && num > 0)
				{
					string text = StringUtil.StringFromCharPtr(intPtr, (int)num);
					if (!flag)
					{
						Guid result;
						Guid.TryParseExact(text, "D", out result);
						return result;
					}
					byte[] array = Convert.FromBase64String(text);
					if (array != null && array.Length == 16)
					{
						return new Guid(array);
					}
				}
			}
			catch
			{
			}
			return default(Guid);
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x0014584C File Offset: 0x00143A4C
		public override string GetKnownRequestHeader(int index)
		{
			if (!this._requestHeadersAvailable)
			{
				if (index <= 12)
				{
					if (index != 11)
					{
						if (index == 12)
						{
							if (this._contentType == 1)
							{
								return "application/x-www-form-urlencoded";
							}
						}
					}
					else if (this._contentType != 0)
					{
						return this._contentTotalLength.ToString(CultureInfo.InvariantCulture);
					}
				}
				else
				{
					if (index == 25)
					{
						return this.GetCookieHeaderInternal();
					}
					if (index == 39)
					{
						return this.GetUserAgentInternal();
					}
				}
				this.ReadRequestHeaders();
			}
			return this._knownRequestHeaders[index];
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x001458C4 File Offset: 0x00143AC4
		public override string GetUnknownRequestHeader(string name)
		{
			if (!this._requestHeadersAvailable)
			{
				this.ReadRequestHeaders();
			}
			int num = this._unknownRequestHeaders.Length;
			for (int i = 0; i < num; i++)
			{
				if (StringUtil.EqualsIgnoreCase(name, this._unknownRequestHeaders[i][0]))
				{
					return this._unknownRequestHeaders[i][1];
				}
			}
			return null;
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x00145912 File Offset: 0x00143B12
		public override string[][] GetUnknownRequestHeaders()
		{
			if (!this._requestHeadersAvailable)
			{
				this.ReadRequestHeaders();
			}
			return this._unknownRequestHeaders;
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x00145928 File Offset: 0x00143B28
		public override string GetServerVariable(string name)
		{
			if (StringUtil.StringStartsWith(name, "HTTP_"))
			{
				return this.GetServerVariableInternalAnsi(name);
			}
			return this.GetServerVariableInternal(name);
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x00145948 File Offset: 0x00143B48
		internal override void SendStatus(int statusCode, int subStatusCode, string statusDescription)
		{
			if (statusDescription == null)
			{
				statusDescription = string.Empty;
			}
			int hresult = UnsafeIISMethods.MgdSetStatusW(this._context, statusCode, subStatusCode, statusDescription, null, this._trySkipIisCustomErrors);
			this._trySkipIisCustomErrors = false;
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x00145982 File Offset: 0x00143B82
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this.SendStatus(statusCode, 0, statusDescription);
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x0014598D File Offset: 0x00143B8D
		internal override void SetHeaderEncoding(Encoding encoding)
		{
			this._headerEncoding = encoding;
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x00145996 File Offset: 0x00143B96
		public override void SendKnownResponseHeader(int index, string value)
		{
			if (index < 0 || index >= 30)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.SetKnownResponseHeader(index, value, false);
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x001459B5 File Offset: 0x00143BB5
		public override void SendUnknownResponseHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.SetUnknownResponseHeader(name, value, false);
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x001459CE File Offset: 0x00143BCE
		public override void SendCalculatedContentLength(int contentLength)
		{
			this.SendKnownResponseHeader(11, contentLength.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x001459E4 File Offset: 0x00143BE4
		public override bool HeadersSent()
		{
			return this._headersSent;
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x001459EC File Offset: 0x00143BEC
		public override bool IsClientConnected()
		{
			return !this._connectionClosed && UnsafeIISMethods.MgdIsClientConnected(this._context);
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x00145A04 File Offset: 0x00143C04
		internal bool TryGetClientDisconnectedCancellationToken(out CancellationToken cancellationToken)
		{
			if (HttpRuntime.IISVersion < IIS7WorkerRequest.IIS_VERSION_75)
			{
				cancellationToken = CancellationToken.None;
				return false;
			}
			if (this._clientDisconnectTokenHelper == null)
			{
				object disposeLockObj = this._disposeLockObj;
				lock (disposeLockObj)
				{
					if (this._clientDisconnectTokenHelper == null)
					{
						if (this._context == IntPtr.Zero)
						{
							this._clientDisconnectTokenHelper = CancellationTokenHelper.StaticDisposed;
						}
						else
						{
							bool isClientConnected;
							int errorCode = UnsafeIISMethods.MgdConfigureAsyncDisconnectNotification(this._context, true, out isClientConnected);
							Marshal.ThrowExceptionForHR(errorCode);
							LazyInitializer.EnsureInitialized<CancellationTokenHelper>(ref this._clientDisconnectTokenHelper, () => new CancellationTokenHelper(!isClientConnected));
						}
					}
				}
			}
			cancellationToken = this._clientDisconnectTokenHelper.Token;
			return true;
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x00145AD4 File Offset: 0x00143CD4
		internal void NotifyOfAsyncDisconnect()
		{
			CancellationTokenHelper cancellationTokenHelper = LazyInitializer.EnsureInitialized<CancellationTokenHelper>(ref this._clientDisconnectTokenHelper, () => new CancellationTokenHelper(true));
			cancellationTokenHelper.Cancel();
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x00145B12 File Offset: 0x00143D12
		internal bool IsHandlerExecutionDenied()
		{
			return UnsafeIISMethods.MgdIsHandlerExecutionDenied(this._context);
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x00145B20 File Offset: 0x00143D20
		internal void AbortConnection()
		{
			object disposeLockObj = this._disposeLockObj;
			lock (disposeLockObj)
			{
				if (this._context != IntPtr.Zero)
				{
					UnsafeIISMethods.MgdAbortConnection(this._context);
					this._connectionClosed = true;
				}
			}
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x00145B80 File Offset: 0x00143D80
		public override void CloseConnection()
		{
			UnsafeIISMethods.MgdCloseConnection(this._context);
			this._connectionClosed = true;
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x00145B94 File Offset: 0x00143D94
		public override IntPtr GetUserToken()
		{
			IntPtr zero = IntPtr.Zero;
			int hresult = UnsafeIISMethods.MgdGetUserToken(this._context, out zero);
			Misc.ThrowIfFailedHr(hresult);
			return zero;
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x00145BBC File Offset: 0x00143DBC
		public override IntPtr GetVirtualPathToken()
		{
			IntPtr zero = IntPtr.Zero;
			int hresult = UnsafeIISMethods.MgdGetVirtualToken(this._context, out zero);
			Misc.ThrowIfFailedHr(hresult);
			return zero;
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x00145BE4 File Offset: 0x00143DE4
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			if (this._connectionClosed)
			{
				return;
			}
			if (length > 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length));
			}
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x00145C00 File Offset: 0x00143E00
		public override void SendResponseFromMemory(IntPtr data, int length)
		{
			if (this._connectionClosed)
			{
				return;
			}
			this.SendResponseFromMemory(data, length, false);
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x00145C14 File Offset: 0x00143E14
		internal override void SendResponseFromMemory(IntPtr data, int length, bool isBufferFromUnmanagedPool)
		{
			if (length > 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length, isBufferFromUnmanagedPool ? BufferType.UnmanagedPool : BufferType.Managed));
			}
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x00145C2E File Offset: 0x00143E2E
		internal void SendResponseFromIISAllocatedRequestMemory(IntPtr data, int length)
		{
			if (data != IntPtr.Zero && length >= 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length, BufferType.IISAllocatedRequestMemory));
			}
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x00145C4F File Offset: 0x00143E4F
		internal override void TransmitFile(string filename, long offset, long length, bool isImpersonating)
		{
			if (this._connectionClosed)
			{
				return;
			}
			if (length > 0L)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(filename, offset, length));
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool SupportsLongTransmitFile
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x00145C70 File Offset: 0x00143E70
		private void SendResponseFromFileStream(FileStream f, long offset, long length)
		{
			long length2 = f.Length;
			if (length == -1L)
			{
				length = length2 - offset;
			}
			if (offset < 0L || length > length2 - offset)
			{
				throw new HttpException(SR.GetString("Invalid_range"));
			}
			if (length > 0L)
			{
				if (offset > 0L)
				{
					f.Seek(offset, SeekOrigin.Begin);
				}
				byte[] array = new byte[(int)length];
				int num = f.Read(array, 0, (int)length);
				if (num > 0)
				{
					this.AddBodyToCachedResponse(new MemoryBytes(array, num));
				}
			}
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x00145CE0 File Offset: 0x00143EE0
		public override void SendResponseFromFile(string name, long offset, long length)
		{
			if (this._connectionClosed)
			{
				return;
			}
			if (length == 0L)
			{
				return;
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
				this.SendResponseFromFileStream(fileStream, offset, length);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x00145D2C File Offset: 0x00143F2C
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
			if (this._connectionClosed)
			{
				return;
			}
			if (length == 0L)
			{
				return;
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(new SafeFileHandle(handle, false), FileAccess.Read, 0, false);
				this.SendResponseFromFileStream(fileStream, offset, length);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x00145D80 File Offset: 0x00143F80
		public override void FlushResponse(bool finalFlush)
		{
			if (this._connectionClosed)
			{
				return;
			}
			this.FlushCachedResponse(finalFlush);
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x00006164 File Offset: 0x00004364
		public override void EndOfRequest()
		{
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x00145D92 File Offset: 0x00143F92
		public override Guid RequestTraceIdentifier
		{
			get
			{
				return this._traceId;
			}
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x00145D9C File Offset: 0x00143F9C
		internal void PushPromise(string virtualPath, string queryString, string method, NameValueCollection headers)
		{
			string[] headersNames = null;
			string[] array = null;
			int num = 0;
			if (headers != null && headers.Count > 0)
			{
				num = headers.Count;
				headersNames = headers.AllKeys;
				array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = headers.Get(i);
				}
			}
			int num2 = UnsafeIISMethods.MgdPushPromise(this._context, virtualPath, queryString, method, num, headersNames, array);
			if (num2 == -2147467263)
			{
				throw new PlatformNotSupportedException();
			}
			Misc.ThrowIfFailedHr(num2);
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x00145E17 File Offset: 0x00144017
		internal unsafe HTTP_COOKED_URL* GetCookedUrl()
		{
			return (HTTP_COOKED_URL*)((void*)this._pCookedUrl);
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x00145E24 File Offset: 0x00144024
		private string GetServerVariableInternalAnsi(string name)
		{
			IntPtr intPtr;
			int length;
			int hresult = UnsafeIISMethods.MgdGetServerVariableA(this._context, name, out intPtr, out length);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr != IntPtr.Zero)
			{
				return StringUtil.StringFromCharPtr(intPtr, length);
			}
			return null;
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x00145E60 File Offset: 0x00144060
		private string GetServerVariableInternal(string name)
		{
			IntPtr intPtr;
			int length;
			int hresult = UnsafeIISMethods.MgdGetServerVariableW(this._context, name, out intPtr, out length);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr != IntPtr.Zero)
			{
				return StringUtil.StringFromWCharPtr(intPtr, length);
			}
			return null;
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x00145E9C File Offset: 0x0014409C
		internal string GetCurrentModuleName()
		{
			string result = null;
			IntPtr ip;
			int num;
			int hresult = UnsafeIISMethods.MgdGetCurrentModuleName(this._context, out ip, out num);
			Misc.ThrowIfFailedHr(hresult);
			if (num > 0)
			{
				result = StringUtil.StringFromWCharPtr(ip, num);
			}
			return result;
		}

		// Token: 0x06005E0D RID: 24077 RVA: 0x00145ED0 File Offset: 0x001440D0
		private string GetUserAgentInternal()
		{
			IntPtr intPtr;
			int length;
			int hresult = UnsafeIISMethods.MgdGetUserAgent(this._context, out intPtr, out length);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr != IntPtr.Zero)
			{
				return StringUtil.StringFromCharPtr(intPtr, length);
			}
			return null;
		}

		// Token: 0x06005E0E RID: 24078 RVA: 0x00145F0C File Offset: 0x0014410C
		private string GetCookieHeaderInternal()
		{
			IntPtr intPtr;
			int length;
			int hresult = UnsafeIISMethods.MgdGetCookieHeader(this._context, out intPtr, out length);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr != IntPtr.Zero)
			{
				return StringUtil.StringFromCharPtr(intPtr, length);
			}
			return null;
		}

		// Token: 0x06005E0F RID: 24079 RVA: 0x00145F48 File Offset: 0x00144148
		private void ReadRequestHeaders()
		{
			if (this._requestHeadersAvailable)
			{
				return;
			}
			this._knownRequestHeaders = new string[40];
			ArrayList arrayList = new ArrayList();
			string serverVariable = this.GetServerVariable("ALL_RAW");
			int num = (serverVariable != null) ? serverVariable.Length : 0;
			int i = 0;
			while (i < num)
			{
				int num2 = serverVariable.IndexOfAny(IIS7WorkerRequest.s_ColonOrNL, i);
				if (num2 < 0)
				{
					break;
				}
				if (serverVariable[num2] == '\n')
				{
					i = num2 + 1;
				}
				else if (num2 == i)
				{
					i++;
				}
				else
				{
					string text = serverVariable.Substring(i, num2 - i).Trim();
					int num3 = serverVariable.IndexOf('\n', num2 + 1);
					if (num3 < 0)
					{
						num3 = num;
					}
					while (num3 < num - 1 && serverVariable[num3 + 1] == ' ')
					{
						num3 = serverVariable.IndexOf('\n', num3 + 1);
						if (num3 < 0)
						{
							num3 = num;
						}
					}
					string text2 = serverVariable.Substring(num2 + 1, num3 - num2 - 1).Trim();
					int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(text);
					if (knownRequestHeaderIndex >= 0)
					{
						this._knownRequestHeaders[knownRequestHeaderIndex] = text2;
					}
					else
					{
						arrayList.Add(text);
						arrayList.Add(text2);
					}
					i = num3 + 1;
				}
			}
			int num4 = arrayList.Count / 2;
			this._unknownRequestHeaders = new string[num4][];
			int num5 = 0;
			for (i = 0; i < num4; i++)
			{
				this._unknownRequestHeaders[i] = new string[2];
				this._unknownRequestHeaders[i][0] = (string)arrayList[num5++];
				this._unknownRequestHeaders[i][1] = (string)arrayList[num5++];
			}
			this._requestHeadersAvailable = true;
		}

		// Token: 0x06005E10 RID: 24080 RVA: 0x001460E5 File Offset: 0x001442E5
		private void AddBodyToCachedResponse(MemoryBytes bytes)
		{
			if (this._cachedResponseBodyBytes == null)
			{
				this._cachedResponseBodyBytes = new ArrayList();
			}
			this._cachedResponseBodyBytes.Add(bytes);
			this._cachedResponseBodyLength += bytes.Size;
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x0014611C File Offset: 0x0014431C
		private void FlushCachedResponse(bool isFinal)
		{
			if (this._connectionClosed)
			{
				return;
			}
			if (this._context == IntPtr.Zero)
			{
				return;
			}
			int num = 0;
			IntPtr[] array = null;
			int[] array2 = null;
			long num2 = 0L;
			int[] array3 = null;
			try
			{
				if (this._cachedResponseBodyLength > 0)
				{
					num = this._cachedResponseBodyBytes.Count;
					array = this.AllocatorProvider.IntPtrBufferAllocator.GetBuffer(num);
					array2 = this.AllocatorProvider.IntBufferAllocator.GetBuffer(num);
					array3 = this.AllocatorProvider.IntBufferAllocator.GetBuffer(num);
					for (int i = 0; i < num; i++)
					{
						MemoryBytes memoryBytes = (MemoryBytes)this._cachedResponseBodyBytes[i];
						array[i] = memoryBytes.LockMemory();
						array3[i] = (int)memoryBytes.BufferType;
						array2[i] = memoryBytes.Size;
						if (memoryBytes.UseTransmitFile)
						{
							num2 += memoryBytes.FileSize;
						}
						else
						{
							num2 += (long)memoryBytes.Size;
						}
					}
				}
				int num3 = (int)num2;
				if (num3 > 0)
				{
					PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_OUT, num3);
				}
				this.FlushCore(true, num, array, array2, array3);
			}
			finally
			{
				this.UnlockCachedResponseBytes();
				this.AllocatorProvider.IntPtrBufferAllocator.ReuseBuffer(array);
				this.AllocatorProvider.IntBufferAllocator.ReuseBuffer(array2);
				this.AllocatorProvider.IntBufferAllocator.ReuseBuffer(array3);
			}
		}

		// Token: 0x06005E12 RID: 24082 RVA: 0x00146274 File Offset: 0x00144474
		private void FlushCore(bool keepConnected, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int[] bodyFragmentTypes)
		{
			if (this._connectionClosed)
			{
				return;
			}
			if (this._context == IntPtr.Zero)
			{
				return;
			}
			int num = UnsafeIISMethods.MgdFlushCore(this._context, keepConnected, numBodyFragments, bodyFragments, bodyFragmentLengths, bodyFragmentTypes);
			if (num < 0)
			{
				this.RaiseCommunicationError(num, false);
			}
		}

		// Token: 0x06005E13 RID: 24083 RVA: 0x001462BC File Offset: 0x001444BC
		internal void UnlockCachedResponseBytes()
		{
			if (this._cachedResponseBodyBytes != null)
			{
				int count = this._cachedResponseBodyBytes.Count;
				for (int i = 0; i < count; i++)
				{
					try
					{
						((MemoryBytes)this._cachedResponseBodyBytes[i]).UnlockMemory();
					}
					catch
					{
					}
				}
			}
			this.ResetCachedResponse();
		}

		// Token: 0x06005E14 RID: 24084 RVA: 0x0014631C File Offset: 0x0014451C
		private void ResetCachedResponse()
		{
			this._cachedResponseBodyLength = 0;
			this._cachedResponseBodyBytes = null;
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x0014632C File Offset: 0x0014452C
		private int GetPreloadedContentInternal(byte[] buffer, int offset, int length)
		{
			if (offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = 0;
			int hresult = UnsafeIISMethods.MgdGetPreloadedContent(this._context, buffer, offset, length, out num);
			Misc.ThrowIfFailedHr(hresult);
			if (num > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, num);
			}
			return num;
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x00146384 File Offset: 0x00144584
		internal override string SetupKernelCaching(int secondsToLive, string originalCacheUrl, bool enableKernelCacheForVaryByStar)
		{
			string cacheUrl = this._cacheUrl;
			if (originalCacheUrl != null && originalCacheUrl != cacheUrl)
			{
				return null;
			}
			if (string.IsNullOrEmpty(cacheUrl) || (!enableKernelCacheForVaryByStar && cacheUrl.IndexOf('?') != -1))
			{
				return null;
			}
			int num = UnsafeIISMethods.MgdSetKernelCachePolicy(this._context, secondsToLive);
			if (num < 0)
			{
				return null;
			}
			return cacheUrl;
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x001463D2 File Offset: 0x001445D2
		internal override void DisableKernelCache()
		{
			UnsafeIISMethods.MgdDisableKernelCache(this._context);
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x001463DF File Offset: 0x001445DF
		internal override void DisableUserCache()
		{
			UnsafeIISMethods.MgdDisableUserCache(this._context);
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x001463EC File Offset: 0x001445EC
		private void DisableIISCache()
		{
			this.DisableKernelCache();
			this.DisableUserCache();
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x001463FA File Offset: 0x001445FA
		// (set) Token: 0x06005E1B RID: 24091 RVA: 0x00146402 File Offset: 0x00144602
		internal override bool TrySkipIisCustomErrors
		{
			get
			{
				return this._trySkipIisCustomErrors;
			}
			set
			{
				this._trySkipIisCustomErrors = value;
			}
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x0014640C File Offset: 0x0014460C
		internal string ReMapHandlerAndGetHandlerTypeString(HttpContext httpContext, string path, out bool handlerExists)
		{
			string result = null;
			IntPtr ip;
			int num;
			int hresult = UnsafeIISMethods.MgdReMapHandler(this._context, path, out ip, out num, out handlerExists);
			Misc.ThrowIfFailedHr(hresult);
			if (num > 0)
			{
				result = StringUtil.StringFromWCharPtr(ip, num);
			}
			if (handlerExists)
			{
				this.ReadRequestBasics();
				httpContext.ConfigurationPath = null;
				try
				{
					this._rewriteNotifyDisabled = true;
					httpContext.Request.InternalRewritePath(VirtualPath.CreateAllowNull(this._filePath), VirtualPath.CreateAllowNull(this._pathInfo), this._queryString, this._rebaseClientPath);
				}
				finally
				{
					this._rewriteNotifyDisabled = false;
				}
			}
			return result;
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x001464A0 File Offset: 0x001446A0
		internal string MapHandlerAndGetHandlerTypeString(string method, string path, bool convertNativeStaticFileModule, bool ignoreWildcardMappings)
		{
			string result = null;
			IntPtr ip;
			int num;
			int hresult = UnsafeIISMethods.MgdMapHandler(this._context, method, path, out ip, out num, convertNativeStaticFileModule, ignoreWildcardMappings);
			Misc.ThrowIfFailedHr(hresult);
			if (num > 0)
			{
				result = StringUtil.StringFromWCharPtr(ip, num);
			}
			return result;
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x001464D8 File Offset: 0x001446D8
		internal string GetManagedHandlerType()
		{
			string result = null;
			IntPtr ip;
			int num;
			int hresult = UnsafeIISMethods.MgdGetHandlerTypeString(this._context, out ip, out num);
			Misc.ThrowIfFailedHr(hresult);
			if (num > 0)
			{
				result = StringUtil.StringFromWCharPtr(ip, num);
			}
			return result;
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0014650C File Offset: 0x0014470C
		internal void SetRemapHandler(string handlerType, string handlerName)
		{
			int hresult = UnsafeIISMethods.MgdSetRemapHandler(this._context, handlerName, handlerType);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x00146530 File Offset: 0x00144730
		internal void SetScriptMapForRemapHandler()
		{
			int hresult = UnsafeIISMethods.MgdSetScriptMapForRemapHandler(this._context);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x00146550 File Offset: 0x00144750
		internal void RewriteNotifyPipeline(string newPath, string newQueryString, bool rebaseClientPath)
		{
			if (this._rewriteNotifyDisabled)
			{
				return;
			}
			if (IntPtr.Zero != this._context)
			{
				string pszUrl = newPath;
				if (newQueryString != null)
				{
					pszUrl = newPath + "?" + newQueryString;
				}
				UnsafeIISMethods.MgdRewriteUrl(this._context, pszUrl, newQueryString != null);
				this._rebaseClientPath = rebaseClientPath;
			}
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x001465A2 File Offset: 0x001447A2
		internal void DisableNotifications(RequestNotification notifications, RequestNotification postNotifications)
		{
			UnsafeIISMethods.MgdDisableNotifications(this._context, notifications, postNotifications);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x001465B1 File Offset: 0x001447B1
		internal void SuppressSendResponseNotifications()
		{
			UnsafeIISMethods.MgdSuppressSendResponseNotifications(this._context);
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x001465BE File Offset: 0x001447BE
		internal void PushResponseToNative()
		{
			this.FlushCachedResponse(false);
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x001465C7 File Offset: 0x001447C7
		internal void ClearResponse(bool clearEntity, bool clearHeaders)
		{
			UnsafeIISMethods.MgdClearResponse(this._context, clearEntity, clearHeaders);
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x001465D8 File Offset: 0x001447D8
		private void GetStatusChanges(HttpContext ctx)
		{
			string description = null;
			ushort statusCode;
			ushort subStatusCode;
			IntPtr intPtr;
			ushort length;
			int hresult = UnsafeIISMethods.MgdGetStatusChanges(this._context, out statusCode, out subStatusCode, out intPtr, out length);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr != IntPtr.Zero)
			{
				description = StringUtil.StringFromCharPtr(intPtr, (int)length);
			}
			this._trySkipIisCustomErrors = false;
			ctx.Response.SynchronizeStatus((int)statusCode, (int)subStatusCode, description);
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x00146630 File Offset: 0x00144830
		internal IntPtr AllocateRequestMemory(int size)
		{
			if (size > 0)
			{
				return UnsafeIISMethods.MgdAllocateRequestMemory(this._context, size);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x00146648 File Offset: 0x00144848
		internal ArrayList GetBufferedResponseChunks(bool disableRecycling, ArrayList substElements, ref bool hasSubstBlocks)
		{
			int num = 32;
			IntPtr[] buffer = this.AllocatorProvider.IntPtrBufferAllocator.GetBuffer(num);
			int[] buffer2 = this.AllocatorProvider.IntBufferAllocator.GetBuffer(num);
			int[] buffer3 = this.AllocatorProvider.IntBufferAllocator.GetBuffer(num);
			int num2 = UnsafeIISMethods.MgdGetResponseChunks(this._context, ref num, buffer, buffer2, buffer3);
			if (num2 < 0)
			{
				if (num2 == -2147024774)
				{
					this.AllocatorProvider.IntPtrBufferAllocator.ReuseBuffer(buffer);
					this.AllocatorProvider.IntBufferAllocator.ReuseBuffer(buffer2);
					this.AllocatorProvider.IntBufferAllocator.ReuseBuffer(buffer3);
					buffer = this.AllocatorProvider.IntPtrBufferAllocator.GetBuffer(num);
					buffer2 = this.AllocatorProvider.IntBufferAllocator.GetBuffer(num);
					buffer3 = this.AllocatorProvider.IntBufferAllocator.GetBuffer(num);
					num2 = UnsafeIISMethods.MgdGetResponseChunks(this._context, ref num, buffer, buffer2, buffer3);
				}
				if (num2 == -2147024883)
				{
					throw new InvalidOperationException(SR.GetString("Invalid_http_data_chunk"));
				}
				Misc.ThrowIfFailedHr(num2);
			}
			ArrayList arrayList = new ArrayList();
			HttpResponseUnmanagedBufferElement httpResponseUnmanagedBufferElement = null;
			HttpSubstBlockResponseElement[] array = null;
			if (substElements != null)
			{
				array = (HttpSubstBlockResponseElement[])substElements.ToArray(typeof(HttpSubstBlockResponseElement));
			}
			for (int i = 0; i < num; i++)
			{
				if (buffer3[i] == 0)
				{
					if (array != null)
					{
						int num3 = -1;
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j].PointerEquals(buffer[i]))
							{
								num3 = j;
								break;
							}
						}
						if (num3 != -1)
						{
							if (httpResponseUnmanagedBufferElement != null)
							{
								arrayList.Add(httpResponseUnmanagedBufferElement);
								httpResponseUnmanagedBufferElement = null;
							}
							arrayList.Add(array[num3]);
							hasSubstBlocks = true;
							goto IL_2F2;
						}
					}
					if (httpResponseUnmanagedBufferElement == null)
					{
						httpResponseUnmanagedBufferElement = new HttpResponseUnmanagedBufferElement();
						if (disableRecycling)
						{
							httpResponseUnmanagedBufferElement.DisableRecycling();
						}
					}
					int num4 = buffer2[i];
					if (num4 <= httpResponseUnmanagedBufferElement.FreeBytes)
					{
						httpResponseUnmanagedBufferElement.Append(buffer[i], 0, num4);
					}
					else
					{
						int num5 = 0;
						do
						{
							int num6 = httpResponseUnmanagedBufferElement.Append(buffer[i], num5, num4);
							num4 -= num6;
							num5 += num6;
							if (httpResponseUnmanagedBufferElement.FreeBytes == 0)
							{
								arrayList.Add(httpResponseUnmanagedBufferElement);
								httpResponseUnmanagedBufferElement = new HttpResponseUnmanagedBufferElement();
								if (disableRecycling)
								{
									httpResponseUnmanagedBufferElement.DisableRecycling();
								}
							}
						}
						while (num4 > 0);
					}
					if (httpResponseUnmanagedBufferElement.FreeBytes == 0)
					{
						arrayList.Add(httpResponseUnmanagedBufferElement);
						httpResponseUnmanagedBufferElement = null;
					}
				}
				else if (buffer3[i] == 1)
				{
					long num7 = 0L;
					long num8 = 0L;
					num2 = UnsafeIISMethods.MgdGetFileChunkInfo(this._context, i, out num7, out num8);
					Misc.ThrowIfFailedHr(num2);
					while (num8 > 0L && num7 >= 0L)
					{
						if (httpResponseUnmanagedBufferElement == null || httpResponseUnmanagedBufferElement.FreeBytes == 0)
						{
							if (httpResponseUnmanagedBufferElement != null)
							{
								arrayList.Add(httpResponseUnmanagedBufferElement);
							}
							httpResponseUnmanagedBufferElement = new HttpResponseUnmanagedBufferElement();
							if (disableRecycling)
							{
								httpResponseUnmanagedBufferElement.DisableRecycling();
							}
						}
						int num9 = httpResponseUnmanagedBufferElement.FreeBytes;
						if ((long)httpResponseUnmanagedBufferElement.FreeBytes > num8)
						{
							num9 = (int)num8;
						}
						num2 = UnsafeIISMethods.MgdReadChunkHandle(this._context, buffer[i], num7, ref num9, httpResponseUnmanagedBufferElement.FreeLocation);
						Misc.ThrowIfFailedHr(num2);
						httpResponseUnmanagedBufferElement.AdjustSize(num9);
						num8 -= (long)num9;
						num7 += (long)num9;
					}
				}
				IL_2F2:;
			}
			if (httpResponseUnmanagedBufferElement != null)
			{
				arrayList.Add(httpResponseUnmanagedBufferElement);
			}
			this.AllocatorProvider.IntPtrBufferAllocator.ReuseBuffer(buffer);
			this.AllocatorProvider.IntBufferAllocator.ReuseBuffer(buffer2);
			this.AllocatorProvider.IntBufferAllocator.ReuseBuffer(buffer3);
			return arrayList;
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x00146998 File Offset: 0x00144B98
		internal bool IsResponseBuffered()
		{
			int num = 0;
			int num2 = UnsafeIISMethods.MgdGetResponseChunks(this._context, ref num, null, null, null);
			if (num2 != -2147024774)
			{
				Misc.ThrowIfFailedHr(num2);
			}
			return num > 0;
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x001469CC File Offset: 0x00144BCC
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void SetPrincipal(IPrincipal user)
		{
			string text = null;
			string text2 = null;
			IntPtr token = IntPtr.Zero;
			if (user != null)
			{
				if (user.Identity != null)
				{
					text = user.Identity.Name;
					text2 = user.Identity.AuthenticationType;
					WindowsIdentity windowsIdentity = user.Identity as WindowsIdentity;
					if (windowsIdentity != null)
					{
						token = windowsIdentity.Token;
					}
					else if (AppSettings.AllowAnonymousImpersonation)
					{
						UnsafeIISMethods.MgdGetAnonymousUserToken(this._context, out token);
					}
				}
				if (text == null)
				{
					text = string.Empty;
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				if (!IIS7WorkerRequest.IsValidUsername(text))
				{
					throw new ArgumentException();
				}
			}
			int hresult = UnsafeIISMethods.MgdSetRequestPrincipal(this._context, text, text2, token);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x00146A6B File Offset: 0x00144C6B
		private static bool IsValidUsername(string username)
		{
			return AppSettings.AllowRelaxedHttpUserName || username.IndexOf('\0') == -1;
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x00146A80 File Offset: 0x00144C80
		internal void ResponseFilterInstalled()
		{
			UnsafeIISMethods.MgdSetResponseFilter(this._context);
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x00146A90 File Offset: 0x00144C90
		private void RaiseCommunicationError(int result, bool throwOnDisconnect)
		{
			if (UnsafeIISMethods.MgdIsClientConnected(this._context))
			{
				throw new HttpException(SR.GetString("Server_Support_Function_Error", new object[]
				{
					result.ToString("X8", CultureInfo.InvariantCulture)
				}), Marshal.GetExceptionForHR(result));
			}
			this.IncrementRequestsDisconnected();
			if (throwOnDisconnect)
			{
				throw new HttpException(SR.GetString("Server_Support_Function_Error_Disconnect", new object[]
				{
					result.ToString("X8", CultureInfo.InvariantCulture)
				}), result);
			}
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x00146B10 File Offset: 0x00144D10
		internal void ExplicitFlush()
		{
			bool async = false;
			bool flag;
			int num = UnsafeIISMethods.MgdExplicitFlush(this._context, async, out flag);
			if (num < 0)
			{
				this.RaiseCommunicationError(num, true);
			}
			this._headersSent = true;
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x00146B44 File Offset: 0x00144D44
		internal void SetServerVariable(string name, string value)
		{
			int hresult = UnsafeIISMethods.MgdSetServerVariableW(this._context, name, value);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x00146B68 File Offset: 0x00144D68
		internal void SetRequestHeader(string name, string value, bool replace)
		{
			int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(name);
			if (knownRequestHeaderIndex >= 0)
			{
				this.SetKnownRequestHeader(knownRequestHeaderIndex, value, replace);
				return;
			}
			this.SetUnknownRequestHeader(name, value, replace);
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x00146B94 File Offset: 0x00144D94
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		private void SetKnownRequestHeader(int index, string value, bool replace)
		{
			if (index == 39)
			{
				index = 40;
			}
			byte[] value2;
			int nullTerminatedByteArray = StringUtil.GetNullTerminatedByteArray(this._headerEncoding, value, out value2);
			int hresult = UnsafeIISMethods.MgdSetKnownHeader(this._context, true, replace, (ushort)index, value2, (ushort)nullTerminatedByteArray);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x00146BD4 File Offset: 0x00144DD4
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		private void SetUnknownRequestHeader(string name, string value, bool replace)
		{
			byte[] value2;
			int nullTerminatedByteArray = StringUtil.GetNullTerminatedByteArray(this._headerEncoding, value, out value2);
			byte[] header;
			StringUtil.GetNullTerminatedByteArray(this._headerEncoding, name, out header);
			int hresult = UnsafeIISMethods.MgdSetUnknownHeader(this._context, true, replace, header, value2, (ushort)nullTerminatedByteArray);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x00146C18 File Offset: 0x00144E18
		internal void SetResponseHeader(string name, string value, bool replace)
		{
			int knownResponseHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(name);
			if (knownResponseHeaderIndex >= 0)
			{
				this.SetKnownResponseHeader(knownResponseHeaderIndex, value, replace);
				return;
			}
			this.SetUnknownResponseHeader(name, value, replace);
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x00146C44 File Offset: 0x00144E44
		private void SetKnownResponseHeader(int index, string value, bool replace)
		{
			if (index == 29 || index == 27 || index == 26)
			{
				this.SetUnknownResponseHeader(HttpWorkerRequest.GetKnownResponseHeaderName(index), value, replace);
				return;
			}
			byte[] value2;
			int nullTerminatedByteArray = StringUtil.GetNullTerminatedByteArray(this._headerEncoding, value, out value2);
			int hresult = UnsafeIISMethods.MgdSetKnownHeader(this._context, false, replace, (ushort)index, value2, (ushort)nullTerminatedByteArray);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x00146C98 File Offset: 0x00144E98
		private void SetUnknownResponseHeader(string name, string value, bool replace)
		{
			if (StringUtil.EqualsIgnoreCase(name, "Set-Cookie"))
			{
				this.DisableIISCache();
			}
			byte[] value2;
			int nullTerminatedByteArray = StringUtil.GetNullTerminatedByteArray(this._headerEncoding, value, out value2);
			byte[] header;
			StringUtil.GetNullTerminatedByteArray(this._headerEncoding, name, out header);
			int hresult = UnsafeIISMethods.MgdSetUnknownHeader(this._context, false, replace, header, value2, (ushort)nullTerminatedByteArray);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x00146CF0 File Offset: 0x00144EF0
		private unsafe void GetServerVarChanges(HttpContext ctx)
		{
			int num;
			IntPtr intPtr;
			IntPtr intPtr2;
			int num2;
			IntPtr intPtr3;
			int hresult = UnsafeIISMethods.MgdGetServerVarChanges(this._context, out num, out intPtr, out intPtr2, out num2, out intPtr3);
			Misc.ThrowIfFailedHr(hresult);
			if (num2 != 0)
			{
				int* ptr = (int*)intPtr3.ToPointer();
				IntPtr* ptr2 = (IntPtr*)intPtr.ToPointer();
				IntPtr* ptr3 = (IntPtr*)intPtr2.ToPointer();
				for (int i = 0; i < num2; i++)
				{
					int num3 = ptr[i];
					IntPtr intPtr4 = ptr2[(IntPtr)num3 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
					IntPtr intPtr5 = ptr3[(IntPtr)num3 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
					string name = StringUtil.StringFromCharPtr(intPtr4, UnsafeNativeMethods.lstrlenA(intPtr4));
					string value = null;
					if (intPtr5 != IntPtr.Zero)
					{
						value = StringUtil.StringFromWCharPtr(intPtr5, UnsafeNativeMethods.lstrlenW(intPtr5));
					}
					ctx.Request.SynchronizeServerVariable(name, value);
				}
			}
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x00146DB8 File Offset: 0x00144FB8
		private unsafe void GetHeaderChanges(HttpContext ctx, bool forRequest)
		{
			int num = forRequest ? 41 : 30;
			int knownHeaderIndex = -1;
			IntPtr intPtr;
			int num2;
			IntPtr intPtr2;
			IntPtr intPtr3;
			IntPtr intPtr4;
			int num3;
			IntPtr intPtr5;
			int hresult = UnsafeIISMethods.MgdGetHeaderChanges(this._context, forRequest, out intPtr, out num2, out intPtr2, out intPtr3, out intPtr4, out num3, out intPtr5);
			Misc.ThrowIfFailedHr(hresult);
			int* ptr = (int*)intPtr4.ToPointer();
			IntPtr* ptr2 = (IntPtr*)intPtr.ToPointer();
			int i = 0;
			while (i < num + 1)
			{
				int num4 = ptr[i];
				if (num4 < 0)
				{
					break;
				}
				string name;
				if (forRequest)
				{
					if (num4 > 40)
					{
						throw new NotSupportedException();
					}
					if (num4 < 39)
					{
						name = HttpWorkerRequest.GetKnownRequestHeaderName(num4);
						goto IL_B7;
					}
					if (num4 == 39)
					{
						name = "Translate";
						goto IL_B7;
					}
					name = HttpWorkerRequest.GetKnownRequestHeaderName(39);
					goto IL_B7;
				}
				else
				{
					if (num4 >= 30)
					{
						throw new NotSupportedException();
					}
					if (num4 != 26)
					{
						name = HttpWorkerRequest.GetKnownResponseHeaderName(num4);
						knownHeaderIndex = num4;
						goto IL_B7;
					}
				}
				IL_10D:
				i++;
				continue;
				IL_B7:
				IntPtr intPtr6 = ptr2[(IntPtr)num4 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
				string value = null;
				if (intPtr6 != IntPtr.Zero)
				{
					value = StringUtil.StringFromCharPtr(intPtr6, UnsafeNativeMethods.lstrlenA(intPtr6));
				}
				if (forRequest)
				{
					ctx.Request.SynchronizeHeader(name, value);
					goto IL_10D;
				}
				ctx.Response.SynchronizeHeader(knownHeaderIndex, name, value);
				goto IL_10D;
			}
			if (num3 != 0)
			{
				int* ptr3 = (int*)intPtr5.ToPointer();
				IntPtr* ptr4 = (IntPtr*)intPtr2.ToPointer();
				IntPtr* ptr5 = (IntPtr*)intPtr3.ToPointer();
				for (int j = 0; j < num3; j++)
				{
					int num5 = ptr3[j];
					IntPtr intPtr7 = ptr4[(IntPtr)num5 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
					IntPtr intPtr8 = (num5 < num2) ? ptr5[(IntPtr)num5 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] : IntPtr.Zero;
					string text = StringUtil.StringFromCharPtr(intPtr7, UnsafeNativeMethods.lstrlenA(intPtr7));
					string value2 = null;
					if (intPtr8 != IntPtr.Zero)
					{
						value2 = StringUtil.StringFromCharPtr(intPtr8, UnsafeNativeMethods.lstrlenA(intPtr8));
					}
					if (forRequest)
					{
						ctx.Request.SynchronizeHeader(text, value2);
					}
					else
					{
						int knownHeaderIndex2 = -1;
						if (StringUtil.EqualsIgnoreCase(text, "Set-Cookie"))
						{
							knownHeaderIndex2 = 27;
						}
						ctx.Response.SynchronizeHeader(knownHeaderIndex2, text, value2);
					}
				}
			}
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x00146FC0 File Offset: 0x001451C0
		private IPrincipal GetUserPrincipal()
		{
			int num = 0;
			int num2 = 0;
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			IntPtr intPtr4;
			int hresult = UnsafeIISMethods.MgdGetPrincipal(this._context, AppDomain.CurrentDomain.Id, out intPtr, out intPtr2, ref num, out intPtr3, ref num2, out intPtr4);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr4 != IntPtr.Zero)
			{
				try
				{
					return (IPrincipal)GCUtil.UnrootObject(intPtr4);
				}
				catch (Exception innerException)
				{
					throw new HttpException(SR.GetString("Failed_to_execute_child_request"), innerException);
				}
			}
			string text = string.Empty;
			if (intPtr3 != IntPtr.Zero && num2 > 0)
			{
				text = StringUtil.StringFromWCharPtr(intPtr3, num2);
			}
			string type = string.Empty;
			if (intPtr2 != IntPtr.Zero && num > 0)
			{
				type = StringUtil.StringFromWCharPtr(intPtr2, num);
			}
			IPrincipal result;
			if (string.IsNullOrEmpty(text))
			{
				if (WindowsAuthenticationModule.IsEnabled)
				{
					result = WindowsAuthenticationModule.AnonymousPrincipal;
				}
				else
				{
					result = null;
				}
			}
			else if (intPtr != IntPtr.Zero)
			{
				IIdentity identity = new WindowsIdentity(intPtr, type, WindowsAccountType.Normal, true);
				result = new WindowsPrincipal((WindowsIdentity)identity);
			}
			else
			{
				IIdentity identity = new GenericIdentity(text, type);
				result = new IIS7UserPrincipal(this, identity);
			}
			return result;
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x001470EC File Offset: 0x001452EC
		internal bool IsUserInRole(string role)
		{
			bool result = false;
			int hresult = UnsafeIISMethods.MgdIsInRole(this._context, role, out result);
			Misc.ThrowIfFailedHr(hresult);
			return result;
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06005E3A RID: 24122 RVA: 0x00147114 File Offset: 0x00145314
		private static bool IsAuthenticationEnabled
		{
			get
			{
				if (!IIS7WorkerRequest.s_AuthenticationChecked)
				{
					bool flag = AuthenticationConfig.Mode > AuthenticationMode.None;
					IIS7WorkerRequest.s_AuthenticationEnabled = flag;
					IIS7WorkerRequest.s_AuthenticationChecked = true;
				}
				return IIS7WorkerRequest.s_AuthenticationEnabled;
			}
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x00147148 File Offset: 0x00145348
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void SynchronizeVariables(HttpContext context)
		{
			if (context.IsChangeInServerVars)
			{
				this.GetServerVarChanges(context);
			}
			if (context.IsChangeInRequestHeaders)
			{
				this.GetHeaderChanges(context, true);
			}
			if (context.IsChangeInResponseHeaders)
			{
				this.GetHeaderChanges(context, false);
			}
			if (context.IsChangeInResponseStatus)
			{
				this.GetStatusChanges(context);
			}
			if (context.IsChangeInUserPrincipal && IIS7WorkerRequest.IsAuthenticationEnabled)
			{
				context.SetPrincipalNoDemand(this.GetUserPrincipal(), false);
			}
			if (context.AreResponseHeadersSent)
			{
				context.Response.HeadersWritten = true;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool SupportsExecuteUrl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x001471C4 File Offset: 0x001453C4
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void ScheduleExecuteUrl(string url, string queryString, string method, bool preserveForm, byte[] entity, NameValueCollection headers, bool preserveUser)
		{
			string[] array = null;
			string[] array2 = null;
			int num = 0;
			if (headers != null && headers.Count > 0)
			{
				num = headers.Count;
				array = new string[num];
				array2 = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = headers.GetKey(i);
					array2[i] = headers.Get(i);
				}
			}
			bool flag = !string.IsNullOrEmpty(queryString);
			if (flag)
			{
				url = url + "?" + queryString;
			}
			int num2 = UnsafeIISMethods.MgdExecuteUrl(this._context, url, flag, preserveForm, entity, (uint)((entity == null) ? 0 : entity.Length), method, num, array, array2, preserveUser);
			if (num2 == -2147024846)
			{
				throw new InvalidOperationException(SR.GetString("TransferRequest_cannot_be_invoked_more_than_once"));
			}
			Misc.ThrowIfFailedHr(num2);
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x00147284 File Offset: 0x00145484
		public unsafe override byte[] GetQueryStringRawBytes()
		{
			int num;
			IntPtr queryStringPtr = this.GetQueryStringPtr(out num);
			if (num == 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			char* ptr = (char*)((void*)queryStringPtr);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)ptr[i];
			}
			return array;
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x001472CD File Offset: 0x001454CD
		public override byte[] GetClientCertificate()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCert;
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x001472E3 File Offset: 0x001454E3
		public override DateTime GetClientCertificateValidFrom()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidFrom;
		}

		// Token: 0x06005E41 RID: 24129 RVA: 0x001472F9 File Offset: 0x001454F9
		public override DateTime GetClientCertificateValidUntil()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidUntil;
		}

		// Token: 0x06005E42 RID: 24130 RVA: 0x0014730F File Offset: 0x0014550F
		public override byte[] GetClientCertificateBinaryIssuer()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertBinaryIssuer;
		}

		// Token: 0x06005E43 RID: 24131 RVA: 0x00147325 File Offset: 0x00145525
		public override int GetClientCertificateEncoding()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertEncoding;
		}

		// Token: 0x06005E44 RID: 24132 RVA: 0x0014733B File Offset: 0x0014553B
		public override byte[] GetClientCertificatePublicKey()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertPublicKey;
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x00147354 File Offset: 0x00145554
		private void FetchClientCertificate()
		{
			if (this._clientCertFetched)
			{
				return;
			}
			this._clientCertFetched = true;
			IntPtr src;
			int num;
			IntPtr src2;
			int num2;
			IntPtr src3;
			int num3;
			uint clientCertEncoding;
			long num4;
			long num5;
			int hresult = UnsafeIISMethods.MgdGetClientCertificate(this._context, out src, out num, out src2, out num2, out src3, out num3, out clientCertEncoding, out num4, out num5);
			Misc.ThrowIfFailedHr(hresult);
			this._clientCertEncoding = (int)clientCertEncoding;
			if (num > 0)
			{
				this._clientCert = new byte[num];
				Misc.CopyMemory(src, 0, this._clientCert, 0, num);
			}
			if (num2 > 0)
			{
				this._clientCertBinaryIssuer = new byte[num2];
				Misc.CopyMemory(src2, 0, this._clientCertBinaryIssuer, 0, num2);
			}
			if (num3 > 0)
			{
				this._clientCertPublicKey = new byte[num3];
				Misc.CopyMemory(src3, 0, this._clientCertPublicKey, 0, num3);
			}
			this._clientCertValidFrom = ((num4 != 0L) ? DateTime.FromFileTime(num4) : DateTime.Now);
			this._clientCertValidUntil = ((num5 != 0L) ? DateTime.FromFileTime(num5) : DateTime.Now);
		}

		// Token: 0x06005E46 RID: 24134 RVA: 0x00147430 File Offset: 0x00145630
		internal ITlsTokenBindingInfo GetTlsTokenBindingInfo()
		{
			TlsTokenBindingHandle tlsTokenBindingHandle = new TlsTokenBindingHandle(this._context);
			if (tlsTokenBindingHandle.IsInvalid)
			{
				return null;
			}
			byte[] providedToken;
			byte[] referredToken;
			using (tlsTokenBindingHandle)
			{
				providedToken = tlsTokenBindingHandle.GetProvidedToken();
				referredToken = tlsTokenBindingHandle.GetReferredToken();
			}
			if (providedToken != null || referredToken != null)
			{
				return new TlsTokenBindingInfo(providedToken, referredToken);
			}
			return null;
		}

		// Token: 0x06005E47 RID: 24135 RVA: 0x00147490 File Offset: 0x00145690
		public override string MapPath(string path)
		{
			return HostingEnvironment.MapPathInternal(path);
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x001277FC File Offset: 0x001259FC
		public override string MachineConfigPath
		{
			get
			{
				return HttpConfigurationSystem.MachineConfigurationFilePath;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x00127803 File Offset: 0x00125A03
		public override string RootWebConfigPath
		{
			get
			{
				return HttpConfigurationSystem.RootWebConfigurationFilePath;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06005E4A RID: 24138 RVA: 0x00147498 File Offset: 0x00145698
		public override string MachineInstallDirectory
		{
			get
			{
				return HttpRuntime.AspInstallDirectory;
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x001474A0 File Offset: 0x001456A0
		internal ChannelBinding HttpChannelBindingToken
		{
			get
			{
				if (this._channelBindingToken == null)
				{
					IntPtr zero = IntPtr.Zero;
					int tokenSize = 0;
					int num = UnsafeIISMethods.MgdGetChannelBindingToken(this._context, out zero, out tokenSize);
					if (num == -2147467263)
					{
						throw new PlatformNotSupportedException();
					}
					Misc.ThrowIfFailedHr(num);
					this._channelBindingToken = new HttpChannelBindingToken(zero, tokenSize);
				}
				return this._channelBindingToken;
			}
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x001474F8 File Offset: 0x001456F8
		internal void InsertEntityBody(byte[] buffer, int offset, int count)
		{
			int hresult = UnsafeIISMethods.MgdInsertEntityBody(this._context, buffer, offset, count);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0014751A File Offset: 0x0014571A
		internal bool IsWebSocketModuleActive()
		{
			return this.GetServerVariableInternal("WEBSOCKET_VERSION") != null;
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x0014752A File Offset: 0x0014572A
		internal bool IsWebSocketRequest()
		{
			return string.Equals(this.GetServerVariableInternal("IIS_WEBSOCK"), "websockets", StringComparison.Ordinal);
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x00147544 File Offset: 0x00145744
		internal void AcceptWebSocket()
		{
			int hresult = UnsafeIISMethods.MgdAcceptWebSocket(this._context);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x00147564 File Offset: 0x00145764
		internal UnmanagedWebSocketContext GetWebSocketContext()
		{
			IntPtr intPtr;
			int num = UnsafeIISMethods.MgdGetWebSocketContext(this._context, out intPtr);
			if (num < 0 || intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new UnmanagedWebSocketContext(intPtr);
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x00147598 File Offset: 0x00145798
		internal bool GetIsChildRequest()
		{
			bool result;
			int hresult = UnsafeIISMethods.MgdGetIsChildContext(this._context, out result);
			Misc.ThrowIfFailedHr(hresult);
			return result;
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x06005E52 RID: 24146 RVA: 0x001475BC File Offset: 0x001457BC
		// (set) Token: 0x06005E53 RID: 24147 RVA: 0x0014762E File Offset: 0x0014582E
		internal IAllocatorProvider AllocatorProvider
		{
			private get
			{
				if (this._allocator == null)
				{
					if (IIS7WorkerRequest.s_DefaultAllocator == null)
					{
						IBufferAllocator allocator = new IntegerArrayAllocator(128, 64);
						IBufferAllocator allocator2 = new IntPtrArrayAllocator(128, 64);
						Interlocked.CompareExchange<IAllocatorProvider>(ref IIS7WorkerRequest.s_DefaultAllocator, new AllocatorProvider
						{
							IntBufferAllocator = new BufferAllocatorWrapper<int>(allocator),
							IntPtrBufferAllocator = new BufferAllocatorWrapper<IntPtr>(allocator2)
						}, null);
					}
					this._allocator = IIS7WorkerRequest.s_DefaultAllocator;
				}
				return this._allocator;
			}
			set
			{
				this._allocator = value;
			}
		}

		// Token: 0x04003136 RID: 12598
		private const int IisHeaderTranslate = 39;

		// Token: 0x04003137 RID: 12599
		private const string IisHeaderTranslateName = "Translate";

		// Token: 0x04003138 RID: 12600
		private const int IisHeaderUserAgent = 40;

		// Token: 0x04003139 RID: 12601
		private const int IisRequestHeaderMaximum = 41;

		// Token: 0x0400313A RID: 12602
		private static readonly Version IIS_VERSION_70 = new Version(7, 0);

		// Token: 0x0400313B RID: 12603
		private static readonly Version IIS_VERSION_75 = new Version(7, 5);

		// Token: 0x0400313C RID: 12604
		private static readonly Version IIS_VERSION_80 = new Version(8, 0);

		// Token: 0x0400313D RID: 12605
		private IntPtr _context;

		// Token: 0x0400313E RID: 12606
		private IntPtr _pCookedUrl;

		// Token: 0x0400313F RID: 12607
		private Encoding _headerEncoding = Encoding.UTF8;

		// Token: 0x04003140 RID: 12608
		private AsyncResultBase _asyncResultBase;

		// Token: 0x04003141 RID: 12609
		private int _contentType;

		// Token: 0x04003142 RID: 12610
		private int _contentTotalLength;

		// Token: 0x04003143 RID: 12611
		private string _appPath;

		// Token: 0x04003144 RID: 12612
		private string _appPathTranslated;

		// Token: 0x04003145 RID: 12613
		private string _path;

		// Token: 0x04003146 RID: 12614
		private string _queryString;

		// Token: 0x04003147 RID: 12615
		private string _filePath;

		// Token: 0x04003148 RID: 12616
		private string _pathInfo;

		// Token: 0x04003149 RID: 12617
		private string _pathTranslated;

		// Token: 0x0400314A RID: 12618
		private string _httpVerb;

		// Token: 0x0400314B RID: 12619
		private bool _rewriteNotifyDisabled;

		// Token: 0x0400314C RID: 12620
		private bool _rebaseClientPath;

		// Token: 0x0400314D RID: 12621
		private bool _requestHeadersAvailable;

		// Token: 0x0400314E RID: 12622
		private string[][] _unknownRequestHeaders;

		// Token: 0x0400314F RID: 12623
		private string[] _knownRequestHeaders;

		// Token: 0x04003150 RID: 12624
		private int _cachedResponseBodyLength;

		// Token: 0x04003151 RID: 12625
		private ArrayList _cachedResponseBodyBytes;

		// Token: 0x04003152 RID: 12626
		private bool _preloadedLengthRead;

		// Token: 0x04003153 RID: 12627
		private int _preloadedLength;

		// Token: 0x04003154 RID: 12628
		private bool _preloadedContentRead;

		// Token: 0x04003155 RID: 12629
		private byte[] _preloadedContent;

		// Token: 0x04003156 RID: 12630
		private string _cacheUrl;

		// Token: 0x04003157 RID: 12631
		private const int CONTENT_NONE = 0;

		// Token: 0x04003158 RID: 12632
		private const int CONTENT_FORM = 1;

		// Token: 0x04003159 RID: 12633
		private const int CONTENT_MULTIPART = 2;

		// Token: 0x0400315A RID: 12634
		private const int CONTENT_OTHER = 3;

		// Token: 0x0400315B RID: 12635
		private const int MIN_ASYNC_SIZE = 2048;

		// Token: 0x0400315C RID: 12636
		private static volatile bool s_AuthenticationChecked;

		// Token: 0x0400315D RID: 12637
		private static bool s_AuthenticationEnabled;

		// Token: 0x0400315E RID: 12638
		private static readonly char[] s_ColonOrNL = new char[]
		{
			':',
			'\n'
		};

		// Token: 0x0400315F RID: 12639
		private Guid _traceId;

		// Token: 0x04003160 RID: 12640
		private bool _traceEnabled;

		// Token: 0x04003161 RID: 12641
		private bool _connectionClosed;

		// Token: 0x04003162 RID: 12642
		private bool _disconnected;

		// Token: 0x04003163 RID: 12643
		private bool _headersSent;

		// Token: 0x04003164 RID: 12644
		private bool _trySkipIisCustomErrors;

		// Token: 0x04003165 RID: 12645
		private bool _clientCertFetched;

		// Token: 0x04003166 RID: 12646
		private DateTime _clientCertValidFrom;

		// Token: 0x04003167 RID: 12647
		private DateTime _clientCertValidUntil;

		// Token: 0x04003168 RID: 12648
		private byte[] _clientCert;

		// Token: 0x04003169 RID: 12649
		private int _clientCertEncoding;

		// Token: 0x0400316A RID: 12650
		private byte[] _clientCertPublicKey;

		// Token: 0x0400316B RID: 12651
		private byte[] _clientCertBinaryIssuer;

		// Token: 0x0400316C RID: 12652
		private ChannelBinding _channelBindingToken;

		// Token: 0x0400316D RID: 12653
		private readonly object _disposeLockObj = new object();

		// Token: 0x0400316E RID: 12654
		private CancellationTokenHelper _clientDisconnectTokenHelper;

		// Token: 0x0400316F RID: 12655
		private static IAllocatorProvider s_DefaultAllocator;

		// Token: 0x04003170 RID: 12656
		private IAllocatorProvider _allocator;
	}
}
