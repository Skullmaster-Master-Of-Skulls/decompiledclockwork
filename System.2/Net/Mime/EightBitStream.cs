using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x02000243 RID: 579
	internal class EightBitStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00071B9C File Offset: 0x0006FD9C
		private WriteStateInfoBase WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new WriteStateInfoBase();
				}
				return this.writeState;
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00071BB7 File Offset: 0x0006FDB7
		internal EightBitStream(Stream stream) : base(stream)
		{
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00071BC0 File Offset: 0x0006FDC0
		internal EightBitStream(Stream stream, bool shouldEncodeLeadingDots) : this(stream)
		{
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00071BD0 File Offset: 0x0006FDD0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			IAsyncResult result;
			if (this.shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				result = base.BeginWrite(this.WriteState.Buffer, 0, this.WriteState.Length, callback, state);
			}
			else
			{
				result = base.BeginWrite(buffer, offset, count, callback, state);
			}
			return result;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00071C57 File Offset: 0x0006FE57
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.EndWrite(asyncResult);
			this.WriteState.BufferFlushed();
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00071C6C File Offset: 0x0006FE6C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.shouldEncodeLeadingDots)
			{
				this.EncodeLines(buffer, offset, count);
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
				return;
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		private void EncodeLines(byte[] buffer, int offset, int count)
		{
			if (ServicePointManager.DisableSmtp7bitEncodingFix)
			{
				for (int i = offset; i < offset + count; i++)
				{
					if (i >= buffer.Length)
					{
						return;
					}
					if (buffer[i] == 13 && i + 1 < offset + count && buffer[i + 1] == 10)
					{
						this.WriteState.AppendCRLF(false);
						i++;
					}
					else if (this.WriteState.CurrentLineLength == 0 && buffer[i] == 46)
					{
						this.WriteState.Append(46);
						this.WriteState.Append(buffer[i]);
					}
					else
					{
						this.WriteState.Append(buffer[i]);
					}
				}
			}
			else
			{
				int num = offset;
				while (num < offset + count && num < buffer.Length)
				{
					if (!this.lastWriteEndedWithCr)
					{
						goto IL_C0;
					}
					this.lastWriteEndedWithCr = false;
					if (buffer[num] != 10)
					{
						this.WriteState.Append(13);
						goto IL_C0;
					}
					this.WriteState.AppendCRLF(false);
					IL_FF:
					num++;
					continue;
					IL_C0:
					if (this.WriteState.CurrentLineLength == 0 && buffer[num] == 46)
					{
						this.WriteState.Append(46);
					}
					if (buffer[num] == 13)
					{
						this.lastWriteEndedWithCr = true;
						goto IL_FF;
					}
					this.WriteState.Append(buffer[num]);
					goto IL_FF;
				}
			}
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00071E10 File Offset: 0x00070010
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.lastWriteEndedWithCr)
				{
					this.lastWriteEndedWithCr = false;
					base.BaseStream.WriteByte(13);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00071E58 File Offset: 0x00070058
		public int DecodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00071E5F File Offset: 0x0007005F
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00071E66 File Offset: 0x00070066
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00071E69 File Offset: 0x00070069
		public string GetEncodedString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400170D RID: 5901
		private WriteStateInfoBase writeState;

		// Token: 0x0400170E RID: 5902
		private bool shouldEncodeLeadingDots;

		// Token: 0x0400170F RID: 5903
		private bool lastWriteEndedWithCr;
	}
}
