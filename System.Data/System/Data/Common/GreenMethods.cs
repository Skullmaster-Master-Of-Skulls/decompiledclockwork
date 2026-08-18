using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x0200014B RID: 331
	internal static class GreenMethods
	{
		// Token: 0x06001545 RID: 5445 RVA: 0x00243668 File Offset: 0x00242A68
		internal static object SystemDataSqlClientSqlProviderServices_Instance()
		{
			if (GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo == null)
			{
				Type type = Type.GetType("System.Data.SqlClient.SqlProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
				if (type != null)
				{
					GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				}
			}
			return GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_GetValue();
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x002436A8 File Offset: 0x00242AA8
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static object SystemDataSqlClientSqlProviderServices_Instance_GetValue()
		{
			object result = null;
			if (GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo != null)
			{
				result = GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo.GetValue(null);
			}
			return result;
		}

		// Token: 0x04000C8F RID: 3215
		private const string ExtensionAssemblyRef = "System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C90 RID: 3216
		private const string SystemDataCommonDbProviderServices_TypeName = "System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C91 RID: 3217
		private const string SystemDataSqlClientSqlProviderServices_TypeName = "System.Data.SqlClient.SqlProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04000C92 RID: 3218
		internal static Type SystemDataCommonDbProviderServices_Type = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);

		// Token: 0x04000C93 RID: 3219
		private static FieldInfo SystemDataSqlClientSqlProviderServices_Instance_FieldInfo;
	}
}
