using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;
using Microsoft.Win32.SafeHandles;

namespace System.Web.Hosting
{
	// Token: 0x020002A6 RID: 678
	internal abstract class ISAPIWorkerRequest : HttpWorkerRequest
	{
		// Token: 0x0600232C RID: 9004 RVA: 0x000973D8 File Offset: 0x000963D8
		private string[] ReadBasics(int[] contentInfo)
		{
			RecyclableByteBuffer recyclableByteBuffer = new RecyclableByteBuffer();
			int i;
			for (i = this.GetBasicsCore(recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length, contentInfo); i < 0; i = this.GetBasicsCore(recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length, contentInfo))
			{
				recyclableByteBuffer.Resize(-i);
			}
			if (i == 0)
			{
				throw new HttpException(SR.GetString("Cannot_retrieve_request_data"));
			}
			string[] decodedTabSeparatedStrings = recyclableByteBuffer.GetDecodedTabSeparatedStrings(Encoding.Default, 6, 0);
			recyclableByteBuffer.Dispose();
			return decodedTabSeparatedStrings;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00097450 File Offset: 0x00096450
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
				int num2 = serverVariable.IndexOfAny(ISAPIWorkerRequest.s_ColonOrNL, i);
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

		// Token: 0x0600232E RID: 9006 RVA: 0x000975F0 File Offset: 0x000965F0
		private void SendHeaders()
		{
			if (!this._headersSent && this._statusSet)
			{
				this._headers.Append("\r\n");
				this.AddHeadersToCachedResponse(this._status.GetEncodedBytesBuffer(), this._headers.GetEncodedBytesBuffer(this._headerEncoding), (this._contentLengthSent || this._chunked) ? 1 : 0);
				this._headersSent = true;
			}
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x0009765C File Offset: 0x0009665C
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

		// Token: 0x06002330 RID: 9008 RVA: 0x000976CC File Offset: 0x000966CC
		private void ResetCachedResponse()
		{
			this._cachedResponseStatus = null;
			this._cachedResponseHeaders = null;
			this._cachedResponseBodyLength = 0;
			this._cachedResponseBodyBytes = null;
			this._requiresAsyncFlushCallback = false;
			this._ignoreMinAsyncSize = false;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000976F8 File Offset: 0x000966F8
		private void AddHeadersToCachedResponse(byte[] status, byte[] header, int keepConnected)
		{
			this._cachedResponseStatus = status;
			this._cachedResponseHeaders = header;
			this._cachedResponseKeepConnected = keepConnected;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0009770F File Offset: 0x0009670F
		private void AddBodyToCachedResponse(MemoryBytes bytes)
		{
			if (this._cachedResponseBodyBytes == null)
			{
				this._cachedResponseBodyBytes = new ArrayList();
			}
			this._cachedResponseBodyBytes.Add(bytes);
			this._cachedResponseBodyLength += bytes.Size;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00097744 File Offset: 0x00096744
		internal void UnlockCachedResponseBytesOnceAfterIoComplete()
		{
			if (Interlocked.Decrement(ref this._cachedResponseBodyBytesIoLockCount) == 0)
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
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000977B0 File Offset: 0x000967B0
		private void FlushCachedResponse(bool isFinal)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			bool flag = false;
			int num = 0;
			IntPtr[] array = null;
			int[] array2 = null;
			long num2 = 0L;
			try
			{
				if (this._cachedResponseBodyLength > 0)
				{
					num = this._cachedResponseBodyBytes.Count;
					array = RecyclableArrayHelper.GetIntPtrArray(num);
					array2 = RecyclableArrayHelper.GetIntegerArray(num);
					for (int i = 0; i < num; i++)
					{
						MemoryBytes memoryBytes = (MemoryBytes)this._cachedResponseBodyBytes[i];
						array[i] = memoryBytes.LockMemory();
						if (!isFinal || !memoryBytes.IsBufferFromUnmanagedPool)
						{
							this._requiresAsyncFlushCallback = true;
						}
						if (memoryBytes.UseTransmitFile)
						{
							array2[i] = -memoryBytes.Size;
							this._ignoreMinAsyncSize = true;
							num2 += memoryBytes.FileSize;
						}
						else
						{
							array2[i] = memoryBytes.Size;
							num2 += (long)memoryBytes.Size;
						}
					}
				}
				int doneWithSession = isFinal ? 1 : 0;
				int finalStatus = isFinal ? ((this._cachedResponseKeepConnected != 0) ? 2 : 1) : 0;
				this._cachedResponseBodyBytesIoLockCount = 2;
				this._endOfRequestCallbackLockCount++;
				if (isFinal)
				{
					PerfCounters.DecrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
				}
				int num3 = (int)num2;
				if (num3 > 0)
				{
					PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_OUT, num3);
				}
				try
				{
					this.FlushCore(this._cachedResponseStatus, this._cachedResponseHeaders, this._cachedResponseKeepConnected, this._cachedResponseBodyLength, num, array, array2, doneWithSession, finalStatus, out flag);
				}
				finally
				{
					if (isFinal)
					{
						this.Close();
						this._ecb = IntPtr.Zero;
					}
				}
			}
			finally
			{
				if (!flag)
				{
					this._cachedResponseBodyBytesIoLockCount--;
					this._endOfRequestCallbackLockCount--;
				}
				this.UnlockCachedResponseBytesOnceAfterIoComplete();
				RecyclableArrayHelper.ReuseIntPtrArray(array);
				RecyclableArrayHelper.ReuseIntegerArray(array2);
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0009798C File Offset: 0x0009698C
		internal void CallEndOfRequestCallbackOnceAfterAllIoComplete()
		{
			if (this._endOfRequestCallback != null && Interlocked.Decrement(ref this._endOfRequestCallbackLockCount) == 0)
			{
				try
				{
					this._endOfRequestCallback(this, this._endOfRequestCallbackArg);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000979D8 File Offset: 0x000969D8
		internal ISAPIWorkerRequest(IntPtr ecb)
		{
			this._ecb = ecb;
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_TOTAL);
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x00097A0B File Offset: 0x00096A0B
		public override Guid RequestTraceIdentifier
		{
			get
			{
				return this._traceId;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x00097A13 File Offset: 0x00096A13
		internal IntPtr Ecb
		{
			get
			{
				return this._ecb;
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00097A1C File Offset: 0x00096A1C
		internal void Initialize()
		{
			this.ReadRequestBasics();
			if (this._appPathTranslated != null && this._appPathTranslated.Length > 2 && !StringUtil.StringEndsWith(this._appPathTranslated, '\\'))
			{
				this._appPathTranslated += "\\";
			}
			PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, this._contentTotalLength);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00097A78 File Offset: 0x00096A78
		internal virtual void ReadRequestBasics()
		{
			int[] array = new int[4];
			string[] array2 = this.ReadBasics(array);
			if (array2 == null || array2.Length != 6)
			{
				throw new HttpException(SR.GetString("Cannot_retrieve_request_data"));
			}
			this._contentType = array[0];
			this._contentTotalLength = array[1];
			this._contentAvailLength = array[2];
			this._queryStringLength = array[3];
			this._method = array2[0];
			this._filePath = array2[1];
			this._pathInfo = array2[2];
			this._path = ((this._pathInfo.Length > 0) ? (this._filePath + this._pathInfo) : this._filePath);
			this._pathTranslated = array2[3];
			this._appPath = array2[4];
			this._appPathTranslated = array2[5];
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x00097B34 File Offset: 0x00096B34
		internal static ISAPIWorkerRequest CreateWorkerRequest(IntPtr ecb, bool useOOP)
		{
			ISAPIWorkerRequest result;
			if (useOOP)
			{
				EtwTrace.TraceEnableCheck(EtwTraceConfigType.DOWNLEVEL, IntPtr.Zero);
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_APPDOMAIN_ENTER, ecb, Thread.GetDomain().FriendlyName, null, false);
				}
				result = new ISAPIWorkerRequestOutOfProc(ecb);
			}
			else
			{
				int num = UnsafeNativeMethods.EcbGetVersion(ecb) >> 16;
				if (num >= 7)
				{
					EtwTrace.TraceEnableCheck(EtwTraceConfigType.IIS7_ISAPI, ecb);
				}
				else
				{
					EtwTrace.TraceEnableCheck(EtwTraceConfigType.DOWNLEVEL, IntPtr.Zero);
				}
				if (EtwTrace.IsTraceEnabled(5, 1))
				{
					EtwTrace.Trace(EtwTraceType.ETW_TYPE_APPDOMAIN_ENTER, ecb, Thread.GetDomain().FriendlyName, null, true);
				}
				if (num >= 7)
				{
					result = new ISAPIWorkerRequestInProcForIIS7(ecb);
				}
				else if (num == 6)
				{
					result = new ISAPIWorkerRequestInProcForIIS6(ecb);
				}
				else
				{
					result = new ISAPIWorkerRequestInProc(ecb);
				}
			}
			return result;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x00097BD6 File Offset: 0x00096BD6
		public override string GetUriPath()
		{
			return this._path;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00097BE0 File Offset: 0x00096BE0
		public override string GetQueryString()
		{
			if (this._queryStringLength == 0)
			{
				return string.Empty;
			}
			int num = this._queryStringLength + 2;
			StringBuilder stringBuilder = new StringBuilder(num);
			int queryStringCore = this.GetQueryStringCore(0, stringBuilder, num);
			if (queryStringCore != 1)
			{
				throw new HttpException(SR.GetString("Cannot_get_query_string"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00097C30 File Offset: 0x00096C30
		public override byte[] GetQueryStringRawBytes()
		{
			if (this._queryStringLength == 0)
			{
				return null;
			}
			byte[] array = new byte[this._queryStringLength];
			int queryStringRawBytesCore = this.GetQueryStringRawBytesCore(array, this._queryStringLength);
			if (queryStringRawBytesCore != 1)
			{
				throw new HttpException(SR.GetString("Cannot_get_query_string_bytes"));
			}
			return array;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x00097C78 File Offset: 0x00096C78
		public override string GetRawUrl()
		{
			string queryString = this.GetQueryString();
			if (!string.IsNullOrEmpty(queryString))
			{
				return this._path + "?" + queryString;
			}
			return this._path;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00097CAC File Offset: 0x00096CAC
		public override string GetHttpVerbName()
		{
			return this._method;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00097CB4 File Offset: 0x00096CB4
		public override string GetHttpVersion()
		{
			return this.GetServerVariable("SERVER_PROTOCOL");
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x00097CC1 File Offset: 0x00096CC1
		public override string GetRemoteAddress()
		{
			return this.GetServerVariable("REMOTE_ADDR");
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00097CCE File Offset: 0x00096CCE
		public override string GetRemoteName()
		{
			return this.GetServerVariable("REMOTE_HOST");
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00097CDB File Offset: 0x00096CDB
		public override int GetRemotePort()
		{
			return 0;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x00097CDE File Offset: 0x00096CDE
		public override string GetLocalAddress()
		{
			return this.GetServerVariable("LOCAL_ADDR");
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x00097CEB File Offset: 0x00096CEB
		public override int GetLocalPort()
		{
			return int.Parse(this.GetServerVariable("SERVER_PORT"));
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00097CFD File Offset: 0x00096CFD
		internal override string GetLocalPortAsString()
		{
			return this.GetServerVariable("SERVER_PORT");
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x00097D0A File Offset: 0x00096D0A
		public override string GetServerName()
		{
			return this.GetServerVariable("SERVER_NAME");
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00097D18 File Offset: 0x00096D18
		public override bool IsSecure()
		{
			string serverVariable = this.GetServerVariable("HTTPS");
			return serverVariable != null && serverVariable.Equals("on");
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00097D41 File Offset: 0x00096D41
		public override string GetFilePath()
		{
			return this._filePath;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00097D49 File Offset: 0x00096D49
		public override string GetFilePathTranslated()
		{
			return this._pathTranslated;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00097D51 File Offset: 0x00096D51
		public override string GetPathInfo()
		{
			return this._pathInfo;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00097D59 File Offset: 0x00096D59
		public override string GetAppPath()
		{
			return this._appPath;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x00097D61 File Offset: 0x00096D61
		public override string GetAppPathTranslated()
		{
			return this._appPathTranslated;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x00097D69 File Offset: 0x00096D69
		public override int GetPreloadedEntityBodyLength()
		{
			return this._contentAvailLength;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00097D74 File Offset: 0x00096D74
		public override int GetPreloadedEntityBody(byte[] buffer, int offset)
		{
			if (this._contentAvailLength == 0)
			{
				return 0;
			}
			if (buffer.Length - offset < this._contentAvailLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			int preloadedPostedContentCore = this.GetPreloadedPostedContentCore(buffer, offset, this._contentAvailLength);
			if (preloadedPostedContentCore < 0)
			{
				throw new HttpException(SR.GetString("Cannot_read_posted_data"));
			}
			return preloadedPostedContentCore;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x00097DC8 File Offset: 0x00096DC8
		public override byte[] GetPreloadedEntityBody()
		{
			if (!this._preloadedContentRead)
			{
				if (this._contentAvailLength > 0)
				{
					this._preloadedContent = new byte[this._contentAvailLength];
					int preloadedPostedContentCore = this.GetPreloadedPostedContentCore(this._preloadedContent, 0, this._contentAvailLength);
					if (preloadedPostedContentCore < 0)
					{
						throw new HttpException(SR.GetString("Cannot_read_posted_data"));
					}
				}
				this._preloadedContentRead = true;
			}
			return this._preloadedContent;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00097E2C File Offset: 0x00096E2C
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return this._contentAvailLength == this._contentTotalLength;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00097E3C File Offset: 0x00096E3C
		public override int GetTotalEntityBodyLength()
		{
			return this._contentTotalLength;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00097E44 File Offset: 0x00096E44
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			return this.ReadEntityBody(buffer, 0, size);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00097E50 File Offset: 0x00096E50
		public override int ReadEntityBody(byte[] buffer, int offset, int size)
		{
			if (offset < 0 || buffer.Length - offset < size)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			int additionalPostedContentCore = this.GetAdditionalPostedContentCore(buffer, offset, size);
			if (additionalPostedContentCore < 0)
			{
				throw new HttpException(SR.GetString("Cannot_read_posted_data"));
			}
			return additionalPostedContentCore;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00097E93 File Offset: 0x00096E93
		public override long GetBytesRead()
		{
			throw new HttpException(SR.GetString("Not_supported"));
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00097EA4 File Offset: 0x00096EA4
		public override string GetKnownRequestHeader(int index)
		{
			if (!this._requestHeadersAvailable)
			{
				switch (index)
				{
				case 11:
					if (this._contentType != 0)
					{
						return this._contentTotalLength.ToString();
					}
					break;
				case 12:
					if (this._contentType == 1)
					{
						return "application/x-www-form-urlencoded";
					}
					break;
				}
				this.ReadRequestHeaders();
			}
			return this._knownRequestHeaders[index];
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00097F00 File Offset: 0x00096F00
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

		// Token: 0x06002359 RID: 9049 RVA: 0x00097F4E File Offset: 0x00096F4E
		public override string[][] GetUnknownRequestHeaders()
		{
			if (!this._requestHeadersAvailable)
			{
				this.ReadRequestHeaders();
			}
			return this._unknownRequestHeaders;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x00097F64 File Offset: 0x00096F64
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this._status.Append(statusCode.ToString());
			this._status.Append(" ");
			this._status.Append(statusDescription);
			this._statusSet = true;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x00097F9B File Offset: 0x00096F9B
		internal override void SetHeaderEncoding(Encoding encoding)
		{
			this._headerEncoding = encoding;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00097FA4 File Offset: 0x00096FA4
		public override void SendKnownResponseHeader(int index, string value)
		{
			if (this._headersSent)
			{
				throw new HttpException(SR.GetString("Cannot_append_header_after_headers_sent"));
			}
			if (index == 27)
			{
				this.DisableKernelCache();
			}
			this._headers.Append(HttpWorkerRequest.GetKnownResponseHeaderName(index));
			this._headers.Append(": ");
			this._headers.Append(value);
			this._headers.Append("\r\n");
			if (index == 11)
			{
				this._contentLengthSent = true;
				return;
			}
			if (index == 6 && value != null && value.Equals("chunked"))
			{
				this._chunked = true;
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0009803C File Offset: 0x0009703C
		public override void SendUnknownResponseHeader(string name, string value)
		{
			if (this._headersSent)
			{
				throw new HttpException(SR.GetString("Cannot_append_header_after_headers_sent"));
			}
			if (StringUtil.EqualsIgnoreCase(name, "Set-Cookie"))
			{
				this.DisableKernelCache();
			}
			this._headers.Append(name);
			this._headers.Append(": ");
			this._headers.Append(value);
			this._headers.Append("\r\n");
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000980AC File Offset: 0x000970AC
		public override void SendCalculatedContentLength(int contentLength)
		{
			this.SendCalculatedContentLength((long)contentLength);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000980B8 File Offset: 0x000970B8
		public override void SendCalculatedContentLength(long contentLength)
		{
			if (!this._headersSent)
			{
				this._headers.Append("Content-Length: ");
				this._headers.Append(contentLength.ToString(CultureInfo.InvariantCulture));
				this._headers.Append("\r\n");
				this._contentLengthSent = true;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0009810B File Offset: 0x0009710B
		public override bool HeadersSent()
		{
			return this._headersSent;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00098113 File Offset: 0x00097113
		public override bool IsClientConnected()
		{
			return this.IsClientConnectedCore() != 0;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00098120 File Offset: 0x00097120
		public override void CloseConnection()
		{
			this.CloseConnectionCore();
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00098129 File Offset: 0x00097129
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			if (length > 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length));
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0009814A File Offset: 0x0009714A
		public override void SendResponseFromMemory(IntPtr data, int length)
		{
			this.SendResponseFromMemory(data, length, false);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00098155 File Offset: 0x00097155
		internal override void SendResponseFromMemory(IntPtr data, int length, bool isBufferFromUnmanagedPool)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			if (length > 0)
			{
				this.AddBodyToCachedResponse(new MemoryBytes(data, length, isBufferFromUnmanagedPool ? BufferType.UnmanagedPool : BufferType.Managed));
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00098180 File Offset: 0x00097180
		internal virtual MemoryBytes PackageFile(string filename, long offset64, long length64, bool isImpersonating)
		{
			int num = Convert.ToInt32(offset64);
			Convert.ToInt32(length64);
			FileStream fileStream = null;
			MemoryBytes result = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				int num2 = (int)(fileStream.Length - (long)num);
				byte[] array = new byte[num2];
				int size = fileStream.Read(array, num, num2);
				result = new MemoryBytes(array, size);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000981F0 File Offset: 0x000971F0
		internal override void TransmitFile(string filename, long offset, long length, bool isImpersonating)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			if (length == 0L)
			{
				return;
			}
			this.AddBodyToCachedResponse(this.PackageFile(filename, offset, length, isImpersonating));
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00098218 File Offset: 0x00097218
		public override void SendResponseFromFile(string filename, long offset, long length)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			if (length == 0L)
			{
				return;
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
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

		// Token: 0x06002369 RID: 9065 RVA: 0x0009826C File Offset: 0x0009726C
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			if (length == 0L)
			{
				return;
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(new SafeFileHandle(handle, false), FileAccess.Read);
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

		// Token: 0x0600236A RID: 9066 RVA: 0x000982C4 File Offset: 0x000972C4
		public override void FlushResponse(bool finalFlush)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			this.FlushCachedResponse(finalFlush);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000982DC File Offset: 0x000972DC
		public override void EndOfRequest()
		{
			this.FlushCachedResponse(true);
			if (this._headers != null)
			{
				this._headers.Dispose();
				this._headers = null;
			}
			if (this._status != null)
			{
				this._status.Dispose();
				this._status = null;
			}
			this.CallEndOfRequestCallbackOnceAfterAllIoComplete();
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0009832A File Offset: 0x0009732A
		public override void SetEndOfSendNotification(HttpWorkerRequest.EndOfSendNotification callback, object extraData)
		{
			if (this._endOfRequestCallback != null)
			{
				throw new InvalidOperationException();
			}
			this._endOfRequestCallback = callback;
			this._endOfRequestCallbackArg = extraData;
			this._endOfRequestCallbackLockCount = 1;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0009834F File Offset: 0x0009734F
		public override string MapPath(string path)
		{
			return HostingEnvironment.MapPathInternal(path);
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x00098357 File Offset: 0x00097357
		public override string MachineConfigPath
		{
			get
			{
				return HttpConfigurationSystem.MachineConfigurationFilePath;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0009835E File Offset: 0x0009735E
		public override string RootWebConfigPath
		{
			get
			{
				return HttpConfigurationSystem.RootWebConfigurationFilePath;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x00098365 File Offset: 0x00097365
		public override string MachineInstallDirectory
		{
			get
			{
				return HttpRuntime.AspInstallDirectory;
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0009836C File Offset: 0x0009736C
		public override IntPtr GetUserToken()
		{
			return this.GetUserTokenCore();
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00098374 File Offset: 0x00097374
		public override IntPtr GetVirtualPathToken()
		{
			return this.GetVirtualPathTokenCore();
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0009837C File Offset: 0x0009737C
		public override byte[] GetClientCertificate()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCert;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00098392 File Offset: 0x00097392
		public override DateTime GetClientCertificateValidFrom()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidFrom;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000983A8 File Offset: 0x000973A8
		public override DateTime GetClientCertificateValidUntil()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidUntil;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000983BE File Offset: 0x000973BE
		public override byte[] GetClientCertificateBinaryIssuer()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertBinaryIssuer;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000983D4 File Offset: 0x000973D4
		public override int GetClientCertificateEncoding()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertEncoding;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000983EA File Offset: 0x000973EA
		public override byte[] GetClientCertificatePublicKey()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertPublicKey;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00098400 File Offset: 0x00097400
		private void FetchClientCertificate()
		{
			if (this._clientCertFetched)
			{
				return;
			}
			this._clientCertFetched = true;
			byte[] array = new byte[8192];
			int[] array2 = new int[4];
			long[] array3 = new long[2];
			int num = this.GetClientCertificateCore(array, array2, array3);
			if (num < 0 && -num > 8192)
			{
				num = -num + 100;
				array = new byte[num];
				num = this.GetClientCertificateCore(array, array2, array3);
			}
			if (num > 0)
			{
				this._clientCertEncoding = array2[0];
				if (array2[1] < array.Length && array2[1] > 0)
				{
					this._clientCert = new byte[array2[1]];
					Array.Copy(array, this._clientCert, array2[1]);
					if (array2[2] + array2[1] < array.Length && array2[2] > 0)
					{
						this._clientCertBinaryIssuer = new byte[array2[2]];
						Array.Copy(array, array2[1], this._clientCertBinaryIssuer, 0, array2[2]);
					}
					if (array2[2] + array2[1] + array2[3] < array.Length && array2[3] > 0)
					{
						this._clientCertPublicKey = new byte[array2[3]];
						Array.Copy(array, array2[1] + array2[2], this._clientCertPublicKey, 0, array2[3]);
					}
				}
			}
			if (num > 0 && array3[0] != 0L)
			{
				this._clientCertValidFrom = DateTime.FromFileTime(array3[0]);
			}
			else
			{
				this._clientCertValidFrom = DateTime.Now;
			}
			if (num > 0 && array3[1] != 0L)
			{
				this._clientCertValidUntil = DateTime.FromFileTime(array3[1]);
				return;
			}
			this._clientCertValidUntil = DateTime.Now;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0009855E File Offset: 0x0009755E
		internal void AppendLogParameter(string logParam)
		{
			this.AppendLogParameterCore(logParam);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00098568 File Offset: 0x00097568
		internal virtual void SendEmptyResponse()
		{
		}

		// Token: 0x0600237C RID: 9084
		internal abstract int GetBasicsCore(byte[] buffer, int size, int[] contentInfo);

		// Token: 0x0600237D RID: 9085
		internal abstract int GetQueryStringCore(int encode, StringBuilder buffer, int size);

		// Token: 0x0600237E RID: 9086
		internal abstract int GetQueryStringRawBytesCore(byte[] buffer, int size);

		// Token: 0x0600237F RID: 9087
		internal abstract int GetPreloadedPostedContentCore(byte[] bytes, int offset, int numBytesToRead);

		// Token: 0x06002380 RID: 9088
		internal abstract int GetAdditionalPostedContentCore(byte[] bytes, int offset, int bufferSize);

		// Token: 0x06002381 RID: 9089
		internal abstract void FlushCore(byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, out bool async);

		// Token: 0x06002382 RID: 9090
		internal abstract int IsClientConnectedCore();

		// Token: 0x06002383 RID: 9091
		internal abstract int CloseConnectionCore();

		// Token: 0x06002384 RID: 9092
		internal abstract int MapUrlToPathCore(string url, byte[] buffer, int size);

		// Token: 0x06002385 RID: 9093
		internal abstract IntPtr GetUserTokenCore();

		// Token: 0x06002386 RID: 9094
		internal abstract IntPtr GetVirtualPathTokenCore();

		// Token: 0x06002387 RID: 9095
		internal abstract int AppendLogParameterCore(string logParam);

		// Token: 0x06002388 RID: 9096
		internal abstract int GetClientCertificateCore(byte[] buffer, int[] pInts, long[] pDates);

		// Token: 0x06002389 RID: 9097
		internal abstract int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut);

		// Token: 0x0600238A RID: 9098 RVA: 0x0009856A File Offset: 0x0009756A
		internal virtual void Close()
		{
		}

		// Token: 0x04001BAA RID: 7082
		private const int CONTENT_NONE = 0;

		// Token: 0x04001BAB RID: 7083
		private const int CONTENT_FORM = 1;

		// Token: 0x04001BAC RID: 7084
		private const int CONTENT_MULTIPART = 2;

		// Token: 0x04001BAD RID: 7085
		private const int CONTENT_OTHER = 3;

		// Token: 0x04001BAE RID: 7086
		private const int STATUS_SUCCESS = 1;

		// Token: 0x04001BAF RID: 7087
		private const int STATUS_SUCCESS_AND_KEEP_CONN = 2;

		// Token: 0x04001BB0 RID: 7088
		private const int STATUS_PENDING = 3;

		// Token: 0x04001BB1 RID: 7089
		private const int STATUS_ERROR = 4;

		// Token: 0x04001BB2 RID: 7090
		protected IntPtr _ecb;

		// Token: 0x04001BB3 RID: 7091
		protected IntPtr _token;

		// Token: 0x04001BB4 RID: 7092
		protected Guid _traceId;

		// Token: 0x04001BB5 RID: 7093
		protected string _method;

		// Token: 0x04001BB6 RID: 7094
		protected string _path;

		// Token: 0x04001BB7 RID: 7095
		protected string _filePath;

		// Token: 0x04001BB8 RID: 7096
		protected string _pathInfo;

		// Token: 0x04001BB9 RID: 7097
		protected string _pathTranslated;

		// Token: 0x04001BBA RID: 7098
		protected string _appPath;

		// Token: 0x04001BBB RID: 7099
		protected string _appPathTranslated;

		// Token: 0x04001BBC RID: 7100
		protected int _contentType;

		// Token: 0x04001BBD RID: 7101
		protected int _contentTotalLength;

		// Token: 0x04001BBE RID: 7102
		protected int _contentAvailLength;

		// Token: 0x04001BBF RID: 7103
		protected int _queryStringLength;

		// Token: 0x04001BC0 RID: 7104
		protected bool _ignoreMinAsyncSize;

		// Token: 0x04001BC1 RID: 7105
		protected bool _requiresAsyncFlushCallback;

		// Token: 0x04001BC2 RID: 7106
		private bool _preloadedContentRead;

		// Token: 0x04001BC3 RID: 7107
		private byte[] _preloadedContent;

		// Token: 0x04001BC4 RID: 7108
		private bool _requestHeadersAvailable;

		// Token: 0x04001BC5 RID: 7109
		private string[][] _unknownRequestHeaders;

		// Token: 0x04001BC6 RID: 7110
		private string[] _knownRequestHeaders;

		// Token: 0x04001BC7 RID: 7111
		private bool _clientCertFetched;

		// Token: 0x04001BC8 RID: 7112
		private DateTime _clientCertValidFrom;

		// Token: 0x04001BC9 RID: 7113
		private DateTime _clientCertValidUntil;

		// Token: 0x04001BCA RID: 7114
		private byte[] _clientCert;

		// Token: 0x04001BCB RID: 7115
		private int _clientCertEncoding;

		// Token: 0x04001BCC RID: 7116
		private byte[] _clientCertPublicKey;

		// Token: 0x04001BCD RID: 7117
		private byte[] _clientCertBinaryIssuer;

		// Token: 0x04001BCE RID: 7118
		private bool _headersSent;

		// Token: 0x04001BCF RID: 7119
		private Encoding _headerEncoding;

		// Token: 0x04001BD0 RID: 7120
		private bool _contentLengthSent;

		// Token: 0x04001BD1 RID: 7121
		private bool _chunked;

		// Token: 0x04001BD2 RID: 7122
		private RecyclableCharBuffer _headers = new RecyclableCharBuffer();

		// Token: 0x04001BD3 RID: 7123
		private RecyclableCharBuffer _status = new RecyclableCharBuffer();

		// Token: 0x04001BD4 RID: 7124
		private bool _statusSet = true;

		// Token: 0x04001BD5 RID: 7125
		private byte[] _cachedResponseStatus;

		// Token: 0x04001BD6 RID: 7126
		private byte[] _cachedResponseHeaders;

		// Token: 0x04001BD7 RID: 7127
		private int _cachedResponseKeepConnected;

		// Token: 0x04001BD8 RID: 7128
		private int _cachedResponseBodyLength;

		// Token: 0x04001BD9 RID: 7129
		private ArrayList _cachedResponseBodyBytes;

		// Token: 0x04001BDA RID: 7130
		private int _cachedResponseBodyBytesIoLockCount;

		// Token: 0x04001BDB RID: 7131
		private HttpWorkerRequest.EndOfSendNotification _endOfRequestCallback;

		// Token: 0x04001BDC RID: 7132
		private object _endOfRequestCallbackArg;

		// Token: 0x04001BDD RID: 7133
		private int _endOfRequestCallbackLockCount;

		// Token: 0x04001BDE RID: 7134
		private static readonly char[] s_ColonOrNL = new char[]
		{
			':',
			'\n'
		};
	}
}
