using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000007 RID: 7
	internal static class PropertyInfoExtensions
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000030D8 File Offset: 0x000012D8
		public static bool IsSameAs(this PropertyInfo propertyInfo, PropertyInfo otherPropertyInfo)
		{
			return propertyInfo == otherPropertyInfo || (propertyInfo.Name == otherPropertyInfo.Name && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType || propertyInfo.DeclaringType.IsSubclassOf(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.IsSubclassOf(propertyInfo.DeclaringType) || propertyInfo.DeclaringType.GetInterfaces().Contains(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.GetInterfaces().Contains(propertyInfo.DeclaringType)));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000316E File Offset: 0x0000136E
		public static bool ContainsSame(this IEnumerable<PropertyInfo> enumerable, PropertyInfo propertyInfo)
		{
			return enumerable.Any(new Func<PropertyInfo, bool>(propertyInfo.IsSameAs));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003182 File Offset: 0x00001382
		public static bool IsValidStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && !propertyInfo.Getter().IsAbstract;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000319C File Offset: 0x0000139C
		public static bool IsValidInterfaceStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.CanRead && (propertyInfo.CanWriteExtended() || propertyInfo.PropertyType.IsCollection()) && propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.PropertyType.IsValidStructuralPropertyType();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000031D2 File Offset: 0x000013D2
		public static bool IsValidEdmScalarProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && propertyInfo.PropertyType.IsValidEdmScalarType();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031EC File Offset: 0x000013EC
		public static bool IsValidEdmNavigationProperty(this PropertyInfo propertyInfo)
		{
			Type type;
			return propertyInfo.IsValidInterfaceStructuralProperty() && ((propertyInfo.PropertyType.IsCollection(out type) && type.IsValidStructuralType()) || propertyInfo.PropertyType.IsValidStructuralType());
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003228 File Offset: 0x00001428
		public static EdmProperty AsEdmPrimitiveProperty(this PropertyInfo propertyInfo)
		{
			Type propertyType = propertyInfo.PropertyType;
			bool nullable = propertyType.TryUnwrapNullableType(out propertyType) || !propertyType.IsValueType();
			PrimitiveType primitiveType;
			if (propertyType.IsPrimitiveType(out primitiveType))
			{
				EdmProperty edmProperty = EdmProperty.CreatePrimitive(propertyInfo.Name, primitiveType);
				edmProperty.Nullable = nullable;
				return edmProperty;
			}
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003274 File Offset: 0x00001474
		public static bool CanWriteExtended(this PropertyInfo propertyInfo)
		{
			if (propertyInfo.CanWrite)
			{
				return true;
			}
			PropertyInfo declaredProperty = PropertyInfoExtensions.GetDeclaredProperty(propertyInfo);
			return declaredProperty != null && declaredProperty.CanWrite;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000032A3 File Offset: 0x000014A3
		public static PropertyInfo GetPropertyInfoForSet(this PropertyInfo propertyInfo)
		{
			PropertyInfo result;
			if (!propertyInfo.CanWrite)
			{
				if ((result = PropertyInfoExtensions.GetDeclaredProperty(propertyInfo)) == null)
				{
					return propertyInfo;
				}
			}
			else
			{
				result = propertyInfo;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003304 File Offset: 0x00001504
		private static PropertyInfo GetDeclaredProperty(PropertyInfo propertyInfo)
		{
			if (!(propertyInfo.DeclaringType == propertyInfo.ReflectedType))
			{
				return propertyInfo.DeclaringType.GetInstanceProperties().SingleOrDefault((PropertyInfo p) => p.Name == propertyInfo.Name && !p.GetIndexParameters().Any<ParameterInfo>() && p.PropertyType == propertyInfo.PropertyType);
			}
			return propertyInfo;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003364 File Offset: 0x00001564
		public static IEnumerable<PropertyInfo> GetPropertiesInHierarchy(this PropertyInfo property)
		{
			List<PropertyInfo> list = new List<PropertyInfo>
			{
				property
			};
			PropertyInfoExtensions.CollectProperties(property, list);
			return list.Distinct<PropertyInfo>();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000338D File Offset: 0x0000158D
		private static void CollectProperties(PropertyInfo property, IList<PropertyInfo> collection)
		{
			PropertyInfoExtensions.FindNextProperty(property, collection, true);
			PropertyInfoExtensions.FindNextProperty(property, collection, false);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003518 File Offset: 0x00001718
		private static void FindNextProperty(PropertyInfo property, IList<PropertyInfo> collection, bool getter)
		{
			MethodInfo methodInfo = getter ? property.Getter() : property.Setter();
			if (methodInfo != null)
			{
				Type type = methodInfo.DeclaringType.BaseType();
				if (type != null && type != typeof(object))
				{
					MethodInfo baseMethod = methodInfo.GetBaseDefinition();
					PropertyInfo propertyInfo = (from p in type.GetInstanceProperties()
					let candidateMethod = getter ? p.Getter() : p.Setter()
					where candidateMethod != null && candidateMethod.GetBaseDefinition() == baseMethod
					select p).FirstOrDefault<PropertyInfo>();
					if (propertyInfo != null)
					{
						collection.Add(propertyInfo);
						PropertyInfoExtensions.CollectProperties(propertyInfo, collection);
					}
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003612 File Offset: 0x00001812
		public static MethodInfo Getter(this PropertyInfo property)
		{
			return property.GetMethod;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000361A File Offset: 0x0000181A
		public static MethodInfo Setter(this PropertyInfo property)
		{
			return property.SetMethod;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003622 File Offset: 0x00001822
		public static bool IsStatic(this PropertyInfo property)
		{
			return (property.Getter() ?? property.Setter()).IsStatic;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000363C File Offset: 0x0000183C
		public static bool IsPublic(this PropertyInfo property)
		{
			MethodInfo methodInfo = property.Getter();
			MethodAttributes methodAttributes = (methodInfo == null) ? MethodAttributes.Private : (methodInfo.Attributes & MethodAttributes.MemberAccessMask);
			MethodInfo methodInfo2 = property.Setter();
			MethodAttributes methodAttributes2 = (methodInfo2 == null) ? MethodAttributes.Private : (methodInfo2.Attributes & MethodAttributes.MemberAccessMask);
			MethodAttributes methodAttributes3 = (methodAttributes > methodAttributes2) ? methodAttributes : methodAttributes2;
			return methodAttributes3 == MethodAttributes.Public;
		}
	}
}
