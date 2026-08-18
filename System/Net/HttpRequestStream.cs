using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004E6 RID: 1254
	internal class HttpRequestStream : Stream
	{
		// Token: 0x060026FE RID: 9982 RVA: 0x000A102F File Offset: 0x000A002F
		internal HttpRequestStream(HttpListenerContext httpContext)
		{
			this.m_HttpContext = httpContext;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x000A103E File Offset: 0x000A003E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x000A1041 File Offset: 0x000A0041
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000A1044 File Offset: 0x000A0044
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000A1047 File Offset: 0x000A0047
		public override void Flush()
		{
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000A1049 File Offset: 0x000A0049
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x000A105A File Offset: 0x000A005A
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x000A106B File Offset: 0x000A006B
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

		// Token: 0x06002706 RID: 9990 RVA: 0x000A107C File Offset: 0x000A007C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000A108D File Offset: 0x000A008D
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000A10A0 File Offset: 0x000A00A0
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
				fixed (byte* ptr = buffer)
				{
					num3 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, 1U, (void*)((byte*)ptr + offset), (uint)size, &num2, null);
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
				Logging.Exit(Logging.HttpListener, this, "Read", "dataRead:" + num);
			}
			return (int)num;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000A125C File Offset: 0x000A025C
		private void UpdateAfterRead(uint statusCode, uint dataRead)
		{
			if (statusCode == 38U || dataRead == 0U)
			{
				this.Close();
			}
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000A126C File Offset: 0x000A026C
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
				try
				{
					if (buffer != null)
					{
						int num3 = buffer.Length;
					}
					this.m_HttpContext.EnsureBoundHandle();
					num2 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, 1U, httpRequestStreamAsyncResult2.m_pPinnedBuffer, (uint)size, null, httpRequestStreamAsyncResult2.m_pOverlapped);
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
					if (num2 == 38U)
					{
						httpRequestStreamAsyncResult2.m_pOverlapped->InternalLow = IntPtr.Zero;
					}
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
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "BeginRead", "");
			}
			return httpRequestStreamAsyncResult2;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000A14C0 File Offset: 0x000A04C0
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

		// Token: 0x0600270C RID: 9996 RVA: 0x000A15B4 File Offset: 0x000A05B4
		public override void Write(byte[] buffer, int offset, int size)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000A15C5 File Offset: 0x000A05C5
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000A15D6 File Offset: 0x000A05D6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000A15E8 File Offset: 0x000A05E8
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

		// Token: 0x040026A0 RID: 9888
		private const int MaxReadSize = 131072;

		// Token: 0x040026A1 RID: 9889
		private HttpListenerContext m_HttpContext;

		// Token: 0x040026A2 RID: 9890
		private uint m_DataChunkOffset;

		// Token: 0x040026A3 RID: 9891
		private int m_DataChunkIndex;

		// Token: 0x040026A4 RID: 9892
		private bool m_Closed;

		// Token: 0x020004E7 RID: 1255
		private class HttpRequestStreamAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002710 RID: 10000 RVA: 0x000A1650 File Offset: 0x000A0650
			internal HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback) : base(asyncObject, userState, callback)
			{
			}

			// Token: 0x06002711 RID: 10001 RVA: 0x000A165B File Offset: 0x000A065B
			internal HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, uint dataAlreadyRead) : base(asyncObject, userState, callback)
			{
				this.m_dataAlreadyRead = dataAlreadyRead;
			}

			// Token: 0x06002712 RID: 10002 RVA: 0x000A1670 File Offset: 0x000A0670
			internal unsafe HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, byte[] buffer, int offset, uint size, uint dataAlreadyRead) : base(asyncObject, userState, callback)
			{
				this.m_dataAlreadyRead = dataAlreadyRead;
				this.m_pOverlapped = new Overlapped
				{
					AsyncResult = this
				}.Pack(HttpRequestStream.HttpRequestStreamAsyncResult.s_IOCallback, buffer);
				this.m_pPinnedBuffer = (void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
			}

			// Token: 0x06002713 RID: 10003 RVA: 0x000A16C4 File Offset: 0x000A06C4
			private unsafe static void Callback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
				HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult = overlapped.AsyncResult as HttpRequestStream.HttpRequestStreamAsyncResult;
				object result = null;
				try
				{
					if (errorCode != 0U && errorCode != 38U)
					{
						httpRequestStreamAsyncResult.ErrorCode = (int)errorCode;
						result = new HttpListenerException((int)errorCode);
					}
					else
					{
						result = numBytes;
						if (Logging.On)
						{
							Logging.Dump(Logging.HttpListener, httpRequestStreamAsyncResult, "Callback", (IntPtr)httpRequestStreamAsyncResult.m_pPinnedBuffer, (int)numBytes);
						}
					}
				}
				catch (Exception ex)
				{
					result = ex;
				}
				httpRequestStreamAsyncResult.InvokeCallback(result);
			}

			// Token: 0x06002714 RID: 10004 RVA: 0x000A1748 File Offset: 0x000A0748
			protected override void Cleanup()
			{
				base.Cleanup();
				if (this.m_pOverlapped != null)
				{
					Overlapped.Free(this.m_pOverlapped);
				}
			}

			// Token: 0x040026A5 RID: 9893
			internal unsafe NativeOverlapped* m_pOverlapped;

			// Token: 0x040026A6 RID: 9894
			internal unsafe void* m_pPinnedBuffer;

			// Token: 0x040026A7 RID: 9895
			internal uint m_dataAlreadyRead;

			// Token: 0x040026A8 RID: 9896
			private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(HttpRequestStream.HttpRequestStreamAsyncResult.Callback);
		}
	}
}
