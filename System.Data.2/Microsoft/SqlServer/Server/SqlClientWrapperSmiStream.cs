using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000053 RID: 83
	internal class SqlClientWrapperSmiStream : Stream
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00043478 File Offset: 0x00042878
		internal SqlClientWrapperSmiStream(SmiEventSink_Default sink, SmiStream stream)
		{
			this._sink = sink;
			this._stream = stream;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0004349C File Offset: 0x0004289C
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000434B4 File Offset: 0x000428B4
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000434CC File Offset: 0x000428CC
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000434E4 File Offset: 0x000428E4
		public override long Length
		{
			get
			{
				long length = this._stream.GetLength(this._sink);
				this._sink.ProcessMessagesAndThrow();
				return length;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00043510 File Offset: 0x00042910
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0004353C File Offset: 0x0004293C
		public override long Position
		{
			get
			{
				long position = this._stream.GetPosition(this._sink);
				this._sink.ProcessMessagesAndThrow();
				return position;
			}
			set
			{
				this._stream.SetPosition(this._sink, value);
				this._sink.ProcessMessagesAndThrow();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00043568 File Offset: 0x00042968
		public override void Flush()
		{
			this._stream.Flush(this._sink);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00043594 File Offset: 0x00042994
		public override long Seek(long offset, SeekOrigin origin)
		{
			long result = this._stream.Seek(this._sink, offset, origin);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000435C4 File Offset: 0x000429C4
		public override void SetLength(long value)
		{
			this._stream.SetLength(this._sink, value);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000435F0 File Offset: 0x000429F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result = this._stream.Read(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00043620 File Offset: 0x00042A20
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0400019F RID: 415
		private SmiEventSink_Default _sink;

		// Token: 0x040001A0 RID: 416
		private SmiStream _stream;
	}
}
