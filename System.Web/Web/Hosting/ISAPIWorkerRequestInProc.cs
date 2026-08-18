using System;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002A7 RID: 679
	internal class ISAPIWorkerRequestInProc : ISAPIWorkerRequest
	{
		// Token: 0x0600238C RID: 9100 RVA: 0x00098590 File Offset: 0x00097590
		internal ISAPIWorkerRequestInProc(IntPtr ecb) : base(ecb)
		{
			if (ecb == IntPtr.Zero || UnsafeNativeMethods.EcbGetTraceContextId(ecb, out this._traceId) != 1)
			{
				this._traceId = Guid.Empty;
			}
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000985C0 File Offset: 0x000975C0
		internal override int GetBasicsCore(byte[] buffer, int size, int[] contentInfo)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetBasics(this._ecb, buffer, size, contentInfo);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000985E4 File Offset: 0x000975E4
		internal override int GetQueryStringCore(int encode, StringBuilder buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetQueryString(this._ecb, encode, buffer, size);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00098608 File Offset: 0x00097608
		internal override int GetQueryStringRawBytesCore(byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetQueryStringRawBytes(this._ecb, buffer, size);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0009862C File Offset: 0x0009762C
		internal override int GetPreloadedPostedContentCore(byte[] bytes, int offset, int numBytesToRead)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			int num = UnsafeNativeMethods.EcbGetPreloadedPostedContent(this._ecb, bytes, offset, numBytesToRead);
			if (num > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, num);
			}
			return num;
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0009866C File Offset: 0x0009766C
		internal override int GetAdditionalPostedContentCore(byte[] bytes, int offset, int bufferSize)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			int num = 0;
			try
			{
				base.IsInReadEntitySync = true;
				num = UnsafeNativeMethods.EcbGetAdditionalPostedContent(this._ecb, bytes, offset, bufferSize);
			}
			finally
			{
				base.IsInReadEntitySync = false;
			}
			if (num > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, num);
			}
			return num;
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000986CC File Offset: 0x000976CC
		internal override int GetClientCertificateCore(byte[] buffer, int[] pInts, long[] pDates)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetClientCertificate(this._ecb, buffer, buffer.Length, pInts, pDates);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000986F3 File Offset: 0x000976F3
		internal override int IsClientConnectedCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbIsClientConnected(this._ecb);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00098714 File Offset: 0x00097714
		internal override void FlushCore(byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, out bool async)
		{
			async = false;
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.EcbFlushCore(this._ecb, status, header, keepConnected, totalBodySize, numBodyFragments, bodyFragments, bodyFragmentLengths, doneWithSession, finalStatus, 0, 0, null);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00098756 File Offset: 0x00097756
		internal override int CloseConnectionCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbCloseConnection(this._ecb);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00098777 File Offset: 0x00097777
		internal override int MapUrlToPathCore(string url, byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbMapUrlToPath(this._ecb, url, buffer, size);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0009879C File Offset: 0x0009779C
		internal override IntPtr GetUserTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.EcbGetImpersonationToken(this._ecb, IntPtr.Zero);
			}
			return this._token;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000987EC File Offset: 0x000977EC
		internal override IntPtr GetVirtualPathTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.EcbGetVirtualPathToken(this._ecb, IntPtr.Zero);
			}
			return this._token;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00098839 File Offset: 0x00097839
		internal override int AppendLogParameterCore(string logParam)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbAppendLogParameter(this._ecb, logParam);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0009885C File Offset: 0x0009785C
		protected virtual string GetServerVariableCore(string name)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return null;
			}
			string result = null;
			RecyclableByteBuffer recyclableByteBuffer = new RecyclableByteBuffer();
			int i;
			for (i = UnsafeNativeMethods.EcbGetServerVariable(this._ecb, name, recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length); i < 0; i = UnsafeNativeMethods.EcbGetServerVariable(this._ecb, name, recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length))
			{
				recyclableByteBuffer.Resize(-i);
			}
			if (i > 0)
			{
				result = recyclableByteBuffer.GetDecodedString(Encoding.UTF8, i);
			}
			recyclableByteBuffer.Dispose();
			return result;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000988E4 File Offset: 0x000978E4
		protected virtual void GetAdditionalServerVariables()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			if (this._additionalServerVars != null)
			{
				return;
			}
			this._additionalServerVars = new string[23];
			for (int i = 0; i < this._additionalServerVars.Length; i++)
			{
				int nameIndex = i + 12;
				RecyclableByteBuffer recyclableByteBuffer = new RecyclableByteBuffer();
				int j;
				for (j = UnsafeNativeMethods.EcbGetServerVariableByIndex(this._ecb, nameIndex, recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length); j < 0; j = UnsafeNativeMethods.EcbGetServerVariableByIndex(this._ecb, nameIndex, recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length))
				{
					recyclableByteBuffer.Resize(-j);
				}
				if (j > 0)
				{
					this._additionalServerVars[i] = recyclableByteBuffer.GetDecodedString(Encoding.UTF8, j);
				}
				recyclableByteBuffer.Dispose();
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0009899A File Offset: 0x0009799A
		private string GetAdditionalServerVar(int index)
		{
			if (this._additionalServerVars == null)
			{
				this.GetAdditionalServerVariables();
			}
			return this._additionalServerVars[index - 12];
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000989B8 File Offset: 0x000979B8
		public override string GetServerVariable(string name)
		{
			if (name != null)
			{
				switch (name.Length)
				{
				case 5:
					if (name == "HTTPS")
					{
						return this._basicServerVars[10];
					}
					break;
				case 7:
					if (name == "ALL_RAW")
					{
						return this._basicServerVars[11];
					}
					break;
				case 9:
					if (name == "AUTH_TYPE")
					{
						return this._basicServerVars[1];
					}
					break;
				case 10:
					if (name == "LOGON_USER")
					{
						return this._basicServerVars[0];
					}
					if (name == "LOCAL_ADDR")
					{
						return this.GetAdditionalServerVar(30);
					}
					if (name == "CERT_FLAGS")
					{
						return this.GetAdditionalServerVar(15);
					}
					break;
				case 11:
					if (name == "SERVER_NAME")
					{
						return this._basicServerVars[8];
					}
					if (name == "SERVER_PORT")
					{
						return this._basicServerVars[9];
					}
					if (name == "REMOTE_HOST")
					{
						return this.GetAdditionalServerVar(31);
					}
					if (name == "REMOTE_PORT")
					{
						return this.GetAdditionalServerVar(32);
					}
					if (name == "REMOTE_ADDR")
					{
						return this.GetAdditionalServerVar(12);
					}
					if (name == "CERT_COOKIE")
					{
						return this.GetAdditionalServerVar(14);
					}
					if (name == "CERT_ISSUER")
					{
						return this.GetAdditionalServerVar(16);
					}
					if (name == "INSTANCE_ID")
					{
						return this.GetAdditionalServerVar(28);
					}
					break;
				case 12:
					if (name == "CERT_KEYSIZE")
					{
						return this.GetAdditionalServerVar(17);
					}
					if (name == "CERT_SUBJECT")
					{
						return this.GetAdditionalServerVar(22);
					}
					break;
				case 13:
					if (name == "AUTH_PASSWORD")
					{
						return this.GetAdditionalServerVar(13);
					}
					if (name == "HTTPS_KEYSIZE")
					{
						return this.GetAdditionalServerVar(24);
					}
					break;
				case 15:
					if (name == "HTTP_USER_AGENT")
					{
						return this.GetKnownRequestHeader(39);
					}
					if (name == "SERVER_PROTOCOL")
					{
						return this.GetAdditionalServerVar(33);
					}
					if (name == "SERVER_SOFTWARE")
					{
						return this.GetAdditionalServerVar(34);
					}
					break;
				case 17:
					if (name == "CERT_SERIALNUMBER")
					{
						return this.GetAdditionalServerVar(19);
					}
					if (name == "GATEWAY_INTERFACE")
					{
						return this.GetAdditionalServerVar(23);
					}
					break;
				case 18:
					if (name == "INSTANCE_META_PATH")
					{
						return this.GetAdditionalServerVar(29);
					}
					if (name == "CERT_SECRETKEYSIZE")
					{
						return this.GetAdditionalServerVar(18);
					}
					if (name == "CERT_SERVER_ISSUER")
					{
						return this.GetAdditionalServerVar(20);
					}
					break;
				case 19:
					if (name == "HTTPS_SECRETKEYSIZE")
					{
						return this.GetAdditionalServerVar(25);
					}
					if (name == "CERT_SERVER_SUBJECT")
					{
						return this.GetAdditionalServerVar(21);
					}
					if (name == "HTTPS_SERVER_ISSUER")
					{
						return this.GetAdditionalServerVar(26);
					}
					break;
				case 20:
					if (name == "HTTPS_SERVER_SUBJECT")
					{
						return this.GetAdditionalServerVar(27);
					}
					break;
				}
			}
			return this.GetServerVariableCore(name);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00098CD5 File Offset: 0x00097CD5
		internal override int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbCallISAPI(this._ecb, iFunction, bufIn, bufIn.Length, bufOut, bufOut.Length);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00098CFF File Offset: 0x00097CFF
		internal override void Close()
		{
			if (this._channelBindingToken != null && !this._channelBindingToken.IsInvalid)
			{
				this._channelBindingToken.Dispose();
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x00098D24 File Offset: 0x00097D24
		internal ChannelBinding HttpChannelBindingToken
		{
			get
			{
				if (this._channelBindingToken == null)
				{
					IntPtr zero = IntPtr.Zero;
					int tokenSize = 0;
					int num = UnsafeNativeMethods.EcbGetChannelBindingToken(this._ecb, out zero, out tokenSize);
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

		// Token: 0x04001BDF RID: 7135
		protected const int NUM_SERVER_VARIABLES = 35;

		// Token: 0x04001BE0 RID: 7136
		protected const int NUM_BASIC_SERVER_VARIABLES = 12;

		// Token: 0x04001BE1 RID: 7137
		protected const int NUM_ADDITIONAL_SERVER_VARIABLES = 23;

		// Token: 0x04001BE2 RID: 7138
		protected const int LOGON_USER = 0;

		// Token: 0x04001BE3 RID: 7139
		protected const int AUTH_TYPE = 1;

		// Token: 0x04001BE4 RID: 7140
		protected const int APPL_PHYSICAL_PATH = 2;

		// Token: 0x04001BE5 RID: 7141
		protected const int REQUEST_METHOD = 3;

		// Token: 0x04001BE6 RID: 7142
		protected const int PATH_INFO = 4;

		// Token: 0x04001BE7 RID: 7143
		protected const int PATH_TRANSLATED = 5;

		// Token: 0x04001BE8 RID: 7144
		protected const int URL = 6;

		// Token: 0x04001BE9 RID: 7145
		protected const int CACHE_URL = 7;

		// Token: 0x04001BEA RID: 7146
		protected const int SERVER_NAME = 8;

		// Token: 0x04001BEB RID: 7147
		protected const int SERVER_PORT = 9;

		// Token: 0x04001BEC RID: 7148
		protected const int HTTPS = 10;

		// Token: 0x04001BED RID: 7149
		protected const int ALL_RAW = 11;

		// Token: 0x04001BEE RID: 7150
		protected const int REMOTE_ADDR = 12;

		// Token: 0x04001BEF RID: 7151
		protected const int AUTH_PASSWORD = 13;

		// Token: 0x04001BF0 RID: 7152
		protected const int CERT_COOKIE = 14;

		// Token: 0x04001BF1 RID: 7153
		protected const int CERT_FLAGS = 15;

		// Token: 0x04001BF2 RID: 7154
		protected const int CERT_ISSUER = 16;

		// Token: 0x04001BF3 RID: 7155
		protected const int CERT_KEYSIZE = 17;

		// Token: 0x04001BF4 RID: 7156
		protected const int CERT_SECRETKEYSIZE = 18;

		// Token: 0x04001BF5 RID: 7157
		protected const int CERT_SERIALNUMBER = 19;

		// Token: 0x04001BF6 RID: 7158
		protected const int CERT_SERVER_ISSUER = 20;

		// Token: 0x04001BF7 RID: 7159
		protected const int CERT_SERVER_SUBJECT = 21;

		// Token: 0x04001BF8 RID: 7160
		protected const int CERT_SUBJECT = 22;

		// Token: 0x04001BF9 RID: 7161
		protected const int GATEWAY_INTERFACE = 23;

		// Token: 0x04001BFA RID: 7162
		protected const int HTTPS_KEYSIZE = 24;

		// Token: 0x04001BFB RID: 7163
		protected const int HTTPS_SECRETKEYSIZE = 25;

		// Token: 0x04001BFC RID: 7164
		protected const int HTTPS_SERVER_ISSUER = 26;

		// Token: 0x04001BFD RID: 7165
		protected const int HTTPS_SERVER_SUBJECT = 27;

		// Token: 0x04001BFE RID: 7166
		protected const int INSTANCE_ID = 28;

		// Token: 0x04001BFF RID: 7167
		protected const int INSTANCE_META_PATH = 29;

		// Token: 0x04001C00 RID: 7168
		protected const int LOCAL_ADDR = 30;

		// Token: 0x04001C01 RID: 7169
		protected const int REMOTE_HOST = 31;

		// Token: 0x04001C02 RID: 7170
		protected const int REMOTE_PORT = 32;

		// Token: 0x04001C03 RID: 7171
		protected const int SERVER_PROTOCOL = 33;

		// Token: 0x04001C04 RID: 7172
		protected const int SERVER_SOFTWARE = 34;

		// Token: 0x04001C05 RID: 7173
		protected string[] _basicServerVars;

		// Token: 0x04001C06 RID: 7174
		protected string[] _additionalServerVars;

		// Token: 0x04001C07 RID: 7175
		private ChannelBinding _channelBindingToken;
	}
}
