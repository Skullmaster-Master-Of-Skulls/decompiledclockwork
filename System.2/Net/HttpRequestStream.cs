using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001BE RID: 446
	internal class HttpRequestStream : Stream
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x0005EDD7 File Offset: 0x0005CFD7
		internal HttpRequestStream(HttpListenerContext httpContext)
		{
			this.m_HttpContext = httpContext;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0005EDE6 File Offset: 0x0005CFE6
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0005EDE9 File Offset: 0x0005CFE9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0005EDEC File Offset: 0x0005CFEC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0005EDEF File Offset: 0x0005CFEF
		internal bool Closed
		{
			get
			{
				return this.m_Closed;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0005EDF7 File Offset: 0x0005CFF7
		internal bool BufferedDataChunksAvailable
		{
			get
			{
				return this.m_DataChunkIndex > -1;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0005EE02 File Offset: 0x0005D002
		internal HttpListenerContext InternalHttpContext
		{
			get
			{
				return this.m_HttpContext;
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0005EE0A File Offset: 0x0005D00A
		public override void Flush()
		{
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0005EE0C File Offset: 0x0005D00C
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0005EE13 File Offset: 0x0005D013
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0005EE24 File Offset: 0x0005D024
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x0005EE35 File Offset: 0x0005D035
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0005EE46 File Offset: 0x0005D046
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0005EE57 File Offset: 0x0005D057
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0005EE68 File Offset: 0x0005D068
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Read", "");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size == 0 || this.m_Closed)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Read", "dataRead:0");
				}
				return 0;
			}
			uint num = 0U;
			if (this.m_DataChunkIndex != -1)
			{
				num = UnsafeNclNativeMethods.HttpApi.GetChunks(this.m_HttpContext.Request.RequestBuffer, this.m_HttpContext.Request.OriginalBlobAddress, ref this.m_DataChunkIndex, ref this.m_DataChunkOffset, buffer, offset, size);
			}
			if (this.m_DataChunkIndex == -1 && (ulong)num < (ulong)((long)size))
			{
				uint num2 = 0U;
				offset += (int)num;
				size -= (int)num;
				if (size > 131072)
				{
					size = 131072;
				}
				uint num3;
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					uint flags = 0U;
					if (!this.m_InOpaqueMode)
					{
						flags = 1U;
					}
					num3 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, flags, (void*)(ptr + offset), (uint)size, out num2, null);
					num += num2;
				}
				if (num3 != 0U && num3 != 38U)
				{
					Exception ex = new HttpListenerException((int)num3);
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "Read", ex);
					}
					throw ex;
				}
				this.UpdateAfterRead(num3, num);
			}
			if (Logging.On)
			{
				Logging.Dump(Logging.HttpListener, this, "Read", buffer, offset, (int)num);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Read", "dataRead:" + num.ToString());
			}
			return (int)num;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0005F033 File Offset: 0x0005D233
		private void UpdateAfterRead(uint statusCode, uint dataRead)
		{
			if (statusCode == 38U || dataRead == 0U)
			{
				this.Close();
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0005F044 File Offset: 0x0005D244
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public unsafe override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "BeginRead", "");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size == 0 || this.m_Closed)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "BeginRead", "");
				}
				HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback);
				httpRequestStreamAsyncResult.InvokeCallback(0U);
				return httpRequestStreamAsyncResult;
			}
			HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult2 = null;
			uint num = 0U;
			if (this.m_DataChunkIndex != -1)
			{
				num = UnsafeNclNativeMethods.HttpApi.GetChunks(this.m_HttpContext.Request.RequestBuffer, this.m_HttpContext.Request.OriginalBlobAddress, ref this.m_DataChunkIndex, ref this.m_DataChunkOffset, buffer, offset, size);
				if (this.m_DataChunkIndex != -1 && (ulong)num == (ulong)((long)size))
				{
					httpRequestStreamAsyncResult2 = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback, buffer, offset, (uint)size, 0U);
					httpRequestStreamAsyncResult2.InvokeCallback(num);
				}
			}
			if (this.m_DataChunkIndex == -1 && (ulong)num < (ulong)((long)size))
			{
				uint num2 = 0U;
				offset += (int)num;
				size -= (int)num;
				if (size > 131072)
				{
					size = 131072;
				}
				httpRequestStreamAsyncResult2 = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback, buffer, offset, (uint)size, num);
				uint numBytes;
				try
				{
					try
					{
						fixed (byte[] array = buffer)
						{
							if (buffer == null || array.Length == 0)
							{
								byte* ptr = null;
							}
							else
							{
								byte* ptr = &array[0];
							}
							this.m_HttpContext.EnsureBoundHandle();
							uint flags = 0U;
							if (!this.m_InOpaqueMode)
							{
								flags = 1U;
							}
							num2 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, flags, httpRequestStreamAsyncResult2.m_pPinnedBuffer, (uint)size, out numBytes, httpRequestStreamAsyncResult2.m_pOverlapped);
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
				catch (Exception e)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "BeginRead", e);
					}
					httpRequestStreamAsyncResult2.InternalCleanup();
					throw;
				}
				if (num2 != 0U && num2 != 997U)
				{
					httpRequestStreamAsyncResult2.InternalCleanup();
					if (num2 != 38U)
					{
						Exception ex = new HttpListenerException((int)num2);
						if (Logging.On)
						{
							Logging.Exception(Logging.HttpListener, this, "BeginRead", ex);
						}
						httpRequestStreamAsyncResult2.InternalCleanup();
						throw ex;
					}
					httpRequestStreamAsyncResult2 = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback, num);
					httpRequestStreamAsyncResult2.InvokeCallback(0U);
				}
				else if (num2 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					httpRequestStreamAsyncResult2.IOCompleted(num2, numBytes);
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "BeginRead", "");
			}
			return httpRequestStreamAsyncResult2;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0005F2C8 File Offset: 0x0005D4C8
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "EndRead", "");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult = asyncResult as HttpRequestStream.HttpRequestStreamAsyncResult;
			if (httpRequestStreamAsyncResult == null || httpRequestStreamAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (httpRequestStreamAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndRead"
				}));
			}
			httpRequestStreamAsyncResult.EndCalled = true;
			object obj = httpRequestStreamAsyncResult.InternalWaitForCompletion();
			Exception ex = obj as Exception;
			if (ex != null)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "EndRead", ex);
				}
				throw ex;
			}
			uint num = (uint)obj;
			this.UpdateAfterRead((uint)httpRequestStreamAsyncResult.ErrorCode, num);
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "EndRead", "");
			}
			return (int)(num + httpRequestStreamAsyncResult.m_dataAlreadyRead);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0005F3B7 File Offset: 0x0005D5B7
		public override void Write(byte[] buffer, int offset, int size)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0005F3C8 File Offset: 0x0005D5C8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0005F3D9 File Offset: 0x0005D5D9
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0005F3EC File Offset: 0x0005D5EC
		protected override void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Dispose", "");
			}
			try
			{
				this.m_Closed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Dispose", "");
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0005F454 File Offset: 0x0005D654
		internal void SwitchToOpaqueMode()
		{
			this.m_InOpaqueMode = true;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0005F45D File Offset: 0x0005D65D
		internal uint GetChunks(byte[] buffer, int offset, int size)
		{
			return UnsafeNclNativeMethods.HttpApi.GetChunks(this.m_HttpContext.Request.RequestBuffer, this.m_HttpContext.Request.OriginalBlobAddress, ref this.m_DataChunkIndex, ref this.m_DataChunkOffset, buffer, offset, size);
		}

		// Token: 0x0400145E RID: 5214
		private HttpListenerContext m_HttpContext;

		// Token: 0x0400145F RID: 5215
		private uint m_DataChunkOffset;

		// Token: 0x04001460 RID: 5216
		private int m_DataChunkIndex;

		// Token: 0x04001461 RID: 5217
		private bool m_Closed;

		// Token: 0x04001462 RID: 5218
		internal const int MaxReadSize = 131072;

		// Token: 0x04001463 RID: 5219
		private bool m_InOpaqueMode;

		// Token: 0x02000751 RID: 1873
		private class HttpRequestStreamAsyncResult : LazyAsyncResult
		{
			// Token: 0x060041F9 RID: 16889 RVA: 0x001123DC File Offset: 0x001105DC
			internal HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback) : base(asyncObject, userState, callback)
			{
			}

			// Token: 0x060041FA RID: 16890 RVA: 0x001123E7 File Offset: 0x001105E7
			internal HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, uint dataAlreadyRead) : base(asyncObject, userState, callback)
			{
				this.m_dataAlreadyRead = dataAlreadyRead;
			}

			// Token: 0x060041FB RID: 16891 RVA: 0x001123FC File Offset: 0x001105FC
			internal unsafe HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, byte[] buffer, int offset, uint size, uint dataAlreadyRead) : base(asyncObject, userState, callback)
			{
				this.m_dataAlreadyRead = dataAlreadyRead;
				this.m_pOverlapped = new Overlapped
				{
					AsyncResult = this
				}.Pack(HttpRequestStream.HttpRequestStreamAsyncResult.s_IOCallback, buffer);
				this.m_pPinnedBuffer = (void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
			}

			// Token: 0x060041FC RID: 16892 RVA: 0x0011244E File Offset: 0x0011064E
			internal void IOCompleted(uint errorCode, uint numBytes)
			{
				HttpRequestStream.HttpRequestStreamAsyncResult.IOCompleted(this, errorCode, numBytes);
			}

			// Token: 0x060041FD RID: 16893 RVA: 0x00112458 File Offset: 0x00110658
			private static void IOCompleted(HttpRequestStream.HttpRequestStreamAsyncResult asyncResult, uint errorCode, uint numBytes)
			{
				object result = null;
				try
				{
					if (errorCode != 0U && errorCode != 38U)
					{
						asyncResult.ErrorCode = (int)errorCode;
						result = new HttpListenerException((int)errorCode);
					}
					else
					{
						result = numBytes;
						if (Logging.On)
						{
							Logging.Dump(Logging.HttpListener, asyncResult, "Callback", (IntPtr)asyncResult.m_pPinnedBuffer, (int)numBytes);
						}
					}
				}
				catch (Exception ex)
				{
					result = ex;
				}
				asyncResult.InvokeCallback(result);
			}

			// Token: 0x060041FE RID: 16894 RVA: 0x001124C8 File Offset: 0x001106C8
			private unsafe static void Callback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
				HttpRequestStream.HttpRequestStreamAsyncResult asyncResult = overlapped.AsyncResult as HttpRequestStream.HttpRequestStreamAsyncResult;
				HttpRequestStream.HttpRequestStreamAsyncResult.IOCompleted(asyncResult, errorCode, numBytes);
			}

			// Token: 0x060041FF RID: 16895 RVA: 0x001124F0 File Offset: 0x001106F0
			protected override void Cleanup()
			{
				base.Cleanup();
				if (this.m_pOverlapped != null)
				{
					Overlapped.Free(this.m_pOverlapped);
				}
			}

			// Token: 0x04003209 RID: 12809
			internal unsafe NativeOverlapped* m_pOverlapped;

			// Token: 0x0400320A RID: 12810
			internal unsafe void* m_pPinnedBuffer;

			// Token: 0x0400320B RID: 12811
			internal uint m_dataAlreadyRead;

			// Token: 0x0400320C RID: 12812
			private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(HttpRequestStream.HttpRequestStreamAsyncResult.Callback);
		}
	}
}
