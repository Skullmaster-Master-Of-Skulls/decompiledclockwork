using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C3 RID: 195
	internal static class QueryableDataSourceHelper
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00024458 File Offset: 0x00022658
		internal static IQueryable AsQueryable(object o)
		{
			IQueryable queryable = o as IQueryable;
			if (queryable != null)
			{
				return queryable;
			}
			string text = o as string;
			if (text != null)
			{
				return new string[]
				{
					text
				}.AsQueryable<string>();
			}
			IEnumerable enumerable = o as IEnumerable;
			if (enumerable == null)
			{
				Type type = typeof(List<>).MakeGenericType(new Type[]
				{
					o.GetType()
				});
				IList list = (IList)DataSourceHelper.CreateObjectInstance(type);
				list.Add(o);
				return list.AsQueryable();
			}
			Type left = QueryableDataSourceHelper.FindGenericEnumerableType(o.GetType());
			if (left != null)
			{
				return enumerable.AsQueryable();
			}
			List<object> list2 = new List<object>();
			foreach (object item in enumerable)
			{
				list2.Add(item);
			}
			return list2.AsQueryable<object>();
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002454C File Offset: 0x0002274C
		public static IList ToList(this IQueryable query, Type dataObjectType)
		{
			MethodInfo methodInfo = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(new Type[]
			{
				dataObjectType
			});
			return (IList)methodInfo.Invoke(null, new object[]
			{
				query
			});
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00024594 File Offset: 0x00022794
		public static bool EnumerableContentEquals(IEnumerable enumerableA, IEnumerable enumerableB)
		{
			IEnumerator enumerator = enumerableA.GetEnumerator();
			IEnumerator enumerator2 = enumerableB.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator2.MoveNext())
				{
					return false;
				}
				object obj = enumerator.Current;
				object obj2 = enumerator2.Current;
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return !enumerator2.MoveNext();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000245F0 File Offset: 0x000227F0
		public static Type FindGenericEnumerableType(Type type)
		{
			while (type != null && type != typeof(object) && type != typeof(string))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type;
				}
				foreach (Type type2 in type.GetInterfaces())
				{
					Type type3 = QueryableDataSourceHelper.FindGenericEnumerableType(type2);
					if (type3 != null)
					{
						return type3;
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00024684 File Offset: 0x00022884
		internal static IDictionary<string, object> ToEscapedParameterKeys(this ParameterCollection parameters, HttpContext context, Control control)
		{
			if (parameters != null)
			{
				return parameters.GetValues(context, control).ToEscapedParameterKeys(control);
			}
			return null;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002469C File Offset: 0x0002289C
		internal static IDictionary<string, object> ToEscapedParameterKeys(this IDictionary parameters, Control owner)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(parameters.Count, StringComparer.OrdinalIgnoreCase);
			foreach (object obj in parameters)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (string.IsNullOrEmpty(text))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ParametersMustBeNamed, new object[]
					{
						owner.ID
					}));
				}
				QueryableDataSourceHelper.ValidateParameterName(text, owner);
				dictionary.Add("@" + text, dictionaryEntry.Value);
			}
			return dictionary;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00024758 File Offset: 0x00022958
		internal static IDictionary<string, object> ToEscapedParameterKeys(this IDictionary<string, object> parameters, Control owner)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(parameters.Count, StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, object> keyValuePair in parameters)
			{
				string key = keyValuePair.Key;
				if (string.IsNullOrEmpty(key))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ParametersMustBeNamed, new object[]
					{
						owner.ID
					}));
				}
				QueryableDataSourceHelper.ValidateParameterName(key, owner);
				dictionary.Add("@" + key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00024800 File Offset: 0x00022A00
		internal static IQueryable CreateOrderByExpression(IOrderedDictionary parameters, IQueryable source, IDynamicQueryable queryable)
		{
			if (parameters != null && parameters.Count > 0)
			{
				string orderByClause = QueryableDataSourceHelper.GetOrderByClause(parameters.ToDictionary());
				if (!string.IsNullOrEmpty(orderByClause))
				{
					return queryable.OrderBy(source, orderByClause, new object[0]);
				}
			}
			return source;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00024840 File Offset: 0x00022A40
		internal static IQueryable CreateWhereExpression(IDictionary<string, object> parameters, IQueryable source, IDynamicQueryable queryable)
		{
			if (parameters != null && parameters.Count > 0)
			{
				QueryableDataSourceHelper.WhereClause whereClause = QueryableDataSourceHelper.GetWhereClause(parameters);
				if (!string.IsNullOrEmpty(whereClause.Expression))
				{
					return queryable.Where(source, whereClause.Expression, new object[]
					{
						whereClause.Parameters
					});
				}
			}
			return source;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002488C File Offset: 0x00022A8C
		private static QueryableDataSourceHelper.WhereClause GetWhereClause(IDictionary<string, object> whereParameters)
		{
			QueryableDataSourceHelper.WhereClause whereClause = new QueryableDataSourceHelper.WhereClause();
			whereClause.Parameters = new Dictionary<string, object>(whereParameters.Count);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (KeyValuePair<string, object> keyValuePair in whereParameters)
			{
				string key = keyValuePair.Key;
				string value = (keyValuePair.Value == null) ? null : keyValuePair.Value.ToString();
				if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
				{
					string text = "@p" + num++.ToString();
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" AND ");
					}
					stringBuilder.Append(key);
					stringBuilder.Append(" == ");
					stringBuilder.Append(text);
					whereClause.Parameters.Add(text, keyValuePair.Value);
				}
			}
			whereClause.Expression = stringBuilder.ToString();
			return whereClause;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00024998 File Offset: 0x00022B98
		private static string GetOrderByClause(IDictionary<string, object> orderByParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in orderByParameters)
			{
				string value = (string)keyValuePair.Value;
				if (!string.IsNullOrEmpty(value))
				{
					string key = keyValuePair.Key;
					QueryableDataSourceHelper.ValidateOrderByParameter(key, value);
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00024A28 File Offset: 0x00022C28
		internal static void ValidateOrderByParameter(string name, string value)
		{
			if (!QueryableDataSourceHelper.AutoGenerateOrderByRegex.IsMatch(value))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_InvalidOrderByFieldName, new object[]
				{
					value,
					name
				}));
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00024A5A File Offset: 0x00022C5A
		internal static void ValidateParameterName(string name, Control owner)
		{
			if (!QueryableDataSourceHelper.IdentifierRegex.IsMatch(name))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_InvalidParameterName, new object[]
				{
					name,
					owner.ID
				}));
			}
		}

		// Token: 0x04000316 RID: 790
		private static readonly string IdentifierPattern = "^\\s*[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}_][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Nd}\\p{Pc}\\p{Mn}\\p{Mc}\\p{Cf}_]*";

		// Token: 0x04000317 RID: 791
		private static readonly Regex IdentifierRegex = new Regex(QueryableDataSourceHelper.IdentifierPattern + "\\s*$");

		// Token: 0x04000318 RID: 792
		private static readonly Regex AutoGenerateOrderByRegex = new Regex(QueryableDataSourceHelper.IdentifierPattern + "(\\s+(asc|ascending|desc|descending))?\\s*$", RegexOptions.IgnoreCase);

		// Token: 0x02000173 RID: 371
		private class WhereClause
		{
			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x0600105F RID: 4191 RVA: 0x00038280 File Offset: 0x00036480
			// (set) Token: 0x06001060 RID: 4192 RVA: 0x00038288 File Offset: 0x00036488
			public string Expression { get; set; }

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06001061 RID: 4193 RVA: 0x00038291 File Offset: 0x00036491
			// (set) Token: 0x06001062 RID: 4194 RVA: 0x00038299 File Offset: 0x00036499
			public IDictionary<string, object> Parameters { get; set; }
		}
	}
}
