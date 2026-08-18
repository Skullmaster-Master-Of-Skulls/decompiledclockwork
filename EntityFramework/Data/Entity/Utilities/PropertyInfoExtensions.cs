using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200000B RID: 11
	internal static class PropertyInfoExtensions
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003964 File Offset: 0x00001B64
		public static bool IsSameAs(this PropertyInfo propertyInfo, PropertyInfo otherPropertyInfo)
		{
			return propertyInfo == otherPropertyInfo || (propertyInfo.Name == otherPropertyInfo.Name && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType || propertyInfo.DeclaringType.IsSubclassOf(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.IsSubclassOf(propertyInfo.DeclaringType) || propertyInfo.DeclaringType.GetInterfaces().Contains(otherPropertyInfo.DeclaringType) || otherPropertyInfo.DeclaringType.GetInterfaces().Contains(propertyInfo.DeclaringType)));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000039FA File Offset: 0x00001BFA
		public static bool ContainsSame(this IEnumerable<PropertyInfo> enumerable, PropertyInfo propertyInfo)
		{
			return enumerable.Any(new Func<PropertyInfo, bool>(propertyInfo.IsSameAs));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A0E File Offset: 0x00001C0E
		public static bool IsValidStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && !propertyInfo.Getter().IsAbstract;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A28 File Offset: 0x00001C28
		public static bool IsValidInterfaceStructuralProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.CanRead && (propertyInfo.CanWriteExtended() || propertyInfo.PropertyType.IsCollection()) && propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.PropertyType.IsValidStructuralPropertyType();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A5E File Offset: 0x00001C5E
		public static bool IsValidEdmScalarProperty(this PropertyInfo propertyInfo)
		{
			return propertyInfo.IsValidInterfaceStructuralProperty() && propertyInfo.PropertyType.IsValidEdmScalarType();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A78 File Offset: 0x00001C78
		public static bool IsValidEdmNavigationProperty(this PropertyInfo propertyInfo)
		{
			Type type;
			return propertyInfo.IsValidInterfaceStructuralProperty() && ((propertyInfo.PropertyType.IsCollection(out type) && type.IsValidStructuralType()) || propertyInfo.PropertyType.IsValidStructuralType());
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003AB4 File Offset: 0x00001CB4
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

		// Token: 0x06000076 RID: 118 RVA: 0x00003B00 File Offset: 0x00001D00
		public static bool CanWriteExtended(this PropertyInfo propertyInfo)
		{
			if (propertyInfo.CanWrite)
			{
				return true;
			}
			PropertyInfo declaredProperty = PropertyInfoExtensions.GetDeclaredProperty(propertyInfo);
			return declaredProperty != null && declaredProperty.CanWrite;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003B2F File Offset: 0x00001D2F
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

		// Token: 0x06000078 RID: 120 RVA: 0x00003B90 File Offset: 0x00001D90
		private static PropertyInfo GetDeclaredProperty(PropertyInfo propertyInfo)
		{
			if (!(propertyInfo.DeclaringType == propertyInfo.ReflectedType))
			{
				return propertyInfo.DeclaringType.GetInstanceProperties().SingleOrDefault((PropertyInfo p) => p.Name == propertyInfo.Name && !p.GetIndexParameters().Any<ParameterInfo>() && p.PropertyType == propertyInfo.PropertyType);
			}
			return propertyInfo;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public static IEnumerable<PropertyInfo> GetPropertiesInHierarchy(this PropertyInfo property)
		{
			List<PropertyInfo> list = new List<PropertyInfo>
			{
				property
			};
			PropertyInfoExtensions.CollectProperties(property, list);
			return list.Distinct<PropertyInfo>();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C19 File Offset: 0x00001E19
		private static void CollectProperties(PropertyInfo property, IList<PropertyInfo> collection)
		{
			PropertyInfoExtensions.FindNextProperty(property, collection, true);
			PropertyInfoExtensions.FindNextProperty(property, collection, false);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003DA4 File Offset: 0x00001FA4
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

		// Token: 0x0600007C RID: 124 RVA: 0x00003E9E File Offset: 0x0000209E
		public static MethodInfo Getter(this PropertyInfo property)
		{
			return property.GetMethod;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003EA6 File Offset: 0x000020A6
		public static MethodInfo Setter(this PropertyInfo property)
		{
			return property.SetMethod;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003EAE File Offset: 0x000020AE
		public static bool IsStatic(this PropertyInfo property)
		{
			return (property.Getter() ?? property.Setter()).IsStatic;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003EC8 File Offset: 0x000020C8
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
