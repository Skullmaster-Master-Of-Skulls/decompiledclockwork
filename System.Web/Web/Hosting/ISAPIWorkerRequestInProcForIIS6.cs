using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002A8 RID: 680
	internal class ISAPIWorkerRequestInProcForIIS6 : ISAPIWorkerRequestInProc
	{
		// Token: 0x060023A1 RID: 9121 RVA: 0x00098D7A File Offset: 0x00097D7A
		internal ISAPIWorkerRequestInProcForIIS6(IntPtr ecb) : base(ecb)
		{
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00098D83 File Offset: 0x00097D83
		internal static void WaitForPendingAsyncIo()
		{
			while (ISAPIWorkerRequestInProcForIIS6._asyncIoCount != 0)
			{
				Thread.Sleep(250);
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00098D98 File Offset: 0x00097D98
		internal override void SendEmptyResponse()
		{
			UnsafeNativeMethods.UpdateLastActivityTimeForHealthMonitor();
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00098DA0 File Offset: 0x00097DA0
		internal override void ReadRequestBasics()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			this.GetBasicServerVariables();
			int num = this._path.Length - this._filePath.Length;
			if (num > 0)
			{
				this._pathInfo = this._path.Substring(this._filePath.Length);
				int num2 = this._pathTranslated.Length - num;
				if (num2 > 0)
				{
					this._pathTranslated = this._pathTranslated.Substring(0, num2);
				}
			}
			else
			{
				this._filePath = this._path;
				this._pathInfo = string.Empty;
			}
			this._appPath = HostingEnvironment.ApplicationVirtualPath;
			int[] array = null;
			try
			{
				array = RecyclableArrayHelper.GetIntegerArray(4);
				UnsafeNativeMethods.EcbGetBasicsContentInfo(this._ecb, array);
				this._contentType = array[0];
				this._contentTotalLength = array[1];
				this._contentAvailLength = array[2];
				this._queryStringLength = array[3];
			}
			finally
			{
				RecyclableArrayHelper.ReuseIntegerArray(array);
			}
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00098E9C File Offset: 0x00097E9C
		private void GetBasicServerVariables()
		{
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			if (this._basicServerVars != null)
			{
				return;
			}
			this._basicServerVars = new string[12];
			ServerVarCharBuffer serverVarCharBuffer = new ServerVarCharBuffer();
			try
			{
				int[] array = new int[12];
				int num = 0;
				int hresult = UnsafeNativeMethods.EcbGetUnicodeServerVariables(this._ecb, serverVarCharBuffer.PinnedAddress, serverVarCharBuffer.Length, array, array.Length, 0, ref num);
				if (num > serverVarCharBuffer.Length)
				{
					serverVarCharBuffer.Resize(num);
					hresult = UnsafeNativeMethods.EcbGetUnicodeServerVariables(this._ecb, serverVarCharBuffer.PinnedAddress, serverVarCharBuffer.Length, array, array.Length, 0, ref num);
				}
				Misc.ThrowIfFailedHr(hresult);
				IntPtr pinnedAddress = serverVarCharBuffer.PinnedAddress;
				for (int i = 0; i < this._basicServerVars.Length; i++)
				{
					this._basicServerVars[i] = Marshal.PtrToStringUni(pinnedAddress, array[i]);
					pinnedAddress = new IntPtr((long)pinnedAddress + 2L * (1L + (long)array[i]));
				}
				this._appPathTranslated = this._basicServerVars[2];
				this._method = this._basicServerVars[3];
				this._path = this._basicServerVars[4];
				this._pathTranslated = this._basicServerVars[5];
				this._filePath = this._basicServerVars[6];
			}
			finally
			{
				serverVarCharBuffer.Dispose();
			}
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00098FE4 File Offset: 0x00097FE4
		protected override void GetAdditionalServerVariables()
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
			ServerVarCharBuffer serverVarCharBuffer = new ServerVarCharBuffer();
			try
			{
				int[] array = new int[23];
				int num = 0;
				int num2 = UnsafeNativeMethods.EcbGetUnicodeServerVariables(this._ecb, serverVarCharBuffer.PinnedAddress, serverVarCharBuffer.Length, array, array.Length, 12, ref num);
				if (num > serverVarCharBuffer.Length)
				{
					serverVarCharBuffer.Resize(num);
					num2 = UnsafeNativeMethods.EcbGetUnicodeServerVariables(this._ecb, serverVarCharBuffer.PinnedAddress, serverVarCharBuffer.Length, array, array.Length, 12, ref num);
				}
				if (num2 != 0)
				{
					Marshal.ThrowExceptionForHR(num2);
				}
				IntPtr pinnedAddress = serverVarCharBuffer.PinnedAddress;
				for (int i = 0; i < this._additionalServerVars.Length; i++)
				{
					this._additionalServerVars[i] = Marshal.PtrToStringUni(pinnedAddress, array[i]);
					pinnedAddress = new IntPtr((long)pinnedAddress + 2L * (1L + (long)array[i]));
				}
			}
			finally
			{
				serverVarCharBuffer.Dispose();
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000990EC File Offset: 0x000980EC
		protected override string GetServerVariableCore(string name)
		{
			if (StringUtil.StringStartsWith(name, "HTTP_"))
			{
				return base.GetServerVariableCore(name);
			}
			return this.GetUnicodeServerVariable("UNICODE_" + name);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00099114 File Offset: 0x00098114
		protected internal string GetUnicodeServerVariable(string name)
		{
			string result = null;
			ServerVarCharBuffer serverVarCharBuffer = new ServerVarCharBuffer();
			try
			{
				result = this.GetUnicodeServerVariable(name, serverVarCharBuffer);
			}
			finally
			{
				serverVarCharBuffer.Dispose();
			}
			return result;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x0009914C File Offset: 0x0009814C
		protected internal string GetUnicodeServerVariable(int nameIndex)
		{
			string result = null;
			ServerVarCharBuffer serverVarCharBuffer = new ServerVarCharBuffer();
			try
			{
				result = this.GetUnicodeServerVariable(nameIndex, serverVarCharBuffer);
			}
			finally
			{
				serverVarCharBuffer.Dispose();
			}
			return result;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00099184 File Offset: 0x00098184
		private string GetUnicodeServerVariable(string name, ServerVarCharBuffer buffer)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return null;
			}
			int num = UnsafeNativeMethods.EcbGetUnicodeServerVariable(this._ecb, name, buffer.PinnedAddress, buffer.Length);
			if (num < 0)
			{
				buffer.Resize(-num);
				num = UnsafeNativeMethods.EcbGetUnicodeServerVariable(this._ecb, name, buffer.PinnedAddress, buffer.Length);
			}
			if (num > 0)
			{
				return Marshal.PtrToStringUni(buffer.PinnedAddress, num);
			}
			return null;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000991F8 File Offset: 0x000981F8
		private string GetUnicodeServerVariable(int nameIndex, ServerVarCharBuffer buffer)
		{
			if (this._ecb == IntPtr.Zero)
			{
				return null;
			}
			int num = UnsafeNativeMethods.EcbGetUnicodeServerVariableByIndex(this._ecb, nameIndex, buffer.PinnedAddress, buffer.Length);
			if (num < 0)
			{
				buffer.Resize(-num);
				num = UnsafeNativeMethods.EcbGetUnicodeServerVariableByIndex(this._ecb, nameIndex, buffer.PinnedAddress, buffer.Length);
			}
			if (num > 0)
			{
				return Marshal.PtrToStringUni(buffer.PinnedAddress, num);
			}
			return null;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00099269 File Offset: 0x00098269
		internal override MemoryBytes PackageFile(string filename, long offset, long size, bool isImpersonating)
		{
			return new MemoryBytes(filename, offset, size);
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00099273 File Offset: 0x00098273
		internal override bool SupportsLongTransmitFile
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00099278 File Offset: 0x00098278
		internal override void FlushCore(byte[] status, byte[] header, int keepConnected, int totalBodySize, int numBodyFragments, IntPtr[] bodyFragments, int[] bodyFragmentLengths, int doneWithSession, int finalStatus, out bool async)
		{
			async = false;
			if (this._ecb == IntPtr.Zero)
			{
				return;
			}
			if (this._headersSentFromExecuteUrl)
			{
				status = null;
				header = null;
			}
			if (doneWithSession != 0 && !HttpRuntime.ShutdownInProgress && (this._ignoreMinAsyncSize || totalBodySize >= 2048))
			{
				if (this._requiresAsyncFlushCallback)
				{
					this._asyncFlushCompletionCallback = new ISAPIAsyncCompletionCallback(this.OnAsyncFlushCompletion);
					this._asyncFinalStatus = finalStatus;
					this._rootedThis = GCHandle.Alloc(this);
					doneWithSession = 0;
					async = true;
					Interlocked.Increment(ref ISAPIWorkerRequestInProcForIIS6._asyncIoCount);
				}
				else
				{
					this._asyncFlushCompletionCallback = null;
					doneWithSession = 0;
					async = true;
				}
			}
			int finalStatus2 = this._trySkipIisCustomErrors ? (finalStatus | 64) : finalStatus;
			int num = UnsafeNativeMethods.EcbFlushCore(this._ecb, status, header, keepConnected, totalBodySize, numBodyFragments, bodyFragments, bodyFragmentLengths, doneWithSession, finalStatus2, this._cacheInKernelMode ? 1 : 0, async ? 1 : 0, this._asyncFlushCompletionCallback);
			if (!this._requiresAsyncFlushCallback && num == 0 && async)
			{
				base.UnlockCachedResponseBytesOnceAfterIoComplete();
				base.CallEndOfRequestCallbackOnceAfterAllIoComplete();
				return;
			}
			if (num != 0 && async)
			{
				async = false;
				UnsafeNativeMethods.EcbFlushCore(this._ecb, null, null, 0, 0, 0, null, null, 1, this._asyncFinalStatus, 0, 0, null);
				if (this._asyncFlushCompletionCallback != null)
				{
					this._rootedThis.Free();
					Interlocked.Decrement(ref ISAPIWorkerRequestInProcForIIS6._asyncIoCount);
					return;
				}
			}
			else if (num != 0 && !async && doneWithSession == 0 && !this._serverSupportFunctionError)
			{
				this._serverSupportFunctionError = true;
				string name = "Server_Support_Function_Error";
				if (num == -2147014843 || num == -2147014842)
				{
					name = "Server_Support_Function_Error_Disconnect";
					PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_DISCONNECTED);
				}
				throw new HttpException(SR.GetString(name, new object[]
				{
					num.ToString("X8", CultureInfo.InvariantCulture)
				}), num);
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00099428 File Offset: 0x00098428
		private void OnAsyncFlushCompletion(IntPtr ecb, int byteCount, int error)
		{
			try
			{
				this._rootedThis.Free();
				UnsafeNativeMethods.EcbFlushCore(ecb, null, null, 0, 0, 0, null, null, 1, this._asyncFinalStatus, 0, 0, null);
				base.UnlockCachedResponseBytesOnceAfterIoComplete();
				UnsafeNativeMethods.RevertToSelf();
				base.CallEndOfRequestCallbackOnceAfterAllIoComplete();
			}
			finally
			{
				Interlocked.Decrement(ref ISAPIWorkerRequestInProcForIIS6._asyncIoCount);
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00099488 File Offset: 0x00098488
		internal override string SetupKernelCaching(int secondsToLive, string originalCacheUrl, bool enableKernelCacheForVaryByStar)
		{
			if (this._ecb == IntPtr.Zero || this._disableKernelCache)
			{
				return null;
			}
			string unicodeServerVariable = this.GetUnicodeServerVariable(7);
			if (originalCacheUrl != null && originalCacheUrl != unicodeServerVariable)
			{
				return null;
			}
			if (string.IsNullOrEmpty(unicodeServerVariable) || (!enableKernelCacheForVaryByStar && unicodeServerVariable.IndexOf('?') != -1))
			{
				return null;
			}
			this._cacheInKernelMode = true;
			return unicodeServerVariable;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000994E7 File Offset: 0x000984E7
		internal override void DisableKernelCache()
		{
			this._disableKernelCache = true;
			this._cacheInKernelMode = false;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000994F7 File Offset: 0x000984F7
		internal override bool SupportsExecuteUrl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000994FC File Offset: 0x000984FC
		internal override IAsyncResult BeginExecuteUrl(string url, string method, string childHeaders, bool sendHeaders, bool addUserIndo, IntPtr token, string name, string authType, byte[] entity, AsyncCallback cb, object state)
		{
			if (this._ecb == IntPtr.Zero || this._asyncResultOfExecuteUrl != null || (sendHeaders && this.HeadersSent()))
			{
				throw new InvalidOperationException(SR.GetString("Cannot_execute_url_in_this_context"));
			}
			if (entity != null && entity.Length > 0)
			{
				int num = UnsafeNativeMethods.EcbGetExecUrlEntityInfo(entity.Length, entity, out this._entity);
				if (num != 1)
				{
					throw new HttpException(SR.GetString("Failed_to_execute_url"));
				}
			}
			HttpAsyncResult httpAsyncResult = new HttpAsyncResult(cb, state);
			this._asyncResultOfExecuteUrl = httpAsyncResult;
			this._executeUrlCompletionCallback = new ISAPIAsyncCompletionCallback(this.OnExecuteUrlCompletion);
			this._rootedThis = GCHandle.Alloc(this);
			int num2 = UnsafeNativeMethods.EcbExecuteUrlUnicode(this._ecb, url, method, childHeaders, sendHeaders, addUserIndo, token, name, authType, this._entity, this._executeUrlCompletionCallback);
			if (num2 != 1)
			{
				if (this._entity != IntPtr.Zero)
				{
					UnsafeNativeMethods.EcbFreeExecUrlEntityInfo(this._entity);
				}
				this._rootedThis.Free();
				this._asyncResultOfExecuteUrl = null;
				throw new HttpException(SR.GetString("Failed_to_execute_url"));
			}
			if (sendHeaders)
			{
				this._headersSentFromExecuteUrl = true;
			}
			return httpAsyncResult;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00099614 File Offset: 0x00098614
		internal override void EndExecuteUrl(IAsyncResult result)
		{
			HttpAsyncResult httpAsyncResult = result as HttpAsyncResult;
			if (httpAsyncResult != null)
			{
				httpAsyncResult.End();
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00099634 File Offset: 0x00098634
		private void OnExecuteUrlCompletion(IntPtr ecb, int byteCount, int error)
		{
			if (this._entity != IntPtr.Zero)
			{
				UnsafeNativeMethods.EcbFreeExecUrlEntityInfo(this._entity);
			}
			this._rootedThis.Free();
			HttpAsyncResult asyncResultOfExecuteUrl = this._asyncResultOfExecuteUrl;
			this._asyncResultOfExecuteUrl = null;
			asyncResultOfExecuteUrl.Complete(false, null, null);
		}

		// Token: 0x04001C08 RID: 7176
		private const int MIN_ASYNC_SIZE = 2048;

		// Token: 0x04001C09 RID: 7177
		private const int TRY_SKIP_IIS_CUSTOM_ERRORS = 64;

		// Token: 0x04001C0A RID: 7178
		private static int _asyncIoCount;

		// Token: 0x04001C0B RID: 7179
		private GCHandle _rootedThis;

		// Token: 0x04001C0C RID: 7180
		private ISAPIAsyncCompletionCallback _asyncFlushCompletionCallback;

		// Token: 0x04001C0D RID: 7181
		private int _asyncFinalStatus;

		// Token: 0x04001C0E RID: 7182
		private bool _serverSupportFunctionError;

		// Token: 0x04001C0F RID: 7183
		private IntPtr _entity;

		// Token: 0x04001C10 RID: 7184
		private bool _cacheInKernelMode;

		// Token: 0x04001C11 RID: 7185
		private bool _disableKernelCache;

		// Token: 0x04001C12 RID: 7186
		protected bool _trySkipIisCustomErrors;

		// Token: 0x04001C13 RID: 7187
		private ISAPIAsyncCompletionCallback _executeUrlCompletionCallback;

		// Token: 0x04001C14 RID: 7188
		private HttpAsyncResult _asyncResultOfExecuteUrl;

		// Token: 0x04001C15 RID: 7189
		private bool _headersSentFromExecuteUrl;
	}
}
