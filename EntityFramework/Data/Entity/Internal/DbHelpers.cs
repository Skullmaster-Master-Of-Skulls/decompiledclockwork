using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000766 RID: 1894
	internal static class DbHelpers
	{
		// Token: 0x06005557 RID: 21847 RVA: 0x00172F60 File Offset: 0x00171160
		public static bool KeyValuesEqual(object x, object y)
		{
			if (x is DBNull)
			{
				x = null;
			}
			if (y is DBNull)
			{
				y = null;
			}
			if (object.Equals(x, y))
			{
				return true;
			}
			byte[] array = x as byte[];
			byte[] array2 = y as byte[];
			if (array == null || array2 == null || array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x00172FC8 File Offset: 0x001711C8
		public static bool PropertyValuesEqual(object x, object y)
		{
			if (x is DBNull)
			{
				x = null;
			}
			if (y is DBNull)
			{
				y = null;
			}
			if (x == null)
			{
				return y == null;
			}
			if (x.GetType().IsValueType() && object.Equals(x, y))
			{
				return true;
			}
			string text = x as string;
			if (text != null)
			{
				return text.Equals(y as string, StringComparison.Ordinal);
			}
			byte[] array = x as byte[];
			if (array == null)
			{
				return object.ReferenceEquals(x, y);
			}
			byte[] array2 = y as byte[];
			if (array2 == null || array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00173062 File Offset: 0x00171262
		public static string QuoteIdentifier(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x00173083 File Offset: 0x00171283
		public static bool TreatAsConnectionString(string nameOrConnectionString)
		{
			return nameOrConnectionString.IndexOf('=') >= 0;
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x00173094 File Offset: 0x00171294
		public static bool TryGetConnectionName(string nameOrConnectionString, out string name)
		{
			int num = nameOrConnectionString.IndexOf('=');
			if (num < 0)
			{
				name = nameOrConnectionString;
				return true;
			}
			if (nameOrConnectionString.IndexOf('=', num + 1) >= 0)
			{
				name = null;
				return false;
			}
			if (nameOrConnectionString.Substring(0, num).Trim().Equals("name", StringComparison.OrdinalIgnoreCase))
			{
				name = nameOrConnectionString.Substring(num + 1).Trim();
				return true;
			}
			name = null;
			return false;
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x00173100 File Offset: 0x00171300
		public static bool IsFullEFConnectionString(string nameOrConnectionString)
		{
			IEnumerable<string> source = from t in nameOrConnectionString.ToUpperInvariant().Split(new char[]
			{
				'=',
				';'
			})
			select t.Trim();
			return source.Contains("PROVIDER") && source.Contains("PROVIDER CONNECTION STRING") && source.Contains("METADATA");
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x00173174 File Offset: 0x00171374
		public static string ParsePropertySelector<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property, string methodName, string paramName)
		{
			string text;
			if (!DbHelpers.TryParsePath(property.Body, out text) || text == null)
			{
				throw new ArgumentException(Strings.DbEntityEntry_BadPropertyExpression(methodName, typeof(TEntity).Name), paramName);
			}
			return text;
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x001731B0 File Offset: 0x001713B0
		public static bool TryParsePath(Expression expression, out string path)
		{
			path = null;
			Expression expression2 = expression.RemoveConvert();
			MemberExpression memberExpression = expression2 as MemberExpression;
			MethodCallExpression methodCallExpression = expression2 as MethodCallExpression;
			if (memberExpression != null)
			{
				string name = memberExpression.Member.Name;
				string text;
				if (!DbHelpers.TryParsePath(memberExpression.Expression, out text))
				{
					return false;
				}
				path = ((text == null) ? name : (text + "." + name));
			}
			else if (methodCallExpression != null)
			{
				if (methodCallExpression.Method.Name == "Select" && methodCallExpression.Arguments.Count == 2)
				{
					string text2;
					if (!DbHelpers.TryParsePath(methodCallExpression.Arguments[0], out text2))
					{
						return false;
					}
					if (text2 != null)
					{
						LambdaExpression lambdaExpression = methodCallExpression.Arguments[1] as LambdaExpression;
						if (lambdaExpression != null)
						{
							string text3;
							if (!DbHelpers.TryParsePath(lambdaExpression.Body, out text3))
							{
								return false;
							}
							if (text3 != null)
							{
								path = text2 + "." + text3;
								return true;
							}
						}
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x001732A8 File Offset: 0x001714A8
		public static IDictionary<string, Type> GetPropertyTypes(Type type)
		{
			IDictionary<string, Type> dictionary;
			if (!DbHelpers._propertyTypes.TryGetValue(type, out dictionary))
			{
				IEnumerable<PropertyInfo> enumerable = from p in type.GetInstanceProperties()
				where p.GetIndexParameters().Length == 0
				select p;
				dictionary = new Dictionary<string, Type>(enumerable.Count<PropertyInfo>());
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					dictionary[propertyInfo.Name] = propertyInfo.PropertyType;
				}
				DbHelpers._propertyTypes.TryAdd(type, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x001733A0 File Offset: 0x001715A0
		public static IDictionary<string, Action<object, object>> GetPropertySetters(Type type)
		{
			IDictionary<string, Action<object, object>> dictionary;
			if (!DbHelpers._propertySetters.TryGetValue(type, out dictionary))
			{
				IEnumerable<PropertyInfo> source = from p in type.GetInstanceProperties()
				where p.GetIndexParameters().Length == 0
				select p;
				dictionary = new Dictionary<string, Action<object, object>>(source.Count<PropertyInfo>());
				foreach (PropertyInfo propertyInfo in from p in source
				select p.GetPropertyInfoForSet())
				{
					MethodInfo methodInfo = propertyInfo.Setter();
					if (methodInfo != null)
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "value");
						ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "instance");
						MethodCallExpression body = Expression.Call(Expression.Convert(parameterExpression2, type), methodInfo, new Expression[]
						{
							Expression.Convert(parameterExpression, propertyInfo.PropertyType)
						});
						Action<object, object> setter = Expression.Lambda<Action<object, object>>(body, new ParameterExpression[]
						{
							parameterExpression2,
							parameterExpression
						}).Compile();
						MethodInfo method = DbHelpers.ConvertAndSetMethod.MakeGenericMethod(new Type[]
						{
							propertyInfo.PropertyType
						});
						Action<object, object, Action<object, object>, string, string> convertAndSet = (Action<object, object, Action<object, object>, string, string>)Delegate.CreateDelegate(typeof(Action<object, object, Action<object, object>, string, string>), method);
						string propertyName = propertyInfo.Name;
						dictionary[propertyInfo.Name] = delegate(object i, object v)
						{
							convertAndSet(i, v, setter, propertyName, type.Name);
						};
					}
				}
				DbHelpers._propertySetters.TryAdd(type, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x0017359C File Offset: 0x0017179C
		private static void ConvertAndSet<T>(object instance, object value, Action<object, object> setter, string propertyName, string typeName)
		{
			if (value == null && typeof(T).IsValueType() && Nullable.GetUnderlyingType(typeof(T)) == null)
			{
				throw Error.DbPropertyValues_CannotSetNullValue(propertyName, typeof(T).Name, typeName);
			}
			setter(instance, (T)((object)value));
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x0017360C File Offset: 0x0017180C
		public static IDictionary<string, Func<object, object>> GetPropertyGetters(Type type)
		{
			IDictionary<string, Func<object, object>> dictionary;
			if (!DbHelpers._propertyGetters.TryGetValue(type, out dictionary))
			{
				IEnumerable<PropertyInfo> enumerable = from p in type.GetInstanceProperties()
				where p.GetIndexParameters().Length == 0
				select p;
				dictionary = new Dictionary<string, Func<object, object>>(enumerable.Count<PropertyInfo>());
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					MethodInfo methodInfo = propertyInfo.Getter();
					if (methodInfo != null)
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
						UnaryExpression body = Expression.Convert(Expression.Call(Expression.Convert(parameterExpression, type), methodInfo), typeof(object));
						dictionary[propertyInfo.Name] = Expression.Lambda<Func<object, object>>(body, new ParameterExpression[]
						{
							parameterExpression
						}).Compile();
					}
				}
				DbHelpers._propertyGetters.TryAdd(type, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x00173714 File Offset: 0x00171914
		public static IQueryable CreateNoTrackingQuery(ObjectQuery query)
		{
			ObjectQuery objectQuery = (ObjectQuery)((IQueryable)query).Provider.CreateQuery(((IQueryable)query).Expression);
			objectQuery.ExecutionStrategy = query.ExecutionStrategy;
			objectQuery.MergeOption = MergeOption.NoTracking;
			objectQuery.Streaming = query.Streaming;
			return objectQuery;
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x0017375C File Offset: 0x0017195C
		public static IQueryable CreateStreamingQuery(ObjectQuery query)
		{
			ObjectQuery objectQuery = (ObjectQuery)((IQueryable)query).Provider.CreateQuery(((IQueryable)query).Expression);
			objectQuery.ExecutionStrategy = query.ExecutionStrategy;
			objectQuery.Streaming = true;
			objectQuery.MergeOption = query.MergeOption;
			return objectQuery;
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x001737A4 File Offset: 0x001719A4
		public static IQueryable CreateQueryWithExecutionStrategy(ObjectQuery query, IDbExecutionStrategy executionStrategy)
		{
			ObjectQuery objectQuery = (ObjectQuery)((IQueryable)query).Provider.CreateQuery(((IQueryable)query).Expression);
			objectQuery.ExecutionStrategy = executionStrategy;
			objectQuery.MergeOption = query.MergeOption;
			objectQuery.Streaming = query.Streaming;
			return objectQuery;
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x00173A74 File Offset: 0x00171C74
		public static IEnumerable<DbValidationError> SplitValidationResults(string propertyName, IEnumerable<ValidationResult> validationResults)
		{
			foreach (ValidationResult validationResult in validationResults)
			{
				if (validationResult != null)
				{
					IEnumerable<string> enumerable;
					if (validationResult.MemberNames != null && validationResult.MemberNames.Any<string>())
					{
						enumerable = validationResult.MemberNames;
					}
					else
					{
						string[] array = new string[1];
						enumerable = array;
					}
					IEnumerable<string> memberNames = enumerable;
					foreach (string memberName in memberNames)
					{
						yield return new DbValidationError(memberName ?? propertyName, validationResult.ErrorMessage);
					}
				}
			}
			yield break;
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x00173A98 File Offset: 0x00171C98
		public static string GetPropertyPath(InternalMemberEntry property)
		{
			return string.Join(".", DbHelpers.GetPropertyPathSegments(property).Reverse<string>());
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x00173BAC File Offset: 0x00171DAC
		private static IEnumerable<string> GetPropertyPathSegments(InternalMemberEntry property)
		{
			do
			{
				yield return property.Name;
				property = ((property is InternalNestedPropertyEntry) ? ((InternalNestedPropertyEntry)property).ParentPropertyEntry : null);
			}
			while (property != null);
			yield break;
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x00173BF4 File Offset: 0x00171DF4
		public static Type CollectionType(Type elementType)
		{
			return DbHelpers._collectionTypes.GetOrAdd(elementType, (Type t) => typeof(ICollection<>).MakeGenericType(new Type[]
			{
				t
			}));
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x00173C1E File Offset: 0x00171E1E
		public static string DatabaseName(this Type contextType)
		{
			return contextType.ToString();
		}

		// Token: 0x040022B0 RID: 8880
		public static readonly MethodInfo ConvertAndSetMethod = typeof(DbHelpers).GetOnlyDeclaredMethod("ConvertAndSet");

		// Token: 0x040022B1 RID: 8881
		private static readonly ConcurrentDictionary<Type, IDictionary<string, Type>> _propertyTypes = new ConcurrentDictionary<Type, IDictionary<string, Type>>();

		// Token: 0x040022B2 RID: 8882
		private static readonly ConcurrentDictionary<Type, IDictionary<string, Action<object, object>>> _propertySetters = new ConcurrentDictionary<Type, IDictionary<string, Action<object, object>>>();

		// Token: 0x040022B3 RID: 8883
		private static readonly ConcurrentDictionary<Type, IDictionary<string, Func<object, object>>> _propertyGetters = new ConcurrentDictionary<Type, IDictionary<string, Func<object, object>>>();

		// Token: 0x040022B4 RID: 8884
		private static readonly ConcurrentDictionary<Type, Type> _collectionTypes = new ConcurrentDictionary<Type, Type>();
	}
}
