using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000054 RID: 84
	internal class SqlClientWrapperSmiStreamChars : SqlStreamChars
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0004364C File Offset: 0x00042A4C
		internal SqlClientWrapperSmiStreamChars(SmiEventSink_Default sink, SmiStream stream)
		{
			this._sink = sink;
			this._stream = stream;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00043670 File Offset: 0x00042A70
		public override bool IsNull
		{
			get
			{
				return this._stream == null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00043688 File Offset: 0x00042A88
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x000436A0 File Offset: 0x00042AA0
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000436B8 File Offset: 0x00042AB8
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x000436D0 File Offset: 0x00042AD0
		public override long Length
		{
			get
			{
				long length = this._stream.GetLength(this._sink);
				this._sink.ProcessMessagesAndThrow();
				if (length > 0L)
				{
					return length / 2L;
				}
				return length;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00043708 File Offset: 0x00042B08
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00043738 File Offset: 0x00042B38
		public override long Position
		{
			get
			{
				long result = this._stream.GetPosition(this._sink) / 2L;
				this._sink.ProcessMessagesAndThrow();
				return result;
			}
			set
			{
				if (value < 0L)
				{
					throw ADP.ArgumentOutOfRange("Position");
				}
				this._stream.SetPosition(this._sink, value * 2L);
				this._sink.ProcessMessagesAndThrow();
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00043778 File Offset: 0x00042B78
		public override void Flush()
		{
			this._stream.Flush(this._sink);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000437A4 File Offset: 0x00042BA4
		public override long Seek(long offset, SeekOrigin origin)
		{
			long result = this._stream.Seek(this._sink, offset * 2L, origin);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000437D4 File Offset: 0x00042BD4
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			this._stream.SetLength(this._sink, value * 2L);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00043814 File Offset: 0x00042C14
		public override int Read(char[] buffer, int offset, int count)
		{
			int result = this._stream.Read(this._sink, buffer, offset * 2, count);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00043844 File Offset: 0x00042C44
		public override void Write(char[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00043870 File Offset: 0x00042C70
		internal int Read(byte[] buffer, int offset, int count)
		{
			int result = this._stream.Read(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000438A0 File Offset: 0x00042CA0
		internal void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x040001A1 RID: 417
		private SmiEventSink_Default _sink;

		// Token: 0x040001A2 RID: 418
		private SmiStream _stream;
	}
}
