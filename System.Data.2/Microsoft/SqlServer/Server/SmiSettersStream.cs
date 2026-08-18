using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004A RID: 74
	internal class SmiSettersStream : Stream
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0003C8F8 File Offset: 0x0003BCF8
		internal SmiSettersStream(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData)
		{
			this._sink = sink;
			this._setters = setters;
			this._ordinal = ordinal;
			this._lengthWritten = 0L;
			this._metaData = metaData;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0003C930 File Offset: 0x0003BD30
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0003C940 File Offset: 0x0003BD40
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0003C950 File Offset: 0x0003BD50
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0003C960 File Offset: 0x0003BD60
		public override long Length
		{
			get
			{
				return this._lengthWritten;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0003C974 File Offset: 0x0003BD74
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0003C988 File Offset: 0x0003BD88
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

		// Token: 0x060002FD RID: 765 RVA: 0x0003C99C File Offset: 0x0003BD9C
		public override void Flush()
		{
			this._lengthWritten = ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0003C9D4 File Offset: 0x0003BDD4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw SQL.StreamSeekNotSupported();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0003C9E8 File Offset: 0x0003BDE8
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw ADP.ArgumentOutOfRange("value");
			}
			ValueUtilsSmi.SetBytesLength(this._sink, this._setters, this._ordinal, this._metaData, value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0003CA24 File Offset: 0x0003BE24
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw SQL.StreamReadNotSupported();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0003CA38 File Offset: 0x0003BE38
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._lengthWritten += ValueUtilsSmi.SetBytes(this._sink, this._setters, this._ordinal, this._metaData, this._lengthWritten, buffer, offset, count);
		}

		// Token: 0x04000160 RID: 352
		private SmiEventSink_Default _sink;

		// Token: 0x04000161 RID: 353
		private ITypedSettersV3 _setters;

		// Token: 0x04000162 RID: 354
		private int _ordinal;

		// Token: 0x04000163 RID: 355
		private long _lengthWritten;

		// Token: 0x04000164 RID: 356
		private SmiMetaData _metaData;
	}
}
