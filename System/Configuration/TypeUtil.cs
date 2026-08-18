using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200071E RID: 1822
	internal static class TypeUtil
	{
		// Token: 0x060037C7 RID: 14279 RVA: 0x000EC3A4 File Offset: 0x000EB3A4
		[ReflectionPermission(SecurityAction.Assert, Flags = (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess))]
		internal static object CreateInstanceWithReflectionPermission(string typeString)
		{
			Type type = Type.GetType(typeString, true);
			return Activator.CreateInstance(type, true);
		}
	}
}
