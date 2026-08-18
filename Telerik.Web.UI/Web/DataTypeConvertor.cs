using System;
using System.Globalization;

namespace Telerik.Web
{
	// Token: 0x02000146 RID: 326
	public class DataTypeConvertor
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		public virtual string ConvertDataEnumToString(DataType dataType)
		{
			string empty = string.Empty;
			switch (dataType)
			{
			case DataType.String:
				return "String";
			case DataType.Number:
				return "Number";
			case DataType.DateTime:
				return "DateTime";
			case DataType.Boolean:
				return "Boolean";
			case DataType.Null:
				return "String";
			case DataType.Other:
				return "String";
			}
			throw new Exception("Type cannot be converted");
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002E150 File Offset: 0x0002C350
		public virtual bool CanConvert(object obj)
		{
			return obj.GetType() == typeof(DateTime) || obj.GetType() == typeof(string) || obj.GetType() == typeof(short) || obj.GetType() == typeof(int) || obj.GetType() == typeof(long) || obj.GetType() == typeof(ushort) || obj.GetType() == typeof(uint) || obj.GetType() == typeof(ulong) || obj.GetType() == typeof(byte) || obj.GetType() == typeof(sbyte) || obj.GetType() == typeof(decimal) || obj.GetType() == typeof(double) || obj.GetType() == typeof(bool) || obj.GetType() == typeof(DBNull) || obj.GetType() == typeof(float);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002E2D4 File Offset: 0x0002C4D4
		public virtual string Convert(object obj)
		{
			string fullName = obj.GetType().FullName;
			string key;
			switch (key = fullName)
			{
			case "System.DateTime":
				return ((DateTime)obj).ToString("yyyy-MM-ddTHH:mm:sss.fff", CultureInfo.InvariantCulture);
			case "System.String":
				return obj.ToString().Trim().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
			case "System.Int16":
				return ((short)obj).ToString("0");
			case "System.Int32":
				return ((int)obj).ToString("0");
			case "System.Int64":
				return ((long)obj).ToString("0");
			case "System.UInt16":
				return ((ushort)obj).ToString("0");
			case "System.UInt32":
				return ((uint)obj).ToString("0");
			case "System.UInt64":
				return ((ulong)obj).ToString("0");
			case "System.Byte":
				return obj.ToString();
			case "System.SByte":
				return obj.ToString();
			case "System.Decimal":
				return ((decimal)obj).ToString(CultureInfo.InvariantCulture);
			case "System.Single":
				return ((float)obj).ToString(CultureInfo.InvariantCulture);
			case "System.Double":
				return ((double)obj).ToString(CultureInfo.InvariantCulture);
			case "System.DBNull":
				return string.Empty;
			case "System.Boolean":
				return ((bool)obj) ? "1" : "0";
			}
			return obj.ToString();
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002E598 File Offset: 0x0002C798
		public virtual DataType ConvertToDataType(object obj)
		{
			string fullName;
			switch (fullName = obj.GetType().FullName)
			{
			case "System.DateTime":
				return DataType.DateTime;
			case "System.String":
				return DataType.String;
			case "System.Int16":
			case "System.Int32":
			case "System.Int64":
			case "System.UInt16":
			case "System.UInt32":
			case "System.UInt64":
			case "System.Byte":
			case "System.SByte":
			case "System.Decimal":
			case "System.Double":
			case "System.Single":
				return DataType.Number;
			case "System.DBNull":
				return DataType.Null;
			case "System.Boolean":
				return DataType.Boolean;
			}
			return DataType.Other;
		}
	}
}
