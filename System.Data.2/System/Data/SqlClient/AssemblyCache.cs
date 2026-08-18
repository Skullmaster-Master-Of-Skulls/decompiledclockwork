using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001A0 RID: 416
	internal sealed class AssemblyCache
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x000AC270 File Offset: 0x000AB670
		private AssemblyCache()
		{
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000AC284 File Offset: 0x000AB684
		internal static int GetLength(object inst)
		{
			return SerializationHelperSql9.SizeInBytes(inst);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000AC298 File Offset: 0x000AB698
		internal static SqlUdtInfo GetInfoFromType(Type t)
		{
			Type type = t;
			SqlUdtInfo sqlUdtInfo;
			for (;;)
			{
				sqlUdtInfo = SqlUdtInfo.TryGetFromType(t);
				if (sqlUdtInfo != null)
				{
					break;
				}
				t = t.BaseType;
				if (!(t != null))
				{
					goto Block_2;
				}
			}
			return sqlUdtInfo;
			Block_2:
			throw SQL.UDTInvalidSqlType(type.AssemblyQualifiedName);
		}
	}
}
