using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x02000330 RID: 816
	internal static class GreenMethods
	{
		// Token: 0x06003353 RID: 13139 RVA: 0x0013CD14 File Offset: 0x0013C114
		internal static object SystemDataSqlClientSqlProviderServices_Instance()
		{
			if (null == GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo)
			{
				Type type = Type.GetType("System.Data.SqlClient.SqlProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
				if (null != type)
				{
					GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
				}
			}
			return GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_GetValue();
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x0013CD5C File Offset: 0x0013C15C
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static object SystemDataSqlClientSqlProviderServices_Instance_GetValue()
		{
			object result = null;
			if (null != GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo)
			{
				result = GreenMethods.SystemDataSqlClientSqlProviderServices_Instance_FieldInfo.GetValue(null);
			}
			return result;
		}

		// Token: 0x04001E0A RID: 7690
		private const string ExtensionAssemblyRef = "System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E0B RID: 7691
		private const string SystemDataCommonDbProviderServices_TypeName = "System.Data.Common.DbProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E0C RID: 7692
		internal static Type SystemDataCommonDbProviderServices_Type = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);

		// Token: 0x04001E0D RID: 7693
		private const string SystemDataSqlClientSqlProviderServices_TypeName = "System.Data.SqlClient.SqlProviderServices, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E0E RID: 7694
		private static FieldInfo SystemDataSqlClientSqlProviderServices_Instance_FieldInfo;
	}
}
