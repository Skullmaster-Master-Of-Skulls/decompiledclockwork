using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000157 RID: 343
	internal sealed class StreamOnSqlChars : SqlStreamChars
	{
		// Token: 0x06001471 RID: 5233 RVA: 0x0009DA00 File Offset: 0x0009CE00
		internal StreamOnSqlChars(SqlChars s)
		{
			this.m_sqlchars = s;
			this.m_lPosition = 0L;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0009DA24 File Offset: 0x0009CE24
		public override bool IsNull
		{
			get
			{
				return this.m_sqlchars == null || this.m_sqlchars.IsNull;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0009DA48 File Offset: 0x0009CE48
		public override bool CanRead
		{
			get
			{
				return this.m_sqlchars != null && !this.m_sqlchars.IsNull;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0009DA70 File Offset: 0x0009CE70
		public override bool CanSeek
		{
			get
			{
				return this.m_sqlchars != null;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0009DA88 File Offset: 0x0009CE88
		public override bool CanWrite
		{
			get
			{
				return this.m_sqlchars != null && (!this.m_sqlchars.IsNull || this.m_sqlchars.m_rgchBuf != null);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0009DABC File Offset: 0x0009CEBC
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this.m_sqlchars.Length;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0009DAE0 File Offset: 0x0009CEE0
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0009DB00 File Offset: 0x0009CF00
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
				if (value < 0L || value > this.m_sqlchars.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_lPosition = value;
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0009DB40 File Offset: 0x0009CF40
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this.m_sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this.m_lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this.m_lPosition + offset;
				if (num < 0L || num > this.m_sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this.m_lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this.m_sqlchars.Length + offset;
				if (num < 0L || num > this.m_sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this.m_lPosition = num;
				break;
			}
			default:
				throw ADP.ArgumentOutOfRange("offset");
			}
			return this.m_lPosition;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0009DC10 File Offset: 0x0009D010
		public override int Read(char[] buffer, int offset, int count)
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
			int num = (int)this.m_sqlchars.Read(this.m_lPosition, buffer, offset, count);
			this.m_lPosition += (long)num;
			return num;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0009DC88 File Offset: 0x0009D088
		public override void Write(char[] buffer, int offset, int count)
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
			this.m_sqlchars.Write(this.m_lPosition, buffer, offset, count);
			this.m_lPosition += (long)count;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0009DD00 File Offset: 0x0009D100
		public override int ReadChar()
		{
			this.CheckIfStreamClosed("ReadChar");
			if (this.m_lPosition >= this.m_sqlchars.Length)
			{
				return -1;
			}
			int result = (int)this.m_sqlchars[this.m_lPosition];
			this.m_lPosition += 1L;
			return result;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0009DD50 File Offset: 0x0009D150
		public override void WriteChar(char value)
		{
			this.CheckIfStreamClosed("WriteChar");
			this.m_sqlchars[this.m_lPosition] = value;
			this.m_lPosition += 1L;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0009DD8C File Offset: 0x0009D18C
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this.m_sqlchars.SetLength(value);
			if (this.m_lPosition > value)
			{
				this.m_lPosition = value;
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0009DDC0 File Offset: 0x0009D1C0
		public override void Flush()
		{
			if (this.m_sqlchars.FStream())
			{
				this.m_sqlchars.m_stream.Flush();
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0009DDEC File Offset: 0x0009D1EC
		protected override void Dispose(bool disposing)
		{
			this.m_sqlchars = null;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0009DE00 File Offset: 0x0009D200
		private bool FClosed()
		{
			return this.m_sqlchars == null;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0009DE18 File Offset: 0x0009D218
		private void CheckIfStreamClosed(string methodname)
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x04000D70 RID: 3440
		private SqlChars m_sqlchars;

		// Token: 0x04000D71 RID: 3441
		private long m_lPosition;
	}
}
