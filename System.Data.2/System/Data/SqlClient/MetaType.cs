using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001CB RID: 459
	internal sealed class MetaType
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x000CC7B4 File Offset: 0x000CBBB4
		public MetaType(byte precision, byte scale, int fixedLength, bool isFixed, bool isLong, bool isPlp, byte tdsType, byte nullableTdsType, string typeName, Type classType, Type sqlType, SqlDbType sqldbType, DbType dbType, byte propBytes)
		{
			this.Precision = precision;
			this.Scale = scale;
			this.FixedLength = fixedLength;
			this.IsFixed = isFixed;
			this.IsLong = isLong;
			this.IsPlp = isPlp;
			this.TDSType = tdsType;
			this.NullableType = nullableTdsType;
			this.TypeName = typeName;
			this.SqlDbType = sqldbType;
			this.DbType = dbType;
			this.ClassType = classType;
			this.SqlType = sqlType;
			this.PropBytes = propBytes;
			this.IsAnsiType = MetaType._IsAnsiType(sqldbType);
			this.IsBinType = MetaType._IsBinType(sqldbType);
			this.IsCharType = MetaType._IsCharType(sqldbType);
			this.IsNCharType = MetaType._IsNCharType(sqldbType);
			this.IsSizeInCharacters = MetaType._IsSizeInCharacters(sqldbType);
			this.IsNewKatmaiType = MetaType._IsNewKatmaiType(sqldbType);
			this.IsVarTime = MetaType._IsVarTime(sqldbType);
			this.Is70Supported = MetaType._Is70Supported(this.SqlDbType);
			this.Is80Supported = MetaType._Is80Supported(this.SqlDbType);
			this.Is90Supported = MetaType._Is90Supported(this.SqlDbType);
			this.Is100Supported = MetaType._Is100Supported(this.SqlDbType);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x000CC8D4 File Offset: 0x000CBCD4
		public int TypeId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000CC8E4 File Offset: 0x000CBCE4
		private static bool _IsAnsiType(SqlDbType type)
		{
			return type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000CC904 File Offset: 0x000CBD04
		private static bool _IsSizeInCharacters(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.Xml || type == SqlDbType.NText;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000CC928 File Offset: 0x000CBD28
		private static bool _IsCharType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText || type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text || type == SqlDbType.Xml;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000CC95C File Offset: 0x000CBD5C
		private static bool _IsNCharType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText || type == SqlDbType.Xml;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000CC980 File Offset: 0x000CBD80
		private static bool _IsBinType(SqlDbType type)
		{
			return type == SqlDbType.Image || type == SqlDbType.Binary || type == SqlDbType.VarBinary || type == SqlDbType.Timestamp || type == SqlDbType.Udt || type == (SqlDbType)24;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000CC9AC File Offset: 0x000CBDAC
		private static bool _Is70Supported(SqlDbType type)
		{
			return type != SqlDbType.BigInt && type > SqlDbType.BigInt && type <= SqlDbType.VarChar;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000CC9CC File Offset: 0x000CBDCC
		private static bool _Is80Supported(SqlDbType type)
		{
			return type >= SqlDbType.BigInt && type <= SqlDbType.Variant;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000CC9E8 File Offset: 0x000CBDE8
		private static bool _Is90Supported(SqlDbType type)
		{
			return MetaType._Is80Supported(type) || SqlDbType.Xml == type || SqlDbType.Udt == type;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000CCA0C File Offset: 0x000CBE0C
		private static bool _Is100Supported(SqlDbType type)
		{
			return MetaType._Is90Supported(type) || SqlDbType.Date == type || SqlDbType.Time == type || SqlDbType.DateTime2 == type || SqlDbType.DateTimeOffset == type;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000CCA38 File Offset: 0x000CBE38
		private static bool _IsNewKatmaiType(SqlDbType type)
		{
			return SqlDbType.Structured == type;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000CCA4C File Offset: 0x000CBE4C
		internal static bool _IsVarTime(SqlDbType type)
		{
			return type == SqlDbType.Time || type == SqlDbType.DateTime2 || type == SqlDbType.DateTimeOffset;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000CCA6C File Offset: 0x000CBE6C
		internal static MetaType GetMetaTypeFromSqlDbType(SqlDbType target, bool isMultiValued)
		{
			switch (target)
			{
			case SqlDbType.BigInt:
				return MetaType.MetaBigInt;
			case SqlDbType.Binary:
				return MetaType.MetaBinary;
			case SqlDbType.Bit:
				return MetaType.MetaBit;
			case SqlDbType.Char:
				return MetaType.MetaChar;
			case SqlDbType.DateTime:
				return MetaType.MetaDateTime;
			case SqlDbType.Decimal:
				return MetaType.MetaDecimal;
			case SqlDbType.Float:
				return MetaType.MetaFloat;
			case SqlDbType.Image:
				return MetaType.MetaImage;
			case SqlDbType.Int:
				return MetaType.MetaInt;
			case SqlDbType.Money:
				return MetaType.MetaMoney;
			case SqlDbType.NChar:
				return MetaType.MetaNChar;
			case SqlDbType.NText:
				return MetaType.MetaNText;
			case SqlDbType.NVarChar:
				return MetaType.MetaNVarChar;
			case SqlDbType.Real:
				return MetaType.MetaReal;
			case SqlDbType.UniqueIdentifier:
				return MetaType.MetaUniqueId;
			case SqlDbType.SmallDateTime:
				return MetaType.MetaSmallDateTime;
			case SqlDbType.SmallInt:
				return MetaType.MetaSmallInt;
			case SqlDbType.SmallMoney:
				return MetaType.MetaSmallMoney;
			case SqlDbType.Text:
				return MetaType.MetaText;
			case SqlDbType.Timestamp:
				return MetaType.MetaTimestamp;
			case SqlDbType.TinyInt:
				return MetaType.MetaTinyInt;
			case SqlDbType.VarBinary:
				return MetaType.MetaVarBinary;
			case SqlDbType.VarChar:
				return MetaType.MetaVarChar;
			case SqlDbType.Variant:
				return MetaType.MetaVariant;
			case (SqlDbType)24:
				return MetaType.MetaSmallVarBinary;
			case SqlDbType.Xml:
				return MetaType.MetaXml;
			case SqlDbType.Udt:
				return MetaType.MetaUdt;
			case SqlDbType.Structured:
				if (isMultiValued)
				{
					return MetaType.MetaTable;
				}
				return MetaType.MetaSUDT;
			case SqlDbType.Date:
				return MetaType.MetaDate;
			case SqlDbType.Time:
				return MetaType.MetaTime;
			case SqlDbType.DateTime2:
				return MetaType.MetaDateTime2;
			case SqlDbType.DateTimeOffset:
				return MetaType.MetaDateTimeOffset;
			}
			throw SQL.InvalidSqlDbType(target);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000CCBE0 File Offset: 0x000CBFE0
		internal static MetaType GetMetaTypeFromDbType(DbType target)
		{
			switch (target)
			{
			case DbType.AnsiString:
				return MetaType.MetaVarChar;
			case DbType.Binary:
				return MetaType.MetaVarBinary;
			case DbType.Byte:
				return MetaType.MetaTinyInt;
			case DbType.Boolean:
				return MetaType.MetaBit;
			case DbType.Currency:
				return MetaType.MetaMoney;
			case DbType.Date:
			case DbType.DateTime:
				return MetaType.MetaDateTime;
			case DbType.Decimal:
				return MetaType.MetaDecimal;
			case DbType.Double:
				return MetaType.MetaFloat;
			case DbType.Guid:
				return MetaType.MetaUniqueId;
			case DbType.Int16:
				return MetaType.MetaSmallInt;
			case DbType.Int32:
				return MetaType.MetaInt;
			case DbType.Int64:
				return MetaType.MetaBigInt;
			case DbType.Object:
				return MetaType.MetaVariant;
			case DbType.Single:
				return MetaType.MetaReal;
			case DbType.String:
				return MetaType.MetaNVarChar;
			case DbType.Time:
				return MetaType.MetaDateTime;
			case DbType.AnsiStringFixedLength:
				return MetaType.MetaChar;
			case DbType.StringFixedLength:
				return MetaType.MetaNChar;
			case DbType.Xml:
				return MetaType.MetaXml;
			case DbType.DateTime2:
				return MetaType.MetaDateTime2;
			case DbType.DateTimeOffset:
				return MetaType.MetaDateTimeOffset;
			}
			throw ADP.DbTypeNotSupported(target, typeof(SqlDbType));
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000CCCF8 File Offset: 0x000CC0F8
		internal static MetaType GetMaxMetaTypeFromMetaType(MetaType mt)
		{
			SqlDbType sqlDbType = mt.SqlDbType;
			if (sqlDbType <= SqlDbType.NChar)
			{
				if (sqlDbType != SqlDbType.Binary)
				{
					if (sqlDbType == SqlDbType.Char)
					{
						goto IL_3E;
					}
					if (sqlDbType != SqlDbType.NChar)
					{
						return mt;
					}
					goto IL_44;
				}
			}
			else if (sqlDbType <= SqlDbType.VarBinary)
			{
				if (sqlDbType == SqlDbType.NVarChar)
				{
					goto IL_44;
				}
				if (sqlDbType != SqlDbType.VarBinary)
				{
					return mt;
				}
			}
			else
			{
				if (sqlDbType == SqlDbType.VarChar)
				{
					goto IL_3E;
				}
				if (sqlDbType != SqlDbType.Udt)
				{
					return mt;
				}
				return MetaType.MetaMaxUdt;
			}
			return MetaType.MetaMaxVarBinary;
			IL_3E:
			return MetaType.MetaMaxVarChar;
			IL_44:
			return MetaType.MetaMaxNVarChar;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000CCD58 File Offset: 0x000CC158
		internal static MetaType GetMetaTypeFromType(Type dataType)
		{
			return MetaType.GetMetaTypeFromValue(dataType, null, false, true);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000CCD70 File Offset: 0x000CC170
		internal static MetaType GetMetaTypeFromValue(object value, bool streamAllowed = true)
		{
			return MetaType.GetMetaTypeFromValue(value.GetType(), value, true, streamAllowed);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000CCD8C File Offset: 0x000CC18C
		private static MetaType GetMetaTypeFromValue(Type dataType, object value, bool inferLen, bool streamAllowed)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (dataType == typeof(byte[]))
				{
					if (!inferLen || ((byte[])value).Length <= 8000)
					{
						return MetaType.MetaVarBinary;
					}
					return MetaType.MetaImage;
				}
				else
				{
					if (dataType == typeof(Guid))
					{
						return MetaType.MetaUniqueId;
					}
					if (dataType == typeof(object))
					{
						return MetaType.MetaVariant;
					}
					if (dataType == typeof(SqlBinary))
					{
						return MetaType.MetaVarBinary;
					}
					if (dataType == typeof(SqlBoolean))
					{
						return MetaType.MetaBit;
					}
					if (dataType == typeof(SqlByte))
					{
						return MetaType.MetaTinyInt;
					}
					if (dataType == typeof(SqlBytes))
					{
						return MetaType.MetaVarBinary;
					}
					if (dataType == typeof(SqlChars))
					{
						return MetaType.MetaNVarChar;
					}
					if (dataType == typeof(SqlDateTime))
					{
						return MetaType.MetaDateTime;
					}
					if (dataType == typeof(SqlDouble))
					{
						return MetaType.MetaFloat;
					}
					if (dataType == typeof(SqlGuid))
					{
						return MetaType.MetaUniqueId;
					}
					if (dataType == typeof(SqlInt16))
					{
						return MetaType.MetaSmallInt;
					}
					if (dataType == typeof(SqlInt32))
					{
						return MetaType.MetaInt;
					}
					if (dataType == typeof(SqlInt64))
					{
						return MetaType.MetaBigInt;
					}
					if (dataType == typeof(SqlMoney))
					{
						return MetaType.MetaMoney;
					}
					if (dataType == typeof(SqlDecimal))
					{
						return MetaType.MetaDecimal;
					}
					if (dataType == typeof(SqlSingle))
					{
						return MetaType.MetaReal;
					}
					if (dataType == typeof(SqlXml))
					{
						return MetaType.MetaXml;
					}
					if (dataType == typeof(SqlString))
					{
						if (!inferLen || ((SqlString)value).IsNull)
						{
							return MetaType.MetaNVarChar;
						}
						return MetaType.PromoteStringType(((SqlString)value).Value);
					}
					else
					{
						if (dataType == typeof(IEnumerable<DbDataRecord>) || dataType == typeof(DataTable))
						{
							return MetaType.MetaTable;
						}
						if (dataType == typeof(TimeSpan))
						{
							return MetaType.MetaTime;
						}
						if (dataType == typeof(DateTimeOffset))
						{
							return MetaType.MetaDateTimeOffset;
						}
						SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(dataType);
						if (sqlUdtInfo != null)
						{
							return MetaType.MetaUdt;
						}
						if (streamAllowed)
						{
							if (typeof(Stream).IsAssignableFrom(dataType))
							{
								return MetaType.MetaVarBinary;
							}
							if (typeof(TextReader).IsAssignableFrom(dataType))
							{
								return MetaType.MetaNVarChar;
							}
							if (typeof(XmlReader).IsAssignableFrom(dataType))
							{
								return MetaType.MetaXml;
							}
						}
						throw ADP.UnknownDataType(dataType);
					}
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return MetaType.MetaBit;
			case TypeCode.Char:
				throw ADP.InvalidDataType(TypeCode.Char);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return MetaType.MetaTinyInt;
			case TypeCode.Int16:
				return MetaType.MetaSmallInt;
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return MetaType.MetaInt;
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return MetaType.MetaBigInt;
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return MetaType.MetaReal;
			case TypeCode.Double:
				return MetaType.MetaFloat;
			case TypeCode.Decimal:
				return MetaType.MetaDecimal;
			case TypeCode.DateTime:
				return MetaType.MetaDateTime;
			case TypeCode.String:
				if (!inferLen)
				{
					return MetaType.MetaNVarChar;
				}
				return MetaType.PromoteStringType((string)value);
			}
			throw ADP.UnknownDataTypeCode(dataType, Type.GetTypeCode(dataType));
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000CD144 File Offset: 0x000CC544
		internal static object GetNullSqlValue(Type sqlType)
		{
			if (sqlType == typeof(SqlSingle))
			{
				return SqlSingle.Null;
			}
			if (sqlType == typeof(SqlString))
			{
				return SqlString.Null;
			}
			if (sqlType == typeof(SqlDouble))
			{
				return SqlDouble.Null;
			}
			if (sqlType == typeof(SqlBinary))
			{
				return SqlBinary.Null;
			}
			if (sqlType == typeof(SqlGuid))
			{
				return SqlGuid.Null;
			}
			if (sqlType == typeof(SqlBoolean))
			{
				return SqlBoolean.Null;
			}
			if (sqlType == typeof(SqlByte))
			{
				return SqlByte.Null;
			}
			if (sqlType == typeof(SqlInt16))
			{
				return SqlInt16.Null;
			}
			if (sqlType == typeof(SqlInt32))
			{
				return SqlInt32.Null;
			}
			if (sqlType == typeof(SqlInt64))
			{
				return SqlInt64.Null;
			}
			if (sqlType == typeof(SqlDecimal))
			{
				return SqlDecimal.Null;
			}
			if (sqlType == typeof(SqlDateTime))
			{
				return SqlDateTime.Null;
			}
			if (sqlType == typeof(SqlMoney))
			{
				return SqlMoney.Null;
			}
			if (sqlType == typeof(SqlXml))
			{
				return SqlXml.Null;
			}
			if (sqlType == typeof(object))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(IEnumerable<DbDataRecord>))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(DataTable))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(DateTime))
			{
				return DBNull.Value;
			}
			if (sqlType == typeof(TimeSpan))
			{
				return DBNull.Value;
			}
			sqlType == typeof(DateTimeOffset);
			return DBNull.Value;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000CD370 File Offset: 0x000CC770
		internal static MetaType PromoteStringType(string s)
		{
			int length = s.Length;
			if (length << 1 > 8000)
			{
				return MetaType.MetaVarChar;
			}
			return MetaType.MetaNVarChar;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000CD39C File Offset: 0x000CC79C
		internal static object GetComValueFromSqlVariant(object sqlVal)
		{
			object result = null;
			if (ADP.IsNull(sqlVal))
			{
				return result;
			}
			if (sqlVal is SqlSingle)
			{
				result = ((SqlSingle)sqlVal).Value;
			}
			else if (sqlVal is SqlString)
			{
				result = ((SqlString)sqlVal).Value;
			}
			else if (sqlVal is SqlDouble)
			{
				result = ((SqlDouble)sqlVal).Value;
			}
			else if (sqlVal is SqlBinary)
			{
				result = ((SqlBinary)sqlVal).Value;
			}
			else if (sqlVal is SqlGuid)
			{
				result = ((SqlGuid)sqlVal).Value;
			}
			else if (sqlVal is SqlBoolean)
			{
				result = ((SqlBoolean)sqlVal).Value;
			}
			else if (sqlVal is SqlByte)
			{
				result = ((SqlByte)sqlVal).Value;
			}
			else if (sqlVal is SqlInt16)
			{
				result = ((SqlInt16)sqlVal).Value;
			}
			else if (sqlVal is SqlInt32)
			{
				result = ((SqlInt32)sqlVal).Value;
			}
			else if (sqlVal is SqlInt64)
			{
				result = ((SqlInt64)sqlVal).Value;
			}
			else if (sqlVal is SqlDecimal)
			{
				result = ((SqlDecimal)sqlVal).Value;
			}
			else if (sqlVal is SqlDateTime)
			{
				result = ((SqlDateTime)sqlVal).Value;
			}
			else if (sqlVal is SqlMoney)
			{
				result = ((SqlMoney)sqlVal).Value;
			}
			else if (sqlVal is SqlXml)
			{
				result = ((SqlXml)sqlVal).Value;
			}
			return result;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000CD56C File Offset: 0x000CC96C
		[Conditional("DEBUG")]
		private static void AssertIsUserDefinedTypeInstance(object sqlValue, string failedAssertMessage)
		{
			Type type = sqlValue.GetType();
			SqlUserDefinedTypeAttribute[] array = (SqlUserDefinedTypeAttribute[])type.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), true);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000CD598 File Offset: 0x000CC998
		internal static object GetSqlValueFromComVariant(object comVal)
		{
			object result = null;
			if (comVal != null && DBNull.Value != comVal)
			{
				if (comVal is float)
				{
					result = new SqlSingle((float)comVal);
				}
				else if (comVal is string)
				{
					result = new SqlString((string)comVal);
				}
				else if (comVal is double)
				{
					result = new SqlDouble((double)comVal);
				}
				else if (comVal is byte[])
				{
					result = new SqlBinary((byte[])comVal);
				}
				else if (comVal is char)
				{
					result = new SqlString(((char)comVal).ToString());
				}
				else if (comVal is char[])
				{
					result = new SqlChars((char[])comVal);
				}
				else if (comVal is Guid)
				{
					result = new SqlGuid((Guid)comVal);
				}
				else if (comVal is bool)
				{
					result = new SqlBoolean((bool)comVal);
				}
				else if (comVal is byte)
				{
					result = new SqlByte((byte)comVal);
				}
				else if (comVal is short)
				{
					result = new SqlInt16((short)comVal);
				}
				else if (comVal is int)
				{
					result = new SqlInt32((int)comVal);
				}
				else if (comVal is long)
				{
					result = new SqlInt64((long)comVal);
				}
				else if (comVal is decimal)
				{
					result = new SqlDecimal((decimal)comVal);
				}
				else if (comVal is DateTime)
				{
					result = new SqlDateTime((DateTime)comVal);
				}
				else if (comVal is XmlReader)
				{
					result = new SqlXml((XmlReader)comVal);
				}
				else if (comVal is TimeSpan || comVal is DateTimeOffset)
				{
					result = comVal;
				}
			}
			return result;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000CD780 File Offset: 0x000CCB80
		internal static SqlDbType GetSqlDbTypeFromOleDbType(short dbType, string typeName)
		{
			SqlDbType result = SqlDbType.Variant;
			if (dbType <= 64)
			{
				switch (dbType)
				{
				case 2:
				case 18:
					return SqlDbType.SmallInt;
				case 3:
					return SqlDbType.Int;
				case 4:
					return SqlDbType.Real;
				case 5:
					return SqlDbType.Float;
				case 6:
					return (typeName == "smallmoney") ? SqlDbType.SmallMoney : SqlDbType.Money;
				case 7:
					break;
				case 8:
					goto IL_1AE;
				case 9:
				case 10:
				case 13:
				case 15:
				case 19:
					return result;
				case 11:
					return SqlDbType.Bit;
				case 12:
					return SqlDbType.Variant;
				case 14:
					goto IL_166;
				case 16:
				case 17:
					return SqlDbType.TinyInt;
				case 20:
					return SqlDbType.BigInt;
				default:
					if (dbType != 64)
					{
						return result;
					}
					break;
				}
			}
			else
			{
				if (dbType != 72)
				{
					switch (dbType)
					{
					case 128:
						goto IL_194;
					case 129:
						break;
					case 130:
						goto IL_1AE;
					case 131:
						goto IL_166;
					case 132:
						return SqlDbType.Udt;
					case 133:
						return SqlDbType.Date;
					case 134:
					case 136:
					case 137:
					case 138:
					case 139:
					case 140:
					case 142:
					case 143:
					case 144:
						return result;
					case 135:
						goto IL_133;
					case 141:
						return SqlDbType.Xml;
					case 145:
						return SqlDbType.Time;
					case 146:
						return SqlDbType.DateTimeOffset;
					default:
						switch (dbType)
						{
						case 200:
							break;
						case 201:
							return SqlDbType.Text;
						case 202:
							goto IL_1AE;
						case 203:
							return SqlDbType.NText;
						case 204:
							goto IL_194;
						case 205:
							return SqlDbType.Image;
						default:
							return result;
						}
						break;
					}
					return (typeName == "char") ? SqlDbType.Char : SqlDbType.VarChar;
					IL_194:
					return (typeName == "binary") ? SqlDbType.Binary : SqlDbType.VarBinary;
				}
				return SqlDbType.UniqueIdentifier;
			}
			IL_133:
			if (typeName == "smalldatetime")
			{
				return SqlDbType.SmallDateTime;
			}
			if (!(typeName == "datetime2"))
			{
				return SqlDbType.DateTime;
			}
			return SqlDbType.DateTime2;
			IL_166:
			return SqlDbType.Decimal;
			IL_1AE:
			result = ((typeName == "nchar") ? SqlDbType.NChar : SqlDbType.NVarChar);
			return result;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000CD96C File Offset: 0x000CCD6C
		internal static MetaType GetSqlDataType(int tdsType, uint userType, int length)
		{
			if (tdsType <= 165)
			{
				if (tdsType <= 111)
				{
					switch (tdsType)
					{
					case 31:
					case 32:
					case 33:
					case 44:
					case 46:
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
						goto IL_279;
					case 34:
						return MetaType.MetaImage;
					case 35:
						return MetaType.MetaText;
					case 36:
						return MetaType.MetaUniqueId;
					case 37:
						return MetaType.MetaSmallVarBinary;
					case 38:
						if (4 > length)
						{
							if (2 != length)
							{
								return MetaType.MetaTinyInt;
							}
							return MetaType.MetaSmallInt;
						}
						else
						{
							if (4 != length)
							{
								return MetaType.MetaBigInt;
							}
							return MetaType.MetaInt;
						}
						break;
					case 39:
						goto IL_1C6;
					case 40:
						return MetaType.MetaDate;
					case 41:
						return MetaType.MetaTime;
					case 42:
						return MetaType.MetaDateTime2;
					case 43:
						return MetaType.MetaDateTimeOffset;
					case 45:
						goto IL_1CC;
					case 47:
						goto IL_1E3;
					case 48:
						return MetaType.MetaTinyInt;
					case 50:
						break;
					case 52:
						return MetaType.MetaSmallInt;
					case 56:
						return MetaType.MetaInt;
					case 58:
						return MetaType.MetaSmallDateTime;
					case 59:
						return MetaType.MetaReal;
					case 60:
						return MetaType.MetaMoney;
					case 61:
						return MetaType.MetaDateTime;
					case 62:
						return MetaType.MetaFloat;
					default:
						switch (tdsType)
						{
						case 98:
							return MetaType.MetaVariant;
						case 99:
							return MetaType.MetaNText;
						case 100:
						case 101:
						case 102:
						case 103:
						case 105:
						case 107:
							goto IL_279;
						case 104:
							break;
						case 106:
						case 108:
							return MetaType.MetaDecimal;
						case 109:
							if (4 != length)
							{
								return MetaType.MetaFloat;
							}
							return MetaType.MetaReal;
						case 110:
							if (4 != length)
							{
								return MetaType.MetaMoney;
							}
							return MetaType.MetaSmallMoney;
						case 111:
							if (4 != length)
							{
								return MetaType.MetaDateTime;
							}
							return MetaType.MetaSmallDateTime;
						default:
							goto IL_279;
						}
						break;
					}
					return MetaType.MetaBit;
				}
				if (tdsType == 122)
				{
					return MetaType.MetaSmallMoney;
				}
				if (tdsType == 127)
				{
					return MetaType.MetaBigInt;
				}
				if (tdsType != 165)
				{
					goto IL_279;
				}
				return MetaType.MetaVarBinary;
			}
			else if (tdsType <= 173)
			{
				if (tdsType != 167)
				{
					if (tdsType != 173)
					{
						goto IL_279;
					}
					goto IL_1CC;
				}
			}
			else
			{
				if (tdsType == 175)
				{
					goto IL_1E3;
				}
				if (tdsType == 231)
				{
					return MetaType.MetaNVarChar;
				}
				switch (tdsType)
				{
				case 239:
					return MetaType.MetaNChar;
				case 240:
					return MetaType.MetaUdt;
				case 241:
					return MetaType.MetaXml;
				case 242:
					goto IL_279;
				case 243:
					return MetaType.MetaTable;
				default:
					goto IL_279;
				}
			}
			IL_1C6:
			return MetaType.MetaVarChar;
			IL_1CC:
			if (80U != userType)
			{
				return MetaType.MetaBinary;
			}
			return MetaType.MetaTimestamp;
			IL_1E3:
			return MetaType.MetaChar;
			IL_279:
			throw SQL.InvalidSqlDbType((SqlDbType)tdsType);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000CDBF8 File Offset: 0x000CCFF8
		internal static MetaType GetDefaultMetaType()
		{
			return MetaType.MetaNVarChar;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000CDC0C File Offset: 0x000CD00C
		internal static string GetStringFromXml(XmlReader xmlreader)
		{
			SqlXml sqlXml = new SqlXml(xmlreader);
			return sqlXml.Value;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000CDC28 File Offset: 0x000CD028
		public static TdsDateTime FromDateTime(DateTime dateTime, byte cb)
		{
			TdsDateTime result = default(TdsDateTime);
			SqlDateTime sqlDateTime;
			if (cb == 8)
			{
				sqlDateTime = new SqlDateTime(dateTime);
				result.time = sqlDateTime.TimeTicks;
			}
			else
			{
				sqlDateTime = new SqlDateTime(dateTime.AddSeconds(30.0));
				result.time = sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute;
			}
			result.days = sqlDateTime.DayTicks;
			return result;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000CDC94 File Offset: 0x000CD094
		public static DateTime ToDateTime(int sqlDays, int sqlTime, int length)
		{
			if (length == 4)
			{
				return new SqlDateTime(sqlDays, sqlTime * SqlDateTime.SQLTicksPerMinute).Value;
			}
			return new SqlDateTime(sqlDays, sqlTime).Value;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000CDCCC File Offset: 0x000CD0CC
		internal static int GetTimeSizeFromScale(byte scale)
		{
			if (scale <= 2)
			{
				return 3;
			}
			if (scale <= 4)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x0400106C RID: 4204
		internal readonly Type ClassType;

		// Token: 0x0400106D RID: 4205
		internal readonly Type SqlType;

		// Token: 0x0400106E RID: 4206
		internal readonly int FixedLength;

		// Token: 0x0400106F RID: 4207
		internal readonly bool IsFixed;

		// Token: 0x04001070 RID: 4208
		internal readonly bool IsLong;

		// Token: 0x04001071 RID: 4209
		internal readonly bool IsPlp;

		// Token: 0x04001072 RID: 4210
		internal readonly byte Precision;

		// Token: 0x04001073 RID: 4211
		internal readonly byte Scale;

		// Token: 0x04001074 RID: 4212
		internal readonly byte TDSType;

		// Token: 0x04001075 RID: 4213
		internal readonly byte NullableType;

		// Token: 0x04001076 RID: 4214
		internal readonly string TypeName;

		// Token: 0x04001077 RID: 4215
		internal readonly SqlDbType SqlDbType;

		// Token: 0x04001078 RID: 4216
		internal readonly DbType DbType;

		// Token: 0x04001079 RID: 4217
		internal readonly byte PropBytes;

		// Token: 0x0400107A RID: 4218
		internal readonly bool IsAnsiType;

		// Token: 0x0400107B RID: 4219
		internal readonly bool IsBinType;

		// Token: 0x0400107C RID: 4220
		internal readonly bool IsCharType;

		// Token: 0x0400107D RID: 4221
		internal readonly bool IsNCharType;

		// Token: 0x0400107E RID: 4222
		internal readonly bool IsSizeInCharacters;

		// Token: 0x0400107F RID: 4223
		internal readonly bool IsNewKatmaiType;

		// Token: 0x04001080 RID: 4224
		internal readonly bool IsVarTime;

		// Token: 0x04001081 RID: 4225
		internal readonly bool Is70Supported;

		// Token: 0x04001082 RID: 4226
		internal readonly bool Is80Supported;

		// Token: 0x04001083 RID: 4227
		internal readonly bool Is90Supported;

		// Token: 0x04001084 RID: 4228
		internal readonly bool Is100Supported;

		// Token: 0x04001085 RID: 4229
		private static readonly MetaType MetaBigInt = new MetaType(19, byte.MaxValue, 8, true, false, false, 127, 38, "bigint", typeof(long), typeof(SqlInt64), SqlDbType.BigInt, DbType.Int64, 0);

		// Token: 0x04001086 RID: 4230
		private static readonly MetaType MetaFloat = new MetaType(15, byte.MaxValue, 8, true, false, false, 62, 109, "float", typeof(double), typeof(SqlDouble), SqlDbType.Float, DbType.Double, 0);

		// Token: 0x04001087 RID: 4231
		private static readonly MetaType MetaReal = new MetaType(7, byte.MaxValue, 4, true, false, false, 59, 109, "real", typeof(float), typeof(SqlSingle), SqlDbType.Real, DbType.Single, 0);

		// Token: 0x04001088 RID: 4232
		private static readonly MetaType MetaBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 173, 173, "binary", typeof(byte[]), typeof(SqlBinary), SqlDbType.Binary, DbType.Binary, 2);

		// Token: 0x04001089 RID: 4233
		private static readonly MetaType MetaTimestamp = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 173, 173, "timestamp", typeof(byte[]), typeof(SqlBinary), SqlDbType.Timestamp, DbType.Binary, 2);

		// Token: 0x0400108A RID: 4234
		internal static readonly MetaType MetaVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 165, 165, "varbinary", typeof(byte[]), typeof(SqlBinary), SqlDbType.VarBinary, DbType.Binary, 2);

		// Token: 0x0400108B RID: 4235
		internal static readonly MetaType MetaMaxVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 165, 165, "varbinary", typeof(byte[]), typeof(SqlBinary), SqlDbType.VarBinary, DbType.Binary, 2);

		// Token: 0x0400108C RID: 4236
		private static readonly MetaType MetaSmallVarBinary = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 37, 173, ADP.StrEmpty, typeof(byte[]), typeof(SqlBinary), (SqlDbType)24, DbType.Binary, 2);

		// Token: 0x0400108D RID: 4237
		internal static readonly MetaType MetaImage = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 34, 34, "image", typeof(byte[]), typeof(SqlBinary), SqlDbType.Image, DbType.Binary, 0);

		// Token: 0x0400108E RID: 4238
		private static readonly MetaType MetaBit = new MetaType(byte.MaxValue, byte.MaxValue, 1, true, false, false, 50, 104, "bit", typeof(bool), typeof(SqlBoolean), SqlDbType.Bit, DbType.Boolean, 0);

		// Token: 0x0400108F RID: 4239
		private static readonly MetaType MetaTinyInt = new MetaType(3, byte.MaxValue, 1, true, false, false, 48, 38, "tinyint", typeof(byte), typeof(SqlByte), SqlDbType.TinyInt, DbType.Byte, 0);

		// Token: 0x04001090 RID: 4240
		private static readonly MetaType MetaSmallInt = new MetaType(5, byte.MaxValue, 2, true, false, false, 52, 38, "smallint", typeof(short), typeof(SqlInt16), SqlDbType.SmallInt, DbType.Int16, 0);

		// Token: 0x04001091 RID: 4241
		private static readonly MetaType MetaInt = new MetaType(10, byte.MaxValue, 4, true, false, false, 56, 38, "int", typeof(int), typeof(SqlInt32), SqlDbType.Int, DbType.Int32, 0);

		// Token: 0x04001092 RID: 4242
		private static readonly MetaType MetaChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 175, 175, "char", typeof(string), typeof(SqlString), SqlDbType.Char, DbType.AnsiStringFixedLength, 7);

		// Token: 0x04001093 RID: 4243
		private static readonly MetaType MetaVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 167, 167, "varchar", typeof(string), typeof(SqlString), SqlDbType.VarChar, DbType.AnsiString, 7);

		// Token: 0x04001094 RID: 4244
		internal static readonly MetaType MetaMaxVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 167, 167, "varchar", typeof(string), typeof(SqlString), SqlDbType.VarChar, DbType.AnsiString, 7);

		// Token: 0x04001095 RID: 4245
		internal static readonly MetaType MetaText = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 35, 35, "text", typeof(string), typeof(SqlString), SqlDbType.Text, DbType.AnsiString, 0);

		// Token: 0x04001096 RID: 4246
		private static readonly MetaType MetaNChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 239, 239, "nchar", typeof(string), typeof(SqlString), SqlDbType.NChar, DbType.StringFixedLength, 7);

		// Token: 0x04001097 RID: 4247
		internal static readonly MetaType MetaNVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 231, 231, "nvarchar", typeof(string), typeof(SqlString), SqlDbType.NVarChar, DbType.String, 7);

		// Token: 0x04001098 RID: 4248
		internal static readonly MetaType MetaMaxNVarChar = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 231, 231, "nvarchar", typeof(string), typeof(SqlString), SqlDbType.NVarChar, DbType.String, 7);

		// Token: 0x04001099 RID: 4249
		internal static readonly MetaType MetaNText = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, false, 99, 99, "ntext", typeof(string), typeof(SqlString), SqlDbType.NText, DbType.String, 7);

		// Token: 0x0400109A RID: 4250
		internal static readonly MetaType MetaDecimal = new MetaType(38, 4, 17, true, false, false, 108, 108, "decimal", typeof(decimal), typeof(SqlDecimal), SqlDbType.Decimal, DbType.Decimal, 2);

		// Token: 0x0400109B RID: 4251
		internal static readonly MetaType MetaXml = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 241, 241, "xml", typeof(string), typeof(SqlXml), SqlDbType.Xml, DbType.Xml, 0);

		// Token: 0x0400109C RID: 4252
		private static readonly MetaType MetaDateTime = new MetaType(23, 3, 8, true, false, false, 61, 111, "datetime", typeof(DateTime), typeof(SqlDateTime), SqlDbType.DateTime, DbType.DateTime, 0);

		// Token: 0x0400109D RID: 4253
		private static readonly MetaType MetaSmallDateTime = new MetaType(16, 0, 4, true, false, false, 58, 111, "smalldatetime", typeof(DateTime), typeof(SqlDateTime), SqlDbType.SmallDateTime, DbType.DateTime, 0);

		// Token: 0x0400109E RID: 4254
		private static readonly MetaType MetaMoney = new MetaType(19, byte.MaxValue, 8, true, false, false, 60, 110, "money", typeof(decimal), typeof(SqlMoney), SqlDbType.Money, DbType.Currency, 0);

		// Token: 0x0400109F RID: 4255
		private static readonly MetaType MetaSmallMoney = new MetaType(10, byte.MaxValue, 4, true, false, false, 122, 110, "smallmoney", typeof(decimal), typeof(SqlMoney), SqlDbType.SmallMoney, DbType.Currency, 0);

		// Token: 0x040010A0 RID: 4256
		private static readonly MetaType MetaUniqueId = new MetaType(byte.MaxValue, byte.MaxValue, 16, true, false, false, 36, 36, "uniqueidentifier", typeof(Guid), typeof(SqlGuid), SqlDbType.UniqueIdentifier, DbType.Guid, 0);

		// Token: 0x040010A1 RID: 4257
		private static readonly MetaType MetaVariant = new MetaType(byte.MaxValue, byte.MaxValue, -1, true, false, false, 98, 98, "sql_variant", typeof(object), typeof(object), SqlDbType.Variant, DbType.Object, 0);

		// Token: 0x040010A2 RID: 4258
		internal static readonly MetaType MetaUdt = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, true, 240, 240, "udt", typeof(object), typeof(object), SqlDbType.Udt, DbType.Object, 0);

		// Token: 0x040010A3 RID: 4259
		private static readonly MetaType MetaMaxUdt = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, true, true, 240, 240, "udt", typeof(object), typeof(object), SqlDbType.Udt, DbType.Object, 0);

		// Token: 0x040010A4 RID: 4260
		private static readonly MetaType MetaTable = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 243, 243, "table", typeof(IEnumerable<DbDataRecord>), typeof(IEnumerable<DbDataRecord>), SqlDbType.Structured, DbType.Object, 0);

		// Token: 0x040010A5 RID: 4261
		private static readonly MetaType MetaSUDT = new MetaType(byte.MaxValue, byte.MaxValue, -1, false, false, false, 31, 31, "", typeof(SqlDataRecord), typeof(SqlDataRecord), SqlDbType.Structured, DbType.Object, 0);

		// Token: 0x040010A6 RID: 4262
		private static readonly MetaType MetaDate = new MetaType(byte.MaxValue, byte.MaxValue, 3, true, false, false, 40, 40, "date", typeof(DateTime), typeof(DateTime), SqlDbType.Date, DbType.Date, 0);

		// Token: 0x040010A7 RID: 4263
		internal static readonly MetaType MetaTime = new MetaType(byte.MaxValue, 7, -1, false, false, false, 41, 41, "time", typeof(TimeSpan), typeof(TimeSpan), SqlDbType.Time, DbType.Time, 1);

		// Token: 0x040010A8 RID: 4264
		private static readonly MetaType MetaDateTime2 = new MetaType(byte.MaxValue, 7, -1, false, false, false, 42, 42, "datetime2", typeof(DateTime), typeof(DateTime), SqlDbType.DateTime2, DbType.DateTime2, 1);

		// Token: 0x040010A9 RID: 4265
		internal static readonly MetaType MetaDateTimeOffset = new MetaType(byte.MaxValue, 7, -1, false, false, false, 43, 43, "datetimeoffset", typeof(DateTimeOffset), typeof(DateTimeOffset), SqlDbType.DateTimeOffset, DbType.DateTimeOffset, 1);
	}
}
