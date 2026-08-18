using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02001231 RID: 4657
	internal class TreeListAggregatesHelper
	{
		// Token: 0x17003E04 RID: 15876
		// (get) Token: 0x0600C01F RID: 49183 RVA: 0x002A9C73 File Offset: 0x002A7E73
		// (set) Token: 0x0600C020 RID: 49184 RVA: 0x002A9C7A File Offset: 0x002A7E7A
		public static Dictionary<TreeListHierarchyIndex, TreeListSourceItem> AggregatedSourceItems { get; set; }

		// Token: 0x17003E05 RID: 15877
		// (get) Token: 0x0600C021 RID: 49185 RVA: 0x002A9C82 File Offset: 0x002A7E82
		// (set) Token: 0x0600C022 RID: 49186 RVA: 0x002A9C89 File Offset: 0x002A7E89
		public static Dictionary<TreeListHierarchyIndex, List<TreeListSourceItem>> AggregatesSourceItemsCollection { get; set; }

		// Token: 0x0600C023 RID: 49187 RVA: 0x002A9C94 File Offset: 0x002A7E94
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public static string PrepareFieldName(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType)
		{
			if (enumerable == null)
			{
				return "";
			}
			fieldName = TreeListEnumerableHelper.TransformDataFieldName(fieldName, queryable.ElementType);
			dataType = TreeListTypeHelper.GetNonNullableType(dataType);
			string arg = dataType.ToString().Split(new char[]
			{
				'.'
			})[1];
			bool flag = TreeListAggregatesHelper.IsRow(queryable.ElementType);
			if (dataType != typeof(string) && dataType != typeof(object))
			{
				arg = string.Format("{0}?", arg);
				if (flag)
				{
					return string.Format("iif({1} == Convert.DBNull, null, {0}({1}))", arg, fieldName);
				}
				return string.Format("{0}({1})", arg, fieldName);
			}
			else
			{
				if (dataType == typeof(string) && flag)
				{
					return string.Format("{0}({1})", "String", fieldName);
				}
				return string.Format("{0}({1})", "object", fieldName);
			}
		}

		// Token: 0x0600C024 RID: 49188 RVA: 0x002A9D6C File Offset: 0x002A7F6C
		public static bool IsRow(Type elementType)
		{
			return elementType == typeof(DataRowView) || elementType == typeof(DataRow) || elementType.GetInterface("IDataRecord") != null;
		}

		// Token: 0x0600C025 RID: 49189 RVA: 0x002A9DA8 File Offset: 0x002A7FA8
		public static object GetAggregate(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType, TreeListAggregateFunction func)
		{
			if (enumerable == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(fieldName))
			{
				fieldName = TreeListAggregatesHelper.PrepareFieldName(enumerable, queryable, fieldName, dataType);
			}
			MethodInfo method = typeof(TreeListAggregatesHelper).GetMethod("GetAggregateByType", BindingFlags.Static | BindingFlags.Public);
			if (dataType != typeof(string) && dataType != typeof(object) && !TreeListTypeHelper.IsNullableType(dataType))
			{
				dataType = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					dataType
				});
			}
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				dataType
			});
			return methodInfo.Invoke(null, new object[]
			{
				queryable,
				fieldName,
				func
			});
		}

		// Token: 0x0600C026 RID: 49190 RVA: 0x002A9E68 File Offset: 0x002A8068
		internal static Type GetPropertyType(Type itemType, string propertyName, TreeListSourceItem itemToSort, object firstItemInstance, TreeListEnumerableHelper.TreeListDataItemEvaluator itemEvaluator)
		{
			if (itemToSort != null && itemToSort.CalculatedColumns.Count > 0 && itemToSort.CalculatedColumns.ContainsKey(propertyName))
			{
				return itemToSort.CalculatedColumns[propertyName].GetType();
			}
			if (itemEvaluator == null)
			{
				itemEvaluator = new TreeListEnumerableHelper.TreeListDataItemEvaluator();
			}
			Type result = typeof(object);
			PropertyDescriptor propertyDescriptor = itemEvaluator.FindProperty(propertyName);
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.PropertyType;
			}
			PropertyInfo property = itemType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				result = property.PropertyType;
			}
			else
			{
				propertyDescriptor = itemEvaluator.FindProperty(firstItemInstance, propertyName);
				if (propertyDescriptor != null)
				{
					result = propertyDescriptor.PropertyType;
				}
			}
			return result;
		}

		// Token: 0x0600C027 RID: 49191 RVA: 0x002A9F00 File Offset: 0x002A8100
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public static object GetAggregateByType<T>(IQueryable queryable, string fieldName, TreeListAggregateFunction func)
		{
			if (func == TreeListAggregateFunction.First)
			{
				return ((IQueryable<T>)queryable.Take(1).Select(fieldName, new object[0])).First<T>();
			}
			IQueryable<T> source;
			if (!string.IsNullOrEmpty(fieldName))
			{
				source = (IQueryable<T>)queryable.Select(fieldName, new object[0]);
			}
			else
			{
				source = queryable.OfType<T>().AsQueryable<T>();
			}
			Type nonNullableType = TreeListTypeHelper.GetNonNullableType(typeof(T));
			if (func == TreeListAggregateFunction.Last)
			{
				return source.Last<T>();
			}
			if (func == TreeListAggregateFunction.Avg)
			{
				if (nonNullableType == typeof(short))
				{
					return source.Cast<short>().Average((short n) => (int)((short)n));
				}
				if (nonNullableType == typeof(int))
				{
					return source.Cast<int>().Average();
				}
				if (nonNullableType == typeof(long))
				{
					return source.Cast<long>().Average((long n) => (long)n);
				}
				if (nonNullableType == typeof(long))
				{
					return source.Cast<long>().Average((long n) => (long)n);
				}
				if (nonNullableType == typeof(decimal))
				{
					return source.Cast<decimal>().Average();
				}
				if (nonNullableType == typeof(float))
				{
					return source.Cast<float>().Average((float n) => (float)n);
				}
				if (nonNullableType == typeof(double))
				{
					return source.Cast<double>().Average();
				}
				if (nonNullableType == typeof(uint))
				{
					return source.Cast<uint>().Average((uint n) => (long)((uint)n));
				}
				if (nonNullableType == typeof(short))
				{
					return source.Cast<short>().Average((short n) => (int)((short)n));
				}
				if (nonNullableType == typeof(ushort))
				{
					return source.Cast<ushort>().Average((ushort n) => (int)((ushort)n));
				}
				throw new NotSupportedException(string.Format("Average is not supported for type \"{0}\"", nonNullableType));
			}
			else if (func == TreeListAggregateFunction.Sum)
			{
				if (nonNullableType == typeof(short))
				{
					return source.Cast<short>().Sum((short n) => (int)((short)n));
				}
				if (nonNullableType == typeof(int))
				{
					return source.Cast<int>().Sum();
				}
				if (nonNullableType == typeof(long))
				{
					return source.Cast<long>().Sum((long n) => (long)n);
				}
				if (nonNullableType == typeof(long))
				{
					return source.Cast<long>().Sum((long n) => (long)n);
				}
				if (nonNullableType == typeof(decimal))
				{
					return source.Cast<decimal>().Sum();
				}
				if (nonNullableType == typeof(float))
				{
					return source.Cast<float>().Sum((float n) => (float)n);
				}
				if (nonNullableType == typeof(double))
				{
					return source.Cast<double>().Sum();
				}
				if (nonNullableType == typeof(uint))
				{
					return source.Cast<uint>().Sum((uint n) => (long)((uint)n));
				}
				if (nonNullableType == typeof(short))
				{
					return source.Cast<short>().Sum((short n) => (int)((short)n));
				}
				if (nonNullableType == typeof(ushort))
				{
					return source.Cast<ushort>().Sum((ushort n) => (int)((ushort)n));
				}
				throw new NotSupportedException(string.Format("Sum is not supported for type \"{0}\"", typeof(T)));
			}
			else
			{
				if (func == TreeListAggregateFunction.CountDistinct)
				{
					return source.Distinct<T>().Count<T>();
				}
				if (func == TreeListAggregateFunction.Count)
				{
					return source.Count<T>();
				}
				if (func == TreeListAggregateFunction.Max)
				{
					return source.Max<T>();
				}
				if (func == TreeListAggregateFunction.Min)
				{
					return source.Min<T>();
				}
				return null;
			}
		}
	}
}
