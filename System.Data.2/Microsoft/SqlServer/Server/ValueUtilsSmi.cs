using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000052 RID: 82
	internal static class ValueUtilsSmi
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0003E8F0 File Offset: 0x0003DCF0
		internal static bool IsDBNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			return ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0003E908 File Offset: 0x0003DD08
		internal static bool GetBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Boolean))
			{
				return ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (bool)value;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0003E94C File Offset: 0x0003DD4C
		internal static byte GetByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Byte))
			{
				return ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (byte)value;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0003E990 File Offset: 0x0003DD90
		private static long GetBytesConversion(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
			if (sqlValue == null)
			{
				throw ADP.InvalidCast();
			}
			SqlBinary sqlBinary = (SqlBinary)sqlValue;
			if (sqlBinary.IsNull)
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				return 0L;
			}
			else
			{
				if (buffer == null)
				{
					return (long)sqlBinary.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)sqlBinary.Length, fieldOffset, buffer.Length, bufferOffset, length);
				Array.Copy(sqlBinary.Value, checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0003EA1C File Offset: 0x0003DE1C
		internal static long GetBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiExtendedMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if ((-1L != metaData.MaxLength && (SqlDbType.VarChar == metaData.SqlDbType || SqlDbType.NVarChar == metaData.SqlDbType || SqlDbType.Char == metaData.SqlDbType || SqlDbType.NChar == metaData.SqlDbType)) || SqlDbType.Xml == metaData.SqlDbType)
			{
				throw SQL.NonBlobColumn(metaData.Name);
			}
			return ValueUtilsSmi.GetBytesInternal(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0003EA84 File Offset: 0x0003DE84
		internal static long GetBytesInternal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool throwOnNull)
		{
			if (!ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.ByteArray))
			{
				return ValueUtilsSmi.GetBytesConversion(sink, getters, ordinal, metaData, fieldOffset, buffer, bufferOffset, length, throwOnNull);
			}
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (throwOnNull)
				{
					throw SQL.SqlNullValue();
				}
				ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, 0L, fieldOffset, buffer.Length, bufferOffset, length);
				return 0L;
			}
			else
			{
				long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return bytesLength_Unchecked;
				}
				if (MetaDataUtilsSmi.IsCharOrXmlType(metaData.SqlDbType))
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				else
				{
					length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, bytesLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				}
				if (length > 0)
				{
					length = ValueUtilsSmi.GetBytes_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0003EB60 File Offset: 0x0003DF60
		internal static long GetChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.CharArray))
			{
				long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
				if (buffer == null)
				{
					return charsLength_Unchecked;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, charsLength_Unchecked, fieldOffset, buffer.Length, bufferOffset, length);
				if (length > 0)
				{
					length = ValueUtilsSmi.GetChars_Unchecked(sink, getters, ordinal, fieldOffset, buffer, bufferOffset, length);
				}
				return (long)length;
			}
			else
			{
				string text = (string)ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
				if (text == null)
				{
					throw ADP.InvalidCast();
				}
				if (buffer == null)
				{
					return (long)text.Length;
				}
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength * 2L, (long)text.Length, fieldOffset, buffer.Length, bufferOffset, length);
				text.CopyTo(checked((int)fieldOffset), buffer, bufferOffset, length);
				return (long)length;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0003EC28 File Offset: 0x0003E028
		internal static DateTime GetDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTime))
			{
				return ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTime)value;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0003EC6C File Offset: 0x0003E06C
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool gettersSupportKatmaiDateTime)
		{
			if (gettersSupportKatmaiDateTime)
			{
				return ValueUtilsSmi.GetDateTimeOffset(sink, (SmiTypedGetterSetter)getters, ordinal, metaData);
			}
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (DateTimeOffset)value;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0003ECB0 File Offset: 0x0003E0B0
		internal static DateTimeOffset GetDateTimeOffset(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.DateTimeOffset))
			{
				return ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
			}
			return (DateTimeOffset)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData, null);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0003ECE8 File Offset: 0x0003E0E8
		internal static decimal GetDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Decimal))
			{
				return ValueUtilsSmi.GetDecimal_PossiblyMoney(sink, getters, ordinal, metaData);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (decimal)value;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0003ED2C File Offset: 0x0003E12C
		internal static double GetDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Double))
			{
				return ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (double)value;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0003ED70 File Offset: 0x0003E170
		internal static Guid GetGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Guid))
			{
				return ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (Guid)value;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0003EDB4 File Offset: 0x0003E1B4
		internal static short GetInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int16))
			{
				return ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (short)value;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0003EDF8 File Offset: 0x0003E1F8
		internal static int GetInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int32))
			{
				return ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (int)value;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0003EE3C File Offset: 0x0003E23C
		internal static long GetInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Int64))
			{
				return ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (long)value;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0003EE80 File Offset: 0x0003E280
		internal static float GetSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Single))
			{
				return ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (float)value;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0003EEC4 File Offset: 0x0003E2C4
		internal static SqlBinary GetSqlBinary(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBinary))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBinary.Null;
				}
				return ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBinary)sqlValue;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0003EF10 File Offset: 0x0003E310
		internal static SqlBoolean GetSqlBoolean(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBoolean))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlBoolean)sqlValue;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0003EF60 File Offset: 0x0003E360
		internal static SqlByte GetSqlByte(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlByte))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					return SqlByte.Null;
				}
				return new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				return (SqlByte)sqlValue;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0003EFB0 File Offset: 0x0003E3B0
		internal static SqlBytes GetSqlBytes(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			SqlBytes result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlBytes))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlBytes.Null;
				}
				else
				{
					long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
					if (0L <= bytesLength_Unchecked && bytesLength_Unchecked < 8000L)
					{
						byte[] byteArray_Unchecked = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
						result = new SqlBytes(byteArray_Unchecked);
					}
					else
					{
						Stream stream = new SmiGettersStream(sink, getters, ordinal, metaData);
						stream = ValueUtilsSmi.CopyIntoNewSmiScratchStream(stream, sink, context);
						result = new SqlBytes(stream);
					}
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlBinary sqlBinary = (SqlBinary)sqlValue;
				if (sqlBinary.IsNull)
				{
					result = SqlBytes.Null;
				}
				else
				{
					result = new SqlBytes(sqlBinary.Value);
				}
			}
			return result;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0003F060 File Offset: 0x0003E460
		internal static SqlChars GetSqlChars(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			SqlChars result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlChars))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlChars.Null;
				}
				else
				{
					long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
					if (charsLength_Unchecked < 4000L || !InOutOfProcHelper.InProc)
					{
						char[] charArray_Unchecked = ValueUtilsSmi.GetCharArray_Unchecked(sink, getters, ordinal);
						result = new SqlChars(charArray_Unchecked);
					}
					else
					{
						Stream source = new SmiGettersStream(sink, getters, ordinal, metaData);
						SqlStreamChars s = ValueUtilsSmi.CopyIntoNewSmiScratchStreamChars(source, sink, context);
						result = new SqlChars(s);
					}
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, null);
				if (sqlXml_Unchecked.IsNull)
				{
					result = SqlChars.Null;
				}
				else
				{
					result = new SqlChars(sqlXml_Unchecked.Value.ToCharArray());
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				SqlString sqlString = (SqlString)sqlValue;
				if (sqlString.IsNull)
				{
					result = SqlChars.Null;
				}
				else
				{
					result = new SqlChars(sqlString.Value.ToCharArray());
				}
			}
			return result;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0003F158 File Offset: 0x0003E558
		internal static SqlDateTime GetSqlDateTime(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDateTime result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDateTime))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlDateTime.Null;
				}
				else
				{
					DateTime dateTime_Unchecked = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					result = new SqlDateTime(dateTime_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlDateTime)sqlValue;
			}
			return result;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0003F1B0 File Offset: 0x0003E5B0
		internal static SqlDecimal GetSqlDecimal(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDecimal result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDecimal))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlDecimal.Null;
				}
				else
				{
					result = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlDecimal)sqlValue;
			}
			return result;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0003F200 File Offset: 0x0003E600
		internal static SqlDouble GetSqlDouble(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlDouble result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlDouble))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlDouble.Null;
				}
				else
				{
					double double_Unchecked = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					result = new SqlDouble(double_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlDouble)sqlValue;
			}
			return result;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0003F258 File Offset: 0x0003E658
		internal static SqlGuid GetSqlGuid(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlGuid result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlGuid))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlGuid.Null;
				}
				else
				{
					Guid guid_Unchecked = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					result = new SqlGuid(guid_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlGuid)sqlValue;
			}
			return result;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0003F2B0 File Offset: 0x0003E6B0
		internal static SqlInt16 GetSqlInt16(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt16 result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt16))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlInt16.Null;
				}
				else
				{
					short int16_Unchecked = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					result = new SqlInt16(int16_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlInt16)sqlValue;
			}
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0003F308 File Offset: 0x0003E708
		internal static SqlInt32 GetSqlInt32(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt32 result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt32))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlInt32.Null;
				}
				else
				{
					int int32_Unchecked = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					result = new SqlInt32(int32_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlInt32)sqlValue;
			}
			return result;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0003F360 File Offset: 0x0003E760
		internal static SqlInt64 GetSqlInt64(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlInt64 result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlInt64))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlInt64.Null;
				}
				else
				{
					long int64_Unchecked = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					result = new SqlInt64(int64_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlInt64)sqlValue;
			}
			return result;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0003F3B8 File Offset: 0x0003E7B8
		internal static SqlMoney GetSqlMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlMoney result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlMoney))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlMoney.Null;
				}
				else
				{
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlMoney)sqlValue;
			}
			return result;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0003F408 File Offset: 0x0003E808
		internal static SqlSingle GetSqlSingle(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlSingle result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlSingle))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlSingle.Null;
				}
				else
				{
					float single_Unchecked = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					result = new SqlSingle(single_Unchecked);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlSingle)sqlValue;
			}
			return result;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0003F460 File Offset: 0x0003E860
		internal static SqlString GetSqlString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			SqlString result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlString))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlString.Null;
				}
				else
				{
					string string_Unchecked = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					result = new SqlString(string_Unchecked);
				}
			}
			else if (SqlDbType.Xml == metaData.SqlDbType)
			{
				SqlXml sqlXml_Unchecked = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, null);
				if (sqlXml_Unchecked.IsNull)
				{
					result = SqlString.Null;
				}
				else
				{
					result = new SqlString(sqlXml_Unchecked.Value);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlString)sqlValue;
			}
			return result;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0003F4EC File Offset: 0x0003E8EC
		internal static SqlXml GetSqlXml(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			SqlXml result;
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.SqlXml))
			{
				if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
				{
					result = SqlXml.Null;
				}
				else
				{
					result = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, context);
				}
			}
			else
			{
				object sqlValue = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, null);
				if (sqlValue == null)
				{
					throw ADP.InvalidCast();
				}
				result = (SqlXml)sqlValue;
			}
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0003F540 File Offset: 0x0003E940
		internal static string GetString(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.String))
			{
				return ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
			}
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (string)value;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0003F584 File Offset: 0x0003E984
		internal static SqlSequentialStreamSmi GetSequentialStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool bypassTypeCheck = false)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (!bypassTypeCheck && !ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Stream))
			{
				throw ADP.InvalidCast();
			}
			long bytesLength_Unchecked = ValueUtilsSmi.GetBytesLength_Unchecked(sink, getters, ordinal);
			return new SqlSequentialStreamSmi(sink, getters, ordinal, bytesLength_Unchecked);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0003F5C0 File Offset: 0x0003E9C0
		internal static SqlSequentialTextReaderSmi GetSequentialTextReader(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (!ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TextReader))
			{
				throw ADP.InvalidCast();
			}
			long charsLength_Unchecked = ValueUtilsSmi.GetCharsLength_Unchecked(sink, getters, ordinal);
			return new SqlSequentialTextReaderSmi(sink, getters, ordinal, charsLength_Unchecked);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0003F5F8 File Offset: 0x0003E9F8
		internal static Stream GetStream(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool bypassTypeCheck = false)
		{
			bool flag = ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
			if (!bypassTypeCheck)
			{
				if (!flag && metaData.SqlDbType == SqlDbType.Variant)
				{
					metaData = getters.GetVariantType(sink, ordinal);
				}
				if (metaData.SqlDbType != SqlDbType.Variant && !ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.Stream))
				{
					throw ADP.InvalidCast();
				}
			}
			byte[] buffer;
			if (flag)
			{
				buffer = new byte[0];
			}
			else
			{
				buffer = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
			}
			return new MemoryStream(buffer, false);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0003F660 File Offset: 0x0003EA60
		internal static TextReader GetTextReader(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			bool flag = ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal);
			if (!flag && metaData.SqlDbType == SqlDbType.Variant)
			{
				metaData = getters.GetVariantType(sink, ordinal);
			}
			if (metaData.SqlDbType != SqlDbType.Variant && !ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TextReader))
			{
				throw ADP.InvalidCast();
			}
			string s;
			if (flag)
			{
				s = string.Empty;
			}
			else
			{
				s = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
			}
			return new StringReader(s);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0003F6C4 File Offset: 0x0003EAC4
		internal static TimeSpan GetTimeSpan(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, bool gettersSupportKatmaiDateTime)
		{
			if (gettersSupportKatmaiDateTime)
			{
				return ValueUtilsSmi.GetTimeSpan(sink, (SmiTypedGetterSetter)getters, ordinal, metaData);
			}
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			object value = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, null);
			if (value == null)
			{
				throw ADP.InvalidCast();
			}
			return (TimeSpan)value;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0003F708 File Offset: 0x0003EB08
		internal static TimeSpan GetTimeSpan(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData)
		{
			ValueUtilsSmi.ThrowIfITypedGettersIsNull(sink, getters, ordinal);
			if (ValueUtilsSmi.CanAccessGetterDirectly(metaData, ExtendedClrTypeCode.TimeSpan))
			{
				return ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
			}
			return (TimeSpan)ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData, null);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0003F740 File Offset: 0x0003EB40
		internal static object GetValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			object result;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				result = DBNull.Value;
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						result = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						result = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						result = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, context);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetValue200(sink, getters, ordinal, metaData, context);
				}
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0003F7E8 File Offset: 0x0003EBE8
		internal static object GetValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			object result = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				result = DBNull.Value;
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					result = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Binary:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					result = ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Char:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.DateTime:
					result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Decimal:
					result = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Float:
					result = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Image:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					result = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Money:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.NChar:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NText:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NVarChar:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Real:
					result = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.UniqueIdentifier:
					result = ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallDateTime:
					result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallInt:
					result = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.SmallMoney:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
					break;
				case SqlDbType.Text:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Timestamp:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					result = ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarBinary:
					result = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					result = ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetValue(sink, getters, ordinal, metaData, context);
					break;
				case SqlDbType.Xml:
					result = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, context).Value;
					break;
				case SqlDbType.Udt:
					result = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0003FA60 File Offset: 0x0003EE60
		internal static object GetSqlValue200(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			object result;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (SqlDbType.Udt == metaData.SqlDbType)
				{
					result = ValueUtilsSmi.NullUdtInstance(metaData);
				}
				else
				{
					result = ValueUtilsSmi.__typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
				}
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						result = ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.Time:
						result = ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal);
						break;
					case SqlDbType.DateTimeOffset:
						result = ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal);
						break;
					default:
						result = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, context);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetSqlValue200(sink, getters, ordinal, metaData, context);
				}
			}
			return result;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0003FB24 File Offset: 0x0003EF24
		internal static object GetSqlValue(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context)
		{
			object result = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				if (SqlDbType.Udt == metaData.SqlDbType)
				{
					result = ValueUtilsSmi.NullUdtInstance(metaData);
				}
				else
				{
					result = ValueUtilsSmi.__typeSpecificNullForSqlValue[(int)metaData.SqlDbType];
				}
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					result = new SqlInt64(ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Binary:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					result = new SqlBoolean(ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Char:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.DateTime:
					result = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Decimal:
					result = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Float:
					result = new SqlDouble(ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Image:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					result = new SqlInt32(ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Money:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.NChar:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NText:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.NVarChar:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Real:
					result = new SqlSingle(ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.UniqueIdentifier:
					result = new SqlGuid(ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallDateTime:
					result = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallInt:
					result = new SqlInt16(ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallMoney:
					result = ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Text:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Timestamp:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					result = new SqlByte(ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.VarBinary:
					result = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.VarChar:
					result = new SqlString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					result = ValueUtilsSmi.GetSqlValue(sink, getters, ordinal, metaData, context);
					break;
				case SqlDbType.Xml:
					result = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, context);
					break;
				case SqlDbType.Udt:
					result = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0003FE20 File Offset: 0x0003F220
		internal static object NullUdtInstance(SmiMetaData metaData)
		{
			Type type = metaData.Type;
			return type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0003FE54 File Offset: 0x0003F254
		internal static SqlBuffer.StorageType SqlDbTypeToStorageType(SqlDbType dbType)
		{
			return ValueUtilsSmi.__dbTypeToStorageType[(int)dbType];
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0003FE6C File Offset: 0x0003F26C
		private static void GetNullOutputParameterSmi(SmiMetaData metaData, SqlBuffer targetBuffer, ref object result)
		{
			if (SqlDbType.Udt == metaData.SqlDbType)
			{
				result = ValueUtilsSmi.NullUdtInstance(metaData);
				return;
			}
			SqlBuffer.StorageType storageType = ValueUtilsSmi.SqlDbTypeToStorageType(metaData.SqlDbType);
			if (storageType == SqlBuffer.StorageType.Empty)
			{
				result = DBNull.Value;
				return;
			}
			if (SqlBuffer.StorageType.SqlBinary == storageType)
			{
				targetBuffer.SqlBinary = SqlBinary.Null;
				return;
			}
			if (SqlBuffer.StorageType.SqlGuid == storageType)
			{
				targetBuffer.SqlGuid = SqlGuid.Null;
				return;
			}
			targetBuffer.SetToNullOfType(storageType);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0003FECC File Offset: 0x0003F2CC
		internal static object GetOutputParameterV3Smi(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData, SmiContext context, SqlBuffer targetBuffer)
		{
			object result = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				ValueUtilsSmi.GetNullOutputParameterSmi(metaData, targetBuffer, ref result);
			}
			else
			{
				switch (metaData.SqlDbType)
				{
				case SqlDbType.BigInt:
					targetBuffer.Int64 = ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Binary:
				case SqlDbType.Image:
				case SqlDbType.Timestamp:
				case SqlDbType.VarBinary:
					targetBuffer.SqlBinary = ValueUtilsSmi.GetSqlBinary_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Bit:
					targetBuffer.Boolean = ValueUtilsSmi.GetBoolean_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Char:
				case SqlDbType.NChar:
				case SqlDbType.NText:
				case SqlDbType.NVarChar:
				case SqlDbType.Text:
				case SqlDbType.VarChar:
					targetBuffer.SetToString(ValueUtilsSmi.GetString_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.DateTime:
				case SqlDbType.SmallDateTime:
				{
					SqlDateTime sqlDateTime = new SqlDateTime(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
					targetBuffer.SetToDateTime(sqlDateTime.DayTicks, sqlDateTime.TimeTicks);
					break;
				}
				case SqlDbType.Decimal:
				{
					SqlDecimal sqlDecimal_Unchecked = ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal);
					targetBuffer.SetToDecimal(sqlDecimal_Unchecked.Precision, sqlDecimal_Unchecked.Scale, sqlDecimal_Unchecked.IsPositive, sqlDecimal_Unchecked.Data);
					break;
				}
				case SqlDbType.Float:
					targetBuffer.Double = ValueUtilsSmi.GetDouble_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Int:
					targetBuffer.Int32 = ValueUtilsSmi.GetInt32_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Money:
				case SqlDbType.SmallMoney:
					targetBuffer.SetToMoney(ValueUtilsSmi.GetInt64_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.Real:
					targetBuffer.Single = ValueUtilsSmi.GetSingle_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.UniqueIdentifier:
					targetBuffer.SqlGuid = new SqlGuid(ValueUtilsSmi.GetGuid_Unchecked(sink, getters, ordinal));
					break;
				case SqlDbType.SmallInt:
					targetBuffer.Int16 = ValueUtilsSmi.GetInt16_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.TinyInt:
					targetBuffer.Byte = ValueUtilsSmi.GetByte_Unchecked(sink, getters, ordinal);
					break;
				case SqlDbType.Variant:
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					ValueUtilsSmi.GetOutputParameterV3Smi(sink, getters, ordinal, metaData, context, targetBuffer);
					break;
				case SqlDbType.Xml:
					targetBuffer.SqlXml = ValueUtilsSmi.GetSqlXml_Unchecked(sink, getters, ordinal, null);
					break;
				case SqlDbType.Udt:
					result = ValueUtilsSmi.GetUdt_LengthChecked(sink, getters, ordinal, metaData);
					break;
				}
			}
			return result;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000400F0 File Offset: 0x0003F4F0
		internal static object GetOutputParameterV200Smi(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal, SmiMetaData metaData, SmiContext context, SqlBuffer targetBuffer)
		{
			object result = null;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				ValueUtilsSmi.GetNullOutputParameterSmi(metaData, targetBuffer, ref result);
			}
			else
			{
				SqlDbType sqlDbType = metaData.SqlDbType;
				if (sqlDbType != SqlDbType.Variant)
				{
					switch (sqlDbType)
					{
					case SqlDbType.Date:
						targetBuffer.SetToDate(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal));
						break;
					case SqlDbType.Time:
						targetBuffer.SetToTime(ValueUtilsSmi.GetTimeSpan_Unchecked(sink, getters, ordinal), metaData.Scale);
						break;
					case SqlDbType.DateTime2:
						targetBuffer.SetToDateTime2(ValueUtilsSmi.GetDateTime_Unchecked(sink, getters, ordinal), metaData.Scale);
						break;
					case SqlDbType.DateTimeOffset:
						targetBuffer.SetToDateTimeOffset(ValueUtilsSmi.GetDateTimeOffset_Unchecked(sink, getters, ordinal), metaData.Scale);
						break;
					default:
						result = ValueUtilsSmi.GetOutputParameterV3Smi(sink, getters, ordinal, metaData, context, targetBuffer);
						break;
					}
				}
				else
				{
					metaData = getters.GetVariantType(sink, ordinal);
					sink.ProcessMessagesAndThrow();
					ValueUtilsSmi.GetOutputParameterV200Smi(sink, getters, ordinal, metaData, context, targetBuffer);
				}
			}
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000401C8 File Offset: 0x0003F5C8
		internal static void SetDBNull(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000401E0 File Offset: 0x0003F5E0
		internal static void SetBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, bool value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Boolean);
			ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00040200 File Offset: 0x0003F600
		internal static void SetByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Byte);
			ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00040220 File Offset: 0x0003F620
		internal static long SetBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetBytes_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00040280 File Offset: 0x0003F680
		internal static long SetBytesLength(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.ByteArray);
			if (length < 0L)
			{
				throw ADP.InvalidDataLength(length);
			}
			if (metaData.MaxLength >= 0L && length > metaData.MaxLength)
			{
				length = metaData.MaxLength;
			}
			setters.SetBytesLength(sink, ordinal, length);
			sink.ProcessMessagesAndThrow();
			return length;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000402D4 File Offset: 0x0003F6D4
		internal static long SetChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.CharArray);
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, fieldOffset, buffer.Length, bufferOffset, length);
			if (length == 0)
			{
				fieldOffset = 0L;
				bufferOffset = 0;
			}
			return (long)ValueUtilsSmi.SetChars_Unchecked(sink, setters, ordinal, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00040334 File Offset: 0x0003F734
		internal static void SetDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTime);
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00040354 File Offset: 0x0003F754
		internal static void SetDateTimeOffset(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTimeOffset value, bool settersSupportKatmaiDateTime)
		{
			if (!settersSupportKatmaiDateTime)
			{
				throw ADP.InvalidCast();
			}
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.DateTimeOffset);
			ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, (SmiTypedGetterSetter)setters, ordinal, value);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00040384 File Offset: 0x0003F784
		internal static void SetDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Decimal);
			ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000403A4 File Offset: 0x0003F7A4
		internal static void SetDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, double value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Double);
			ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000403C4 File Offset: 0x0003F7C4
		internal static void SetGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, Guid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Guid);
			ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000403E4 File Offset: 0x0003F7E4
		internal static void SetInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, short value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int16);
			ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00040404 File Offset: 0x0003F804
		internal static void SetInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, int value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int32);
			ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00040424 File Offset: 0x0003F824
		internal static void SetInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, long value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Int64);
			ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00040444 File Offset: 0x0003F844
		internal static void SetSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, float value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.Single);
			ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00040464 File Offset: 0x0003F864
		internal static void SetSqlBinary(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBinary);
			ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00040488 File Offset: 0x0003F888
		internal static void SetSqlBoolean(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBoolean value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBoolean);
			ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000404A8 File Offset: 0x0003F8A8
		internal static void SetSqlByte(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlByte value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlByte);
			ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000404C8 File Offset: 0x0003F8C8
		internal static void SetSqlBytes(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlBytes);
			ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000404EC File Offset: 0x0003F8EC
		internal static void SetSqlChars(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlChars);
			ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00040510 File Offset: 0x0003F910
		internal static void SetSqlDateTime(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDateTime);
			ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00040530 File Offset: 0x0003F930
		internal static void SetSqlDecimal(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDecimal value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDecimal);
			ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00040550 File Offset: 0x0003F950
		internal static void SetSqlDouble(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDouble value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlDouble);
			ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00040570 File Offset: 0x0003F970
		internal static void SetSqlGuid(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlGuid value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlGuid);
			ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00040590 File Offset: 0x0003F990
		internal static void SetSqlInt16(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt16 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt16);
			ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000405B0 File Offset: 0x0003F9B0
		internal static void SetSqlInt32(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt32 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt32);
			ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000405D0 File Offset: 0x0003F9D0
		internal static void SetSqlInt64(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlInt64 value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlInt64);
			ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000405F0 File Offset: 0x0003F9F0
		internal static void SetSqlMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlMoney);
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00040610 File Offset: 0x0003FA10
		internal static void SetSqlSingle(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlSingle value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlSingle);
			ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00040630 File Offset: 0x0003FA30
		internal static void SetSqlString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlString);
			ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00040654 File Offset: 0x0003FA54
		internal static void SetSqlXml(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlXml value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.SqlXml);
			ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00040674 File Offset: 0x0003FA74
		internal static void SetString(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value)
		{
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.String);
			ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, value, 0);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00040698 File Offset: 0x0003FA98
		internal static void SetTimeSpan(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, TimeSpan value, bool settersSupportKatmaiDateTime)
		{
			if (!settersSupportKatmaiDateTime)
			{
				throw ADP.InvalidCast();
			}
			ValueUtilsSmi.ThrowIfInvalidSetterAccess(metaData, ExtendedClrTypeCode.TimeSpan);
			ValueUtilsSmi.SetTimeSpan_Checked(sink, (SmiTypedGetterSetter)setters, ordinal, metaData, value);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000406C8 File Offset: 0x0003FAC8
		internal static void SetCompatibleValue(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.Invalid:
				throw ADP.UnknownDataType(value.GetType());
			case ExtendedClrTypeCode.Boolean:
				ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, ordinal, (bool)value);
				return;
			case ExtendedClrTypeCode.Byte:
				ValueUtilsSmi.SetByte_Unchecked(sink, setters, ordinal, (byte)value);
				return;
			case ExtendedClrTypeCode.Char:
			{
				char[] value2 = new char[]
				{
					(char)value
				};
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, value2, ExtendedClrTypeCode.CharArray, 0);
				return;
			}
			case ExtendedClrTypeCode.DateTime:
				ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			case ExtendedClrTypeCode.DBNull:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Decimal:
				ValueUtilsSmi.SetDecimal_PossiblyMoney(sink, setters, ordinal, metaData, (decimal)value);
				return;
			case ExtendedClrTypeCode.Double:
				ValueUtilsSmi.SetDouble_Unchecked(sink, setters, ordinal, (double)value);
				return;
			case ExtendedClrTypeCode.Empty:
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			case ExtendedClrTypeCode.Int16:
				ValueUtilsSmi.SetInt16_Unchecked(sink, setters, ordinal, (short)value);
				return;
			case ExtendedClrTypeCode.Int32:
				ValueUtilsSmi.SetInt32_Unchecked(sink, setters, ordinal, (int)value);
				return;
			case ExtendedClrTypeCode.Int64:
				ValueUtilsSmi.SetInt64_Unchecked(sink, setters, ordinal, (long)value);
				return;
			case ExtendedClrTypeCode.SByte:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Single:
				ValueUtilsSmi.SetSingle_Unchecked(sink, setters, ordinal, (float)value);
				return;
			case ExtendedClrTypeCode.String:
				ValueUtilsSmi.SetString_LengthChecked(sink, setters, ordinal, metaData, (string)value, offset);
				return;
			case ExtendedClrTypeCode.UInt16:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt32:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.UInt64:
				throw ADP.InvalidCast();
			case ExtendedClrTypeCode.Object:
				ValueUtilsSmi.SetUdt_LengthChecked(sink, setters, ordinal, metaData, value);
				return;
			case ExtendedClrTypeCode.ByteArray:
				ValueUtilsSmi.SetByteArray_LengthChecked(sink, setters, ordinal, metaData, (byte[])value, offset);
				return;
			case ExtendedClrTypeCode.CharArray:
				ValueUtilsSmi.SetCharArray_LengthChecked(sink, setters, ordinal, metaData, (char[])value, offset);
				return;
			case ExtendedClrTypeCode.Guid:
				ValueUtilsSmi.SetGuid_Unchecked(sink, setters, ordinal, (Guid)value);
				return;
			case ExtendedClrTypeCode.SqlBinary:
				ValueUtilsSmi.SetSqlBinary_LengthChecked(sink, setters, ordinal, metaData, (SqlBinary)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBoolean:
				ValueUtilsSmi.SetSqlBoolean_Unchecked(sink, setters, ordinal, (SqlBoolean)value);
				return;
			case ExtendedClrTypeCode.SqlByte:
				ValueUtilsSmi.SetSqlByte_Unchecked(sink, setters, ordinal, (SqlByte)value);
				return;
			case ExtendedClrTypeCode.SqlDateTime:
				ValueUtilsSmi.SetSqlDateTime_Checked(sink, setters, ordinal, metaData, (SqlDateTime)value);
				return;
			case ExtendedClrTypeCode.SqlDouble:
				ValueUtilsSmi.SetSqlDouble_Unchecked(sink, setters, ordinal, (SqlDouble)value);
				return;
			case ExtendedClrTypeCode.SqlGuid:
				ValueUtilsSmi.SetSqlGuid_Unchecked(sink, setters, ordinal, (SqlGuid)value);
				return;
			case ExtendedClrTypeCode.SqlInt16:
				ValueUtilsSmi.SetSqlInt16_Unchecked(sink, setters, ordinal, (SqlInt16)value);
				return;
			case ExtendedClrTypeCode.SqlInt32:
				ValueUtilsSmi.SetSqlInt32_Unchecked(sink, setters, ordinal, (SqlInt32)value);
				return;
			case ExtendedClrTypeCode.SqlInt64:
				ValueUtilsSmi.SetSqlInt64_Unchecked(sink, setters, ordinal, (SqlInt64)value);
				return;
			case ExtendedClrTypeCode.SqlMoney:
				ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, (SqlMoney)value);
				return;
			case ExtendedClrTypeCode.SqlDecimal:
				ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, ordinal, (SqlDecimal)value);
				return;
			case ExtendedClrTypeCode.SqlSingle:
				ValueUtilsSmi.SetSqlSingle_Unchecked(sink, setters, ordinal, (SqlSingle)value);
				return;
			case ExtendedClrTypeCode.SqlString:
				ValueUtilsSmi.SetSqlString_LengthChecked(sink, setters, ordinal, metaData, (SqlString)value, offset);
				return;
			case ExtendedClrTypeCode.SqlChars:
				ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, ordinal, metaData, (SqlChars)value, offset);
				return;
			case ExtendedClrTypeCode.SqlBytes:
				ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, ordinal, metaData, (SqlBytes)value, offset);
				return;
			case ExtendedClrTypeCode.SqlXml:
				ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, ordinal, (SqlXml)value);
				return;
			case ExtendedClrTypeCode.DataTable:
			case ExtendedClrTypeCode.DbDataReader:
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
			case ExtendedClrTypeCode.TimeSpan:
			case ExtendedClrTypeCode.DateTimeOffset:
				break;
			case ExtendedClrTypeCode.Stream:
				ValueUtilsSmi.SetStream_Unchecked(sink, setters, ordinal, metaData, (StreamDataFeed)value);
				return;
			case ExtendedClrTypeCode.TextReader:
				ValueUtilsSmi.SetTextReader_Unchecked(sink, setters, ordinal, metaData, (TextDataFeed)value);
				return;
			case ExtendedClrTypeCode.XmlReader:
				ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, ((XmlDataFeed)value)._source);
				break;
			default:
				return;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00040A18 File Offset: 0x0003FE18
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, int length, ParameterPeekAheadValue peekAhead, SqlBuffer.StorageType storageType)
		{
			if (typeCode != ExtendedClrTypeCode.DateTime)
			{
				ValueUtilsSmi.SetCompatibleValueV200(sink, setters, ordinal, metaData, value, typeCode, offset, length, peekAhead);
				return;
			}
			if (storageType == SqlBuffer.StorageType.DateTime2)
			{
				ValueUtilsSmi.SetDateTime2_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			if (storageType == SqlBuffer.StorageType.Date)
			{
				ValueUtilsSmi.SetDate_Checked(sink, setters, ordinal, metaData, (DateTime)value);
				return;
			}
			ValueUtilsSmi.SetDateTime_Checked(sink, setters, ordinal, metaData, (DateTime)value);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00040A7C File Offset: 0x0003FE7C
		internal static void SetCompatibleValueV200(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, object value, ExtendedClrTypeCode typeCode, int offset, int length, ParameterPeekAheadValue peekAhead)
		{
			switch (typeCode)
			{
			case ExtendedClrTypeCode.DataTable:
				ValueUtilsSmi.SetDataTable_Unchecked(sink, setters, ordinal, metaData, (DataTable)value);
				return;
			case ExtendedClrTypeCode.DbDataReader:
				ValueUtilsSmi.SetDbDataReader_Unchecked(sink, setters, ordinal, metaData, (DbDataReader)value);
				return;
			case ExtendedClrTypeCode.IEnumerableOfSqlDataRecord:
				ValueUtilsSmi.SetIEnumerableOfSqlDataRecord_Unchecked(sink, setters, ordinal, metaData, (IEnumerable<SqlDataRecord>)value, peekAhead);
				return;
			case ExtendedClrTypeCode.TimeSpan:
				ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, ordinal, metaData, (TimeSpan)value);
				return;
			case ExtendedClrTypeCode.DateTimeOffset:
				ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, ordinal, (DateTimeOffset)value);
				return;
			default:
				ValueUtilsSmi.SetCompatibleValue(sink, setters, ordinal, metaData, value, typeCode, offset);
				return;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00040B10 File Offset: 0x0003FF10
		internal static void FillCompatibleITypedSettersFromReader(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataReader reader)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_2BE;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BE;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_2BE;
					case SqlDbType.Char:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BE;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_2BE;
					case SqlDbType.Decimal:
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, reader.GetSqlDecimal(i));
						goto IL_2BE;
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_2BE;
					case SqlDbType.Image:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BE;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_2BE;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_2BE;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BE;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_2BE;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_2BE;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_2BE;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_2BE;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], reader.GetSqlMoney(i));
						goto IL_2BE;
					case SqlDbType.Text:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BE;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BE;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_2BE;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BE;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetSqlChars_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlChars(i), 0);
						goto IL_2BE;
					case SqlDbType.Variant:
					{
						object sqlValue = reader.GetSqlValue(i);
						ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
						ValueUtilsSmi.SetCompatibleValue(sink, setters, i, metaData[i], sqlValue, typeCode, 0);
						goto IL_2BE;
					}
					case SqlDbType.Xml:
						ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, reader.GetSqlXml(i));
						goto IL_2BE;
					case SqlDbType.Udt:
						ValueUtilsSmi.SetSqlBytes_LengthChecked(sink, setters, i, metaData[i], reader.GetSqlBytes(i), 0);
						goto IL_2BE;
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_2BE:;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00040DE8 File Offset: 0x000401E8
		internal static void FillCompatibleSettersFromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, IList<SmiExtendedMetaData> metaData, DbDataReader reader)
		{
			for (int i = 0; i < metaData.Count; i++)
			{
				if (!reader.IsDBNull(i))
				{
					switch (metaData[i].SqlDbType)
					{
					case SqlDbType.BigInt:
						ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, reader.GetInt64(i));
						goto IL_425;
					case SqlDbType.Binary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Bit:
						ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, reader.GetBoolean(i));
						goto IL_425;
					case SqlDbType.Char:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.DateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_425;
					case SqlDbType.Decimal:
					{
						SqlDataReader sqlDataReader = reader as SqlDataReader;
						if (sqlDataReader != null)
						{
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, sqlDataReader.GetSqlDecimal(i));
							goto IL_425;
						}
						ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, new SqlDecimal(reader.GetDecimal(i)));
						goto IL_425;
					}
					case SqlDbType.Float:
						ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, reader.GetDouble(i));
						goto IL_425;
					case SqlDbType.Image:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Int:
						ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, reader.GetInt32(i));
						goto IL_425;
					case SqlDbType.Money:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_425;
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Real:
						ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, reader.GetFloat(i));
						goto IL_425;
					case SqlDbType.UniqueIdentifier:
						ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, reader.GetGuid(i));
						goto IL_425;
					case SqlDbType.SmallDateTime:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_425;
					case SqlDbType.SmallInt:
						ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, reader.GetInt16(i));
						goto IL_425;
					case SqlDbType.SmallMoney:
						ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], new SqlMoney(reader.GetDecimal(i)));
						goto IL_425;
					case SqlDbType.Text:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Timestamp:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.TinyInt:
						ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, reader.GetByte(i));
						goto IL_425;
					case SqlDbType.VarBinary:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.VarChar:
						ValueUtilsSmi.SetCharsOrString_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Variant:
					{
						SqlDataReader sqlDataReader2 = reader as SqlDataReader;
						SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
						object value;
						if (sqlDataReader2 != null)
						{
							value = sqlDataReader2.GetSqlValue(i);
							storageType = sqlDataReader2.GetVariantInternalStorageType(i);
						}
						else
						{
							value = reader.GetValue(i);
						}
						ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(metaData[i].SqlDbType, metaData[i].IsMultiValued, value, null, 210UL);
						if (storageType == SqlBuffer.StorageType.DateTime2 || storageType == SqlBuffer.StorageType.Date)
						{
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], value, typeCode, 0, 0, null, storageType);
							goto IL_425;
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], value, typeCode, 0, 0, null);
						goto IL_425;
					}
					case SqlDbType.Xml:
					{
						SqlDataReader sqlDataReader3 = reader as SqlDataReader;
						if (sqlDataReader3 != null)
						{
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, sqlDataReader3.GetSqlXml(i));
							goto IL_425;
						}
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					}
					case SqlDbType.Udt:
						ValueUtilsSmi.SetBytes_FromReader(sink, setters, i, metaData[i], reader, 0);
						goto IL_425;
					case SqlDbType.Date:
					case SqlDbType.DateTime2:
						ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], reader.GetDateTime(i));
						goto IL_425;
					case SqlDbType.Time:
					{
						SqlDataReader sqlDataReader4 = reader as SqlDataReader;
						TimeSpan value2;
						if (sqlDataReader4 != null)
						{
							value2 = sqlDataReader4.GetTimeSpan(i);
						}
						else
						{
							value2 = (TimeSpan)reader.GetValue(i);
						}
						ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], value2);
						goto IL_425;
					}
					case SqlDbType.DateTimeOffset:
					{
						SqlDataReader sqlDataReader5 = reader as SqlDataReader;
						DateTimeOffset value3;
						if (sqlDataReader5 != null)
						{
							value3 = sqlDataReader5.GetDateTimeOffset(i);
						}
						else
						{
							value3 = (DateTimeOffset)reader.GetValue(i);
						}
						ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, value3);
						goto IL_425;
					}
					}
					throw ADP.NotSupported();
				}
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				IL_425:;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0004122C File Offset: 0x0004062C
		internal static void FillCompatibleITypedSettersFromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataRecord record)
		{
			ValueUtilsSmi.FillCompatibleITypedSettersFromRecord(sink, setters, metaData, record, null);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00041244 File Offset: 0x00040644
		internal static void FillCompatibleITypedSettersFromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, SmiMetaData[] metaData, SqlDataRecord record, SmiDefaultFieldsProperty useDefaultValues)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (useDefaultValues == null || !useDefaultValues[i])
				{
					if (!record.IsDBNull(i))
					{
						switch (metaData[i].SqlDbType)
						{
						case SqlDbType.BigInt:
							ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, record.GetInt64(i));
							goto IL_296;
						case SqlDbType.Binary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.Bit:
							ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, record.GetBoolean(i));
							goto IL_296;
						case SqlDbType.Char:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.DateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_296;
						case SqlDbType.Decimal:
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, record.GetSqlDecimal(i));
							goto IL_296;
						case SqlDbType.Float:
							ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, record.GetDouble(i));
							goto IL_296;
						case SqlDbType.Image:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.Int:
							ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, record.GetInt32(i));
							goto IL_296;
						case SqlDbType.Money:
							ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_296;
						case SqlDbType.NChar:
						case SqlDbType.NText:
						case SqlDbType.NVarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.Real:
							ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, record.GetFloat(i));
							goto IL_296;
						case SqlDbType.UniqueIdentifier:
							ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, record.GetGuid(i));
							goto IL_296;
						case SqlDbType.SmallDateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_296;
						case SqlDbType.SmallInt:
							ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, record.GetInt16(i));
							goto IL_296;
						case SqlDbType.SmallMoney:
							ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_296;
						case SqlDbType.Text:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.Timestamp:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.TinyInt:
							ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, record.GetByte(i));
							goto IL_296;
						case SqlDbType.VarBinary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.VarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						case SqlDbType.Variant:
						{
							object sqlValue = record.GetSqlValue(i);
							ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
							ValueUtilsSmi.SetCompatibleValue(sink, setters, i, metaData[i], sqlValue, typeCode, 0);
							goto IL_296;
						}
						case SqlDbType.Xml:
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, record.GetSqlXml(i));
							goto IL_296;
						case SqlDbType.Udt:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_296;
						}
						throw ADP.NotSupported();
					}
					ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				}
				IL_296:;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000414F4 File Offset: 0x000408F4
		internal static void FillCompatibleSettersFromRecord(SmiEventSink_Default sink, SmiTypedGetterSetter setters, SmiMetaData[] metaData, SqlDataRecord record, SmiDefaultFieldsProperty useDefaultValues)
		{
			for (int i = 0; i < metaData.Length; i++)
			{
				if (useDefaultValues == null || !useDefaultValues[i])
				{
					if (!record.IsDBNull(i))
					{
						switch (metaData[i].SqlDbType)
						{
						case SqlDbType.BigInt:
							ValueUtilsSmi.SetInt64_Unchecked(sink, setters, i, record.GetInt64(i));
							goto IL_32A;
						case SqlDbType.Binary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.Bit:
							ValueUtilsSmi.SetBoolean_Unchecked(sink, setters, i, record.GetBoolean(i));
							goto IL_32A;
						case SqlDbType.Char:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.DateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_32A;
						case SqlDbType.Decimal:
							ValueUtilsSmi.SetSqlDecimal_Unchecked(sink, setters, i, record.GetSqlDecimal(i));
							goto IL_32A;
						case SqlDbType.Float:
							ValueUtilsSmi.SetDouble_Unchecked(sink, setters, i, record.GetDouble(i));
							goto IL_32A;
						case SqlDbType.Image:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.Int:
							ValueUtilsSmi.SetInt32_Unchecked(sink, setters, i, record.GetInt32(i));
							goto IL_32A;
						case SqlDbType.Money:
							ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_32A;
						case SqlDbType.NChar:
						case SqlDbType.NText:
						case SqlDbType.NVarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.Real:
							ValueUtilsSmi.SetSingle_Unchecked(sink, setters, i, record.GetFloat(i));
							goto IL_32A;
						case SqlDbType.UniqueIdentifier:
							ValueUtilsSmi.SetGuid_Unchecked(sink, setters, i, record.GetGuid(i));
							goto IL_32A;
						case SqlDbType.SmallDateTime:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_32A;
						case SqlDbType.SmallInt:
							ValueUtilsSmi.SetInt16_Unchecked(sink, setters, i, record.GetInt16(i));
							goto IL_32A;
						case SqlDbType.SmallMoney:
							ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, i, metaData[i], record.GetSqlMoney(i));
							goto IL_32A;
						case SqlDbType.Text:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.Timestamp:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.TinyInt:
							ValueUtilsSmi.SetByte_Unchecked(sink, setters, i, record.GetByte(i));
							goto IL_32A;
						case SqlDbType.VarBinary:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.VarChar:
							ValueUtilsSmi.SetChars_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.Variant:
						{
							object sqlValue = record.GetSqlValue(i);
							ExtendedClrTypeCode typeCode = MetaDataUtilsSmi.DetermineExtendedTypeCode(sqlValue);
							ValueUtilsSmi.SetCompatibleValueV200(sink, setters, i, metaData[i], sqlValue, typeCode, 0, -1, null);
							goto IL_32A;
						}
						case SqlDbType.Xml:
							ValueUtilsSmi.SetSqlXml_Unchecked(sink, setters, i, record.GetSqlXml(i));
							goto IL_32A;
						case SqlDbType.Udt:
							ValueUtilsSmi.SetBytes_FromRecord(sink, setters, i, metaData[i], record, 0);
							goto IL_32A;
						case SqlDbType.Date:
						case SqlDbType.DateTime2:
							ValueUtilsSmi.SetDateTime_Checked(sink, setters, i, metaData[i], record.GetDateTime(i));
							goto IL_32A;
						case SqlDbType.Time:
						{
							TimeSpan value;
							if (record != null)
							{
								value = record.GetTimeSpan(i);
							}
							else
							{
								value = (TimeSpan)record.GetValue(i);
							}
							ValueUtilsSmi.SetTimeSpan_Checked(sink, setters, i, metaData[i], value);
							goto IL_32A;
						}
						case SqlDbType.DateTimeOffset:
						{
							DateTimeOffset value2;
							if (record != null)
							{
								value2 = record.GetDateTimeOffset(i);
							}
							else
							{
								value2 = (DateTimeOffset)record.GetValue(i);
							}
							ValueUtilsSmi.SetDateTimeOffset_Unchecked(sink, setters, i, value2);
							goto IL_32A;
						}
						}
						throw ADP.NotSupported();
					}
					ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, i);
				}
				IL_32A:;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00041838 File Offset: 0x00040C38
		internal static Stream CopyIntoNewSmiScratchStream(Stream source, SmiEventSink_Default sink, SmiContext context)
		{
			Stream stream;
			if (context == null)
			{
				stream = new MemoryStream();
			}
			else
			{
				stream = new SqlClientWrapperSmiStream(sink, context.GetScratchStream(sink));
			}
			int num;
			if (source.CanSeek && 8000L > source.Length)
			{
				num = (int)source.Length;
			}
			else
			{
				num = 8000;
			}
			byte[] buffer = new byte[num];
			int count;
			while ((count = source.Read(buffer, 0, num)) != 0)
			{
				stream.Write(buffer, 0, count);
			}
			stream.Flush();
			stream.Seek(0L, SeekOrigin.Begin);
			return stream;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000418B4 File Offset: 0x00040CB4
		internal static SqlStreamChars CopyIntoNewSmiScratchStreamChars(Stream source, SmiEventSink_Default sink, SmiContext context)
		{
			SqlClientWrapperSmiStreamChars sqlClientWrapperSmiStreamChars = new SqlClientWrapperSmiStreamChars(sink, context.GetScratchStream(sink));
			int num;
			if (source.CanSeek && 8000L > source.Length)
			{
				num = (int)source.Length;
			}
			else
			{
				num = 8000;
			}
			byte[] buffer = new byte[num];
			int count;
			while ((count = source.Read(buffer, 0, num)) != 0)
			{
				sqlClientWrapperSmiStreamChars.Write(buffer, 0, count);
			}
			sqlClientWrapperSmiStreamChars.Flush();
			sqlClientWrapperSmiStreamChars.Seek(0L, SeekOrigin.Begin);
			return sqlClientWrapperSmiStreamChars;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00041928 File Offset: 0x00040D28
		private static object GetUdt_LengthChecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			object result;
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				Type type = metaData.Type;
				result = type.InvokeMember("Null", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, new object[0], CultureInfo.InvariantCulture);
			}
			else
			{
				Stream s = new SmiGettersStream(sink, getters, ordinal, metaData);
				result = SerializationHelperSql9.Deserialize(s, metaData.Type);
			}
			return result;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00041980 File Offset: 0x00040D80
		private static decimal GetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiMetaData metaData)
		{
			if (SqlDbType.Decimal == metaData.SqlDbType)
			{
				return ValueUtilsSmi.GetSqlDecimal_Unchecked(sink, getters, ordinal).Value;
			}
			return ValueUtilsSmi.GetSqlMoney_Unchecked(sink, getters, ordinal).Value;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000419B8 File Offset: 0x00040DB8
		private static void SetDecimal_PossiblyMoney(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, decimal value)
		{
			if (SqlDbType.Decimal == metaData.SqlDbType || SqlDbType.Variant == metaData.SqlDbType)
			{
				ValueUtilsSmi.SetDecimal_Unchecked(sink, setters, ordinal, value);
				return;
			}
			ValueUtilsSmi.SetSqlMoney_Checked(sink, setters, ordinal, metaData, new SqlMoney(value));
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000419F4 File Offset: 0x00040DF4
		private static void VerifyDateTimeRange(SqlDbType dbType, DateTime value)
		{
			if (SqlDbType.SmallDateTime == dbType && (ValueUtilsSmi.x_dtSmallMax < value || ValueUtilsSmi.x_dtSmallMin > value))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00041A28 File Offset: 0x00040E28
		private static void VerifyTimeRange(SqlDbType dbType, TimeSpan value)
		{
			if (SqlDbType.Time == dbType && (ValueUtilsSmi.x_timeMin > value || value > ValueUtilsSmi.x_timeMax))
			{
				throw ADP.InvalidMetaDataValue();
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00041A5C File Offset: 0x00040E5C
		private static void SetDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime_Unchecked(sink, setters, ordinal, (SqlDbType.Date == metaData.SqlDbType) ? value.Date : value);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00041A94 File Offset: 0x00040E94
		private static void SetTimeSpan_Checked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, TimeSpan value)
		{
			ValueUtilsSmi.VerifyTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetTimeSpan_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00041AB8 File Offset: 0x00040EB8
		private static void SetSqlDateTime_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDateTime value)
		{
			if (!value.IsNull)
			{
				ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value.Value);
			}
			ValueUtilsSmi.SetSqlDateTime_Unchecked(sink, setters, ordinal, value);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00041AEC File Offset: 0x00040EEC
		private static void SetDateTime2_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDateTime2_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00041B14 File Offset: 0x00040F14
		private static void SetDate_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			ValueUtilsSmi.VerifyDateTimeRange(metaData.SqlDbType, value);
			ValueUtilsSmi.SetDate_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00041B3C File Offset: 0x00040F3C
		private static void SetSqlMoney_Checked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (!value.IsNull && SqlDbType.SmallMoney == metaData.SqlDbType)
			{
				decimal value2 = value.Value;
				if (TdsEnums.SQL_SMALL_MONEY_MIN > value2 || TdsEnums.SQL_SMALL_MONEY_MAX < value2)
				{
					throw SQL.MoneyOverflow(value2.ToString(CultureInfo.InvariantCulture));
				}
			}
			ValueUtilsSmi.SetSqlMoney_Unchecked(sink, setters, ordinal, metaData, value);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00041B9C File Offset: 0x00040F9C
		private static void SetByteArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, byte[] buffer, int offset)
		{
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, buffer, offset, length);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00041BDC File Offset: 0x00040FDC
		private static void SetCharArray_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, char[] buffer, int offset)
		{
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, buffer.Length, offset, buffer.Length - offset);
			ValueUtilsSmi.SetCharArray_Unchecked(sink, setters, ordinal, buffer, offset, length);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00041C1C File Offset: 0x0004101C
		private static void SetSqlBinary_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBinary value, int offset)
		{
			int length = 0;
			if (!value.IsNull)
			{
				length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, value.Length - offset);
			}
			ValueUtilsSmi.SetSqlBinary_Unchecked(sink, setters, ordinal, value, offset, length);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00041C6C File Offset: 0x0004106C
		private static void SetBytes_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetBytes(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num));
			int num3;
			if (num2 > 8000 || num2 < 0)
			{
				num3 = 8000;
			}
			else
			{
				num3 = num2;
			}
			byte[] buffer = new byte[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long bytes;
			while ((num2 < 0 || num6 < (long)num2) && (bytes = record.GetBytes(ordinal, num5, buffer, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetBytes(sink, ordinal, num5, buffer, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetBytesLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00041D30 File Offset: 0x00041130
		private static void SetBytes_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2 = 8000;
			byte[] buffer = new byte[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long bytes;
			while ((num < 0 || num5 < (long)num) && (bytes = reader.GetBytes(ordinal, num4, buffer, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetBytes(sink, ordinal, num4, buffer, 0, checked((int)bytes));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetBytesLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00041DC4 File Offset: 0x000411C4
		private static void SetSqlBytes_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlBytes value, int offset)
		{
			int num = 0;
			if (!value.IsNull)
			{
				long num2 = value.Length;
				if (num2 > 2147483647L)
				{
					num2 = -1L;
				}
				num = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num2, offset, (int)num2));
			}
			ValueUtilsSmi.SetSqlBytes_Unchecked(sink, setters, ordinal, value, 0, (long)num);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00041E1C File Offset: 0x0004121C
		private static void SetChars_FromRecord(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlDataRecord record, int offset)
		{
			long num = record.GetChars(ordinal, 0L, null, 0, 0);
			if (num > 2147483647L)
			{
				num = -1L;
			}
			int num2 = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num - offset));
			int num3;
			if (num2 > 4000 || num2 < 0)
			{
				if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
				{
					num3 = 8000;
				}
				else
				{
					num3 = 4000;
				}
			}
			else
			{
				num3 = num2;
			}
			char[] buffer = new char[num3];
			long num4 = 1L;
			long num5 = (long)offset;
			long num6 = 0L;
			long chars;
			while ((num2 < 0 || num6 < (long)num2) && (chars = record.GetChars(ordinal, num5, buffer, 0, num3)) != 0L && num4 != 0L)
			{
				num4 = (long)setters.SetChars(sink, ordinal, num5, buffer, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num5 += num4;
					num6 += num4;
				}
			}
			setters.SetCharsLength(sink, ordinal, num5);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00041EF8 File Offset: 0x000412F8
		private static void SetCharsOrString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			bool flag = false;
			try
			{
				ValueUtilsSmi.SetChars_FromReader(sink, setters, ordinal, metaData, reader, offset);
				flag = true;
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
			}
			if (!flag)
			{
				ValueUtilsSmi.SetString_FromReader(sink, setters, ordinal, metaData, reader, offset);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00041F54 File Offset: 0x00041354
		private static void SetChars_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			int num = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, -1, offset, -1);
			int num2;
			if (MetaDataUtilsSmi.IsAnsiType(metaData.SqlDbType))
			{
				num2 = 8000;
			}
			else
			{
				num2 = 4000;
			}
			char[] buffer = new char[num2];
			long num3 = 1L;
			long num4 = (long)offset;
			long num5 = 0L;
			long chars;
			while ((num < 0 || num5 < (long)num) && (chars = reader.GetChars(ordinal, num4, buffer, 0, num2)) != 0L && num3 != 0L)
			{
				num3 = (long)setters.SetChars(sink, ordinal, num4, buffer, 0, checked((int)chars));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num4 += num3;
					num5 += num3;
				}
			}
			setters.SetCharsLength(sink, ordinal, num4);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00041FFC File Offset: 0x000413FC
		private static void SetString_FromReader(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader reader, int offset)
		{
			string @string = reader.GetString(ordinal);
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, (long)@string.Length, 0L, -1, offset, -1);
			setters.SetString(sink, ordinal, @string, offset, length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00042044 File Offset: 0x00041444
		private static void SetSqlChars_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlChars value, int offset)
		{
			int length = 0;
			if (!value.IsNull)
			{
				long num = value.Length;
				if (num > 2147483647L)
				{
					num = -1L;
				}
				length = checked(ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, (int)num, offset, (int)num - offset));
			}
			ValueUtilsSmi.SetSqlChars_Unchecked(sink, setters, ordinal, value, 0, length);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0004209C File Offset: 0x0004149C
		private static void SetSqlString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset)
		{
			if (value.IsNull)
			{
				ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, ordinal);
				return;
			}
			string value2 = value.Value;
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value2.Length, offset, value2.Length - offset);
			ValueUtilsSmi.SetSqlString_Unchecked(sink, setters, ordinal, metaData, value, offset, length);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000420F8 File Offset: 0x000414F8
		private static void SetString_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, string value, int offset)
		{
			int length = ValueUtilsSmi.CheckXetParameters(metaData.SqlDbType, metaData.MaxLength, -1L, 0L, value.Length, offset, checked(value.Length - offset));
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value, offset, length);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0004213C File Offset: 0x0004153C
		private static void SetUdt_LengthChecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, object value)
		{
			if (ADP.IsNull(value))
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			Stream s = new SmiSettersStream(sink, setters, ordinal, metaData);
			SerializationHelperSql9.Serialize(s, value);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00042174 File Offset: 0x00041574
		private static void ThrowIfInvalidSetterAccess(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			if (!ValueUtilsSmi.CanAccessSetterDirectly(metaData, setterTypeCode))
			{
				throw ADP.InvalidCast();
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00042190 File Offset: 0x00041590
		private static void ThrowIfITypedGettersIsNull(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			if (ValueUtilsSmi.IsDBNull_Unchecked(sink, getters, ordinal))
			{
				throw SQL.SqlNullValue();
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000421B0 File Offset: 0x000415B0
		private static bool CanAccessGetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.__canAccessGetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (ExtendedClrTypeCode.DataTable == setterTypeCode || ExtendedClrTypeCode.DbDataReader == setterTypeCode || ExtendedClrTypeCode.IEnumerableOfSqlDataRecord == setterTypeCode))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000421EC File Offset: 0x000415EC
		private static bool CanAccessSetterDirectly(SmiMetaData metaData, ExtendedClrTypeCode setterTypeCode)
		{
			bool flag = ValueUtilsSmi.__canAccessSetterDirectly[(int)setterTypeCode, (int)metaData.SqlDbType];
			if (flag && (ExtendedClrTypeCode.DataTable == setterTypeCode || ExtendedClrTypeCode.DbDataReader == setterTypeCode || ExtendedClrTypeCode.IEnumerableOfSqlDataRecord == setterTypeCode))
			{
				flag = metaData.IsMultiValued;
			}
			return flag;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00042228 File Offset: 0x00041628
		private static long PositiveMin(long first, long second)
		{
			if (first < 0L)
			{
				return second;
			}
			if (second < 0L)
			{
				return first;
			}
			return Math.Min(first, second);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0004224C File Offset: 0x0004164C
		private static int CheckXetParameters(SqlDbType dbType, long maxLength, long actualLength, long fieldOffset, int bufferLength, int bufferOffset, int length)
		{
			if (0L > fieldOffset)
			{
				throw ADP.NegativeParameter("fieldOffset");
			}
			if (bufferOffset < 0)
			{
				throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
			}
			checked
			{
				if (bufferLength < 0)
				{
					length = (int)ValueUtilsSmi.PositiveMin(unchecked((long)length), ValueUtilsSmi.PositiveMin(maxLength, actualLength));
					if (length < -1)
					{
						length = -1;
					}
					return length;
				}
				if (bufferOffset > bufferLength)
				{
					throw ADP.InvalidDestinationBufferIndex(bufferLength, bufferOffset, "bufferOffset");
				}
				if (length + bufferOffset > bufferLength)
				{
					throw ADP.InvalidBufferSizeOrIndex(length, bufferOffset);
				}
			}
			if (length < 0)
			{
				throw ADP.InvalidDataLength((long)length);
			}
			if (0L <= actualLength && actualLength <= fieldOffset)
			{
				return 0;
			}
			length = Math.Min(length, bufferLength - bufferOffset);
			if (SqlDbType.Variant == dbType)
			{
				length = Math.Min(length, 8000);
			}
			if (0L <= actualLength)
			{
				length = (int)Math.Min((long)length, actualLength - fieldOffset);
			}
			else if (SqlDbType.Udt != dbType && 0L <= maxLength)
			{
				length = (int)Math.Min((long)length, maxLength - fieldOffset);
			}
			if (length < 0)
			{
				return 0;
			}
			return length;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00042338 File Offset: 0x00041738
		private static bool IsDBNull_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool result = getters.IsDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00042358 File Offset: 0x00041758
		private static bool GetBoolean_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			bool boolean = getters.GetBoolean(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return boolean;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00042378 File Offset: 0x00041778
		private static byte GetByte_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			byte @byte = getters.GetByte(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @byte;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00042398 File Offset: 0x00041798
		private static byte[] GetByteArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long bytesLength = getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = checked((int)bytesLength);
			byte[] array = new byte[num];
			getters.GetBytes(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000423D4 File Offset: 0x000417D4
		internal static int GetBytes_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int bytes = getters.GetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return bytes;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000423F8 File Offset: 0x000417F8
		private static long GetBytesLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long bytesLength = getters.GetBytesLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return bytesLength;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00042418 File Offset: 0x00041818
		private static char[] GetCharArray_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long charsLength = getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			int num = checked((int)charsLength);
			char[] array = new char[num];
			getters.GetChars(sink, ordinal, 0L, array, 0, num);
			sink.ProcessMessagesAndThrow();
			return array;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00042454 File Offset: 0x00041854
		internal static int GetChars_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int chars = getters.GetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return chars;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00042478 File Offset: 0x00041878
		private static long GetCharsLength_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long charsLength = getters.GetCharsLength(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return charsLength;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00042498 File Offset: 0x00041898
		private static DateTime GetDateTime_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			DateTime dateTime = getters.GetDateTime(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTime;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000424B8 File Offset: 0x000418B8
		private static DateTimeOffset GetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			DateTimeOffset dateTimeOffset = getters.GetDateTimeOffset(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return dateTimeOffset;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000424D8 File Offset: 0x000418D8
		private static double GetDouble_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			double @double = getters.GetDouble(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @double;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000424F8 File Offset: 0x000418F8
		private static Guid GetGuid_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			Guid guid = getters.GetGuid(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return guid;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00042518 File Offset: 0x00041918
		private static short GetInt16_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			short @int = getters.GetInt16(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00042538 File Offset: 0x00041938
		private static int GetInt32_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			int @int = getters.GetInt32(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00042558 File Offset: 0x00041958
		private static long GetInt64_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @int;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00042578 File Offset: 0x00041978
		private static float GetSingle_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			float single = getters.GetSingle(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return single;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00042598 File Offset: 0x00041998
		private static SqlBinary GetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			byte[] byteArray_Unchecked = ValueUtilsSmi.GetByteArray_Unchecked(sink, getters, ordinal);
			return new SqlBinary(byteArray_Unchecked);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000425B4 File Offset: 0x000419B4
		private static SqlDecimal GetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			SqlDecimal sqlDecimal = getters.GetSqlDecimal(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return sqlDecimal;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000425D4 File Offset: 0x000419D4
		private static SqlMoney GetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			long @int = getters.GetInt64(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return new SqlMoney(@int, 1);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000425F8 File Offset: 0x000419F8
		private static SqlXml GetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal, SmiContext context)
		{
			if (context == null && InOutOfProcHelper.InProc)
			{
				context = SmiContextFactory.Instance.GetCurrentContext();
			}
			Stream source = new SmiGettersStream(sink, getters, ordinal, SmiMetaData.DefaultXml);
			Stream value = ValueUtilsSmi.CopyIntoNewSmiScratchStream(source, sink, context);
			return new SqlXml(value);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0004263C File Offset: 0x00041A3C
		private static string GetString_Unchecked(SmiEventSink_Default sink, ITypedGettersV3 getters, int ordinal)
		{
			string @string = getters.GetString(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return @string;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0004265C File Offset: 0x00041A5C
		private static TimeSpan GetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter getters, int ordinal)
		{
			TimeSpan timeSpan = getters.GetTimeSpan(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			return timeSpan;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0004267C File Offset: 0x00041A7C
		private static void SetBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, bool value)
		{
			setters.SetBoolean(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00042698 File Offset: 0x00041A98
		private static void SetByteArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetBytes(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetBytesLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000426D4 File Offset: 0x00041AD4
		private static void SetStream_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, StreamDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			byte[] buffer = new byte[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(buffer, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetBytes(sink, ordinal, (long)num, buffer, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetBytesLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00042758 File Offset: 0x00041B58
		private static void SetTextReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metadata, TextDataFeed feed)
		{
			long maxLength = metadata.MaxLength;
			char[] buffer = new char[4096];
			int num = 0;
			do
			{
				int num2 = 4096;
				if (maxLength > 0L && (long)(num + num2) > maxLength)
				{
					num2 = (int)(maxLength - (long)num);
				}
				int num3 = feed._source.Read(buffer, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				setters.SetChars(sink, ordinal, (long)num, buffer, 0, num3);
				sink.ProcessMessagesAndThrow();
				num += num3;
			}
			while (maxLength <= 0L || (long)num < maxLength);
			setters.SetCharsLength(sink, ordinal, (long)num);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000427DC File Offset: 0x00041BDC
		private static void SetByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, byte value)
		{
			setters.SetByte(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000427F8 File Offset: 0x00041BF8
		private static int SetBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int result = setters.SetBytes(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0004281C File Offset: 0x00041C1C
		private static void SetCharArray_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, char[] buffer, int bufferOffset, int length)
		{
			if (length > 0)
			{
				setters.SetChars(sink, ordinal, 0L, buffer, bufferOffset, length);
				sink.ProcessMessagesAndThrow();
			}
			setters.SetCharsLength(sink, ordinal, (long)length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00042858 File Offset: 0x00041C58
		private static int SetChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int result = setters.SetChars(sink, ordinal, fieldOffset, buffer, bufferOffset, length);
			sink.ProcessMessagesAndThrow();
			return result;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0004287C File Offset: 0x00041C7C
		private static void SetDBNull_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal)
		{
			setters.SetDBNull(sink, ordinal);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00042898 File Offset: 0x00041C98
		private static void SetDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, decimal value)
		{
			setters.SetSqlDecimal(sink, ordinal, new SqlDecimal(value));
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000428BC File Offset: 0x00041CBC
		private static void SetDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, DateTime value)
		{
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000428D8 File Offset: 0x00041CD8
		private static void SetDateTime2_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDateTime2);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00042904 File Offset: 0x00041D04
		private static void SetDate_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, DateTime value)
		{
			setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultDate);
			setters.SetDateTime(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00042930 File Offset: 0x00041D30
		private static void SetTimeSpan_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, TimeSpan value)
		{
			setters.SetTimeSpan(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0004294C File Offset: 0x00041D4C
		private static void SetDateTimeOffset_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, DateTimeOffset value)
		{
			setters.SetDateTimeOffset(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00042968 File Offset: 0x00041D68
		private static void SetDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, double value)
		{
			setters.SetDouble(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00042984 File Offset: 0x00041D84
		private static void SetGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, Guid value)
		{
			setters.SetGuid(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000429A0 File Offset: 0x00041DA0
		private static void SetInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, short value)
		{
			setters.SetInt16(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000429BC File Offset: 0x00041DBC
		private static void SetInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, int value)
		{
			setters.SetInt32(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000429D8 File Offset: 0x00041DD8
		private static void SetInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, long value)
		{
			setters.SetInt64(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000429F4 File Offset: 0x00041DF4
		private static void SetSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, float value)
		{
			setters.SetSingle(sink, ordinal, value);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00042A10 File Offset: 0x00041E10
		private static void SetSqlBinary_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBinary value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				ValueUtilsSmi.SetByteArray_Unchecked(sink, setters, ordinal, value.Value, offset, length);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00042A4C File Offset: 0x00041E4C
		private static void SetSqlBoolean_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBoolean value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetBoolean(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00042A84 File Offset: 0x00041E84
		private static void SetSqlByte_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlByte value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetByte(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00042ABC File Offset: 0x00041EBC
		private static void SetSqlBytes_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlBytes value, int offset, long length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 8000L || length < 0L)
			{
				num = 8000;
			}
			else
			{
				num = checked((int)length);
			}
			byte[] buffer = new byte[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0L || num4 < length) && (num5 = value.Read(num3, buffer, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetBytes(sink, ordinal, num3, buffer, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetBytesLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00042B5C File Offset: 0x00041F5C
		private static void SetSqlChars_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlChars value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			int num;
			if (length > 4000 || length < 0)
			{
				num = 4000;
			}
			else
			{
				num = length;
			}
			char[] buffer = new char[num];
			long num2 = 1L;
			long num3 = (long)offset;
			long num4 = 0L;
			long num5;
			while ((length < 0 || num4 < (long)length) && (num5 = value.Read(num3, buffer, 0, num)) != 0L && num2 != 0L)
			{
				num2 = (long)setters.SetChars(sink, ordinal, num3, buffer, 0, checked((int)num5));
				sink.ProcessMessagesAndThrow();
				checked
				{
					num3 += num2;
					num4 += num2;
				}
			}
			setters.SetCharsLength(sink, ordinal, num3);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00042BFC File Offset: 0x00041FFC
		private static void SetSqlDateTime_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDateTime value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDateTime(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00042C34 File Offset: 0x00042034
		private static void SetSqlDecimal_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDecimal value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSqlDecimal(sink, ordinal, value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00042C64 File Offset: 0x00042064
		private static void SetSqlDouble_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlDouble value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetDouble(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00042C9C File Offset: 0x0004209C
		private static void SetSqlGuid_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlGuid value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetGuid(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00042CD4 File Offset: 0x000420D4
		private static void SetSqlInt16_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt16 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt16(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00042D0C File Offset: 0x0004210C
		private static void SetSqlInt32_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt32 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt32(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00042D44 File Offset: 0x00042144
		private static void SetSqlInt64_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlInt64 value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetInt64(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00042D7C File Offset: 0x0004217C
		private static void SetSqlMoney_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlMoney value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				if (SqlDbType.Variant == metaData.SqlDbType)
				{
					setters.SetVariantMetaData(sink, ordinal, SmiMetaData.DefaultMoney);
					sink.ProcessMessagesAndThrow();
				}
				setters.SetInt64(sink, ordinal, value.ToSqlInternalRepresentation());
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00042DD0 File Offset: 0x000421D0
		private static void SetSqlSingle_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlSingle value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
			}
			else
			{
				setters.SetSingle(sink, ordinal, value.Value);
			}
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00042E08 File Offset: 0x00042208
		private static void SetSqlString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SmiMetaData metaData, SqlString value, int offset, int length)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			if (SqlDbType.Variant == metaData.SqlDbType)
			{
				metaData = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, (long)value.LCID, value.SqlCompareOptions, null);
				setters.SetVariantMetaData(sink, ordinal, metaData);
				sink.ProcessMessagesAndThrow();
			}
			ValueUtilsSmi.SetString_Unchecked(sink, setters, ordinal, value.Value, offset, length);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00042E7C File Offset: 0x0004227C
		private static void SetSqlXml_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, SqlXml value)
		{
			if (value.IsNull)
			{
				setters.SetDBNull(sink, ordinal);
				sink.ProcessMessagesAndThrow();
				return;
			}
			ValueUtilsSmi.SetXmlReader_Unchecked(sink, setters, ordinal, value.CreateReader());
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00042EB0 File Offset: 0x000422B0
		private static void SetXmlReader_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, XmlReader xmlReader)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CloseOutput = false;
			xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
			xmlWriterSettings.Encoding = Encoding.Unicode;
			xmlWriterSettings.OmitXmlDeclaration = true;
			Stream output = new SmiSettersStream(sink, setters, ordinal, SmiMetaData.DefaultXml);
			XmlWriter xmlWriter = XmlWriter.Create(output, xmlWriterSettings);
			xmlReader.Read();
			while (!xmlReader.EOF)
			{
				xmlWriter.WriteNode(xmlReader, true);
			}
			xmlWriter.Flush();
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00042F20 File Offset: 0x00042320
		private static void SetString_Unchecked(SmiEventSink_Default sink, ITypedSettersV3 setters, int ordinal, string value, int offset, int length)
		{
			setters.SetString(sink, ordinal, value, offset, length);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00042F40 File Offset: 0x00042340
		private static void SetDataTable_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DataTable value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			ExtendedClrTypeCode[] array = new ExtendedClrTypeCode[metaData.FieldMetaData.Count];
			for (int i = 0; i < metaData.FieldMetaData.Count; i++)
			{
				array[i] = ExtendedClrTypeCode.Invalid;
			}
			foreach (object obj in value.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				for (int j = 0; j < metaData.FieldMetaData.Count; j++)
				{
					SmiMetaData smiMetaData = metaData.FieldMetaData[j];
					if (dataRow.IsNull(j))
					{
						ValueUtilsSmi.SetDBNull_Unchecked(sink, setters, j);
					}
					else
					{
						object value2 = dataRow[j];
						if (ExtendedClrTypeCode.Invalid == array[j])
						{
							array[j] = MetaDataUtilsSmi.DetermineExtendedTypeCodeForUseWithSqlDbType(smiMetaData.SqlDbType, smiMetaData.IsMultiValued, value2, smiMetaData.Type, 210UL);
						}
						ValueUtilsSmi.SetCompatibleValueV200(sink, setters, j, smiMetaData, value2, array[j], 0, -1, null);
					}
				}
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0004307C File Offset: 0x0004247C
		private static void SetDbDataReader_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, DbDataReader value)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			while (value.Read())
			{
				setters.NewElement(sink);
				sink.ProcessMessagesAndThrow();
				ValueUtilsSmi.FillCompatibleSettersFromReader(sink, setters, metaData.FieldMetaData, value);
			}
			setters.EndElements(sink);
			sink.ProcessMessagesAndThrow();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000430D0 File Offset: 0x000424D0
		private static void SetIEnumerableOfSqlDataRecord_Unchecked(SmiEventSink_Default sink, SmiTypedGetterSetter setters, int ordinal, SmiMetaData metaData, IEnumerable<SqlDataRecord> value, ParameterPeekAheadValue peekAhead)
		{
			setters = setters.GetTypedGetterSetter(sink, ordinal);
			sink.ProcessMessagesAndThrow();
			IEnumerator<SqlDataRecord> enumerator = null;
			try
			{
				SmiExtendedMetaData[] array = new SmiExtendedMetaData[metaData.FieldMetaData.Count];
				metaData.FieldMetaData.CopyTo(array, 0);
				SmiDefaultFieldsProperty useDefaultValues = (SmiDefaultFieldsProperty)metaData.ExtendedProperties[SmiPropertySelector.DefaultFields];
				int num = 1;
				if (peekAhead != null && peekAhead.FirstRecord != null)
				{
					enumerator = peekAhead.Enumerator;
					setters.NewElement(sink);
					sink.ProcessMessagesAndThrow();
					SmiTypedGetterSetter setters2 = setters;
					SmiMetaData[] metaData2 = array;
					ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, setters2, metaData2, peekAhead.FirstRecord, useDefaultValues);
					num++;
				}
				else
				{
					enumerator = value.GetEnumerator();
				}
				using (enumerator)
				{
					while (enumerator.MoveNext())
					{
						setters.NewElement(sink);
						sink.ProcessMessagesAndThrow();
						SqlDataRecord sqlDataRecord = enumerator.Current;
						if (sqlDataRecord.FieldCount != array.Length)
						{
							throw SQL.EnumeratedRecordFieldCountChanged(num);
						}
						for (int i = 0; i < sqlDataRecord.FieldCount; i++)
						{
							if (!MetaDataUtilsSmi.IsCompatible(metaData.FieldMetaData[i], sqlDataRecord.GetSqlMetaData(i)))
							{
								throw SQL.EnumeratedRecordMetaDataChanged(sqlDataRecord.GetName(i), num);
							}
						}
						SmiTypedGetterSetter setters3 = setters;
						SmiMetaData[] metaData2 = array;
						ValueUtilsSmi.FillCompatibleSettersFromRecord(sink, setters3, metaData2, sqlDataRecord, useDefaultValues);
						num++;
					}
					setters.EndElements(sink);
					sink.ProcessMessagesAndThrow();
				}
			}
			finally
			{
				IDisposable disposable = enumerator;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x04000190 RID: 400
		private const int __maxByteChunkSize = 8000;

		// Token: 0x04000191 RID: 401
		private const int __maxCharChunkSize = 4000;

		// Token: 0x04000192 RID: 402
		private const int NoLengthLimit = -1;

		// Token: 0x04000193 RID: 403
		private const int constBinBufferSize = 4096;

		// Token: 0x04000194 RID: 404
		private const int constTextBufferSize = 4096;

		// Token: 0x04000195 RID: 405
		private static object[] __typeSpecificNullForSqlValue = new object[]
		{
			SqlInt64.Null,
			SqlBinary.Null,
			SqlBoolean.Null,
			SqlString.Null,
			SqlDateTime.Null,
			SqlDecimal.Null,
			SqlDouble.Null,
			SqlBinary.Null,
			SqlInt32.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlString.Null,
			SqlString.Null,
			SqlSingle.Null,
			SqlGuid.Null,
			SqlDateTime.Null,
			SqlInt16.Null,
			SqlMoney.Null,
			SqlString.Null,
			SqlBinary.Null,
			SqlByte.Null,
			SqlBinary.Null,
			SqlString.Null,
			DBNull.Value,
			null,
			SqlXml.Null,
			null,
			null,
			null,
			null,
			null,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		// Token: 0x04000196 RID: 406
		private static SqlBuffer.StorageType[] __dbTypeToStorageType = new SqlBuffer.StorageType[]
		{
			SqlBuffer.StorageType.Int64,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.Boolean,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.DateTime,
			SqlBuffer.StorageType.Decimal,
			SqlBuffer.StorageType.Double,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.Int32,
			SqlBuffer.StorageType.Money,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.Single,
			SqlBuffer.StorageType.SqlGuid,
			SqlBuffer.StorageType.DateTime,
			SqlBuffer.StorageType.Int16,
			SqlBuffer.StorageType.Money,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.Byte,
			SqlBuffer.StorageType.SqlBinary,
			SqlBuffer.StorageType.String,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.SqlXml,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Empty,
			SqlBuffer.StorageType.Date,
			SqlBuffer.StorageType.Time,
			SqlBuffer.StorageType.DateTime2,
			SqlBuffer.StorageType.DateTimeOffset
		};

		// Token: 0x04000197 RID: 407
		private static readonly DateTime x_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04000198 RID: 408
		private static readonly DateTime x_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04000199 RID: 409
		private static readonly TimeSpan x_timeMin = TimeSpan.Zero;

		// Token: 0x0400019A RID: 410
		private static readonly TimeSpan x_timeMax = new TimeSpan(863999999999L);

		// Token: 0x0400019B RID: 411
		private const bool X = true;

		// Token: 0x0400019C RID: 412
		private const bool _ = false;

		// Token: 0x0400019D RID: 413
		private static bool[,] __canAccessGetterDirectly = new bool[,]
		{
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				false,
				true,
				true,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			}
		};

		// Token: 0x0400019E RID: 414
		private static bool[,] __canAccessSetterDirectly = new bool[,]
		{
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				true,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				true,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				true
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			},
			{
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false,
				false
			}
		};
	}
}
