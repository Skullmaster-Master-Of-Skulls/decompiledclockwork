using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004A RID: 74
	internal class SmiSettersStream : Stream
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x001DFD18 File Offset: 0x001DF118
		internal SmiSettersStream(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._setters = setters;
			this._ordinal = ordinal;
			this._lengthWritten = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x001DFD58 File Offset: 0x001DF158
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x001DFD68 File Offset: 0x001DF168
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x001DFD78 File Offset: 0x001DF178
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x001DFD88 File Offset: 0x001DF188
		public override long Length
		{
			get
			{
				return this._lengthWritten;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x001DFDA8 File Offset: 0x001DF1A8
		// (set) Token: 0x060002CA RID: 714 RVA: 0x001DFDC8 File Offset: 0x001DF1C8
		public override long Position
		{
			get
			{
				return this._lengthWritten;
			}
			set
			{
				throw SQL.StreamSeekNotSupported();
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x001DFDE8 File Offset: 0x001DF1E8
		public override void Flush()
		{
			this._lengthWritten = ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x001DFE28 File Offset: 0x001DF228
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x001DFE48 File Offset: 0x001DF248
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, value);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x001DFE88 File Offset: 0x001DF288
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamReadNotSupported();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x001DFEA8 File Offset: 0x001DF2A8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._lengthWritten += ValueUtilsSmi.SetBytes(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten, buffer, offset, count);
		}

		// Token: 0x040005FE RID: 1534
		private SmiEventSink_Default _sink;

		// Token: 0x040005FF RID: 1535
		private ITypedSettersV3 _setters;

		// Token: 0x04000600 RID: 1536
		private int _ordinal;

		// Token: 0x04000601 RID: 1537
		private long _lengthWritten;

		// Token: 0x04000602 RID: 1538
		private SmiMetaData _metaData;
	}
}
