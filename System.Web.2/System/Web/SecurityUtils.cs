using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x02000015 RID: 21
	internal static class SecurityUtils
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000366C File Offset: 0x0000186C
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000368B File Offset: 0x0000188B
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

		// Token: 0x060000AC RID: 172 RVA: 0x000036AC File Offset: 0x000018AC
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

		// Token: 0x060000AD RID: 173 RVA: 0x000036E4 File Offset: 0x000018E4
		[SecuritySafeCritical]
		private static void DemandGrantSet(Assembly assembly)
		{
			PermissionSet permissionSet = assembly.PermissionSet;
			permissionSet.AddPermission(SecurityUtils.RestrictedMemberAccessPermission);
			permissionSet.Demand();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000370C File Offset: 0x0000190C
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

		// Token: 0x060000AF RID: 175 RVA: 0x0000373C File Offset: 0x0000193C
		internal static object SecureCreateInstance(Type type)
		{
			return SecurityUtils.SecureCreateInstance(type, null, false);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003748 File Offset: 0x00001948
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

		// Token: 0x060000B1 RID: 177 RVA: 0x0000379F File Offset: 0x0000199F
		internal static object SecureCreateInstance(Type type, object[] args)
		{
			return SecurityUtils.SecureCreateInstance(type, args, false);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000037A9 File Offset: 0x000019A9
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic)
		{
			return SecurityUtils.SecureConstructorInvoke(type, argTypes, args, allowNonPublic, BindingFlags.Default);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000037B8 File Offset: 0x000019B8
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic, BindingFlags extraFlags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
			{
				allowNonPublic = false;
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | extraFlags;
			if (!allowNonPublic)
			{
				bindingFlags &= ~BindingFlags.NonPublic;
			}
			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, argTypes, null);
			if (constructor != null)
			{
				return constructor.Invoke(args);
			}
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003824 File Offset: 0x00001A24
		private static bool GenericArgumentsAreVisible(MethodInfo method)
		{
			if (method.IsGenericMethod)
			{
				Type[] genericArguments = method.GetGenericArguments();
				foreach (Type type in genericArguments)
				{
					if (!type.IsVisible)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003860 File Offset: 0x00001A60
		internal static object FieldInfoGetValue(FieldInfo field, object target)
		{
			Type declaringType = field.DeclaringType;
			if (declaringType == null)
			{
				if (!field.IsPublic)
				{
					SecurityUtils.DemandGrantSet(field.Module.Assembly);
				}
			}
			else if (!(declaringType != null) || !declaringType.IsVisible || !field.IsPublic)
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return field.GetValue(target);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000038C0 File Offset: 0x00001AC0
		internal static object MethodInfoInvoke(MethodInfo method, object target, object[] args)
		{
			Type declaringType = method.DeclaringType;
			if (declaringType == null)
			{
				if (!method.IsPublic || !SecurityUtils.GenericArgumentsAreVisible(method))
				{
					SecurityUtils.DemandGrantSet(method.Module.Assembly);
				}
			}
			else if (!declaringType.IsVisible || !method.IsPublic || !SecurityUtils.GenericArgumentsAreVisible(method))
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return method.Invoke(target, args);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003928 File Offset: 0x00001B28
		internal static object ConstructorInfoInvoke(ConstructorInfo ctor, object[] args)
		{
			Type declaringType = ctor.DeclaringType;
			if (declaringType != null && (!declaringType.IsVisible || !ctor.IsPublic))
			{
				SecurityUtils.DemandReflectionAccess(declaringType);
			}
			return ctor.Invoke(args);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003962 File Offset: 0x00001B62
		internal static object ArrayCreateInstance(Type type, int length)
		{
			if (!type.IsVisible)
			{
				SecurityUtils.DemandReflectionAccess(type);
			}
			return Array.CreateInstance(type, length);
		}

		// Token: 0x0400006F RID: 111
		private static volatile ReflectionPermission memberAccessPermission;

		// Token: 0x04000070 RID: 112
		private static volatile ReflectionPermission restrictedMemberAccessPermission;
	}
}
