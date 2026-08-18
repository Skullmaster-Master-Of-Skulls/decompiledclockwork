using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x02000361 RID: 865
	internal class _SslStream
	{
		// Token: 0x06001FAA RID: 8106 RVA: 0x00093D60 File Offset: 0x00091F60
		internal _SslStream(SslState sslState)
		{
			if (PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				PinnableBufferCacheEventSource.Log.DebugMessage1("CTOR: In System.Net._SslStream.SslStream", (long)this.GetHashCode());
			}
			this._SslState = sslState;
			this._Reader = new FixedSizeReader(this._SslState.InnerStream);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00093DB2 File Offset: 0x00091FB2
		private void FreeReadBuffer()
		{
			if (this._InternalBufferFromPinnableCache)
			{
				_SslStream.s_PinnableReadBufferCache.FreeBuffer(this._InternalBuffer);
				this._InternalBufferFromPinnableCache = false;
			}
			this._InternalBuffer = null;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00093DDC File Offset: 0x00091FDC
		~_SslStream()
		{
			if (this._InternalBufferFromPinnableCache)
			{
				if (PinnableBufferCacheEventSource.Log.IsEnabled())
				{
					PinnableBufferCacheEventSource.Log.DebugMessage2("DTOR: In System.Net._SslStream.~SslStream Freeing Read Buffer", (long)this.GetHashCode(), PinnableBufferCacheEventSource.AddressOfByteArray(this._InternalBuffer));
				}
				this.FreeReadBuffer();
			}
			if (this._PinnableOutputBuffer != null)
			{
				if (PinnableBufferCacheEventSource.Log.IsEnabled())
				{
					PinnableBufferCacheEventSource.Log.DebugMessage2("DTOR: In System.Net._SslStream.~SslStream Freeing Write Buffer", (long)this.GetHashCode(), PinnableBufferCacheEventSource.AddressOfByteArray(this._PinnableOutputBuffer));
				}
				_SslStream.s_PinnableWriteBufferCache.FreeBuffer(this._PinnableOutputBuffer);
			}
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00093E84 File Offset: 0x00092084
		internal int Read(byte[] buffer, int offset, int count)
		{
			return this.ProcessRead(buffer, offset, count, null);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00093E90 File Offset: 0x00092090
		internal void Write(byte[] buffer, int offset, int count)
		{
			this.ProcessWrite(buffer, offset, count, null);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00093E9C File Offset: 0x0009209C
		internal void Write(BufferOffsetSize[] buffers)
		{
			this.ProcessWrite(buffers, null);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00093EA8 File Offset: 0x000920A8
		internal IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, count, asyncState, asyncCallback);
			AsyncProtocolRequest asyncRequest = new AsyncProtocolRequest(bufferAsyncResult);
			this.ProcessRead(buffer, offset, count, asyncRequest);
			return bufferAsyncResult;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00093ED8 File Offset: 0x000920D8
		internal int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
			if (bufferAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					asyncResult.GetType().FullName
				}), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedRead, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndRead"
				}));
			}
			bufferAsyncResult.InternalWaitForCompletion();
			if (!(bufferAsyncResult.Result is Exception))
			{
				return (int)bufferAsyncResult.Result;
			}
			if (bufferAsyncResult.Result is IOException)
			{
				throw (Exception)bufferAsyncResult.Result;
			}
			throw new IOException(SR.GetString("net_io_read"), (Exception)bufferAsyncResult.Result);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00093FA8 File Offset: 0x000921A8
		internal IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, asyncState, asyncCallback);
			AsyncProtocolRequest asyncRequest = new AsyncProtocolRequest(lazyAsyncResult);
			this.ProcessWrite(buffer, offset, count, asyncRequest);
			return lazyAsyncResult;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00093FD4 File Offset: 0x000921D4
		internal IAsyncResult BeginWrite(BufferOffsetSize[] buffers, AsyncCallback asyncCallback, object asyncState)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, asyncState, asyncCallback);
			_SslStream.SplitWriteAsyncProtocolRequest asyncRequest = new _SslStream.SplitWriteAsyncProtocolRequest(lazyAsyncResult);
			this.ProcessWrite(buffers, asyncRequest);
			return lazyAsyncResult;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00093FFC File Offset: 0x000921FC
		internal void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					asyncResult.GetType().FullName
				}), "asyncResult");
			}
			if (Interlocked.Exchange(ref this._NestedWrite, 0) == 0)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndWrite"
				}));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			if (!(lazyAsyncResult.Result is Exception))
			{
				return;
			}
			if (lazyAsyncResult.Result is IOException)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
			throw new IOException(SR.GetString("net_io_write"), (Exception)lazyAsyncResult.Result);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x000940BE File Offset: 0x000922BE
		internal bool DataAvailable
		{
			get
			{
				return this.InternalBufferCount != 0;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x000940C9 File Offset: 0x000922C9
		private byte[] InternalBuffer
		{
			get
			{
				return this._InternalBuffer;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x000940D1 File Offset: 0x000922D1
		private int InternalOffset
		{
			get
			{
				return this._InternalOffset;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x000940D9 File Offset: 0x000922D9
		private int InternalBufferCount
		{
			get
			{
				return this._InternalBufferCount;
			}
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000940E1 File Offset: 0x000922E1
		private void DecrementInternalBufferCount(int decrCount)
		{
			this._InternalOffset += decrCount;
			this._InternalBufferCount -= decrCount;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00094100 File Offset: 0x00092300
		private void EnsureInternalBufferSize(int curOffset, int addSize)
		{
			if (this._InternalBuffer == null || this._InternalBuffer.Length < addSize + curOffset)
			{
				bool internalBufferFromPinnableCache = this._InternalBufferFromPinnableCache;
				byte[] internalBuffer = this._InternalBuffer;
				int num = addSize + curOffset;
				if (num <= 16416)
				{
					if (PinnableBufferCacheEventSource.Log.IsEnabled())
					{
						PinnableBufferCacheEventSource.Log.DebugMessage2("In System.Net._SslStream.EnsureInternalBufferSize IS pinnable", (long)this.GetHashCode(), (long)num);
					}
					this._InternalBufferFromPinnableCache = true;
					this._InternalBuffer = _SslStream.s_PinnableReadBufferCache.AllocateBuffer();
				}
				else
				{
					if (PinnableBufferCacheEventSource.Log.IsEnabled())
					{
						PinnableBufferCacheEventSource.Log.DebugMessage2("In System.Net._SslStream.EnsureInternalBufferSize NOT pinnable", (long)this.GetHashCode(), (long)num);
					}
					this._InternalBufferFromPinnableCache = false;
					this._InternalBuffer = new byte[num];
				}
				if (internalBuffer != null && curOffset != 0)
				{
					Buffer.BlockCopy(internalBuffer, 0, this._InternalBuffer, 0, curOffset);
				}
				if (internalBufferFromPinnableCache)
				{
					_SslStream.s_PinnableReadBufferCache.FreeBuffer(internalBuffer);
				}
			}
			this._InternalOffset = curOffset;
			this._InternalBufferCount = curOffset + addSize;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000941E8 File Offset: 0x000923E8
		private void ValidateParameters(byte[] buffer, int offset, int count)
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
			if (count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("net_offset_plus_count"));
			}
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00094240 File Offset: 0x00092440
		private void ProcessWrite(BufferOffsetSize[] buffers, _SslStream.SplitWriteAsyncProtocolRequest asyncRequest)
		{
			this._SslState.CheckThrow(true, true);
			foreach (BufferOffsetSize bufferOffsetSize in buffers)
			{
				this.ValidateParameters(bufferOffsetSize.Buffer, bufferOffsetSize.Offset, bufferOffsetSize.Size);
			}
			if (Interlocked.Exchange(ref this._NestedWrite, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(asyncRequest != null) ? "BeginWrite" : "Write",
					"write"
				}));
			}
			bool flag = false;
			try
			{
				SplitWritesState splitWritesState = new SplitWritesState(buffers);
				if (asyncRequest != null)
				{
					asyncRequest.SetNextRequest(splitWritesState, _SslStream._ResumeAsyncWriteCallback);
				}
				this.StartWriting(splitWritesState, asyncRequest);
			}
			catch (Exception ex)
			{
				this._SslState.FinishWrite();
				flag = true;
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_write"), ex);
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedWrite = 0;
				}
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00094344 File Offset: 0x00092544
		private void ProcessWrite(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (this._SslState.LastPayload != null)
			{
				BufferOffsetSize[] buffers = new BufferOffsetSize[]
				{
					new BufferOffsetSize(buffer, offset, count, false)
				};
				if (asyncRequest != null)
				{
					this.ProcessWrite(buffers, new _SslStream.SplitWriteAsyncProtocolRequest(asyncRequest.UserAsyncResult));
					return;
				}
				this.ProcessWrite(buffers, null);
				return;
			}
			else
			{
				this.ValidateParameters(buffer, offset, count);
				this._SslState.CheckThrow(true, true);
				if (Interlocked.Exchange(ref this._NestedWrite, 1) == 1)
				{
					throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
					{
						(asyncRequest != null) ? "BeginWrite" : "Write",
						"write"
					}));
				}
				bool flag = false;
				try
				{
					this.StartWriting(buffer, offset, count, asyncRequest);
				}
				catch (Exception ex)
				{
					this._SslState.FinishWrite();
					flag = true;
					if (ex is IOException)
					{
						throw;
					}
					throw new IOException(SR.GetString("net_io_write"), ex);
				}
				finally
				{
					if (asyncRequest == null || flag)
					{
						this._NestedWrite = 0;
					}
				}
				return;
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00094450 File Offset: 0x00092650
		private void StartWriting(SplitWritesState splitWrite, _SslStream.SplitWriteAsyncProtocolRequest asyncRequest)
		{
			while (!splitWrite.IsDone)
			{
				if (this._SslState.CheckEnqueueWrite(asyncRequest))
				{
					return;
				}
				byte[] lastHandshakePayload = null;
				if (this._SslState.LastPayload != null)
				{
					lastHandshakePayload = this._SslState.LastPayload;
					this._SslState.LastPayloadConsumed();
				}
				BufferOffsetSize[] buffers = splitWrite.GetNextBuffers();
				buffers = this.EncryptBuffers(buffers, lastHandshakePayload);
				if (asyncRequest != null)
				{
					IAsyncResult asyncResult = ((NetworkStream)this._SslState.InnerStream).BeginMultipleWrite(buffers, _SslStream._MulitpleWriteCallback, asyncRequest);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					((NetworkStream)this._SslState.InnerStream).EndMultipleWrite(asyncResult);
				}
				else
				{
					((NetworkStream)this._SslState.InnerStream).MultipleWrite(buffers);
				}
				this._SslState.FinishWrite();
			}
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser();
			}
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00094520 File Offset: 0x00092720
		private BufferOffsetSize[] EncryptBuffers(BufferOffsetSize[] buffers, byte[] lastHandshakePayload)
		{
			List<BufferOffsetSize> list = null;
			SecurityStatus securityStatus = SecurityStatus.OK;
			foreach (BufferOffsetSize bufferOffsetSize in buffers)
			{
				int num = Math.Min(bufferOffsetSize.Size, this._SslState.MaxDataSize);
				byte[] buffer = null;
				int size;
				securityStatus = this._SslState.EncryptData(bufferOffsetSize.Buffer, bufferOffsetSize.Offset, num, ref buffer, out size);
				if (securityStatus != SecurityStatus.OK)
				{
					break;
				}
				if (num != bufferOffsetSize.Size || list != null)
				{
					if (list == null)
					{
						list = new List<BufferOffsetSize>(buffers.Length * (bufferOffsetSize.Size / num + 1));
						if (lastHandshakePayload != null)
						{
							list.Add(new BufferOffsetSize(lastHandshakePayload, false));
						}
						foreach (BufferOffsetSize bufferOffsetSize2 in buffers)
						{
							if (bufferOffsetSize2 == bufferOffsetSize)
							{
								break;
							}
							list.Add(bufferOffsetSize2);
						}
					}
					list.Add(new BufferOffsetSize(buffer, 0, size, false));
					while ((bufferOffsetSize.Size -= num) != 0)
					{
						bufferOffsetSize.Offset += num;
						num = Math.Min(bufferOffsetSize.Size, this._SslState.MaxDataSize);
						buffer = null;
						securityStatus = this._SslState.EncryptData(bufferOffsetSize.Buffer, bufferOffsetSize.Offset, num, ref buffer, out size);
						if (securityStatus != SecurityStatus.OK)
						{
							break;
						}
						list.Add(new BufferOffsetSize(buffer, 0, size, false));
					}
				}
				else
				{
					bufferOffsetSize.Buffer = buffer;
					bufferOffsetSize.Offset = 0;
					bufferOffsetSize.Size = size;
				}
				if (securityStatus != SecurityStatus.OK)
				{
					break;
				}
			}
			if (securityStatus != SecurityStatus.OK)
			{
				ProtocolToken protocolToken = new ProtocolToken(null, securityStatus);
				throw new IOException(SR.GetString("net_io_encrypt"), protocolToken.GetException());
			}
			if (list != null)
			{
				buffers = list.ToArray();
			}
			else if (lastHandshakePayload != null)
			{
				BufferOffsetSize[] array3 = new BufferOffsetSize[buffers.Length + 1];
				Array.Copy(buffers, 0, array3, 1, buffers.Length);
				array3[0] = new BufferOffsetSize(lastHandshakePayload, false);
				buffers = array3;
			}
			return buffers;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000946F8 File Offset: 0x000928F8
		private void StartWriting(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(buffer, offset, count, _SslStream._ResumeAsyncWriteCallback);
			}
			if (count >= 0)
			{
				byte[] array = null;
				if (this._PinnableOutputBufferInUse == null)
				{
					if (this._PinnableOutputBuffer == null)
					{
						this._PinnableOutputBuffer = _SslStream.s_PinnableWriteBufferCache.AllocateBuffer();
					}
					this._PinnableOutputBufferInUse = buffer;
					array = this._PinnableOutputBuffer;
					if (PinnableBufferCacheEventSource.Log.IsEnabled())
					{
						PinnableBufferCacheEventSource.Log.DebugMessage3("In System.Net._SslStream.StartWriting Trying Pinnable", (long)this.GetHashCode(), (long)count, PinnableBufferCacheEventSource.AddressOfByteArray(array));
					}
				}
				else if (PinnableBufferCacheEventSource.Log.IsEnabled())
				{
					PinnableBufferCacheEventSource.Log.DebugMessage2("In System.Net._SslStream.StartWriting BufferInUse", (long)this.GetHashCode(), (long)count);
				}
				while (!this._SslState.CheckEnqueueWrite(asyncRequest))
				{
					int num = Math.Min(count, this._SslState.MaxDataSize);
					int num2;
					SecurityStatus securityStatus = this._SslState.EncryptData(buffer, offset, num, ref array, out num2);
					if (securityStatus != SecurityStatus.OK)
					{
						ProtocolToken protocolToken = new ProtocolToken(null, securityStatus);
						throw new IOException(SR.GetString("net_io_encrypt"), protocolToken.GetException());
					}
					if (PinnableBufferCacheEventSource.Log.IsEnabled())
					{
						PinnableBufferCacheEventSource.Log.DebugMessage3("In System.Net._SslStream.StartWriting Got Encrypted Buffer", (long)this.GetHashCode(), (long)num2, PinnableBufferCacheEventSource.AddressOfByteArray(array));
					}
					if (asyncRequest != null)
					{
						asyncRequest.SetNextRequest(buffer, offset + num, count - num, _SslStream._ResumeAsyncWriteCallback);
						IAsyncResult asyncResult = this._SslState.InnerStream.BeginWrite(array, 0, num2, _SslStream._WriteCallback, asyncRequest);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						this._SslState.InnerStream.EndWrite(asyncResult);
					}
					else
					{
						this._SslState.InnerStream.Write(array, 0, num2);
					}
					offset += num;
					count -= num;
					this._SslState.FinishWrite();
					if (count == 0)
					{
						goto IL_19B;
					}
				}
				return;
			}
			IL_19B:
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser();
			}
			if (buffer == this._PinnableOutputBufferInUse)
			{
				this._PinnableOutputBufferInUse = null;
				if (PinnableBufferCacheEventSource.Log.IsEnabled())
				{
					PinnableBufferCacheEventSource.Log.DebugMessage1("In System.Net._SslStream.StartWriting Freeing buffer.", (long)this.GetHashCode());
				}
			}
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000948E0 File Offset: 0x00092AE0
		private int ProcessRead(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			this.ValidateParameters(buffer, offset, count);
			if (Interlocked.Exchange(ref this._NestedRead, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					(asyncRequest != null) ? "BeginRead" : "Read",
					"read"
				}));
			}
			bool flag = false;
			int result;
			try
			{
				if (this.InternalBufferCount != 0)
				{
					int num = (this.InternalBufferCount > count) ? count : this.InternalBufferCount;
					if (num != 0)
					{
						Buffer.BlockCopy(this.InternalBuffer, this.InternalOffset, buffer, offset, num);
						this.DecrementInternalBufferCount(num);
					}
					if (asyncRequest != null)
					{
						asyncRequest.CompleteUser(num);
					}
					result = num;
				}
				else
				{
					result = this.StartReading(buffer, offset, count, asyncRequest);
				}
			}
			catch (Exception ex)
			{
				this._SslState.FinishRead(null);
				flag = true;
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_read"), ex);
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedRead = 0;
				}
			}
			return result;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000949EC File Offset: 0x00092BEC
		internal void ProcessReadForPoll(byte[] buffer, int offset, int count, int microSeconds)
		{
			this.ValidateParameters(buffer, offset, count);
			if (Interlocked.Exchange(ref this._NestedRead, 1) == 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					"ReadForPoll",
					"read"
				}));
			}
			try
			{
				if (this.InternalBufferCount == 0)
				{
					this.StartReadingWithPoll(buffer, offset, count, microSeconds);
				}
			}
			catch (Exception ex)
			{
				this._SslState.FinishRead(null);
				if (ex is IOException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_read"), ex);
			}
			finally
			{
				this._NestedRead = 0;
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00094A9C File Offset: 0x00092C9C
		private int StartReading(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			int num;
			for (;;)
			{
				if (asyncRequest != null)
				{
					asyncRequest.SetNextRequest(buffer, offset, count, _SslStream._ResumeAsyncReadCallback);
				}
				num = this._SslState.CheckEnqueueRead(buffer, offset, count, asyncRequest);
				if (num == 0)
				{
					break;
				}
				if (num != -1)
				{
					goto Block_2;
				}
				int result;
				if ((result = this.StartFrameHeader(buffer, offset, count, asyncRequest)) != -1)
				{
					return result;
				}
			}
			return 0;
			Block_2:
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser(num);
			}
			return num;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00094AFC File Offset: 0x00092CFC
		private void StartReadingWithPoll(byte[] buffer, int offset, int count, int microSeconds)
		{
			for (;;)
			{
				int num = this._SslState.CheckEnqueueRead(buffer, offset, count, null);
				if (num == 0)
				{
					break;
				}
				if (num != -1)
				{
					return;
				}
				NetworkStream networkStream = this._SslState.InnerStream as NetworkStream;
				if (networkStream != null && !networkStream.Poll(microSeconds, SelectMode.SelectRead))
				{
					return;
				}
				if (this.StartFrameHeader(buffer, offset, count, null) != -1)
				{
					return;
				}
			}
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00094B54 File Offset: 0x00092D54
		private int StartFrameHeader(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			this.EnsureInternalBufferSize(0, 5);
			int readBytes;
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(this.InternalBuffer, 0, 5, _SslStream._ReadHeaderCallback);
				this._Reader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return 0;
				}
				readBytes = asyncRequest.Result;
			}
			else
			{
				readBytes = this._Reader.ReadPacket(this.InternalBuffer, 0, 5);
			}
			return this.StartFrameBody(readBytes, buffer, offset, count, asyncRequest);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00094BC8 File Offset: 0x00092DC8
		private int StartFrameBody(int readBytes, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				this.DecrementInternalBufferCount(this.InternalBufferCount);
				if (asyncRequest != null)
				{
					asyncRequest.CompleteUser(0);
				}
				return 0;
			}
			readBytes = this._SslState.GetRemainingFrameSize(this.InternalBuffer, readBytes);
			if (readBytes < 0)
			{
				throw new IOException(SR.GetString("net_frame_read_size"));
			}
			this.EnsureInternalBufferSize(5, readBytes);
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(this.InternalBuffer, 5, readBytes, _SslStream._ReadFrameCallback);
				this._Reader.AsyncReadPacket(asyncRequest);
				if (!asyncRequest.MustCompleteSynchronously)
				{
					return 0;
				}
				readBytes = asyncRequest.Result;
			}
			else
			{
				readBytes = this._Reader.ReadPacket(this.InternalBuffer, 5, readBytes);
			}
			return this.ProcessFrameBody(readBytes, buffer, offset, count, asyncRequest);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00094C84 File Offset: 0x00092E84
		private int ProcessFrameBody(int readBytes, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (readBytes == 0)
			{
				throw new IOException(SR.GetString("net_io_eof"));
			}
			readBytes += 5;
			int num = 0;
			SecurityStatus securityStatus = this._SslState.DecryptData(this.InternalBuffer, ref num, ref readBytes);
			if (securityStatus != SecurityStatus.OK)
			{
				byte[] array = null;
				if (readBytes != 0)
				{
					array = new byte[readBytes];
					Buffer.BlockCopy(this.InternalBuffer, num, array, 0, readBytes);
				}
				this.DecrementInternalBufferCount(this.InternalBufferCount);
				return this.ProcessReadErrorCode(securityStatus, buffer, offset, count, asyncRequest, array);
			}
			if (readBytes == 0 && count != 0)
			{
				this.DecrementInternalBufferCount(this.InternalBufferCount);
				return -1;
			}
			this.EnsureInternalBufferSize(0, num + readBytes);
			this.DecrementInternalBufferCount(num);
			if (readBytes > count)
			{
				readBytes = count;
			}
			Buffer.BlockCopy(this.InternalBuffer, this.InternalOffset, buffer, offset, readBytes);
			this.DecrementInternalBufferCount(readBytes);
			this._SslState.FinishRead(null);
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser(readBytes);
			}
			return readBytes;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00094D64 File Offset: 0x00092F64
		private int ProcessReadErrorCode(SecurityStatus errorCode, byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest, byte[] extraBuffer)
		{
			ProtocolToken protocolToken = new ProtocolToken(null, errorCode);
			if (protocolToken.Renegotiate)
			{
				this._SslState.ReplyOnReAuthentication(extraBuffer);
				return -1;
			}
			if (protocolToken.CloseConnection)
			{
				this._SslState.FinishRead(null);
				if (asyncRequest != null)
				{
					asyncRequest.CompleteUser(0);
				}
				return 0;
			}
			throw new IOException(SR.GetString("net_io_decrypt"), protocolToken.GetException());
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00094DCC File Offset: 0x00092FCC
		private static void WriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			AsyncProtocolRequest asyncProtocolRequest = (AsyncProtocolRequest)transportResult.AsyncState;
			_SslStream sslStream = (_SslStream)asyncProtocolRequest.AsyncObject;
			try
			{
				sslStream._SslState.InnerStream.EndWrite(transportResult);
				sslStream._SslState.FinishWrite();
				if (asyncProtocolRequest.Count == 0)
				{
					asyncProtocolRequest.Count = -1;
				}
				sslStream.StartWriting(asyncProtocolRequest.Buffer, asyncProtocolRequest.Offset, asyncProtocolRequest.Count, asyncProtocolRequest);
			}
			catch (Exception e)
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslStream._SslState.FinishWrite();
				asyncProtocolRequest.CompleteWithError(e);
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00094E70 File Offset: 0x00093070
		private static void MulitpleWriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			_SslStream.SplitWriteAsyncProtocolRequest splitWriteAsyncProtocolRequest = (_SslStream.SplitWriteAsyncProtocolRequest)transportResult.AsyncState;
			_SslStream sslStream = (_SslStream)splitWriteAsyncProtocolRequest.AsyncObject;
			try
			{
				((NetworkStream)sslStream._SslState.InnerStream).EndMultipleWrite(transportResult);
				sslStream._SslState.FinishWrite();
				sslStream.StartWriting(splitWriteAsyncProtocolRequest.SplitWritesState, splitWriteAsyncProtocolRequest);
			}
			catch (Exception e)
			{
				if (splitWriteAsyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslStream._SslState.FinishWrite();
				splitWriteAsyncProtocolRequest.CompleteWithError(e);
			}
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00094F00 File Offset: 0x00093100
		private static void ResumeAsyncReadCallback(AsyncProtocolRequest request)
		{
			try
			{
				((_SslStream)request.AsyncObject).StartReading(request.Buffer, request.Offset, request.Count, request);
			}
			catch (Exception e)
			{
				if (request.IsUserCompleted)
				{
					throw;
				}
				((_SslStream)request.AsyncObject)._SslState.FinishRead(null);
				request.CompleteWithError(e);
			}
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00094F70 File Offset: 0x00093170
		private static void ResumeAsyncWriteCallback(AsyncProtocolRequest asyncRequest)
		{
			try
			{
				_SslStream.SplitWriteAsyncProtocolRequest splitWriteAsyncProtocolRequest = asyncRequest as _SslStream.SplitWriteAsyncProtocolRequest;
				if (splitWriteAsyncProtocolRequest != null)
				{
					((_SslStream)asyncRequest.AsyncObject).StartWriting(splitWriteAsyncProtocolRequest.SplitWritesState, splitWriteAsyncProtocolRequest);
				}
				else
				{
					((_SslStream)asyncRequest.AsyncObject).StartWriting(asyncRequest.Buffer, asyncRequest.Offset, asyncRequest.Count, asyncRequest);
				}
			}
			catch (Exception e)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				((_SslStream)asyncRequest.AsyncObject)._SslState.FinishWrite();
				asyncRequest.CompleteWithError(e);
			}
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00095000 File Offset: 0x00093200
		private static void ReadHeaderCallback(AsyncProtocolRequest asyncRequest)
		{
			try
			{
				_SslStream sslStream = (_SslStream)asyncRequest.AsyncObject;
				BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)asyncRequest.UserAsyncResult;
				if (-1 == sslStream.StartFrameBody(asyncRequest.Result, bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest))
				{
					sslStream.StartReading(bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest);
				}
			}
			catch (Exception e)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(e);
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00095088 File Offset: 0x00093288
		private static void ReadFrameCallback(AsyncProtocolRequest asyncRequest)
		{
			try
			{
				_SslStream sslStream = (_SslStream)asyncRequest.AsyncObject;
				BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)asyncRequest.UserAsyncResult;
				if (-1 == sslStream.ProcessFrameBody(asyncRequest.Result, bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest))
				{
					sslStream.StartReading(bufferAsyncResult.Buffer, bufferAsyncResult.Offset, bufferAsyncResult.Count, asyncRequest);
				}
			}
			catch (Exception e)
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(e);
			}
		}

		// Token: 0x04001D4C RID: 7500
		private static AsyncCallback _WriteCallback = new AsyncCallback(_SslStream.WriteCallback);

		// Token: 0x04001D4D RID: 7501
		private static AsyncCallback _MulitpleWriteCallback = new AsyncCallback(_SslStream.MulitpleWriteCallback);

		// Token: 0x04001D4E RID: 7502
		private static AsyncProtocolCallback _ResumeAsyncWriteCallback = new AsyncProtocolCallback(_SslStream.ResumeAsyncWriteCallback);

		// Token: 0x04001D4F RID: 7503
		private static AsyncProtocolCallback _ResumeAsyncReadCallback = new AsyncProtocolCallback(_SslStream.ResumeAsyncReadCallback);

		// Token: 0x04001D50 RID: 7504
		private static AsyncProtocolCallback _ReadHeaderCallback = new AsyncProtocolCallback(_SslStream.ReadHeaderCallback);

		// Token: 0x04001D51 RID: 7505
		private static AsyncProtocolCallback _ReadFrameCallback = new AsyncProtocolCallback(_SslStream.ReadFrameCallback);

		// Token: 0x04001D52 RID: 7506
		private const int PinnableReadBufferSize = 16416;

		// Token: 0x04001D53 RID: 7507
		private static PinnableBufferCache s_PinnableReadBufferCache = new PinnableBufferCache("System.Net.SslStream", 16416);

		// Token: 0x04001D54 RID: 7508
		private const int PinnableWriteBufferSize = 5120;

		// Token: 0x04001D55 RID: 7509
		private static PinnableBufferCache s_PinnableWriteBufferCache = new PinnableBufferCache("System.Net.SslStream", 5120);

		// Token: 0x04001D56 RID: 7510
		private SslState _SslState;

		// Token: 0x04001D57 RID: 7511
		private int _NestedWrite;

		// Token: 0x04001D58 RID: 7512
		private int _NestedRead;

		// Token: 0x04001D59 RID: 7513
		private byte[] _InternalBuffer;

		// Token: 0x04001D5A RID: 7514
		private bool _InternalBufferFromPinnableCache;

		// Token: 0x04001D5B RID: 7515
		private byte[] _PinnableOutputBuffer;

		// Token: 0x04001D5C RID: 7516
		private byte[] _PinnableOutputBufferInUse;

		// Token: 0x04001D5D RID: 7517
		private int _InternalOffset;

		// Token: 0x04001D5E RID: 7518
		private int _InternalBufferCount;

		// Token: 0x04001D5F RID: 7519
		private FixedSizeReader _Reader;

		// Token: 0x020007D6 RID: 2006
		private class SplitWriteAsyncProtocolRequest : AsyncProtocolRequest
		{
			// Token: 0x060043B8 RID: 17336 RVA: 0x0011D6EA File Offset: 0x0011B8EA
			internal SplitWriteAsyncProtocolRequest(LazyAsyncResult userAsyncResult) : base(userAsyncResult)
			{
			}

			// Token: 0x060043B9 RID: 17337 RVA: 0x0011D6F3 File Offset: 0x0011B8F3
			internal void SetNextRequest(SplitWritesState splitWritesState, AsyncProtocolCallback callback)
			{
				this.SplitWritesState = splitWritesState;
				base.SetNextRequest(null, 0, 0, callback);
			}

			// Token: 0x040034B3 RID: 13491
			internal SplitWritesState SplitWritesState;
		}
	}
}
