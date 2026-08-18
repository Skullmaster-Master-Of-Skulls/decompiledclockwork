using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x020007A1 RID: 1953
	internal static class SecurityUtils
	{
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x001013B0 File Offset: 0x001003B0
		private static bool HasReflectionPermission
		{
			get
			{
				try
				{
					new ReflectionPermission(PermissionState.Unrestricted).Demand();
					return true;
				}
				catch (SecurityException)
				{
				}
				return false;
			}
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x001013E4 File Offset: 0x001003E4
		internal static object SecureCreateInstance(Type type)
		{
			return SecurityUtils.SecureCreateInstance(type, null);
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x001013F0 File Offset: 0x001003F0
		internal static object SecureCreateInstance(Type type, object[] args)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.Assembly == typeof(SecurityUtils).Assembly && !type.IsPublic && !type.IsNestedPublic)
			{
				new ReflectionPermission(PermissionState.Unrestricted).Demand();
			}
			return Activator.CreateInstance(type, args);
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x00101444 File Offset: 0x00100444
		internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (type.Assembly == typeof(SecurityUtils).Assembly)
			{
				if (!type.IsPublic && !type.IsNestedPublic)
				{
					new ReflectionPermission(PermissionState.Unrestricted).Demand();
				}
				else if (allowNonPublic && !SecurityUtils.HasReflectionPermission)
				{
					allowNonPublic = false;
				}
			}
			if (allowNonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return Activator.CreateInstance(type, bindingFlags, null, args, null);
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x001014B8 File Offset: 0x001004B8
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic)
		{
			return SecurityUtils.SecureConstructorInvoke(type, argTypes, args, allowNonPublic, BindingFlags.Default);
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x001014C4 File Offset: 0x001004C4
		internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic, BindingFlags extraFlags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | extraFlags;
			if (type.Assembly == typeof(SecurityUtils).Assembly)
			{
				if (!type.IsPublic && !type.IsNestedPublic)
				{
					new ReflectionPermission(PermissionState.Unrestricted).Demand();
				}
				else if (allowNonPublic && !SecurityUtils.HasReflectionPermission)
				{
					allowNonPublic = false;
				}
			}
			if (allowNonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, argTypes, null);
			if (constructor != null)
			{
				return constructor.Invoke(args);
			}
			return null;
		}
	}
}
