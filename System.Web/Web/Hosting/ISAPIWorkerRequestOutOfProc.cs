using System;
using System.Collections;
using System.Text;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002AA RID: 682
	internal class ISAPIWorkerRequestOutOfProc : ISAPIWorkerRequest
	{
		// Token: 0x060023BF RID: 9151 RVA: 0x0009980B File Offset: 0x0009880B
		internal ISAPIWorkerRequestOutOfProc(IntPtr ecb) : base(ecb)
		{
			UnsafeNativeMethods.PMGetTraceContextId(ecb, out this._traceId);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00099824 File Offset: 0x00098824
		private void GetAllServerVars()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			RecyclableByteBuffer recyclableByteBuffer = new RecyclableByteBuffer();
			int i;
			for (i = UnsafeNativeMethods.PMGetAllServerVariables(this._ecb, recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length); i < 0; i = UnsafeNativeMethods.PMGetAllServerVariables(this._ecb, recyclableByteBuffer.Buffer, recyclableByteBuffer.Buffer.Length))
			{
				recyclableByteBuffer.Resize(-i);
			}
			if (i == 0)
			{
				throw new HttpException(SR.GetString("Cannot_retrieve_request_data"));
			}
			string[] decodedTabSeparatedStrings = recyclableByteBuffer.GetDecodedTabSeparatedStrings(Encoding.Default, 31, 1);
			recyclableByteBuffer.Dispose();
			this._serverVars = new Hashtable(32, StringComparer.OrdinalIgnoreCase);
			this._serverVars.Add("APPL_MD_PATH", HttpRuntime.AppDomainAppIdInternal);
			for (int j = 1; j < 32; j++)
			{
				this._serverVars.Add(ISAPIWorkerRequestOutOfProc._serverVarNames[j], decodedTabSeparatedStrings[j - 1]);
			}
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000998FF File Offset: 0x000988FF
		internal override int GetBasicsCore(byte[] buffer, int size, int[] contentInfo)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetBasics(this._ecb, buffer, size, contentInfo);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00099923 File Offset: 0x00098923
		internal override int GetQueryStringCore(int encode, StringBuilder buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetQueryString(this._ecb, encode, buffer, size);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00099947 File Offset: 0x00098947
		internal override int GetQueryStringRawBytesCore(byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetQueryStringRawBytes(this._ecb, buffer, size);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0009996C File Offset: 0x0009896C
		internal override int GetPreloadedPostedContentCore(byte[] bytes, int offset, int numBytesToRead)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			int num = UnsafeNativeMethods.PMGetPreloadedPostedContent(this._ecb, bytes, offset, numBytesToRead);
			if (num > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, num);
			}
			return num;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000999AC File Offset: 0x000989AC
		internal override int GetAdditionalPostedContentCore(byte[] bytes, int offset, int bufferSize)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			int num = UnsafeNativeMethods.PMGetAdditionalPostedContent(this._ecb, bytes, offset, bufferSize);
			if (num > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, num);
			}
			return num;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000999E9 File Offset: 0x000989E9
		internal override int IsClientConnectedCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMIsClientConnected(this._ecb);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00099A0C File Offset: 0x00098A0C
		internal override MemoryBytes PackageFile(string filename, long offset64, long length64, bool isImpersonating)
		{
			int value = Convert.ToInt32(offset64);
			int num = Convert.ToInt32(length64);
			byte[] bytes = BitConverter.GetBytes(value);
			byte[] bytes2 = BitConverter.GetBytes(num);
			byte[] bytes3 = Encoding.Unicode.GetBytes(filename);
			byte[] array = new byte[4 + bytes.Length + bytes2.Length + bytes3.Length + 2];
			if (isImpersonating)
			{
				array[0] = 49;
			}
			else
			{
				array[0] = 48;
			}
			Buffer.BlockCopy(bytes, 0, array, 4, bytes.Length);
			Buffer.BlockCopy(bytes2, 0, array, 4 + bytes.Length, bytes2.Length);
			Buffer.BlockCopy(bytes3, 0, array, 4 + bytes.Length + bytes2.Length, bytes3.Length);
			return new MemoryBytes(array, array.Length, true, (long)num);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00099AB0 File Offset: 0x00098AB0
		internal override void FlushCore(byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, out bool async)
		{
			async = false;
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			if (numBodyFragments > 1)
			{
				int num2;
				for (int i = 0; i < numBodyFragments; i = num2)
				{
					bool flag = i == 0;
					int num = bodyFragmentLengths[i];
					bool flag2 = bodyFragmentLengths[i] < 0;
					num2 = i + 1;
					if (!flag2)
					{
						while (num2 < numBodyFragments && num + bodyFragmentLengths[num2] < 31744 && bodyFragmentLengths[num2] >= 0)
						{
							num += bodyFragmentLengths[num2];
							num2++;
						}
					}
					bool flag3 = num2 == numBodyFragments;
					if (flag2)
					{
						num = -num;
					}
					UnsafeNativeMethods.PMFlushCore(this._ecb, flag ? status : null, flag ? header : null, keepConnected, num, i, num2 - i, bodyFragments, bodyFragmentLengths, flag3 ? doneWithSession : 0, flag3 ? finalStatus : 0);
				}
				return;
			}
			UnsafeNativeMethods.PMFlushCore(this._ecb, status, header, keepConnected, totalBodySize, 0, numBodyFragments, bodyFragments, bodyFragmentLengths, doneWithSession, finalStatus);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00099B96 File Offset: 0x00098B96
		internal override int CloseConnectionCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMCloseConnection(this._ecb);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00099BB7 File Offset: 0x00098BB7
		internal override int MapUrlToPathCore(string url, byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMMapUrlToPath(this._ecb, url, buffer, size);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00099BDB File Offset: 0x00098BDB
		internal override IntPtr GetUserTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.PMGetImpersonationToken(this._ecb);
			}
			return this._token;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00099C18 File Offset: 0x00098C18
		internal override IntPtr GetVirtualPathTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.PMGetVirtualPathToken(this._ecb);
			}
			return this._token;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00099C55 File Offset: 0x00098C55
		internal override int AppendLogParameterCore(string logParam)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMAppendLogParameter(this._ecb, logParam);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00099C77 File Offset: 0x00098C77
		internal override int GetClientCertificateCore(byte[] buffer, int[] pInts, long[] pDates)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetClientCertificate(this._ecb, buffer, buffer.Length, pInts, pDates);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00099C9E File Offset: 0x00098C9E
		public override string GetServerVariable(string name)
		{
			if (name.Equals("PATH_TRANSLATED"))
			{
				return this.GetFilePathTranslated();
			}
			if (this._serverVars == null)
			{
				this.GetAllServerVars();
			}
			return (string)this._serverVars[name];
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00099CD3 File Offset: 0x00098CD3
		internal override int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMCallISAPI(this._ecb, iFunction, bufIn, bufIn.Length, bufOut, bufOut.Length);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00099CFD File Offset: 0x00098CFD
		internal override void SendEmptyResponse()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.PMEmptyResponse(this._ecb);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00099D20 File Offset: 0x00098D20
		internal override DateTime GetStartTime()
		{
			if (this._ecb == IntPtr.Zero || this._useBaseTime)
			{
				return base.GetStartTime();
			}
			long filetime = UnsafeNativeMethods.PMGetStartTimeStamp(this._ecb);
			return DateTimeUtil.FromFileTimeToUtc(filetime);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00099D60 File Offset: 0x00098D60
		internal override void ResetStartTime()
		{
			base.ResetStartTime();
			this._useBaseTime = true;
		}

		// Token: 0x04001C18 RID: 7192
		private const int PM_FLUSH_THRESHOLD = 31744;

		// Token: 0x04001C19 RID: 7193
		private const int _numServerVars = 32;

		// Token: 0x04001C1A RID: 7194
		private bool _useBaseTime;

		// Token: 0x04001C1B RID: 7195
		private IDictionary _serverVars;

		// Token: 0x04001C1C RID: 7196
		private static string[] _serverVarNames = new string[]
		{
			"APPL_MD_PATH",
			"ALL_RAW",
			"AUTH_PASSWORD",
			"AUTH_TYPE",
			"CERT_COOKIE",
			"CERT_FLAGS",
			"CERT_ISSUER",
			"CERT_KEYSIZE",
			"CERT_SECRETKEYSIZE",
			"CERT_SERIALNUMBER",
			"CERT_SERVER_ISSUER",
			"CERT_SERVER_SUBJECT",
			"CERT_SUBJECT",
			"GATEWAY_INTERFACE",
			"HTTP_COOKIE",
			"HTTP_USER_AGENT",
			"HTTPS",
			"HTTPS_KEYSIZE",
			"HTTPS_SECRETKEYSIZE",
			"HTTPS_SERVER_ISSUER",
			"HTTPS_SERVER_SUBJECT",
			"INSTANCE_ID",
			"INSTANCE_META_PATH",
			"LOCAL_ADDR",
			"LOGON_USER",
			"REMOTE_ADDR",
			"REMOTE_HOST",
			"SERVER_NAME",
			"SERVER_PORT",
			"SERVER_PROTOCOL",
			"SERVER_SOFTWARE",
			"REMOTE_PORT"
		};
	}
}
