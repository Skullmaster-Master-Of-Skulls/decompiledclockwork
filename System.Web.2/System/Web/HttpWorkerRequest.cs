using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Management;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000110 RID: 272
	[ComVisible(false)]
	public abstract class HttpWorkerRequest
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x0002E4C2 File Offset: 0x0002C6C2
		protected HttpWorkerRequest()
		{
			this._startTime = DateTime.UtcNow;
		}

		// Token: 0x060010AF RID: 4271
		public abstract string GetUriPath();

		// Token: 0x060010B0 RID: 4272
		public abstract string GetQueryString();

		// Token: 0x060010B1 RID: 4273
		public abstract string GetRawUrl();

		// Token: 0x060010B2 RID: 4274
		public abstract string GetHttpVerbName();

		// Token: 0x060010B3 RID: 4275
		public abstract string GetHttpVersion();

		// Token: 0x060010B4 RID: 4276
		public abstract string GetRemoteAddress();

		// Token: 0x060010B5 RID: 4277
		public abstract int GetRemotePort();

		// Token: 0x060010B6 RID: 4278
		public abstract string GetLocalAddress();

		// Token: 0x060010B7 RID: 4279
		public abstract int GetLocalPort();

		// Token: 0x060010B8 RID: 4280 RVA: 0x0002E4D8 File Offset: 0x0002C6D8
		internal virtual string GetLocalPortAsString()
		{
			return this.GetLocalPort().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		internal bool IsLocal()
		{
			string remoteAddress = this.GetRemoteAddress();
			return !string.IsNullOrEmpty(remoteAddress) && (remoteAddress == "127.0.0.1" || remoteAddress == "::1" || remoteAddress == this.GetLocalAddress());
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0002E544 File Offset: 0x0002C744
		internal static string GetRawUrlHelper(string cacheUrl)
		{
			if (cacheUrl != null)
			{
				int num = 0;
				for (int i = 0; i < cacheUrl.Length; i++)
				{
					if (cacheUrl[i] == '/' && ++num == 3)
					{
						return cacheUrl.Substring(i);
					}
				}
			}
			throw new HttpException(SR.GetString("Cache_url_invalid"));
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0002E591 File Offset: 0x0002C791
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0002E59B File Offset: 0x0002C79B
		internal bool IsInReadEntitySync
		{
			get
			{
				return this._isInReadEntitySync;
			}
			set
			{
				this._isInReadEntitySync = value;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual byte[] GetQueryStringRawBytes()
		{
			return null;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002E5A6 File Offset: 0x0002C7A6
		public virtual string GetRemoteName()
		{
			return this.GetRemoteAddress();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0002E5AE File Offset: 0x0002C7AE
		public virtual string GetServerName()
		{
			return this.GetLocalAddress();
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0002E5B6 File Offset: 0x0002C7B6
		public virtual long GetConnectionID()
		{
			return 0L;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0002E5B6 File Offset: 0x0002C7B6
		public virtual long GetUrlContextID()
		{
			return 0L;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetAppPoolID()
		{
			return null;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00007722 File Offset: 0x00005922
		public virtual int GetRequestReason()
		{
			return 0;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002E5BA File Offset: 0x0002C7BA
		public virtual IntPtr GetUserToken()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		internal WindowsIdentity GetLogonUserIdentity()
		{
			IntPtr userToken = this.GetUserToken();
			if (userToken != IntPtr.Zero)
			{
				string serverVariable = this.GetServerVariable("LOGON_USER");
				string serverVariable2 = this.GetServerVariable("AUTH_TYPE");
				bool isAuthenticated = !string.IsNullOrEmpty(serverVariable) || (!string.IsNullOrEmpty(serverVariable2) && !StringUtil.EqualsIgnoreCase(serverVariable2, "basic"));
				return HttpWorkerRequest.CreateWindowsIdentityWithAssert(userToken, (serverVariable2 == null) ? "" : serverVariable2, WindowsAccountType.Normal, isAuthenticated);
			}
			return null;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002E637 File Offset: 0x0002C837
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static WindowsIdentity CreateWindowsIdentityWithAssert(IntPtr token, string authType, WindowsAccountType accountType, bool isAuthenticated)
		{
			return new WindowsIdentity(token, authType, accountType, isAuthenticated);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002E5BA File Offset: 0x0002C7BA
		public virtual IntPtr GetVirtualPathToken()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsSecure()
		{
			return false;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0002E642 File Offset: 0x0002C842
		public virtual string GetProtocol()
		{
			if (!this.IsSecure())
			{
				return "http";
			}
			return "https";
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0002E657 File Offset: 0x0002C857
		public virtual string GetFilePath()
		{
			return this.GetUriPath();
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0002E65F File Offset: 0x0002C85F
		internal VirtualPath GetFilePathObject()
		{
			return VirtualPath.Create(this.GetFilePath(), VirtualPathOptions.AllowNull | VirtualPathOptions.AllowAbsolutePath);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetFilePathTranslated()
		{
			return null;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00028752 File Offset: 0x00026952
		public virtual string GetPathInfo()
		{
			return string.Empty;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetAppPath()
		{
			return null;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetAppPathTranslated()
		{
			return null;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0002E670 File Offset: 0x0002C870
		public virtual int GetPreloadedEntityBodyLength()
		{
			byte[] preloadedEntityBody = this.GetPreloadedEntityBody();
			if (preloadedEntityBody == null)
			{
				return 0;
			}
			return preloadedEntityBody.Length;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0002E68C File Offset: 0x0002C88C
		public virtual int GetPreloadedEntityBody(byte[] buffer, int offset)
		{
			int num = 0;
			byte[] preloadedEntityBody = this.GetPreloadedEntityBody();
			if (preloadedEntityBody != null)
			{
				num = preloadedEntityBody.Length;
				Buffer.BlockCopy(preloadedEntityBody, 0, buffer, offset, num);
			}
			return num;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual byte[] GetPreloadedEntityBody()
		{
			return null;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsEntireEntityBodyIsPreloaded()
		{
			return false;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0002E6B4 File Offset: 0x0002C8B4
		public virtual int GetTotalEntityBodyLength()
		{
			int result = 0;
			string knownRequestHeader = this.GetKnownRequestHeader(11);
			if (knownRequestHeader != null)
			{
				try
				{
					result = int.Parse(knownRequestHeader, CultureInfo.InvariantCulture);
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00007722 File Offset: 0x00005922
		public virtual int ReadEntityBody(byte[] buffer, int size)
		{
			return 0;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0002E6F4 File Offset: 0x0002C8F4
		public virtual int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			byte[] array = new byte[size];
			int num = this.ReadEntityBody(array, size);
			if (num > 0)
			{
				if (offset < 0 || buffer.Length - offset < size)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				Buffer.BlockCopy(array, 0, buffer, offset, num);
			}
			return num;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool SupportsAsyncFlush
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00010D64 File Offset: 0x0000EF64
		public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00010D64 File Offset: 0x0000EF64
		public virtual void EndFlush(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool SupportsAsyncRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00010D64 File Offset: 0x0000EF64
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00010D64 File Offset: 0x0000EF64
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetKnownRequestHeader(int index)
		{
			return null;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetUnknownRequestHeader(string name)
		{
			return null;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0000298D File Offset: 0x00000B8D
		[CLSCompliant(false)]
		public virtual string[][] GetUnknownRequestHeaders()
		{
			return null;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetServerVariable(string name)
		{
			return null;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0002E5B6 File Offset: 0x0002C7B6
		public virtual long GetBytesRead()
		{
			return 0L;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0002E737 File Offset: 0x0002C937
		internal virtual DateTime GetStartTime()
		{
			return this._startTime;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0002E73F File Offset: 0x0002C93F
		internal virtual void ResetStartTime()
		{
			this._startTime = DateTime.UtcNow;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string MapPath(string virtualPath)
		{
			return null;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string MachineConfigPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string RootWebConfigPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string MachineInstallDirectory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void RaiseTraceEvent(IntegratedTraceType traceType, string eventData)
		{
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void RaiseTraceEvent(WebBaseEvent webEvent)
		{
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0002E74C File Offset: 0x0002C94C
		public virtual Guid RequestTraceIdentifier
		{
			get
			{
				return this._traceId;
			}
		}

		// Token: 0x060010EB RID: 4331
		public abstract void SendStatus(int statusCode, string statusDescription);

		// Token: 0x060010EC RID: 4332 RVA: 0x0002E754 File Offset: 0x0002C954
		internal virtual void SendStatus(int statusCode, int subStatusCode, string statusDescription)
		{
			this.SendStatus(statusCode, statusDescription);
		}

		// Token: 0x060010ED RID: 4333
		public abstract void SendKnownResponseHeader(int index, string value);

		// Token: 0x060010EE RID: 4334
		public abstract void SendUnknownResponseHeader(string name, string value);

		// Token: 0x060010EF RID: 4335 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void SetHeaderEncoding(Encoding encoding)
		{
		}

		// Token: 0x060010F0 RID: 4336
		public abstract void SendResponseFromMemory(byte[] data, int length);

		// Token: 0x060010F1 RID: 4337 RVA: 0x0002E760 File Offset: 0x0002C960
		public virtual void SendResponseFromMemory(IntPtr data, int length)
		{
			if (length > 0)
			{
				InternalSecurityPermissions.UnmanagedCode.Demand();
				byte[] array = new byte[length];
				Misc.CopyMemory(data, 0, array, 0, length);
				this.SendResponseFromMemory(array, length);
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0002E794 File Offset: 0x0002C994
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal virtual void SendResponseFromMemory(IntPtr data, int length, bool isBufferFromUnmanagedPool)
		{
			this.SendResponseFromMemory(data, length);
		}

		// Token: 0x060010F3 RID: 4339
		public abstract void SendResponseFromFile(string filename, long offset, long length);

		// Token: 0x060010F4 RID: 4340
		public abstract void SendResponseFromFile(IntPtr handle, long offset, long length);

		// Token: 0x060010F5 RID: 4341 RVA: 0x0002E79E File Offset: 0x0002C99E
		internal virtual void TransmitFile(string filename, long length, bool isImpersonating)
		{
			this.TransmitFile(filename, 0L, length, isImpersonating);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0002E7AB File Offset: 0x0002C9AB
		internal virtual void TransmitFile(string filename, long offset, long length, bool isImpersonating)
		{
			this.SendResponseFromFile(filename, offset, length);
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool SupportsLongTransmitFile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0000298D File Offset: 0x00000B8D
		internal virtual string SetupKernelCaching(int secondsToLive, string originalCacheUrl, bool enableKernelCacheForVaryByStar)
		{
			return null;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void DisableKernelCache()
		{
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void DisableUserCache()
		{
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual bool TrySkipIisCustomErrors
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool SupportsExecuteUrl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0002E7B6 File Offset: 0x0002C9B6
		internal virtual IAsyncResult BeginExecuteUrl(string url, string method, string headers, bool sendHeaders, bool addUserIndo, IntPtr token, string name, string authType, byte[] entity, AsyncCallback cb, object state)
		{
			throw new NotSupportedException(SR.GetString("ExecuteUrl_not_supported"));
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void EndExecuteUrl(IAsyncResult result)
		{
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void UpdateInitialCounters()
		{
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void UpdateResponseCounters(bool finalFlush, int bytesOut)
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void UpdateRequestCounters(int bytesIn)
		{
		}

		// Token: 0x06001103 RID: 4355
		public abstract void FlushResponse(bool finalFlush);

		// Token: 0x06001104 RID: 4356
		public abstract void EndOfRequest();

		// Token: 0x06001105 RID: 4357 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void SetEndOfSendNotification(HttpWorkerRequest.EndOfSendNotification callback, object extraData)
		{
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void SendCalculatedContentLength(int contentLength)
		{
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0002E7C7 File Offset: 0x0002C9C7
		public virtual void SendCalculatedContentLength(long contentLength)
		{
			this.SendCalculatedContentLength(Convert.ToInt32(contentLength));
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool HeadersSent()
		{
			return true;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool IsClientConnected()
		{
			return true;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void CloseConnection()
		{
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0002E7D5 File Offset: 0x0002C9D5
		public virtual byte[] GetClientCertificate()
		{
			return new byte[0];
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0002E7DD File Offset: 0x0002C9DD
		public virtual DateTime GetClientCertificateValidFrom()
		{
			return DateTime.Now;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0002E7DD File Offset: 0x0002C9DD
		public virtual DateTime GetClientCertificateValidUntil()
		{
			return DateTime.Now;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0002E7D5 File Offset: 0x0002C9D5
		public virtual byte[] GetClientCertificateBinaryIssuer()
		{
			return new byte[0];
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00007722 File Offset: 0x00005922
		public virtual int GetClientCertificateEncoding()
		{
			return 0;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0002E7D5 File Offset: 0x0002C9D5
		public virtual byte[] GetClientCertificatePublicKey()
		{
			return new byte[0];
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		public bool HasEntityBody()
		{
			string knownRequestHeader = this.GetKnownRequestHeader(11);
			if (knownRequestHeader != null && !knownRequestHeader.Equals("0"))
			{
				return true;
			}
			if (this.GetKnownRequestHeader(6) != null)
			{
				return true;
			}
			if (this.GetPreloadedEntityBody() != null)
			{
				return true;
			}
			this.IsEntireEntityBodyIsPreloaded();
			return false;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0002E82C File Offset: 0x0002CA2C
		public static string GetStatusDescription(int code)
		{
			if (code >= 100 && code < 600)
			{
				int num = code / 100;
				int num2 = code % 100;
				if (num2 < HttpWorkerRequest.s_HTTPStatusDescriptions[num].Length)
				{
					return HttpWorkerRequest.s_HTTPStatusDescriptions[num][num2];
				}
			}
			return string.Empty;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0002E86C File Offset: 0x0002CA6C
		public static int GetKnownRequestHeaderIndex(string header)
		{
			object obj = HttpWorkerRequest.s_requestHeadersLoookupTable[header];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0002E890 File Offset: 0x0002CA90
		public static string GetKnownRequestHeaderName(int index)
		{
			return HttpWorkerRequest.s_requestHeaderNames[index];
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0002E899 File Offset: 0x0002CA99
		internal static string GetServerVariableNameFromKnownRequestHeaderIndex(int index)
		{
			return HttpWorkerRequest.s_serverVarFromRequestHeaderNames[index];
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
		public static int GetKnownResponseHeaderIndex(string header)
		{
			object obj = HttpWorkerRequest.s_responseHeadersLoookupTable[header];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		public static string GetKnownResponseHeaderName(int index)
		{
			return HttpWorkerRequest.s_responseHeaderNames[index];
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		private static void DefineHeader(bool isRequest, bool isResponse, int index, string headerName, string serverVarName)
		{
			if (isRequest)
			{
				HttpWorkerRequest.s_serverVarFromRequestHeaderNames[index] = serverVarName;
				HttpWorkerRequest.s_requestHeaderNames[index] = headerName;
				HttpWorkerRequest.s_requestHeadersLoookupTable.Add(headerName, index);
			}
			if (isResponse)
			{
				HttpWorkerRequest.s_responseHeaderNames[index] = headerName;
				HttpWorkerRequest.s_responseHeadersLoookupTable.Add(headerName, index);
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0002E928 File Offset: 0x0002CB28
		static HttpWorkerRequest()
		{
			HttpWorkerRequest.DefineHeader(true, true, 0, "Cache-Control", "HTTP_CACHE_CONTROL");
			HttpWorkerRequest.DefineHeader(true, true, 1, "Connection", "HTTP_CONNECTION");
			HttpWorkerRequest.DefineHeader(true, true, 2, "Date", "HTTP_DATE");
			HttpWorkerRequest.DefineHeader(true, true, 3, "Keep-Alive", "HTTP_KEEP_ALIVE");
			HttpWorkerRequest.DefineHeader(true, true, 4, "Pragma", "HTTP_PRAGMA");
			HttpWorkerRequest.DefineHeader(true, true, 5, "Trailer", "HTTP_TRAILER");
			HttpWorkerRequest.DefineHeader(true, true, 6, "Transfer-Encoding", "HTTP_TRANSFER_ENCODING");
			HttpWorkerRequest.DefineHeader(true, true, 7, "Upgrade", "HTTP_UPGRADE");
			HttpWorkerRequest.DefineHeader(true, true, 8, "Via", "HTTP_VIA");
			HttpWorkerRequest.DefineHeader(true, true, 9, "Warning", "HTTP_WARNING");
			HttpWorkerRequest.DefineHeader(true, true, 10, "Allow", "HTTP_ALLOW");
			HttpWorkerRequest.DefineHeader(true, true, 11, "Content-Length", "HTTP_CONTENT_LENGTH");
			HttpWorkerRequest.DefineHeader(true, true, 12, "Content-Type", "HTTP_CONTENT_TYPE");
			HttpWorkerRequest.DefineHeader(true, true, 13, "Content-Encoding", "HTTP_CONTENT_ENCODING");
			HttpWorkerRequest.DefineHeader(true, true, 14, "Content-Language", "HTTP_CONTENT_LANGUAGE");
			HttpWorkerRequest.DefineHeader(true, true, 15, "Content-Location", "HTTP_CONTENT_LOCATION");
			HttpWorkerRequest.DefineHeader(true, true, 16, "Content-MD5", "HTTP_CONTENT_MD5");
			HttpWorkerRequest.DefineHeader(true, true, 17, "Content-Range", "HTTP_CONTENT_RANGE");
			HttpWorkerRequest.DefineHeader(true, true, 18, "Expires", "HTTP_EXPIRES");
			HttpWorkerRequest.DefineHeader(true, true, 19, "Last-Modified", "HTTP_LAST_MODIFIED");
			HttpWorkerRequest.DefineHeader(true, false, 20, "Accept", "HTTP_ACCEPT");
			HttpWorkerRequest.DefineHeader(true, false, 21, "Accept-Charset", "HTTP_ACCEPT_CHARSET");
			HttpWorkerRequest.DefineHeader(true, false, 22, "Accept-Encoding", "HTTP_ACCEPT_ENCODING");
			HttpWorkerRequest.DefineHeader(true, false, 23, "Accept-Language", "HTTP_ACCEPT_LANGUAGE");
			HttpWorkerRequest.DefineHeader(true, false, 24, "Authorization", "HTTP_AUTHORIZATION");
			HttpWorkerRequest.DefineHeader(true, false, 25, "Cookie", "HTTP_COOKIE");
			HttpWorkerRequest.DefineHeader(true, false, 26, "Expect", "HTTP_EXPECT");
			HttpWorkerRequest.DefineHeader(true, false, 27, "From", "HTTP_FROM");
			HttpWorkerRequest.DefineHeader(true, false, 28, "Host", "HTTP_HOST");
			HttpWorkerRequest.DefineHeader(true, false, 29, "If-Match", "HTTP_IF_MATCH");
			HttpWorkerRequest.DefineHeader(true, false, 30, "If-Modified-Since", "HTTP_IF_MODIFIED_SINCE");
			HttpWorkerRequest.DefineHeader(true, false, 31, "If-None-Match", "HTTP_IF_NONE_MATCH");
			HttpWorkerRequest.DefineHeader(true, false, 32, "If-Range", "HTTP_IF_RANGE");
			HttpWorkerRequest.DefineHeader(true, false, 33, "If-Unmodified-Since", "HTTP_IF_UNMODIFIED_SINCE");
			HttpWorkerRequest.DefineHeader(true, false, 34, "Max-Forwards", "HTTP_MAX_FORWARDS");
			HttpWorkerRequest.DefineHeader(true, false, 35, "Proxy-Authorization", "HTTP_PROXY_AUTHORIZATION");
			HttpWorkerRequest.DefineHeader(true, false, 36, "Referer", "HTTP_REFERER");
			HttpWorkerRequest.DefineHeader(true, false, 37, "Range", "HTTP_RANGE");
			HttpWorkerRequest.DefineHeader(true, false, 38, "TE", "HTTP_TE");
			HttpWorkerRequest.DefineHeader(true, false, 39, "User-Agent", "HTTP_USER_AGENT");
			HttpWorkerRequest.DefineHeader(false, true, 20, "Accept-Ranges", null);
			HttpWorkerRequest.DefineHeader(false, true, 21, "Age", null);
			HttpWorkerRequest.DefineHeader(false, true, 22, "ETag", null);
			HttpWorkerRequest.DefineHeader(false, true, 23, "Location", null);
			HttpWorkerRequest.DefineHeader(false, true, 24, "Proxy-Authenticate", null);
			HttpWorkerRequest.DefineHeader(false, true, 25, "Retry-After", null);
			HttpWorkerRequest.DefineHeader(false, true, 26, "Server", null);
			HttpWorkerRequest.DefineHeader(false, true, 27, "Set-Cookie", null);
			HttpWorkerRequest.DefineHeader(false, true, 28, "Vary", null);
			HttpWorkerRequest.DefineHeader(false, true, 29, "WWW-Authenticate", null);
		}

		// Token: 0x0400064C RID: 1612
		private DateTime _startTime;

		// Token: 0x0400064D RID: 1613
		private volatile bool _isInReadEntitySync;

		// Token: 0x0400064E RID: 1614
		private Guid _traceId;

		// Token: 0x0400064F RID: 1615
		public const int HeaderCacheControl = 0;

		// Token: 0x04000650 RID: 1616
		public const int HeaderConnection = 1;

		// Token: 0x04000651 RID: 1617
		public const int HeaderDate = 2;

		// Token: 0x04000652 RID: 1618
		public const int HeaderKeepAlive = 3;

		// Token: 0x04000653 RID: 1619
		public const int HeaderPragma = 4;

		// Token: 0x04000654 RID: 1620
		public const int HeaderTrailer = 5;

		// Token: 0x04000655 RID: 1621
		public const int HeaderTransferEncoding = 6;

		// Token: 0x04000656 RID: 1622
		public const int HeaderUpgrade = 7;

		// Token: 0x04000657 RID: 1623
		public const int HeaderVia = 8;

		// Token: 0x04000658 RID: 1624
		public const int HeaderWarning = 9;

		// Token: 0x04000659 RID: 1625
		public const int HeaderAllow = 10;

		// Token: 0x0400065A RID: 1626
		public const int HeaderContentLength = 11;

		// Token: 0x0400065B RID: 1627
		public const int HeaderContentType = 12;

		// Token: 0x0400065C RID: 1628
		public const int HeaderContentEncoding = 13;

		// Token: 0x0400065D RID: 1629
		public const int HeaderContentLanguage = 14;

		// Token: 0x0400065E RID: 1630
		public const int HeaderContentLocation = 15;

		// Token: 0x0400065F RID: 1631
		public const int HeaderContentMd5 = 16;

		// Token: 0x04000660 RID: 1632
		public const int HeaderContentRange = 17;

		// Token: 0x04000661 RID: 1633
		public const int HeaderExpires = 18;

		// Token: 0x04000662 RID: 1634
		public const int HeaderLastModified = 19;

		// Token: 0x04000663 RID: 1635
		public const int HeaderAccept = 20;

		// Token: 0x04000664 RID: 1636
		public const int HeaderAcceptCharset = 21;

		// Token: 0x04000665 RID: 1637
		public const int HeaderAcceptEncoding = 22;

		// Token: 0x04000666 RID: 1638
		public const int HeaderAcceptLanguage = 23;

		// Token: 0x04000667 RID: 1639
		public const int HeaderAuthorization = 24;

		// Token: 0x04000668 RID: 1640
		public const int HeaderCookie = 25;

		// Token: 0x04000669 RID: 1641
		public const int HeaderExpect = 26;

		// Token: 0x0400066A RID: 1642
		public const int HeaderFrom = 27;

		// Token: 0x0400066B RID: 1643
		public const int HeaderHost = 28;

		// Token: 0x0400066C RID: 1644
		public const int HeaderIfMatch = 29;

		// Token: 0x0400066D RID: 1645
		public const int HeaderIfModifiedSince = 30;

		// Token: 0x0400066E RID: 1646
		public const int HeaderIfNoneMatch = 31;

		// Token: 0x0400066F RID: 1647
		public const int HeaderIfRange = 32;

		// Token: 0x04000670 RID: 1648
		public const int HeaderIfUnmodifiedSince = 33;

		// Token: 0x04000671 RID: 1649
		public const int HeaderMaxForwards = 34;

		// Token: 0x04000672 RID: 1650
		public const int HeaderProxyAuthorization = 35;

		// Token: 0x04000673 RID: 1651
		public const int HeaderReferer = 36;

		// Token: 0x04000674 RID: 1652
		public const int HeaderRange = 37;

		// Token: 0x04000675 RID: 1653
		public const int HeaderTe = 38;

		// Token: 0x04000676 RID: 1654
		public const int HeaderUserAgent = 39;

		// Token: 0x04000677 RID: 1655
		public const int RequestHeaderMaximum = 40;

		// Token: 0x04000678 RID: 1656
		public const int HeaderAcceptRanges = 20;

		// Token: 0x04000679 RID: 1657
		public const int HeaderAge = 21;

		// Token: 0x0400067A RID: 1658
		public const int HeaderEtag = 22;

		// Token: 0x0400067B RID: 1659
		public const int HeaderLocation = 23;

		// Token: 0x0400067C RID: 1660
		public const int HeaderProxyAuthenticate = 24;

		// Token: 0x0400067D RID: 1661
		public const int HeaderRetryAfter = 25;

		// Token: 0x0400067E RID: 1662
		public const int HeaderServer = 26;

		// Token: 0x0400067F RID: 1663
		public const int HeaderSetCookie = 27;

		// Token: 0x04000680 RID: 1664
		public const int HeaderVary = 28;

		// Token: 0x04000681 RID: 1665
		public const int HeaderWwwAuthenticate = 29;

		// Token: 0x04000682 RID: 1666
		public const int ResponseHeaderMaximum = 30;

		// Token: 0x04000683 RID: 1667
		public const int ReasonResponseCacheMiss = 0;

		// Token: 0x04000684 RID: 1668
		public const int ReasonFileHandleCacheMiss = 1;

		// Token: 0x04000685 RID: 1669
		public const int ReasonCachePolicy = 2;

		// Token: 0x04000686 RID: 1670
		public const int ReasonCacheSecurity = 3;

		// Token: 0x04000687 RID: 1671
		public const int ReasonClientDisconnect = 4;

		// Token: 0x04000688 RID: 1672
		public const int ReasonDefault = 0;

		// Token: 0x04000689 RID: 1673
		private static readonly string[][] s_HTTPStatusDescriptions = new string[][]
		{
			null,
			new string[]
			{
				"Continue",
				"Switching Protocols",
				"Processing"
			},
			new string[]
			{
				"OK",
				"Created",
				"Accepted",
				"Non-Authoritative Information",
				"No Content",
				"Reset Content",
				"Partial Content",
				"Multi-Status"
			},
			new string[]
			{
				"Multiple Choices",
				"Moved Permanently",
				"Found",
				"See Other",
				"Not Modified",
				"Use Proxy",
				string.Empty,
				"Temporary Redirect"
			},
			new string[]
			{
				"Bad Request",
				"Unauthorized",
				"Payment Required",
				"Forbidden",
				"Not Found",
				"Method Not Allowed",
				"Not Acceptable",
				"Proxy Authentication Required",
				"Request Timeout",
				"Conflict",
				"Gone",
				"Length Required",
				"Precondition Failed",
				"Request Entity Too Large",
				"Request-Uri Too Long",
				"Unsupported Media Type",
				"Requested Range Not Satisfiable",
				"Expectation Failed",
				string.Empty,
				string.Empty,
				string.Empty,
				string.Empty,
				"Unprocessable Entity",
				"Locked",
				"Failed Dependency"
			},
			new string[]
			{
				"Internal Server Error",
				"Not Implemented",
				"Bad Gateway",
				"Service Unavailable",
				"Gateway Timeout",
				"Http Version Not Supported",
				string.Empty,
				"Insufficient Storage"
			}
		};

		// Token: 0x0400068A RID: 1674
		private static string[] s_serverVarFromRequestHeaderNames = new string[40];

		// Token: 0x0400068B RID: 1675
		private static string[] s_requestHeaderNames = new string[40];

		// Token: 0x0400068C RID: 1676
		private static string[] s_responseHeaderNames = new string[30];

		// Token: 0x0400068D RID: 1677
		private static Hashtable s_requestHeadersLoookupTable = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400068E RID: 1678
		private static Hashtable s_responseHeadersLoookupTable = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x020008F7 RID: 2295
		// (Invoke) Token: 0x0600687D RID: 26749
		public delegate void EndOfSendNotification(HttpWorkerRequest wr, object extraData);
	}
}
