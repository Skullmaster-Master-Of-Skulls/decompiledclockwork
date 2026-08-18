using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020002C3 RID: 707
	internal class SqlClientWrapperSmiStreamChars : SqlStreamChars
	{
		// Token: 0x0600239E RID: 9118 RVA: 0x00291298 File Offset: 0x00290698
		internal SqlClientWrapperSmiStreamChars(SmiEventSink_Default sink, SmiStream stream)
		{
			this._sink = sink;
			this._stream = stream;
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x002912C8 File Offset: 0x002906C8
		public override bool IsNull
		{
			get
			{
				return null == this._stream;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x002912E8 File Offset: 0x002906E8
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00291308 File Offset: 0x00290708
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x00291328 File Offset: 0x00290728
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00291348 File Offset: 0x00290748
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

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00291388 File Offset: 0x00290788
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x002913B8 File Offset: 0x002907B8
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

		// Token: 0x060023A6 RID: 9126 RVA: 0x002913F8 File Offset: 0x002907F8
		public override void Flush()
		{
			this._stream.Flush(this._sink);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00291428 File Offset: 0x00290828
		public override long Seek(long offset, SeekOrigin origin)
		{
			long result = this._stream.Seek(this._sink, offset * 2L, origin);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00291458 File Offset: 0x00290858
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			this._stream.SetLength(this._sink, value * 2L);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00291498 File Offset: 0x00290898
		public override int Read(char[] buffer, int offset, int count)
		{
			int result = this._stream.Read(this._sink, buffer, offset * 2, count);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x002914C8 File Offset: 0x002908C8
		public override void Write(char[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x002914F8 File Offset: 0x002908F8
		internal int Read(byte[] buffer, int offset, int count)
		{
			int result = this._stream.Read(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00291528 File Offset: 0x00290928
		internal void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0400171D RID: 5917
		private SmiEventSink_Default _sink;

		// Token: 0x0400171E RID: 5918
		private SmiStream _stream;
	}
}
