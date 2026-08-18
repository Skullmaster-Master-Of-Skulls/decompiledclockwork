using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x020004E8 RID: 1256
	internal class HttpResponseStream : Stream
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x000A1778 File Offset: 0x000A0778
		internal HttpResponseStream(HttpListenerContext httpContext)
		{
			this.m_HttpContext = httpContext;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000A1798 File Offset: 0x000A0798
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
				if (this.m_LeftToWrite == 0L)
				{
					this.Close();
				}
				else if (knownMethod == UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbOPTIONS && this.m_LeftToWrite > 0L)
				{
					throw new ProtocolViolationException(SR.GetString("net_nouploadonget"));
				}
			}
			return result;
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x000A1836 File Offset: 0x000A0836
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000A1839 File Offset: 0x000A0839
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000A183C File Offset: 0x000A083C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000A183F File Offset: 0x000A083F
		public override void Flush()
		{
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000A1841 File Offset: 0x000A0841
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000A1852 File Offset: 0x000A0852
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x000A1863 File Offset: 0x000A0863
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

		// Token: 0x0600271F RID: 10015 RVA: 0x000A1874 File Offset: 0x000A0874
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000A1885 File Offset: 0x000A0885
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000A1896 File Offset: 0x000A0896
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000A18A7 File Offset: 0x000A08A7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000A18B8 File Offset: 0x000A08B8
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000A18CC File Offset: 0x000A08CC
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
			if (size == 0 || this.m_Closed)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Write", "");
				}
				return;
			}
			if (this.m_LeftToWrite > 0L && (long)size > this.m_LeftToWrite)
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
				try
				{
					fixed (byte* ptr = buffer)
					{
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
							num2 = this.m_HttpContext.Response.SendHeaders(&http_DATA_CHUNK, null, http_FLAGS);
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
					byte* ptr = null;
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

		// Token: 0x06002725 RID: 10021 RVA: 0x000A1BC0 File Offset: 0x000A0BC0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
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
			if (size == 0 || this.m_Closed)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "BeginWrite", "");
				}
				HttpResponseStreamAsyncResult httpResponseStreamAsyncResult = new HttpResponseStreamAsyncResult(this, state, callback);
				httpResponseStreamAsyncResult.InvokeCallback(0U);
				return httpResponseStreamAsyncResult;
			}
			if (this.m_LeftToWrite > 0L && (long)size > this.m_LeftToWrite)
			{
				throw new ProtocolViolationException(SR.GetString("net_entitytoobig"));
			}
			http_FLAGS |= ((this.m_LeftToWrite == (long)size) ? UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE : UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA);
			bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
			HttpResponseStreamAsyncResult httpResponseStreamAsyncResult2 = new HttpResponseStreamAsyncResult(this, state, callback, buffer, offset, size, this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked, sentHeaders);
			this.UpdateAfterWrite((uint)((this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked) ? 0 : size));
			uint num;
			try
			{
				if (!sentHeaders)
				{
					num = this.m_HttpContext.Response.SendHeaders(null, httpResponseStreamAsyncResult2, http_FLAGS);
				}
				else
				{
					this.m_HttpContext.EnsureBoundHandle();
					num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, httpResponseStreamAsyncResult2.dataChunkCount, httpResponseStreamAsyncResult2.pDataChunks, null, SafeLocalFree.Zero, 0U, httpResponseStreamAsyncResult2.m_pOverlapped, null);
				}
			}
			catch (Exception e)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "BeginWrite", e);
				}
				httpResponseStreamAsyncResult2.InternalCleanup();
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
					this.m_HttpContext.Abort();
					throw ex;
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "BeginWrite", "");
			}
			return httpResponseStreamAsyncResult2;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000A1DEC File Offset: 0x000A0DEC
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
				this.m_HttpContext.Abort();
				throw ex;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "EndWrite", "");
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000A1ECC File Offset: 0x000A0ECC
		private void UpdateAfterWrite(uint dataWritten)
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

		// Token: 0x06002728 RID: 10024 RVA: 0x000A1EF8 File Offset: 0x000A0EF8
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
					if (this.m_LeftToWrite > 0L)
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
							fixed (void* ptr = NclConstants.ChunkTerminator)
							{
								UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* ptr2 = null;
								if (this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked)
								{
									UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK http_DATA_CHUNK = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
									http_DATA_CHUNK.DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
									http_DATA_CHUNK.pBuffer = (byte*)ptr;
									http_DATA_CHUNK.BufferLength = (uint)NclConstants.ChunkTerminator.Length;
									ptr2 = &http_DATA_CHUNK;
								}
								if (!sentHeaders)
								{
									num = this.m_HttpContext.Response.SendHeaders(ptr2, null, http_FLAGS);
								}
								else
								{
									num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, (ptr2 != null) ? 1 : 0, ptr2, null, SafeLocalFree.Zero, 0U, null, null);
									if (this.m_HttpContext.Listener.IgnoreWriteExceptions)
									{
										num = 0U;
									}
								}
								goto IL_1F6;
							}
						}
						finally
						{
							void* ptr = null;
						}
					}
					if (!sentHeaders)
					{
						num = this.m_HttpContext.Response.SendHeaders(null, null, http_FLAGS);
					}
					IL_1F6:
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

		// Token: 0x040026A9 RID: 9897
		private HttpListenerContext m_HttpContext;

		// Token: 0x040026AA RID: 9898
		private long m_LeftToWrite = long.MinValue;

		// Token: 0x040026AB RID: 9899
		private bool m_Closed;
	}
}
