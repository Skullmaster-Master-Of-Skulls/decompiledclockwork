using System;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x0200030E RID: 782
	internal sealed class SqlStream : Stream
	{
		// Token: 0x060028D0 RID: 10448 RVA: 0x002B2228 File Offset: 0x002B1628
		internal SqlStream(SqlDataReader reader, bool addByteOrderMark, bool processAllRows) : this(0, reader, addByteOrderMark, processAllRows, true)
		{
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x002B2248 File Offset: 0x002B1648
		internal SqlStream(int columnOrdinal, SqlDataReader reader, bool addByteOrderMark, bool processAllRows, bool advanceReader)
		{
			this._columnOrdinal = columnOrdinal;
			this._reader = reader;
			this._bom = (addByteOrderMark ? 65279 : 0);
			this._processAllRows = processAllRows;
			this._advanceReader = advanceReader;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x002B2298 File Offset: 0x002B1698
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x002B22A8 File Offset: 0x002B16A8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x002B22B8 File Offset: 0x002B16B8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x002B22C8 File Offset: 0x002B16C8
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x002B22E8 File Offset: 0x002B16E8
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x002B2308 File Offset: 0x002B1708
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

		// Token: 0x060028D8 RID: 10456 RVA: 0x002B2328 File Offset: 0x002B1728
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

		// Token: 0x060028D9 RID: 10457 RVA: 0x002B2398 File Offset: 0x002B1798
		public override void Flush()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x002B23B8 File Offset: 0x002B17B8
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

		// Token: 0x060028DB RID: 10459 RVA: 0x002B2508 File Offset: 0x002B1908
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
					if (this._advanceReader && 0L == this._bytesCol)
					{
						flag = false;
						while (!this._readFirstRow || this._processAllRows)
						{
							if (this._reader.Read())
							{
								this._readFirstRow = true;
								flag = true;
								goto IL_79;
							}
							if (!this._reader.NextResult())
							{
								goto IL_79;
							}
						}
						this._reader.Close();
					}
					IL_79:
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

		// Token: 0x060028DC RID: 10460 RVA: 0x002B2648 File Offset: 0x002B1A48
		internal XmlReader ToXmlReader()
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlReaderSettings.CloseInput = true;
			MethodInfo method = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
			object[] array = new object[3];
			array[0] = this;
			array[1] = xmlReaderSettings;
			object[] parameters = array;
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			XmlReader result;
			try
			{
				result = (XmlReader)method.Invoke(null, parameters);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x002B26D8 File Offset: 0x002B1AD8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x002B26F8 File Offset: 0x002B1AF8
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x002B2718 File Offset: 0x002B1B18
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x04001998 RID: 6552
		private SqlDataReader _reader;

		// Token: 0x04001999 RID: 6553
		private int _columnOrdinal;

		// Token: 0x0400199A RID: 6554
		private long _bytesCol;

		// Token: 0x0400199B RID: 6555
		private int _bom;

		// Token: 0x0400199C RID: 6556
		private byte[] _bufferedData;

		// Token: 0x0400199D RID: 6557
		private bool _processAllRows;

		// Token: 0x0400199E RID: 6558
		private bool _advanceReader;

		// Token: 0x0400199F RID: 6559
		private bool _readFirstRow;

		// Token: 0x040019A0 RID: 6560
		private bool _endOfColumn;
	}
}
