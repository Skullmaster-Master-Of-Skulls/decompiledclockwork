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
	// Token: 0x020001ED RID: 493
	internal abstract class DataFormEnumerableHelper
	{
		// Token: 0x06001164 RID: 4452
		public abstract int GetCount<TSource>(IEnumerable<TSource> source);

		// Token: 0x06001165 RID: 4453
		public abstract int GetCount(IEnumerable source);

		// Token: 0x06001166 RID: 4454
		public abstract IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize);

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0003F313 File Offset: 0x0003D513
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0003F31B File Offset: 0x0003D51B
		public virtual bool IsBoundUsingDataSourceID { get; set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0003F324 File Offset: 0x0003D524
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x0003F32C File Offset: 0x0003D52C
		public bool AllowStableSort { get; set; }

		// Token: 0x0600116B RID: 4459 RVA: 0x0003F335 File Offset: 0x0003D535
		public DataFormEnumerableHelper() : this(false)
		{
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003F33E File Offset: 0x0003D53E
		public DataFormEnumerableHelper(bool allowStableSort)
		{
			this.AllowStableSort = allowStableSort;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003F34D File Offset: 0x0003D54D
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public int GetCount(IQueryable source)
		{
			return source.Count();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003F355 File Offset: 0x0003D555
		public IQueryable GetPage(IQueryable enumerable, int startIndex, int pageSize)
		{
			return this.ApplyExplicitEFSort(enumerable).Page(startIndex, pageSize);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003F368 File Offset: 0x0003D568
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

		// Token: 0x06001170 RID: 4464 RVA: 0x0003F444 File Offset: 0x0003D644
		protected static Type GetItemType(IEnumerable source, out bool isCustomTypeDescriptor)
		{
			Type type = typeof(object);
			DataFormEnumerableHelper._customTypeDescriptorProperties = null;
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
							DataFormEnumerableHelper._customTypeDescriptorProperties = ((ICustomTypeDescriptor)enumerator.Current).GetProperties();
							isCustomTypeDescriptor = true;
						}
					}
					DataFormEnumerableHelper.TryReset(enumerator);
				}
			}
			return type;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003F57F File Offset: 0x0003D77F
		public static DataFormEnumerableHelper Instantiate(IEnumerable source)
		{
			if (source != null && (source is IQueryable || (!DataFormEnumerableHelper.IsDataReader(source) && !DataFormEnumerableHelper.IsListOfInheritedObjects(source) && DataFormEnumerableHelper.TryReset(source.GetEnumerator()))))
			{
				return new DataFormLinqEnumerableHelper(true);
			}
			return new DataFormInMemoryEnumerableHelper(true);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003F5B6 File Offset: 0x0003D7B6
		protected static bool IsDataReader(IEnumerable source)
		{
			return source != null && source.GetType().GetInterface("IDataReader") != null;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0003F5D4 File Offset: 0x0003D7D4
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

		// Token: 0x06001174 RID: 4468 RVA: 0x0003F610 File Offset: 0x0003D810
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

		// Token: 0x040004FE RID: 1278
		protected static PropertyDescriptorCollection _customTypeDescriptorProperties;
	}
}
