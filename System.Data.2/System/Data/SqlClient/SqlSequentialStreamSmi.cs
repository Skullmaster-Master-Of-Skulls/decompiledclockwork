using System;
using System.Data.Common;
using System.IO;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001F6 RID: 502
	internal sealed class SqlSequentialStreamSmi : Stream
	{
		// Token: 0x06001F31 RID: 7985 RVA: 0x000D83CC File Offset: 0x000D77CC
		internal SqlSequentialStreamSmi(SmiEventSink_Default sink, ITypedGettersV3 getters, int columnIndex, long length)
		{
			this._sink = sink;
			this._getters = getters;
			this._columnIndex = columnIndex;
			this._length = length;
			this._position = 0L;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x000D8404 File Offset: 0x000D7804
		public override bool CanRead
		{
			get
			{
				return this._sink != null && this._getters != null;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000D8424 File Offset: 0x000D7824
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x000D8434 File Offset: 0x000D7834
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000D8444 File Offset: 0x000D7844
		public override void Flush()
		{
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000D8454 File Offset: 0x000D7854
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x000D8468 File Offset: 0x000D7868
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x000D847C File Offset: 0x000D787C
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000D8490 File Offset: 0x000D7890
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000D84A4 File Offset: 0x000D78A4
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			int result;
			try
			{
				int num = (int)Math.Min((long)count, this._length - this._position);
				int num2 = 0;
				if (num > 0)
				{
					num2 = ValueUtilsSmi.GetBytes_Unchecked(this._sink, this._getters, this._columnIndex, this._position, buffer, offset, num);
					this._position += (long)num2;
				}
				result = num2;
			}
			catch (SqlException internalException)
			{
				throw ADP.ErrorReadingFromStream(internalException);
			}
			return result;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000D8540 File Offset: 0x000D7940
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000D8554 File Offset: 0x000D7954
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000D8568 File Offset: 0x000D7968
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000D857C File Offset: 0x000D797C
		internal void SetClosed()
		{
			this._sink = null;
			this._getters = null;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000D8598 File Offset: 0x000D7998
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040011A2 RID: 4514
		private SmiEventSink_Default _sink;

		// Token: 0x040011A3 RID: 4515
		private ITypedGettersV3 _getters;

		// Token: 0x040011A4 RID: 4516
		private int _columnIndex;

		// Token: 0x040011A5 RID: 4517
		private long _position;

		// Token: 0x040011A6 RID: 4518
		private long _length;
	}
}
