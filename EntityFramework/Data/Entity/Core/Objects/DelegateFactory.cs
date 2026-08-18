using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020004E9 RID: 1257
	internal static class DelegateFactory
	{
		// Token: 0x06002ECB RID: 11979 RVA: 0x000DFCAC File Offset: 0x000DDEAC
		internal static Func<object> GetConstructorDelegateForType(ClrComplexType clrType)
		{
			Func<object> result;
			if ((result = clrType.Constructor) == null)
			{
				result = (clrType.Constructor = DelegateFactory.CreateConstructor(clrType.ClrType));
			}
			return result;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000DFCD8 File Offset: 0x000DDED8
		internal static Func<object> GetConstructorDelegateForType(ClrEntityType clrType)
		{
			Func<object> result;
			if ((result = clrType.Constructor) == null)
			{
				result = (clrType.Constructor = DelegateFactory.CreateConstructor(clrType.ClrType));
			}
			return result;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000DFD04 File Offset: 0x000DDF04
		internal static object GetValue(EdmProperty property, object target)
		{
			Func<object, object> getterDelegateForProperty = DelegateFactory.GetGetterDelegateForProperty(property);
			return getterDelegateForProperty(target);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000DFD20 File Offset: 0x000DDF20
		internal static Func<object, object> GetGetterDelegateForProperty(EdmProperty property)
		{
			Func<object, object> result;
			if ((result = property.ValueGetter) == null)
			{
				result = (property.ValueGetter = DelegateFactory.CreatePropertyGetter(property.EntityDeclaringType, property.PropertyInfo));
			}
			return result;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000DFD54 File Offset: 0x000DDF54
		internal static void SetValue(EdmProperty property, object target, object value)
		{
			Action<object, object> setterDelegateForProperty = DelegateFactory.GetSetterDelegateForProperty(property);
			setterDelegateForProperty(target, value);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000DFD70 File Offset: 0x000DDF70
		internal static Action<object, object> GetSetterDelegateForProperty(EdmProperty property)
		{
			Action<object, object> action = property.ValueSetter;
			if (action == null)
			{
				action = DelegateFactory.CreatePropertySetter(property.EntityDeclaringType, property.PropertyInfo, property.Nullable);
				property.ValueSetter = action;
			}
			return action;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000DFDA8 File Offset: 0x000DDFA8
		internal static RelatedEnd GetRelatedEnd(RelationshipManager sourceRelationshipManager, AssociationEndMember sourceMember, AssociationEndMember targetMember, RelatedEnd existingRelatedEnd)
		{
			Func<RelationshipManager, RelatedEnd, RelatedEnd> func = sourceMember.GetRelatedEnd;
			if (func == null)
			{
				func = DelegateFactory.CreateGetRelatedEndMethod(sourceMember, targetMember);
				sourceMember.GetRelatedEnd = func;
			}
			return func(sourceRelationshipManager, existingRelatedEnd);
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000DFDD8 File Offset: 0x000DDFD8
		internal static Action<object, object> CreateNavigationPropertySetter(Type declaringType, PropertyInfo navigationProperty)
		{
			PropertyInfo propertyInfoForSet = navigationProperty.GetPropertyInfoForSet();
			MethodInfo methodInfo = propertyInfoForSet.Setter();
			if (methodInfo == null)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyNoSetter);
			}
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsStatic);
			}
			if (methodInfo.DeclaringType.IsValueType())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
			}
			ParameterExpression parameterExpression;
			ParameterExpression parameterExpression2;
			return Expression.Lambda<Action<object, object>>(Expression.Assign(Expression.Property(Expression.Convert(parameterExpression, declaringType), propertyInfoForSet), Expression.Convert(parameterExpression2, navigationProperty.PropertyType)), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000DFE98 File Offset: 0x000DE098
		internal static ConstructorInfo GetConstructorForType(Type type)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[0]);
			if (null == declaredConstructor)
			{
				throw new InvalidOperationException(Strings.CodeGen_ConstructorNoParameterless(type.FullName));
			}
			return declaredConstructor;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000DFED0 File Offset: 0x000DE0D0
		internal static NewExpression GetNewExpressionForCollectionType(Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(HashSet<>))
			{
				ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[]
				{
					typeof(IEqualityComparer<>).MakeGenericType(type.GetGenericArguments())
				});
				return Expression.New(declaredConstructor, new Expression[]
				{
					Expression.New(typeof(ObjectReferenceEqualityComparer))
				});
			}
			return Expression.New(DelegateFactory.GetConstructorForType(type));
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000DFF4F File Offset: 0x000DE14F
		internal static Func<object> CreateConstructor(Type type)
		{
			DelegateFactory.GetConstructorForType(type);
			return Expression.Lambda<Func<object>>(Expression.New(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000DFF70 File Offset: 0x000DE170
		internal static Func<object, object> CreatePropertyGetter(Type entityDeclaringType, PropertyInfo propertyInfo)
		{
			MethodInfo methodInfo = propertyInfo.Getter();
			if (methodInfo == null)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyNoGetter);
			}
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsStatic);
			}
			if (propertyInfo.DeclaringType.IsValueType())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
			}
			if (propertyInfo.GetIndexParameters().Any<ParameterInfo>())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsIndexed);
			}
			Type propertyType = propertyInfo.PropertyType;
			if (propertyType.IsPointer)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyUnsupportedType);
			}
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "entity");
			Expression expression = Expression.Property(Expression.Convert(parameterExpression, entityDeclaringType), propertyInfo);
			if (propertyType.IsValueType())
			{
				expression = Expression.Convert(expression, typeof(object));
			}
			return Expression.Lambda<Func<object, object>>(expression, new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000E004C File Offset: 0x000DE24C
		internal static Action<object, object> CreatePropertySetter(Type entityDeclaringType, PropertyInfo propertyInfo, bool allowNull)
		{
			PropertyInfo property = DelegateFactory.ValidateSetterProperty(propertyInfo);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "entity");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "target");
			Type propertyType = propertyInfo.PropertyType;
			if (propertyType.IsValueType() && Nullable.GetUnderlyingType(propertyType) == null)
			{
				allowNull = false;
			}
			Expression expression = Expression.TypeIs(parameterExpression2, propertyType);
			if (allowNull)
			{
				expression = Expression.Or(Expression.ReferenceEqual(parameterExpression2, Expression.Constant(null)), expression);
			}
			return Expression.Lambda<Action<object, object>>(Expression.IfThenElse(expression, Expression.Assign(Expression.Property(Expression.Convert(parameterExpression, entityDeclaringType), property), Expression.Convert(parameterExpression2, propertyInfo.PropertyType)), Expression.Call(DelegateFactory._throwSetInvalidValue, parameterExpression2, Expression.Constant(propertyType), Expression.Constant(entityDeclaringType.Name), Expression.Constant(propertyInfo.Name))), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000E0138 File Offset: 0x000DE338
		internal static PropertyInfo ValidateSetterProperty(PropertyInfo propertyInfo)
		{
			PropertyInfo propertyInfoForSet = propertyInfo.GetPropertyInfoForSet();
			MethodInfo methodInfo = propertyInfoForSet.Setter();
			if (methodInfo == null)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyNoSetter);
			}
			if (methodInfo.IsStatic)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsStatic);
			}
			if (propertyInfoForSet.DeclaringType.IsValueType())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyDeclaringTypeIsValueType);
			}
			if (propertyInfoForSet.GetIndexParameters().Any<ParameterInfo>())
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyIsIndexed);
			}
			if (propertyInfoForSet.PropertyType.IsPointer)
			{
				throw new InvalidOperationException(Strings.CodeGen_PropertyUnsupportedType);
			}
			return propertyInfoForSet;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000E01C4 File Offset: 0x000DE3C4
		private static Func<RelationshipManager, RelatedEnd, RelatedEnd> CreateGetRelatedEndMethod(AssociationEndMember sourceMember, AssociationEndMember targetMember)
		{
			EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(sourceMember);
			EntityType entityTypeForEnd2 = MetadataHelper.GetEntityTypeForEnd(targetMember);
			NavigationPropertyAccessor navigationPropertyAccessor = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd2, targetMember, sourceMember);
			NavigationPropertyAccessor navigationPropertyAccessor2 = MetadataHelper.GetNavigationPropertyAccessor(entityTypeForEnd, sourceMember, targetMember);
			MethodInfo declaredMethod = typeof(DelegateFactory).GetDeclaredMethod("CreateGetRelatedEndMethod", new Type[]
			{
				typeof(AssociationEndMember),
				typeof(AssociationEndMember),
				typeof(NavigationPropertyAccessor),
				typeof(NavigationPropertyAccessor)
			});
			MethodInfo methodInfo = declaredMethod.MakeGenericMethod(new Type[]
			{
				entityTypeForEnd.ClrType,
				entityTypeForEnd2.ClrType
			});
			object obj = methodInfo.Invoke(null, new object[]
			{
				sourceMember,
				targetMember,
				navigationPropertyAccessor,
				navigationPropertyAccessor2
			});
			return (Func<RelationshipManager, RelatedEnd, RelatedEnd>)obj;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000E02EC File Offset: 0x000DE4EC
		private static Func<RelationshipManager, RelatedEnd, RelatedEnd> CreateGetRelatedEndMethod<TSource, TTarget>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor) where TSource : class where TTarget : class
		{
			Func<RelationshipManager, RelatedEnd, RelatedEnd> result;
			switch (targetMember.RelationshipMultiplicity)
			{
			case RelationshipMultiplicity.ZeroOrOne:
			case RelationshipMultiplicity.One:
				result = ((RelationshipManager manager, RelatedEnd relatedEnd) => manager.GetRelatedReference<TSource, TTarget>(sourceMember, targetMember, sourceAccessor, targetAccessor, relatedEnd));
				break;
			case RelationshipMultiplicity.Many:
				result = ((RelationshipManager manager, RelatedEnd relatedEnd) => manager.GetRelatedCollection<TSource, TTarget>(sourceMember, targetMember, sourceAccessor, targetAccessor, relatedEnd));
				break;
			default:
			{
				Type typeFromHandle = typeof(RelationshipMultiplicity);
				throw new ArgumentOutOfRangeException(typeFromHandle.Name, Strings.ADP_InvalidEnumerationValue(typeFromHandle.Name, ((int)targetMember.RelationshipMultiplicity).ToString(CultureInfo.InvariantCulture)));
			}
			}
			return result;
		}

		// Token: 0x040011CD RID: 4557
		private static readonly MethodInfo _throwSetInvalidValue = typeof(EntityUtil).GetDeclaredMethod("ThrowSetInvalidValue", new Type[]
		{
			typeof(object),
			typeof(Type),
			typeof(string),
			typeof(string)
		});
	}
}
