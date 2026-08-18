using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001FB RID: 507
	internal sealed class SqlStream : Stream
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x000D9324 File Offset: 0x000D8724
		internal SqlStream(SqlDataReader reader, bool addByteOrderMark, bool processAllRows) : this(0, reader, addByteOrderMark, processAllRows, true)
		{
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000D933C File Offset: 0x000D873C
		internal SqlStream(int columnOrdinal, SqlDataReader reader, bool addByteOrderMark, bool processAllRows, bool advanceReader)
		{
			this._columnOrdinal = columnOrdinal;
			this._reader = reader;
			this._bom = (addByteOrderMark ? 65279 : 0);
			this._processAllRows = processAllRows;
			this._advanceReader = advanceReader;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000D9380 File Offset: 0x000D8780
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x000D9390 File Offset: 0x000D8790
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000D93A0 File Offset: 0x000D87A0
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000D93B0 File Offset: 0x000D87B0
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x000D93C4 File Offset: 0x000D87C4
		// (set) Token: 0x06001F73 RID: 8051 RVA: 0x000D93D8 File Offset: 0x000D87D8
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

		// Token: 0x06001F74 RID: 8052 RVA: 0x000D93EC File Offset: 0x000D87EC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._advanceReader && this._reader != null && !this._reader.IsClosed)
				{
					this._reader.Close();
				}
				this._reader = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000D9454 File Offset: 0x000D8854
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000D9468 File Offset: 0x000D8868
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			if (this._reader == null)
			{
				throw ADP.StreamClosed("Read");
			}
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw ADP.ArgumentOutOfRange(string.Empty, (offset < 0) ? "offset" : "count");
			}
			if (buffer.Length - offset < count)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			if (this._bom > 0)
			{
				this._bufferedData = new byte[2];
				num2 = this.ReadBytes(this._bufferedData, 0, 2);
				if (num2 < 2 || (this._bufferedData[0] == 223 && this._bufferedData[1] == 255))
				{
					this._bom = 0;
				}
				while (count > 0 && this._bom > 0)
				{
					buffer[offset] = (byte)this._bom;
					this._bom >>= 8;
					offset++;
					count--;
					num++;
				}
			}
			if (num2 > 0)
			{
				while (count > 0)
				{
					buffer[offset++] = this._bufferedData[0];
					num++;
					count--;
					if (num2 > 1 && count > 0)
					{
						buffer[offset++] = this._bufferedData[1];
						num++;
						count--;
						break;
					}
				}
				this._bufferedData = null;
			}
			return num + this.ReadBytes(buffer, offset, count);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000D95B0 File Offset: 0x000D89B0
		private static bool AdvanceToNextRow(SqlDataReader reader)
		{
			while (!reader.Read())
			{
				if (!reader.NextResult())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000D95D0 File Offset: 0x000D89D0
		private int ReadBytes(byte[] buffer, int offset, int count)
		{
			bool flag = true;
			int num = 0;
			if (this._reader.IsClosed || this._endOfColumn)
			{
				return 0;
			}
			try
			{
				while (count > 0)
				{
					if (this._advanceReader && this._bytesCol == 0L)
					{
						flag = false;
						if ((!this._readFirstRow || this._processAllRows) && SqlStream.AdvanceToNextRow(this._reader))
						{
							this._readFirstRow = true;
							if (this._reader.IsDBNull(this._columnOrdinal))
							{
								continue;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						break;
					}
					int num2 = (int)this._reader.GetBytesInternal(this._columnOrdinal, this._bytesCol, buffer, offset, count);
					if (num2 < count)
					{
						this._bytesCol = 0L;
						flag = false;
						if (!this._advanceReader)
						{
							this._endOfColumn = true;
						}
					}
					else
					{
						this._bytesCol += (long)num2;
					}
					count -= num2;
					offset += num2;
					num += num2;
				}
				if (!flag && this._advanceReader)
				{
					this._reader.Close();
				}
			}
			catch (Exception e)
			{
				if (this._advanceReader && ADP.IsCatchableExceptionType(e))
				{
					this._reader.Close();
				}
				throw;
			}
			return num;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000D9704 File Offset: 0x000D8B04
		internal XmlReader ToXmlReader()
		{
			return SqlXml.CreateSqlXmlReader(this, true, true);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000D971C File Offset: 0x000D8B1C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000D9730 File Offset: 0x000D8B30
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000D9744 File Offset: 0x000D8B44
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x040011CF RID: 4559
		private SqlDataReader _reader;

		// Token: 0x040011D0 RID: 4560
		private int _columnOrdinal;

		// Token: 0x040011D1 RID: 4561
		private long _bytesCol;

		// Token: 0x040011D2 RID: 4562
		private int _bom;

		// Token: 0x040011D3 RID: 4563
		private byte[] _bufferedData;

		// Token: 0x040011D4 RID: 4564
		private bool _processAllRows;

		// Token: 0x040011D5 RID: 4565
		private bool _advanceReader;

		// Token: 0x040011D6 RID: 4566
		private bool _readFirstRow;

		// Token: 0x040011D7 RID: 4567
		private bool _endOfColumn;
	}
}
