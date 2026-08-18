using System;
using System.CodeDom.Compiler;

namespace System.Data.Entity.SqlServer.Resources
{
	// Token: 0x02000015 RID: 21
	[GeneratedCode("Resources.SqlServer.tt", "1.0.0.0")]
	internal static class Error
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004AEB File Offset: 0x00002CEB
		internal static Exception InvalidDatabaseName(object p0)
		{
			return new ArgumentException(Strings.InvalidDatabaseName(p0));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004AF8 File Offset: 0x00002CF8
		internal static Exception SqlServerMigrationSqlGenerator_UnknownOperation(object p0, object p1)
		{
			return new InvalidOperationException(Strings.SqlServerMigrationSqlGenerator_UnknownOperation(p0, p1));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004B06 File Offset: 0x00002D06
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004B0E File Offset: 0x00002D0E
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004B15 File Offset: 0x00002D15
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
