using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020019AA RID: 6570
	internal class ListViewLinqEnumerableHelper : ListViewEnumerableHelper
	{
		// Token: 0x0600FE28 RID: 65064 RVA: 0x0039163F File Offset: 0x0038F83F
		public ListViewLinqEnumerableHelper() : base(false)
		{
		}

		// Token: 0x0600FE29 RID: 65065 RVA: 0x00391648 File Offset: 0x0038F848
		public ListViewLinqEnumerableHelper(bool allowStableSort) : base(allowStableSort)
		{
		}

		// Token: 0x0600FE2A RID: 65066 RVA: 0x00391654 File Offset: 0x0038F854
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IQueryable ConvertToQueryable(IEnumerable source)
		{
			IQueryable queryable = source as IQueryable;
			if (queryable != null)
			{
				return queryable;
			}
			return ListViewLinqEnumerableHelper.ToGenericEnumerable(source).AsQueryable();
		}

		// Token: 0x0600FE2B RID: 65067 RVA: 0x00391678 File Offset: 0x0038F878
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IQueryable ConvertToQueryable<TSource>(IEnumerable<TSource> source)
		{
			return source.AsQueryable<TSource>();
		}

		// Token: 0x0600FE2C RID: 65068 RVA: 0x00391680 File Offset: 0x0038F880
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IQueryable ConvertToQueryable(IQueryable source)
		{
			return source;
		}

		// Token: 0x0600FE2D RID: 65069 RVA: 0x00391683 File Offset: 0x0038F883
		public override int GetCount<TSource>(IEnumerable<TSource> source)
		{
			return base.GetCount(this.ConvertToQueryable<TSource>(source));
		}

		// Token: 0x0600FE2E RID: 65070 RVA: 0x00391692 File Offset: 0x0038F892
		public override int GetCount(IEnumerable source)
		{
			return base.GetCount(this.ConvertToQueryable(source));
		}

		// Token: 0x0600FE2F RID: 65071 RVA: 0x003916A1 File Offset: 0x0038F8A1
		public override IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize)
		{
			return base.GetPage(this.ConvertToQueryable(enumerable), startIndex, pageSize);
		}

		// Token: 0x0600FE30 RID: 65072 RVA: 0x003916B2 File Offset: 0x0038F8B2
		public override IEnumerable Sort(IEnumerable originalEnumerable, RadListViewSortExpressionCollection sortExpressions)
		{
			return base.Sort(this.ConvertToQueryable(originalEnumerable), sortExpressions);
		}

		// Token: 0x0600FE31 RID: 65073 RVA: 0x003916C2 File Offset: 0x0038F8C2
		public override IEnumerable Filter(IEnumerable source, RadListViewFilterExpressionCollection filterExpressionCollection)
		{
			return base.Filter(this.ConvertToQueryable(source), filterExpressionCollection);
		}

		// Token: 0x0600FE32 RID: 65074 RVA: 0x003916D4 File Offset: 0x0038F8D4
		internal static IEnumerable ToGenericEnumerable(IEnumerable source)
		{
			bool flag = false;
			Type itemType = ListViewEnumerableHelper.GetItemType(source, out flag);
			if (source.GetType().GetInterface(typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				itemType
			}).Name) != null)
			{
				return source;
			}
			Type type = typeof(ListViewLinqEnumerableHelper.ListViewGenericEnumerable<>).MakeGenericType(new Type[]
			{
				itemType
			});
			if (flag)
			{
				type = typeof(ListViewLinqEnumerableHelper.ListViewEntityGenericEnumerable<>).MakeGenericType(new Type[]
				{
					itemType
				});
			}
			return (IEnumerable)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				source
			}, null);
		}

		// Token: 0x020019AB RID: 6571
		internal class ListViewGenericEnumerable<T> : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x0600FE33 RID: 65075 RVA: 0x00391780 File Offset: 0x0038F980
			internal ListViewGenericEnumerable(IEnumerable source)
			{
				this.source = source;
			}

			// Token: 0x0600FE34 RID: 65076 RVA: 0x0039178F File Offset: 0x0038F98F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.source.GetEnumerator();
			}

			// Token: 0x0600FE35 RID: 65077 RVA: 0x003918EC File Offset: 0x0038FAEC
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				foreach (object obj in this.source)
				{
					T item = (T)((object)obj);
					yield return item;
				}
				yield break;
			}

			// Token: 0x0400481D RID: 18461
			private IEnumerable source;
		}

		// Token: 0x020019AC RID: 6572
		internal class ListViewEntityGenericEnumerable<T> : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x0600FE36 RID: 65078 RVA: 0x00391908 File Offset: 0x0038FB08
			internal ListViewEntityGenericEnumerable(IEnumerable source)
			{
				this.source = source;
			}

			// Token: 0x0600FE37 RID: 65079 RVA: 0x00391917 File Offset: 0x0038FB17
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.source.GetEnumerator();
			}

			// Token: 0x0600FE38 RID: 65080 RVA: 0x00391A7C File Offset: 0x0038FC7C
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				foreach (object item in this.source)
				{
					yield return (T)((object)(item as ICustomTypeDescriptor).GetPropertyOwner(null));
				}
				yield break;
			}

			// Token: 0x0400481E RID: 18462
			private IEnumerable source;
		}
	}
}
