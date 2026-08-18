using System;
using System.Globalization;
using System.Text;

namespace System.Web.SessionState
{
	// Token: 0x0200013C RID: 316
	internal class StateHttpWorkerRequest : HttpWorkerRequest
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x000361A0 File Offset: 0x000343A0
		internal StateHttpWorkerRequest(IntPtr tracker, UnsafeNativeMethods.StateProtocolVerb methodIndex, string uri, UnsafeNativeMethods.StateProtocolExclusive exclusive, int extraFlags, int timeout, int lockCookieExists, int lockCookie, int contentLength, IntPtr content)
		{
			this._tracker = tracker;
			this._methodIndex = methodIndex;
			switch (this._methodIndex)
			{
			case UnsafeNativeMethods.StateProtocolVerb.GET:
				this._method = "GET";
				break;
			case UnsafeNativeMethods.StateProtocolVerb.PUT:
				this._method = "PUT";
				break;
			case UnsafeNativeMethods.StateProtocolVerb.DELETE:
				this._method = "DELETE";
				break;
			case UnsafeNativeMethods.StateProtocolVerb.HEAD:
				this._method = "HEAD";
				break;
			}
			this._uri = uri;
			if (this._uri.StartsWith("//", StringComparison.Ordinal))
			{
				this._uri = this._uri.Substring(1);
			}
			this._exclusive = exclusive;
			this._extraFlags = extraFlags;
			this._timeout = timeout;
			this._lockCookie = lockCookie;
			this._lockCookieExists = (lockCookieExists != 0);
			this._contentLength = contentLength;
			if (contentLength != 0)
			{
				ulong num = (ulong)((long)content);
				this._content = new byte[]
				{
					(byte)(num & 255UL),
					(byte)((num & 65280UL) >> 8),
					(byte)((num & 16711680UL) >> 16),
					(byte)((num & (ulong)-16777216) >> 24),
					(byte)((num & 1095216660480UL) >> 32),
					(byte)((num & 280375465082880UL) >> 40),
					(byte)((num & 71776119061217280UL) >> 48),
					(byte)((num & 18374686479671623680UL) >> 56)
				};
			}
			this._status = new StringBuilder(256);
			this._headers = new StringBuilder(256);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0003632E File Offset: 0x0003452E
		public override string GetUriPath()
		{
			return HttpUtility.UrlDecode(this._uri);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0000298D File Offset: 0x00000B8D
		public override string GetFilePath()
		{
			return null;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0000298D File Offset: 0x00000B8D
		public override string GetQueryString()
		{
			return null;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003633B File Offset: 0x0003453B
		public override string GetRawUrl()
		{
			return this._uri;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00036343 File Offset: 0x00034543
		public override string GetHttpVerbName()
		{
			return this._method;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0003634B File Offset: 0x0003454B
		public override string GetHttpVersion()
		{
			return "HTTP/1.0";
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00036354 File Offset: 0x00034554
		public override string GetRemoteAddress()
		{
			if (this._remoteAddress == null)
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				UnsafeNativeMethods.STWNDGetRemoteAddress(this._tracker, stringBuilder);
				this._remoteAddress = stringBuilder.ToString();
			}
			return this._remoteAddress;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0003638F File Offset: 0x0003458F
		public override int GetRemotePort()
		{
			if (this._remotePort == 0)
			{
				this._remotePort = UnsafeNativeMethods.STWNDGetRemotePort(this._tracker);
			}
			return this._remotePort;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000363B0 File Offset: 0x000345B0
		public override string GetLocalAddress()
		{
			if (this._localAddress == null)
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				UnsafeNativeMethods.STWNDGetLocalAddress(this._tracker, stringBuilder);
				this._localAddress = stringBuilder.ToString();
			}
			return this._localAddress;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000363EB File Offset: 0x000345EB
		public override int GetLocalPort()
		{
			if (this._localPort == 0)
			{
				this._localPort = UnsafeNativeMethods.STWNDGetLocalPort(this._tracker);
			}
			return this._localPort;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0003640C File Offset: 0x0003460C
		public override byte[] GetPreloadedEntityBody()
		{
			return this._content;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool IsEntireEntityBodyIsPreloaded()
		{
			return true;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00036414 File Offset: 0x00034614
		public override string MapPath(string virtualPath)
		{
			return virtualPath;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00007722 File Offset: 0x00005922
		public override int ReadEntityBody(byte[] buffer, int size)
		{
			return 0;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00036417 File Offset: 0x00034617
		public override long GetBytesRead()
		{
			throw new NotSupportedException(SR.GetString("Not_supported"));
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00036428 File Offset: 0x00034628
		public override string GetKnownRequestHeader(int index)
		{
			string result = null;
			if (index == 11)
			{
				result = this._contentLength.ToString(CultureInfo.InvariantCulture);
			}
			return result;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00036450 File Offset: 0x00034650
		public override string GetUnknownRequestHeader(string name)
		{
			string result = null;
			if (name.Equals("Http_Exclusive"))
			{
				UnsafeNativeMethods.StateProtocolExclusive exclusive = this._exclusive;
				if (exclusive != UnsafeNativeMethods.StateProtocolExclusive.ACQUIRE)
				{
					if (exclusive == UnsafeNativeMethods.StateProtocolExclusive.RELEASE)
					{
						result = "release";
					}
				}
				else
				{
					result = "acquire";
				}
			}
			else if (name.Equals("Http_Timeout"))
			{
				if (this._timeout != -1)
				{
					result = this._timeout.ToString(CultureInfo.InvariantCulture);
				}
			}
			else if (name.Equals("Http_LockCookie"))
			{
				if (this._lockCookieExists)
				{
					result = this._lockCookie.ToString(CultureInfo.InvariantCulture);
				}
			}
			else if (name.Equals("Http_ExtraFlags") && this._extraFlags != -1)
			{
				result = this._extraFlags.ToString(CultureInfo.InvariantCulture);
			}
			return result;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0003650C File Offset: 0x0003470C
		public override string[][] GetUnknownRequestHeaders()
		{
			int num = 0;
			if (this._exclusive != (UnsafeNativeMethods.StateProtocolExclusive)(-1))
			{
				num++;
			}
			if (this._extraFlags != -1)
			{
				num++;
			}
			if (this._timeout != -1)
			{
				num++;
			}
			if (this._lockCookieExists)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			string[][] array = new string[num][];
			int num2 = 0;
			if (this._exclusive != (UnsafeNativeMethods.StateProtocolExclusive)(-1))
			{
				array[0] = new string[2];
				array[0][0] = "Http_Exclusive";
				if (this._exclusive == UnsafeNativeMethods.StateProtocolExclusive.ACQUIRE)
				{
					array[0][1] = "acquire";
				}
				else
				{
					array[0][1] = "release";
				}
				num2++;
			}
			if (this._timeout != -1)
			{
				array[num2] = new string[2];
				array[num2][0] = "Http_Timeout";
				array[num2][1] = this._timeout.ToString(CultureInfo.InvariantCulture);
				num2++;
			}
			if (this._lockCookieExists)
			{
				array[num2] = new string[2];
				array[num2][0] = "Http_LockCookie";
				array[num2][1] = this._lockCookie.ToString(CultureInfo.InvariantCulture);
				num2++;
			}
			if (this._extraFlags != -1)
			{
				array[num2] = new string[2];
				array[num2][0] = "Http_ExtraFlags";
				array[num2][1] = this._extraFlags.ToString(CultureInfo.InvariantCulture);
				num2++;
			}
			return array;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003663A File Offset: 0x0003483A
		public override void SendStatus(int statusCode, string statusDescription)
		{
			this._statusCode = statusCode;
			this._status.Append(statusCode.ToString(CultureInfo.InvariantCulture) + " " + statusDescription + "\r\n");
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0003666C File Offset: 0x0003486C
		public override void SendKnownResponseHeader(int index, string value)
		{
			this._headers.Append(HttpWorkerRequest.GetKnownResponseHeaderName(index));
			this._headers.Append(": ");
			this._headers.Append(value);
			this._headers.Append("\r\n");
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000366BA File Offset: 0x000348BA
		public override void SendUnknownResponseHeader(string name, string value)
		{
			this._headers.Append(name);
			this._headers.Append(": ");
			this._headers.Append(value);
			this._headers.Append("\r\n");
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00006164 File Offset: 0x00004364
		public override void SendCalculatedContentLength(int contentLength)
		{
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000366F8 File Offset: 0x000348F8
		public override bool HeadersSent()
		{
			return this._sent;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00036700 File Offset: 0x00034900
		public override bool IsClientConnected()
		{
			return UnsafeNativeMethods.STWNDIsClientConnected(this._tracker);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003670D File Offset: 0x0003490D
		public override void CloseConnection()
		{
			UnsafeNativeMethods.STWNDCloseConnection(this._tracker);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0003671C File Offset: 0x0003491C
		private void SendResponse()
		{
			if (!this._sent)
			{
				this._sent = true;
				UnsafeNativeMethods.STWNDSendResponse(this._tracker, this._status, this._status.Length, this._headers, this._headers.Length, this._unmanagedState);
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0003676C File Offset: 0x0003496C
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			if (this._statusCode == 200)
			{
				if (IntPtr.Size == 4)
				{
					this._unmanagedState = (IntPtr)((int)data[0] | (int)data[1] << 8 | (int)data[2] << 16 | (int)data[3] << 24);
				}
				else
				{
					this._unmanagedState = (IntPtr)((long)((ulong)data[0] | (ulong)data[1] << 8 | (ulong)data[2] << 16 | (ulong)data[3] << 24 | (ulong)data[4] << 32 | (ulong)data[5] << 40 | (ulong)data[6] << 48 | (ulong)data[7] << 56));
				}
			}
			this.SendResponse();
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00036417 File Offset: 0x00034617
		public override void SendResponseFromFile(string filename, long offset, long length)
		{
			throw new NotSupportedException(SR.GetString("Not_supported"));
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00036417 File Offset: 0x00034617
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
			throw new NotSupportedException(SR.GetString("Not_supported"));
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000367FE File Offset: 0x000349FE
		public override void FlushResponse(bool finalFlush)
		{
			this.SendResponse();
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00036806 File Offset: 0x00034A06
		public override void EndOfRequest()
		{
			this.SendResponse();
			UnsafeNativeMethods.STWNDEndOfRequest(this._tracker);
		}

		// Token: 0x040014AA RID: 5290
		private const int ADDRESS_LENGTH_MAX = 64;

		// Token: 0x040014AB RID: 5291
		private IntPtr _tracker;

		// Token: 0x040014AC RID: 5292
		private string _uri;

		// Token: 0x040014AD RID: 5293
		private UnsafeNativeMethods.StateProtocolExclusive _exclusive;

		// Token: 0x040014AE RID: 5294
		private int _extraFlags;

		// Token: 0x040014AF RID: 5295
		private int _timeout;

		// Token: 0x040014B0 RID: 5296
		private int _lockCookie;

		// Token: 0x040014B1 RID: 5297
		private bool _lockCookieExists;

		// Token: 0x040014B2 RID: 5298
		private int _contentLength;

		// Token: 0x040014B3 RID: 5299
		private byte[] _content;

		// Token: 0x040014B4 RID: 5300
		private UnsafeNativeMethods.StateProtocolVerb _methodIndex;

		// Token: 0x040014B5 RID: 5301
		private string _method;

		// Token: 0x040014B6 RID: 5302
		private string _remoteAddress;

		// Token: 0x040014B7 RID: 5303
		private int _remotePort;

		// Token: 0x040014B8 RID: 5304
		private string _localAddress;

		// Token: 0x040014B9 RID: 5305
		private int _localPort;

		// Token: 0x040014BA RID: 5306
		private StringBuilder _status;

		// Token: 0x040014BB RID: 5307
		private int _statusCode;

		// Token: 0x040014BC RID: 5308
		private StringBuilder _headers;

		// Token: 0x040014BD RID: 5309
		private IntPtr _unmanagedState;

		// Token: 0x040014BE RID: 5310
		private bool _sent;
	}
}
