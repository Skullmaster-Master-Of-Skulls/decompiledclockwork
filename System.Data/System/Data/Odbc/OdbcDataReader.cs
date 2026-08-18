using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x020001E5 RID: 485
	public sealed class OdbcDataReader : DbDataReader
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x00260098 File Offset: 0x0025F498
		internal OdbcDataReader(OdbcCommand command, CMDWrapper cmdWrapper, CommandBehavior commandbehavior)
		{
			this.command = command;
			this._commandBehavior = commandbehavior;
			this._cmdText = command.CommandText;
			this._cmdWrapper = cmdWrapper;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x002600F8 File Offset: 0x0025F4F8
		private CNativeBuffer Buffer
		{
			get
			{
				CNativeBuffer dataReaderBuf = this._cmdWrapper._dataReaderBuf;
				if (dataReaderBuf == null)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return dataReaderBuf;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00260128 File Offset: 0x0025F528
		private OdbcConnection Connection
		{
			get
			{
				if (this._cmdWrapper != null)
				{
					return this._cmdWrapper.Connection;
				}
				return null;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00260158 File Offset: 0x0025F558
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x00260178 File Offset: 0x0025F578
		internal OdbcCommand Command
		{
			get
			{
				return this.command;
			}
			set
			{
				this.command = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00260198 File Offset: 0x0025F598
		private OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._cmdWrapper.StatementHandle;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x002601B8 File Offset: 0x0025F5B8
		private OdbcStatementHandle KeyInfoStatementHandle
		{
			get
			{
				return this._cmdWrapper.KeyInfoStatement;
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x002601D8 File Offset: 0x0025F5D8
		internal bool IsBehavior(CommandBehavior behavior)
		{
			return this.IsCommandBehavior(behavior);
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x002601F8 File Offset: 0x0025F5F8
		internal bool IsCancelingCommand
		{
			get
			{
				return this.command != null && this.command.Canceling;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00260228 File Offset: 0x0025F628
		internal bool IsNonCancelingCommand
		{
			get
			{
				return this.command != null && !this.command.Canceling;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x00260258 File Offset: 0x0025F658
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00260288 File Offset: 0x0025F688
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._noMoreResults)
				{
					return 0;
				}
				if (this.dataCache == null)
				{
					short num;
					ODBC32.RetCode retCode = this.FieldCountNoThrow(out num);
					if (retCode != ODBC32.RetCode.SUCCESS)
					{
						this.Connection.HandleError(this.StatementHandle, retCode);
					}
				}
				if (this.dataCache == null)
				{
					return 0;
				}
				return this.dataCache._count;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x002602F8 File Offset: 0x0025F6F8
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._hasRows == OdbcDataReader.HasRowsStatus.DontKnow)
				{
					this.Read();
					this._skipReadOnce = true;
				}
				return this._hasRows == OdbcDataReader.HasRowsStatus.HasRows;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00260338 File Offset: 0x0025F738
		internal ODBC32.RetCode FieldCountNoThrow(out short cColsAffected)
		{
			if (this.IsCancelingCommand)
			{
				cColsAffected = 0;
				return ODBC32.RetCode.ERROR;
			}
			ODBC32.RetCode retCode = this.StatementHandle.NumberOfResultColumns(out cColsAffected);
			if (retCode == ODBC32.RetCode.SUCCESS)
			{
				this._hiddenColumns = 0;
				if (this.IsCommandBehavior(CommandBehavior.KeyInfo) && !this.Connection.ProviderInfo.NoSqlSoptSSNoBrowseTable && !this.Connection.ProviderInfo.NoSqlSoptSSHiddenColumns)
				{
					for (int i = 0; i < (int)cColsAffected; i++)
					{
						if (this.GetColAttribute(i, (ODBC32.SQL_DESC)1211, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE)._value == 1L)
						{
							this._hiddenColumns = (int)cColsAffected - i;
							cColsAffected = (short)i;
							break;
						}
					}
				}
				this.dataCache = new DbCache(this, (int)cColsAffected);
			}
			else
			{
				cColsAffected = 0;
			}
			return retCode;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x002603E8 File Offset: 0x0025F7E8
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00260408 File Offset: 0x0025F808
		private SQLLEN GetRowCount()
		{
			if (!this.IsClosed)
			{
				SQLLEN result;
				ODBC32.RetCode retCode = this.StatementHandle.RowCount(out result);
				if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					return result;
				}
			}
			return -1;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00260448 File Offset: 0x0025F848
		internal int CalculateRecordsAffected(int cRowsAffected)
		{
			if (0 <= cRowsAffected)
			{
				if (-1 == this.recordAffected)
				{
					this.recordAffected = cRowsAffected;
				}
				else
				{
					this.recordAffected += cRowsAffected;
				}
			}
			return this.recordAffected;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x00260488 File Offset: 0x0025F888
		public override int RecordsAffected
		{
			get
			{
				return this.recordAffected;
			}
		}

		// Token: 0x170003A3 RID: 931
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x170003A4 RID: 932
		public override object this[string value]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(value));
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x002604E8 File Offset: 0x0025F8E8
		public override void Close()
		{
			this.Close(false);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00260508 File Offset: 0x0025F908
		private void Close(bool disposing)
		{
			Exception ex = null;
			CMDWrapper cmdWrapper = this._cmdWrapper;
			if (cmdWrapper != null && cmdWrapper.StatementHandle != null)
			{
				if (this.IsNonCancelingCommand)
				{
					this.NextResult(disposing, !disposing);
					if (this.command != null)
					{
						if (this.command.HasParameters)
						{
							this.command.Parameters.GetOutputValues(this._cmdWrapper);
						}
						cmdWrapper.FreeStatementHandle(ODBC32.STMT.CLOSE);
						this.command.CloseFromDataReader();
					}
				}
				cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.CLOSE);
			}
			if (this.command != null)
			{
				this.command.CloseFromDataReader();
				if (this.IsCommandBehavior(CommandBehavior.CloseConnection))
				{
					this.command.Parameters.RebindCollection = true;
					this.Connection.Close();
				}
			}
			else if (cmdWrapper != null)
			{
				cmdWrapper.Dispose();
			}
			this.command = null;
			this._isClosed = true;
			this.dataCache = null;
			this.metadata = null;
			this.schemaTable = null;
			this._isRead = false;
			this._hasRows = OdbcDataReader.HasRowsStatus.DontKnow;
			this._isValidResult = false;
			this._noMoreResults = true;
			this._noMoreRows = true;
			this._fieldNameLookup = null;
			this.SetCurrentRowColumnInfo(-1, 0);
			if (ex != null && !disposing)
			{
				throw ex;
			}
			this._cmdWrapper = null;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00260638 File Offset: 0x0025FA38
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(true);
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00260658 File Offset: 0x0025FA58
		public override string GetDataTypeName(int i)
		{
			if (this.dataCache != null)
			{
				DbSchemaInfo schema = this.dataCache.GetSchema(i);
				if (schema._typename == null)
				{
					schema._typename = this.GetColAttributeStr(i, ODBC32.SQL_DESC.TYPE_NAME, ODBC32.SQL_COLUMN.TYPE_NAME, ODBC32.HANDLER.THROW);
				}
				return schema._typename;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x002606A8 File Offset: 0x0025FAA8
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x002606C8 File Offset: 0x0025FAC8
		public override Type GetFieldType(int i)
		{
			if (this.dataCache != null)
			{
				DbSchemaInfo schema = this.dataCache.GetSchema(i);
				if (schema._type == null)
				{
					schema._type = this.GetSqlType(i)._type;
				}
				return schema._type;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00260718 File Offset: 0x0025FB18
		public override string GetName(int i)
		{
			if (this.dataCache != null)
			{
				DbSchemaInfo schema = this.dataCache.GetSchema(i);
				if (schema._name == null)
				{
					schema._name = this.GetColAttributeStr(i, ODBC32.SQL_DESC.NAME, ODBC32.SQL_COLUMN.NAME, ODBC32.HANDLER.THROW);
					if (schema._name == null)
					{
						schema._name = "";
					}
				}
				return schema._name;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00260778 File Offset: 0x0025FB78
		public override int GetOrdinal(string value)
		{
			if (this._fieldNameLookup == null)
			{
				if (this.dataCache == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(value);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x002607B8 File Offset: 0x0025FBB8
		private int IndexOf(string value)
		{
			if (this._fieldNameLookup == null)
			{
				if (this.dataCache == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.IndexOf(value);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x002607F8 File Offset: 0x0025FBF8
		private bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00260818 File Offset: 0x0025FC18
		internal object GetValue(int i, TypeMap typemap)
		{
			ODBC32.SQL_TYPE sql_type = typemap._sql_type;
			if (sql_type != ODBC32.SQL_TYPE.SS_VARIANT)
			{
				switch (sql_type)
				{
				case ODBC32.SQL_TYPE.GUID:
					return this.internalGetGuid(i);
				case ODBC32.SQL_TYPE.WLONGVARCHAR:
				case ODBC32.SQL_TYPE.WVARCHAR:
				case ODBC32.SQL_TYPE.WCHAR:
				case ODBC32.SQL_TYPE.LONGVARCHAR:
				case ODBC32.SQL_TYPE.CHAR:
				case ODBC32.SQL_TYPE.VARCHAR:
					return this.internalGetString(i);
				case ODBC32.SQL_TYPE.BIT:
					return this.internalGetBoolean(i);
				case ODBC32.SQL_TYPE.TINYINT:
					return this.internalGetByte(i);
				case ODBC32.SQL_TYPE.BIGINT:
					return this.internalGetInt64(i);
				case ODBC32.SQL_TYPE.LONGVARBINARY:
				case ODBC32.SQL_TYPE.VARBINARY:
				case ODBC32.SQL_TYPE.BINARY:
					return this.internalGetBytes(i);
				case (ODBC32.SQL_TYPE)0:
				case (ODBC32.SQL_TYPE)9:
				case (ODBC32.SQL_TYPE)10:
				case ODBC32.SQL_TYPE.TIMESTAMP:
					break;
				case ODBC32.SQL_TYPE.NUMERIC:
				case ODBC32.SQL_TYPE.DECIMAL:
					return this.internalGetDecimal(i);
				case ODBC32.SQL_TYPE.INTEGER:
					return this.internalGetInt32(i);
				case ODBC32.SQL_TYPE.SMALLINT:
					return this.internalGetInt16(i);
				case ODBC32.SQL_TYPE.FLOAT:
				case ODBC32.SQL_TYPE.DOUBLE:
					return this.internalGetDouble(i);
				case ODBC32.SQL_TYPE.REAL:
					return this.internalGetFloat(i);
				default:
					switch (sql_type)
					{
					case ODBC32.SQL_TYPE.TYPE_DATE:
						return this.internalGetDate(i);
					case ODBC32.SQL_TYPE.TYPE_TIME:
						return this.internalGetTime(i);
					case ODBC32.SQL_TYPE.TYPE_TIMESTAMP:
						return this.internalGetDateTime(i);
					}
					break;
				}
				return this.internalGetBytes(i);
			}
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null)
				{
					int num;
					bool flag = this.QueryFieldInfo(i, ODBC32.SQL_C.BINARY, out num);
					if (flag)
					{
						ODBC32.SQL_TYPE sqltype = (ODBC32.SQL_TYPE)this.GetColAttribute(i, (ODBC32.SQL_DESC)1216, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.THROW)._value;
						return this.GetValue(i, TypeMap.FromSqlType(sqltype));
					}
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00260998 File Offset: 0x0025FD98
		public override object GetValue(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null)
				{
					this.dataCache[i] = this.GetValue(i, this.GetSqlType(i));
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x002609E8 File Offset: 0x0025FDE8
		public override int GetValues(object[] values)
		{
			if (this._isRead)
			{
				int num = Math.Min(values.Length, this.FieldCount);
				for (int i = 0; i < num; i++)
				{
					values[i] = this.GetValue(i);
				}
				return num;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00260A38 File Offset: 0x0025FE38
		private TypeMap GetSqlType(int i)
		{
			DbSchemaInfo schema = this.dataCache.GetSchema(i);
			TypeMap typeMap;
			if (schema._dbtype == null)
			{
				schema._dbtype = new ODBC32.SQL_TYPE?((ODBC32.SQL_TYPE)((int)this.GetColAttribute(i, ODBC32.SQL_DESC.CONCISE_TYPE, ODBC32.SQL_COLUMN.TYPE, ODBC32.HANDLER.THROW)._value));
				typeMap = TypeMap.FromSqlType(schema._dbtype.Value);
				if (typeMap._signType)
				{
					bool unsigned = this.GetColAttribute(i, ODBC32.SQL_DESC.UNSIGNED, ODBC32.SQL_COLUMN.UNSIGNED, ODBC32.HANDLER.THROW)._value != 0L;
					typeMap = TypeMap.UpgradeSignedType(typeMap, unsigned);
					schema._dbtype = new ODBC32.SQL_TYPE?(typeMap._sql_type);
				}
			}
			else
			{
				typeMap = TypeMap.FromSqlType(schema._dbtype.Value);
			}
			this.Connection.SetSupportedType(schema._dbtype.Value);
			return typeMap;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00260AF8 File Offset: 0x0025FEF8
		public override bool IsDBNull(int i)
		{
			if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				return Convert.IsDBNull(this.GetValue(i));
			}
			object obj = this.dataCache[i];
			if (obj != null)
			{
				return Convert.IsDBNull(obj);
			}
			TypeMap sqlType = this.GetSqlType(i);
			if (sqlType._bufferSize > 0)
			{
				return Convert.IsDBNull(this.GetValue(i));
			}
			int num;
			return !this.QueryFieldInfo(i, sqlType._sql_c, out num);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00260B68 File Offset: 0x0025FF68
		public override byte GetByte(int i)
		{
			return (byte)this.internalGetByte(i);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00260B88 File Offset: 0x0025FF88
		private object internalGetByte(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.UTINYINT))
				{
					this.dataCache[i] = this.Buffer.ReadByte(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00260BE8 File Offset: 0x0025FFE8
		public override char GetChar(int i)
		{
			return (char)this.internalGetChar(i);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00260C08 File Offset: 0x00260008
		private object internalGetChar(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					this.dataCache[i] = this.Buffer.ReadChar(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00260C68 File Offset: 0x00260068
		public override short GetInt16(int i)
		{
			return (short)this.internalGetInt16(i);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00260C88 File Offset: 0x00260088
		private object internalGetInt16(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.SSHORT))
				{
					this.dataCache[i] = this.Buffer.ReadInt16(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00260CE8 File Offset: 0x002600E8
		public override int GetInt32(int i)
		{
			return (int)this.internalGetInt32(i);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00260D08 File Offset: 0x00260108
		private object internalGetInt32(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.SLONG))
				{
					this.dataCache[i] = this.Buffer.ReadInt32(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00260D68 File Offset: 0x00260168
		public override long GetInt64(int i)
		{
			return (long)this.internalGetInt64(i);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00260D88 File Offset: 0x00260188
		private object internalGetInt64(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					string s = (string)this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.WCHAR, -3);
					this.dataCache[i] = long.Parse(s, CultureInfo.InvariantCulture);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00260E08 File Offset: 0x00260208
		public override bool GetBoolean(int i)
		{
			return (bool)this.internalGetBoolean(i);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00260E28 File Offset: 0x00260228
		private object internalGetBoolean(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.BIT))
				{
					this.dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.BIT, -1);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00260E88 File Offset: 0x00260288
		public override float GetFloat(int i)
		{
			return (float)this.internalGetFloat(i);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00260EA8 File Offset: 0x002602A8
		private object internalGetFloat(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.REAL))
				{
					this.dataCache[i] = this.Buffer.ReadSingle(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00260F08 File Offset: 0x00260308
		public DateTime GetDate(int i)
		{
			return (DateTime)this.internalGetDate(i);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00260F28 File Offset: 0x00260328
		private object internalGetDate(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_DATE))
				{
					this.dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_DATE, -1);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00260F88 File Offset: 0x00260388
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.internalGetDateTime(i);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00260FA8 File Offset: 0x002603A8
		private object internalGetDateTime(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_TIMESTAMP))
				{
					this.dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_TIMESTAMP, -1);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00261008 File Offset: 0x00260408
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.internalGetDecimal(i);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00261028 File Offset: 0x00260428
		private object internalGetDecimal(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.WCHAR))
				{
					string text = null;
					try
					{
						text = (string)this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.WCHAR, -3);
						this.dataCache[i] = decimal.Parse(text, CultureInfo.InvariantCulture);
					}
					catch (OverflowException ex)
					{
						this.dataCache[i] = text;
						throw ex;
					}
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x002610D8 File Offset: 0x002604D8
		public override double GetDouble(int i)
		{
			return (double)this.internalGetDouble(i);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x002610F8 File Offset: 0x002604F8
		private object internalGetDouble(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.DOUBLE))
				{
					this.dataCache[i] = this.Buffer.ReadDouble(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00261158 File Offset: 0x00260558
		public override Guid GetGuid(int i)
		{
			return (Guid)this.internalGetGuid(i);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00261178 File Offset: 0x00260578
		private object internalGetGuid(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.GUID))
				{
					this.dataCache[i] = this.Buffer.ReadGuid(0);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x002611D8 File Offset: 0x002605D8
		public override string GetString(int i)
		{
			return (string)this.internalGetString(i);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x002611F8 File Offset: 0x002605F8
		private object internalGetString(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null)
				{
					CNativeBuffer buffer = this.Buffer;
					int num = buffer.Length - 4;
					int num2;
					if (this.GetData(i, ODBC32.SQL_C.WCHAR, buffer.Length - 2, out num2))
					{
						if (num2 <= num && -4 != num2)
						{
							string text = buffer.PtrToStringUni(0, Math.Min(num2, num) / 2);
							this.dataCache[i] = text;
							return text;
						}
						char[] array = new char[num / 2];
						int num3 = (num2 == -4) ? num : num2;
						StringBuilder stringBuilder = new StringBuilder(num3 / 2);
						int num4 = num;
						int num5 = (-4 == num2) ? -1 : (num2 - num4);
						bool data;
						do
						{
							int num6 = num4 / 2;
							buffer.ReadChars(0, array, 0, num6);
							stringBuilder.Append(array, 0, num6);
							if (num5 == 0)
							{
								break;
							}
							data = this.GetData(i, ODBC32.SQL_C.WCHAR, buffer.Length - 2, out num2);
							if (-4 != num2)
							{
								num4 = Math.Min(num2, num);
								if (0 < num5)
								{
									num5 -= num4;
								}
								else
								{
									num5 = 0;
								}
							}
						}
						while (data);
						this.dataCache[i] = stringBuilder.ToString();
					}
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00261328 File Offset: 0x00260728
		public TimeSpan GetTime(int i)
		{
			return (TimeSpan)this.internalGetTime(i);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00261348 File Offset: 0x00260748
		private object internalGetTime(int i)
		{
			if (this._isRead)
			{
				if (this.dataCache.AccessIndex(i) == null && this.GetData(i, ODBC32.SQL_C.TYPE_TIME))
				{
					this.dataCache[i] = this.Buffer.MarshalToManaged(0, ODBC32.SQL_C.TYPE_TIME, -1);
				}
				return this.dataCache[i];
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x002613A8 File Offset: 0x002607A8
		private void SetCurrentRowColumnInfo(int row, int column)
		{
			if (this._row != row || this._column != column)
			{
				this._row = row;
				this._column = column;
				this._sequentialBytesRead = 0L;
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x002613E8 File Offset: 0x002607E8
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this.GetBytesOrChars(i, dataIndex, buffer, false, bufferIndex, length);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00261408 File Offset: 0x00260808
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this.GetBytesOrChars(i, dataIndex, buffer, true, bufferIndex, length);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00261428 File Offset: 0x00260828
		private long GetBytesOrChars(int i, long dataIndex, Array buffer, bool isCharsBuffer, int bufferIndex, int length)
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderNoData();
			}
			if (!this._isRead)
			{
				throw ADP.DataReaderNoData();
			}
			if (dataIndex < 0L)
			{
				throw ADP.ArgumentOutOfRange("dataIndex");
			}
			if (bufferIndex < 0)
			{
				throw ADP.ArgumentOutOfRange("bufferIndex");
			}
			if (length < 0)
			{
				throw ADP.ArgumentOutOfRange("length");
			}
			string method = isCharsBuffer ? "GetChars" : "GetBytes";
			this.SetCurrentRowColumnInfo(this._row, i);
			object obj;
			if (isCharsBuffer)
			{
				obj = (string)this.dataCache[i];
			}
			else
			{
				obj = (byte[])this.dataCache[i];
			}
			if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) || obj != null)
			{
				if (2147483647L < dataIndex)
				{
					throw ADP.ArgumentOutOfRange("dataIndex");
				}
				if (obj == null)
				{
					if (isCharsBuffer)
					{
						obj = (string)this.internalGetString(i);
					}
					else
					{
						obj = (byte[])this.internalGetBytes(i);
					}
				}
				int num = isCharsBuffer ? ((string)obj).Length : ((byte[])obj).Length;
				if (buffer == null)
				{
					return (long)num;
				}
				if (length == 0)
				{
					return 0L;
				}
				if (dataIndex >= (long)num)
				{
					return 0L;
				}
				int val = num - (int)dataIndex;
				int num2 = Math.Min(val, length);
				num2 = Math.Min(num2, buffer.Length - bufferIndex);
				if (num2 <= 0)
				{
					return 0L;
				}
				if (isCharsBuffer)
				{
					((string)obj).CopyTo((int)dataIndex, (char[])buffer, bufferIndex, num2);
				}
				else
				{
					Array.Copy((byte[])obj, (int)dataIndex, (byte[])buffer, bufferIndex, num2);
				}
				return (long)num2;
			}
			else if (buffer == null)
			{
				ODBC32.SQL_C sqlctype = isCharsBuffer ? ODBC32.SQL_C.WCHAR : ODBC32.SQL_C.BINARY;
				int num3;
				bool flag = !this.QueryFieldInfo(i, sqlctype, out num3);
				if (flag)
				{
					if (isCharsBuffer)
					{
						throw ADP.InvalidCast();
					}
					return -1L;
				}
				else
				{
					if (isCharsBuffer)
					{
						return (long)(num3 / 2);
					}
					return (long)num3;
				}
			}
			else
			{
				if ((isCharsBuffer && dataIndex < this._sequentialBytesRead / 2L) || (!isCharsBuffer && dataIndex < this._sequentialBytesRead))
				{
					throw ADP.NonSeqByteAccess(dataIndex, this._sequentialBytesRead, method);
				}
				if (isCharsBuffer)
				{
					dataIndex -= this._sequentialBytesRead / 2L;
				}
				else
				{
					dataIndex -= this._sequentialBytesRead;
				}
				if (dataIndex > 0L)
				{
					int num4 = this.readBytesOrCharsSequentialAccess(i, null, isCharsBuffer, 0, dataIndex);
					if ((long)num4 < dataIndex)
					{
						return 0L;
					}
				}
				length = Math.Min(length, buffer.Length - bufferIndex);
				if (length <= 0)
				{
					if (isCharsBuffer)
					{
						int num5;
						bool flag2 = !this.QueryFieldInfo(i, ODBC32.SQL_C.WCHAR, out num5);
						if (flag2)
						{
							throw ADP.InvalidCast();
						}
					}
					return 0L;
				}
				return (long)this.readBytesOrCharsSequentialAccess(i, buffer, isCharsBuffer, bufferIndex, (long)length);
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00261698 File Offset: 0x00260A98
		private int readBytesOrCharsSequentialAccess(int i, Array buffer, bool isCharsBuffer, int bufferIndex, long bytesOrCharsLength)
		{
			int num = 0;
			long num2 = isCharsBuffer ? checked(bytesOrCharsLength * 2L) : bytesOrCharsLength;
			CNativeBuffer buffer2 = this.Buffer;
			while (num2 > 0L)
			{
				int num3;
				int num4;
				bool data;
				if (isCharsBuffer)
				{
					num3 = (int)Math.Min(num2, (long)(buffer2.Length - 4));
					data = this.GetData(i, ODBC32.SQL_C.WCHAR, num3 + 2, out num4);
				}
				else
				{
					num3 = (int)Math.Min(num2, (long)(buffer2.Length - 2));
					data = this.GetData(i, ODBC32.SQL_C.BINARY, num3, out num4);
				}
				if (!data)
				{
					if (isCharsBuffer)
					{
						string text = (string)this.dataCache[i];
					}
					else
					{
						byte[] array = (byte[])this.dataCache[i];
					}
				}
				bool flag = false;
				if (num4 == 0)
				{
					break;
				}
				int num5;
				if (-4 == num4)
				{
					num5 = num3;
				}
				else if (num4 > num3)
				{
					num5 = num3;
				}
				else
				{
					num5 = num4;
					flag = true;
				}
				this._sequentialBytesRead += (long)num5;
				if (isCharsBuffer)
				{
					int num6 = num5 / 2;
					if (buffer != null)
					{
						buffer2.ReadChars(0, (char[])buffer, bufferIndex, num6);
						bufferIndex += num6;
					}
					num += num6;
				}
				else
				{
					if (buffer != null)
					{
						buffer2.ReadBytes(0, (byte[])buffer, bufferIndex, num5);
						bufferIndex += num5;
					}
					num += num5;
				}
				num2 -= (long)num5;
				if (flag)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x002617C8 File Offset: 0x00260BC8
		private object internalGetBytes(int i)
		{
			if (this.dataCache.AccessIndex(i) == null)
			{
				int num = this.Buffer.Length - 4;
				int num2 = 0;
				int j;
				if (this.GetData(i, ODBC32.SQL_C.BINARY, num, out j))
				{
					CNativeBuffer buffer = this.Buffer;
					byte[] array;
					if (-4 != j)
					{
						array = new byte[j];
						this.Buffer.ReadBytes(0, array, num2, Math.Min(j, num));
						while (j > num)
						{
							this.GetData(i, ODBC32.SQL_C.BINARY, num, out j);
							num2 += num;
							buffer.ReadBytes(0, array, num2, Math.Min(j, num));
						}
					}
					else
					{
						List<byte[]> list = new List<byte[]>();
						int num3 = 0;
						do
						{
							int num4 = (-4 != j) ? j : num;
							array = new byte[num4];
							num3 += num4;
							buffer.ReadBytes(0, array, 0, num4);
							list.Add(array);
						}
						while (-4 == j && this.GetData(i, ODBC32.SQL_C.BINARY, num, out j));
						array = new byte[num3];
						foreach (byte[] array2 in list)
						{
							array2.CopyTo(array, num2);
							num2 += array2.Length;
						}
					}
					this.dataCache[i] = array;
				}
			}
			return this.dataCache[i];
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00261928 File Offset: 0x00260D28
		private SQLLEN GetColAttribute(int iColumn, ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId, ODBC32.HANDLER handler)
		{
			short num = 0;
			if (this.Connection == null || this._cmdWrapper.Canceling)
			{
				return -1;
			}
			OdbcStatementHandle statementHandle = this.StatementHandle;
			SQLLEN result;
			ODBC32.RetCode retCode;
			if (this.Connection.IsV3Driver)
			{
				retCode = statementHandle.ColumnAttribute(iColumn + 1, (short)v3FieldId, this.Buffer, out num, out result);
			}
			else
			{
				if (v2FieldId == (ODBC32.SQL_COLUMN)(-1))
				{
					return 0;
				}
				retCode = statementHandle.ColumnAttribute(iColumn + 1, (short)v2FieldId, this.Buffer, out num, out result);
			}
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if (retCode == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
				{
					this.Connection.FlagUnsupportedColAttr(v3FieldId, v2FieldId);
				}
				if (handler == ODBC32.HANDLER.THROW)
				{
					this.Connection.HandleError(statementHandle, retCode);
				}
				return -1;
			}
			return result;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x002619E8 File Offset: 0x00260DE8
		private string GetColAttributeStr(int i, ODBC32.SQL_DESC v3FieldId, ODBC32.SQL_COLUMN v2FieldId, ODBC32.HANDLER handler)
		{
			short num = 0;
			CNativeBuffer buffer = this.Buffer;
			buffer.WriteInt16(0, 0);
			OdbcStatementHandle statementHandle = this.StatementHandle;
			if (this.Connection == null || this._cmdWrapper.Canceling || statementHandle == null)
			{
				return "";
			}
			ODBC32.RetCode retCode;
			if (this.Connection.IsV3Driver)
			{
				SQLLEN sqllen;
				retCode = statementHandle.ColumnAttribute(i + 1, (short)v3FieldId, buffer, out num, out sqllen);
			}
			else
			{
				if (v2FieldId == (ODBC32.SQL_COLUMN)(-1))
				{
					return null;
				}
				SQLLEN sqllen;
				retCode = statementHandle.ColumnAttribute(i + 1, (short)v2FieldId, buffer, out num, out sqllen);
			}
			if (retCode != ODBC32.RetCode.SUCCESS || num == 0)
			{
				if (retCode == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
				{
					this.Connection.FlagUnsupportedColAttr(v3FieldId, v2FieldId);
				}
				if (handler == ODBC32.HANDLER.THROW)
				{
					this.Connection.HandleError(statementHandle, retCode);
				}
				return null;
			}
			return buffer.PtrToStringUni(0, (int)(num / 2));
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00261AB8 File Offset: 0x00260EB8
		private string GetDescFieldStr(int i, ODBC32.SQL_DESC attribute, ODBC32.HANDLER handler)
		{
			int num = 0;
			if (this.Connection == null || this._cmdWrapper.Canceling)
			{
				return "";
			}
			if (!this.Connection.IsV3Driver)
			{
				return null;
			}
			CNativeBuffer buffer = this.Buffer;
			using (OdbcDescriptorHandle odbcDescriptorHandle = new OdbcDescriptorHandle(this.StatementHandle, ODBC32.SQL_ATTR.APP_PARAM_DESC))
			{
				ODBC32.RetCode descriptionField = odbcDescriptorHandle.GetDescriptionField(i + 1, attribute, buffer, out num);
				if (descriptionField != ODBC32.RetCode.SUCCESS || num == 0)
				{
					if (descriptionField == ODBC32.RetCode.ERROR && "HY091" == this.Command.GetDiagSqlState())
					{
						this.Connection.FlagUnsupportedColAttr(attribute, ODBC32.SQL_COLUMN.COUNT);
					}
					if (handler == ODBC32.HANDLER.THROW)
					{
						this.Connection.HandleError(this.StatementHandle, descriptionField);
					}
					return null;
				}
			}
			return buffer.PtrToStringUni(0, num / 2);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00261BA8 File Offset: 0x00260FA8
		private bool QueryFieldInfo(int i, ODBC32.SQL_C sqlctype, out int cbLengthOrIndicator)
		{
			int cb = 0;
			if (sqlctype == ODBC32.SQL_C.WCHAR)
			{
				cb = 2;
			}
			return this.GetData(i, sqlctype, cb, out cbLengthOrIndicator);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00261BC8 File Offset: 0x00260FC8
		private bool GetData(int i, ODBC32.SQL_C sqlctype)
		{
			int num;
			return this.GetData(i, sqlctype, this.Buffer.Length - 4, out num);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00261BF8 File Offset: 0x00260FF8
		private bool GetData(int i, ODBC32.SQL_C sqlctype, int cb, out int cbLengthOrIndicator)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.IsCancelingCommand)
			{
				throw ADP.DataReaderNoData();
			}
			CNativeBuffer buffer = this.Buffer;
			ODBC32.RetCode data = this.StatementHandle.GetData(i + 1, sqlctype, buffer, cb, out intPtr);
			ODBC32.RetCode retCode = data;
			switch (retCode)
			{
			case ODBC32.RetCode.SUCCESS:
				break;
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				if ((int)intPtr == -4)
				{
				}
				break;
			default:
				if (retCode != ODBC32.RetCode.NO_DATA)
				{
					this.Connection.HandleError(this.StatementHandle, data);
				}
				else
				{
					if (sqlctype != ODBC32.SQL_C.WCHAR && sqlctype != ODBC32.SQL_C.BINARY)
					{
						this.Connection.HandleError(this.StatementHandle, data);
					}
					if (intPtr == (IntPtr)(-4))
					{
						intPtr = (IntPtr)0;
					}
				}
				break;
			}
			this.SetCurrentRowColumnInfo(this._row, i);
			if (intPtr == (IntPtr)(-1))
			{
				this.dataCache[i] = DBNull.Value;
				cbLengthOrIndicator = 0;
				return false;
			}
			cbLengthOrIndicator = (int)intPtr;
			return true;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00261CE8 File Offset: 0x002610E8
		public override bool Read()
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("Read");
			}
			if (this.IsCancelingCommand)
			{
				this._isRead = false;
				return false;
			}
			if (this._skipReadOnce)
			{
				this._skipReadOnce = false;
				return this._isRead;
			}
			if (this._noMoreRows || this._noMoreResults || this.IsCommandBehavior(CommandBehavior.SchemaOnly))
			{
				return false;
			}
			if (!this._isValidResult)
			{
				return false;
			}
			ODBC32.RetCode retCode = this.StatementHandle.Fetch();
			ODBC32.RetCode retCode2 = retCode;
			switch (retCode2)
			{
			case ODBC32.RetCode.SUCCESS:
				this._hasRows = OdbcDataReader.HasRowsStatus.HasRows;
				this._isRead = true;
				break;
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				this.Connection.HandleErrorNoThrow(this.StatementHandle, retCode);
				this._hasRows = OdbcDataReader.HasRowsStatus.HasRows;
				this._isRead = true;
				break;
			default:
				if (retCode2 != ODBC32.RetCode.NO_DATA)
				{
					this.Connection.HandleError(this.StatementHandle, retCode);
				}
				else
				{
					this._isRead = false;
					if (this._hasRows == OdbcDataReader.HasRowsStatus.DontKnow)
					{
						this._hasRows = OdbcDataReader.HasRowsStatus.HasNoRows;
					}
				}
				break;
			}
			this.dataCache.FlushValues();
			if (this.IsCommandBehavior(CommandBehavior.SingleRow))
			{
				this._noMoreRows = true;
				this.SetCurrentRowColumnInfo(-1, 0);
			}
			else
			{
				this.SetCurrentRowColumnInfo(this._row + 1, 0);
			}
			return this._isRead;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00261E18 File Offset: 0x00261218
		internal void FirstResult()
		{
			SQLLEN rowCount = this.GetRowCount();
			this.CalculateRecordsAffected(rowCount);
			short num;
			if (this.FieldCountNoThrow(out num) == ODBC32.RetCode.SUCCESS && num == 0)
			{
				this.NextResult();
				return;
			}
			this._isValidResult = true;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00261E58 File Offset: 0x00261258
		public override bool NextResult()
		{
			return this.NextResult(false, false);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00261E78 File Offset: 0x00261278
		private bool NextResult(bool disposing, bool allresults)
		{
			ODBC32.RetCode retcode = ODBC32.RetCode.SUCCESS;
			bool flag = false;
			bool flag2 = this.IsCommandBehavior(CommandBehavior.SingleResult);
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("NextResult");
			}
			this._fieldNameLookup = null;
			if (this.IsCancelingCommand || this._noMoreResults)
			{
				return false;
			}
			this._isRead = false;
			this._hasRows = OdbcDataReader.HasRowsStatus.DontKnow;
			this._fieldNameLookup = null;
			this.metadata = null;
			this.schemaTable = null;
			int num = 0;
			OdbcErrorCollection odbcErrorCollection = null;
			ODBC32.RetCode retCode;
			bool flag3;
			do
			{
				this._isValidResult = false;
				retCode = this.StatementHandle.MoreResults();
				flag3 = (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO);
				if (retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this.Connection.HandleErrorNoThrow(this.StatementHandle, retCode);
				}
				else if (!disposing && retCode != ODBC32.RetCode.NO_DATA && retCode != ODBC32.RetCode.SUCCESS)
				{
					if (odbcErrorCollection == null)
					{
						retcode = retCode;
						odbcErrorCollection = new OdbcErrorCollection();
					}
					ODBC32.GetDiagErrors(odbcErrorCollection, null, this.StatementHandle, retCode);
					num++;
				}
				if (!disposing && flag3)
				{
					num = 0;
					SQLLEN rowCount = this.GetRowCount();
					this.CalculateRecordsAffected(rowCount);
					if (!flag2)
					{
						short num2;
						this.FieldCountNoThrow(out num2);
						flag = (0 != num2);
						this._isValidResult = flag;
					}
				}
			}
			while ((!flag2 && flag3 && !flag) || (ODBC32.RetCode.NO_DATA != retCode && allresults && num < 2000) || (flag2 && flag3));
			if (2000 <= num)
			{
				Bid.Trace("<odbc.OdbcDataReader.NextResult|INFO> 2000 consecutive failed results");
			}
			if (retCode == ODBC32.RetCode.NO_DATA)
			{
				this.dataCache = null;
				this._noMoreResults = true;
			}
			if (odbcErrorCollection != null)
			{
				odbcErrorCollection.SetSource(this.Connection.Driver);
				OdbcException ex = OdbcException.CreateException(odbcErrorCollection, retcode);
				this.Connection.ConnectionIsAlive(ex);
				throw ex;
			}
			return flag3;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00262008 File Offset: 0x00261408
		private void BuildMetaDataInfo()
		{
			int fieldCount = this.FieldCount;
			OdbcDataReader.MetaData[] array = new OdbcDataReader.MetaData[fieldCount];
			bool flag = this.IsCommandBehavior(CommandBehavior.KeyInfo);
			List<string> list;
			if (flag)
			{
				list = new List<string>();
			}
			else
			{
				list = null;
			}
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = new OdbcDataReader.MetaData();
				array[i].ordinal = i;
				TypeMap typeMap = TypeMap.FromSqlType((ODBC32.SQL_TYPE)((int)this.GetColAttribute(i, ODBC32.SQL_DESC.CONCISE_TYPE, ODBC32.SQL_COLUMN.TYPE, ODBC32.HANDLER.THROW)._value));
				if (typeMap._signType)
				{
					bool unsigned = this.GetColAttribute(i, ODBC32.SQL_DESC.UNSIGNED, ODBC32.SQL_COLUMN.UNSIGNED, ODBC32.HANDLER.THROW)._value != 0L;
					typeMap = TypeMap.UpgradeSignedType(typeMap, unsigned);
				}
				array[i].typemap = typeMap;
				array[i].size = this.GetColAttribute(i, ODBC32.SQL_DESC.OCTET_LENGTH, ODBC32.SQL_COLUMN.LENGTH, ODBC32.HANDLER.IGNORE);
				switch (array[i].typemap._sql_type)
				{
				case ODBC32.SQL_TYPE.WLONGVARCHAR:
				case ODBC32.SQL_TYPE.WVARCHAR:
				case ODBC32.SQL_TYPE.WCHAR:
				{
					OdbcDataReader.MetaData metaData = array[i];
					metaData.size /= 2;
					break;
				}
				}
				array[i].precision = (byte)this.GetColAttribute(i, (ODBC32.SQL_DESC)4, ODBC32.SQL_COLUMN.PRECISION, ODBC32.HANDLER.IGNORE);
				array[i].scale = (byte)this.GetColAttribute(i, (ODBC32.SQL_DESC)5, ODBC32.SQL_COLUMN.SCALE, ODBC32.HANDLER.IGNORE);
				array[i].isAutoIncrement = (this.GetColAttribute(i, ODBC32.SQL_DESC.AUTO_UNIQUE_VALUE, ODBC32.SQL_COLUMN.AUTO_INCREMENT, ODBC32.HANDLER.IGNORE) == 1);
				array[i].isReadOnly = (this.GetColAttribute(i, ODBC32.SQL_DESC.UPDATABLE, ODBC32.SQL_COLUMN.UPDATABLE, ODBC32.HANDLER.IGNORE) == 0);
				ODBC32.SQL_NULLABILITY sql_NULLABILITY = (ODBC32.SQL_NULLABILITY)this.GetColAttribute(i, ODBC32.SQL_DESC.NULLABLE, ODBC32.SQL_COLUMN.NULLABLE, ODBC32.HANDLER.IGNORE)._value;
				array[i].isNullable = (sql_NULLABILITY == ODBC32.SQL_NULLABILITY.NULLABLE);
				ODBC32.SQL_TYPE sql_type = array[i].typemap._sql_type;
				if (sql_type == ODBC32.SQL_TYPE.WLONGVARCHAR || sql_type == ODBC32.SQL_TYPE.LONGVARBINARY || sql_type == ODBC32.SQL_TYPE.LONGVARCHAR)
				{
					array[i].isLong = true;
				}
				else
				{
					array[i].isLong = false;
				}
				if (this.IsCommandBehavior(CommandBehavior.KeyInfo))
				{
					if (!this.Connection.ProviderInfo.NoSqlCASSColumnKey)
					{
						bool flag2 = this.GetColAttribute(i, (ODBC32.SQL_DESC)1212, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
						if (flag2)
						{
							array[i].isKeyColumn = flag2;
							array[i].isUnique = true;
							flag = false;
						}
					}
					array[i].baseSchemaName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.SCHEMA_NAME, ODBC32.SQL_COLUMN.OWNER_NAME, ODBC32.HANDLER.IGNORE);
					array[i].baseCatalogName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.CATALOG_NAME, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE);
					array[i].baseTableName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.BASE_TABLE_NAME, ODBC32.SQL_COLUMN.TABLE_NAME, ODBC32.HANDLER.IGNORE);
					array[i].baseColumnName = this.GetColAttributeStr(i, ODBC32.SQL_DESC.BASE_COLUMN_NAME, ODBC32.SQL_COLUMN.NAME, ODBC32.HANDLER.IGNORE);
					if (this.Connection.IsV3Driver)
					{
						if (array[i].baseTableName == null || array[i].baseTableName.Length == 0)
						{
							array[i].baseTableName = this.GetDescFieldStr(i, ODBC32.SQL_DESC.BASE_TABLE_NAME, ODBC32.HANDLER.IGNORE);
						}
						if (array[i].baseColumnName == null || array[i].baseColumnName.Length == 0)
						{
							array[i].baseColumnName = this.GetDescFieldStr(i, ODBC32.SQL_DESC.BASE_COLUMN_NAME, ODBC32.HANDLER.IGNORE);
						}
					}
					if (array[i].baseTableName != null && !list.Contains(array[i].baseTableName))
					{
						list.Add(array[i].baseTableName);
					}
				}
				if ((array[i].isKeyColumn || array[i].isAutoIncrement) && sql_NULLABILITY == ODBC32.SQL_NULLABILITY.UNKNOWN)
				{
					array[i].isNullable = false;
				}
			}
			if (!this.Connection.ProviderInfo.NoSqlCASSColumnKey)
			{
				for (int j = fieldCount; j < fieldCount + this._hiddenColumns; j++)
				{
					bool flag2 = this.GetColAttribute(j, (ODBC32.SQL_DESC)1212, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
					if (flag2)
					{
						bool flag3 = this.GetColAttribute(j, (ODBC32.SQL_DESC)1211, (ODBC32.SQL_COLUMN)(-1), ODBC32.HANDLER.IGNORE) == 1;
						if (flag3)
						{
							for (int k = 0; k < fieldCount; k++)
							{
								array[k].isKeyColumn = false;
								array[k].isUnique = false;
							}
						}
					}
				}
			}
			this.metadata = array;
			if (this.IsCommandBehavior(CommandBehavior.KeyInfo))
			{
				if (list != null && list.Count > 0)
				{
					List<string>.Enumerator enumerator = list.GetEnumerator();
					OdbcDataReader.QualifiedTableName qualifiedTableName = new OdbcDataReader.QualifiedTableName(this.Connection.QuoteChar("GetSchemaTable"));
					while (enumerator.MoveNext())
					{
						string table = enumerator.Current;
						qualifiedTableName.Table = table;
						if (this.RetrieveKeyInfo(flag, qualifiedTableName, false) <= 0)
						{
							this.RetrieveKeyInfo(flag, qualifiedTableName, true);
						}
					}
					return;
				}
				OdbcDataReader.QualifiedTableName qualifiedTableName2 = new OdbcDataReader.QualifiedTableName(this.Connection.QuoteChar("GetSchemaTable"), this.GetTableNameFromCommandText());
				if (!ADP.IsEmpty(qualifiedTableName2.Table))
				{
					this.SetBaseTableNames(qualifiedTableName2);
					if (this.RetrieveKeyInfo(flag, qualifiedTableName2, false) <= 0)
					{
						this.RetrieveKeyInfo(flag, qualifiedTableName2, true);
					}
				}
			}
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00262458 File Offset: 0x00261858
		private DataTable NewSchemaTable()
		{
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = this.FieldCount;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(new DataColumn("ColumnName", typeof(string)));
			columns.Add(new DataColumn("ColumnOrdinal", typeof(int)));
			columns.Add(new DataColumn("ColumnSize", typeof(int)));
			columns.Add(new DataColumn("NumericPrecision", typeof(short)));
			columns.Add(new DataColumn("NumericScale", typeof(short)));
			columns.Add(new DataColumn("DataType", typeof(object)));
			columns.Add(new DataColumn("ProviderType", typeof(int)));
			columns.Add(new DataColumn("IsLong", typeof(bool)));
			columns.Add(new DataColumn("AllowDBNull", typeof(bool)));
			columns.Add(new DataColumn("IsReadOnly", typeof(bool)));
			columns.Add(new DataColumn("IsRowVersion", typeof(bool)));
			columns.Add(new DataColumn("IsUnique", typeof(bool)));
			columns.Add(new DataColumn("IsKey", typeof(bool)));
			columns.Add(new DataColumn("IsAutoIncrement", typeof(bool)));
			columns.Add(new DataColumn("BaseSchemaName", typeof(string)));
			columns.Add(new DataColumn("BaseCatalogName", typeof(string)));
			columns.Add(new DataColumn("BaseTableName", typeof(string)));
			columns.Add(new DataColumn("BaseColumnName", typeof(string)));
			foreach (object obj in columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataColumn.ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x002626B8 File Offset: 0x00261AB8
		public override DataTable GetSchemaTable()
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed("GetSchemaTable");
			}
			if (this._noMoreResults)
			{
				return null;
			}
			if (this.schemaTable != null)
			{
				return this.schemaTable;
			}
			DataTable dataTable = this.NewSchemaTable();
			if (this.FieldCount == 0)
			{
				return dataTable;
			}
			if (this.metadata == null)
			{
				this.BuildMetaDataInfo();
			}
			DataColumn column = dataTable.Columns["ColumnName"];
			DataColumn column2 = dataTable.Columns["ColumnOrdinal"];
			DataColumn column3 = dataTable.Columns["ColumnSize"];
			DataColumn column4 = dataTable.Columns["NumericPrecision"];
			DataColumn column5 = dataTable.Columns["NumericScale"];
			DataColumn column6 = dataTable.Columns["DataType"];
			DataColumn column7 = dataTable.Columns["ProviderType"];
			DataColumn column8 = dataTable.Columns["IsLong"];
			DataColumn column9 = dataTable.Columns["AllowDBNull"];
			DataColumn column10 = dataTable.Columns["IsReadOnly"];
			DataColumn column11 = dataTable.Columns["IsRowVersion"];
			DataColumn column12 = dataTable.Columns["IsUnique"];
			DataColumn column13 = dataTable.Columns["IsKey"];
			DataColumn column14 = dataTable.Columns["IsAutoIncrement"];
			DataColumn column15 = dataTable.Columns["BaseSchemaName"];
			DataColumn column16 = dataTable.Columns["BaseCatalogName"];
			DataColumn column17 = dataTable.Columns["BaseTableName"];
			DataColumn column18 = dataTable.Columns["BaseColumnName"];
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[column] = this.GetName(i);
				dataRow[column2] = i;
				dataRow[column3] = (int)Math.Min(Math.Max(-2147483648L, this.metadata[i].size._value), 2147483647L);
				dataRow[column4] = (short)this.metadata[i].precision;
				dataRow[column5] = (short)this.metadata[i].scale;
				dataRow[column6] = this.metadata[i].typemap._type;
				dataRow[column7] = this.metadata[i].typemap._odbcType;
				dataRow[column8] = this.metadata[i].isLong;
				dataRow[column9] = this.metadata[i].isNullable;
				dataRow[column10] = this.metadata[i].isReadOnly;
				dataRow[column11] = this.metadata[i].isRowVersion;
				dataRow[column12] = this.metadata[i].isUnique;
				dataRow[column13] = this.metadata[i].isKeyColumn;
				dataRow[column14] = this.metadata[i].isAutoIncrement;
				dataRow[column15] = this.metadata[i].baseSchemaName;
				dataRow[column16] = this.metadata[i].baseCatalogName;
				dataRow[column17] = this.metadata[i].baseTableName;
				dataRow[column18] = this.metadata[i].baseColumnName;
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			this.schemaTable = dataTable;
			return dataTable;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00262A58 File Offset: 0x00261E58
		internal int RetrieveKeyInfo(bool needkeyinfo, OdbcDataReader.QualifiedTableName qualifiedTableName, bool quoted)
		{
			int num = 0;
			IntPtr value = IntPtr.Zero;
			if (this.IsClosed || this._cmdWrapper == null)
			{
				return 0;
			}
			this._cmdWrapper.CreateKeyInfoStatementHandle();
			CNativeBuffer buffer = this.Buffer;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag);
				ODBC32.RetCode retCode;
				if (needkeyinfo)
				{
					if (!this.Connection.ProviderInfo.NoSqlPrimaryKeys)
					{
						retCode = this.KeyInfoStatementHandle.PrimaryKeys(qualifiedTableName.Catalog, qualifiedTableName.Schema, qualifiedTableName.GetTable(quoted));
						if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
						{
							bool flag2 = false;
							buffer.WriteInt16(0, 0);
							retCode = this.KeyInfoStatementHandle.BindColumn2(4, ODBC32.SQL_C.WCHAR, buffer.PtrOffset(0, 256), (IntPtr)256, buffer.PtrOffset(256, IntPtr.Size).Handle);
							while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
							{
								value = buffer.ReadIntPtr(256);
								string text = buffer.PtrToStringUni(0, (int)value / 2);
								int ordinalFromBaseColName = this.GetOrdinalFromBaseColName(text);
								if (ordinalFromBaseColName == -1)
								{
									flag2 = true;
									break;
								}
								num++;
								this.metadata[ordinalFromBaseColName].isKeyColumn = true;
								this.metadata[ordinalFromBaseColName].isUnique = true;
								this.metadata[ordinalFromBaseColName].isNullable = false;
								this.metadata[ordinalFromBaseColName].baseTableName = qualifiedTableName.Table;
								if (this.metadata[ordinalFromBaseColName].baseColumnName == null)
								{
									this.metadata[ordinalFromBaseColName].baseColumnName = text;
								}
							}
							if (flag2)
							{
								foreach (OdbcDataReader.MetaData metaData in this.metadata)
								{
									metaData.isKeyColumn = false;
								}
							}
							retCode = this.KeyInfoStatementHandle.BindColumn3(4, ODBC32.SQL_C.WCHAR, buffer.DangerousGetHandle());
						}
						else if ("IM001" == this.Command.GetDiagSqlState())
						{
							this.Connection.ProviderInfo.NoSqlPrimaryKeys = true;
						}
					}
					if (num == 0)
					{
						this.KeyInfoStatementHandle.MoreResults();
						num += this.RetrieveKeyInfoFromStatistics(qualifiedTableName, quoted);
					}
					this.KeyInfoStatementHandle.MoreResults();
				}
				retCode = this.KeyInfoStatementHandle.SpecialColumns(qualifiedTableName.GetTable(quoted));
				if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					value = IntPtr.Zero;
					buffer.WriteInt16(0, 0);
					retCode = this.KeyInfoStatementHandle.BindColumn2(2, ODBC32.SQL_C.WCHAR, buffer.PtrOffset(0, 256), (IntPtr)256, buffer.PtrOffset(256, IntPtr.Size).Handle);
					while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
					{
						value = buffer.ReadIntPtr(256);
						string text = buffer.PtrToStringUni(0, (int)value / 2);
						int ordinalFromBaseColName = this.GetOrdinalFromBaseColName(text);
						if (ordinalFromBaseColName != -1)
						{
							this.metadata[ordinalFromBaseColName].isRowVersion = true;
							if (this.metadata[ordinalFromBaseColName].baseColumnName == null)
							{
								this.metadata[ordinalFromBaseColName].baseColumnName = text;
							}
						}
					}
					retCode = this.KeyInfoStatementHandle.BindColumn3(2, ODBC32.SQL_C.WCHAR, buffer.DangerousGetHandle());
					retCode = this.KeyInfoStatementHandle.MoreResults();
				}
			}
			finally
			{
				if (flag)
				{
					buffer.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00262D98 File Offset: 0x00262198
		private int RetrieveKeyInfoFromStatistics(OdbcDataReader.QualifiedTableName qualifiedTableName, bool quoted)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string currentindexname = string.Empty;
			int[] array = new int[16];
			int[] array2 = new int[16];
			int num = 0;
			int num2 = 0;
			bool flag = false;
			IntPtr value = IntPtr.Zero;
			IntPtr value2 = IntPtr.Zero;
			int num3 = 0;
			string tableName = string.Copy(qualifiedTableName.GetTable(quoted));
			ODBC32.RetCode retCode = this.KeyInfoStatementHandle.Statistics(tableName);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				return 0;
			}
			CNativeBuffer buffer = this.Buffer;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag2);
				HandleRef buffer2 = buffer.PtrOffset(0, 256);
				HandleRef buffer3 = buffer.PtrOffset(256, 256);
				HandleRef buffer4 = buffer.PtrOffset(512, 4);
				IntPtr handle = buffer.PtrOffset(520, IntPtr.Size).Handle;
				IntPtr handle2 = buffer.PtrOffset(528, IntPtr.Size).Handle;
				IntPtr handle3 = buffer.PtrOffset(536, IntPtr.Size).Handle;
				buffer.WriteInt16(256, 0);
				retCode = this.KeyInfoStatementHandle.BindColumn2(6, ODBC32.SQL_C.WCHAR, buffer3, (IntPtr)256, handle2);
				retCode = this.KeyInfoStatementHandle.BindColumn2(8, ODBC32.SQL_C.SSHORT, buffer4, (IntPtr)4, handle3);
				buffer.WriteInt16(512, 0);
				retCode = this.KeyInfoStatementHandle.BindColumn2(9, ODBC32.SQL_C.WCHAR, buffer2, (IntPtr)256, handle);
				while (this.KeyInfoStatementHandle.Fetch() == ODBC32.RetCode.SUCCESS)
				{
					value2 = buffer.ReadIntPtr(520);
					value = buffer.ReadIntPtr(528);
					if (buffer.ReadInt16(256) != 0)
					{
						text = buffer.PtrToStringUni(0, (int)value2 / 2);
						text2 = buffer.PtrToStringUni(256, (int)value / 2);
						int num4 = (int)buffer.ReadInt16(512);
						if (this.SameIndexColumn(currentindexname, text2, num4, num2))
						{
							if (!flag)
							{
								num4 = this.GetOrdinalFromBaseColName(text, qualifiedTableName.Table);
								if (num4 == -1)
								{
									flag = true;
								}
								else if (num2 < 16)
								{
									array[num2++] = num4;
								}
								else
								{
									flag = true;
								}
							}
						}
						else
						{
							if (!flag && num2 != 0 && (num == 0 || num > num2))
							{
								num = num2;
								for (int i = 0; i < num2; i++)
								{
									array2[i] = array[i];
								}
							}
							num2 = 0;
							currentindexname = text2;
							flag = false;
							num4 = this.GetOrdinalFromBaseColName(text, qualifiedTableName.Table);
							if (num4 == -1)
							{
								flag = true;
							}
							else
							{
								array[num2++] = num4;
							}
						}
					}
				}
				if (!flag && num2 != 0 && (num == 0 || num > num2))
				{
					num = num2;
					for (int j = 0; j < num2; j++)
					{
						array2[j] = array[j];
					}
				}
				if (num != 0)
				{
					for (int k = 0; k < num; k++)
					{
						int num5 = array2[k];
						num3++;
						this.metadata[num5].isKeyColumn = true;
						this.metadata[num5].isNullable = false;
						this.metadata[num5].isUnique = true;
						if (this.metadata[num5].baseTableName == null)
						{
							this.metadata[num5].baseTableName = qualifiedTableName.Table;
						}
						if (this.metadata[num5].baseColumnName == null)
						{
							this.metadata[num5].baseColumnName = text;
						}
					}
				}
				this._cmdWrapper.FreeKeyInfoStatementHandle(ODBC32.STMT.UNBIND);
			}
			finally
			{
				if (flag2)
				{
					buffer.DangerousRelease();
				}
			}
			return num3;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00263118 File Offset: 0x00262518
		internal bool SameIndexColumn(string currentindexname, string indexname, int ordinal, int ncols)
		{
			return !ADP.IsEmpty(currentindexname) && (currentindexname == indexname && ordinal == ncols + 1);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00263148 File Offset: 0x00262548
		internal int GetOrdinalFromBaseColName(string columnname)
		{
			return this.GetOrdinalFromBaseColName(columnname, null);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00263168 File Offset: 0x00262568
		internal int GetOrdinalFromBaseColName(string columnname, string tablename)
		{
			if (ADP.IsEmpty(columnname))
			{
				return -1;
			}
			if (this.metadata != null)
			{
				int fieldCount = this.FieldCount;
				for (int i = 0; i < fieldCount; i++)
				{
					if (this.metadata[i].baseColumnName != null && columnname == this.metadata[i].baseColumnName)
					{
						if (ADP.IsEmpty(tablename))
						{
							return i;
						}
						if (tablename == this.metadata[i].baseTableName)
						{
							return i;
						}
					}
				}
			}
			return this.IndexOf(columnname);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x002631E8 File Offset: 0x002625E8
		internal string GetTableNameFromCommandText()
		{
			if (this.command == null)
			{
				return null;
			}
			string text = this._cmdText;
			if (ADP.IsEmpty(text))
			{
				return null;
			}
			CStringTokenizer cstringTokenizer = new CStringTokenizer(text, this.Connection.QuoteChar("GetSchemaTable")[0], this.Connection.EscapeChar("GetSchemaTable"));
			int num;
			if (cstringTokenizer.StartsWith("select"))
			{
				num = cstringTokenizer.FindTokenIndex("from");
			}
			else if (cstringTokenizer.StartsWith("insert") || cstringTokenizer.StartsWith("update") || cstringTokenizer.StartsWith("delete"))
			{
				num = cstringTokenizer.CurrentPosition;
			}
			else
			{
				num = -1;
			}
			if (num == -1)
			{
				return null;
			}
			string result = cstringTokenizer.NextToken();
			text = cstringTokenizer.NextToken();
			if (text.Length > 0 && text[0] == ',')
			{
				return null;
			}
			if (text.Length == 2 && (text[0] == 'a' || text[0] == 'A') && (text[1] == 's' || text[1] == 'S'))
			{
				text = cstringTokenizer.NextToken();
				text = cstringTokenizer.NextToken();
				if (text.Length > 0 && text[0] == ',')
				{
					return null;
				}
			}
			return result;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00263318 File Offset: 0x00262718
		internal void SetBaseTableNames(OdbcDataReader.QualifiedTableName qualifiedTableName)
		{
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				if (this.metadata[i].baseTableName == null)
				{
					this.metadata[i].baseTableName = qualifiedTableName.Table;
					this.metadata[i].baseSchemaName = qualifiedTableName.Schema;
					this.metadata[i].baseCatalogName = qualifiedTableName.Catalog;
				}
			}
		}

		// Token: 0x04000FE6 RID: 4070
		private OdbcCommand command;

		// Token: 0x04000FE7 RID: 4071
		private int recordAffected = -1;

		// Token: 0x04000FE8 RID: 4072
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000FE9 RID: 4073
		private DbCache dataCache;

		// Token: 0x04000FEA RID: 4074
		private OdbcDataReader.HasRowsStatus _hasRows;

		// Token: 0x04000FEB RID: 4075
		private bool _isClosed;

		// Token: 0x04000FEC RID: 4076
		private bool _isRead;

		// Token: 0x04000FED RID: 4077
		private bool _isValidResult;

		// Token: 0x04000FEE RID: 4078
		private bool _noMoreResults;

		// Token: 0x04000FEF RID: 4079
		private bool _noMoreRows;

		// Token: 0x04000FF0 RID: 4080
		private bool _skipReadOnce;

		// Token: 0x04000FF1 RID: 4081
		private int _hiddenColumns;

		// Token: 0x04000FF2 RID: 4082
		private CommandBehavior _commandBehavior;

		// Token: 0x04000FF3 RID: 4083
		private int _row = -1;

		// Token: 0x04000FF4 RID: 4084
		private int _column = -1;

		// Token: 0x04000FF5 RID: 4085
		private long _sequentialBytesRead;

		// Token: 0x04000FF6 RID: 4086
		private static int _objectTypeCount;

		// Token: 0x04000FF7 RID: 4087
		internal readonly int ObjectID = Interlocked.Increment(ref OdbcDataReader._objectTypeCount);

		// Token: 0x04000FF8 RID: 4088
		private OdbcDataReader.MetaData[] metadata;

		// Token: 0x04000FF9 RID: 4089
		private DataTable schemaTable;

		// Token: 0x04000FFA RID: 4090
		private string _cmdText;

		// Token: 0x04000FFB RID: 4091
		private CMDWrapper _cmdWrapper;

		// Token: 0x020001E6 RID: 486
		private enum HasRowsStatus
		{
			// Token: 0x04000FFD RID: 4093
			DontKnow,
			// Token: 0x04000FFE RID: 4094
			HasRows,
			// Token: 0x04000FFF RID: 4095
			HasNoRows
		}

		// Token: 0x020001E7 RID: 487
		internal sealed class QualifiedTableName
		{
			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x06001B60 RID: 7008 RVA: 0x00263388 File Offset: 0x00262788
			internal string Catalog
			{
				get
				{
					return this._catalogName;
				}
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x06001B61 RID: 7009 RVA: 0x002633A8 File Offset: 0x002627A8
			internal string Schema
			{
				get
				{
					return this._schemaName;
				}
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x06001B62 RID: 7010 RVA: 0x002633C8 File Offset: 0x002627C8
			// (set) Token: 0x06001B63 RID: 7011 RVA: 0x002633E8 File Offset: 0x002627E8
			internal string Table
			{
				get
				{
					return this._tableName;
				}
				set
				{
					this._quotedTableName = value;
					this._tableName = this.UnQuote(value);
				}
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06001B64 RID: 7012 RVA: 0x00263418 File Offset: 0x00262818
			internal string QuotedTable
			{
				get
				{
					return this._quotedTableName;
				}
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x00263438 File Offset: 0x00262838
			internal string GetTable(bool flag)
			{
				if (!flag)
				{
					return this.Table;
				}
				return this.QuotedTable;
			}

			// Token: 0x06001B66 RID: 7014 RVA: 0x00263458 File Offset: 0x00262858
			internal QualifiedTableName(string quoteChar)
			{
				this._quoteChar = quoteChar;
			}

			// Token: 0x06001B67 RID: 7015 RVA: 0x00263478 File Offset: 0x00262878
			internal QualifiedTableName(string quoteChar, string qualifiedname)
			{
				this._quoteChar = quoteChar;
				string[] array = DbCommandBuilder.ParseProcedureName(qualifiedname, quoteChar, quoteChar);
				this._catalogName = this.UnQuote(array[1]);
				this._schemaName = this.UnQuote(array[2]);
				this._quotedTableName = array[3];
				this._tableName = this.UnQuote(array[3]);
			}

			// Token: 0x06001B68 RID: 7016 RVA: 0x002634D8 File Offset: 0x002628D8
			private string UnQuote(string str)
			{
				if (str != null && str.Length > 0)
				{
					char c = this._quoteChar[0];
					if (str[0] == c && str.Length > 1 && str[str.Length - 1] == c)
					{
						str = str.Substring(1, str.Length - 2);
					}
				}
				return str;
			}

			// Token: 0x04001000 RID: 4096
			private string _catalogName;

			// Token: 0x04001001 RID: 4097
			private string _schemaName;

			// Token: 0x04001002 RID: 4098
			private string _tableName;

			// Token: 0x04001003 RID: 4099
			private string _quotedTableName;

			// Token: 0x04001004 RID: 4100
			private string _quoteChar;
		}

		// Token: 0x020001E8 RID: 488
		private sealed class MetaData
		{
			// Token: 0x04001005 RID: 4101
			internal int ordinal;

			// Token: 0x04001006 RID: 4102
			internal TypeMap typemap;

			// Token: 0x04001007 RID: 4103
			internal SQLLEN size;

			// Token: 0x04001008 RID: 4104
			internal byte precision;

			// Token: 0x04001009 RID: 4105
			internal byte scale;

			// Token: 0x0400100A RID: 4106
			internal bool isAutoIncrement;

			// Token: 0x0400100B RID: 4107
			internal bool isUnique;

			// Token: 0x0400100C RID: 4108
			internal bool isReadOnly;

			// Token: 0x0400100D RID: 4109
			internal bool isNullable;

			// Token: 0x0400100E RID: 4110
			internal bool isRowVersion;

			// Token: 0x0400100F RID: 4111
			internal bool isLong;

			// Token: 0x04001010 RID: 4112
			internal bool isKeyColumn;

			// Token: 0x04001011 RID: 4113
			internal string baseSchemaName;

			// Token: 0x04001012 RID: 4114
			internal string baseCatalogName;

			// Token: 0x04001013 RID: 4115
			internal string baseTableName;

			// Token: 0x04001014 RID: 4116
			internal string baseColumnName;
		}
	}
}
