using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000155 RID: 341
	internal sealed class StreamOnSqlBytes : Stream
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x0009CCA0 File Offset: 0x0009C0A0
		internal StreamOnSqlBytes(SqlBytes sb)
		{
			this.m_sb = sb;
			this.m_lPosition = 0L;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0009CCC4 File Offset: 0x0009C0C4
		public override bool CanRead
		{
			get
			{
				return this.m_sb != null && !this.m_sb.IsNull;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0009CCEC File Offset: 0x0009C0EC
		public override bool CanSeek
		{
			get
			{
				return this.m_sb != null;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0009CD04 File Offset: 0x0009C104
		public override bool CanWrite
		{
			get
			{
				return this.m_sb != null && (!this.m_sb.IsNull || this.m_sb.m_rgbBuf != null);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0009CD38 File Offset: 0x0009C138
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this.m_sb.Length;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0009CD5C File Offset: 0x0009C15C
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x0009CD7C File Offset: 0x0009C17C
		public override long Position
		{
			get
			{
				this.CheckIfStreamClosed("get_Position");
				return this.m_lPosition;
			}
			set
			{
				this.CheckIfStreamClosed("set_Position");
				if (value < 0L || value > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_lPosition = value;
			}
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0009CDBC File Offset: 0x0009C1BC
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this.m_lPosition + offset;
				if (num < 0L || num > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this.m_sb.Length + offset;
				if (num < 0L || num > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this.m_lPosition;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0009CE8C File Offset: 0x0009C28C
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = (int)this.m_sb.Read(this.m_lPosition, buffer, offset, count);
			this.m_lPosition += (long)num;
			return num;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0009CF04 File Offset: 0x0009C304
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.m_sb.Write(this.m_lPosition, buffer, offset, count);
			this.m_lPosition += (long)count;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0009CF7C File Offset: 0x0009C37C
		public override int ReadByte()
		{
			this.CheckIfStreamClosed("ReadByte");
			if (this.m_lPosition >= this.m_sb.Length)
			{
				return -1;
			}
			int result = (int)this.m_sb[this.m_lPosition];
			this.m_lPosition += 1L;
			return result;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0009CFCC File Offset: 0x0009C3CC
		public override void WriteByte(byte value)
		{
			this.CheckIfStreamClosed("WriteByte");
			this.m_sb[this.m_lPosition] = value;
			this.m_lPosition += 1L;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0009D008 File Offset: 0x0009C408
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this.m_sb.SetLength(value);
			if (this.m_lPosition > value)
			{
				this.m_lPosition = value;
			}
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0009D03C File Offset: 0x0009C43C
		public override void Flush()
		{
			if (this.m_sb.FStream())
			{
				this.m_sb.m_stream.Flush();
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0009D068 File Offset: 0x0009C468
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.m_sb = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0009D0A4 File Offset: 0x0009C4A4
		private bool FClosed()
		{
			return this.m_sb == null;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0009D0BC File Offset: 0x0009C4BC
		private void CheckIfStreamClosed(string methodname)
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x04000D67 RID: 3431
		private SqlBytes m_sb;

		// Token: 0x04000D68 RID: 3432
		private long m_lPosition;
	}
}
