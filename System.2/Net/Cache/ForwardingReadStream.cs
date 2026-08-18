using System;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031C RID: 796
	internal class ForwardingReadStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C70 RID: 7280 RVA: 0x00087177 File Offset: 0x00085377
		internal ForwardingReadStream(Stream originalStream, Stream shadowStream, long bytesToSkip, bool throwOnWriteError) : base(originalStream)
		{
			if (!shadowStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("net_cache_shadowstream_not_writable"), "shadowStream");
			}
			this.m_ShadowStream = shadowStream;
			this.m_BytesToSkip = bytesToSkip;
			this.m_ThrowOnWriteError = throwOnWriteError;
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x000871B3 File Offset: 0x000853B3
		public override bool CanRead
		{
			get
			{
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000871C0 File Offset: 0x000853C0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000871C3 File Offset: 0x000853C3
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000871C6 File Offset: 0x000853C6
		public override long Length
		{
			get
			{
				return base.WrappedStream.Length - this.m_BytesToSkip;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x000871DA File Offset: 0x000853DA
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x000871EE File Offset: 0x000853EE
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position - this.m_BytesToSkip;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000871FF File Offset: 0x000853FF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00087210 File Offset: 0x00085410
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00087221 File Offset: 0x00085421
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00087232 File Offset: 0x00085432
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00087243 File Offset: 0x00085443
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00087254 File Offset: 0x00085454
		public override void Flush()
		{
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00087258 File Offset: 0x00085458
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
						int num2 = base.WrappedStream.Read(array, 0, (this.m_BytesToSkip < (long)array.Length) ? ((int)this.m_BytesToSkip) : array.Length);
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
				num = base.WrappedStream.Read(buffer, offset, count);
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
					catch (Exception ex2)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
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

		// Token: 0x06001C7E RID: 7294 RVA: 0x0008743C File Offset: 0x0008563C
		private void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			ForwardingReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as ForwardingReadStream.InnerAsyncResult;
			this.ReadComplete(transportResult);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00087468 File Offset: 0x00085668
		private void ReadComplete(IAsyncResult transportResult)
		{
			for (;;)
			{
				ForwardingReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as ForwardingReadStream.InnerAsyncResult;
				try
				{
					if (!innerAsyncResult.IsWriteCompletion)
					{
						innerAsyncResult.Count = base.WrappedStream.EndRead(transportResult);
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
					catch (Exception ex)
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
						transportResult = base.WrappedStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
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
					catch (Exception ex2)
					{
					}
					if (transportResult.CompletedSynchronously)
					{
						throw;
					}
					innerAsyncResult.InvokeCallback(result2);
				}
				break;
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000876A0 File Offset: 0x000858A0
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
					result = base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
				}
				else
				{
					ForwardingReadStream.InnerAsyncResult innerAsyncResult = new ForwardingReadStream.InnerAsyncResult(state, callback, buffer, offset, count);
					if (this.m_BytesToSkip != 0L)
					{
						ForwardingReadStream.InnerAsyncResult userState = innerAsyncResult;
						innerAsyncResult = new ForwardingReadStream.InnerAsyncResult(userState, null, new byte[4096], 0, (this.m_BytesToSkip < (long)buffer.Length) ? ((int)this.m_BytesToSkip) : buffer.Length);
					}
					IAsyncResult asyncResult = base.WrappedStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
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

		// Token: 0x06001C81 RID: 7297 RVA: 0x000877C0 File Offset: 0x000859C0
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
			if (innerAsyncResult == null && base.WrappedStream.EndRead(asyncResult) == 0)
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

		// Token: 0x06001C82 RID: 7298 RVA: 0x000878B4 File Offset: 0x00085AB4
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000878C0 File Offset: 0x00085AC0
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
				}
				this.Dispose(true, closeState);
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00087940 File Offset: 0x00085B40
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (disposing)
				{
					try
					{
						ICloseEx closeEx = base.WrappedStream as ICloseEx;
						if (closeEx != null)
						{
							closeEx.CloseEx(closeState);
						}
						else
						{
							base.WrappedStream.Close();
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
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000879D4 File Offset: 0x00085BD4
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout && this.m_ShadowStream.CanTimeout;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000879F0 File Offset: 0x00085BF0
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x00087A00 File Offset: 0x00085C00
		public override int ReadTimeout
		{
			get
			{
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_ShadowStream.ReadTimeout = value;
				wrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00087A27 File Offset: 0x00085C27
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x00087A34 File Offset: 0x00085C34
		public override int WriteTimeout
		{
			get
			{
				return this.m_ShadowStream.WriteTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_ShadowStream.WriteTimeout = value;
				wrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x04001B9D RID: 7069
		private Stream m_ShadowStream;

		// Token: 0x04001B9E RID: 7070
		private int m_ReadNesting;

		// Token: 0x04001B9F RID: 7071
		private bool m_ShadowStreamIsDead;

		// Token: 0x04001BA0 RID: 7072
		private AsyncCallback m_ReadCallback;

		// Token: 0x04001BA1 RID: 7073
		private long m_BytesToSkip;

		// Token: 0x04001BA2 RID: 7074
		private bool m_ThrowOnWriteError;

		// Token: 0x04001BA3 RID: 7075
		private bool m_SeenReadEOF;

		// Token: 0x04001BA4 RID: 7076
		private int _Disposed;

		// Token: 0x020007BA RID: 1978
		private class InnerAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004341 RID: 17217 RVA: 0x0011A05E File Offset: 0x0011825E
			public InnerAsyncResult(object userState, AsyncCallback userCallback, byte[] buffer, int offset, int count) : base(null, userState, userCallback)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
			}

			// Token: 0x0400345B RID: 13403
			public byte[] Buffer;

			// Token: 0x0400345C RID: 13404
			public int Offset;

			// Token: 0x0400345D RID: 13405
			public int Count;

			// Token: 0x0400345E RID: 13406
			public bool IsWriteCompletion;
		}
	}
}
