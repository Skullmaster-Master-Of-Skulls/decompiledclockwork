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
using Microsoft.Win32.SafeHandles;

namespace System.Web.Hosting
{
	// Token: 0x02000291 RID: 657
	internal sealed class IIS7WorkerRequest : HttpWorkerRequest
	{
		// Token: 0x060021C9 RID: 8649 RVA: 0x00093BF0 File Offset: 0x00092BF0
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

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x00093C76 File Offset: 0x00092C76
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

		// Token: 0x060021CB RID: 8651 RVA: 0x00093C98 File Offset: 0x00092C98
		internal void ReadRequestBasics()
		{
			IntPtr ip;
			int num;
			int hresult = UnsafeIISMethods.MgdGetRequestBasics(this._context, out this._contentType, out this._contentTotalLength, out ip, out num);
			Misc.ThrowIfFailedHr(hresult);
			this._pathTranslated = ((num <= 0) ? string.Empty : StringUtil.StringFromWCharPtr(ip, num));
			this._path = this.GetUriPathInternal(true, false);
			this._filePath = this.GetUriPathInternal(false, false);
			int num2 = this._path.Length - this._filePath.Length;
			if (num2 > 0)
			{
				this._pathInfo = this._path.Substring(this._filePath.Length);
				int num3 = this._pathTranslated.Length - num2;
				if (num3 > 0)
				{
					this._pathTranslated = this._pathTranslated.Substring(0, num3);
				}
			}
			else
			{
				this._filePath = this._path;
				this._pathInfo = string.Empty;
			}
			this._queryString = this.GetQueryString();
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x00093D80 File Offset: 0x00092D80
		internal static IIS7WorkerRequest CreateWorkerRequest(IntPtr requestContext, bool etwProviderEnabled)
		{
			IIS7WorkerRequest iis7WorkerRequest = new IIS7WorkerRequest(requestContext, etwProviderEnabled);
			if (iis7WorkerRequest != null)
			{
				iis7WorkerRequest.Initialize();
			}
			return iis7WorkerRequest;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x00093DA0 File Offset: 0x00092DA0
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

		// Token: 0x060021CE RID: 8654 RVA: 0x00093E2C File Offset: 0x00092E2C
		internal void Initialize()
		{
			this.ReadRequestBasics();
			this.InitAppVars();
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00093E3A File Offset: 0x00092E3A
		internal void Dispose()
		{
			this._context = IntPtr.Zero;
			if (this._channelBindingToken != null && !this._channelBindingToken.IsInvalid)
			{
				this._channelBindingToken.Dispose();
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x00093E68 File Offset: 0x00092E68
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

		// Token: 0x060021D1 RID: 8657 RVA: 0x00093EC8 File Offset: 0x00092EC8
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

		// Token: 0x060021D2 RID: 8658 RVA: 0x00093F20 File Offset: 0x00092F20
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

		// Token: 0x060021D3 RID: 8659 RVA: 0x00093F66 File Offset: 0x00092F66
		public override string GetUriPath()
		{
			return this._path;
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00093F70 File Offset: 0x00092F70
		public override string GetQueryString()
		{
			IntPtr intPtr;
			int length;
			int hresult = UnsafeIISMethods.MgdGetQueryString(this._context, out intPtr, out length);
			Misc.ThrowIfFailedHr(hresult);
			if (intPtr == IntPtr.Zero)
			{
				return string.Empty;
			}
			return StringUtil.StringFromWCharPtr(intPtr, length);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00093FB0 File Offset: 0x00092FB0
		public override string GetRawUrl()
		{
			if (this._rawUrl == null)
			{
				this._rawUrl = this.GetRequestUri();
				if (this._rawUrl != null)
				{
					this._isRewriteModuleEnabled = true;
					return this._rawUrl;
				}
				if (!string.IsNullOrEmpty(this._queryString))
				{
					this._rawUrl = this._path + "?" + this._queryString;
				}
				else
				{
					this._rawUrl = this._path;
				}
			}
			return this._rawUrl;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00094024 File Offset: 0x00093024
		internal override void SetRawUrl(string path)
		{
			this._rawUrl = path;
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0009402D File Offset: 0x0009302D
		internal override bool IsRewriteModuleEnabled
		{
			get
			{
				if (this._rawUrl == null)
				{
					this.GetRawUrl();
				}
				return this._isRewriteModuleEnabled;
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00094044 File Offset: 0x00093044
		private string GetRequestUri()
		{
			if (this.GetServerVariable("IIS_WasUrlRewritten") != "1")
			{
				return null;
			}
			string serverVariable = this.GetServerVariable("CACHE_URL");
			if (serverVariable == null)
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < serverVariable.Length; i++)
			{
				if (serverVariable[i] == '/' && ++num == 3)
				{
					return serverVariable.Substring(i);
				}
			}
			return null;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000940AA File Offset: 0x000930AA
		public override string GetHttpVerbName()
		{
			return this.GetMethodInternal();
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000940B2 File Offset: 0x000930B2
		public override string GetHttpVersion()
		{
			return this.GetServerVariable("SERVER_PROTOCOL");
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000940BF File Offset: 0x000930BF
		public override string GetRemoteAddress()
		{
			return this.GetServerVariable("REMOTE_ADDR");
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000940CC File Offset: 0x000930CC
		public override string GetRemoteName()
		{
			return this.GetServerVariable("REMOTE_HOST");
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000940D9 File Offset: 0x000930D9
		public override int GetRemotePort()
		{
			return UnsafeIISMethods.MgdGetRemotePort(this._context);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000940E6 File Offset: 0x000930E6
		public override string GetLocalAddress()
		{
			return this.GetServerVariable("LOCAL_ADDR");
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000940F3 File Offset: 0x000930F3
		public override int GetLocalPort()
		{
			return UnsafeIISMethods.MgdGetLocalPort(this._context);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00094100 File Offset: 0x00093100
		public override string GetServerName()
		{
			return this.GetServerVariable("SERVER_NAME");
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0009410D File Offset: 0x0009310D
		internal override string GetLocalPortAsString()
		{
			return this.GetServerVariable("SERVER_PORT");
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0009411C File Offset: 0x0009311C
		public override bool IsSecure()
		{
			string serverVariable = this.GetServerVariable("HTTPS");
			return serverVariable != null && serverVariable.Equals("on");
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x00094145 File Offset: 0x00093145
		public override string GetFilePath()
		{
			return this._filePath;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0009414D File Offset: 0x0009314D
		public override string GetFilePathTranslated()
		{
			return this._pathTranslated;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00094155 File Offset: 0x00093155
		public override string GetPathInfo()
		{
			return this._pathInfo;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0009415D File Offset: 0x0009315D
		public override string GetAppPath()
		{
			return this._appPath;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00094165 File Offset: 0x00093165
		public override string GetAppPathTranslated()
		{
			return this._appPathTranslated;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x00094170 File Offset: 0x00093170
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

		// Token: 0x060021E9 RID: 8681 RVA: 0x000941B0 File Offset: 0x000931B0
		public override int GetPreloadedEntityBody(byte[] buffer, int offset)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (this.GetPreloadedEntityBodyLength() == 0)
			{
				return 0;
			}
			int length = buffer.Length - offset;
			return this.GetPreloadedContentInternal(buffer, offset, length);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x000941F8 File Offset: 0x000931F8
		public override byte[] GetPreloadedEntityBody()
		{
			byte[] array = null;
			int preloadedEntityBodyLength = this.GetPreloadedEntityBodyLength();
			if (preloadedEntityBodyLength > 0)
			{
				array = new byte[preloadedEntityBodyLength];
				this.GetPreloadedContentInternal(array, 0, preloadedEntityBodyLength);
			}
			return array;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00094224 File Offset: 0x00093224
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return this.GetTotalEntityBodyLength() == this.GetPreloadedEntityBodyLength();
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00094234 File Offset: 0x00093234
		public override int GetTotalEntityBodyLength()
		{
			return this._contentTotalLength;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0009423C File Offset: 0x0009323C
		private int ReadEntityCoreSync(byte[] buffer, int offset, int size, long timeout)
		{
			int num = 0;
			int num2 = 0;
			uint timeout2 = 0U;
			if (timeout <= 0L)
			{
				timeout = 1L;
			}
			if (timeout <= (long)((ulong)-1))
			{
				timeout2 = (uint)timeout;
			}
			try
			{
				base.IsInReadEntitySync = true;
				num2 = UnsafeIISMethods.MgdSyncReadRequest(this._context, buffer, offset, size, out num, timeout2);
				if (num2 == -2147023436)
				{
					throw new HttpException(SR.GetString("Request_timed_out"));
				}
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

		// Token: 0x060021EE RID: 8686 RVA: 0x000942C8 File Offset: 0x000932C8
		internal int ReadEntityBodyWithTimeout(byte[] buffer, int size, long timeout)
		{
			if (size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return this.ReadEntityCoreSync(buffer, 0, size, timeout);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000942E5 File Offset: 0x000932E5
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			return this.ReadEntityBodyWithTimeout(buffer, size, long.MaxValue);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000942F8 File Offset: 0x000932F8
		public override int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			if (offset < 0 || buffer.Length - offset < size)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			return this.ReadEntityCoreSync(buffer, offset, size, long.MaxValue);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00094323 File Offset: 0x00093323
		public override long GetBytesRead()
		{
			throw new HttpException(SR.GetString("Not_supported"));
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00094334 File Offset: 0x00093334
		public override string GetKnownRequestHeader(int index)
		{
			if (!this._requestHeadersAvailable)
			{
				switch (index)
				{
				case 11:
					if (this._contentType != 0)
					{
						return this._contentTotalLength.ToString(CultureInfo.InvariantCulture);
					}
					break;
				case 12:
					if (this._contentType == 1)
					{
						return "application/x-www-form-urlencoded";
					}
					break;
				default:
					if (index == 25)
					{
						return this.GetCookieHeaderInternal();
					}
					if (index == 39)
					{
						return this.GetUserAgentInternal();
					}
					break;
				}
				this.ReadRequestHeaders();
			}
			return this._knownRequestHeaders[index];
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000943AC File Offset: 0x000933AC
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

		// Token: 0x060021F4 RID: 8692 RVA: 0x000943FA File Offset: 0x000933FA
		public override string[][] GetUnknownRequestHeaders()
		{
			if (!this._requestHeadersAvailable)
			{
				this.ReadRequestHeaders();
			}
			return this._unknownRequestHeaders;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00094410 File Offset: 0x00093410
		public override string GetServerVariable(string name)
		{
			if (StringUtil.StringStartsWith(name, "HTTP_"))
			{
				return this.GetServerVariableInternalAnsi(name);
			}
			return this.GetServerVariableInternal(name);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00094430 File Offset: 0x00093430
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

		// Token: 0x060021F7 RID: 8695 RVA: 0x0009446A File Offset: 0x0009346A
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this.SendStatus(statusCode, 0, statusDescription);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00094475 File Offset: 0x00093475
		internal override void SetHeaderEncoding(Encoding encoding)
		{
			this._headerEncoding = encoding;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0009447E File Offset: 0x0009347E
		public override void SendKnownResponseHeader(int index, string value)
		{
			if (index < 0 || index >= 30)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.SetKnownResponseHeader(index, value, false);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0009449D File Offset: 0x0009349D
		public override void SendUnknownResponseHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.SetUnknownResponseHeader(name, value, false);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000944B6 File Offset: 0x000934B6
		public override void SendCalculatedContentLength(int contentLength)
		{
			this.SendKnownResponseHeader(11, contentLength.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000944CC File Offset: 0x000934CC
		public override bool HeadersSent()
		{
			return this._headersSent;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000944D4 File Offset: 0x000934D4
		public override bool IsClientConnected()
		{
			return !this._connectionClosed && UnsafeIISMethods.MgdIsClientConnected(this._context);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000944EB File Offset: 0x000934EB
		internal bool IsHandlerExecutionDenied()
		{
			return UnsafeIISMethods.MgdIsHandlerExecutionDenied(this._context);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000944F8 File Offset: 0x000934F8
		internal void AbortConnection()
		{
			IntPtr zero = IntPtr.Zero;
			Interlocked.Exchange(ref zero, this._context);
			if (zero != IntPtr.Zero)
			{
				UnsafeIISMethods.MgdAbortConnection(zero);
				this._connectionClosed = true;
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00094533 File Offset: 0x00093533
		public override void CloseConnection()
		{
			UnsafeIISMethods.MgdCloseConnection(this._context);
			this._connectionClosed = true;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00094548 File Offset: 0x00093548
		public override IntPtr GetUserToken()
		{
			IntPtr zero = IntPtr.Zero;
			int hresult = UnsafeIISMethods.MgdGetUserToken(this._context, out zero);
			Misc.ThrowIfFailedHr(hresult);
			return zero;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00094570 File Offset: 0x00093570
		public override IntPtr GetVirtualPathToken()
		{
			IntPtr zero = IntPtr.Zero;
			int hresult = UnsafeIISMethods.MgdGetVirtualToken(this._context, out zero);
			Misc.ThrowIfFailedHr(hresult);
			return zero;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00094598 File Offset: 0x00093598
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

		// Token: 0x06002204 RID: 8708 RVA: 0x000945B4 File Offset: 0x000935B4
		public override void SendResponseFromMemory(IntPtr data, int length)
		{
			if (this._connectionClosed)
			{
				return;
			}
			this.SendResponseFromMemory(data, length, false);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000945C8 File Offset: 0x000935C8
		internal override void SendResponseFromMemory(IntPtr data, int length, bool isBufferFromUnmanagedPool)
		{
			if (length > 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length, isBufferFromUnmanagedPool ? BufferType.UnmanagedPool : BufferType.Managed));
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000945E2 File Offset: 0x000935E2
		internal void SendResponseFromIISAllocatedRequestMemory(IntPtr data, int length)
		{
			if (data != IntPtr.Zero && length >= 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length, BufferType.IISAllocatedRequestMemory));
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00094603 File Offset: 0x00093603
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

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00094621 File Offset: 0x00093621
		internal override bool SupportsLongTransmitFile
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00094624 File Offset: 0x00093624
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

		// Token: 0x0600220A RID: 8714 RVA: 0x00094694 File Offset: 0x00093694
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

		// Token: 0x0600220B RID: 8715 RVA: 0x000946E4 File Offset: 0x000936E4
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

		// Token: 0x0600220C RID: 8716 RVA: 0x00094738 File Offset: 0x00093738
		public override void FlushResponse(bool finalFlush)
		{
			if (this._connectionClosed)
			{
				return;
			}
			this.FlushCachedResponse(finalFlush);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0009474A File Offset: 0x0009374A
		public override void EndOfRequest()
		{
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x0009474C File Offset: 0x0009374C
		public override Guid RequestTraceIdentifier
		{
			get
			{
				return this._traceId;
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00094754 File Offset: 0x00093754
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

		// Token: 0x06002210 RID: 8720 RVA: 0x00094790 File Offset: 0x00093790
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

		// Token: 0x06002211 RID: 8721 RVA: 0x000947CC File Offset: 0x000937CC
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

		// Token: 0x06002212 RID: 8722 RVA: 0x00094800 File Offset: 0x00093800
		private string GetMethodInternal()
		{
			string empty = string.Empty;
			IntPtr ip;
			int length;
			int hresult = UnsafeIISMethods.MgdGetMethod(this._context, out ip, out length);
			Misc.ThrowIfFailedHr(hresult);
			return StringUtil.StringFromCharPtr(ip, length);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00094834 File Offset: 0x00093834
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

		// Token: 0x06002214 RID: 8724 RVA: 0x00094870 File Offset: 0x00093870
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

		// Token: 0x06002215 RID: 8725 RVA: 0x000948AC File Offset: 0x000938AC
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

		// Token: 0x06002216 RID: 8726 RVA: 0x00094A49 File Offset: 0x00093A49
		private void AddBodyToCachedResponse(MemoryBytes bytes)
		{
			if (this._cachedResponseBodyBytes == null)
			{
				this._cachedResponseBodyBytes = new ArrayList();
			}
			this._cachedResponseBodyBytes.Add(bytes);
			this._cachedResponseBodyLength += bytes.Size;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00094A80 File Offset: 0x00093A80
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
					array = RecyclableArrayHelper.GetIntPtrArray(num);
					array2 = RecyclableArrayHelper.GetIntegerArray(num);
					array3 = RecyclableArrayHelper.GetIntegerArray(num);
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
				RecyclableArrayHelper.ReuseIntPtrArray(array);
				RecyclableArrayHelper.ReuseIntegerArray(array2);
				RecyclableArrayHelper.ReuseIntegerArray(array3);
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00094B9C File Offset: 0x00093B9C
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

		// Token: 0x06002219 RID: 8729 RVA: 0x00094BE4 File Offset: 0x00093BE4
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

		// Token: 0x0600221A RID: 8730 RVA: 0x00094C44 File Offset: 0x00093C44
		private void ResetCachedResponse()
		{
			this._cachedResponseBodyLength = 0;
			this._cachedResponseBodyBytes = null;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00094C54 File Offset: 0x00093C54
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

		// Token: 0x0600221C RID: 8732 RVA: 0x00094CAC File Offset: 0x00093CAC
		internal override string SetupKernelCaching(int secondsToLive, string originalCacheUrl, bool enableKernelCacheForVaryByStar)
		{
			string serverVariable = this.GetServerVariable("CACHE_URL");
			if (originalCacheUrl != null && originalCacheUrl != serverVariable)
			{
				return null;
			}
			if (string.IsNullOrEmpty(serverVariable) || (!enableKernelCacheForVaryByStar && serverVariable.IndexOf('?') != -1))
			{
				return null;
			}
			int num = UnsafeIISMethods.MgdSetKernelCachePolicy(this._context, secondsToLive);
			if (num < 0)
			{
				return null;
			}
			return serverVariable;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00094CFF File Offset: 0x00093CFF
		internal override void DisableKernelCache()
		{
			UnsafeIISMethods.MgdDisableKernelCache(this._context);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00094D0C File Offset: 0x00093D0C
		internal void DisableIISCache()
		{
			UnsafeIISMethods.MgdDisableKernelCache(this._context);
			UnsafeIISMethods.MgdDisableUserCache(this._context);
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00094D24 File Offset: 0x00093D24
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x00094D2C File Offset: 0x00093D2C
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

		// Token: 0x06002221 RID: 8737 RVA: 0x00094D38 File Offset: 0x00093D38
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

		// Token: 0x06002222 RID: 8738 RVA: 0x00094DCC File Offset: 0x00093DCC
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

		// Token: 0x06002223 RID: 8739 RVA: 0x00094E04 File Offset: 0x00093E04
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

		// Token: 0x06002224 RID: 8740 RVA: 0x00094E38 File Offset: 0x00093E38
		internal void SetRemapHandler(string handlerType, string handlerName)
		{
			int hresult = UnsafeIISMethods.MgdSetRemapHandler(this._context, handlerName, handlerType);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00094E5C File Offset: 0x00093E5C
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
				UnsafeIISMethods.MgdRewriteUrl(this._context, pszUrl, null != newQueryString);
				this._rebaseClientPath = rebaseClientPath;
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00094EB1 File Offset: 0x00093EB1
		internal void DisableNotifications(RequestNotification notifications, RequestNotification postNotifications)
		{
			UnsafeIISMethods.MgdDisableNotifications(this._context, notifications, postNotifications);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00094EC0 File Offset: 0x00093EC0
		internal void PushResponseToNative()
		{
			this.FlushCachedResponse(false);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00094EC9 File Offset: 0x00093EC9
		internal void ClearResponse(bool clearEntity, bool clearHeaders)
		{
			UnsafeIISMethods.MgdClearResponse(this._context, clearEntity, clearHeaders);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00094EDC File Offset: 0x00093EDC
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

		// Token: 0x0600222A RID: 8746 RVA: 0x00094F34 File Offset: 0x00093F34
		internal IntPtr AllocateRequestMemory(int size)
		{
			if (size > 0)
			{
				return UnsafeIISMethods.MgdAllocateRequestMemory(this._context, size);
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00094F4C File Offset: 0x00093F4C
		internal ArrayList GetBufferedResponseChunks(bool disableRecycling, ArrayList substElements, ref bool hasSubstBlocks)
		{
			int num = 32;
			IntPtr[] intPtrArray = RecyclableArrayHelper.GetIntPtrArray(num);
			int[] integerArray = RecyclableArrayHelper.GetIntegerArray(num);
			int[] integerArray2 = RecyclableArrayHelper.GetIntegerArray(num);
			int num2 = UnsafeIISMethods.MgdGetResponseChunks(this._context, ref num, intPtrArray, integerArray, integerArray2);
			if (num2 < 0)
			{
				if (num2 == -2147024774)
				{
					RecyclableArrayHelper.ReuseIntPtrArray(intPtrArray);
					RecyclableArrayHelper.ReuseIntegerArray(integerArray);
					RecyclableArrayHelper.ReuseIntegerArray(integerArray2);
					intPtrArray = RecyclableArrayHelper.GetIntPtrArray(num);
					integerArray = RecyclableArrayHelper.GetIntegerArray(num);
					integerArray2 = RecyclableArrayHelper.GetIntegerArray(num);
					num2 = UnsafeIISMethods.MgdGetResponseChunks(this._context, ref num, intPtrArray, integerArray, integerArray2);
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
				if (integerArray2[i] == 0)
				{
					if (array != null)
					{
						int num3 = -1;
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j].PointerEquals(intPtrArray[i]))
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
							goto IL_2AD;
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
					int num4 = integerArray[i];
					if (num4 <= httpResponseUnmanagedBufferElement.FreeBytes)
					{
						httpResponseUnmanagedBufferElement.Append(intPtrArray[i], 0, num4);
					}
					else
					{
						int num5 = 0;
						do
						{
							int num6 = httpResponseUnmanagedBufferElement.Append(intPtrArray[i], num5, num4);
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
				else if (integerArray2[i] == 1)
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
						num2 = UnsafeIISMethods.MgdReadChunkHandle(this._context, intPtrArray[i], num7, ref num9, httpResponseUnmanagedBufferElement.FreeLocation);
						Misc.ThrowIfFailedHr(num2);
						httpResponseUnmanagedBufferElement.AdjustSize(num9);
						num8 -= (long)num9;
						num7 += (long)num9;
					}
				}
				IL_2AD:;
			}
			if (httpResponseUnmanagedBufferElement != null)
			{
				arrayList.Add(httpResponseUnmanagedBufferElement);
			}
			RecyclableArrayHelper.ReuseIntPtrArray(intPtrArray);
			RecyclableArrayHelper.ReuseIntegerArray(integerArray);
			RecyclableArrayHelper.ReuseIntegerArray(integerArray2);
			return arrayList;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00095238 File Offset: 0x00094238
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void SetPrincipal(IPrincipal user, IntPtr pManagedPrincipal)
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
			int hresult = UnsafeIISMethods.MgdSetRequestPrincipal(this._context, pManagedPrincipal, text, text2, token);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000952D7 File Offset: 0x000942D7
		internal void ResponseFilterInstalled()
		{
			UnsafeIISMethods.MgdSetResponseFilter(this._context);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000952E4 File Offset: 0x000942E4
		private void RaiseCommunicationError(int result, bool throwOnDisconnect)
		{
			if (UnsafeIISMethods.MgdIsClientConnected(this._context))
			{
				throw new HttpException(SR.GetString("Server_Support_Function_Error", new object[]
				{
					result.ToString("X8", CultureInfo.InvariantCulture)
				}), Marshal.GetExceptionForHR(result));
			}
			if (!this._disconnected)
			{
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_DISCONNECTED);
				this._disconnected = true;
			}
			if (throwOnDisconnect)
			{
				throw new HttpException(SR.GetString("Server_Support_Function_Error_Disconnect", new object[]
				{
					result.ToString("X8", CultureInfo.InvariantCulture)
				}), result);
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00095375 File Offset: 0x00094375
		private static bool IsValidUsername(string username)
		{
			return AppSettings.AllowRelaxedHttpUserName || username.IndexOf('\0') == -1;
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0009538C File Offset: 0x0009438C
		internal void ExplicitFlush()
		{
			int num = UnsafeIISMethods.MgdExplicitFlush(this._context);
			if (num < 0)
			{
				this.RaiseCommunicationError(num, true);
			}
			this._headersSent = true;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000953B8 File Offset: 0x000943B8
		internal void SetServerVariable(string name, string value)
		{
			int hresult = UnsafeIISMethods.MgdSetServerVariableW(this._context, name, value);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000953DC File Offset: 0x000943DC
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

		// Token: 0x06002233 RID: 8755 RVA: 0x00095408 File Offset: 0x00094408
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		private void SetKnownRequestHeader(int index, string value, bool replace)
		{
			if (index == 39)
			{
				index = 40;
			}
			byte[] array = (value != null) ? this._headerEncoding.GetBytes(value) : null;
			int num = (array != null) ? array.Length : 0;
			int hresult = UnsafeIISMethods.MgdSetKnownHeader(this._context, true, replace, (ushort)index, array, (ushort)num);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x00095454 File Offset: 0x00094454
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		private void SetUnknownRequestHeader(string name, string value, bool replace)
		{
			byte[] array = (value != null) ? this._headerEncoding.GetBytes(value) : null;
			int num = (array != null) ? array.Length : 0;
			int byteCount = this._headerEncoding.GetByteCount(name);
			byte[] array2 = new byte[byteCount + 1];
			this._headerEncoding.GetBytes(name, 0, name.Length, array2, 0);
			array2[byteCount] = 0;
			int hresult = UnsafeIISMethods.MgdSetUnknownHeader(this._context, true, replace, array2, array, (ushort)num);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000954C8 File Offset: 0x000944C8
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

		// Token: 0x06002236 RID: 8758 RVA: 0x000954F4 File Offset: 0x000944F4
		private void SetKnownResponseHeader(int index, string value, bool replace)
		{
			if (index == 29 || index == 27 || index == 26)
			{
				this.SetUnknownResponseHeader(HttpWorkerRequest.GetKnownResponseHeaderName(index), value, replace);
				return;
			}
			byte[] array = (value != null) ? this._headerEncoding.GetBytes(value) : null;
			int num = (array != null) ? array.Length : 0;
			int hresult = UnsafeIISMethods.MgdSetKnownHeader(this._context, false, replace, (ushort)index, array, (ushort)num);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00095558 File Offset: 0x00094558
		private void SetUnknownResponseHeader(string name, string value, bool replace)
		{
			if (StringUtil.EqualsIgnoreCase(name, "Set-Cookie"))
			{
				this.DisableIISCache();
			}
			byte[] array = (value != null) ? this._headerEncoding.GetBytes(value) : null;
			int num = (array != null) ? array.Length : 0;
			int byteCount = this._headerEncoding.GetByteCount(name);
			byte[] array2 = new byte[byteCount + 1];
			this._headerEncoding.GetBytes(name, 0, name.Length, array2, 0);
			array2[byteCount] = 0;
			int hresult = UnsafeIISMethods.MgdSetUnknownHeader(this._context, false, replace, array2, array, (ushort)num);
			Misc.ThrowIfFailedHr(hresult);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000955E0 File Offset: 0x000945E0
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

		// Token: 0x06002239 RID: 8761 RVA: 0x000956B0 File Offset: 0x000946B0
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
			for (int i = 0; i < num + 1; i++)
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
					}
					else if (num4 == 39)
					{
						name = "Translate";
					}
					else
					{
						name = HttpWorkerRequest.GetKnownRequestHeaderName(39);
					}
				}
				else
				{
					if (num4 >= 30)
					{
						throw new NotSupportedException();
					}
					name = HttpWorkerRequest.GetKnownResponseHeaderName(num4);
					knownHeaderIndex = num4;
				}
				IntPtr intPtr6 = ptr2[(IntPtr)num4 * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
				string value = null;
				if (intPtr6 != IntPtr.Zero)
				{
					value = StringUtil.StringFromCharPtr(intPtr6, UnsafeNativeMethods.lstrlenA(intPtr6));
				}
				if (forRequest)
				{
					ctx.Request.SynchronizeHeader(name, value);
				}
				else
				{
					ctx.Response.SynchronizeHeader(knownHeaderIndex, name, value);
				}
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

		// Token: 0x0600223A RID: 8762 RVA: 0x000958BC File Offset: 0x000948BC
		private IPrincipal GetUserPrincipal()
		{
			int num = 0;
			int num2 = 0;
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			int hresult = UnsafeIISMethods.MgdGetPrincipal(this._context, out intPtr, out intPtr2, ref num, out intPtr3, ref num2);
			Misc.ThrowIfFailedHr(hresult);
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
				result = WindowsAuthenticationModule.AnonymousPrincipal;
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

		// Token: 0x0600223B RID: 8763 RVA: 0x00095988 File Offset: 0x00094988
		internal bool IsUserInRole(string role)
		{
			bool result = false;
			int hresult = UnsafeIISMethods.MgdIsInRole(this._context, role, out result);
			Misc.ThrowIfFailedHr(hresult);
			return result;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000959B0 File Offset: 0x000949B0
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
			if (context.IsChangeInUserPrincipal && WindowsAuthenticationModule.IsEnabled)
			{
				context.SetPrincipalNoDemand(this.GetUserPrincipal(), false);
			}
			if (context.AreResponseHeadersSent)
			{
				context.Response.HeadersWritten = true;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x00095A2B File Offset: 0x00094A2B
		internal override bool SupportsExecuteUrl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00095A30 File Offset: 0x00094A30
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		internal void ScheduleExecuteUrl(string url, string queryString, string method, bool preserveForm, byte[] entity, NameValueCollection headers)
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
			int num2 = UnsafeIISMethods.MgdExecuteUrl(this._context, url, flag, preserveForm, entity, (uint)((entity == null) ? 0 : entity.Length), method, num, array, array2);
			if (num2 == -2147024846)
			{
				throw new InvalidOperationException(SR.GetString("TransferRequest_cannot_be_invoked_more_than_once"));
			}
			Misc.ThrowIfFailedHr(num2);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00095AEC File Offset: 0x00094AEC
		public unsafe override byte[] GetQueryStringRawBytes()
		{
			IntPtr value;
			int num;
			int hresult = UnsafeIISMethods.MgdGetQueryString(this._context, out value, out num);
			Misc.ThrowIfFailedHr(hresult);
			if (num == 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			char* ptr = (char*)((void*)value);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)ptr[i];
			}
			return array;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00095B44 File Offset: 0x00094B44
		public override byte[] GetClientCertificate()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCert;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00095B5A File Offset: 0x00094B5A
		public override DateTime GetClientCertificateValidFrom()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidFrom;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00095B70 File Offset: 0x00094B70
		public override DateTime GetClientCertificateValidUntil()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidUntil;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x00095B86 File Offset: 0x00094B86
		public override byte[] GetClientCertificateBinaryIssuer()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertBinaryIssuer;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00095B9C File Offset: 0x00094B9C
		public override int GetClientCertificateEncoding()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertEncoding;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00095BB2 File Offset: 0x00094BB2
		public override byte[] GetClientCertificatePublicKey()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertPublicKey;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00095BC8 File Offset: 0x00094BC8
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

		// Token: 0x06002247 RID: 8775 RVA: 0x00095CA8 File Offset: 0x00094CA8
		public override string MapPath(string path)
		{
			return HostingEnvironment.MapPathInternal(path);
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00095CB0 File Offset: 0x00094CB0
		public override string MachineConfigPath
		{
			get
			{
				return HttpConfigurationSystem.MachineConfigurationFilePath;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00095CB7 File Offset: 0x00094CB7
		public override string RootWebConfigPath
		{
			get
			{
				return HttpConfigurationSystem.RootWebConfigurationFilePath;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00095CBE File Offset: 0x00094CBE
		public override string MachineInstallDirectory
		{
			get
			{
				return HttpRuntime.AspInstallDirectory;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x00095CC8 File Offset: 0x00094CC8
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

		// Token: 0x04001B32 RID: 6962
		private const int IisHeaderTranslate = 39;

		// Token: 0x04001B33 RID: 6963
		private const string IisHeaderTranslateName = "Translate";

		// Token: 0x04001B34 RID: 6964
		private const int IisHeaderUserAgent = 40;

		// Token: 0x04001B35 RID: 6965
		private const int IisRequestHeaderMaximum = 41;

		// Token: 0x04001B36 RID: 6966
		private const int CONTENT_NONE = 0;

		// Token: 0x04001B37 RID: 6967
		private const int CONTENT_FORM = 1;

		// Token: 0x04001B38 RID: 6968
		private const int CONTENT_MULTIPART = 2;

		// Token: 0x04001B39 RID: 6969
		private const int CONTENT_OTHER = 3;

		// Token: 0x04001B3A RID: 6970
		private const int MIN_ASYNC_SIZE = 2048;

		// Token: 0x04001B3B RID: 6971
		private IntPtr _context;

		// Token: 0x04001B3C RID: 6972
		private Encoding _headerEncoding = Encoding.UTF8;

		// Token: 0x04001B3D RID: 6973
		private int _contentType;

		// Token: 0x04001B3E RID: 6974
		private int _contentTotalLength;

		// Token: 0x04001B3F RID: 6975
		private string _appPath;

		// Token: 0x04001B40 RID: 6976
		private string _appPathTranslated;

		// Token: 0x04001B41 RID: 6977
		private string _path;

		// Token: 0x04001B42 RID: 6978
		private string _queryString;

		// Token: 0x04001B43 RID: 6979
		private string _filePath;

		// Token: 0x04001B44 RID: 6980
		private string _pathInfo;

		// Token: 0x04001B45 RID: 6981
		private string _pathTranslated;

		// Token: 0x04001B46 RID: 6982
		private string _rawUrl;

		// Token: 0x04001B47 RID: 6983
		private bool _isRewriteModuleEnabled;

		// Token: 0x04001B48 RID: 6984
		private bool _rewriteNotifyDisabled;

		// Token: 0x04001B49 RID: 6985
		private bool _rebaseClientPath;

		// Token: 0x04001B4A RID: 6986
		private bool _requestHeadersAvailable;

		// Token: 0x04001B4B RID: 6987
		private string[][] _unknownRequestHeaders;

		// Token: 0x04001B4C RID: 6988
		private string[] _knownRequestHeaders;

		// Token: 0x04001B4D RID: 6989
		private int _cachedResponseBodyLength;

		// Token: 0x04001B4E RID: 6990
		private ArrayList _cachedResponseBodyBytes;

		// Token: 0x04001B4F RID: 6991
		private bool _preloadedLengthRead;

		// Token: 0x04001B50 RID: 6992
		private int _preloadedLength;

		// Token: 0x04001B51 RID: 6993
		private static readonly char[] s_ColonOrNL = new char[]
		{
			':',
			'\n'
		};

		// Token: 0x04001B52 RID: 6994
		private Guid _traceId;

		// Token: 0x04001B53 RID: 6995
		private bool _traceEnabled;

		// Token: 0x04001B54 RID: 6996
		private bool _connectionClosed;

		// Token: 0x04001B55 RID: 6997
		private bool _disconnected;

		// Token: 0x04001B56 RID: 6998
		private bool _headersSent;

		// Token: 0x04001B57 RID: 6999
		private bool _trySkipIisCustomErrors;

		// Token: 0x04001B58 RID: 7000
		private bool _clientCertFetched;

		// Token: 0x04001B59 RID: 7001
		private DateTime _clientCertValidFrom;

		// Token: 0x04001B5A RID: 7002
		private DateTime _clientCertValidUntil;

		// Token: 0x04001B5B RID: 7003
		private byte[] _clientCert;

		// Token: 0x04001B5C RID: 7004
		private int _clientCertEncoding;

		// Token: 0x04001B5D RID: 7005
		private byte[] _clientCertPublicKey;

		// Token: 0x04001B5E RID: 7006
		private byte[] _clientCertBinaryIssuer;

		// Token: 0x04001B5F RID: 7007
		private ChannelBinding _channelBindingToken;
	}
}
