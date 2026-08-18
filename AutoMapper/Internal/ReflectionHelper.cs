using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000B9 RID: 185
	internal static class ReflectionHelper
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x00014A04 File Offset: 0x00012C04
		public static object Map(ResolutionContext context, MemberInfo member, object value)
		{
			Type memberType = member.GetMemberType();
			return context.Engine.Mapper.Map(value, ((value != null) ? value.GetType() : null) ?? memberType, memberType);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00014A3B File Offset: 0x00012C3B
		public static bool IsDynamic(this object obj)
		{
			return obj is IDynamicMetaObjectProvider;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00014A46 File Offset: 0x00012C46
		public static bool IsDynamic(this Type type)
		{
			return typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00014A58 File Offset: 0x00012C58
		public static void SetMemberValue(this MemberInfo propertyOrField, object target, object value)
		{
			PropertyInfo propertyInfo = propertyOrField as PropertyInfo;
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(target, value, null);
				return;
			}
			FieldInfo fieldInfo = propertyOrField as FieldInfo;
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(target, value);
				return;
			}
			throw ReflectionHelper.Expected(propertyOrField);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014A9E File Offset: 0x00012C9E
		private static ArgumentOutOfRangeException Expected(MemberInfo propertyOrField)
		{
			return new ArgumentOutOfRangeException("propertyOrField", "Expected a property or field, not " + propertyOrField);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014AB8 File Offset: 0x00012CB8
		public static object GetMemberValue(this MemberInfo propertyOrField, object target)
		{
			PropertyInfo propertyInfo = propertyOrField as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.GetValue(target, null);
			}
			FieldInfo fieldInfo = propertyOrField as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.GetValue(target);
			}
			throw ReflectionHelper.Expected(propertyOrField);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00014AFC File Offset: 0x00012CFC
		public static IEnumerable<MemberInfo> GetMemberPath(Type type, string fullMemberName)
		{
			MemberInfo property = null;
			foreach (string name in fullMemberName.Split(new char[]
			{
				'.'
			}))
			{
				Type currentType = ReflectionHelper.GetCurrentType(property, type);
				yield return property = currentType.GetMember(name).Single<MemberInfo>();
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014B14 File Offset: 0x00012D14
		private static Type GetCurrentType(MemberInfo member, Type type)
		{
			Type type2 = ((member != null) ? member.GetMemberType() : null) ?? type;
			if (type2.IsGenericType() && typeof(IEnumerable).IsAssignableFrom(type2))
			{
				type2 = type2.GetTypeInfo().GenericTypeArguments[0];
			}
			return type2;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014B5C File Offset: 0x00012D5C
		public static MemberInfo GetFieldOrProperty(this LambdaExpression expression)
		{
			MemberExpression memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentOutOfRangeException("expression", "Expected a property/field access expression, not " + expression);
			}
			return memberExpression.Member;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00014B88 File Offset: 0x00012D88
		public static MemberInfo FindProperty(LambdaExpression lambdaExpression)
		{
			Expression expression = lambdaExpression;
			bool flag = false;
			while (!flag)
			{
				ExpressionType nodeType = expression.NodeType;
				if (nodeType != ExpressionType.Convert)
				{
					if (nodeType != ExpressionType.Lambda)
					{
						if (nodeType != ExpressionType.MemberAccess)
						{
							flag = true;
						}
						else
						{
							MemberExpression memberExpression = (MemberExpression)expression;
							if (memberExpression.Expression.NodeType != ExpressionType.Parameter && memberExpression.Expression.NodeType != ExpressionType.Convert)
							{
								throw new ArgumentException(string.Format("Expression '{0}' must resolve to top-level member and not any child object's properties. Use a custom resolver on the child type or the AfterMap option instead.", lambdaExpression), "lambdaExpression");
							}
							return memberExpression.Member;
						}
					}
					else
					{
						expression = ((LambdaExpression)expression).Body;
					}
				}
				else
				{
					expression = ((UnaryExpression)expression).Operand;
				}
			}
			throw new AutoMapperConfigurationException("Custom configuration for members is only supported for top-level individual members on a type.");
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014C20 File Offset: 0x00012E20
		public static Type GetMemberType(this MemberInfo memberInfo)
		{
			if (memberInfo is MethodInfo)
			{
				return ((MethodInfo)memberInfo).ReturnType;
			}
			if (memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)memberInfo).PropertyType;
			}
			if (memberInfo is FieldInfo)
			{
				return ((FieldInfo)memberInfo).FieldType;
			}
			return null;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014C60 File Offset: 0x00012E60
		public static IMemberGetter ToMemberGetter(this MemberInfo accessorCandidate)
		{
			if (accessorCandidate == null)
			{
				return null;
			}
			if (accessorCandidate is PropertyInfo)
			{
				return new PropertyGetter((PropertyInfo)accessorCandidate);
			}
			if (accessorCandidate is FieldInfo)
			{
				return new FieldGetter((FieldInfo)accessorCandidate);
			}
			if (accessorCandidate is MethodInfo)
			{
				return new MethodGetter((MethodInfo)accessorCandidate);
			}
			return null;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00014CB8 File Offset: 0x00012EB8
		public static IMemberAccessor ToMemberAccessor(this MemberInfo accessorCandidate)
		{
			FieldInfo fieldInfo = accessorCandidate as FieldInfo;
			if (fieldInfo != null)
			{
				if (!accessorCandidate.DeclaringType.IsValueType())
				{
					return new FieldAccessor(fieldInfo);
				}
				return new ValueTypeFieldAccessor(fieldInfo);
			}
			else
			{
				PropertyInfo propertyInfo = accessorCandidate as PropertyInfo;
				if (!(propertyInfo != null))
				{
					return null;
				}
				if (!accessorCandidate.DeclaringType.IsValueType())
				{
					return new PropertyAccessor(propertyInfo);
				}
				return new ValueTypePropertyAccessor(propertyInfo);
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014D24 File Offset: 0x00012F24
		public static Type ReplaceItemType(this Type targetType, Type oldType, Type newType)
		{
			if (targetType == oldType)
			{
				return newType;
			}
			if (targetType.IsGenericType())
			{
				Type[] genericTypeArguments = targetType.GetTypeInfo().GenericTypeArguments;
				Type[] array = new Type[genericTypeArguments.Length];
				for (int i = 0; i < genericTypeArguments.Length; i++)
				{
					array[i] = genericTypeArguments[i].ReplaceItemType(oldType, newType);
				}
				return targetType.GetGenericTypeDefinition().MakeGenericType(array);
			}
			return targetType;
		}
	}
}
