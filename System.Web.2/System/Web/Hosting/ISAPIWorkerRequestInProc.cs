using System;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007CB RID: 1995
	internal class ISAPIWorkerRequestInProc : ISAPIWorkerRequest
	{
		// Token: 0x06005FB9 RID: 24505 RVA: 0x0014A19B File Offset: 0x0014839B
		internal ISAPIWorkerRequestInProc(IntPtr ecb) : base(ecb)
		{
			if (ecb == IntPtr.Zero || UnsafeNativeMethods.EcbGetTraceContextId(ecb, out this._traceId) != 1)
			{
				this._traceId = Guid.Empty;
			}
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x0014A1CB File Offset: 0x001483CB
		internal override int GetBasicsCore(byte[] buffer, int size, int[] contentInfo)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetBasics(this._ecb, buffer, size, contentInfo);
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x0014A1EF File Offset: 0x001483EF
		internal override int GetQueryStringCore(int encode, StringBuilder buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetQueryString(this._ecb, encode, buffer, size);
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x0014A213 File Offset: 0x00148413
		internal override int GetQueryStringRawBytesCore(byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetQueryStringRawBytes(this._ecb, buffer, size);
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x0014A238 File Offset: 0x00148438
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

		// Token: 0x06005FBE RID: 24510 RVA: 0x0014A278 File Offset: 0x00148478
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

		// Token: 0x06005FBF RID: 24511 RVA: 0x0014A2D8 File Offset: 0x001484D8
		internal override int GetClientCertificateCore(byte[] buffer, int[] pInts, long[] pDates)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbGetClientCertificate(this._ecb, buffer, buffer.Length, pInts, pDates);
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x0014A2FF File Offset: 0x001484FF
		internal override int IsClientConnectedCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbIsClientConnected(this._ecb);
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x0014A320 File Offset: 0x00148520
		internal override void FlushCore(byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, out bool async)
		{
			async = false;
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.EcbFlushCore(this._ecb, status, header, keepConnected, totalBodySize, numBodyFragments, bodyFragments, bodyFragmentLengths, doneWithSession, finalStatus, 0, 0, null);
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x0014A362 File Offset: 0x00148562
		internal override int CloseConnectionCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbCloseConnection(this._ecb);
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x0014A383 File Offset: 0x00148583
		internal override int MapUrlToPathCore(string url, byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbMapUrlToPath(this._ecb, url, buffer, size);
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x0014A3A8 File Offset: 0x001485A8
		internal override IntPtr GetUserTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.EcbGetImpersonationToken(this._ecb, IntPtr.Zero);
			}
			return this._token;
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x0014A3F8 File Offset: 0x001485F8
		internal override IntPtr GetVirtualPathTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.EcbGetVirtualPathToken(this._ecb, IntPtr.Zero);
			}
			return this._token;
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x0014A445 File Offset: 0x00148645
		internal override int AppendLogParameterCore(string logParam)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbAppendLogParameter(this._ecb, logParam);
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x0014A468 File Offset: 0x00148668
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

		// Token: 0x06005FC8 RID: 24520 RVA: 0x0014A4F0 File Offset: 0x001486F0
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

		// Token: 0x06005FC9 RID: 24521 RVA: 0x0014A5A6 File Offset: 0x001487A6
		private string GetAdditionalServerVar(int index)
		{
			if (this._additionalServerVars == null)
			{
				this.GetAdditionalServerVariables();
			}
			return this._additionalServerVars[index - 12];
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x0014A5C4 File Offset: 0x001487C4
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

		// Token: 0x06005FCB RID: 24523 RVA: 0x0014A8E1 File Offset: 0x00148AE1
		internal override int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.EcbCallISAPI(this._ecb, iFunction, bufIn, bufIn.Length, bufOut, bufOut.Length);
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x0014A90B File Offset: 0x00148B0B
		internal override void Close()
		{
			if (this._channelBindingToken != null && !this._channelBindingToken.IsInvalid)
			{
				this._channelBindingToken.Dispose();
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x06005FCD RID: 24525 RVA: 0x0014A930 File Offset: 0x00148B30
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

		// Token: 0x040031F9 RID: 12793
		protected const int NUM_SERVER_VARIABLES = 35;

		// Token: 0x040031FA RID: 12794
		protected const int NUM_BASIC_SERVER_VARIABLES = 12;

		// Token: 0x040031FB RID: 12795
		protected const int NUM_ADDITIONAL_SERVER_VARIABLES = 23;

		// Token: 0x040031FC RID: 12796
		protected const int LOGON_USER = 0;

		// Token: 0x040031FD RID: 12797
		protected const int AUTH_TYPE = 1;

		// Token: 0x040031FE RID: 12798
		protected const int APPL_PHYSICAL_PATH = 2;

		// Token: 0x040031FF RID: 12799
		protected const int REQUEST_METHOD = 3;

		// Token: 0x04003200 RID: 12800
		protected const int PATH_INFO = 4;

		// Token: 0x04003201 RID: 12801
		protected const int PATH_TRANSLATED = 5;

		// Token: 0x04003202 RID: 12802
		protected const int URL = 6;

		// Token: 0x04003203 RID: 12803
		protected const int CACHE_URL = 7;

		// Token: 0x04003204 RID: 12804
		protected const int SERVER_NAME = 8;

		// Token: 0x04003205 RID: 12805
		protected const int SERVER_PORT = 9;

		// Token: 0x04003206 RID: 12806
		protected const int HTTPS = 10;

		// Token: 0x04003207 RID: 12807
		protected const int ALL_RAW = 11;

		// Token: 0x04003208 RID: 12808
		protected const int REMOTE_ADDR = 12;

		// Token: 0x04003209 RID: 12809
		protected const int AUTH_PASSWORD = 13;

		// Token: 0x0400320A RID: 12810
		protected const int CERT_COOKIE = 14;

		// Token: 0x0400320B RID: 12811
		protected const int CERT_FLAGS = 15;

		// Token: 0x0400320C RID: 12812
		protected const int CERT_ISSUER = 16;

		// Token: 0x0400320D RID: 12813
		protected const int CERT_KEYSIZE = 17;

		// Token: 0x0400320E RID: 12814
		protected const int CERT_SECRETKEYSIZE = 18;

		// Token: 0x0400320F RID: 12815
		protected const int CERT_SERIALNUMBER = 19;

		// Token: 0x04003210 RID: 12816
		protected const int CERT_SERVER_ISSUER = 20;

		// Token: 0x04003211 RID: 12817
		protected const int CERT_SERVER_SUBJECT = 21;

		// Token: 0x04003212 RID: 12818
		protected const int CERT_SUBJECT = 22;

		// Token: 0x04003213 RID: 12819
		protected const int GATEWAY_INTERFACE = 23;

		// Token: 0x04003214 RID: 12820
		protected const int HTTPS_KEYSIZE = 24;

		// Token: 0x04003215 RID: 12821
		protected const int HTTPS_SECRETKEYSIZE = 25;

		// Token: 0x04003216 RID: 12822
		protected const int HTTPS_SERVER_ISSUER = 26;

		// Token: 0x04003217 RID: 12823
		protected const int HTTPS_SERVER_SUBJECT = 27;

		// Token: 0x04003218 RID: 12824
		protected const int INSTANCE_ID = 28;

		// Token: 0x04003219 RID: 12825
		protected const int INSTANCE_META_PATH = 29;

		// Token: 0x0400321A RID: 12826
		protected const int LOCAL_ADDR = 30;

		// Token: 0x0400321B RID: 12827
		protected const int REMOTE_HOST = 31;

		// Token: 0x0400321C RID: 12828
		protected const int REMOTE_PORT = 32;

		// Token: 0x0400321D RID: 12829
		protected const int SERVER_PROTOCOL = 33;

		// Token: 0x0400321E RID: 12830
		protected const int SERVER_SOFTWARE = 34;

		// Token: 0x0400321F RID: 12831
		protected string[] _basicServerVars;

		// Token: 0x04003220 RID: 12832
		protected string[] _additionalServerVars;

		// Token: 0x04003221 RID: 12833
		private ChannelBinding _channelBindingToken;
	}
}
