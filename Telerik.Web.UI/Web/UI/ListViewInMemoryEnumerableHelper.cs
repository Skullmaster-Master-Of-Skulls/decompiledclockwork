using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020019A9 RID: 6569
	internal class ListViewInMemoryEnumerableHelper : ListViewEnumerableHelper
	{
		// Token: 0x0600FE19 RID: 65049 RVA: 0x003908C5 File Offset: 0x0038EAC5
		public ListViewInMemoryEnumerableHelper() : this(false)
		{
		}

		// Token: 0x0600FE1A RID: 65050 RVA: 0x003908CE File Offset: 0x0038EACE
		internal ListViewInMemoryEnumerableHelper(bool allowStableSort) : base(allowStableSort)
		{
		}

		// Token: 0x0600FE1B RID: 65051 RVA: 0x003908D8 File Offset: 0x0038EAD8
		public override int GetCount<TSource>(IEnumerable<TSource> source)
		{
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count;
			}
			return this.GetCount(source);
		}

		// Token: 0x0600FE1C RID: 65052 RVA: 0x00390900 File Offset: 0x0038EB00
		public override int GetCount(IEnumerable source)
		{
			ICollection collection = source as ICollection;
			if (collection != null)
			{
				return collection.Count;
			}
			Array array = source as Array;
			if (array != null)
			{
				return array.Length;
			}
			int num = 0;
			foreach (object obj in source)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600FE1D RID: 65053 RVA: 0x00390C4C File Offset: 0x0038EE4C
		public override IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize)
		{
			startIndex = Math.Max(startIndex, 0);
			if (enumerable is IList)
			{
				IList list = (IList)enumerable;
				int itemCounter = 0;
				for (int i = startIndex; i < list.Count; i++)
				{
					yield return list[i];
					itemCounter++;
					if (pageSize == itemCounter)
					{
						break;
					}
				}
			}
			else
			{
				int index = 0;
				foreach (object item in enumerable)
				{
					if (index < startIndex)
					{
						index++;
					}
					else
					{
						yield return item;
						index++;
						if (pageSize + startIndex == index)
						{
							yield break;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600FE1E RID: 65054 RVA: 0x00390D5C File Offset: 0x0038EF5C
		public override IEnumerable Sort(IEnumerable originalEnumerable, RadListViewSortExpressionCollection sortExpressions)
		{
			bool isCustomTypeDescriptor;
			Type itemType = ListViewEnumerableHelper.GetItemType(originalEnumerable, out isCustomTypeDescriptor);
			TFunc<IEnumerable, IEnumerable> tfunc = delegate(IEnumerable input)
			{
				IEnumerable enumerable = input;
				bool flag = true;
				foreach (object obj in sortExpressions)
				{
					RadListViewSortExpression radListViewSortExpression = (RadListViewSortExpression)obj;
					if (radListViewSortExpression.SortOrder != RadListViewSortOrder.None)
					{
						if (flag)
						{
							enumerable = this.SortByField(enumerable, itemType, radListViewSortExpression.FieldName, this.IsDesending(radListViewSortExpression.SortOrder), isCustomTypeDescriptor);
							flag = false;
						}
						else
						{
							enumerable = this.ThenBy((IOrderedEnumerable<object>)enumerable, itemType, radListViewSortExpression.FieldName, this.IsDesending(radListViewSortExpression.SortOrder), isCustomTypeDescriptor);
						}
					}
				}
				return enumerable;
			};
			return tfunc(originalEnumerable);
		}

		// Token: 0x0600FE1F RID: 65055 RVA: 0x00390DA3 File Offset: 0x0038EFA3
		private bool IsDesending(RadListViewSortOrder sortOrder)
		{
			return sortOrder == RadListViewSortOrder.Descending;
		}

		// Token: 0x0600FE20 RID: 65056 RVA: 0x00390DAC File Offset: 0x0038EFAC
		private IEnumerable ThenBy(IOrderedEnumerable<object> input, Type itemType, string propertyName, bool sortOrder, bool isCustomTypeDescriptor)
		{
			Type propertyType = ListViewInMemoryEnumerableHelper.GetPropertyType(itemType, propertyName, isCustomTypeDescriptor);
			if (!RadListView.IsBindableType(propertyType))
			{
				return input;
			}
			bool allowStableSort = base.AllowStableSort;
			if (propertyType == typeof(string))
			{
				return input.CreateOrderedEnumerable<string>(this.GetEvalFunc<string>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(int) || propertyType == typeof(int?))
			{
				return input.CreateOrderedEnumerable<int?>(this.GetEvalFunc<int?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(short) || propertyType == typeof(short?))
			{
				return input.CreateOrderedEnumerable<short?>(this.GetEvalFunc<short?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(long) || propertyType == typeof(long?))
			{
				return input.CreateOrderedEnumerable<long?>(this.GetEvalFunc<long?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
			{
				return input.CreateOrderedEnumerable<DateTime?>(this.GetEvalFunc<DateTime?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
			{
				return input.CreateOrderedEnumerable<decimal?>(this.GetEvalFunc<decimal?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeSpan?))
			{
				return input.CreateOrderedEnumerable<TimeSpan?>(this.GetEvalFunc<TimeSpan?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
			{
				return input.CreateOrderedEnumerable<Guid?>(this.GetEvalFunc<Guid?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(bool) || propertyType == typeof(bool?))
			{
				return input.CreateOrderedEnumerable<bool?>(this.GetEvalFunc<bool?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(double) || propertyType == typeof(double?))
			{
				return input.CreateOrderedEnumerable<double?>(this.GetEvalFunc<double?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return input.CreateOrderedEnumerable<float?>(this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return input.CreateOrderedEnumerable<float?>(this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			return input;
		}

		// Token: 0x0600FE21 RID: 65057 RVA: 0x00391088 File Offset: 0x0038F288
		private TFunc<object, TResult> GetEvalFunc<TResult>(string propertyName)
		{
			return delegate(object element)
			{
				object obj = DataBinder.Eval(element, propertyName);
				if (obj == Convert.DBNull)
				{
					return default(TResult);
				}
				return (TResult)((object)obj);
			};
		}

		// Token: 0x0600FE22 RID: 65058 RVA: 0x003910B0 File Offset: 0x0038F2B0
		private IEnumerable SortByField(IEnumerable input, Type itemType, string propertyName, bool sortOrder, bool isCustomTypeDescriptor)
		{
			Type propertyType = ListViewInMemoryEnumerableHelper.GetPropertyType(itemType, propertyName, isCustomTypeDescriptor);
			if (!RadListView.IsBindableType(propertyType))
			{
				return input;
			}
			bool allowStableSort = base.AllowStableSort;
			if (propertyType == typeof(string))
			{
				return new OrderByEnumerable<object, string>(input, this.GetEvalFunc<string>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(int) || propertyType == typeof(int?))
			{
				return new OrderByEnumerable<object, int?>(input, this.GetEvalFunc<int?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(short) || propertyType == typeof(short?))
			{
				return new OrderByEnumerable<object, short?>(input, this.GetEvalFunc<short?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(long) || propertyType == typeof(long?))
			{
				return new OrderByEnumerable<object, long>(input, this.GetEvalFunc<long>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
			{
				return new OrderByEnumerable<object, DateTime?>(input, this.GetEvalFunc<DateTime?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
			{
				return new OrderByEnumerable<object, decimal?>(input, this.GetEvalFunc<decimal?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(TimeSpan) || propertyType == typeof(TimeSpan?))
			{
				return new OrderByEnumerable<object, TimeSpan?>(input, this.GetEvalFunc<TimeSpan?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
			{
				return new OrderByEnumerable<object, Guid?>(input, this.GetEvalFunc<Guid?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(bool) || propertyType == typeof(bool?))
			{
				return new OrderByEnumerable<object, bool?>(input, this.GetEvalFunc<bool?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(double) || propertyType == typeof(double?))
			{
				return new OrderByEnumerable<object, double?>(input, this.GetEvalFunc<double?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return new OrderByEnumerable<object, float?>(input, this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			if (propertyType == typeof(float) || propertyType == typeof(float?))
			{
				return new OrderByEnumerable<object, float?>(input, this.GetEvalFunc<float?>(propertyName), null, sortOrder, allowStableSort);
			}
			return input;
		}

		// Token: 0x0600FE23 RID: 65059 RVA: 0x00391350 File Offset: 0x0038F550
		private static Type GetPropertyType(Type itemType, string propertyName, bool isCustomTypeDescriptor)
		{
			Type result = typeof(object);
			if (isCustomTypeDescriptor && ListViewEnumerableHelper._customTypeDescriptorProperties != null)
			{
				PropertyDescriptor propertyDescriptor = ListViewEnumerableHelper._customTypeDescriptorProperties.Find(propertyName, true);
				if (propertyDescriptor != null)
				{
					result = propertyDescriptor.PropertyType;
				}
			}
			else
			{
				PropertyInfo property = itemType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (property != null)
				{
					result = property.PropertyType;
				}
			}
			return result;
		}

		// Token: 0x0600FE24 RID: 65060 RVA: 0x003913A6 File Offset: 0x0038F5A6
		public static TSource[] ToArray<TSource>(IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ListViewInMemoryEnumerableHelper.ToList<TSource>(source).ToArray();
		}

		// Token: 0x0600FE25 RID: 65061 RVA: 0x003913C1 File Offset: 0x0038F5C1
		public static List<TSource> ToList<TSource>(IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new List<TSource>(source);
		}

		// Token: 0x0600FE26 RID: 65062 RVA: 0x003913D8 File Offset: 0x0038F5D8
		public override IEnumerable Filter(IEnumerable source, RadListViewFilterExpressionCollection filterExpressionCollection)
		{
			foreach (RadListViewFilterExpression radListViewFilterExpression in filterExpressionCollection)
			{
				source = this.Where(source, radListViewFilterExpression.ToPredicate());
			}
			return source;
		}

		// Token: 0x0600FE27 RID: 65063 RVA: 0x00391614 File Offset: 0x0038F814
		protected IEnumerable Where(IEnumerable source, Predicate<object> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (object item in source)
			{
				if (predicate(item))
				{
					yield return item;
				}
			}
			yield break;
		}
	}
}
