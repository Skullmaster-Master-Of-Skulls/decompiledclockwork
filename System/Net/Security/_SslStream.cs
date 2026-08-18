using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x020005A2 RID: 1442
	internal class _SslStream
	{
		// Token: 0x06002CAC RID: 11436 RVA: 0x000C0AA3 File Offset: 0x000BFAA3
		internal _SslStream(SslState sslState)
		{
			this._SslState = sslState;
			this._Reader = new FixedSizeReader(this._SslState.InnerStream);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000C0AC8 File Offset: 0x000BFAC8
		internal int Read(byte[] buffer, int offset, int count)
		{
			return this.ProcessRead(buffer, offset, count, null);
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000C0AD4 File Offset: 0x000BFAD4
		internal void Write(byte[] buffer, int offset, int count)
		{
			this.ProcessWrite(buffer, offset, count, null);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000C0AE0 File Offset: 0x000BFAE0
		internal void Write(BufferOffsetSize[] buffers)
		{
			this.ProcessWrite(buffers, null);
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000C0AEC File Offset: 0x000BFAEC
		internal IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, count, asyncState, asyncCallback);
			AsyncProtocolRequest asyncRequest = new AsyncProtocolRequest(bufferAsyncResult);
			this.ProcessRead(buffer, offset, count, asyncRequest);
			return bufferAsyncResult;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000C0B1C File Offset: 0x000BFB1C
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
			throw new IOException(SR.GetString("net_io_write"), (Exception)bufferAsyncResult.Result);
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000C0BF0 File Offset: 0x000BFBF0
		internal IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, asyncState, asyncCallback);
			AsyncProtocolRequest asyncRequest = new AsyncProtocolRequest(lazyAsyncResult);
			this.ProcessWrite(buffer, offset, count, asyncRequest);
			return lazyAsyncResult;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000C0C1C File Offset: 0x000BFC1C
		internal IAsyncResult BeginWrite(BufferOffsetSize[] buffers, AsyncCallback asyncCallback, object asyncState)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, asyncState, asyncCallback);
			_SslStream.SplitWriteAsyncProtocolRequest asyncRequest = new _SslStream.SplitWriteAsyncProtocolRequest(lazyAsyncResult);
			this.ProcessWrite(buffers, asyncRequest);
			return lazyAsyncResult;
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000C0C44 File Offset: 0x000BFC44
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

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000C0D0A File Offset: 0x000BFD0A
		internal bool DataAvailable
		{
			get
			{
				return this.InternalBufferCount != 0;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000C0D18 File Offset: 0x000BFD18
		private byte[] InternalBuffer
		{
			get
			{
				return this._InternalBuffer;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000C0D20 File Offset: 0x000BFD20
		private int InternalOffset
		{
			get
			{
				return this._InternalOffset;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000C0D28 File Offset: 0x000BFD28
		private int InternalBufferCount
		{
			get
			{
				return this._InternalBufferCount;
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000C0D30 File Offset: 0x000BFD30
		private void DecrementInternalBufferCount(int decrCount)
		{
			this._InternalOffset += decrCount;
			this._InternalBufferCount -= decrCount;
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000C0D50 File Offset: 0x000BFD50
		private void EnsureInternalBufferSize(int curOffset, int addSize)
		{
			if (this._InternalBuffer == null || this._InternalBuffer.Length < addSize + curOffset)
			{
				byte[] internalBuffer = this._InternalBuffer;
				this._InternalBuffer = new byte[addSize + curOffset];
				if (internalBuffer != null && curOffset != 0)
				{
					Buffer.BlockCopy(internalBuffer, 0, this._InternalBuffer, 0, curOffset);
				}
			}
			this._InternalOffset = curOffset;
			this._InternalBufferCount = curOffset + addSize;
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000C0DAC File Offset: 0x000BFDAC
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
				throw new ArgumentOutOfRangeException(SR.GetString("net_offset_plus_count"));
			}
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000C0E00 File Offset: 0x000BFE00
		private void ProcessWrite(BufferOffsetSize[] buffers, _SslStream.SplitWriteAsyncProtocolRequest asyncRequest)
		{
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
			catch
			{
				this._SslState.FinishWrite();
				flag = true;
				throw new IOException(SR.GetString("net_io_write"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			finally
			{
				if (asyncRequest == null || flag)
				{
					this._NestedWrite = 0;
				}
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000C0F38 File Offset: 0x000BFF38
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
				catch
				{
					this._SslState.FinishWrite();
					flag = true;
					throw new IOException(SR.GetString("net_io_write"), new Exception(SR.GetString("net_nonClsCompliantException")));
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

		// Token: 0x06002CBE RID: 11454 RVA: 0x000C1070 File Offset: 0x000C0070
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

		// Token: 0x06002CBF RID: 11455 RVA: 0x000C1140 File Offset: 0x000C0140
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

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000C1308 File Offset: 0x000C0308
		private void StartWriting(byte[] buffer, int offset, int count, AsyncProtocolRequest asyncRequest)
		{
			if (asyncRequest != null)
			{
				asyncRequest.SetNextRequest(buffer, offset, count, _SslStream._ResumeAsyncWriteCallback);
			}
			if (count >= 0)
			{
				byte[] buffer2 = null;
				while (!this._SslState.CheckEnqueueWrite(asyncRequest))
				{
					int num = Math.Min(count, this._SslState.MaxDataSize);
					int count2;
					SecurityStatus securityStatus = this._SslState.EncryptData(buffer, offset, num, ref buffer2, out count2);
					if (securityStatus != SecurityStatus.OK)
					{
						ProtocolToken protocolToken = new ProtocolToken(null, securityStatus);
						throw new IOException(SR.GetString("net_io_encrypt"), protocolToken.GetException());
					}
					if (asyncRequest != null)
					{
						asyncRequest.SetNextRequest(buffer, offset + num, count - num, _SslStream._ResumeAsyncWriteCallback);
						IAsyncResult asyncResult = this._SslState.InnerStream.BeginWrite(buffer2, 0, count2, _SslStream._WriteCallback, asyncRequest);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						this._SslState.InnerStream.EndWrite(asyncResult);
					}
					else
					{
						this._SslState.InnerStream.Write(buffer2, 0, count2);
					}
					offset += num;
					count -= num;
					this._SslState.FinishWrite();
					if (count == 0)
					{
						goto IL_F3;
					}
				}
				return;
			}
			IL_F3:
			if (asyncRequest != null)
			{
				asyncRequest.CompleteUser();
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000C1414 File Offset: 0x000C0414
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
			catch
			{
				this._SslState.FinishRead(null);
				flag = true;
				throw new IOException(SR.GetString("net_io_read"), new Exception(SR.GetString("net_nonClsCompliantException")));
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

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000C1560 File Offset: 0x000C0560
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

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000C15C0 File Offset: 0x000C05C0
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

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000C1634 File Offset: 0x000C0634
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

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000C16F0 File Offset: 0x000C06F0
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

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000C17D0 File Offset: 0x000C07D0
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

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000C1838 File Offset: 0x000C0838
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
			catch
			{
				if (asyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslStream._SslState.FinishWrite();
				asyncProtocolRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000C1914 File Offset: 0x000C0914
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
			catch
			{
				if (splitWriteAsyncProtocolRequest.IsUserCompleted)
				{
					throw;
				}
				sslStream._SslState.FinishWrite();
				splitWriteAsyncProtocolRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000C19DC File Offset: 0x000C09DC
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
			catch
			{
				if (request.IsUserCompleted)
				{
					throw;
				}
				((_SslStream)request.AsyncObject)._SslState.FinishRead(null);
				request.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000C1A90 File Offset: 0x000C0A90
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
			catch
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				((_SslStream)asyncRequest.AsyncObject)._SslState.FinishWrite();
				asyncRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000C1B64 File Offset: 0x000C0B64
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
			catch
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000C1C1C File Offset: 0x000C0C1C
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
			catch
			{
				if (asyncRequest.IsUserCompleted)
				{
					throw;
				}
				asyncRequest.CompleteWithError(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x04002A63 RID: 10851
		private static AsyncCallback _WriteCallback = new AsyncCallback(_SslStream.WriteCallback);

		// Token: 0x04002A64 RID: 10852
		private static AsyncCallback _MulitpleWriteCallback = new AsyncCallback(_SslStream.MulitpleWriteCallback);

		// Token: 0x04002A65 RID: 10853
		private static AsyncProtocolCallback _ResumeAsyncWriteCallback = new AsyncProtocolCallback(_SslStream.ResumeAsyncWriteCallback);

		// Token: 0x04002A66 RID: 10854
		private static AsyncProtocolCallback _ResumeAsyncReadCallback = new AsyncProtocolCallback(_SslStream.ResumeAsyncReadCallback);

		// Token: 0x04002A67 RID: 10855
		private static AsyncProtocolCallback _ReadHeaderCallback = new AsyncProtocolCallback(_SslStream.ReadHeaderCallback);

		// Token: 0x04002A68 RID: 10856
		private static AsyncProtocolCallback _ReadFrameCallback = new AsyncProtocolCallback(_SslStream.ReadFrameCallback);

		// Token: 0x04002A69 RID: 10857
		private SslState _SslState;

		// Token: 0x04002A6A RID: 10858
		private int _NestedWrite;

		// Token: 0x04002A6B RID: 10859
		private int _NestedRead;

		// Token: 0x04002A6C RID: 10860
		private byte[] _InternalBuffer;

		// Token: 0x04002A6D RID: 10861
		private int _InternalOffset;

		// Token: 0x04002A6E RID: 10862
		private int _InternalBufferCount;

		// Token: 0x04002A6F RID: 10863
		private FixedSizeReader _Reader;

		// Token: 0x020005A3 RID: 1443
		private class SplitWriteAsyncProtocolRequest : AsyncProtocolRequest
		{
			// Token: 0x06002CCE RID: 11470 RVA: 0x000C1D47 File Offset: 0x000C0D47
			internal SplitWriteAsyncProtocolRequest(LazyAsyncResult userAsyncResult) : base(userAsyncResult)
			{
			}

			// Token: 0x06002CCF RID: 11471 RVA: 0x000C1D50 File Offset: 0x000C0D50
			internal void SetNextRequest(SplitWritesState splitWritesState, AsyncProtocolCallback callback)
			{
				this.SplitWritesState = splitWritesState;
				base.SetNextRequest(null, 0, 0, callback);
			}

			// Token: 0x04002A70 RID: 10864
			internal SplitWritesState SplitWritesState;
		}
	}
}
