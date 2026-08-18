using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007CD RID: 1997
	internal class ISAPIWorkerRequestInProcForIIS6 : ISAPIWorkerRequestInProc
	{
		// Token: 0x06005FD3 RID: 24531 RVA: 0x0014AA4F File Offset: 0x00148C4F
		internal ISAPIWorkerRequestInProcForIIS6(IntPtr ecb) : base(ecb)
		{
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x0014AA58 File Offset: 0x00148C58
		internal static void WaitForPendingAsyncIo()
		{
			while (ISAPIWorkerRequestInProcForIIS6._asyncIoCount != 0)
			{
				Thread.Sleep(250);
			}
		}

		// Token: 0x06005FD5 RID: 24533 RVA: 0x0014AA6D File Offset: 0x00148C6D
		internal override void SendEmptyResponse()
		{
			UnsafeNativeMethods.UpdateLastActivityTimeForHealthMonitor();
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x0014AA74 File Offset: 0x00148C74
		public override string GetRawUrl()
		{
			return HttpWorkerRequest.GetRawUrlHelper(this.GetUnicodeServerVariable(7));
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x0014AA90 File Offset: 0x00148C90
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

		// Token: 0x06005FD8 RID: 24536 RVA: 0x0014AB8C File Offset: 0x00148D8C
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

		// Token: 0x06005FD9 RID: 24537 RVA: 0x0014ACD4 File Offset: 0x00148ED4
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

		// Token: 0x06005FDA RID: 24538 RVA: 0x0014ADDC File Offset: 0x00148FDC
		protected override string GetServerVariableCore(string name)
		{
			if (StringUtil.StringStartsWith(name, "HTTP_"))
			{
				return base.GetServerVariableCore(name);
			}
			return this.GetUnicodeServerVariable("UNICODE_" + name);
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x0014AE04 File Offset: 0x00149004
		private string GetUnicodeServerVariable(string name)
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

		// Token: 0x06005FDC RID: 24540 RVA: 0x0014AE3C File Offset: 0x0014903C
		private string GetUnicodeServerVariable(int nameIndex)
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

		// Token: 0x06005FDD RID: 24541 RVA: 0x0014AE74 File Offset: 0x00149074
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

		// Token: 0x06005FDE RID: 24542 RVA: 0x0014AEE8 File Offset: 0x001490E8
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

		// Token: 0x06005FDF RID: 24543 RVA: 0x0014AF59 File Offset: 0x00149159
		internal override MemoryBytes PackageFile(string filename, long offset, long size, bool isImpersonating)
		{
			return new MemoryBytes(filename, offset, size);
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06005FE0 RID: 24544 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool SupportsLongTransmitFile
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x0014AF64 File Offset: 0x00149164
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
			bool flag = false;
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
			else
			{
				flag = (this._asyncResultBase is FlushAsyncResult);
				if (flag)
				{
					this._requiresAsyncFlushCallback = true;
					this._asyncFlushCompletionCallback = new ISAPIAsyncCompletionCallback(this.OnAsyncFlushCompletion);
					this._asyncFinalStatus = finalStatus;
					this._rootedThis = GCHandle.Alloc(this);
					async = true;
					Interlocked.Increment(ref ISAPIWorkerRequestInProcForIIS6._asyncIoCount);
				}
			}
			int finalStatus2 = this._trySkipIisCustomErrors ? (finalStatus | 64) : finalStatus;
			int num = UnsafeNativeMethods.EcbFlushCore(this._ecb, status, header, keepConnected, totalBodySize, numBodyFragments, bodyFragments, bodyFragmentLengths, doneWithSession, finalStatus2, this._cacheInKernelMode ? 1 : 0, async ? 1 : 0, this._asyncFlushCompletionCallback);
			if ((!this._requiresAsyncFlushCallback && num == 0) & async)
			{
				base.UnlockCachedResponseBytesOnceAfterIoComplete();
				base.CallEndOfRequestCallbackOnceAfterAllIoComplete();
				return;
			}
			if (num != 0 & async)
			{
				async = false;
				if (!flag)
				{
					UnsafeNativeMethods.EcbFlushCore(this._ecb, null, null, 0, 0, 0, null, null, 1, this._asyncFinalStatus, 0, 0, null);
				}
				if (this._asyncFlushCompletionCallback != null)
				{
					this._rootedThis.Free();
					Interlocked.Decrement(ref ISAPIWorkerRequestInProcForIIS6._asyncIoCount);
				}
				if (flag)
				{
					this._asyncResultBase = null;
					this.IncrementRequestsDisconnected();
					throw new HttpException(SR.GetString("ClientDisconnected"), num);
				}
			}
			else if (num != 0 && !async && doneWithSession == 0 && !this._serverSupportFunctionError)
			{
				this._serverSupportFunctionError = true;
				string name = "Server_Support_Function_Error";
				if (num == -2147014843 || num == -2147014842)
				{
					name = "Server_Support_Function_Error_Disconnect";
					this.IncrementRequestsDisconnected();
				}
				throw new HttpException(SR.GetString(name, new object[]
				{
					num.ToString("X8", CultureInfo.InvariantCulture)
				}), num);
			}
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x0014B194 File Offset: 0x00149394
		private void OnAsyncFlushCompletion(IntPtr ecb, int byteCount, int error)
		{
			try
			{
				FlushAsyncResult flushAsyncResult = this._asyncResultBase as FlushAsyncResult;
				this._rootedThis.Free();
				if (flushAsyncResult == null)
				{
					UnsafeNativeMethods.EcbFlushCore(ecb, null, null, 0, 0, 0, null, null, 1, this._asyncFinalStatus, 0, 0, null);
				}
				else
				{
					flushAsyncResult.HResult = error;
				}
				base.UnlockCachedResponseBytesOnceAfterIoComplete();
				UnsafeNativeMethods.RevertToSelf();
				if (flushAsyncResult == null)
				{
					base.CallEndOfRequestCallbackOnceAfterAllIoComplete();
				}
			}
			finally
			{
				Interlocked.Decrement(ref ISAPIWorkerRequestInProcForIIS6._asyncIoCount);
			}
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x0014B210 File Offset: 0x00149410
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

		// Token: 0x06005FE4 RID: 24548 RVA: 0x0014B26F File Offset: 0x0014946F
		internal override void DisableKernelCache()
		{
			this._disableKernelCache = true;
			this._cacheInKernelMode = false;
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06005FE5 RID: 24549 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsAsyncFlush
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x0014B280 File Offset: 0x00149480
		public override IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			if (this._ecb == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			FlushAsyncResult flushAsyncResult = new FlushAsyncResult(callback, state);
			if (Interlocked.CompareExchange<AsyncResultBase>(ref this._asyncResultBase, flushAsyncResult, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Async_operation_pending"));
			}
			if (this._asyncCompletionCallback == null)
			{
				this._asyncCompletionCallback = new AsyncCompletionCallback(this.OnAsyncCompletion);
			}
			try
			{
				flushAsyncResult.MarkCallToBeginMethodStarted();
				this.FlushResponse(false);
			}
			finally
			{
				flushAsyncResult.MarkCallToBeginMethodCompleted();
			}
			return flushAsyncResult;
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x0014B310 File Offset: 0x00149510
		public override void EndFlush(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			FlushAsyncResult flushAsyncResult = asyncResult as FlushAsyncResult;
			if (flushAsyncResult == null)
			{
				throw new ArgumentException(null, "asyncResult");
			}
			flushAsyncResult.ReleaseWaitHandleWhenSignaled();
			if (flushAsyncResult.HResult < 0)
			{
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), flushAsyncResult.HResult);
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06005FE8 RID: 24552 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsAsyncRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x0014B36C File Offset: 0x0014956C
		internal void OnAsyncCompletion(int bytesCompleted, int hresult, IntPtr pAsyncCompletionContext)
		{
			if (this._asyncResultBase is ReadAsyncResult)
			{
				this._rootedThis.Free();
			}
			AsyncResultBase asyncResultBase = this._asyncResultBase;
			this._asyncResultBase = null;
			asyncResultBase.Complete(bytesCompleted, hresult, pAsyncCompletionContext, false);
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x0014B3AC File Offset: 0x001495AC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("InvalidOffsetOrCount", new object[]
				{
					"offset",
					"count"
				}));
			}
			if (this._ecb == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			ReadAsyncResult readAsyncResult = new ReadAsyncResult(callback, state, buffer, offset, count, false);
			if (count == 0)
			{
				readAsyncResult.Complete(0, 0, IntPtr.Zero, true);
				return readAsyncResult;
			}
			if (Interlocked.CompareExchange<AsyncResultBase>(ref this._asyncResultBase, readAsyncResult, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Async_operation_pending"));
			}
			if (this._asyncCompletionCallback == null)
			{
				this._asyncCompletionCallback = new AsyncCompletionCallback(this.OnAsyncCompletion);
			}
			this._rootedThis = GCHandle.Alloc(this);
			int num;
			try
			{
				readAsyncResult.MarkCallToBeginMethodStarted();
				num = UnsafeNativeMethods.EcbReadClientAsync(this._ecb, count, this._asyncCompletionCallback);
			}
			finally
			{
				readAsyncResult.MarkCallToBeginMethodCompleted();
			}
			if (num < 0)
			{
				this._rootedThis.Free();
				this._asyncResultBase = null;
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), num);
			}
			return readAsyncResult;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x0014B4F4 File Offset: 0x001496F4
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ReadAsyncResult readAsyncResult = asyncResult as ReadAsyncResult;
			if (readAsyncResult == null)
			{
				throw new ArgumentException(null, "asyncResult");
			}
			readAsyncResult.ReleaseWaitHandleWhenSignaled();
			if (readAsyncResult.HResult < 0)
			{
				this.IncrementRequestsDisconnected();
				throw new HttpException(SR.GetString("ClientDisconnected"), readAsyncResult.HResult);
			}
			return readAsyncResult.BytesRead;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x0014B556 File Offset: 0x00149756
		private void IncrementRequestsDisconnected()
		{
			if (!this._disconnected)
			{
				PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_DISCONNECTED);
				this._disconnected = true;
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool SupportsExecuteUrl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x0014B570 File Offset: 0x00149770
		internal override IAsyncResult BeginExecuteUrl(string url, string method, string childHeaders, bool sendHeaders, bool addUserIndo, IntPtr token, string name, string authType, byte[] entity, AsyncCallback cb, object state)
		{
			if (this._ecb == IntPtr.Zero || this._asyncResultOfExecuteUrl != null || (sendHeaders && this.HeadersSent()))
			{
				throw new InvalidOperationException(SR.GetString("Cannot_execute_url_in_this_context"));
			}
			if (entity != null && entity.Length != 0)
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
			int num2;
			try
			{
				httpAsyncResult.MarkCallToBeginMethodStarted();
				num2 = UnsafeNativeMethods.EcbExecuteUrlUnicode(this._ecb, url, method, childHeaders, sendHeaders, addUserIndo, token, name, authType, this._entity, this._executeUrlCompletionCallback);
			}
			finally
			{
				httpAsyncResult.MarkCallToBeginMethodCompleted();
			}
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

		// Token: 0x06005FEF RID: 24559 RVA: 0x0014B6A4 File Offset: 0x001498A4
		internal override void EndExecuteUrl(IAsyncResult result)
		{
			HttpAsyncResult httpAsyncResult = result as HttpAsyncResult;
			if (httpAsyncResult != null)
			{
				httpAsyncResult.End();
			}
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x0014B6C4 File Offset: 0x001498C4
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

		// Token: 0x04003222 RID: 12834
		private static int _asyncIoCount;

		// Token: 0x04003223 RID: 12835
		private bool _disconnected;

		// Token: 0x04003224 RID: 12836
		private const int MIN_ASYNC_SIZE = 2048;

		// Token: 0x04003225 RID: 12837
		private GCHandle _rootedThis;

		// Token: 0x04003226 RID: 12838
		private ISAPIAsyncCompletionCallback _asyncFlushCompletionCallback;

		// Token: 0x04003227 RID: 12839
		private int _asyncFinalStatus;

		// Token: 0x04003228 RID: 12840
		private bool _serverSupportFunctionError;

		// Token: 0x04003229 RID: 12841
		private IntPtr _entity;

		// Token: 0x0400322A RID: 12842
		private bool _cacheInKernelMode;

		// Token: 0x0400322B RID: 12843
		private bool _disableKernelCache;

		// Token: 0x0400322C RID: 12844
		protected bool _trySkipIisCustomErrors;

		// Token: 0x0400322D RID: 12845
		private const int TRY_SKIP_IIS_CUSTOM_ERRORS = 64;

		// Token: 0x0400322E RID: 12846
		private ISAPIAsyncCompletionCallback _executeUrlCompletionCallback;

		// Token: 0x0400322F RID: 12847
		private HttpAsyncResult _asyncResultOfExecuteUrl;

		// Token: 0x04003230 RID: 12848
		private bool _headersSentFromExecuteUrl;
	}
}
