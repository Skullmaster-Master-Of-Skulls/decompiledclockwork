using System;
using System.Data;
using EncryptionClassLibrary;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000180 RID: 384
	public static class IDataReaderAdapter
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x00078FD8 File Offset: 0x000771D8
		public static bool ContainsColumn(this IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00079018 File Offset: 0x00077218
		public static string GetStringFromRecord(this IDataRecord record, string colName)
		{
			return (record[colName] is DBNull) ? string.Empty : ((string)record[colName]).Trim();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00079050 File Offset: 0x00077250
		public static string GetEncryptedStringFromRecord(this IDataRecord record, IBatchDecryptor batchDecryptor, string colName)
		{
			byte[] array = (record[colName] is DBNull) ? null : ((byte[])record[colName]);
			return (array == null) ? string.Empty : batchDecryptor.Decrypt(array);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00079094 File Offset: 0x00077294
		public static int GetIntFromRecord(this IDataRecord record, string colName, int defaultValue = 0)
		{
			return (record[colName] is DBNull) ? defaultValue : ((int)record[colName]);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000790C4 File Offset: 0x000772C4
		public static DateTime? GetDateTimeFromRecord(this IDataRecord record, string colName)
		{
			return (record[colName] is DBNull) ? null : new DateTime?((DateTime)record[colName]);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00079100 File Offset: 0x00077300
		public static DateTime GetDateTimeFromRecord(this IDataRecord record, string colName, DateTime defaultValue)
		{
			return (record[colName] is DBNull) ? defaultValue : ((DateTime)record[colName]);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00079130 File Offset: 0x00077330
		public static bool? GetBoolFromRecord(this IDataRecord record, string colName)
		{
			return (record[colName] is DBNull) ? null : new bool?(Convert.ToBoolean(record[colName]));
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0007916C File Offset: 0x0007736C
		public static bool GetBoolFromRecord(this IDataRecord record, string colName, bool defaultValue)
		{
			return (record[colName] is DBNull) ? defaultValue : Convert.ToBoolean(record[colName]);
		}
	}
}
