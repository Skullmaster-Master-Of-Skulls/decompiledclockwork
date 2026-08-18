using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x0200088E RID: 2190
	internal sealed class TailStream : Stream
	{
		// Token: 0x06004FB5 RID: 20405 RVA: 0x00115587 File Offset: 0x00114587
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x001155A2 File Offset: 0x001145A2
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x001155AC File Offset: 0x001145AC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x001155FC File Offset: 0x001145FC
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x0011560E File Offset: 0x0011460E
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x00115611 File Offset: 0x00114611
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06004FBB RID: 20411 RVA: 0x00115614 File Offset: 0x00114614
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x00115617 File Offset: 0x00114617
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06004FBD RID: 20413 RVA: 0x00115628 File Offset: 0x00114628
		// (set) Token: 0x06004FBE RID: 20414 RVA: 0x00115639 File Offset: 0x00114639
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0011564A File Offset: 0x0011464A
		public override void Flush()
		{
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0011564C File Offset: 0x0011464C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0011565D File Offset: 0x0011465D
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0011566E File Offset: 0x0011466E
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00115680 File Offset: 0x00114680
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x04002914 RID: 10516
		private byte[] _Buffer;

		// Token: 0x04002915 RID: 10517
		private int _BufferSize;

		// Token: 0x04002916 RID: 10518
		private int _BufferIndex;

		// Token: 0x04002917 RID: 10519
		private bool _BufferFull;
	}
}
