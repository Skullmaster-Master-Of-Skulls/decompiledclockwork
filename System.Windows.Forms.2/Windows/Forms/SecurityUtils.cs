using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000116 RID: 278
	internal static class SecurityUtils
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x000159FC File Offset: 0x00013BFC
		private static ReflectionPermission MemberAccessPermission
		{
			get
			{
				if (SecurityUtils.memberAccessPermission == null)
				{
					SecurityUtils.memberAccessPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);
				}
				return SecurityUtils.memberAccessPermission;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00015A1B File Offset: 0x00013C1B
		private static ReflectionPermission RestrictedMemberAccessPermission
		{
			get
			{
				if (SecurityUtils.restrictedMemberAccessPermission == null)
				{
					SecurityUtils.restrictedMemberAccessPermission = new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess);
				}
				return SecurityUtils.restrictedMemberAccessPermission;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00015A3C File Offset: 0x00013C3C
		private static void DemandReflectionAccess(Type type)
		{
			try
			{
				SecurityUtils.MemberAccessPermission.Demand();
			}
			catch (SecurityException)
			{
				SecurityUtils.DemandGrantSet(type.Assembly);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00015A74 File Offset: 0x00013C74
		[SecuritySafeCritical]
		private static void DemandGrantSet(Assembly assembly)
		{
			PermissionSet permissionSet = assembly.PermissionSet;
			permissionSet.AddPermission(SecurityUtils.RestrictedMemberAccessPermission);
			permissionSet.Demand();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00015A9C File Offset: 0x00013C9C
		private static bool HasReflectionPermission(Type type)
		{
			try
			{
				SecurityUtils.DemandReflectionAccess(type);
				return true;
			}
			catch (SecurityException)
			{
			}
			return false;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00015ACC File Offset: 0x00013CCC
		internal static object SecureCreateInstance(Type type)
		{
			return SecurityUtils.SecureCreateInstance(type, null, false);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00015AD8 File Offset: 0x00013CD8
		internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
			{
				allowNonPublic = false;
			}
			if (allowNonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return Activator.CreateInstance(type, bindingFlags, null, args, null);
		}

		// Token: 0x0400050A RID: 1290
		private static volatile ReflectionPermission memberAccessPermission;

		// Token: 0x0400050B RID: 1291
		private static volatile ReflectionPermission restrictedMemberAccessPermission;
	}
}
