using System;
using System.Collections;
using System.Text;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007CE RID: 1998
	internal class ISAPIWorkerRequestOutOfProc : ISAPIWorkerRequest
	{
		// Token: 0x06005FF1 RID: 24561 RVA: 0x0014B710 File Offset: 0x00149910
		internal ISAPIWorkerRequestOutOfProc(IntPtr ecb) : base(ecb)
		{
			UnsafeNativeMethods.PMGetTraceContextId(ecb, out this._traceId);
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x0014B728 File Offset: 0x00149928
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
			this._serverVars.Add("APPL_MD_PATH", HttpRuntime.AppDomainAppId);
			for (int j = 1; j < 32; j++)
			{
				this._serverVars.Add(ISAPIWorkerRequestOutOfProc._serverVarNames[j], decodedTabSeparatedStrings[j - 1]);
			}
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x0014B803 File Offset: 0x00149A03
		internal override int GetBasicsCore(byte[] buffer, int size, int[] contentInfo)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetBasics(this._ecb, buffer, size, contentInfo);
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x0014B827 File Offset: 0x00149A27
		internal override int GetQueryStringCore(int encode, StringBuilder buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetQueryString(this._ecb, encode, buffer, size);
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x0014B84B File Offset: 0x00149A4B
		internal override int GetQueryStringRawBytesCore(byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetQueryStringRawBytes(this._ecb, buffer, size);
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x0014B870 File Offset: 0x00149A70
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

		// Token: 0x06005FF7 RID: 24567 RVA: 0x0014B8B0 File Offset: 0x00149AB0
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

		// Token: 0x06005FF8 RID: 24568 RVA: 0x0014B8ED File Offset: 0x00149AED
		internal override int IsClientConnectedCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMIsClientConnected(this._ecb);
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x0014B910 File Offset: 0x00149B10
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

		// Token: 0x06005FFA RID: 24570 RVA: 0x0014B9B4 File Offset: 0x00149BB4
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

		// Token: 0x06005FFB RID: 24571 RVA: 0x0014BA9A File Offset: 0x00149C9A
		internal override int CloseConnectionCore()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMCloseConnection(this._ecb);
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x0014BABB File Offset: 0x00149CBB
		internal override int MapUrlToPathCore(string url, byte[] buffer, int size)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMMapUrlToPath(this._ecb, url, buffer, size);
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x0014BADF File Offset: 0x00149CDF
		internal override IntPtr GetUserTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.PMGetImpersonationToken(this._ecb);
			}
			return this._token;
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x0014BB1C File Offset: 0x00149D1C
		internal override IntPtr GetVirtualPathTokenCore()
		{
			if (this._token == IntPtr.Zero && this._ecb != IntPtr.Zero)
			{
				this._token = UnsafeNativeMethods.PMGetVirtualPathToken(this._ecb);
			}
			return this._token;
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x0014BB59 File Offset: 0x00149D59
		internal override int AppendLogParameterCore(string logParam)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMAppendLogParameter(this._ecb, logParam);
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x0014BB7B File Offset: 0x00149D7B
		internal override int GetClientCertificateCore(byte[] buffer, int[] pInts, long[] pDates)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMGetClientCertificate(this._ecb, buffer, buffer.Length, pInts, pDates);
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x0014BBA2 File Offset: 0x00149DA2
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

		// Token: 0x06006002 RID: 24578 RVA: 0x0014BBD7 File Offset: 0x00149DD7
		internal override int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.PMCallISAPI(this._ecb, iFunction, bufIn, bufIn.Length, bufOut, bufOut.Length);
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x0014BC01 File Offset: 0x00149E01
		internal override void SendEmptyResponse()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			UnsafeNativeMethods.PMEmptyResponse(this._ecb);
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x0014BC24 File Offset: 0x00149E24
		internal override DateTime GetStartTime()
		{
			if (this._ecb == IntPtr.Zero || this._useBaseTime)
			{
				return base.GetStartTime();
			}
			long filetime = UnsafeNativeMethods.PMGetStartTimeStamp(this._ecb);
			return DateTimeUtil.FromFileTimeToUtc(filetime);
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x0014BC64 File Offset: 0x00149E64
		internal override void ResetStartTime()
		{
			base.ResetStartTime();
			this._useBaseTime = true;
		}

		// Token: 0x04003231 RID: 12849
		private const int PM_FLUSH_THRESHOLD = 31744;

		// Token: 0x04003232 RID: 12850
		private bool _useBaseTime;

		// Token: 0x04003233 RID: 12851
		private const int _numServerVars = 32;

		// Token: 0x04003234 RID: 12852
		private IDictionary _serverVars;

		// Token: 0x04003235 RID: 12853
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
