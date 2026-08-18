using System;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003B RID: 59
	internal class SmiGettersStream : Stream
	{
		// Token: 0x06000207 RID: 519 RVA: 0x001DD388 File Offset: 0x001DC788
		internal SmiGettersStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._getters = getters;
			this._ordinal = ordinal;
			this._readPosition = 0L;
			this._metaData = metaData;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000208 RID: 520 RVA: 0x001DD3C8 File Offset: 0x001DC7C8
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000209 RID: 521 RVA: 0x001DD3D8 File Offset: 0x001DC7D8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600020A RID: 522 RVA: 0x001DD3E8 File Offset: 0x001DC7E8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600020B RID: 523 RVA: 0x001DD3F8 File Offset: 0x001DC7F8
		public override long Length
		{
			get
			{
				return ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, 0L, null, 0, 0, false);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600020C RID: 524 RVA: 0x001DD428 File Offset: 0x001DC828
		// (set) Token: 0x0600020D RID: 525 RVA: 0x001DD448 File Offset: 0x001DC848
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

		// Token: 0x0600020E RID: 526 RVA: 0x001DD468 File Offset: 0x001DC868
		public override void Flush()
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x001DD488 File Offset: 0x001DC888
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x001DD4A8 File Offset: 0x001DC8A8
		public override void SetLength(long value)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x001DD4C8 File Offset: 0x001DC8C8
		public override int Read(byte[] buffer, int offset, int count)
		{
			long bytesInternal = ValueUtilsSmi.GetBytesInternal(this._sink, this._getters, this._ordinal, this._metaData, this._readPosition, buffer, offset, count, false);
			this._readPosition += bytesInternal;
			return checked((int)bytesInternal);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x001DD518 File Offset: 0x001DC918
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamWriteNotSupported();
		}

		// Token: 0x0400059A RID: 1434
		private SmiEventSink_Default _sink;

		// Token: 0x0400059B RID: 1435
		private ITypedGettersV3 _getters;

		// Token: 0x0400059C RID: 1436
		private int _ordinal;

		// Token: 0x0400059D RID: 1437
		private long _readPosition;

		// Token: 0x0400059E RID: 1438
		private SmiMetaData _metaData;
	}
}
