using System;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031B RID: 795
	internal class CombinedReadStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C57 RID: 7255 RVA: 0x00086B90 File Offset: 0x00084D90
		internal CombinedReadStream(Stream headStream, Stream tailStream) : base(tailStream)
		{
			this.m_HeadStream = headStream;
			this.m_HeadEOF = (headStream == Stream.Null);
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00086BAE File Offset: 0x00084DAE
		public override bool CanRead
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.CanRead;
				}
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00086BCF File Offset: 0x00084DCF
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00086BD2 File Offset: 0x00084DD2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00086BD5 File Offset: 0x00084DD5
		public override long Length
		{
			get
			{
				return base.WrappedStream.Length + (this.m_HeadEOF ? this.m_HeadLength : this.m_HeadStream.Length);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00086BFE File Offset: 0x00084DFE
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00086C27 File Offset: 0x00084E27
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position + (this.m_HeadEOF ? this.m_HeadLength : this.m_HeadStream.Position);
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00086C38 File Offset: 0x00084E38
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00086C49 File Offset: 0x00084E49
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00086C5A File Offset: 0x00084E5A
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00086C6B File Offset: 0x00084E6B
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00086C7C File Offset: 0x00084E7C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00086C8D File Offset: 0x00084E8D
		public override void Flush()
		{
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00086C90 File Offset: 0x00084E90
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result;
			try
			{
				if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
				{
					throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
					{
						"Read",
						"read"
					}));
				}
				if (this.m_HeadEOF)
				{
					result = base.WrappedStream.Read(buffer, offset, count);
				}
				else
				{
					int num = this.m_HeadStream.Read(buffer, offset, count);
					this.m_HeadLength += (long)num;
					if (num == 0 && count != 0)
					{
						this.m_HeadEOF = true;
						this.m_HeadStream.Close();
						num = base.WrappedStream.Read(buffer, offset, count);
					}
					result = num;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
			}
			return result;
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00086D54 File Offset: 0x00084F54
		private void ReadCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			CombinedReadStream.InnerAsyncResult innerAsyncResult = transportResult.AsyncState as CombinedReadStream.InnerAsyncResult;
			try
			{
				int num;
				if (!this.m_HeadEOF)
				{
					num = this.m_HeadStream.EndRead(transportResult);
					this.m_HeadLength += (long)num;
				}
				else
				{
					num = base.WrappedStream.EndRead(transportResult);
				}
				if (!this.m_HeadEOF && num == 0 && innerAsyncResult.Count != 0)
				{
					this.m_HeadEOF = true;
					this.m_HeadStream.Close();
					IAsyncResult asyncResult = base.WrappedStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					num = base.WrappedStream.EndRead(asyncResult);
				}
				innerAsyncResult.Buffer = null;
				innerAsyncResult.InvokeCallback(num);
			}
			catch (Exception result)
			{
				if (innerAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				innerAsyncResult.InvokeCallback(result);
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00086E44 File Offset: 0x00085044
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				if (Interlocked.Increment(ref this.m_ReadNesting) != 1)
				{
					throw new NotSupportedException(SR.GetString("net_io_invalidnestedcall", new object[]
					{
						"BeginRead",
						"read"
					}));
				}
				if (this.m_ReadCallback == null)
				{
					this.m_ReadCallback = new AsyncCallback(this.ReadCallback);
				}
				if (this.m_HeadEOF)
				{
					result = base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
				}
				else
				{
					CombinedReadStream.InnerAsyncResult innerAsyncResult = new CombinedReadStream.InnerAsyncResult(state, callback, buffer, offset, count);
					IAsyncResult asyncResult = this.m_HeadStream.BeginRead(buffer, offset, count, this.m_ReadCallback, innerAsyncResult);
					if (!asyncResult.CompletedSynchronously)
					{
						result = innerAsyncResult;
					}
					else
					{
						int num = this.m_HeadStream.EndRead(asyncResult);
						this.m_HeadLength += (long)num;
						if (num == 0 && innerAsyncResult.Count != 0)
						{
							this.m_HeadEOF = true;
							this.m_HeadStream.Close();
							result = base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
						}
						else
						{
							innerAsyncResult.Buffer = null;
							innerAsyncResult.InvokeCallback(count);
							result = innerAsyncResult;
						}
					}
				}
			}
			catch
			{
				Interlocked.Decrement(ref this.m_ReadNesting);
				throw;
			}
			return result;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00086F80 File Offset: 0x00085180
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
			CombinedReadStream.InnerAsyncResult innerAsyncResult = asyncResult as CombinedReadStream.InnerAsyncResult;
			if (innerAsyncResult == null)
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.EndRead(asyncResult);
				}
				return base.WrappedStream.EndRead(asyncResult);
			}
			else
			{
				innerAsyncResult.InternalWaitForCompletion();
				if (innerAsyncResult.Result is Exception)
				{
					throw (Exception)innerAsyncResult.Result;
				}
				return (int)innerAsyncResult.Result;
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00087029 File Offset: 0x00085229
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00087033 File Offset: 0x00085233
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00087040 File Offset: 0x00085240
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (disposing)
				{
					try
					{
						if (!this.m_HeadEOF)
						{
							ICloseEx closeEx = this.m_HeadStream as ICloseEx;
							if (closeEx != null)
							{
								closeEx.CloseEx(closeState);
							}
							else
							{
								this.m_HeadStream.Close();
							}
						}
					}
					finally
					{
						ICloseEx closeEx2 = base.WrappedStream as ICloseEx;
						if (closeEx2 != null)
						{
							closeEx2.CloseEx(closeState);
						}
						else
						{
							base.WrappedStream.Close();
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x000870C8 File Offset: 0x000852C8
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout && this.m_HeadStream.CanTimeout;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x000870E4 File Offset: 0x000852E4
		// (set) Token: 0x06001C6D RID: 7277 RVA: 0x00087108 File Offset: 0x00085308
		public override int ReadTimeout
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.ReadTimeout;
				}
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_HeadStream.ReadTimeout = value;
				wrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0008712F File Offset: 0x0008532F
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x00087150 File Offset: 0x00085350
		public override int WriteTimeout
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.WriteTimeout;
				}
				return base.WrappedStream.WriteTimeout;
			}
			set
			{
				Stream wrappedStream = base.WrappedStream;
				this.m_HeadStream.WriteTimeout = value;
				wrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x04001B98 RID: 7064
		private Stream m_HeadStream;

		// Token: 0x04001B99 RID: 7065
		private bool m_HeadEOF;

		// Token: 0x04001B9A RID: 7066
		private long m_HeadLength;

		// Token: 0x04001B9B RID: 7067
		private int m_ReadNesting;

		// Token: 0x04001B9C RID: 7068
		private AsyncCallback m_ReadCallback;

		// Token: 0x020007B9 RID: 1977
		private class InnerAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004340 RID: 17216 RVA: 0x0011A03C File Offset: 0x0011823C
			public InnerAsyncResult(object userState, AsyncCallback userCallback, byte[] buffer, int offset, int count) : base(null, userState, userCallback)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
			}

			// Token: 0x04003458 RID: 13400
			public byte[] Buffer;

			// Token: 0x04003459 RID: 13401
			public int Offset;

			// Token: 0x0400345A RID: 13402
			public int Count;
		}
	}
}
