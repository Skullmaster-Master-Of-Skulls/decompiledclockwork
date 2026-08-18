using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000031 RID: 49
	internal class MetaDataUtilsSmi
	{
		// Token: 0x0600019C RID: 412 RVA: 0x001DB848 File Offset: 0x001DAC48
		internal static bool IsCharOrXmlType(SqlDbType type)
		{
			return MetaDataUtilsSmi.IsUnicodeType(type) || MetaDataUtilsSmi.IsAnsiType(type) || type == SqlDbType.Xml;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x001DB878 File Offset: 0x001DAC78
		internal static bool IsUnicodeType(SqlDbType type)
		{
			return type == SqlDbType.NChar || type == SqlDbType.NVarChar || type == SqlDbType.NText;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x001DB898 File Offset: 0x001DAC98
		internal static bool IsAnsiType(SqlDbType type)
		{
			return type == SqlDbType.Char || type == SqlDbType.VarChar || type == SqlDbType.Text;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x001DB8B8 File Offset: 0x001DACB8
		internal static bool IsBinaryType(SqlDbType type)
		{
			return type == SqlDbType.Binary || type == SqlDbType.VarBinary || type == SqlDbType.Image;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x001DB8D8 File Offset: 0x001DACD8
		internal static bool IsPlpFormat(SmiMetaData metaData)
		{
			return metaData.MaxLength == -1L || metaData.SqlDbType == SqlDbType.Image || metaData.SqlDbType == SqlDbType.NText || metaData.SqlDbType == SqlDbType.Text || metaData.SqlDbType == SqlDbType.Udt;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x001DB918 File Offset: 0x001DAD18
		internal static ExtendedClrTypeCode DetermineExtendedTypeCodeForUseWithSqlDbType(SqlDbType dbType, bool isMultiValued, object value, Type udtType, ulong smiVersion)
		{
			ExtendedClrTypeCode extendedClrTypeCode = ExtendedClrTypeCode.Invalid;
			if (value == null)
			{
				extendedClrTypeCode = ExtendedClrTypeCode.Empty;
			}
			else if (DBNull.Value == value)
			{
				extendedClrTypeCode = ExtendedClrTypeCode.DBNull;
			}
			else
			{
				switch (dbType)
				{
				case SqlDbType.BigInt:
					if (value.GetType() == typeof(long))
					{
						return ExtendedClrTypeCode.Int64;
					}
					if (value.GetType() == typeof(SqlInt64))
					{
						return ExtendedClrTypeCode.SqlInt64;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Int64)
					{
						return ExtendedClrTypeCode.Int64;
					}
					return extendedClrTypeCode;
				case SqlDbType.Binary:
				case SqlDbType.Image:
				case SqlDbType.Timestamp:
				case SqlDbType.VarBinary:
					if (value.GetType() == typeof(byte[]))
					{
						return ExtendedClrTypeCode.ByteArray;
					}
					if (value.GetType() == typeof(SqlBinary))
					{
						return ExtendedClrTypeCode.SqlBinary;
					}
					if (value.GetType() == typeof(SqlBytes))
					{
						return ExtendedClrTypeCode.SqlBytes;
					}
					return extendedClrTypeCode;
				case SqlDbType.Bit:
					if (value.GetType() == typeof(bool))
					{
						return ExtendedClrTypeCode.Boolean;
					}
					if (value.GetType() == typeof(SqlBoolean))
					{
						return ExtendedClrTypeCode.SqlBoolean;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Boolean)
					{
						return ExtendedClrTypeCode.Boolean;
					}
					return extendedClrTypeCode;
				case SqlDbType.Char:
				case SqlDbType.NChar:
				case SqlDbType.NText:
				case SqlDbType.NVarChar:
				case SqlDbType.Text:
				case SqlDbType.VarChar:
					if (value.GetType() == typeof(string))
					{
						return ExtendedClrTypeCode.String;
					}
					if (value.GetType() == typeof(SqlString))
					{
						return ExtendedClrTypeCode.SqlString;
					}
					if (value.GetType() == typeof(char[]))
					{
						return ExtendedClrTypeCode.CharArray;
					}
					if (value.GetType() == typeof(SqlChars))
					{
						return ExtendedClrTypeCode.SqlChars;
					}
					if (value.GetType() == typeof(char))
					{
						return ExtendedClrTypeCode.Char;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Char)
					{
						return ExtendedClrTypeCode.Char;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.String)
					{
						return ExtendedClrTypeCode.String;
					}
					return extendedClrTypeCode;
				case SqlDbType.DateTime:
				case SqlDbType.SmallDateTime:
					break;
				case SqlDbType.Decimal:
					if (value.GetType() == typeof(decimal))
					{
						return ExtendedClrTypeCode.Decimal;
					}
					if (value.GetType() == typeof(SqlDecimal))
					{
						return ExtendedClrTypeCode.SqlDecimal;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Decimal)
					{
						return ExtendedClrTypeCode.Decimal;
					}
					return extendedClrTypeCode;
				case SqlDbType.Float:
					if (value.GetType() == typeof(SqlDouble))
					{
						return ExtendedClrTypeCode.SqlDouble;
					}
					if (value.GetType() == typeof(double))
					{
						return ExtendedClrTypeCode.Double;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Double)
					{
						return ExtendedClrTypeCode.Double;
					}
					return extendedClrTypeCode;
				case SqlDbType.Int:
					if (value.GetType() == typeof(int))
					{
						return ExtendedClrTypeCode.Int32;
					}
					if (value.GetType() == typeof(SqlInt32))
					{
						return ExtendedClrTypeCode.SqlInt32;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Int32)
					{
						return ExtendedClrTypeCode.Int32;
					}
					return extendedClrTypeCode;
				case SqlDbType.Money:
				case SqlDbType.SmallMoney:
					if (value.GetType() == typeof(SqlMoney))
					{
						return ExtendedClrTypeCode.SqlMoney;
					}
					if (value.GetType() == typeof(decimal))
					{
						return ExtendedClrTypeCode.Decimal;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Decimal)
					{
						return ExtendedClrTypeCode.Decimal;
					}
					return extendedClrTypeCode;
				case SqlDbType.Real:
					if (value.GetType() == typeof(float))
					{
						return ExtendedClrTypeCode.Single;
					}
					if (value.GetType() == typeof(SqlSingle))
					{
						return ExtendedClrTypeCode.SqlSingle;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Single)
					{
						return ExtendedClrTypeCode.Single;
					}
					return extendedClrTypeCode;
				case SqlDbType.UniqueIdentifier:
					if (value.GetType() == typeof(SqlGuid))
					{
						return ExtendedClrTypeCode.SqlGuid;
					}
					if (value.GetType() == typeof(Guid))
					{
						return ExtendedClrTypeCode.Guid;
					}
					return extendedClrTypeCode;
				case SqlDbType.SmallInt:
					if (value.GetType() == typeof(short))
					{
						return ExtendedClrTypeCode.Int16;
					}
					if (value.GetType() == typeof(SqlInt16))
					{
						return ExtendedClrTypeCode.SqlInt16;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Int16)
					{
						return ExtendedClrTypeCode.Int16;
					}
					return extendedClrTypeCode;
				case SqlDbType.TinyInt:
					if (value.GetType() == typeof(byte))
					{
						return ExtendedClrTypeCode.Byte;
					}
					if (value.GetType() == typeof(SqlByte))
					{
						return ExtendedClrTypeCode.SqlByte;
					}
					if (Type.GetTypeCode(value.GetType()) == TypeCode.Byte)
					{
						return ExtendedClrTypeCode.Byte;
					}
					return extendedClrTypeCode;
				case SqlDbType.Variant:
					extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(value);
					if (ExtendedClrTypeCode.SqlXml == extendedClrTypeCode)
					{
						return ExtendedClrTypeCode.Invalid;
					}
					return extendedClrTypeCode;
				case (SqlDbType)24:
				case (SqlDbType)26:
				case (SqlDbType)27:
				case (SqlDbType)28:
					return extendedClrTypeCode;
				case SqlDbType.Xml:
					if (value.GetType() == typeof(SqlXml))
					{
						return ExtendedClrTypeCode.SqlXml;
					}
					if (value.GetType() == typeof(string))
					{
						return ExtendedClrTypeCode.String;
					}
					return extendedClrTypeCode;
				case SqlDbType.Udt:
					if (udtType == null || value.GetType() == udtType)
					{
						return ExtendedClrTypeCode.Object;
					}
					return ExtendedClrTypeCode.Invalid;
				case SqlDbType.Structured:
					if (!isMultiValued)
					{
						return extendedClrTypeCode;
					}
					if (value is DataTable)
					{
						return ExtendedClrTypeCode.DataTable;
					}
					if (value is IEnumerable<SqlDataRecord>)
					{
						return ExtendedClrTypeCode.IEnumerableOfSqlDataRecord;
					}
					if (value is DbDataReader)
					{
						return ExtendedClrTypeCode.DbDataReader;
					}
					return extendedClrTypeCode;
				case SqlDbType.Date:
				case SqlDbType.DateTime2:
					if (smiVersion < 210UL)
					{
						return extendedClrTypeCode;
					}
					break;
				case SqlDbType.Time:
					if (value.GetType() == typeof(TimeSpan) && smiVersion >= 210UL)
					{
						return ExtendedClrTypeCode.TimeSpan;
					}
					return extendedClrTypeCode;
				case SqlDbType.DateTimeOffset:
					if (value.GetType() == typeof(DateTimeOffset) && smiVersion >= 210UL)
					{
						return ExtendedClrTypeCode.DateTimeOffset;
					}
					return extendedClrTypeCode;
				default:
					return extendedClrTypeCode;
				}
				if (value.GetType() == typeof(DateTime))
				{
					extendedClrTypeCode = ExtendedClrTypeCode.DateTime;
				}
				else if (value.GetType() == typeof(SqlDateTime))
				{
					extendedClrTypeCode = ExtendedClrTypeCode.SqlDateTime;
				}
				else if (Type.GetTypeCode(value.GetType()) == TypeCode.DateTime)
				{
					extendedClrTypeCode = ExtendedClrTypeCode.DateTime;
				}
			}
			return extendedClrTypeCode;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x001DBEF8 File Offset: 0x001DB2F8
		internal static ExtendedClrTypeCode DetermineExtendedTypeCodeFromType(Type clrType)
		{
			object obj = MetaDataUtilsSmi.__typeToExtendedTypeCodeMap[clrType];
			ExtendedClrTypeCode result;
			if (obj == null)
			{
				result = ExtendedClrTypeCode.Invalid;
			}
			else
			{
				result = (ExtendedClrTypeCode)obj;
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x001DBF28 File Offset: 0x001DB328
		internal static ExtendedClrTypeCode DetermineExtendedTypeCode(object value)
		{
			ExtendedClrTypeCode result;
			if (value == null)
			{
				result = ExtendedClrTypeCode.Empty;
			}
			else
			{
				result = MetaDataUtilsSmi.DetermineExtendedTypeCodeFromType(value.GetType());
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x001DBF58 File Offset: 0x001DB358
		internal static SqlDbType InferSqlDbTypeFromTypeCode(ExtendedClrTypeCode typeCode)
		{
			return MetaDataUtilsSmi.__extendedTypeCodeToSqlDbTypeMap[(int)(typeCode + 1)];
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x001DBF78 File Offset: 0x001DB378
		internal static SqlDbType InferSqlDbTypeFromType(Type type)
		{
			ExtendedClrTypeCode extendedClrTypeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeFromType(type);
			SqlDbType result;
			if (ExtendedClrTypeCode.Invalid == extendedClrTypeCode)
			{
				result = (SqlDbType)(-1);
			}
			else
			{
				result = MetaDataUtilsSmi.InferSqlDbTypeFromTypeCode(extendedClrTypeCode);
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x001DBFA8 File Offset: 0x001DB3A8
		internal static SqlDbType InferSqlDbTypeFromType_Katmai(Type type)
		{
			SqlDbType sqlDbType = MetaDataUtilsSmi.InferSqlDbTypeFromType(type);
			if (SqlDbType.DateTime == sqlDbType)
			{
				sqlDbType = SqlDbType.DateTime2;
			}
			return sqlDbType;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x001DBFC8 File Offset: 0x001DB3C8
		internal static bool IsValidForSmiVersion(SmiExtendedMetaData md, ulong smiVersion)
		{
			return 210UL == smiVersion || (md.SqlDbType != SqlDbType.Structured && md.SqlDbType != SqlDbType.Date && md.SqlDbType != SqlDbType.DateTime2 && md.SqlDbType != SqlDbType.DateTimeOffset && md.SqlDbType != SqlDbType.Time);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x001DC018 File Offset: 0x001DB418
		internal static SqlMetaData SmiExtendedMetaDataToSqlMetaData(SmiExtendedMetaData source)
		{
			if (SqlDbType.Xml == source.SqlDbType)
			{
				return new SqlMetaData(source.Name, source.SqlDbType, source.MaxLength, source.Precision, source.Scale, source.LocaleId, source.CompareOptions, source.TypeSpecificNamePart1, source.TypeSpecificNamePart2, source.TypeSpecificNamePart3, true, source.Type);
			}
			return new SqlMetaData(source.Name, source.SqlDbType, source.MaxLength, source.Precision, source.Scale, source.LocaleId, source.CompareOptions, source.Type);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x001DC0B8 File Offset: 0x001DB4B8
		internal static SmiExtendedMetaData SqlMetaDataToSmiExtendedMetaData(SqlMetaData source)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (SqlDbType.Xml == source.SqlDbType)
			{
				text = source.XmlSchemaCollectionDatabase;
				text2 = source.XmlSchemaCollectionOwningSchema;
				text3 = source.XmlSchemaCollectionName;
			}
			else if (SqlDbType.Udt == source.SqlDbType)
			{
				string serverTypeName = source.ServerTypeName;
				if (serverTypeName != null)
				{
					string[] array = SqlParameter.ParseTypeName(serverTypeName, true);
					if (1 == array.Length)
					{
						text3 = array[0];
					}
					else if (2 == array.Length)
					{
						text2 = array[0];
						text3 = array[1];
					}
					else
					{
						if (3 != array.Length)
						{
							throw ADP.ArgumentOutOfRange("typeName");
						}
						text = array[0];
						text2 = array[1];
						text3 = array[2];
					}
					if ((!ADP.IsEmpty(text) && 255 < text.Length) || (!ADP.IsEmpty(text2) && 255 < text2.Length) || (!ADP.IsEmpty(text3) && 255 < text3.Length))
					{
						throw ADP.ArgumentOutOfRange("typeName");
					}
				}
			}
			return new SmiExtendedMetaData(source.SqlDbType, source.MaxLength, source.Precision, source.Scale, source.LocaleId, source.CompareOptions, source.Type, source.Name, text, text2, text3);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x001DC1D8 File Offset: 0x001DB5D8
		internal static bool IsCompatible(SmiMetaData firstMd, SqlMetaData secondMd)
		{
			return firstMd.SqlDbType == secondMd.SqlDbType && firstMd.MaxLength == secondMd.MaxLength && firstMd.Precision == secondMd.Precision && firstMd.Scale == secondMd.Scale && firstMd.CompareOptions == secondMd.CompareOptions && firstMd.LocaleId == secondMd.LocaleId && firstMd.Type == secondMd.Type && firstMd.SqlDbType != SqlDbType.Structured && !firstMd.IsMultiValued;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x001DC268 File Offset: 0x001DB668
		internal static long AdjustMaxLength(SqlDbType dbType, long maxLength)
		{
			if (-1L != maxLength)
			{
				if (maxLength < 0L)
				{
					maxLength = -2L;
				}
				switch (dbType)
				{
				case SqlDbType.Binary:
					if (maxLength > 8000L)
					{
						maxLength = -2L;
					}
					break;
				case SqlDbType.Bit:
					break;
				case SqlDbType.Char:
					if (maxLength > 8000L)
					{
						maxLength = -2L;
					}
					break;
				default:
					switch (dbType)
					{
					case SqlDbType.NChar:
						if (maxLength > 4000L)
						{
							maxLength = -2L;
						}
						break;
					case SqlDbType.NText:
						break;
					case SqlDbType.NVarChar:
						if (4000L < maxLength)
						{
							maxLength = -1L;
						}
						break;
					default:
						switch (dbType)
						{
						case SqlDbType.VarBinary:
							if (8000L < maxLength)
							{
								maxLength = -1L;
							}
							break;
						case SqlDbType.VarChar:
							if (8000L < maxLength)
							{
								maxLength = -1L;
							}
							break;
						}
						break;
					}
					break;
				}
			}
			return maxLength;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x001DC328 File Offset: 0x001DB728
		internal static SmiExtendedMetaData SmiMetaDataFromDataColumn(DataColumn column, DataTable parent)
		{
			SqlDbType sqlDbType = MetaDataUtilsSmi.InferSqlDbTypeFromType_Katmai(column.DataType);
			if ((SqlDbType)(-1) == sqlDbType)
			{
				throw SQL.UnsupportedColumnTypeForSqlProvider(column.ColumnName, column.DataType.Name);
			}
			long num = MetaDataUtilsSmi.AdjustMaxLength(sqlDbType, (long)column.MaxLength);
			if (-2L == num)
			{
				throw SQL.InvalidColumnMaxLength(column.ColumnName, num);
			}
			byte b;
			byte b4;
			checked
			{
				if (column.DataType == typeof(SqlDecimal))
				{
					b = 0;
					byte b2 = 0;
					foreach (object obj in parent.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						object obj2 = dataRow[column];
						if (!(obj2 is DBNull))
						{
							SqlDecimal sqlDecimal = (SqlDecimal)obj2;
							if (!sqlDecimal.IsNull)
							{
								byte b3 = sqlDecimal.Precision - sqlDecimal.Scale;
								if (b3 > b2)
								{
									b2 = b3;
								}
								if (sqlDecimal.Scale > b)
								{
									b = sqlDecimal.Scale;
								}
							}
						}
					}
					b4 = b2 + b;
					if (SqlDecimal.MaxPrecision < b4)
					{
						throw SQL.InvalidTableDerivedPrecisionForTvp(column.ColumnName, b4);
					}
					if (b4 == 0)
					{
						b4 = 1;
					}
				}
				else if (sqlDbType == SqlDbType.DateTime2 || sqlDbType == SqlDbType.DateTimeOffset || sqlDbType == SqlDbType.Time)
				{
					b4 = 0;
					b = SmiMetaData.DefaultTime.Scale;
				}
				else if (sqlDbType == SqlDbType.Decimal)
				{
					b = 0;
					byte b5 = 0;
					foreach (object obj3 in parent.Rows)
					{
						DataRow dataRow2 = (DataRow)obj3;
						object obj4 = dataRow2[column];
						if (!(obj4 is DBNull))
						{
							SqlDecimal sqlDecimal2 = (decimal)obj4;
							byte b6 = sqlDecimal2.Precision - sqlDecimal2.Scale;
							if (b6 > b5)
							{
								b5 = b6;
							}
							if (sqlDecimal2.Scale > b)
							{
								b = sqlDecimal2.Scale;
							}
						}
					}
					b4 = b5 + b;
					if (SqlDecimal.MaxPrecision < b4)
					{
						throw SQL.InvalidTableDerivedPrecisionForTvp(column.ColumnName, b4);
					}
					if (b4 == 0)
					{
						b4 = 1;
					}
				}
				else
				{
					b4 = 0;
					b = 0;
				}
			}
			return new SmiExtendedMetaData(sqlDbType, num, b4, b, (long)column.Locale.LCID, SmiMetaData.DefaultNVarChar.CompareOptions, column.DataType, false, null, null, column.ColumnName, null, null, null);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x001DC598 File Offset: 0x001DB998
		internal static SmiExtendedMetaData SmiMetaDataFromSchemaTableRow(DataRow schemaRow)
		{
			string text = "";
			object obj = schemaRow[SchemaTableColumn.ColumnName];
			if (DBNull.Value != obj)
			{
				text = (string)obj;
			}
			obj = schemaRow[SchemaTableColumn.DataType];
			if (DBNull.Value == obj)
			{
				throw SQL.NullSchemaTableDataTypeNotSupported(text);
			}
			Type type = (Type)obj;
			SqlDbType sqlDbType = MetaDataUtilsSmi.InferSqlDbTypeFromType_Katmai(type);
			if ((SqlDbType)(-1) == sqlDbType)
			{
				if (typeof(object) != type)
				{
					throw SQL.UnsupportedColumnTypeForSqlProvider(text, type.ToString());
				}
				sqlDbType = SqlDbType.VarBinary;
			}
			long num = 0L;
			byte b = 0;
			byte b2 = 0;
			switch (sqlDbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Float:
			case SqlDbType.Image:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.NText:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.Text:
			case SqlDbType.Timestamp:
			case SqlDbType.TinyInt:
			case SqlDbType.Variant:
			case SqlDbType.Xml:
			case SqlDbType.Date:
				goto IL_2FD;
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				obj = schemaRow[SchemaTableColumn.ColumnSize];
				if (DBNull.Value == obj)
				{
					if (SqlDbType.Binary == sqlDbType)
					{
						num = 8000L;
						goto IL_2FD;
					}
					num = -1L;
					goto IL_2FD;
				}
				else
				{
					num = Convert.ToInt64(obj, null);
					if (num > 8000L)
					{
						num = -1L;
					}
					if (num < 0L && (num != -1L || SqlDbType.Binary == sqlDbType))
					{
						throw SQL.InvalidColumnMaxLength(text, num);
					}
					goto IL_2FD;
				}
				break;
			case SqlDbType.Char:
			case SqlDbType.VarChar:
				obj = schemaRow[SchemaTableColumn.ColumnSize];
				if (DBNull.Value == obj)
				{
					if (SqlDbType.Char == sqlDbType)
					{
						num = 8000L;
						goto IL_2FD;
					}
					num = -1L;
					goto IL_2FD;
				}
				else
				{
					num = Convert.ToInt64(obj, null);
					if (num > 8000L)
					{
						num = -1L;
					}
					if (num < 0L && (num != -1L || SqlDbType.Char == sqlDbType))
					{
						throw SQL.InvalidColumnMaxLength(text, num);
					}
					goto IL_2FD;
				}
				break;
			case SqlDbType.Decimal:
				obj = schemaRow[SchemaTableColumn.NumericPrecision];
				if (DBNull.Value == obj)
				{
					b = SmiMetaData.DefaultDecimal.Precision;
				}
				else
				{
					b = Convert.ToByte(obj, null);
				}
				obj = schemaRow[SchemaTableColumn.NumericScale];
				if (DBNull.Value == obj)
				{
					b2 = SmiMetaData.DefaultDecimal.Scale;
				}
				else
				{
					b2 = Convert.ToByte(obj, null);
				}
				if (b < 1 || b > SqlDecimal.MaxPrecision || b2 < 0 || b2 > SqlDecimal.MaxScale || b2 > b)
				{
					throw SQL.InvalidColumnPrecScale();
				}
				goto IL_2FD;
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
				obj = schemaRow[SchemaTableColumn.ColumnSize];
				if (DBNull.Value == obj)
				{
					if (SqlDbType.NChar == sqlDbType)
					{
						num = 4000L;
						goto IL_2FD;
					}
					num = -1L;
					goto IL_2FD;
				}
				else
				{
					num = Convert.ToInt64(obj, null);
					if (num > 4000L)
					{
						num = -1L;
					}
					if (num < 0L && (num != -1L || SqlDbType.NChar == sqlDbType))
					{
						throw SQL.InvalidColumnMaxLength(text, num);
					}
					goto IL_2FD;
				}
				break;
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				obj = schemaRow[SchemaTableColumn.NumericScale];
				if (DBNull.Value == obj)
				{
					b2 = SmiMetaData.DefaultTime.Scale;
				}
				else
				{
					b2 = Convert.ToByte(obj, null);
				}
				if (b2 > 7)
				{
					throw SQL.InvalidColumnPrecScale();
				}
				if (b2 < 0)
				{
					b2 = SmiMetaData.DefaultTime.Scale;
					goto IL_2FD;
				}
				goto IL_2FD;
			}
			throw SQL.UnsupportedColumnTypeForSqlProvider(text, type.ToString());
			IL_2FD:
			return new SmiExtendedMetaData(sqlDbType, num, b, b2, (long)CultureInfo.CurrentCulture.LCID, SmiMetaData.GetDefaultForType(sqlDbType).CompareOptions, null, false, null, null, text, null, null, null);
		}

		// Token: 0x04000572 RID: 1394
		internal const SqlDbType InvalidSqlDbType = (SqlDbType)(-1);

		// Token: 0x04000573 RID: 1395
		internal const long InvalidMaxLength = -2L;

		// Token: 0x04000574 RID: 1396
		private static readonly SqlDbType[] __extendedTypeCodeToSqlDbTypeMap = new SqlDbType[]
		{
			(SqlDbType)(-1),
			SqlDbType.Bit,
			SqlDbType.TinyInt,
			SqlDbType.NVarChar,
			SqlDbType.DateTime,
			(SqlDbType)(-1),
			SqlDbType.Decimal,
			SqlDbType.Float,
			(SqlDbType)(-1),
			SqlDbType.SmallInt,
			SqlDbType.Int,
			SqlDbType.BigInt,
			(SqlDbType)(-1),
			SqlDbType.Real,
			SqlDbType.NVarChar,
			(SqlDbType)(-1),
			(SqlDbType)(-1),
			(SqlDbType)(-1),
			(SqlDbType)(-1),
			SqlDbType.VarBinary,
			SqlDbType.NVarChar,
			SqlDbType.UniqueIdentifier,
			SqlDbType.VarBinary,
			SqlDbType.Bit,
			SqlDbType.TinyInt,
			SqlDbType.DateTime,
			SqlDbType.Float,
			SqlDbType.UniqueIdentifier,
			SqlDbType.SmallInt,
			SqlDbType.Int,
			SqlDbType.BigInt,
			SqlDbType.Money,
			SqlDbType.Decimal,
			SqlDbType.Real,
			SqlDbType.NVarChar,
			SqlDbType.NVarChar,
			SqlDbType.VarBinary,
			SqlDbType.Xml,
			SqlDbType.Structured,
			SqlDbType.Structured,
			SqlDbType.Structured,
			SqlDbType.Time,
			SqlDbType.DateTimeOffset
		};

		// Token: 0x04000575 RID: 1397
		private static readonly Hashtable __typeToExtendedTypeCodeMap = new Hashtable(42)
		{
			{
				typeof(bool),
				ExtendedClrTypeCode.Boolean
			},
			{
				typeof(byte),
				ExtendedClrTypeCode.Byte
			},
			{
				typeof(char),
				ExtendedClrTypeCode.Char
			},
			{
				typeof(DateTime),
				ExtendedClrTypeCode.DateTime
			},
			{
				typeof(DBNull),
				ExtendedClrTypeCode.DBNull
			},
			{
				typeof(decimal),
				ExtendedClrTypeCode.Decimal
			},
			{
				typeof(double),
				ExtendedClrTypeCode.Double
			},
			{
				typeof(short),
				ExtendedClrTypeCode.Int16
			},
			{
				typeof(int),
				ExtendedClrTypeCode.Int32
			},
			{
				typeof(long),
				ExtendedClrTypeCode.Int64
			},
			{
				typeof(sbyte),
				ExtendedClrTypeCode.SByte
			},
			{
				typeof(float),
				ExtendedClrTypeCode.Single
			},
			{
				typeof(string),
				ExtendedClrTypeCode.String
			},
			{
				typeof(ushort),
				ExtendedClrTypeCode.UInt16
			},
			{
				typeof(uint),
				ExtendedClrTypeCode.UInt32
			},
			{
				typeof(ulong),
				ExtendedClrTypeCode.UInt64
			},
			{
				typeof(object),
				ExtendedClrTypeCode.Object
			},
			{
				typeof(byte[]),
				ExtendedClrTypeCode.ByteArray
			},
			{
				typeof(char[]),
				ExtendedClrTypeCode.CharArray
			},
			{
				typeof(Guid),
				ExtendedClrTypeCode.Guid
			},
			{
				typeof(SqlBinary),
				ExtendedClrTypeCode.SqlBinary
			},
			{
				typeof(SqlBoolean),
				ExtendedClrTypeCode.SqlBoolean
			},
			{
				typeof(SqlByte),
				ExtendedClrTypeCode.SqlByte
			},
			{
				typeof(SqlDateTime),
				ExtendedClrTypeCode.SqlDateTime
			},
			{
				typeof(SqlDouble),
				ExtendedClrTypeCode.SqlDouble
			},
			{
				typeof(SqlGuid),
				ExtendedClrTypeCode.SqlGuid
			},
			{
				typeof(SqlInt16),
				ExtendedClrTypeCode.SqlInt16
			},
			{
				typeof(SqlInt32),
				ExtendedClrTypeCode.SqlInt32
			},
			{
				typeof(SqlInt64),
				ExtendedClrTypeCode.SqlInt64
			},
			{
				typeof(SqlMoney),
				ExtendedClrTypeCode.SqlMoney
			},
			{
				typeof(SqlDecimal),
				ExtendedClrTypeCode.SqlDecimal
			},
			{
				typeof(SqlSingle),
				ExtendedClrTypeCode.SqlSingle
			},
			{
				typeof(SqlString),
				ExtendedClrTypeCode.SqlString
			},
			{
				typeof(SqlChars),
				ExtendedClrTypeCode.SqlChars
			},
			{
				typeof(SqlBytes),
				ExtendedClrTypeCode.SqlBytes
			},
			{
				typeof(SqlXml),
				ExtendedClrTypeCode.SqlXml
			},
			{
				typeof(DataTable),
				ExtendedClrTypeCode.DataTable
			},
			{
				typeof(DbDataReader),
				ExtendedClrTypeCode.DbDataReader
			},
			{
				typeof(IEnumerable<SqlDataRecord>),
				ExtendedClrTypeCode.IEnumerableOfSqlDataRecord
			},
			{
				typeof(TimeSpan),
				ExtendedClrTypeCode.TimeSpan
			},
			{
				typeof(DateTimeOffset),
				ExtendedClrTypeCode.DateTimeOffset
			}
		};
	}
}
