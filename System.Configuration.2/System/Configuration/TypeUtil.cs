using System;
using System.Configuration.Internal;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Web;

namespace System.Configuration
{
	// Token: 0x0200009C RID: 156
	internal static class TypeUtil
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0001D064 File Offset: 0x0001B264
		private static Type GetLegacyType(string typeString)
		{
			Type result = null;
			try
			{
				Assembly assembly = typeof(ConfigurationException).Assembly;
				result = assembly.GetType(typeString, false);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		private static Type GetTypeImpl(string typeString, bool throwOnError)
		{
			Type type = null;
			Exception ex = null;
			try
			{
				type = Type.GetType(typeString, throwOnError);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (type == null)
			{
				type = TypeUtil.GetLegacyType(typeString);
				if (type == null && ex != null)
				{
					throw ex;
				}
			}
			return type;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
		internal static Type GetTypeWithReflectionPermission(IInternalConfigHost host, string typeString, bool throwOnError)
		{
			Type type = null;
			Exception ex = null;
			try
			{
				type = host.GetConfigType(typeString, throwOnError);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (type == null)
			{
				type = TypeUtil.GetLegacyType(typeString);
				if (type == null && ex != null)
				{
					throw ex;
				}
			}
			return type;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001D144 File Offset: 0x0001B344
		internal static Type GetTypeWithReflectionPermission(string typeString, bool throwOnError)
		{
			return TypeUtil.GetTypeImpl(typeString, throwOnError);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001D14D File Offset: 0x0001B34D
		internal static T CreateInstance<T>(string typeString)
		{
			return TypeUtil.CreateInstanceRestricted<T>(null, typeString);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001D158 File Offset: 0x0001B358
		internal static T CreateInstanceRestricted<T>(Type callingType, string typeString)
		{
			Type typeImpl = TypeUtil.GetTypeImpl(typeString, true);
			TypeUtil.VerifyAssignableType(typeof(T), typeImpl, true);
			return (T)((object)TypeUtil.CreateInstanceRestricted(callingType, typeImpl));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001D18C File Offset: 0x0001B38C
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static object CreateInstanceWithReflectionPermission(Type type)
		{
			return Activator.CreateInstance(type, true);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
		internal static object CreateInstanceRestricted(Type callingType, Type targetType)
		{
			if (TypeUtil.CallerHasMemberAccessOrAspNetPermission())
			{
				return TypeUtil.CreateInstanceWithReflectionPermission(targetType);
			}
			DynamicMethod dynamicMethod = TypeUtil.CreateDynamicMethod(callingType, typeof(object), new Type[]
			{
				typeof(Type)
			});
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldc_I4_1);
			ilgenerator.Emit(OpCodes.Call, typeof(Activator).GetMethod("CreateInstance", new Type[]
			{
				typeof(Type),
				typeof(bool)
			}));
			TypeUtil.PreventTailCall(ilgenerator);
			ilgenerator.Emit(OpCodes.Ret);
			Func<Type, object> func = (Func<Type, object>)dynamicMethod.CreateDelegate(typeof(Func<Type, object>));
			return func(targetType);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001D270 File Offset: 0x0001B470
		internal static Delegate CreateDelegateRestricted(Type callingType, Type delegateType, MethodInfo targetMethod)
		{
			if (TypeUtil.CallerHasMemberAccessOrAspNetPermission())
			{
				return Delegate.CreateDelegate(delegateType, targetMethod);
			}
			DynamicMethod dynamicMethod = TypeUtil.CreateDynamicMethod(callingType, typeof(Delegate), new Type[]
			{
				typeof(Type),
				typeof(MethodInfo)
			});
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Call, typeof(Delegate).GetMethod("CreateDelegate", new Type[]
			{
				typeof(Type),
				typeof(MethodInfo)
			}));
			TypeUtil.PreventTailCall(ilgenerator);
			ilgenerator.Emit(OpCodes.Ret);
			Func<Type, MethodInfo, Delegate> func = (Func<Type, MethodInfo, Delegate>)dynamicMethod.CreateDelegate(typeof(Func<Type, MethodInfo, Delegate>));
			return func(delegateType, targetMethod);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001D348 File Offset: 0x0001B548
		private static DynamicMethod CreateDynamicMethod(Type owner, Type returnType, Type[] parameterTypes)
		{
			if (owner != null)
			{
				return TypeUtil.CreateDynamicMethodWithUnrestrictedPermission(owner, returnType, parameterTypes);
			}
			return new DynamicMethod("temp-dynamic-method", returnType, parameterTypes);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001D368 File Offset: 0x0001B568
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static DynamicMethod CreateDynamicMethodWithUnrestrictedPermission(Type owner, Type returnType, Type[] parameterTypes)
		{
			return new DynamicMethod("temp-dynamic-method", returnType, parameterTypes, owner);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001D377 File Offset: 0x0001B577
		private static void PreventTailCall(ILGenerator ilGen)
		{
			ilGen.Emit(OpCodes.Volatile);
			ilGen.Emit(OpCodes.Ldsfld, typeof(string).GetField("Empty"));
			ilGen.Emit(OpCodes.Pop);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		internal static ConstructorInfo GetConstructorWithReflectionPermission(Type type, Type baseType, bool throwOnError)
		{
			type = TypeUtil.VerifyAssignableType(baseType, type, throwOnError);
			if (type == null)
			{
				return null;
			}
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			ConstructorInfo constructor = type.GetConstructor(bindingAttr, null, CallingConventions.HasThis, Type.EmptyTypes, null);
			if (constructor == null && throwOnError)
			{
				throw new TypeLoadException(SR.GetString("TypeNotPublic", new object[]
				{
					type.AssemblyQualifiedName
				}));
			}
			return constructor;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001D411 File Offset: 0x0001B611
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		internal static object InvokeCtorWithReflectionPermission(ConstructorInfo ctor)
		{
			return ctor.Invoke(null);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001D41C File Offset: 0x0001B61C
		internal static bool IsTypeFromTrustedAssemblyWithoutAptca(Type type)
		{
			Assembly assembly = type.Assembly;
			return assembly.GlobalAssemblyCache && !TypeUtil.HasAptcaBit(assembly);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001D443 File Offset: 0x0001B643
		internal static Type VerifyAssignableType(Type baseType, Type type, bool throwOnError)
		{
			if (baseType.IsAssignableFrom(type))
			{
				return type;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(SR.GetString("Config_type_doesnt_inherit_from_type", new object[]
				{
					type.FullName,
					baseType.FullName
				}));
			}
			return null;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001D47C File Offset: 0x0001B67C
		private static bool HasAptcaBit(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(typeof(AllowPartiallyTrustedCallersAttribute), false);
			return customAttributes != null && customAttributes.Length != 0;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		internal static bool IsCallerFullTrust
		{
			get
			{
				bool result = false;
				try
				{
					if (TypeUtil.s_fullTrustPermissionSet == null)
					{
						TypeUtil.s_fullTrustPermissionSet = new PermissionSet(PermissionState.Unrestricted);
					}
					TypeUtil.s_fullTrustPermissionSet.Demand();
					result = true;
				}
				catch
				{
				}
				return result;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		private static bool CallerHasMemberAccessOrAspNetPermission()
		{
			try
			{
				TypeUtil.s_memberAccessPermission.Demand();
				return true;
			}
			catch (SecurityException)
			{
			}
			try
			{
				TypeUtil.s_aspNetHostingPermission.Demand();
				return true;
			}
			catch (SecurityException)
			{
			}
			return false;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001D544 File Offset: 0x0001B744
		internal static bool IsTypeAllowedInConfig(Type t)
		{
			if (TypeUtil.IsCallerFullTrust)
			{
				return true;
			}
			Assembly assembly = t.Assembly;
			return !assembly.GlobalAssemblyCache || TypeUtil.HasAptcaBit(assembly);
		}

		// Token: 0x0400035F RID: 863
		private static volatile PermissionSet s_fullTrustPermissionSet;

		// Token: 0x04000360 RID: 864
		private static readonly ReflectionPermission s_memberAccessPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess);

		// Token: 0x04000361 RID: 865
		private static readonly AspNetHostingPermission s_aspNetHostingPermission = new AspNetHostingPermission(AspNetHostingPermissionLevel.Minimal);
	}
}
