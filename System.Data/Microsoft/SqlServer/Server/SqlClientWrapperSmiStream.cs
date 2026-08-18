using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020002C0 RID: 704
	internal class SqlClientWrapperSmiStream : Stream
	{
		// Token: 0x06002371 RID: 9073 RVA: 0x00290E28 File Offset: 0x00290228
		internal SqlClientWrapperSmiStream(SmiEventSink_Default sink, SmiStream stream)
		{
			this._sink = sink;
			this._stream = stream;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x00290E58 File Offset: 0x00290258
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x00290E78 File Offset: 0x00290278
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00290E98 File Offset: 0x00290298
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x00290EB8 File Offset: 0x002902B8
		public override long Length
		{
			get
			{
				long length = this._stream.GetLength(this._sink);
				this._sink.ProcessMessagesAndThrow();
				return length;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00290EE8 File Offset: 0x002902E8
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x00290F18 File Offset: 0x00290318
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

		// Token: 0x06002378 RID: 9080 RVA: 0x00290F48 File Offset: 0x00290348
		public override void Flush()
		{
			this._stream.Flush(this._sink);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00290F78 File Offset: 0x00290378
		public override long Seek(long offset, SeekOrigin origin)
		{
			long result = this._stream.Seek(this._sink, offset, origin);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00290FA8 File Offset: 0x002903A8
		public override void SetLength(long value)
		{
			this._stream.SetLength(this._sink, value);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00290FD8 File Offset: 0x002903D8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int result = this._stream.Read(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00291008 File Offset: 0x00290408
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._stream.Write(this._sink, buffer, offset, count);
			this._sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0400171B RID: 5915
		private SmiEventSink_Default _sink;

		// Token: 0x0400171C RID: 5916
		private SmiStream _stream;
	}
}
