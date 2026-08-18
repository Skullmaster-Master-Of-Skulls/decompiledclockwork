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
	// Token: 0x020007CA RID: 1994
	internal abstract class ISAPIWorkerRequest : HttpWorkerRequest
	{
		// Token: 0x06005F5A RID: 24410 RVA: 0x001490B4 File Offset: 0x001472B4
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

		// Token: 0x06005F5B RID: 24411 RVA: 0x0014912C File Offset: 0x0014732C
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

		// Token: 0x06005F5C RID: 24412 RVA: 0x001492CC File Offset: 0x001474CC
		private void SendHeaders()
		{
			if (!this._headersSent && this._statusSet)
			{
				this._headers.Append("\r\n");
				this.AddHeadersToCachedResponse(this._status.GetEncodedBytesBuffer(), this._headers.GetEncodedBytesBuffer(this._headerEncoding), (this._contentLengthSent || this._chunked) ? 1 : 0);
				this._headersSent = true;
			}
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x00149338 File Offset: 0x00147538
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

		// Token: 0x06005F5E RID: 24414 RVA: 0x001493A8 File Offset: 0x001475A8
		private void ResetCachedResponse()
		{
			this._cachedResponseStatus = null;
			this._cachedResponseHeaders = null;
			this._cachedResponseBodyLength = 0;
			this._cachedResponseBodyBytes = null;
			this._requiresAsyncFlushCallback = false;
			this._ignoreMinAsyncSize = false;
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x001493D4 File Offset: 0x001475D4
		private void AddHeadersToCachedResponse(byte[] status, byte[] header, int keepConnected)
		{
			this._cachedResponseStatus = status;
			this._cachedResponseHeaders = header;
			this._cachedResponseKeepConnected = keepConnected;
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x001493EB File Offset: 0x001475EB
		private void AddBodyToCachedResponse(MemoryBytes bytes)
		{
			if (this._cachedResponseBodyBytes == null)
			{
				this._cachedResponseBodyBytes = new ArrayList();
			}
			this._cachedResponseBodyBytes.Add(bytes);
			this._cachedResponseBodyLength += bytes.Size;
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x00149420 File Offset: 0x00147620
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
				FlushAsyncResult flushAsyncResult = this._asyncResultBase as FlushAsyncResult;
				if (flushAsyncResult != null)
				{
					this._endOfRequestCallbackLockCount--;
					this._asyncCompletionCallback(0, flushAsyncResult.HResult, IntPtr.Zero);
				}
			}
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x001494C0 File Offset: 0x001476C0
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

		// Token: 0x06005F63 RID: 24419 RVA: 0x0014968C File Offset: 0x0014788C
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

		// Token: 0x06005F64 RID: 24420 RVA: 0x001496D8 File Offset: 0x001478D8
		internal ISAPIWorkerRequest(IntPtr ecb)
		{
			this._ecb = ecb;
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_TOTAL);
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06005F65 RID: 24421 RVA: 0x0014970B File Offset: 0x0014790B
		public override Guid RequestTraceIdentifier
		{
			get
			{
				return this._traceId;
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x00149713 File Offset: 0x00147913
		internal IntPtr Ecb
		{
			get
			{
				return this._ecb;
			}
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x0014971C File Offset: 0x0014791C
		internal void Initialize()
		{
			this.ReadRequestBasics();
			if (this._appPathTranslated != null && this._appPathTranslated.Length > 2 && !StringUtil.StringEndsWith(this._appPathTranslated, '\\'))
			{
				this._appPathTranslated += "\\";
			}
			PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, this._contentTotalLength);
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x00149778 File Offset: 0x00147978
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

		// Token: 0x06005F69 RID: 24425 RVA: 0x00149834 File Offset: 0x00147A34
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

		// Token: 0x06005F6A RID: 24426 RVA: 0x001498D6 File Offset: 0x00147AD6
		public override string GetUriPath()
		{
			return this._path;
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x001498E0 File Offset: 0x00147AE0
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

		// Token: 0x06005F6C RID: 24428 RVA: 0x00149930 File Offset: 0x00147B30
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

		// Token: 0x06005F6D RID: 24429 RVA: 0x00149978 File Offset: 0x00147B78
		public override string GetRawUrl()
		{
			string queryString = this.GetQueryString();
			if (!string.IsNullOrEmpty(queryString))
			{
				return this._path + "?" + queryString;
			}
			return this._path;
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x001499AC File Offset: 0x00147BAC
		public override string GetHttpVerbName()
		{
			return this._method;
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x001452F2 File Offset: 0x001434F2
		public override string GetHttpVersion()
		{
			return this.GetServerVariable("SERVER_PROTOCOL");
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x001452FF File Offset: 0x001434FF
		public override string GetRemoteAddress()
		{
			return this.GetServerVariable("REMOTE_ADDR");
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x0014530C File Offset: 0x0014350C
		public override string GetRemoteName()
		{
			return this.GetServerVariable("REMOTE_HOST");
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x00007722 File Offset: 0x00005922
		public override int GetRemotePort()
		{
			return 0;
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x00145326 File Offset: 0x00143526
		public override string GetLocalAddress()
		{
			return this.GetServerVariable("LOCAL_ADDR");
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x001499B4 File Offset: 0x00147BB4
		public override int GetLocalPort()
		{
			return int.Parse(this.GetServerVariable("SERVER_PORT"));
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x0014534D File Offset: 0x0014354D
		internal override string GetLocalPortAsString()
		{
			return this.GetServerVariable("SERVER_PORT");
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x00145340 File Offset: 0x00143540
		public override string GetServerName()
		{
			return this.GetServerVariable("SERVER_NAME");
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x001499C8 File Offset: 0x00147BC8
		public override bool IsSecure()
		{
			string serverVariable = this.GetServerVariable("HTTPS");
			return serverVariable != null && serverVariable.Equals("on");
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x001499F1 File Offset: 0x00147BF1
		public override string GetFilePath()
		{
			return this._filePath;
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x001499F9 File Offset: 0x00147BF9
		public override string GetFilePathTranslated()
		{
			return this._pathTranslated;
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x00149A01 File Offset: 0x00147C01
		public override string GetPathInfo()
		{
			return this._pathInfo;
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x00149A09 File Offset: 0x00147C09
		public override string GetAppPath()
		{
			return this._appPath;
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x00149A11 File Offset: 0x00147C11
		public override string GetAppPathTranslated()
		{
			return this._appPathTranslated;
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x00149A19 File Offset: 0x00147C19
		public override int GetPreloadedEntityBodyLength()
		{
			return this._contentAvailLength;
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x00149A24 File Offset: 0x00147C24
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

		// Token: 0x06005F7F RID: 24447 RVA: 0x00149A88 File Offset: 0x00147C88
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return this._contentAvailLength == this._contentTotalLength;
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x00149A98 File Offset: 0x00147C98
		public override int GetTotalEntityBodyLength()
		{
			return this._contentTotalLength;
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x00149AA0 File Offset: 0x00147CA0
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			return this.ReadEntityBody(buffer, 0, size);
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x00149AAC File Offset: 0x00147CAC
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

		// Token: 0x06005F83 RID: 24451 RVA: 0x001454F2 File Offset: 0x001436F2
		public override long GetBytesRead()
		{
			throw new HttpException(SR.GetString("Not_supported"));
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x00149AF0 File Offset: 0x00147CF0
		public override string GetKnownRequestHeader(int index)
		{
			if (!this._requestHeadersAvailable)
			{
				if (index != 11)
				{
					if (index == 12 && this._contentType == 1)
					{
						return "application/x-www-form-urlencoded";
					}
				}
				else if (this._contentType != 0)
				{
					return this._contentTotalLength.ToString();
				}
				this.ReadRequestHeaders();
			}
			return this._knownRequestHeaders[index];
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x00149B40 File Offset: 0x00147D40
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

		// Token: 0x06005F86 RID: 24454 RVA: 0x00149B8E File Offset: 0x00147D8E
		public override string[][] GetUnknownRequestHeaders()
		{
			if (!this._requestHeadersAvailable)
			{
				this.ReadRequestHeaders();
			}
			return this._unknownRequestHeaders;
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x00149BA4 File Offset: 0x00147DA4
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this._status.Append(statusCode.ToString());
			this._status.Append(" ");
			this._status.Append(statusDescription);
			this._statusSet = true;
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x00149BDB File Offset: 0x00147DDB
		internal override void SetHeaderEncoding(Encoding encoding)
		{
			this._headerEncoding = encoding;
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x00149BE4 File Offset: 0x00147DE4
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

		// Token: 0x06005F8A RID: 24458 RVA: 0x00149C7C File Offset: 0x00147E7C
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

		// Token: 0x06005F8B RID: 24459 RVA: 0x00149CEC File Offset: 0x00147EEC
		public override void SendCalculatedContentLength(int contentLength)
		{
			this.SendCalculatedContentLength((long)contentLength);
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x00149CF8 File Offset: 0x00147EF8
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

		// Token: 0x06005F8D RID: 24461 RVA: 0x00149D4B File Offset: 0x00147F4B
		public override bool HeadersSent()
		{
			return this._headersSent;
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x00149D53 File Offset: 0x00147F53
		public override bool IsClientConnected()
		{
			return this.IsClientConnectedCore() != 0;
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x00149D60 File Offset: 0x00147F60
		public override void CloseConnection()
		{
			this.CloseConnectionCore();
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x00149D69 File Offset: 0x00147F69
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

		// Token: 0x06005F91 RID: 24465 RVA: 0x00149D8A File Offset: 0x00147F8A
		public override void SendResponseFromMemory(IntPtr data, int length)
		{
			this.SendResponseFromMemory(data, length, false);
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x00149D95 File Offset: 0x00147F95
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

		// Token: 0x06005F93 RID: 24467 RVA: 0x00149DC0 File Offset: 0x00147FC0
		internal virtual MemoryBytes PackageFile(string filename, long offset64, long length64, bool isImpersonating)
		{
			int num = Convert.ToInt32(offset64);
			int num2 = Convert.ToInt32(length64);
			FileStream fileStream = null;
			MemoryBytes result = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				int num3 = (int)(fileStream.Length - (long)num);
				byte[] array = new byte[num3];
				int size = fileStream.Read(array, num, num3);
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

		// Token: 0x06005F94 RID: 24468 RVA: 0x00149E34 File Offset: 0x00148034
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

		// Token: 0x06005F95 RID: 24469 RVA: 0x00149E5C File Offset: 0x0014805C
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

		// Token: 0x06005F96 RID: 24470 RVA: 0x00149EAC File Offset: 0x001480AC
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

		// Token: 0x06005F97 RID: 24471 RVA: 0x00149F00 File Offset: 0x00148100
		public override void FlushResponse(bool finalFlush)
		{
			if (!this._headersSent)
			{
				this.SendHeaders();
			}
			this.FlushCachedResponse(finalFlush);
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x00149F18 File Offset: 0x00148118
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

		// Token: 0x06005F99 RID: 24473 RVA: 0x00149F66 File Offset: 0x00148166
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

		// Token: 0x06005F9A RID: 24474 RVA: 0x00147490 File Offset: 0x00145690
		public override string MapPath(string path)
		{
			return HostingEnvironment.MapPathInternal(path);
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06005F9B RID: 24475 RVA: 0x001277FC File Offset: 0x001259FC
		public override string MachineConfigPath
		{
			get
			{
				return HttpConfigurationSystem.MachineConfigurationFilePath;
			}
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06005F9C RID: 24476 RVA: 0x00127803 File Offset: 0x00125A03
		public override string RootWebConfigPath
		{
			get
			{
				return HttpConfigurationSystem.RootWebConfigurationFilePath;
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x06005F9D RID: 24477 RVA: 0x00147498 File Offset: 0x00145698
		public override string MachineInstallDirectory
		{
			get
			{
				return HttpRuntime.AspInstallDirectory;
			}
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x00149F8B File Offset: 0x0014818B
		public override IntPtr GetUserToken()
		{
			return this.GetUserTokenCore();
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x00149F93 File Offset: 0x00148193
		public override IntPtr GetVirtualPathToken()
		{
			return this.GetVirtualPathTokenCore();
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x00149F9B File Offset: 0x0014819B
		public override byte[] GetClientCertificate()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCert;
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x00149FB1 File Offset: 0x001481B1
		public override DateTime GetClientCertificateValidFrom()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidFrom;
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x00149FC7 File Offset: 0x001481C7
		public override DateTime GetClientCertificateValidUntil()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertValidUntil;
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x00149FDD File Offset: 0x001481DD
		public override byte[] GetClientCertificateBinaryIssuer()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertBinaryIssuer;
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x00149FF3 File Offset: 0x001481F3
		public override int GetClientCertificateEncoding()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertEncoding;
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x0014A009 File Offset: 0x00148209
		public override byte[] GetClientCertificatePublicKey()
		{
			if (!this._clientCertFetched)
			{
				this.FetchClientCertificate();
			}
			return this._clientCertPublicKey;
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x0014A020 File Offset: 0x00148220
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

		// Token: 0x06005FA7 RID: 24487 RVA: 0x0014A17A File Offset: 0x0014837A
		internal void AppendLogParameter(string logParam)
		{
			this.AppendLogParameterCore(logParam);
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void SendEmptyResponse()
		{
		}

		// Token: 0x06005FA9 RID: 24489
		internal abstract int GetBasicsCore(byte[] buffer, int size, int[] contentInfo);

		// Token: 0x06005FAA RID: 24490
		internal abstract int GetQueryStringCore(int encode, StringBuilder buffer, int size);

		// Token: 0x06005FAB RID: 24491
		internal abstract int GetQueryStringRawBytesCore(byte[] buffer, int size);

		// Token: 0x06005FAC RID: 24492
		internal abstract int GetPreloadedPostedContentCore(byte[] bytes, int offset, int numBytesToRead);

		// Token: 0x06005FAD RID: 24493
		internal abstract int GetAdditionalPostedContentCore(byte[] bytes, int offset, int bufferSize);

		// Token: 0x06005FAE RID: 24494
		internal abstract void FlushCore(byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, out bool async);

		// Token: 0x06005FAF RID: 24495
		internal abstract int IsClientConnectedCore();

		// Token: 0x06005FB0 RID: 24496
		internal abstract int CloseConnectionCore();

		// Token: 0x06005FB1 RID: 24497
		internal abstract int MapUrlToPathCore(string url, byte[] buffer, int size);

		// Token: 0x06005FB2 RID: 24498
		internal abstract IntPtr GetUserTokenCore();

		// Token: 0x06005FB3 RID: 24499
		internal abstract IntPtr GetVirtualPathTokenCore();

		// Token: 0x06005FB4 RID: 24500
		internal abstract int AppendLogParameterCore(string logParam);

		// Token: 0x06005FB5 RID: 24501
		internal abstract int GetClientCertificateCore(byte[] buffer, int[] pInts, long[] pDates);

		// Token: 0x06005FB6 RID: 24502
		internal abstract int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut);

		// Token: 0x06005FB7 RID: 24503 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void Close()
		{
		}

		// Token: 0x040031C2 RID: 12738
		protected IntPtr _ecb;

		// Token: 0x040031C3 RID: 12739
		protected IntPtr _token;

		// Token: 0x040031C4 RID: 12740
		protected Guid _traceId;

		// Token: 0x040031C5 RID: 12741
		protected AsyncResultBase _asyncResultBase;

		// Token: 0x040031C6 RID: 12742
		protected AsyncCompletionCallback _asyncCompletionCallback;

		// Token: 0x040031C7 RID: 12743
		protected string _method;

		// Token: 0x040031C8 RID: 12744
		protected string _path;

		// Token: 0x040031C9 RID: 12745
		protected string _filePath;

		// Token: 0x040031CA RID: 12746
		protected string _pathInfo;

		// Token: 0x040031CB RID: 12747
		protected string _pathTranslated;

		// Token: 0x040031CC RID: 12748
		protected string _appPath;

		// Token: 0x040031CD RID: 12749
		protected string _appPathTranslated;

		// Token: 0x040031CE RID: 12750
		protected int _contentType;

		// Token: 0x040031CF RID: 12751
		protected int _contentTotalLength;

		// Token: 0x040031D0 RID: 12752
		protected int _contentAvailLength;

		// Token: 0x040031D1 RID: 12753
		protected int _queryStringLength;

		// Token: 0x040031D2 RID: 12754
		protected bool _ignoreMinAsyncSize;

		// Token: 0x040031D3 RID: 12755
		protected bool _requiresAsyncFlushCallback;

		// Token: 0x040031D4 RID: 12756
		private bool _preloadedContentRead;

		// Token: 0x040031D5 RID: 12757
		private byte[] _preloadedContent;

		// Token: 0x040031D6 RID: 12758
		private bool _requestHeadersAvailable;

		// Token: 0x040031D7 RID: 12759
		private string[][] _unknownRequestHeaders;

		// Token: 0x040031D8 RID: 12760
		private string[] _knownRequestHeaders;

		// Token: 0x040031D9 RID: 12761
		private bool _clientCertFetched;

		// Token: 0x040031DA RID: 12762
		private DateTime _clientCertValidFrom;

		// Token: 0x040031DB RID: 12763
		private DateTime _clientCertValidUntil;

		// Token: 0x040031DC RID: 12764
		private byte[] _clientCert;

		// Token: 0x040031DD RID: 12765
		private int _clientCertEncoding;

		// Token: 0x040031DE RID: 12766
		private byte[] _clientCertPublicKey;

		// Token: 0x040031DF RID: 12767
		private byte[] _clientCertBinaryIssuer;

		// Token: 0x040031E0 RID: 12768
		private bool _headersSent;

		// Token: 0x040031E1 RID: 12769
		private Encoding _headerEncoding;

		// Token: 0x040031E2 RID: 12770
		private bool _contentLengthSent;

		// Token: 0x040031E3 RID: 12771
		private bool _chunked;

		// Token: 0x040031E4 RID: 12772
		private RecyclableCharBuffer _headers = new RecyclableCharBuffer();

		// Token: 0x040031E5 RID: 12773
		private RecyclableCharBuffer _status = new RecyclableCharBuffer();

		// Token: 0x040031E6 RID: 12774
		private bool _statusSet = true;

		// Token: 0x040031E7 RID: 12775
		private byte[] _cachedResponseStatus;

		// Token: 0x040031E8 RID: 12776
		private byte[] _cachedResponseHeaders;

		// Token: 0x040031E9 RID: 12777
		private int _cachedResponseKeepConnected;

		// Token: 0x040031EA RID: 12778
		private int _cachedResponseBodyLength;

		// Token: 0x040031EB RID: 12779
		private ArrayList _cachedResponseBodyBytes;

		// Token: 0x040031EC RID: 12780
		private int _cachedResponseBodyBytesIoLockCount;

		// Token: 0x040031ED RID: 12781
		private HttpWorkerRequest.EndOfSendNotification _endOfRequestCallback;

		// Token: 0x040031EE RID: 12782
		private object _endOfRequestCallbackArg;

		// Token: 0x040031EF RID: 12783
		private int _endOfRequestCallbackLockCount;

		// Token: 0x040031F0 RID: 12784
		private const int CONTENT_NONE = 0;

		// Token: 0x040031F1 RID: 12785
		private const int CONTENT_FORM = 1;

		// Token: 0x040031F2 RID: 12786
		private const int CONTENT_MULTIPART = 2;

		// Token: 0x040031F3 RID: 12787
		private const int CONTENT_OTHER = 3;

		// Token: 0x040031F4 RID: 12788
		private const int STATUS_SUCCESS = 1;

		// Token: 0x040031F5 RID: 12789
		private const int STATUS_SUCCESS_AND_KEEP_CONN = 2;

		// Token: 0x040031F6 RID: 12790
		private const int STATUS_PENDING = 3;

		// Token: 0x040031F7 RID: 12791
		private const int STATUS_ERROR = 4;

		// Token: 0x040031F8 RID: 12792
		private static readonly char[] s_ColonOrNL = new char[]
		{
			':',
			'\n'
		};
	}
}
