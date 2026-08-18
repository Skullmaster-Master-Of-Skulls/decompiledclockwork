using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001FF RID: 511
	internal class SqlUdtInfo
	{
		// Token: 0x06001FA1 RID: 8097 RVA: 0x000DA6B8 File Offset: 0x000D9AB8
		private SqlUdtInfo(SqlUserDefinedTypeAttribute attr)
		{
			this.SerializationFormat = attr.Format;
			this.IsByteOrdered = attr.IsByteOrdered;
			this.IsFixedLength = attr.IsFixedLength;
			this.MaxByteSize = attr.MaxByteSize;
			this.Name = attr.Name;
			this.ValidationMethodName = attr.ValidationMethodName;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000DA714 File Offset: 0x000D9B14
		internal static SqlUdtInfo GetFromType(Type target)
		{
			SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(target);
			if (sqlUdtInfo == null)
			{
				throw InvalidUdtException.Create(target, "SqlUdtReason_NoUdtAttribute");
			}
			return sqlUdtInfo;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000DA738 File Offset: 0x000D9B38
		internal static SqlUdtInfo TryGetFromType(Type target)
		{
			if (SqlUdtInfo.m_types2UdtInfo == null)
			{
				SqlUdtInfo.m_types2UdtInfo = new Dictionary<Type, SqlUdtInfo>();
			}
			SqlUdtInfo sqlUdtInfo = null;
			if (!SqlUdtInfo.m_types2UdtInfo.TryGetValue(target, out sqlUdtInfo))
			{
				object[] customAttributes = target.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					sqlUdtInfo = new SqlUdtInfo((SqlUserDefinedTypeAttribute)customAttributes[0]);
				}
				SqlUdtInfo.m_types2UdtInfo.Add(target, sqlUdtInfo);
			}
			return sqlUdtInfo;
		}

		// Token: 0x040011E8 RID: 4584
		internal readonly Format SerializationFormat;

		// Token: 0x040011E9 RID: 4585
		internal readonly bool IsByteOrdered;

		// Token: 0x040011EA RID: 4586
		internal readonly bool IsFixedLength;

		// Token: 0x040011EB RID: 4587
		internal readonly int MaxByteSize;

		// Token: 0x040011EC RID: 4588
		internal readonly string Name;

		// Token: 0x040011ED RID: 4589
		internal readonly string ValidationMethodName;

		// Token: 0x040011EE RID: 4590
		[ThreadStatic]
		private static Dictionary<Type, SqlUdtInfo> m_types2UdtInfo;
	}
}
