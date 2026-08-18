using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x0200028C RID: 652
	internal sealed class TypeMap
	{
		// Token: 0x0600272E RID: 10030 RVA: 0x00108D9C File Offset: 0x0010819C
		private TypeMap(OdbcType odbcType, DbType dbType, Type type, ODBC32.SQL_TYPE sql_type, ODBC32.SQL_C sql_c, ODBC32.SQL_C param_sql_c, int bsize, int csize, bool signType)
		{
			this._odbcType = odbcType;
			this._dbType = dbType;
			this._type = type;
			this._sql_type = sql_type;
			this._sql_c = sql_c;
			this._param_sql_c = param_sql_c;
			this._bufferSize = bsize;
			this._columnSize = csize;
			this._signType = signType;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00108DF4 File Offset: 0x001081F4
		internal static TypeMap FromOdbcType(OdbcType odbcType)
		{
			switch (odbcType)
			{
			case OdbcType.BigInt:
				return TypeMap._BigInt;
			case OdbcType.Binary:
				return TypeMap._Binary;
			case OdbcType.Bit:
				return TypeMap._Bit;
			case OdbcType.Char:
				return TypeMap._Char;
			case OdbcType.DateTime:
				return TypeMap._DateTime;
			case OdbcType.Decimal:
				return TypeMap._Decimal;
			case OdbcType.Numeric:
				return TypeMap._Numeric;
			case OdbcType.Double:
				return TypeMap._Double;
			case OdbcType.Image:
				return TypeMap._Image;
			case OdbcType.Int:
				return TypeMap._Int;
			case OdbcType.NChar:
				return TypeMap._NChar;
			case OdbcType.NText:
				return TypeMap._NText;
			case OdbcType.NVarChar:
				return TypeMap._NVarChar;
			case OdbcType.Real:
				return TypeMap._Real;
			case OdbcType.UniqueIdentifier:
				return TypeMap._UniqueId;
			case OdbcType.SmallDateTime:
				return TypeMap._SmallDT;
			case OdbcType.SmallInt:
				return TypeMap._SmallInt;
			case OdbcType.Text:
				return TypeMap._Text;
			case OdbcType.Timestamp:
				return TypeMap._Timestamp;
			case OdbcType.TinyInt:
				return TypeMap._TinyInt;
			case OdbcType.VarBinary:
				return TypeMap._VarBinary;
			case OdbcType.VarChar:
				return TypeMap._VarChar;
			case OdbcType.Date:
				return TypeMap._Date;
			case OdbcType.Time:
				return TypeMap._Time;
			default:
				throw ODBC.UnknownOdbcType(odbcType);
			}
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00108F04 File Offset: 0x00108304
		internal static TypeMap FromDbType(DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				return TypeMap._VarChar;
			case DbType.Binary:
				return TypeMap._VarBinary;
			case DbType.Byte:
				return TypeMap._TinyInt;
			case DbType.Boolean:
				return TypeMap._Bit;
			case DbType.Currency:
				return TypeMap._Decimal;
			case DbType.Date:
				return TypeMap._Date;
			case DbType.DateTime:
				return TypeMap._DateTime;
			case DbType.Decimal:
				return TypeMap._Decimal;
			case DbType.Double:
				return TypeMap._Double;
			case DbType.Guid:
				return TypeMap._UniqueId;
			case DbType.Int16:
				return TypeMap._SmallInt;
			case DbType.Int32:
				return TypeMap._Int;
			case DbType.Int64:
				return TypeMap._BigInt;
			case DbType.Single:
				return TypeMap._Real;
			case DbType.String:
				return TypeMap._NVarChar;
			case DbType.Time:
				return TypeMap._Time;
			case DbType.AnsiStringFixedLength:
				return TypeMap._Char;
			case DbType.StringFixedLength:
				return TypeMap._NChar;
			}
			throw ADP.DbTypeNotSupported(dbType, typeof(OdbcType));
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x00108FF8 File Offset: 0x001083F8
		internal static TypeMap FromSystemType(Type dataType)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (dataType == typeof(byte[]))
				{
					return TypeMap._VarBinary;
				}
				if (dataType == typeof(Guid))
				{
					return TypeMap._UniqueId;
				}
				if (dataType == typeof(TimeSpan))
				{
					return TypeMap._Time;
				}
				if (dataType == typeof(char[]))
				{
					return TypeMap._NVarChar;
				}
				throw ADP.UnknownDataType(dataType);
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return TypeMap._Bit;
			case TypeCode.Char:
			case TypeCode.String:
				return TypeMap._NVarChar;
			case TypeCode.SByte:
				return TypeMap._SmallInt;
			case TypeCode.Byte:
				return TypeMap._TinyInt;
			case TypeCode.Int16:
				return TypeMap._SmallInt;
			case TypeCode.UInt16:
				return TypeMap._Int;
			case TypeCode.Int32:
				return TypeMap._Int;
			case TypeCode.UInt32:
				return TypeMap._BigInt;
			case TypeCode.Int64:
				return TypeMap._BigInt;
			case TypeCode.UInt64:
				return TypeMap._Numeric;
			case TypeCode.Single:
				return TypeMap._Real;
			case TypeCode.Double:
				return TypeMap._Double;
			case TypeCode.Decimal:
				return TypeMap._Numeric;
			case TypeCode.DateTime:
				return TypeMap._DateTime;
			}
			throw ADP.UnknownDataTypeCode(dataType, Type.GetTypeCode(dataType));
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x00109138 File Offset: 0x00108538
		internal static TypeMap FromSqlType(ODBC32.SQL_TYPE sqltype)
		{
			switch (sqltype)
			{
			case ODBC32.SQL_TYPE.SS_TIME_EX:
			case ODBC32.SQL_TYPE.SS_UTCDATETIME:
				throw ODBC.UnknownSQLType(sqltype);
			case ODBC32.SQL_TYPE.SS_XML:
				return TypeMap._XML;
			case ODBC32.SQL_TYPE.SS_UDT:
				return TypeMap._UDT;
			case ODBC32.SQL_TYPE.SS_VARIANT:
				return TypeMap._Variant;
			default:
				switch (sqltype)
				{
				case ODBC32.SQL_TYPE.GUID:
					return TypeMap._UniqueId;
				case ODBC32.SQL_TYPE.WLONGVARCHAR:
					return TypeMap._NText;
				case ODBC32.SQL_TYPE.WVARCHAR:
					return TypeMap._NVarChar;
				case ODBC32.SQL_TYPE.WCHAR:
					return TypeMap._NChar;
				case ODBC32.SQL_TYPE.BIT:
					return TypeMap._Bit;
				case ODBC32.SQL_TYPE.TINYINT:
					return TypeMap._TinyInt;
				case ODBC32.SQL_TYPE.BIGINT:
					return TypeMap._BigInt;
				case ODBC32.SQL_TYPE.LONGVARBINARY:
					return TypeMap._Image;
				case ODBC32.SQL_TYPE.VARBINARY:
					return TypeMap._VarBinary;
				case ODBC32.SQL_TYPE.BINARY:
					return TypeMap._Binary;
				case ODBC32.SQL_TYPE.LONGVARCHAR:
					return TypeMap._Text;
				case (ODBC32.SQL_TYPE)0:
				case (ODBC32.SQL_TYPE)9:
				case (ODBC32.SQL_TYPE)10:
					goto IL_146;
				case ODBC32.SQL_TYPE.CHAR:
					return TypeMap._Char;
				case ODBC32.SQL_TYPE.NUMERIC:
					return TypeMap._Numeric;
				case ODBC32.SQL_TYPE.DECIMAL:
					return TypeMap._Decimal;
				case ODBC32.SQL_TYPE.INTEGER:
					return TypeMap._Int;
				case ODBC32.SQL_TYPE.SMALLINT:
					return TypeMap._SmallInt;
				case ODBC32.SQL_TYPE.FLOAT:
					return TypeMap._Double;
				case ODBC32.SQL_TYPE.REAL:
					return TypeMap._Real;
				case ODBC32.SQL_TYPE.DOUBLE:
					return TypeMap._Double;
				case ODBC32.SQL_TYPE.TIMESTAMP:
					break;
				case ODBC32.SQL_TYPE.VARCHAR:
					return TypeMap._VarChar;
				default:
					switch (sqltype)
					{
					case ODBC32.SQL_TYPE.TYPE_DATE:
						return TypeMap._Date;
					case ODBC32.SQL_TYPE.TYPE_TIME:
						return TypeMap._Time;
					case ODBC32.SQL_TYPE.TYPE_TIMESTAMP:
						break;
					default:
						goto IL_146;
					}
					break;
				}
				return TypeMap._DateTime;
				IL_146:
				throw ODBC.UnknownSQLType(sqltype);
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x00109294 File Offset: 0x00108694
		internal static TypeMap UpgradeSignedType(TypeMap typeMap, bool unsigned)
		{
			if (unsigned)
			{
				switch (typeMap._dbType)
				{
				case DbType.Int16:
					return TypeMap._Int;
				case DbType.Int32:
					return TypeMap._BigInt;
				case DbType.Int64:
					return TypeMap._Decimal;
				default:
					return typeMap;
				}
			}
			else
			{
				DbType dbType = typeMap._dbType;
				if (dbType == DbType.Byte)
				{
					return TypeMap._SmallInt;
				}
				return typeMap;
			}
		}

		// Token: 0x04001A21 RID: 6689
		private static readonly TypeMap _BigInt = new TypeMap(OdbcType.BigInt, DbType.Int64, typeof(long), ODBC32.SQL_TYPE.BIGINT, ODBC32.SQL_C.SBIGINT, ODBC32.SQL_C.SBIGINT, 8, 20, true);

		// Token: 0x04001A22 RID: 6690
		private static readonly TypeMap _Binary = new TypeMap(OdbcType.Binary, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.BINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001A23 RID: 6691
		private static readonly TypeMap _Bit = new TypeMap(OdbcType.Bit, DbType.Boolean, typeof(bool), ODBC32.SQL_TYPE.BIT, ODBC32.SQL_C.BIT, ODBC32.SQL_C.BIT, 1, 1, false);

		// Token: 0x04001A24 RID: 6692
		internal static readonly TypeMap _Char = new TypeMap(OdbcType.Char, DbType.AnsiStringFixedLength, typeof(string), ODBC32.SQL_TYPE.CHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x04001A25 RID: 6693
		private static readonly TypeMap _DateTime = new TypeMap(OdbcType.DateTime, DbType.DateTime, typeof(DateTime), ODBC32.SQL_TYPE.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, 16, 23, false);

		// Token: 0x04001A26 RID: 6694
		private static readonly TypeMap _Date = new TypeMap(OdbcType.Date, DbType.Date, typeof(DateTime), ODBC32.SQL_TYPE.TYPE_DATE, ODBC32.SQL_C.TYPE_DATE, ODBC32.SQL_C.TYPE_DATE, 6, 10, false);

		// Token: 0x04001A27 RID: 6695
		private static readonly TypeMap _Time = new TypeMap(OdbcType.Time, DbType.Time, typeof(TimeSpan), ODBC32.SQL_TYPE.TYPE_TIME, ODBC32.SQL_C.TYPE_TIME, ODBC32.SQL_C.TYPE_TIME, 6, 12, false);

		// Token: 0x04001A28 RID: 6696
		private static readonly TypeMap _Decimal = new TypeMap(OdbcType.Decimal, DbType.Decimal, typeof(decimal), ODBC32.SQL_TYPE.DECIMAL, ODBC32.SQL_C.NUMERIC, ODBC32.SQL_C.NUMERIC, 19, 28, false);

		// Token: 0x04001A29 RID: 6697
		private static readonly TypeMap _Double = new TypeMap(OdbcType.Double, DbType.Double, typeof(double), ODBC32.SQL_TYPE.DOUBLE, ODBC32.SQL_C.DOUBLE, ODBC32.SQL_C.DOUBLE, 8, 15, false);

		// Token: 0x04001A2A RID: 6698
		internal static readonly TypeMap _Image = new TypeMap(OdbcType.Image, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.LONGVARBINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001A2B RID: 6699
		private static readonly TypeMap _Int = new TypeMap(OdbcType.Int, DbType.Int32, typeof(int), ODBC32.SQL_TYPE.INTEGER, ODBC32.SQL_C.SLONG, ODBC32.SQL_C.SLONG, 4, 10, true);

		// Token: 0x04001A2C RID: 6700
		private static readonly TypeMap _NChar = new TypeMap(OdbcType.NChar, DbType.StringFixedLength, typeof(string), ODBC32.SQL_TYPE.WCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.WCHAR, -1, -1, false);

		// Token: 0x04001A2D RID: 6701
		internal static readonly TypeMap _NText = new TypeMap(OdbcType.NText, DbType.String, typeof(string), ODBC32.SQL_TYPE.WLONGVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.WCHAR, -1, -1, false);

		// Token: 0x04001A2E RID: 6702
		private static readonly TypeMap _Numeric = new TypeMap(OdbcType.Numeric, DbType.Decimal, typeof(decimal), ODBC32.SQL_TYPE.NUMERIC, ODBC32.SQL_C.NUMERIC, ODBC32.SQL_C.NUMERIC, 19, 28, false);

		// Token: 0x04001A2F RID: 6703
		internal static readonly TypeMap _NVarChar = new TypeMap(OdbcType.NVarChar, DbType.String, typeof(string), ODBC32.SQL_TYPE.WVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.WCHAR, -1, -1, false);

		// Token: 0x04001A30 RID: 6704
		private static readonly TypeMap _Real = new TypeMap(OdbcType.Real, DbType.Single, typeof(float), ODBC32.SQL_TYPE.REAL, ODBC32.SQL_C.REAL, ODBC32.SQL_C.REAL, 4, 7, false);

		// Token: 0x04001A31 RID: 6705
		private static readonly TypeMap _UniqueId = new TypeMap(OdbcType.UniqueIdentifier, DbType.Guid, typeof(Guid), ODBC32.SQL_TYPE.GUID, ODBC32.SQL_C.GUID, ODBC32.SQL_C.GUID, 16, 36, false);

		// Token: 0x04001A32 RID: 6706
		private static readonly TypeMap _SmallDT = new TypeMap(OdbcType.SmallDateTime, DbType.DateTime, typeof(DateTime), ODBC32.SQL_TYPE.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, ODBC32.SQL_C.TYPE_TIMESTAMP, 16, 23, false);

		// Token: 0x04001A33 RID: 6707
		private static readonly TypeMap _SmallInt = new TypeMap(OdbcType.SmallInt, DbType.Int16, typeof(short), ODBC32.SQL_TYPE.SMALLINT, ODBC32.SQL_C.SSHORT, ODBC32.SQL_C.SSHORT, 2, 5, true);

		// Token: 0x04001A34 RID: 6708
		internal static readonly TypeMap _Text = new TypeMap(OdbcType.Text, DbType.AnsiString, typeof(string), ODBC32.SQL_TYPE.LONGVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x04001A35 RID: 6709
		private static readonly TypeMap _Timestamp = new TypeMap(OdbcType.Timestamp, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.BINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001A36 RID: 6710
		private static readonly TypeMap _TinyInt = new TypeMap(OdbcType.TinyInt, DbType.Byte, typeof(byte), ODBC32.SQL_TYPE.TINYINT, ODBC32.SQL_C.UTINYINT, ODBC32.SQL_C.UTINYINT, 1, 3, true);

		// Token: 0x04001A37 RID: 6711
		private static readonly TypeMap _VarBinary = new TypeMap(OdbcType.VarBinary, DbType.Binary, typeof(byte[]), ODBC32.SQL_TYPE.VARBINARY, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001A38 RID: 6712
		internal static readonly TypeMap _VarChar = new TypeMap(OdbcType.VarChar, DbType.AnsiString, typeof(string), ODBC32.SQL_TYPE.VARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x04001A39 RID: 6713
		private static readonly TypeMap _Variant = new TypeMap(OdbcType.Binary, DbType.Binary, typeof(object), ODBC32.SQL_TYPE.SS_VARIANT, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001A3A RID: 6714
		private static readonly TypeMap _UDT = new TypeMap(OdbcType.Binary, DbType.Binary, typeof(object), ODBC32.SQL_TYPE.SS_UDT, ODBC32.SQL_C.BINARY, ODBC32.SQL_C.BINARY, -1, -1, false);

		// Token: 0x04001A3B RID: 6715
		private static readonly TypeMap _XML = new TypeMap(OdbcType.Text, DbType.AnsiString, typeof(string), ODBC32.SQL_TYPE.LONGVARCHAR, ODBC32.SQL_C.WCHAR, ODBC32.SQL_C.CHAR, -1, -1, false);

		// Token: 0x04001A3C RID: 6716
		internal readonly OdbcType _odbcType;

		// Token: 0x04001A3D RID: 6717
		internal readonly DbType _dbType;

		// Token: 0x04001A3E RID: 6718
		internal readonly Type _type;

		// Token: 0x04001A3F RID: 6719
		internal readonly ODBC32.SQL_TYPE _sql_type;

		// Token: 0x04001A40 RID: 6720
		internal readonly ODBC32.SQL_C _sql_c;

		// Token: 0x04001A41 RID: 6721
		internal readonly ODBC32.SQL_C _param_sql_c;

		// Token: 0x04001A42 RID: 6722
		internal readonly int _bufferSize;

		// Token: 0x04001A43 RID: 6723
		internal readonly int _columnSize;

		// Token: 0x04001A44 RID: 6724
		internal readonly bool _signType;
	}
}
