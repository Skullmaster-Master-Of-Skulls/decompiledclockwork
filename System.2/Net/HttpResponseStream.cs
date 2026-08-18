using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001BF RID: 447
	internal class HttpResponseStream : Stream
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x0005F493 File Offset: 0x0005D693
		internal HttpResponseStream(HttpListenerContext httpContext)
		{
			this.m_HttpContext = httpContext;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0005F4B4 File Offset: 0x0005D6B4
		internal UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS ComputeLeftToWrite()
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS result = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			if (!this.m_HttpContext.Response.ComputedHeaders)
			{
				result = this.m_HttpContext.Response.ComputeHeaders();
			}
			if (this.m_LeftToWrite == -9223372036854775808L)
			{
				UnsafeNclNativeMethods.HttpApi.HTTP_VERB knownMethod = this.m_HttpContext.GetKnownMethod();
				this.m_LeftToWrite = ((knownMethod != UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbHEAD) ? this.m_HttpContext.Response.ContentLength64 : 0L);
			}
			return result;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0005F522 File Offset: 0x0005D722
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0005F525 File Offset: 0x0005D725
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0005F528 File Offset: 0x0005D728
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0005F52B File Offset: 0x0005D72B
		internal bool Closed
		{
			get
			{
				return this.m_Closed;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0005F533 File Offset: 0x0005D733
		internal HttpListenerContext InternalHttpContext
		{
			get
			{
				return this.m_HttpContext;
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0005F53B File Offset: 0x0005D73B
		internal void SetClosedFlag()
		{
			this.m_Closed = true;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0005F544 File Offset: 0x0005D744
		public override void Flush()
		{
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0005F546 File Offset: 0x0005D746
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0005F54D File Offset: 0x0005D74D
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0005F55E File Offset: 0x0005D75E
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x0005F56F File Offset: 0x0005D76F
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

		// Token: 0x060011A0 RID: 4512 RVA: 0x0005F580 File Offset: 0x0005D780
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0005F591 File Offset: 0x0005D791
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0005F5A2 File Offset: 0x0005D7A2
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0005F5B3 File Offset: 0x0005D7B3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0005F5D8 File Offset: 0x0005D7D8
		public unsafe override void Write(byte[] buffer, int offset, int size)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Write", "");
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
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = this.ComputeLeftToWrite();
			if (this.m_Closed || (size == 0 && this.m_LeftToWrite != 0L))
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Write", "");
				}
				return;
			}
			if (this.m_LeftToWrite >= 0L && (long)size > this.m_LeftToWrite)
			{
				throw new ProtocolViolationException(SR.GetString("net_entitytoobig"));
			}
			uint num = (uint)size;
			SafeLocalFree safeLocalFree = null;
			IntPtr intPtr = IntPtr.Zero;
			bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
			uint num2;
			try
			{
				if (size == 0)
				{
					num2 = this.m_HttpContext.Response.SendHeaders(null, null, http_FLAGS, false);
				}
				else
				{
					try
					{
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
							byte* ptr2 = ptr;
							if (this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked)
							{
								string text = size.ToString("x", CultureInfo.InvariantCulture);
								num += (uint)(text.Length + 4);
								safeLocalFree = SafeLocalFree.LocalAlloc((int)num);
								intPtr = safeLocalFree.DangerousGetHandle();
								for (int i = 0; i < text.Length; i++)
								{
									Marshal.WriteByte(intPtr, i, (byte)text[i]);
								}
								Marshal.WriteInt16(intPtr, text.Length, 2573);
								Marshal.Copy(buffer, offset, IntPtrHelper.Add(intPtr, text.Length + 2), size);
								Marshal.WriteInt16(intPtr, (int)(num - 2U), 2573);
								ptr2 = (byte*)((void*)intPtr);
								offset = 0;
							}
							UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK http_DATA_CHUNK = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
							http_DATA_CHUNK.DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
							http_DATA_CHUNK.pBuffer = ptr2 + offset;
							http_DATA_CHUNK.BufferLength = num;
							http_FLAGS |= ((this.m_LeftToWrite == (long)size) ? UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE : UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA);
							if (!sentHeaders)
							{
								num2 = this.m_HttpContext.Response.SendHeaders(&http_DATA_CHUNK, null, http_FLAGS, false);
							}
							else
							{
								num2 = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, 1, &http_DATA_CHUNK, null, SafeLocalFree.Zero, 0U, null, null);
								if (this.m_HttpContext.Listener.IgnoreWriteExceptions)
								{
									num2 = 0U;
								}
							}
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			if (num2 != 0U && num2 != 38U)
			{
				Exception ex = new HttpListenerException((int)num2);
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "Write", ex);
				}
				this.m_Closed = true;
				this.m_HttpContext.Abort();
				throw ex;
			}
			this.UpdateAfterWrite(num);
			if (Logging.On)
			{
				Logging.Dump(Logging.HttpListener, this, "Write", buffer, offset, (int)num);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Write", "");
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public unsafe override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
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
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = this.ComputeLeftToWrite();
			if (this.m_Closed || (size == 0 && this.m_LeftToWrite != 0L))
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "BeginWrite", "");
				}
				HttpResponseStreamAsyncResult httpResponseStreamAsyncResult = new HttpResponseStreamAsyncResult(this, state, callback);
				httpResponseStreamAsyncResult.InvokeCallback(0U);
				return httpResponseStreamAsyncResult;
			}
			if (this.m_LeftToWrite >= 0L && (long)size > this.m_LeftToWrite)
			{
				throw new ProtocolViolationException(SR.GetString("net_entitytoobig"));
			}
			uint numBytes = 0U;
			http_FLAGS |= ((this.m_LeftToWrite == (long)size) ? UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE : UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA);
			bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
			HttpResponseStreamAsyncResult httpResponseStreamAsyncResult2 = new HttpResponseStreamAsyncResult(this, state, callback, buffer, offset, size, this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked, sentHeaders);
			this.UpdateAfterWrite((uint)((this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked) ? 0 : size));
			uint num;
			try
			{
				if (!sentHeaders)
				{
					num = this.m_HttpContext.Response.SendHeaders(null, httpResponseStreamAsyncResult2, http_FLAGS, false);
				}
				else
				{
					this.m_HttpContext.EnsureBoundHandle();
					num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, httpResponseStreamAsyncResult2.dataChunkCount, httpResponseStreamAsyncResult2.pDataChunks, &numBytes, SafeLocalFree.Zero, 0U, httpResponseStreamAsyncResult2.m_pOverlapped, null);
				}
			}
			catch (Exception e)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "BeginWrite", e);
				}
				httpResponseStreamAsyncResult2.InternalCleanup();
				this.m_Closed = true;
				this.m_HttpContext.Abort();
				throw;
			}
			if (num != 0U && num != 997U)
			{
				httpResponseStreamAsyncResult2.InternalCleanup();
				if (!this.m_HttpContext.Listener.IgnoreWriteExceptions || !sentHeaders)
				{
					Exception ex = new HttpListenerException((int)num);
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "BeginWrite", ex);
					}
					this.m_Closed = true;
					this.m_HttpContext.Abort();
					throw ex;
				}
			}
			if (num == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
			{
				httpResponseStreamAsyncResult2.IOCompleted(num, numBytes);
			}
			if ((http_FLAGS & UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA) == UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE)
			{
				this.m_LastWrite = httpResponseStreamAsyncResult2;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "BeginWrite", "");
			}
			return httpResponseStreamAsyncResult2;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0005FB60 File Offset: 0x0005DD60
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "EndWrite", "");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpResponseStreamAsyncResult httpResponseStreamAsyncResult = asyncResult as HttpResponseStreamAsyncResult;
			if (httpResponseStreamAsyncResult == null || httpResponseStreamAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (httpResponseStreamAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndWrite"
				}));
			}
			httpResponseStreamAsyncResult.EndCalled = true;
			object obj = httpResponseStreamAsyncResult.InternalWaitForCompletion();
			Exception ex = obj as Exception;
			if (ex != null)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "EndWrite", ex);
				}
				this.m_Closed = true;
				this.m_HttpContext.Abort();
				throw ex;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "EndWrite", "");
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0005FC45 File Offset: 0x0005DE45
		private void UpdateAfterWrite(uint dataWritten)
		{
			if (!this.m_InOpaqueMode)
			{
				if (this.m_LeftToWrite > 0L)
				{
					this.m_LeftToWrite -= (long)((ulong)dataWritten);
				}
				if (this.m_LeftToWrite == 0L)
				{
					this.m_Closed = true;
				}
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0005FC78 File Offset: 0x0005DE78
		protected unsafe override void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close", "");
			}
			try
			{
				if (disposing)
				{
					if (this.m_Closed)
					{
						if (Logging.On)
						{
							Logging.Exit(Logging.HttpListener, this, "Close", "");
						}
						return;
					}
					this.m_Closed = true;
					UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = this.ComputeLeftToWrite();
					if (this.m_LeftToWrite > 0L && !this.m_InOpaqueMode)
					{
						throw new InvalidOperationException(SR.GetString("net_io_notenoughbyteswritten"));
					}
					bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
					if (sentHeaders && this.m_LeftToWrite == 0L)
					{
						if (Logging.On)
						{
							Logging.Exit(Logging.HttpListener, this, "Close", "");
						}
						return;
					}
					uint num = 0U;
					if ((this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked || this.m_HttpContext.Response.BoundaryType == BoundaryType.None) && string.Compare(this.m_HttpContext.Request.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase) != 0)
					{
						if (this.m_HttpContext.Response.BoundaryType == BoundaryType.None)
						{
							http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
						}
						try
						{
							byte[] array;
							void* pBuffer;
							if ((array = NclConstants.ChunkTerminator) == null || array.Length == 0)
							{
								pBuffer = null;
							}
							else
							{
								pBuffer = (void*)(&array[0]);
							}
							UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* ptr = null;
							if (this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked)
							{
								UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK http_DATA_CHUNK = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
								http_DATA_CHUNK.DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
								http_DATA_CHUNK.pBuffer = (byte*)pBuffer;
								http_DATA_CHUNK.BufferLength = (uint)NclConstants.ChunkTerminator.Length;
								ptr = &http_DATA_CHUNK;
							}
							if (!sentHeaders)
							{
								num = this.m_HttpContext.Response.SendHeaders(ptr, null, http_FLAGS, false);
								goto IL_200;
							}
							num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, (ptr != null) ? 1 : 0, ptr, null, SafeLocalFree.Zero, 0U, null, null);
							if (this.m_HttpContext.Listener.IgnoreWriteExceptions)
							{
								num = 0U;
							}
							goto IL_200;
						}
						finally
						{
							byte[] array = null;
						}
					}
					if (!sentHeaders)
					{
						num = this.m_HttpContext.Response.SendHeaders(null, null, http_FLAGS, false);
					}
					IL_200:
					if (num != 0U && num != 38U)
					{
						Exception ex = new HttpListenerException((int)num);
						if (Logging.On)
						{
							Logging.Exception(Logging.HttpListener, this, "Close", ex);
						}
						this.m_HttpContext.Abort();
						throw ex;
					}
					this.m_LeftToWrite = 0L;
				}
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

		// Token: 0x060011AA RID: 4522 RVA: 0x0005FF20 File Offset: 0x0005E120
		internal void SwitchToOpaqueMode()
		{
			this.m_InOpaqueMode = true;
			this.m_LeftToWrite = long.MaxValue;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0005FF38 File Offset: 0x0005E138
		internal void CancelLastWrite(CriticalHandle requestQueueHandle)
		{
			HttpResponseStreamAsyncResult lastWrite = this.m_LastWrite;
			if (lastWrite != null && !lastWrite.IsCompleted)
			{
				UnsafeNclNativeMethods.CancelIoEx(requestQueueHandle, lastWrite.m_pOverlapped);
			}
		}

		// Token: 0x04001464 RID: 5220
		private HttpListenerContext m_HttpContext;

		// Token: 0x04001465 RID: 5221
		private long m_LeftToWrite = long.MinValue;

		// Token: 0x04001466 RID: 5222
		private bool m_Closed;

		// Token: 0x04001467 RID: 5223
		private bool m_InOpaqueMode;

		// Token: 0x04001468 RID: 5224
		private HttpResponseStreamAsyncResult m_LastWrite;
	}
}
