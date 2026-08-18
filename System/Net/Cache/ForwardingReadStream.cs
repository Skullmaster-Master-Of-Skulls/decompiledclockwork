using System;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200057C RID: 1404
	internal class ForwardingReadStream : Stream, ICloseEx
	{
		// Token: 0x06002AD6 RID: 10966 RVA: 0x000B6284 File Offset: 0x000B5284
		internal ForwardingReadStream(Stream originalStream, Stream shadowStream, long bytesToSkip, bool throwOnWriteError)
		{
			if (!shadowStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("net_cache_shadowstream_not_writable"), "shadowStream");
			}
			this.m_OriginalStream = originalStream;
			this.m_ShadowStream = shadowStream;
			this.m_BytesToSkip = bytesToSkip;
			this.m_ThrowOnWriteError = throwOnWriteError;
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000B62D1 File Offset: 0x000B52D1
		public override bool CanRead
		{
			get
			{
				return this.m_OriginalStream.CanRead;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x000B62DE File Offset: 0x000B52DE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000B62E1 File Offset: 0x000B52E1
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000B62E4 File Offset: 0x000B52E4
		public override long Length
		{
			get
			{
				return this.m_OriginalStream.Length - this.m_BytesToSkip;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000B62F8 File Offset: 0x000B52F8
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000B630C File Offset: 0x000B530C
		public override long Position
		{
			get
			{
				return this.m_OriginalStream.Position - this.m_BytesToSkip;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000B631D File Offset: 0x000B531D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000B632E File Offset: 0x000B532E
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000B633F File Offset: 0x000B533F
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000B6350 File Offset: 0x000B5350
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000B6361 File Offset: 0x000B5361
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000B6372 File Offset: 0x000B5372
		public override void Flush()
		{
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000B6374 File Offset: 0x000B5374
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			int num = -1;
			if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					"Read",
					"read"
				}));
			}
			int result;
			try
			{
				if (this.m_BytesToSkip != 0L)
				{
					byte[] array = new byte[4096];
					while (this.m_BytesToSkip != 0L)
					{
						int num2 = this.m_OriginalStream.Read(array, 0, (this.m_BytesToSkip < (long)array.Length) ? ((int)this.m_BytesToSkip) : array.Length);
						if (num2 == 0)
						{
							this.m_SeenReadEOF = true;
						}
						this.m_BytesToSkip -= (long)num2;
						if (!this.m_ShadowStreamIsDead)
						{
							this.m_ShadowStream.Write(array, 0, num2);
						}
					}
				}
				num = this.m_OriginalStream.Read(buffer, offset, count);
				if (num == 0)
				{
					this.m_SeenReadEOF = true;
				}
				if (this.m_ShadowStreamIsDead)
				{
					result = num;
				}
				else
				{
					flag = true;
					this.m_ShadowStream.Write(buffer, offset, num);
					result = num;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!this.m_ShadowStreamIsDead)
				{
					this.m_ShadowStreamIsDead = true;
					try
					{
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
					}
					catch
					{
					}
				}
				if (!flag || this.m_ThrowOnWriteError)
				{
					throw;
				}
				result = num;
			}
			catch
			{
				if (!this.m_ShadowStreamIsDead)
				{
					this.m_ShadowStreamIsDead = true;
					try
					{
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception exception)
					{
						if (NclUtilities.IsFatal(exception))
						{
							throw;
						}
					}
					catch
					{
					}
				}
				if (!flag || this.m_ThrowOnWriteError)
				{
					throw;
				}
				result = num;
			}
			finally
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
			}
			return result;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000B6624 File Offset: 0x000B5624
		private void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			object asyncState = transportResult.AsyncState;
			this.ReadComplete(transportResult);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000B6640 File Offset: 0x000B5640
		private void ReadComplete(IAsyncResult transportResult)
		{
			for (;;)
			{
				ForwardingReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as ForwardingReadStream.InnerAsyncResult;
				try
				{
					if (!innerAsyncResult.IsWriteCompletion)
					{
						innerAsyncResult.Count = this.m_OriginalStream.EndRead(transportResult);
						if (innerAsyncResult.Count == 0)
						{
							this.m_SeenReadEOF = true;
						}
						if (!this.m_ShadowStreamIsDead)
						{
							innerAsyncResult.IsWriteCompletion = true;
							transportResult = this.m_ShadowStream.BeginWrite(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
							if (transportResult.CompletedSynchronously)
							{
								continue;
							}
							break;
						}
					}
					else
					{
						this.m_ShadowStream.EndWrite(transportResult);
						innerAsyncResult.IsWriteCompletion = false;
					}
				}
				catch (Exception result)
				{
					if (innerAsyncResult.InternalPeekCompleted)
					{
						throw;
					}
					try
					{
						this.m_ShadowStreamIsDead = true;
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception)
					{
					}
					catch
					{
					}
					if (!innerAsyncResult.IsWriteCompletion || this.m_ThrowOnWriteError)
					{
						if (transportResult.CompletedSynchronously)
						{
							throw;
						}
						innerAsyncResult.InvokeCallback(result);
						break;
					}
				}
				catch
				{
					if (innerAsyncResult.InternalPeekCompleted)
					{
						throw;
					}
					try
					{
						this.m_ShadowStreamIsDead = true;
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception)
					{
					}
					catch
					{
					}
					if (!innerAsyncResult.IsWriteCompletion || this.m_ThrowOnWriteError)
					{
						if (transportResult.CompletedSynchronously)
						{
							throw;
						}
						innerAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
						break;
					}
				}
				try
				{
					if (this.m_BytesToSkip != 0L)
					{
						this.m_BytesToSkip -= (long)innerAsyncResult.Count;
						innerAsyncResult.Count = ((this.m_BytesToSkip < (long)innerAsyncResult.Buffer.Length) ? ((int)this.m_BytesToSkip) : innerAsyncResult.Buffer.Length);
						if (this.m_BytesToSkip == 0L)
						{
							transportResult = innerAsyncResult;
							innerAsyncResult = (innerAsyncResult.AsyncState as ForwardingReadStream.InnerAsyncResult);
						}
						transportResult = this.m_OriginalStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
						if (transportResult.CompletedSynchronously)
						{
							continue;
						}
					}
					else
					{
						innerAsyncResult.InvokeCallback(innerAsyncResult.Count);
					}
				}
				catch (Exception result2)
				{
					if (innerAsyncResult.InternalPeekCompleted)
					{
						throw;
					}
					try
					{
						this.m_ShadowStreamIsDead = true;
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception)
					{
					}
					catch
					{
					}
					if (transportResult.CompletedSynchronously)
					{
						throw;
					}
					innerAsyncResult.InvokeCallback(result2);
				}
				catch
				{
					if (innerAsyncResult.InternalPeekCompleted)
					{
						throw;
					}
					try
					{
						this.m_ShadowStreamIsDead = true;
						if (this.m_ShadowStream is ICloseEx)
						{
							((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						}
						else
						{
							this.m_ShadowStream.Close();
						}
					}
					catch (Exception)
					{
					}
					catch
					{
					}
					if (transportResult.CompletedSynchronously)
					{
						throw;
					}
					innerAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
				break;
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000B69C8 File Offset: 0x000B59C8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
			{
				throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
				{
					"BeginRead",
					"read"
				}));
			}
			IAsyncResult result;
			try
			{
				if (this.m_ReadCallback == null)
				{
					this.m_ReadCallback = new AsyncCallback(this.ReadCallback);
				}
				if (this.m_ShadowStreamIsDead && this.m_BytesToSkip == 0L)
				{
					result = this.m_OriginalStream.BeginRead(buffer, offset, count, callback, state);
				}
				else
				{
					ForwardingReadStream.InnerAsyncResult innerAsyncResult = new ForwardingReadStream.InnerAsyncResult(state, callback, buffer, offset, count);
					if (this.m_BytesToSkip != 0L)
					{
						ForwardingReadStream.InnerAsyncResult userState = innerAsyncResult;
						innerAsyncResult = new ForwardingReadStream.InnerAsyncResult(userState, null, new byte[4096], 0, (this.m_BytesToSkip < (long)buffer.Length) ? ((int)this.m_BytesToSkip) : buffer.Length);
					}
					IAsyncResult asyncResult = this.m_OriginalStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
					if (asyncResult.CompletedSynchronously)
					{
						this.ReadComplete(asyncResult);
					}
					result = innerAsyncResult;
				}
			}
			catch
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
				throw;
			}
			return result;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000B6AF0 File Offset: 0x000B5AF0
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (Interlocked.Decrement(ref this.m_ReadNesting) != 0)
			{
				Interlocked.Increment(ref this.m_ReadNesting);
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndRead"
				}));
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ForwardingReadStream.InnerAsyncResult innerAsyncResult = asyncResult as ForwardingReadStream.InnerAsyncResult;
			if (innerAsyncResult == null && this.m_OriginalStream.EndRead(asyncResult) == 0)
			{
				this.m_SeenReadEOF = true;
			}
			bool flag = false;
			try
			{
				innerAsyncResult.InternalWaitForCompletion();
				if (innerAsyncResult.Result is Exception)
				{
					throw (Exception)innerAsyncResult.Result;
				}
				flag = true;
			}
			finally
			{
				if (!flag && !this.m_ShadowStreamIsDead)
				{
					this.m_ShadowStreamIsDead = true;
					if (this.m_ShadowStream is ICloseEx)
					{
						((ICloseEx)this.m_ShadowStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
					}
					else
					{
						this.m_ShadowStream.Close();
					}
				}
			}
			return (int)innerAsyncResult.Result;
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000B6BE4 File Offset: 0x000B5BE4
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000B6BF4 File Offset: 0x000B5BF4
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if (Interlocked.Increment(ref this._Disposed) == 1)
			{
				if (closeState == CloseExState.Silent)
				{
					try
					{
						int num = 0;
						int num2;
						while (num < ConnectStream.s_DrainingBuffer.Length && (num2 = this.Read(ConnectStream.s_DrainingBuffer, 0, ConnectStream.s_DrainingBuffer.Length)) > 0)
						{
							num += num2;
						}
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
					}
					catch
					{
					}
				}
				this.Dispose(true, closeState);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000B6C8C File Offset: 0x000B5C8C
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				ICloseEx closeEx = this.m_OriginalStream as ICloseEx;
				if (closeEx != null)
				{
					closeEx.CloseEx(closeState);
				}
				else
				{
					this.m_OriginalStream.Close();
				}
			}
			finally
			{
				if (!this.m_SeenReadEOF)
				{
					closeState |= CloseExState.Abort;
				}
				if (this.m_ShadowStream is ICloseEx)
				{
					((ICloseEx)this.m_ShadowStream).CloseEx(closeState);
				}
				else
				{
					this.m_ShadowStream.Close();
				}
			}
			if (!disposing)
			{
				this.m_OriginalStream = null;
				this.m_ShadowStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000B6D20 File Offset: 0x000B5D20
		public override bool CanTimeout
		{
			get
			{
				return this.m_OriginalStream.CanTimeout && this.m_ShadowStream.CanTimeout;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000B6D3C File Offset: 0x000B5D3C
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x000B6D4C File Offset: 0x000B5D4C
		public override int ReadTimeout
		{
			get
			{
				return this.m_OriginalStream.ReadTimeout;
			}
			set
			{
				Stream originalStream = this.m_OriginalStream;
				this.m_ShadowStream.ReadTimeout = value;
				originalStream.ReadTimeout = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000B6D73 File Offset: 0x000B5D73
		// (set) Token: 0x06002AEF RID: 10991 RVA: 0x000B6D80 File Offset: 0x000B5D80
		public override int WriteTimeout
		{
			get
			{
				return this.m_ShadowStream.WriteTimeout;
			}
			set
			{
				Stream originalStream = this.m_OriginalStream;
				this.m_ShadowStream.WriteTimeout = value;
				originalStream.WriteTimeout = value;
			}
		}

		// Token: 0x04002992 RID: 10642
		private Stream m_OriginalStream;

		// Token: 0x04002993 RID: 10643
		private Stream m_ShadowStream;

		// Token: 0x04002994 RID: 10644
		private int m_ReadNesting;

		// Token: 0x04002995 RID: 10645
		private bool m_ShadowStreamIsDead;

		// Token: 0x04002996 RID: 10646
		private AsyncCallback m_ReadCallback;

		// Token: 0x04002997 RID: 10647
		private long m_BytesToSkip;

		// Token: 0x04002998 RID: 10648
		private bool m_ThrowOnWriteError;

		// Token: 0x04002999 RID: 10649
		private bool m_SeenReadEOF;

		// Token: 0x0400299A RID: 10650
		private int _Disposed;

		// Token: 0x0200057D RID: 1405
		private class InnerAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002AF0 RID: 10992 RVA: 0x000B6DA7 File Offset: 0x000B5DA7
			public InnerAsyncResult(object userState, AsyncCallback userCallback, byte[] buffer, int offset, int count) : base(null, userState, userCallback)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
			}

			// Token: 0x0400299B RID: 10651
			public byte[] Buffer;

			// Token: 0x0400299C RID: 10652
			public int Offset;

			// Token: 0x0400299D RID: 10653
			public int Count;

			// Token: 0x0400299E RID: 10654
			public bool IsWriteCompletion;
		}
	}
}
