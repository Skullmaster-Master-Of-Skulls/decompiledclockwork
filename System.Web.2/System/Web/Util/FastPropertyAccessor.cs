using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Web.UI;

namespace System.Web.Util
{
	// Token: 0x020001FB RID: 507
	internal class FastPropertyAccessor
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x0004D178 File Offset: 0x0004B378
		static FastPropertyAccessor()
		{
			FastPropertyAccessor._getPropertyMethod = typeof(IWebPropertyAccessor).GetMethod("GetProperty");
			FastPropertyAccessor._setPropertyMethod = typeof(IWebPropertyAccessor).GetMethod("SetProperty");
			FastPropertyAccessor._interfacesToImplement = new Type[1];
			FastPropertyAccessor._interfacesToImplement[0] = typeof(IWebPropertyAccessor);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0004D21C File Offset: 0x0004B41C
		private static string GetUniqueCompilationName()
		{
			return Guid.NewGuid().ToString().Replace('-', '_');
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0004D248 File Offset: 0x0004B448
		private Type GetPropertyAccessorTypeWithAssert(Type type, string propertyName, PropertyInfo propInfo, FieldInfo fieldInfo)
		{
			MethodInfo methodInfo = null;
			MethodInfo methodInfo2 = null;
			Type type2;
			if (propInfo != null)
			{
				methodInfo = propInfo.GetGetMethod();
				methodInfo2 = propInfo.GetSetMethod();
				type2 = propInfo.PropertyType;
			}
			else
			{
				type2 = fieldInfo.FieldType;
			}
			if (this._dynamicModule == null)
			{
				lock (this)
				{
					if (this._dynamicModule == null)
					{
						string uniqueCompilationName = FastPropertyAccessor.GetUniqueCompilationName();
						AssemblyName assemblyName = new AssemblyName();
						assemblyName.Name = "A_" + uniqueCompilationName;
						AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, null, true, null);
						this._dynamicModule = assemblyBuilder.DefineDynamicModule("M_" + uniqueCompilationName);
					}
				}
			}
			string str = string.Concat(new string[]
			{
				Util.MakeValidTypeNameFromString(type.Name),
				"_",
				propertyName,
				"_",
				FastPropertyAccessor._uniqueId++.ToString()
			});
			TypeBuilder typeBuilder = this._dynamicModule.DefineType("T_" + str, TypeAttributes.Public, typeof(object), FastPropertyAccessor._interfacesToImplement);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("GetProperty", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, typeof(object), FastPropertyAccessor._getPropertyParameterList);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			if (methodInfo != null)
			{
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Castclass, type);
				if (propInfo != null)
				{
					ilgenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				}
				ilgenerator.Emit(OpCodes.Box, type2);
				ilgenerator.Emit(OpCodes.Ret);
				typeBuilder.DefineMethodOverride(methodBuilder, FastPropertyAccessor._getPropertyMethod);
			}
			else
			{
				ConstructorInfo constructor = typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes);
				ilgenerator.Emit(OpCodes.Newobj, constructor);
				ilgenerator.Emit(OpCodes.Throw);
			}
			methodBuilder = typeBuilder.DefineMethod("SetProperty", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, null, FastPropertyAccessor._setPropertyParameterList);
			ilgenerator = methodBuilder.GetILGenerator();
			if (fieldInfo != null || methodInfo2 != null)
			{
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Castclass, type);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				if (type2.IsPrimitive)
				{
					ilgenerator.Emit(OpCodes.Unbox, type2);
					if (type2 == typeof(sbyte))
					{
						ilgenerator.Emit(OpCodes.Ldind_I1);
					}
					else if (type2 == typeof(byte))
					{
						ilgenerator.Emit(OpCodes.Ldind_U1);
					}
					else if (type2 == typeof(short))
					{
						ilgenerator.Emit(OpCodes.Ldind_I2);
					}
					else if (type2 == typeof(ushort))
					{
						ilgenerator.Emit(OpCodes.Ldind_U2);
					}
					else if (type2 == typeof(uint))
					{
						ilgenerator.Emit(OpCodes.Ldind_U4);
					}
					else if (type2 == typeof(int))
					{
						ilgenerator.Emit(OpCodes.Ldind_I4);
					}
					else if (type2 == typeof(long))
					{
						ilgenerator.Emit(OpCodes.Ldind_I8);
					}
					else if (type2 == typeof(ulong))
					{
						ilgenerator.Emit(OpCodes.Ldind_I8);
					}
					else if (type2 == typeof(bool))
					{
						ilgenerator.Emit(OpCodes.Ldind_I1);
					}
					else if (type2 == typeof(char))
					{
						ilgenerator.Emit(OpCodes.Ldind_U2);
					}
					else if (type2 == typeof(decimal))
					{
						ilgenerator.Emit(OpCodes.Ldobj, type2);
					}
					else if (type2 == typeof(float))
					{
						ilgenerator.Emit(OpCodes.Ldind_R4);
					}
					else if (type2 == typeof(double))
					{
						ilgenerator.Emit(OpCodes.Ldind_R8);
					}
					else
					{
						ilgenerator.Emit(OpCodes.Ldobj, type2);
					}
				}
				else if (type2.IsValueType)
				{
					ilgenerator.Emit(OpCodes.Unbox, type2);
					ilgenerator.Emit(OpCodes.Ldobj, type2);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Castclass, type2);
				}
				if (propInfo != null)
				{
					ilgenerator.EmitCall(OpCodes.Callvirt, methodInfo2, null);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Stfld, fieldInfo);
				}
			}
			ilgenerator.Emit(OpCodes.Ret);
			typeBuilder.DefineMethodOverride(methodBuilder, FastPropertyAccessor._setPropertyMethod);
			return typeBuilder.CreateType();
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0004D710 File Offset: 0x0004B910
		private static void GetPropertyInfo(Type type, string propertyName, out PropertyInfo propInfo, out FieldInfo fieldInfo, out Type declaringType)
		{
			propInfo = FastPropertyAccessor.GetPropertyMostSpecific(type, propertyName);
			fieldInfo = null;
			if (propInfo != null)
			{
				MethodInfo methodInfo = propInfo.GetGetMethod();
				if (methodInfo == null)
				{
					methodInfo = propInfo.GetSetMethod();
				}
				declaringType = methodInfo.GetBaseDefinition().DeclaringType;
				if (declaringType.IsGenericType)
				{
					declaringType = type;
				}
				if (declaringType != type)
				{
					propInfo = declaringType.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
					return;
				}
			}
			else
			{
				fieldInfo = type.GetField(propertyName);
				if (fieldInfo == null)
				{
					throw new ArgumentException();
				}
				declaringType = fieldInfo.DeclaringType;
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0004D7A4 File Offset: 0x0004B9A4
		private static IWebPropertyAccessor GetPropertyAccessor(Type type, string propertyName)
		{
			if (FastPropertyAccessor.s_accessorGenerator == null || FastPropertyAccessor.s_accessorCache == null)
			{
				object obj = FastPropertyAccessor.s_lockObject;
				lock (obj)
				{
					if (FastPropertyAccessor.s_accessorGenerator == null || FastPropertyAccessor.s_accessorCache == null)
					{
						FastPropertyAccessor.s_accessorGenerator = new FastPropertyAccessor();
						FastPropertyAccessor.s_accessorCache = new Hashtable();
					}
				}
			}
			int num = HashCodeCombiner.CombineHashCodes(type.GetHashCode(), propertyName.GetHashCode());
			IWebPropertyAccessor webPropertyAccessor = (IWebPropertyAccessor)FastPropertyAccessor.s_accessorCache[num];
			if (webPropertyAccessor != null)
			{
				return webPropertyAccessor;
			}
			FieldInfo fieldInfo = null;
			PropertyInfo propInfo = null;
			Type type2;
			FastPropertyAccessor.GetPropertyInfo(type, propertyName, out propInfo, out fieldInfo, out type2);
			int num2 = 0;
			if (type2 != type)
			{
				num2 = HashCodeCombiner.CombineHashCodes(type2.GetHashCode(), propertyName.GetHashCode());
				webPropertyAccessor = (IWebPropertyAccessor)FastPropertyAccessor.s_accessorCache[num2];
				if (webPropertyAccessor != null)
				{
					object syncRoot = FastPropertyAccessor.s_accessorCache.SyncRoot;
					lock (syncRoot)
					{
						FastPropertyAccessor.s_accessorCache[num] = webPropertyAccessor;
					}
					return webPropertyAccessor;
				}
			}
			if (webPropertyAccessor == null)
			{
				FastPropertyAccessor obj2 = FastPropertyAccessor.s_accessorGenerator;
				Type propertyAccessorTypeWithAssert;
				lock (obj2)
				{
					propertyAccessorTypeWithAssert = FastPropertyAccessor.s_accessorGenerator.GetPropertyAccessorTypeWithAssert(type2, propertyName, propInfo, fieldInfo);
				}
				webPropertyAccessor = (IWebPropertyAccessor)HttpRuntime.CreateNonPublicInstance(propertyAccessorTypeWithAssert);
			}
			object syncRoot2 = FastPropertyAccessor.s_accessorCache.SyncRoot;
			lock (syncRoot2)
			{
				FastPropertyAccessor.s_accessorCache[num] = webPropertyAccessor;
				if (num2 != 0)
				{
					FastPropertyAccessor.s_accessorCache[num2] = webPropertyAccessor;
				}
			}
			return webPropertyAccessor;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0004D978 File Offset: 0x0004BB78
		internal static object GetProperty(object target, string propName, bool inDesigner)
		{
			if (!inDesigner)
			{
				IWebPropertyAccessor propertyAccessor = FastPropertyAccessor.GetPropertyAccessor(target.GetType(), propName);
				return propertyAccessor.GetProperty(target);
			}
			FieldInfo fieldInfo = null;
			PropertyInfo propertyInfo = null;
			Type type;
			FastPropertyAccessor.GetPropertyInfo(target.GetType(), propName, out propertyInfo, out fieldInfo, out type);
			if (propertyInfo != null)
			{
				return propertyInfo.GetValue(target, null);
			}
			if (fieldInfo != null)
			{
				return fieldInfo.GetValue(target);
			}
			throw new ArgumentException();
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0004D9DC File Offset: 0x0004BBDC
		private static PropertyInfo GetPropertyMostSpecific(Type type, string name)
		{
			Type type2 = type;
			while (type2 != null)
			{
				PropertyInfo property = type2.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
				if (property != null)
				{
					return property;
				}
				type2 = type2.BaseType;
			}
			return null;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0004DA14 File Offset: 0x0004BC14
		internal static void SetProperty(object target, string propName, object val, bool inDesigner)
		{
			if (!inDesigner)
			{
				IWebPropertyAccessor propertyAccessor = FastPropertyAccessor.GetPropertyAccessor(target.GetType(), propName);
				propertyAccessor.SetProperty(target, val);
				return;
			}
			FieldInfo fieldInfo = null;
			PropertyInfo propertyInfo = null;
			Type type = null;
			FastPropertyAccessor.GetPropertyInfo(target.GetType(), propName, out propertyInfo, out fieldInfo, out type);
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(target, val, null);
				return;
			}
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(target, val);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x0400179F RID: 6047
		private static object s_lockObject = new object();

		// Token: 0x040017A0 RID: 6048
		private static FastPropertyAccessor s_accessorGenerator;

		// Token: 0x040017A1 RID: 6049
		private static Hashtable s_accessorCache;

		// Token: 0x040017A2 RID: 6050
		private static MethodInfo _getPropertyMethod;

		// Token: 0x040017A3 RID: 6051
		private static MethodInfo _setPropertyMethod;

		// Token: 0x040017A4 RID: 6052
		private static Type[] _getPropertyParameterList = new Type[]
		{
			typeof(object)
		};

		// Token: 0x040017A5 RID: 6053
		private static Type[] _setPropertyParameterList = new Type[]
		{
			typeof(object),
			typeof(object)
		};

		// Token: 0x040017A6 RID: 6054
		private static Type[] _interfacesToImplement;

		// Token: 0x040017A7 RID: 6055
		private static int _uniqueId;

		// Token: 0x040017A8 RID: 6056
		private const BindingFlags _declaredFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x040017A9 RID: 6057
		private ModuleBuilder _dynamicModule;
	}
}
