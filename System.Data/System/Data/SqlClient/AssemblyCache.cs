using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020002A2 RID: 674
	internal sealed class AssemblyCache
	{
		// Token: 0x060022A0 RID: 8864 RVA: 0x0028C618 File Offset: 0x0028BA18
		private AssemblyCache()
		{
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0028C638 File Offset: 0x0028BA38
		internal static int GetLength(object inst)
		{
			return SerializationHelperSql9.SizeInBytes(inst);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0028C658 File Offset: 0x0028BA58
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
				if (t == null)
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
