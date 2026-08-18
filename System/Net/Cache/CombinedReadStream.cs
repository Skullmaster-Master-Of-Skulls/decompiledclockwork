using System;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200057A RID: 1402
	internal class CombinedReadStream : Stream, ICloseEx
	{
		// Token: 0x06002ABC RID: 10940 RVA: 0x000B5C34 File Offset: 0x000B4C34
		internal CombinedReadStream(Stream headStream, Stream tailStream)
		{
			this.m_HeadStream = headStream;
			this.m_TailStream = tailStream;
			this.m_HeadEOF = (headStream == Stream.Null);
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000B5C58 File Offset: 0x000B4C58
		public override bool CanRead
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.CanRead;
				}
				return this.m_TailStream.CanRead;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000B5C79 File Offset: 0x000B4C79
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000B5C7C File Offset: 0x000B4C7C
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000B5C7F File Offset: 0x000B4C7F
		public override long Length
		{
			get
			{
				return this.m_TailStream.Length + (this.m_HeadEOF ? this.m_HeadLength : this.m_HeadStream.Length);
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000B5CA8 File Offset: 0x000B4CA8
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x000B5CD1 File Offset: 0x000B4CD1
		public override long Position
		{
			get
			{
				return this.m_TailStream.Position + (this.m_HeadEOF ? this.m_HeadLength : this.m_HeadStream.Position);
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000B5CE2 File Offset: 0x000B4CE2
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000B5CF3 File Offset: 0x000B4CF3
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000B5D04 File Offset: 0x000B4D04
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000B5D15 File Offset: 0x000B4D15
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000B5D26 File Offset: 0x000B4D26
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000B5D37 File Offset: 0x000B4D37
		public override void Flush()
		{
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000B5D3C File Offset: 0x000B4D3C
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
					result = this.m_TailStream.Read(buffer, offset, count);
				}
				else
				{
					int num = this.m_HeadStream.Read(buffer, offset, count);
					this.m_HeadLength += (long)num;
					if (num == 0 && count != 0)
					{
						this.m_HeadEOF = true;
						this.m_HeadStream.Close();
						num = this.m_TailStream.Read(buffer, offset, count);
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

		// Token: 0x06002ACA RID: 10954 RVA: 0x000B5E00 File Offset: 0x000B4E00
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
					num = this.m_TailStream.EndRead(transportResult);
				}
				if (!this.m_HeadEOF && num == 0 && innerAsyncResult.Count != 0)
				{
					this.m_HeadEOF = true;
					this.m_HeadStream.Close();
					IAsyncResult asyncResult = this.m_TailStream.BeginRead(innerAsyncResult.Buffer, innerAsyncResult.Offset, innerAsyncResult.Count, this.m_ReadCallback, innerAsyncResult);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					num = this.m_TailStream.EndRead(asyncResult);
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
			catch
			{
				if (innerAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				innerAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000B5F20 File Offset: 0x000B4F20
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
					result = this.m_TailStream.BeginRead(buffer, offset, count, callback, state);
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
							result = this.m_TailStream.BeginRead(buffer, offset, count, callback, state);
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

		// Token: 0x06002ACC RID: 10956 RVA: 0x000B6064 File Offset: 0x000B5064
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
				return this.m_TailStream.EndRead(asyncResult);
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

		// Token: 0x06002ACD RID: 10957 RVA: 0x000B610F File Offset: 0x000B510F
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000B611F File Offset: 0x000B511F
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000B6130 File Offset: 0x000B5130
		protected virtual void Dispose(bool disposing, CloseExState closeState)
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
				ICloseEx closeEx2 = this.m_TailStream as ICloseEx;
				if (closeEx2 != null)
				{
					closeEx2.CloseEx(closeState);
				}
				else
				{
					this.m_TailStream.Close();
				}
			}
			if (!disposing)
			{
				this.m_HeadStream = null;
				this.m_TailStream = null;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000B61B0 File Offset: 0x000B51B0
		public override bool CanTimeout
		{
			get
			{
				return this.m_TailStream.CanTimeout && this.m_HeadStream.CanTimeout;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x000B61CC File Offset: 0x000B51CC
		// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x000B61F0 File Offset: 0x000B51F0
		public override int ReadTimeout
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.ReadTimeout;
				}
				return this.m_TailStream.ReadTimeout;
			}
			set
			{
				Stream tailStream = this.m_TailStream;
				this.m_HeadStream.ReadTimeout = value;
				tailStream.ReadTimeout = value;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000B6217 File Offset: 0x000B5217
		// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x000B6238 File Offset: 0x000B5238
		public override int WriteTimeout
		{
			get
			{
				if (!this.m_HeadEOF)
				{
					return this.m_HeadStream.WriteTimeout;
				}
				return this.m_TailStream.WriteTimeout;
			}
			set
			{
				Stream tailStream = this.m_TailStream;
				this.m_HeadStream.WriteTimeout = value;
				tailStream.WriteTimeout = value;
			}
		}

		// Token: 0x04002989 RID: 10633
		private Stream m_HeadStream;

		// Token: 0x0400298A RID: 10634
		private Stream m_TailStream;

		// Token: 0x0400298B RID: 10635
		private bool m_HeadEOF;

		// Token: 0x0400298C RID: 10636
		private long m_HeadLength;

		// Token: 0x0400298D RID: 10637
		private int m_ReadNesting;

		// Token: 0x0400298E RID: 10638
		private AsyncCallback m_ReadCallback;

		// Token: 0x0200057B RID: 1403
		private class InnerAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002AD5 RID: 10965 RVA: 0x000B625F File Offset: 0x000B525F
			public InnerAsyncResult(object userState, AsyncCallback userCallback, byte[] buffer, int offset, int count) : base(null, userState, userCallback)
			{
				this.Buffer = buffer;
				this.Offset = offset;
				this.Count = count;
			}

			// Token: 0x0400298F RID: 10639
			public byte[] Buffer;

			// Token: 0x04002990 RID: 10640
			public int Offset;

			// Token: 0x04002991 RID: 10641
			public int Count;
		}
	}
}
