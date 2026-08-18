using System;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000041 RID: 65
	internal class SmiGettersStream : Stream
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0003A4AC File Offset: 0x000398AC
		internal SmiGettersStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._getters = getters;
			this._ordinal = ordinal;
			this._readPosition = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0003A4E4 File Offset: 0x000398E4
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0003A4F4 File Offset: 0x000398F4
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0003A504 File Offset: 0x00039904
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0003A514 File Offset: 0x00039914
		public override long Length
		{
			get
			{
				return ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, 0L, null, 0, 0, false);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0003A544 File Offset: 0x00039944
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0003A558 File Offset: 0x00039958
		public override long Position
		{
			get
			{
				return this._readPosition;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0003A56C File Offset: 0x0003996C
		public override void Flush()
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0003A580 File Offset: 0x00039980
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0003A594 File Offset: 0x00039994
		public override void SetLength(long value)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0003A5A8 File Offset: 0x000399A8
		public override int Read(byte[] buffer, int offset, int count)
		{
			long bytesInternal = ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, this._readPosition, buffer, offset, count, false);
			this._readPosition += bytesInternal;
			return checked((int)bytesInternal);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0003A5F0 File Offset: 0x000399F0
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0400010C RID: 268
		private SmiEventSink_Default _sink;

		// Token: 0x0400010D RID: 269
		private ITypedGettersV3 _getters;

		// Token: 0x0400010E RID: 270
		private int _ordinal;

		// Token: 0x0400010F RID: 271
		private long _readPosition;

		// Token: 0x04000110 RID: 272
		private SmiMetaData _metaData;
	}
}
