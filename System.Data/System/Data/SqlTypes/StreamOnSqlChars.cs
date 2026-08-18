using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000347 RID: 839
	internal sealed class StreamOnSqlChars : SqlStreamChars
	{
		// Token: 0x06002C2F RID: 11311 RVA: 0x002C7418 File Offset: 0x002C6818
		internal StreamOnSqlChars(SqlChars s)
		{
			this.m_sqlchars = s;
			this.m_lPosition = 0L;
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x002C7448 File Offset: 0x002C6848
		public override bool IsNull
		{
			get
			{
				return this.m_sqlchars == null || this.m_sqlchars.IsNull;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x002C7478 File Offset: 0x002C6878
		public override bool CanRead
		{
			get
			{
				return this.m_sqlchars != null && !this.m_sqlchars.IsNull;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x002C74A8 File Offset: 0x002C68A8
		public override bool CanSeek
		{
			get
			{
				return this.m_sqlchars != null;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x002C74C8 File Offset: 0x002C68C8
		public override bool CanWrite
		{
			get
			{
				return this.m_sqlchars != null && (!this.m_sqlchars.IsNull || this.m_sqlchars.m_rgchBuf != null);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x002C7508 File Offset: 0x002C6908
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this.m_sqlchars.Length;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x002C7538 File Offset: 0x002C6938
		// (set) Token: 0x06002C36 RID: 11318 RVA: 0x002C7558 File Offset: 0x002C6958
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

		// Token: 0x06002C37 RID: 11319 RVA: 0x002C7598 File Offset: 0x002C6998
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

		// Token: 0x06002C38 RID: 11320 RVA: 0x002C7678 File Offset: 0x002C6A78
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

		// Token: 0x06002C39 RID: 11321 RVA: 0x002C76F8 File Offset: 0x002C6AF8
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

		// Token: 0x06002C3A RID: 11322 RVA: 0x002C7778 File Offset: 0x002C6B78
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

		// Token: 0x06002C3B RID: 11323 RVA: 0x002C77C8 File Offset: 0x002C6BC8
		public override void WriteChar(char value)
		{
			this.CheckIfStreamClosed("WriteChar");
			this.m_sqlchars[this.m_lPosition] = value;
			this.m_lPosition += 1L;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x002C7808 File Offset: 0x002C6C08
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this.m_sqlchars.SetLength(value);
			if (this.m_lPosition > value)
			{
				this.m_lPosition = value;
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x002C7848 File Offset: 0x002C6C48
		public override void Flush()
		{
			if (this.m_sqlchars.FStream())
			{
				this.m_sqlchars.m_stream.Flush();
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x002C7878 File Offset: 0x002C6C78
		protected override void Dispose(bool disposing)
		{
			this.m_sqlchars = null;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x002C7898 File Offset: 0x002C6C98
		private bool FClosed()
		{
			return this.m_sqlchars == null;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x002C78B8 File Offset: 0x002C6CB8
		private void CheckIfStreamClosed(string methodname)
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x04001C87 RID: 7303
		private SqlChars m_sqlchars;

		// Token: 0x04001C88 RID: 7304
		private long m_lPosition;
	}
}
