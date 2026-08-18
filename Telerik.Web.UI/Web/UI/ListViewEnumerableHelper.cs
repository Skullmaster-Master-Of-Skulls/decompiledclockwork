using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Web.Data;
using Telerik.Web.Data.Extensions;

namespace Telerik.Web.UI
{
	// Token: 0x020019A8 RID: 6568
	internal abstract class ListViewEnumerableHelper
	{
		// Token: 0x0600FE04 RID: 65028
		public abstract int GetCount<TSource>(IEnumerable<TSource> source);

		// Token: 0x0600FE05 RID: 65029
		public abstract int GetCount(IEnumerable source);

		// Token: 0x0600FE06 RID: 65030
		public abstract IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize);

		// Token: 0x0600FE07 RID: 65031
		public abstract IEnumerable Sort(IEnumerable originalEnumerable, RadListViewSortExpressionCollection sortExpressions);

		// Token: 0x0600FE08 RID: 65032
		public abstract IEnumerable Filter(IEnumerable source, RadListViewFilterExpressionCollection filterExpressionCollection);

		// Token: 0x17004CB9 RID: 19641
		// (get) Token: 0x0600FE09 RID: 65033 RVA: 0x0039052B File Offset: 0x0038E72B
		// (set) Token: 0x0600FE0A RID: 65034 RVA: 0x00390533 File Offset: 0x0038E733
		public virtual bool IsBoundUsingDataSourceID { get; set; }

		// Token: 0x17004CBA RID: 19642
		// (get) Token: 0x0600FE0B RID: 65035 RVA: 0x0039053C File Offset: 0x0038E73C
		// (set) Token: 0x0600FE0C RID: 65036 RVA: 0x00390544 File Offset: 0x0038E744
		public bool AllowStableSort { get; set; }

		// Token: 0x0600FE0D RID: 65037 RVA: 0x0039054D File Offset: 0x0038E74D
		public ListViewEnumerableHelper() : this(false)
		{
		}

		// Token: 0x0600FE0E RID: 65038 RVA: 0x00390556 File Offset: 0x0038E756
		public ListViewEnumerableHelper(bool allowStableSort)
		{
			this.AllowStableSort = allowStableSort;
		}

		// Token: 0x0600FE0F RID: 65039 RVA: 0x00390565 File Offset: 0x0038E765
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public int GetCount(IQueryable source)
		{
			return source.Count();
		}

		// Token: 0x0600FE10 RID: 65040 RVA: 0x0039056D File Offset: 0x0038E76D
		public IQueryable GetPage(IQueryable enumerable, int startIndex, int pageSize)
		{
			return this.ApplyExplicitEFSort(enumerable).Page(startIndex, pageSize);
		}

		// Token: 0x0600FE11 RID: 65041 RVA: 0x00390580 File Offset: 0x0038E780
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private IQueryable ApplyExplicitEFSort(IQueryable source)
		{
			Type elementType = source.ElementType;
			if (!this.IsBoundUsingDataSourceID && elementType != null && (elementType.InheritsFrom("EntityDataSourceWrapper") || elementType.InheritsFrom("EntityObject") || elementType.InheritsFrom("ComplexObject")))
			{
				if (source.Expression is MethodCallExpression)
				{
					string name = ((MethodCallExpression)source.Expression).Method.Name;
					if (name == "OrderBy" || name == "OrderByDescending" || name == "ThenBy" || name == "ThenByDescending")
					{
						return source;
					}
				}
				return source.Sort(new SortDescriptor[]
				{
					new SortDescriptor
					{
						Member = elementType.FirstSortablePropertyName(),
						SortDirection = ListSortDirection.Ascending
					}
				});
			}
			return source;
		}

		// Token: 0x0600FE12 RID: 65042 RVA: 0x0039065B File Offset: 0x0038E85B
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public IQueryable Sort(IQueryable originalEnumerable, RadListViewSortExpressionCollection sortExpressions)
		{
			return originalEnumerable.Sort(sortExpressions.GetSortDescriptors());
		}

		// Token: 0x0600FE13 RID: 65043 RVA: 0x0039066C File Offset: 0x0038E86C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public IQueryable Filter(IQueryable source, RadListViewFilterExpressionCollection filterExpressionCollection)
		{
			IEnumerable<IFilterDescriptor> enumerable = new WPFDataEngineExpressionBuilder(filterExpressionCollection).Build();
			if (enumerable != null)
			{
				return source.Where(enumerable);
			}
			return source;
		}

		// Token: 0x0600FE14 RID: 65044 RVA: 0x00390694 File Offset: 0x0038E894
		protected static Type GetItemType(IEnumerable source, out bool isCustomTypeDescriptor)
		{
			Type type = typeof(object);
			ListViewEnumerableHelper._customTypeDescriptorProperties = null;
			isCustomTypeDescriptor = false;
			DataView dataView = source as DataView;
			if (dataView != null && dataView.Count == 0)
			{
				return typeof(DataRowView);
			}
			bool flag = false;
			Type type2 = null;
			if (source.GetType().IsGenericType && (!source.GetType().IsNested || source.GetType().IsGenericTypeDefinition))
			{
				Type[] genericArguments = source.GetType().GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					flag = true;
					type2 = genericArguments[0];
				}
			}
			if (flag)
			{
				type = type2;
			}
			else
			{
				IEnumerator enumerator = source.GetEnumerator();
				if (enumerator != null && enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						type = enumerator.Current.GetType();
						if (type != null && type.Name == "EntityDataSourceWrapper" && enumerator.Current is ICustomTypeDescriptor)
						{
							object propertyOwner = (enumerator.Current as ICustomTypeDescriptor).GetPropertyOwner(null);
							type = propertyOwner.GetType();
							isCustomTypeDescriptor = true;
						}
						if (enumerator.Current is ICustomTypeDescriptor)
						{
							ListViewEnumerableHelper._customTypeDescriptorProperties = ((ICustomTypeDescriptor)enumerator.Current).GetProperties();
							isCustomTypeDescriptor = true;
						}
					}
					ListViewEnumerableHelper.TryReset(enumerator);
				}
			}
			return type;
		}

		// Token: 0x0600FE15 RID: 65045 RVA: 0x003907D0 File Offset: 0x0038E9D0
		public static ListViewEnumerableHelper Instantiate(IEnumerable source, bool allowStableSort)
		{
			if (source is ImageGalleryItemCollection)
			{
				return new ListViewInMemoryEnumerableHelper(allowStableSort);
			}
			if (source != null && (source is IQueryable || (!ListViewEnumerableHelper.IsDataReader(source) && !ListViewEnumerableHelper.IsListOfInheritedObjects(source) && ListViewEnumerableHelper.TryReset(source.GetEnumerator()))))
			{
				return new ListViewLinqEnumerableHelper(allowStableSort);
			}
			return new ListViewInMemoryEnumerableHelper(allowStableSort);
		}

		// Token: 0x0600FE16 RID: 65046 RVA: 0x00390821 File Offset: 0x0038EA21
		protected static bool IsDataReader(IEnumerable source)
		{
			return source != null && source.GetType().GetInterface("IDataReader") != null;
		}

		// Token: 0x0600FE17 RID: 65047 RVA: 0x00390840 File Offset: 0x0038EA40
		public static bool TryReset(IEnumerator enumerator)
		{
			bool result = false;
			try
			{
				enumerator.Reset();
				result = true;
			}
			catch (NotSupportedException)
			{
			}
			catch (NotImplementedException)
			{
			}
			return result;
		}

		// Token: 0x0600FE18 RID: 65048 RVA: 0x0039087C File Offset: 0x0038EA7C
		protected static bool IsListOfInheritedObjects(IEnumerable source)
		{
			IList list = source as IList;
			if (list != null && list.Count > 1)
			{
				object obj = list[0];
				object obj2 = list[1];
				if (obj != null && obj2 != null)
				{
					return obj.GetType() != obj2.GetType();
				}
			}
			return false;
		}

		// Token: 0x0400481A RID: 18458
		protected static PropertyDescriptorCollection _customTypeDescriptorProperties;
	}
}
