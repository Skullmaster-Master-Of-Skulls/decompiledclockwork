using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	// Token: 0x0200027E RID: 638
	internal sealed class NativeDBType
	{
		// Token: 0x060026A2 RID: 9890 RVA: 0x00105674 File Offset: 0x00104A74
		internal static bool HasHighBit(short value)
		{
			return (-4096 & value) != 0;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0010568C File Offset: 0x00104A8C
		private NativeDBType(byte maxpre, int fixlen, bool isfixed, bool islong, OleDbType enumOleDbType, short dbType, string dbstring, Type dataType, short wType, DbType enumDbType)
		{
			this.enumOleDbType = enumOleDbType;
			this.dbType = dbType;
			this.dbPart = ((-1 == fixlen) ? 7 : 5);
			this.isfixed = isfixed;
			this.islong = islong;
			this.maxpre = maxpre;
			this.fixlen = fixlen;
			this.wType = wType;
			this.dataSourceType = dbstring;
			this.dbString = new StringMemHandle(dbstring);
			this.dataType = dataType;
			this.enumDbType = enumDbType;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x00105708 File Offset: 0x00104B08
		internal bool IsVariableLength
		{
			get
			{
				return -1 == this.fixlen;
			}
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00105720 File Offset: 0x00104B20
		internal static NativeDBType FromDataType(OleDbType enumOleDbType)
		{
			if (enumOleDbType <= OleDbType.Filetime)
			{
				switch (enumOleDbType)
				{
				case OleDbType.Empty:
					return NativeDBType.D_Empty;
				case (OleDbType)1:
				case (OleDbType)15:
					break;
				case OleDbType.SmallInt:
					return NativeDBType.D_SmallInt;
				case OleDbType.Integer:
					return NativeDBType.D_Integer;
				case OleDbType.Single:
					return NativeDBType.D_Single;
				case OleDbType.Double:
					return NativeDBType.D_Double;
				case OleDbType.Currency:
					return NativeDBType.D_Currency;
				case OleDbType.Date:
					return NativeDBType.D_Date;
				case OleDbType.BSTR:
					return NativeDBType.D_BSTR;
				case OleDbType.IDispatch:
					return NativeDBType.D_IDispatch;
				case OleDbType.Error:
					return NativeDBType.D_Error;
				case OleDbType.Boolean:
					return NativeDBType.D_Boolean;
				case OleDbType.Variant:
					return NativeDBType.D_Variant;
				case OleDbType.IUnknown:
					return NativeDBType.D_IUnknown;
				case OleDbType.Decimal:
					return NativeDBType.D_Decimal;
				case OleDbType.TinyInt:
					return NativeDBType.D_TinyInt;
				case OleDbType.UnsignedTinyInt:
					return NativeDBType.D_UnsignedTinyInt;
				case OleDbType.UnsignedSmallInt:
					return NativeDBType.D_UnsignedSmallInt;
				case OleDbType.UnsignedInt:
					return NativeDBType.D_UnsignedInt;
				case OleDbType.BigInt:
					return NativeDBType.D_BigInt;
				case OleDbType.UnsignedBigInt:
					return NativeDBType.D_UnsignedBigInt;
				default:
					if (enumOleDbType == OleDbType.Filetime)
					{
						return NativeDBType.D_Filetime;
					}
					break;
				}
			}
			else
			{
				if (enumOleDbType == OleDbType.Guid)
				{
					return NativeDBType.D_Guid;
				}
				switch (enumOleDbType)
				{
				case OleDbType.Binary:
					return NativeDBType.D_Binary;
				case OleDbType.Char:
					return NativeDBType.D_Char;
				case OleDbType.WChar:
					return NativeDBType.D_WChar;
				case OleDbType.Numeric:
					return NativeDBType.D_Numeric;
				case (OleDbType)132:
				case (OleDbType)136:
				case (OleDbType)137:
					break;
				case OleDbType.DBDate:
					return NativeDBType.D_DBDate;
				case OleDbType.DBTime:
					return NativeDBType.D_DBTime;
				case OleDbType.DBTimeStamp:
					return NativeDBType.D_DBTimeStamp;
				case OleDbType.PropVariant:
					return NativeDBType.D_PropVariant;
				case OleDbType.VarNumeric:
					return NativeDBType.D_VarNumeric;
				default:
					switch (enumOleDbType)
					{
					case OleDbType.VarChar:
						return NativeDBType.D_VarChar;
					case OleDbType.LongVarChar:
						return NativeDBType.D_LongVarChar;
					case OleDbType.VarWChar:
						return NativeDBType.D_VarWChar;
					case OleDbType.LongVarWChar:
						return NativeDBType.D_LongVarWChar;
					case OleDbType.VarBinary:
						return NativeDBType.D_VarBinary;
					case OleDbType.LongVarBinary:
						return NativeDBType.D_LongVarBinary;
					}
					break;
				}
			}
			throw ODB.InvalidOleDbType(enumOleDbType);
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x001058F0 File Offset: 0x00104CF0
		internal static NativeDBType FromSystemType(object value)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				switch (convertible.GetTypeCode())
				{
				case TypeCode.Empty:
					return NativeDBType.D_Empty;
				case TypeCode.Object:
					return NativeDBType.D_Variant;
				case TypeCode.DBNull:
					throw ADP.InvalidDataType(TypeCode.DBNull);
				case TypeCode.Boolean:
					return NativeDBType.D_Boolean;
				case TypeCode.Char:
					return NativeDBType.D_Char;
				case TypeCode.SByte:
					return NativeDBType.D_TinyInt;
				case TypeCode.Byte:
					return NativeDBType.D_UnsignedTinyInt;
				case TypeCode.Int16:
					return NativeDBType.D_SmallInt;
				case TypeCode.UInt16:
					return NativeDBType.D_UnsignedSmallInt;
				case TypeCode.Int32:
					return NativeDBType.D_Integer;
				case TypeCode.UInt32:
					return NativeDBType.D_UnsignedInt;
				case TypeCode.Int64:
					return NativeDBType.D_BigInt;
				case TypeCode.UInt64:
					return NativeDBType.D_UnsignedBigInt;
				case TypeCode.Single:
					return NativeDBType.D_Single;
				case TypeCode.Double:
					return NativeDBType.D_Double;
				case TypeCode.Decimal:
					return NativeDBType.D_Decimal;
				case TypeCode.DateTime:
					return NativeDBType.D_DBTimeStamp;
				case TypeCode.String:
					return NativeDBType.D_VarWChar;
				}
				throw ADP.UnknownDataTypeCode(value.GetType(), convertible.GetTypeCode());
			}
			if (value is byte[])
			{
				return NativeDBType.D_VarBinary;
			}
			if (value is Guid)
			{
				return NativeDBType.D_Guid;
			}
			if (value is TimeSpan)
			{
				return NativeDBType.D_DBTime;
			}
			return NativeDBType.D_Variant;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x00105A14 File Offset: 0x00104E14
		internal static NativeDBType FromDbType(DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				return NativeDBType.D_VarChar;
			case DbType.Binary:
				return NativeDBType.D_VarBinary;
			case DbType.Byte:
				return NativeDBType.D_UnsignedTinyInt;
			case DbType.Boolean:
				return NativeDBType.D_Boolean;
			case DbType.Currency:
				return NativeDBType.D_Currency;
			case DbType.Date:
				return NativeDBType.D_DBDate;
			case DbType.DateTime:
				return NativeDBType.D_DBTimeStamp;
			case DbType.Decimal:
				return NativeDBType.D_Decimal;
			case DbType.Double:
				return NativeDBType.D_Double;
			case DbType.Guid:
				return NativeDBType.D_Guid;
			case DbType.Int16:
				return NativeDBType.D_SmallInt;
			case DbType.Int32:
				return NativeDBType.D_Integer;
			case DbType.Int64:
				return NativeDBType.D_BigInt;
			case DbType.Object:
				return NativeDBType.D_Variant;
			case DbType.SByte:
				return NativeDBType.D_TinyInt;
			case DbType.Single:
				return NativeDBType.D_Single;
			case DbType.String:
				return NativeDBType.D_VarWChar;
			case DbType.Time:
				return NativeDBType.D_DBTime;
			case DbType.UInt16:
				return NativeDBType.D_UnsignedSmallInt;
			case DbType.UInt32:
				return NativeDBType.D_UnsignedInt;
			case DbType.UInt64:
				return NativeDBType.D_UnsignedBigInt;
			case DbType.VarNumeric:
				return NativeDBType.D_VarNumeric;
			case DbType.AnsiStringFixedLength:
				return NativeDBType.D_Char;
			case DbType.StringFixedLength:
				return NativeDBType.D_WChar;
			case DbType.Xml:
				return NativeDBType.D_Xml;
			}
			throw ADP.DbTypeNotSupported(dbType, typeof(OleDbType));
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00105B3C File Offset: 0x00104F3C
		internal static NativeDBType FromDBType(short dbType, bool isLong, bool isFixed)
		{
			if (dbType <= 64)
			{
				switch (dbType)
				{
				case 2:
					return NativeDBType.D_SmallInt;
				case 3:
					return NativeDBType.D_Integer;
				case 4:
					return NativeDBType.D_Single;
				case 5:
					return NativeDBType.D_Double;
				case 6:
					return NativeDBType.D_Currency;
				case 7:
					return NativeDBType.D_Date;
				case 8:
					return NativeDBType.D_BSTR;
				case 9:
					return NativeDBType.D_IDispatch;
				case 10:
					return NativeDBType.D_Error;
				case 11:
					return NativeDBType.D_Boolean;
				case 12:
					return NativeDBType.D_Variant;
				case 13:
					return NativeDBType.D_IUnknown;
				case 14:
					return NativeDBType.D_Decimal;
				case 15:
					break;
				case 16:
					return NativeDBType.D_TinyInt;
				case 17:
					return NativeDBType.D_UnsignedTinyInt;
				case 18:
					return NativeDBType.D_UnsignedSmallInt;
				case 19:
					return NativeDBType.D_UnsignedInt;
				case 20:
					return NativeDBType.D_BigInt;
				case 21:
					return NativeDBType.D_UnsignedBigInt;
				default:
					if (dbType == 64)
					{
						return NativeDBType.D_Filetime;
					}
					break;
				}
			}
			else
			{
				if (dbType == 72)
				{
					return NativeDBType.D_Guid;
				}
				switch (dbType)
				{
				case 128:
					if (isLong)
					{
						return NativeDBType.D_LongVarBinary;
					}
					if (!isFixed)
					{
						return NativeDBType.D_VarBinary;
					}
					return NativeDBType.D_Binary;
				case 129:
					if (isLong)
					{
						return NativeDBType.D_LongVarChar;
					}
					if (!isFixed)
					{
						return NativeDBType.D_VarChar;
					}
					return NativeDBType.D_Char;
				case 130:
					if (isLong)
					{
						return NativeDBType.D_LongVarWChar;
					}
					if (!isFixed)
					{
						return NativeDBType.D_VarWChar;
					}
					return NativeDBType.D_WChar;
				case 131:
					return NativeDBType.D_Numeric;
				case 132:
					return NativeDBType.D_Udt;
				case 133:
					return NativeDBType.D_DBDate;
				case 134:
					return NativeDBType.D_DBTime;
				case 135:
					return NativeDBType.D_DBTimeStamp;
				case 136:
					return NativeDBType.D_Chapter;
				case 138:
					return NativeDBType.D_PropVariant;
				case 139:
					return NativeDBType.D_VarNumeric;
				case 141:
					return NativeDBType.D_Xml;
				}
			}
			if ((4096 & dbType) != 0)
			{
				throw ODB.DBBindingGetVector();
			}
			return NativeDBType.D_Variant;
		}

		// Token: 0x04001863 RID: 6243
		internal const short EMPTY = 0;

		// Token: 0x04001864 RID: 6244
		internal const short NULL = 1;

		// Token: 0x04001865 RID: 6245
		internal const short I2 = 2;

		// Token: 0x04001866 RID: 6246
		internal const short I4 = 3;

		// Token: 0x04001867 RID: 6247
		internal const short R4 = 4;

		// Token: 0x04001868 RID: 6248
		internal const short R8 = 5;

		// Token: 0x04001869 RID: 6249
		internal const short CY = 6;

		// Token: 0x0400186A RID: 6250
		internal const short DATE = 7;

		// Token: 0x0400186B RID: 6251
		internal const short BSTR = 8;

		// Token: 0x0400186C RID: 6252
		internal const short IDISPATCH = 9;

		// Token: 0x0400186D RID: 6253
		internal const short ERROR = 10;

		// Token: 0x0400186E RID: 6254
		internal const short BOOL = 11;

		// Token: 0x0400186F RID: 6255
		internal const short VARIANT = 12;

		// Token: 0x04001870 RID: 6256
		internal const short IUNKNOWN = 13;

		// Token: 0x04001871 RID: 6257
		internal const short DECIMAL = 14;

		// Token: 0x04001872 RID: 6258
		internal const short I1 = 16;

		// Token: 0x04001873 RID: 6259
		internal const short UI1 = 17;

		// Token: 0x04001874 RID: 6260
		internal const short UI2 = 18;

		// Token: 0x04001875 RID: 6261
		internal const short UI4 = 19;

		// Token: 0x04001876 RID: 6262
		internal const short I8 = 20;

		// Token: 0x04001877 RID: 6263
		internal const short UI8 = 21;

		// Token: 0x04001878 RID: 6264
		internal const short FILETIME = 64;

		// Token: 0x04001879 RID: 6265
		internal const short DBUTCDATETIME = 65;

		// Token: 0x0400187A RID: 6266
		internal const short DBTIME_EX = 66;

		// Token: 0x0400187B RID: 6267
		internal const short GUID = 72;

		// Token: 0x0400187C RID: 6268
		internal const short BYTES = 128;

		// Token: 0x0400187D RID: 6269
		internal const short STR = 129;

		// Token: 0x0400187E RID: 6270
		internal const short WSTR = 130;

		// Token: 0x0400187F RID: 6271
		internal const short NUMERIC = 131;

		// Token: 0x04001880 RID: 6272
		internal const short UDT = 132;

		// Token: 0x04001881 RID: 6273
		internal const short DBDATE = 133;

		// Token: 0x04001882 RID: 6274
		internal const short DBTIME = 134;

		// Token: 0x04001883 RID: 6275
		internal const short DBTIMESTAMP = 135;

		// Token: 0x04001884 RID: 6276
		internal const short HCHAPTER = 136;

		// Token: 0x04001885 RID: 6277
		internal const short PROPVARIANT = 138;

		// Token: 0x04001886 RID: 6278
		internal const short VARNUMERIC = 139;

		// Token: 0x04001887 RID: 6279
		internal const short XML = 141;

		// Token: 0x04001888 RID: 6280
		internal const short VECTOR = 4096;

		// Token: 0x04001889 RID: 6281
		internal const short ARRAY = 8192;

		// Token: 0x0400188A RID: 6282
		internal const short BYREF = 16384;

		// Token: 0x0400188B RID: 6283
		internal const short RESERVED = -32768;

		// Token: 0x0400188C RID: 6284
		internal const short HighMask = -4096;

		// Token: 0x0400188D RID: 6285
		private const string S_BINARY = "DBTYPE_BINARY";

		// Token: 0x0400188E RID: 6286
		private const string S_BOOL = "DBTYPE_BOOL";

		// Token: 0x0400188F RID: 6287
		private const string S_BSTR = "DBTYPE_BSTR";

		// Token: 0x04001890 RID: 6288
		private const string S_CHAR = "DBTYPE_CHAR";

		// Token: 0x04001891 RID: 6289
		private const string S_CY = "DBTYPE_CY";

		// Token: 0x04001892 RID: 6290
		private const string S_DATE = "DBTYPE_DATE";

		// Token: 0x04001893 RID: 6291
		private const string S_DBDATE = "DBTYPE_DBDATE";

		// Token: 0x04001894 RID: 6292
		private const string S_DBTIME = "DBTYPE_DBTIME";

		// Token: 0x04001895 RID: 6293
		private const string S_DBTIMESTAMP = "DBTYPE_DBTIMESTAMP";

		// Token: 0x04001896 RID: 6294
		private const string S_DECIMAL = "DBTYPE_DECIMAL";

		// Token: 0x04001897 RID: 6295
		private const string S_ERROR = "DBTYPE_ERROR";

		// Token: 0x04001898 RID: 6296
		private const string S_FILETIME = "DBTYPE_FILETIME";

		// Token: 0x04001899 RID: 6297
		private const string S_GUID = "DBTYPE_GUID";

		// Token: 0x0400189A RID: 6298
		private const string S_I1 = "DBTYPE_I1";

		// Token: 0x0400189B RID: 6299
		private const string S_I2 = "DBTYPE_I2";

		// Token: 0x0400189C RID: 6300
		private const string S_I4 = "DBTYPE_I4";

		// Token: 0x0400189D RID: 6301
		private const string S_I8 = "DBTYPE_I8";

		// Token: 0x0400189E RID: 6302
		private const string S_IDISPATCH = "DBTYPE_IDISPATCH";

		// Token: 0x0400189F RID: 6303
		private const string S_IUNKNOWN = "DBTYPE_IUNKNOWN";

		// Token: 0x040018A0 RID: 6304
		private const string S_LONGVARBINARY = "DBTYPE_LONGVARBINARY";

		// Token: 0x040018A1 RID: 6305
		private const string S_LONGVARCHAR = "DBTYPE_LONGVARCHAR";

		// Token: 0x040018A2 RID: 6306
		private const string S_NUMERIC = "DBTYPE_NUMERIC";

		// Token: 0x040018A3 RID: 6307
		private const string S_PROPVARIANT = "DBTYPE_PROPVARIANT";

		// Token: 0x040018A4 RID: 6308
		private const string S_R4 = "DBTYPE_R4";

		// Token: 0x040018A5 RID: 6309
		private const string S_R8 = "DBTYPE_R8";

		// Token: 0x040018A6 RID: 6310
		private const string S_UDT = "DBTYPE_UDT";

		// Token: 0x040018A7 RID: 6311
		private const string S_UI1 = "DBTYPE_UI1";

		// Token: 0x040018A8 RID: 6312
		private const string S_UI2 = "DBTYPE_UI2";

		// Token: 0x040018A9 RID: 6313
		private const string S_UI4 = "DBTYPE_UI4";

		// Token: 0x040018AA RID: 6314
		private const string S_UI8 = "DBTYPE_UI8";

		// Token: 0x040018AB RID: 6315
		private const string S_VARBINARY = "DBTYPE_VARBINARY";

		// Token: 0x040018AC RID: 6316
		private const string S_VARCHAR = "DBTYPE_VARCHAR";

		// Token: 0x040018AD RID: 6317
		private const string S_VARIANT = "DBTYPE_VARIANT";

		// Token: 0x040018AE RID: 6318
		private const string S_VARNUMERIC = "DBTYPE_VARNUMERIC";

		// Token: 0x040018AF RID: 6319
		private const string S_WCHAR = "DBTYPE_WCHAR";

		// Token: 0x040018B0 RID: 6320
		private const string S_WVARCHAR = "DBTYPE_WVARCHAR";

		// Token: 0x040018B1 RID: 6321
		private const string S_WLONGVARCHAR = "DBTYPE_WLONGVARCHAR";

		// Token: 0x040018B2 RID: 6322
		private const string S_XML = "DBTYPE_XML";

		// Token: 0x040018B3 RID: 6323
		private static readonly NativeDBType D_Binary = new NativeDBType(byte.MaxValue, -1, true, false, OleDbType.Binary, 128, "DBTYPE_BINARY", typeof(byte[]), 128, DbType.Binary);

		// Token: 0x040018B4 RID: 6324
		private static readonly NativeDBType D_Boolean = new NativeDBType(byte.MaxValue, 2, true, false, OleDbType.Boolean, 11, "DBTYPE_BOOL", typeof(bool), 11, DbType.Boolean);

		// Token: 0x040018B5 RID: 6325
		private static readonly NativeDBType D_BSTR = new NativeDBType(byte.MaxValue, ADP.PtrSize, false, false, OleDbType.BSTR, 8, "DBTYPE_BSTR", typeof(string), 8, DbType.String);

		// Token: 0x040018B6 RID: 6326
		private static readonly NativeDBType D_Char = new NativeDBType(byte.MaxValue, -1, true, false, OleDbType.Char, 129, "DBTYPE_CHAR", typeof(string), 130, DbType.AnsiStringFixedLength);

		// Token: 0x040018B7 RID: 6327
		private static readonly NativeDBType D_Currency = new NativeDBType(19, 8, true, false, OleDbType.Currency, 6, "DBTYPE_CY", typeof(decimal), 6, DbType.Currency);

		// Token: 0x040018B8 RID: 6328
		private static readonly NativeDBType D_Date = new NativeDBType(byte.MaxValue, 8, true, false, OleDbType.Date, 7, "DBTYPE_DATE", typeof(DateTime), 7, DbType.DateTime);

		// Token: 0x040018B9 RID: 6329
		private static readonly NativeDBType D_DBDate = new NativeDBType(byte.MaxValue, 6, true, false, OleDbType.DBDate, 133, "DBTYPE_DBDATE", typeof(DateTime), 133, DbType.Date);

		// Token: 0x040018BA RID: 6330
		private static readonly NativeDBType D_DBTime = new NativeDBType(byte.MaxValue, 6, true, false, OleDbType.DBTime, 134, "DBTYPE_DBTIME", typeof(TimeSpan), 134, DbType.Time);

		// Token: 0x040018BB RID: 6331
		private static readonly NativeDBType D_DBTimeStamp = new NativeDBType(byte.MaxValue, 16, true, false, OleDbType.DBTimeStamp, 135, "DBTYPE_DBTIMESTAMP", typeof(DateTime), 135, DbType.DateTime);

		// Token: 0x040018BC RID: 6332
		private static readonly NativeDBType D_Decimal = new NativeDBType(28, 16, true, false, OleDbType.Decimal, 14, "DBTYPE_DECIMAL", typeof(decimal), 14, DbType.Decimal);

		// Token: 0x040018BD RID: 6333
		private static readonly NativeDBType D_Error = new NativeDBType(byte.MaxValue, 4, true, false, OleDbType.Error, 10, "DBTYPE_ERROR", typeof(int), 10, DbType.Int32);

		// Token: 0x040018BE RID: 6334
		private static readonly NativeDBType D_Filetime = new NativeDBType(byte.MaxValue, 8, true, false, OleDbType.Filetime, 64, "DBTYPE_FILETIME", typeof(DateTime), 64, DbType.DateTime);

		// Token: 0x040018BF RID: 6335
		private static readonly NativeDBType D_Guid = new NativeDBType(byte.MaxValue, 16, true, false, OleDbType.Guid, 72, "DBTYPE_GUID", typeof(Guid), 72, DbType.Guid);

		// Token: 0x040018C0 RID: 6336
		private static readonly NativeDBType D_TinyInt = new NativeDBType(3, 1, true, false, OleDbType.TinyInt, 16, "DBTYPE_I1", typeof(short), 16, DbType.SByte);

		// Token: 0x040018C1 RID: 6337
		private static readonly NativeDBType D_SmallInt = new NativeDBType(5, 2, true, false, OleDbType.SmallInt, 2, "DBTYPE_I2", typeof(short), 2, DbType.Int16);

		// Token: 0x040018C2 RID: 6338
		private static readonly NativeDBType D_Integer = new NativeDBType(10, 4, true, false, OleDbType.Integer, 3, "DBTYPE_I4", typeof(int), 3, DbType.Int32);

		// Token: 0x040018C3 RID: 6339
		private static readonly NativeDBType D_BigInt = new NativeDBType(19, 8, true, false, OleDbType.BigInt, 20, "DBTYPE_I8", typeof(long), 20, DbType.Int64);

		// Token: 0x040018C4 RID: 6340
		private static readonly NativeDBType D_IDispatch = new NativeDBType(byte.MaxValue, ADP.PtrSize, true, false, OleDbType.IDispatch, 9, "DBTYPE_IDISPATCH", typeof(object), 9, DbType.Object);

		// Token: 0x040018C5 RID: 6341
		private static readonly NativeDBType D_IUnknown = new NativeDBType(byte.MaxValue, ADP.PtrSize, true, false, OleDbType.IUnknown, 13, "DBTYPE_IUNKNOWN", typeof(object), 13, DbType.Object);

		// Token: 0x040018C6 RID: 6342
		private static readonly NativeDBType D_LongVarBinary = new NativeDBType(byte.MaxValue, -1, false, true, OleDbType.LongVarBinary, 128, "DBTYPE_LONGVARBINARY", typeof(byte[]), 128, DbType.Binary);

		// Token: 0x040018C7 RID: 6343
		private static readonly NativeDBType D_LongVarChar = new NativeDBType(byte.MaxValue, -1, false, true, OleDbType.LongVarChar, 129, "DBTYPE_LONGVARCHAR", typeof(string), 130, DbType.AnsiString);

		// Token: 0x040018C8 RID: 6344
		private static readonly NativeDBType D_Numeric = new NativeDBType(28, 19, true, false, OleDbType.Numeric, 131, "DBTYPE_NUMERIC", typeof(decimal), 131, DbType.Decimal);

		// Token: 0x040018C9 RID: 6345
		private static readonly NativeDBType D_PropVariant = new NativeDBType(byte.MaxValue, NativeOledbWrapper.SizeOfPROPVARIANT, true, false, OleDbType.PropVariant, 138, "DBTYPE_PROPVARIANT", typeof(object), 12, DbType.Object);

		// Token: 0x040018CA RID: 6346
		private static readonly NativeDBType D_Single = new NativeDBType(7, 4, true, false, OleDbType.Single, 4, "DBTYPE_R4", typeof(float), 4, DbType.Single);

		// Token: 0x040018CB RID: 6347
		private static readonly NativeDBType D_Double = new NativeDBType(15, 8, true, false, OleDbType.Double, 5, "DBTYPE_R8", typeof(double), 5, DbType.Double);

		// Token: 0x040018CC RID: 6348
		private static readonly NativeDBType D_UnsignedTinyInt = new NativeDBType(3, 1, true, false, OleDbType.UnsignedTinyInt, 17, "DBTYPE_UI1", typeof(byte), 17, DbType.Byte);

		// Token: 0x040018CD RID: 6349
		private static readonly NativeDBType D_UnsignedSmallInt = new NativeDBType(5, 2, true, false, OleDbType.UnsignedSmallInt, 18, "DBTYPE_UI2", typeof(int), 18, DbType.UInt16);

		// Token: 0x040018CE RID: 6350
		private static readonly NativeDBType D_UnsignedInt = new NativeDBType(10, 4, true, false, OleDbType.UnsignedInt, 19, "DBTYPE_UI4", typeof(long), 19, DbType.UInt32);

		// Token: 0x040018CF RID: 6351
		private static readonly NativeDBType D_UnsignedBigInt = new NativeDBType(20, 8, true, false, OleDbType.UnsignedBigInt, 21, "DBTYPE_UI8", typeof(decimal), 21, DbType.UInt64);

		// Token: 0x040018D0 RID: 6352
		private static readonly NativeDBType D_VarBinary = new NativeDBType(byte.MaxValue, -1, false, false, OleDbType.VarBinary, 128, "DBTYPE_VARBINARY", typeof(byte[]), 128, DbType.Binary);

		// Token: 0x040018D1 RID: 6353
		private static readonly NativeDBType D_VarChar = new NativeDBType(byte.MaxValue, -1, false, false, OleDbType.VarChar, 129, "DBTYPE_VARCHAR", typeof(string), 130, DbType.AnsiString);

		// Token: 0x040018D2 RID: 6354
		private static readonly NativeDBType D_Variant = new NativeDBType(byte.MaxValue, ODB.SizeOf_Variant, true, false, OleDbType.Variant, 12, "DBTYPE_VARIANT", typeof(object), 12, DbType.Object);

		// Token: 0x040018D3 RID: 6355
		private static readonly NativeDBType D_VarNumeric = new NativeDBType(byte.MaxValue, 16, true, false, OleDbType.VarNumeric, 139, "DBTYPE_VARNUMERIC", typeof(decimal), 14, DbType.VarNumeric);

		// Token: 0x040018D4 RID: 6356
		private static readonly NativeDBType D_WChar = new NativeDBType(byte.MaxValue, -1, true, false, OleDbType.WChar, 130, "DBTYPE_WCHAR", typeof(string), 130, DbType.StringFixedLength);

		// Token: 0x040018D5 RID: 6357
		private static readonly NativeDBType D_VarWChar = new NativeDBType(byte.MaxValue, -1, false, false, OleDbType.VarWChar, 130, "DBTYPE_WVARCHAR", typeof(string), 130, DbType.String);

		// Token: 0x040018D6 RID: 6358
		private static readonly NativeDBType D_LongVarWChar = new NativeDBType(byte.MaxValue, -1, false, true, OleDbType.LongVarWChar, 130, "DBTYPE_WLONGVARCHAR", typeof(string), 130, DbType.String);

		// Token: 0x040018D7 RID: 6359
		private static readonly NativeDBType D_Chapter = new NativeDBType(byte.MaxValue, ADP.PtrSize, false, false, OleDbType.Empty, 136, "DBTYPE_UDT", typeof(IDataReader), 136, DbType.Object);

		// Token: 0x040018D8 RID: 6360
		private static readonly NativeDBType D_Empty = new NativeDBType(byte.MaxValue, 0, false, false, OleDbType.Empty, 0, "", null, 0, DbType.Object);

		// Token: 0x040018D9 RID: 6361
		private static readonly NativeDBType D_Xml = new NativeDBType(byte.MaxValue, -1, false, false, OleDbType.VarWChar, 141, "DBTYPE_XML", typeof(string), 130, DbType.String);

		// Token: 0x040018DA RID: 6362
		private static readonly NativeDBType D_Udt = new NativeDBType(byte.MaxValue, -1, false, false, OleDbType.VarBinary, 132, "DBTYPE_BINARY", typeof(byte[]), 128, DbType.Binary);

		// Token: 0x040018DB RID: 6363
		internal static readonly NativeDBType Default = NativeDBType.D_VarWChar;

		// Token: 0x040018DC RID: 6364
		internal static readonly byte MaximumDecimalPrecision = NativeDBType.D_Decimal.maxpre;

		// Token: 0x040018DD RID: 6365
		private const int FixedDbPart = 5;

		// Token: 0x040018DE RID: 6366
		private const int VarblDbPart = 7;

		// Token: 0x040018DF RID: 6367
		internal readonly OleDbType enumOleDbType;

		// Token: 0x040018E0 RID: 6368
		internal readonly DbType enumDbType;

		// Token: 0x040018E1 RID: 6369
		internal readonly short dbType;

		// Token: 0x040018E2 RID: 6370
		internal readonly short wType;

		// Token: 0x040018E3 RID: 6371
		internal readonly Type dataType;

		// Token: 0x040018E4 RID: 6372
		internal readonly int dbPart;

		// Token: 0x040018E5 RID: 6373
		internal readonly bool isfixed;

		// Token: 0x040018E6 RID: 6374
		internal readonly bool islong;

		// Token: 0x040018E7 RID: 6375
		internal readonly byte maxpre;

		// Token: 0x040018E8 RID: 6376
		internal readonly int fixlen;

		// Token: 0x040018E9 RID: 6377
		internal readonly string dataSourceType;

		// Token: 0x040018EA RID: 6378
		internal readonly StringMemHandle dbString;
	}
}
