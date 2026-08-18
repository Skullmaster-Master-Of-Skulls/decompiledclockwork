using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000003 RID: 3
	internal static class TypeExtensions
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static TypeExtensions()
		{
			foreach (PrimitiveType primitiveType in PrimitiveType.GetEdmPrimitiveTypes())
			{
				if (!TypeExtensions._primitiveTypesMap.ContainsKey(primitiveType.ClrEquivalentType))
				{
					TypeExtensions._primitiveTypesMap.Add(primitiveType.ClrEquivalentType, primitiveType);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021AC File Offset: 0x000003AC
		public static bool IsCollection(this Type type)
		{
			return type.IsCollection(out type);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021B6 File Offset: 0x000003B6
		public static bool IsCollection(this Type type, out Type elementType)
		{
			elementType = type.TryGetElementType(typeof(ICollection<>));
			if (elementType == null || type.IsArray)
			{
				elementType = type;
				return false;
			}
			return true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021FC File Offset: 0x000003FC
		public static IEnumerable<PropertyInfo> GetNonIndexerProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
			where p.IsPublic() && !p.GetIndexParameters().Any<ParameterInfo>()
			select p;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002228 File Offset: 0x00000428
		public static Type TryGetElementType(this Type type, Type interfaceOrBaseType)
		{
			if (type.IsGenericTypeDefinition())
			{
				return null;
			}
			List<Type> list = type.GetGenericTypeImplementations(interfaceOrBaseType).ToList<Type>();
			if (list.Count != 1)
			{
				return null;
			}
			return list[0].GetGenericArguments().FirstOrDefault<Type>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002290 File Offset: 0x00000490
		public static IEnumerable<Type> GetGenericTypeImplementations(this Type type, Type interfaceOrBaseType)
		{
			if (!type.IsGenericTypeDefinition())
			{
				return from t in (interfaceOrBaseType.IsInterface() ? type.GetInterfaces() : type.GetBaseTypes()).Union(new Type[]
				{
					type
				})
				where t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceOrBaseType
				select t;
			}
			return Enumerable.Empty<Type>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023F8 File Offset: 0x000005F8
		public static IEnumerable<Type> GetBaseTypes(this Type type)
		{
			type = type.BaseType();
			while (type != null)
			{
				yield return type;
				type = type.BaseType();
			}
			yield break;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002418 File Offset: 0x00000618
		public static Type GetTargetType(this Type type)
		{
			Type result;
			if (!type.IsCollection(out result))
			{
				result = type;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002432 File Offset: 0x00000632
		public static bool TryUnwrapNullableType(this Type type, out Type underlyingType)
		{
			underlyingType = (Nullable.GetUnderlyingType(type) ?? type);
			return underlyingType != type;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002449 File Offset: 0x00000649
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType() || Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002464 File Offset: 0x00000664
		public static bool IsValidStructuralType(this Type type)
		{
			return !type.IsGenericType() && !type.IsValueType() && !type.IsPrimitive() && !type.IsInterface() && !type.IsArray && !(type == typeof(string)) && !(type == typeof(DbGeography)) && !(type == typeof(DbGeometry)) && type.IsValidStructuralPropertyType();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024D8 File Offset: 0x000006D8
		public static bool IsValidStructuralPropertyType(this Type type)
		{
			return !type.IsGenericTypeDefinition() && !type.IsPointer && !(type == typeof(object)) && !typeof(ComplexObject).IsAssignableFrom(type) && !typeof(EntityObject).IsAssignableFrom(type) && !typeof(StructuralObject).IsAssignableFrom(type) && !typeof(EntityKey).IsAssignableFrom(type) && !typeof(EntityReference).IsAssignableFrom(type);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002564 File Offset: 0x00000764
		public static bool IsPrimitiveType(this Type type, out PrimitiveType primitiveType)
		{
			return TypeExtensions._primitiveTypesMap.TryGetValue(type, out primitiveType);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000257C File Offset: 0x0000077C
		public static T CreateInstance<T>(this Type type, Func<string, string, string> typeMessageFactory, Func<string, Exception> exceptionFactory = null)
		{
			exceptionFactory = (exceptionFactory ?? ((string s) => new InvalidOperationException(s)));
			if (!typeof(T).IsAssignableFrom(type))
			{
				throw exceptionFactory(typeMessageFactory(type.ToString(), typeof(T).ToString()));
			}
			return type.CreateInstance(exceptionFactory);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025E0 File Offset: 0x000007E0
		public static T CreateInstance<T>(this Type type, Func<string, Exception> exceptionFactory = null)
		{
			exceptionFactory = (exceptionFactory ?? ((string s) => new InvalidOperationException(s)));
			if (type.GetDeclaredConstructor(new Type[0]) == null)
			{
				throw exceptionFactory(Strings.CreateInstance_NoParameterlessConstructor(type));
			}
			if (type.IsAbstract())
			{
				throw exceptionFactory(Strings.CreateInstance_AbstractType(type));
			}
			if (type.IsGenericType())
			{
				throw exceptionFactory(Strings.CreateInstance_GenericType(type));
			}
			return (T)((object)Activator.CreateInstance(type, true));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002658 File Offset: 0x00000858
		public static bool IsValidEdmScalarType(this Type type)
		{
			type.TryUnwrapNullableType(out type);
			PrimitiveType primitiveType;
			return type.IsPrimitiveType(out primitiveType) || type.IsEnum();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002680 File Offset: 0x00000880
		public static string NestingNamespace(this Type type)
		{
			if (!type.IsNested)
			{
				return type.Namespace;
			}
			string fullName = type.FullName;
			return fullName.Substring(0, fullName.Length - type.Name.Length - 1).Replace('+', '.');
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026C7 File Offset: 0x000008C7
		public static string FullNameWithNesting(this Type type)
		{
			if (!type.IsNested)
			{
				return type.FullName;
			}
			return type.FullName.Replace('+', '.');
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000274C File Offset: 0x0000094C
		public static bool OverridesEqualsOrGetHashCode(this Type type)
		{
			while (type != typeof(object))
			{
				if (type.GetDeclaredMethods().Any((MethodInfo m) => (m.Name == "Equals" || m.Name == "GetHashCode") && m.DeclaringType != typeof(object) && m.GetBaseDefinition().DeclaringType == typeof(object)))
				{
					return true;
				}
				type = type.BaseType();
			}
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027A4 File Offset: 0x000009A4
		public static bool IsPublic(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsPublic || (typeInfo.IsNestedPublic && type.DeclaringType.IsPublic());
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027D7 File Offset: 0x000009D7
		public static bool IsNotPublic(this Type type)
		{
			return !type.IsPublic();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027E2 File Offset: 0x000009E2
		public static MethodInfo GetOnlyDeclaredMethod(this Type type, string name)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002838 File Offset: 0x00000A38
		public static MethodInfo GetDeclaredMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault((MethodInfo m) => (from p in m.GetParameters()
			select p.ParameterType).SequenceEqual(parameterTypes));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000287F File Offset: 0x00000A7F
		public static MethodInfo GetPublicInstanceMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetRuntimeMethod(name, (MethodInfo m) => m.IsPublic && !m.IsStatic, parameterTypes);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028D4 File Offset: 0x00000AD4
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Func<MethodInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return (from t in parameterTypes
			select type.GetRuntimeMethod(name, predicate, t)).FirstOrDefault((MethodInfo m) => m != null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A00 File Offset: 0x00000C00
		private static MethodInfo GetRuntimeMethod(this Type type, string name, Func<MethodInfo, bool> predicate, Type[] parameterTypes)
		{
			MethodInfo[] methods = type.GetRuntimeMethods().Where(delegate(MethodInfo m)
			{
				if (name == m.Name && predicate(m))
				{
					return (from p in m.GetParameters()
					select p.ParameterType).SequenceEqual(parameterTypes);
				}
				return false;
			}).ToArray<MethodInfo>();
			if (methods.Length == 1)
			{
				return methods[0];
			}
			return methods.SingleOrDefault((MethodInfo m) => !methods.Any((MethodInfo m2) => m2.DeclaringType.IsSubclassOf(m.DeclaringType)));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A75 File Offset: 0x00000C75
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return type.GetTypeInfo().DeclaredMethods;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A82 File Offset: 0x00000C82
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredMethods(name);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A90 File Offset: 0x00000C90
		public static PropertyInfo GetDeclaredProperty(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredProperty(name);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A9E File Offset: 0x00000C9E
		public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
		{
			return type.GetTypeInfo().DeclaredProperties;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public static IEnumerable<PropertyInfo> GetInstanceProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
			where !p.IsStatic()
			select p;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static IEnumerable<PropertyInfo> GetNonHiddenProperties(this Type type)
		{
			return from property in type.GetRuntimeProperties()
			group property by property.Name into propertyGroup
			select TypeExtensions.MostDerived(propertyGroup);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B48 File Offset: 0x00000D48
		private static PropertyInfo MostDerived(IEnumerable<PropertyInfo> properties)
		{
			PropertyInfo propertyInfo = null;
			foreach (PropertyInfo propertyInfo2 in properties)
			{
				if (propertyInfo == null || (propertyInfo.DeclaringType != null && propertyInfo.DeclaringType.IsAssignableFrom(propertyInfo2.DeclaringType)))
				{
					propertyInfo = propertyInfo2;
				}
			}
			return propertyInfo;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public static PropertyInfo GetAnyProperty(this Type type, string name)
		{
			List<PropertyInfo> source = (from p in type.GetRuntimeProperties()
			where p.Name == name
			select p).ToList<PropertyInfo>();
			if (source.Count<PropertyInfo>() > 1)
			{
				throw new AmbiguousMatchException();
			}
			return source.SingleOrDefault<PropertyInfo>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C48 File Offset: 0x00000E48
		public static PropertyInfo GetInstanceProperty(this Type type, string name)
		{
			List<PropertyInfo> source = (from p in type.GetRuntimeProperties()
			where p.Name == name && !p.IsStatic()
			select p).ToList<PropertyInfo>();
			if (source.Count<PropertyInfo>() > 1)
			{
				throw new AmbiguousMatchException();
			}
			return source.SingleOrDefault<PropertyInfo>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CBC File Offset: 0x00000EBC
		public static PropertyInfo GetStaticProperty(this Type type, string name)
		{
			List<PropertyInfo> source = (from p in type.GetRuntimeProperties()
			where p.Name == name && p.IsStatic()
			select p).ToList<PropertyInfo>();
			if (source.Count<PropertyInfo>() > 1)
			{
				throw new AmbiguousMatchException();
			}
			return source.SingleOrDefault<PropertyInfo>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D08 File Offset: 0x00000F08
		public static PropertyInfo GetTopProperty(this Type type, string name)
		{
			PropertyInfo declaredProperty;
			for (;;)
			{
				TypeInfo typeInfo = type.GetTypeInfo();
				declaredProperty = typeInfo.GetDeclaredProperty(name);
				if (declaredProperty != null && !(declaredProperty.GetMethod ?? declaredProperty.SetMethod).IsStatic)
				{
					break;
				}
				type = typeInfo.BaseType;
				if (!(type != null))
				{
					goto Block_3;
				}
			}
			return declaredProperty;
			Block_3:
			return null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002D58 File Offset: 0x00000F58
		public static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002D65 File Offset: 0x00000F65
		public static Type BaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D72 File Offset: 0x00000F72
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002D7F File Offset: 0x00000F7F
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static TypeAttributes Attributes(this Type type)
		{
			return type.GetTypeInfo().Attributes;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D99 File Offset: 0x00000F99
		public static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DB3 File Offset: 0x00000FB3
		public static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DCD File Offset: 0x00000FCD
		public static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002DDA File Offset: 0x00000FDA
		public static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public static bool IsSerializable(this Type type)
		{
			return type.GetTypeInfo().IsSerializable;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static bool IsGenericParameter(this Type type)
		{
			return type.GetTypeInfo().IsGenericParameter;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E01 File Offset: 0x00001001
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E0E File Offset: 0x0000100E
		public static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E1B File Offset: 0x0000101B
		public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E78 File Offset: 0x00001078
		public static ConstructorInfo GetDeclaredConstructor(this Type type, params Type[] parameterTypes)
		{
			return type.GetDeclaredConstructors().SingleOrDefault(delegate(ConstructorInfo c)
			{
				if (!c.IsStatic)
				{
					return (from p in c.GetParameters()
					select p.ParameterType).SequenceEqual(parameterTypes);
				}
				return false;
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EAC File Offset: 0x000010AC
		public static ConstructorInfo GetPublicConstructor(this Type type, params Type[] parameterTypes)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(parameterTypes);
			if (!(declaredConstructor != null) || !declaredConstructor.IsPublic)
			{
				return null;
			}
			return declaredConstructor;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F04 File Offset: 0x00001104
		public static ConstructorInfo GetDeclaredConstructor(this Type type, Func<ConstructorInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return (from p in parameterTypes
			select type.GetDeclaredConstructor(p)).FirstOrDefault((ConstructorInfo c) => c != null && predicate(c));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F48 File Offset: 0x00001148
		public static bool IsSubclassOf(this Type type, Type otherType)
		{
			return type.GetTypeInfo().IsSubclassOf(otherType);
		}

		// Token: 0x04000001 RID: 1
		private static readonly Dictionary<Type, PrimitiveType> _primitiveTypesMap = new Dictionary<Type, PrimitiveType>();
	}
}
