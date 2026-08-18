using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000312 RID: 786
	internal class SqlUdtInfo
	{
		// Token: 0x06002904 RID: 10500 RVA: 0x002B3768 File Offset: 0x002B2B68
		private SqlUdtInfo(SqlUserDefinedTypeAttribute attr)
		{
			this.SerializationFormat = attr.Format;
			this.IsByteOrdered = attr.IsByteOrdered;
			this.IsFixedLength = attr.IsFixedLength;
			this.MaxByteSize = attr.MaxByteSize;
			this.Name = attr.Name;
			this.ValidationMethodName = attr.ValidationMethodName;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x002B37C8 File Offset: 0x002B2BC8
		internal static SqlUdtInfo GetFromType(Type target)
		{
			SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(target);
			if (sqlUdtInfo == null)
			{
				throw InvalidUdtException.Create(target, "SqlUdtReason_NoUdtAttribute");
			}
			return sqlUdtInfo;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x002B37F8 File Offset: 0x002B2BF8
		internal static SqlUdtInfo TryGetFromType(Type target)
		{
			SqlUdtInfo result = null;
			object[] customAttributes = target.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
			if (customAttributes != null && customAttributes.Length == 1)
			{
				result = new SqlUdtInfo((SqlUserDefinedTypeAttribute)customAttributes[0]);
			}
			return result;
		}

		// Token: 0x040019B1 RID: 6577
		internal readonly Format SerializationFormat;

		// Token: 0x040019B2 RID: 6578
		internal readonly bool IsByteOrdered;

		// Token: 0x040019B3 RID: 6579
		internal readonly bool IsFixedLength;

		// Token: 0x040019B4 RID: 6580
		internal readonly int MaxByteSize;

		// Token: 0x040019B5 RID: 6581
		internal readonly string Name;

		// Token: 0x040019B6 RID: 6582
		internal readonly string ValidationMethodName;
	}
}
