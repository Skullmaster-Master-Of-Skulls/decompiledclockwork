using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000006 RID: 6
	internal static class TypeExtensions
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000239C File Offset: 0x0000059C
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

		// Token: 0x06000011 RID: 17 RVA: 0x00002410 File Offset: 0x00000610
		public static bool IsCollection(this Type type)
		{
			return type.IsCollection(out type);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000241A File Offset: 0x0000061A
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002460 File Offset: 0x00000660
		public static IEnumerable<PropertyInfo> GetNonIndexerProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
			where p.IsPublic() && !p.GetIndexParameters().Any<ParameterInfo>()
			select p;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000248C File Offset: 0x0000068C
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

		// Token: 0x06000015 RID: 21 RVA: 0x000024F4 File Offset: 0x000006F4
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

		// Token: 0x06000016 RID: 22 RVA: 0x0000265C File Offset: 0x0000085C
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

		// Token: 0x06000017 RID: 23 RVA: 0x0000267C File Offset: 0x0000087C
		public static Type GetTargetType(this Type type)
		{
			Type result;
			if (!type.IsCollection(out result))
			{
				result = type;
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002696 File Offset: 0x00000896
		public static bool TryUnwrapNullableType(this Type type, out Type underlyingType)
		{
			underlyingType = (Nullable.GetUnderlyingType(type) ?? type);
			return underlyingType != type;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026AD File Offset: 0x000008AD
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType() || Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026C8 File Offset: 0x000008C8
		public static bool IsValidStructuralType(this Type type)
		{
			return !type.IsGenericType() && !type.IsValueType() && !type.IsPrimitive() && !type.IsInterface() && !type.IsArray && !(type == typeof(string)) && !(type == typeof(DbGeography)) && !(type == typeof(DbGeometry)) && type.IsValidStructuralPropertyType();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000273C File Offset: 0x0000093C
		public static bool IsValidStructuralPropertyType(this Type type)
		{
			return !type.IsGenericTypeDefinition() && !type.IsPointer && !(type == typeof(object)) && !typeof(ComplexObject).IsAssignableFrom(type) && !typeof(EntityObject).IsAssignableFrom(type) && !typeof(StructuralObject).IsAssignableFrom(type) && !typeof(EntityKey).IsAssignableFrom(type) && !typeof(EntityReference).IsAssignableFrom(type);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027C8 File Offset: 0x000009C8
		public static bool IsPrimitiveType(this Type type, out PrimitiveType primitiveType)
		{
			return TypeExtensions._primitiveTypesMap.TryGetValue(type, out primitiveType);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027D8 File Offset: 0x000009D8
		public static bool IsValidEdmScalarType(this Type type)
		{
			type.TryUnwrapNullableType(out type);
			PrimitiveType primitiveType;
			return type.IsPrimitiveType(out primitiveType) || type.IsEnum();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002800 File Offset: 0x00000A00
		public static string NestingNamespace(this Type type)
		{
			if (!type.IsNested)
			{
				return type.Namespace;
			}
			string fullName = type.FullName;
			return fullName.Substring(0, fullName.Length - type.Name.Length - 1).Replace('+', '.');
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002847 File Offset: 0x00000A47
		public static string FullNameWithNesting(this Type type)
		{
			if (!type.IsNested)
			{
				return type.FullName;
			}
			return type.FullName.Replace('+', '.');
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028CC File Offset: 0x00000ACC
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

		// Token: 0x06000021 RID: 33 RVA: 0x00002924 File Offset: 0x00000B24
		public static bool IsPublic(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsPublic || (typeInfo.IsNestedPublic && type.DeclaringType.IsPublic());
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002957 File Offset: 0x00000B57
		public static bool IsNotPublic(this Type type)
		{
			return !type.IsPublic();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002962 File Offset: 0x00000B62
		public static MethodInfo GetOnlyDeclaredMethod(this Type type, string name)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000029B8 File Offset: 0x00000BB8
		public static MethodInfo GetDeclaredMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault((MethodInfo m) => (from p in m.GetParameters()
			select p.ParameterType).SequenceEqual(parameterTypes));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029FF File Offset: 0x00000BFF
		public static MethodInfo GetPublicInstanceMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetRuntimeMethod(name, (MethodInfo m) => m.IsPublic && !m.IsStatic, parameterTypes);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A54 File Offset: 0x00000C54
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Func<MethodInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return (from t in parameterTypes
			select type.GetRuntimeMethod(name, predicate, t)).FirstOrDefault((MethodInfo m) => m != null);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B80 File Offset: 0x00000D80
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

		// Token: 0x06000028 RID: 40 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return type.GetTypeInfo().DeclaredMethods;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002C02 File Offset: 0x00000E02
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredMethods(name);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C10 File Offset: 0x00000E10
		public static PropertyInfo GetDeclaredProperty(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredProperty(name);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C1E File Offset: 0x00000E1E
		public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
		{
			return type.GetTypeInfo().DeclaredProperties;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C36 File Offset: 0x00000E36
		public static IEnumerable<PropertyInfo> GetInstanceProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
			where !p.IsStatic()
			select p;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C70 File Offset: 0x00000E70
		public static IEnumerable<PropertyInfo> GetNonHiddenProperties(this Type type)
		{
			return from property in type.GetRuntimeProperties()
			group property by property.Name into propertyGroup
			select TypeExtensions.MostDerived(propertyGroup);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002CC8 File Offset: 0x00000EC8
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

		// Token: 0x0600002F RID: 47 RVA: 0x00002D54 File Offset: 0x00000F54
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

		// Token: 0x06000030 RID: 48 RVA: 0x00002DC8 File Offset: 0x00000FC8
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

		// Token: 0x06000031 RID: 49 RVA: 0x00002E3C File Offset: 0x0000103C
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

		// Token: 0x06000032 RID: 50 RVA: 0x00002E88 File Offset: 0x00001088
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

		// Token: 0x06000033 RID: 51 RVA: 0x00002ED8 File Offset: 0x000010D8
		public static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002EE5 File Offset: 0x000010E5
		public static Type BaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002EF2 File Offset: 0x000010F2
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002EFF File Offset: 0x000010FF
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002F0C File Offset: 0x0000110C
		public static TypeAttributes Attributes(this Type type)
		{
			return type.GetTypeInfo().Attributes;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F19 File Offset: 0x00001119
		public static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F26 File Offset: 0x00001126
		public static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F33 File Offset: 0x00001133
		public static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F40 File Offset: 0x00001140
		public static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F4D File Offset: 0x0000114D
		public static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F5A File Offset: 0x0000115A
		public static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F67 File Offset: 0x00001167
		public static bool IsSerializable(this Type type)
		{
			return type.GetTypeInfo().IsSerializable;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F74 File Offset: 0x00001174
		public static bool IsGenericParameter(this Type type)
		{
			return type.GetTypeInfo().IsGenericParameter;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F81 File Offset: 0x00001181
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F8E File Offset: 0x0000118E
		public static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F9B File Offset: 0x0000119B
		public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FF8 File Offset: 0x000011F8
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

		// Token: 0x06000044 RID: 68 RVA: 0x0000302C File Offset: 0x0000122C
		public static ConstructorInfo GetPublicConstructor(this Type type, params Type[] parameterTypes)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(parameterTypes);
			if (!(declaredConstructor != null) || !declaredConstructor.IsPublic)
			{
				return null;
			}
			return declaredConstructor;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003084 File Offset: 0x00001284
		public static ConstructorInfo GetDeclaredConstructor(this Type type, Func<ConstructorInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return (from p in parameterTypes
			select type.GetDeclaredConstructor(p)).FirstOrDefault((ConstructorInfo c) => c != null && predicate(c));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030C8 File Offset: 0x000012C8
		public static bool IsSubclassOf(this Type type, Type otherType)
		{
			return type.GetTypeInfo().IsSubclassOf(otherType);
		}

		// Token: 0x04000003 RID: 3
		private static readonly Dictionary<Type, PrimitiveType> _primitiveTypesMap = new Dictionary<Type, PrimitiveType>();
	}
}
