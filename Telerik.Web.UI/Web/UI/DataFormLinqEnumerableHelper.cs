using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020001EF RID: 495
	internal class DataFormLinqEnumerableHelper : DataFormEnumerableHelper
	{
		// Token: 0x0600117F RID: 4479 RVA: 0x0003FD13 File Offset: 0x0003DF13
		public DataFormLinqEnumerableHelper() : base(false)
		{
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003FD1C File Offset: 0x0003DF1C
		public DataFormLinqEnumerableHelper(bool allowStableSort) : base(allowStableSort)
		{
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003FD28 File Offset: 0x0003DF28
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IQueryable ConvertToQueryable(IEnumerable source)
		{
			IQueryable queryable = source as IQueryable;
			if (queryable != null)
			{
				return queryable;
			}
			return DataFormLinqEnumerableHelper.ToGenericEnumerable(source).AsQueryable();
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003FD4C File Offset: 0x0003DF4C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IQueryable ConvertToQueryable<TSource>(IEnumerable<TSource> source)
		{
			return source.AsQueryable<TSource>();
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003FD54 File Offset: 0x0003DF54
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IQueryable ConvertToQueryable(IQueryable source)
		{
			return source;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003FD57 File Offset: 0x0003DF57
		public override int GetCount<TSource>(IEnumerable<TSource> source)
		{
			return base.GetCount(this.ConvertToQueryable<TSource>(source));
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003FD66 File Offset: 0x0003DF66
		public override int GetCount(IEnumerable source)
		{
			return base.GetCount(this.ConvertToQueryable(source));
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003FD75 File Offset: 0x0003DF75
		public override IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize)
		{
			return base.GetPage(this.ConvertToQueryable(enumerable), startIndex, pageSize);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003FD88 File Offset: 0x0003DF88
		internal static IEnumerable ToGenericEnumerable(IEnumerable source)
		{
			bool flag = false;
			Type itemType = DataFormEnumerableHelper.GetItemType(source, out flag);
			if (source.GetType().GetInterface(typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				itemType
			}).Name) != null)
			{
				return source;
			}
			Type type = typeof(DataFormLinqEnumerableHelper.DataFormGenericEnumerable<>).MakeGenericType(new Type[]
			{
				itemType
			});
			if (flag)
			{
				type = typeof(DataFormLinqEnumerableHelper.DataFormEntityGenericEnumerable<>).MakeGenericType(new Type[]
				{
					itemType
				});
			}
			return (IEnumerable)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				source
			}, null);
		}

		// Token: 0x020001F0 RID: 496
		internal class DataFormGenericEnumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06001188 RID: 4488 RVA: 0x0003FE34 File Offset: 0x0003E034
			internal DataFormGenericEnumerable(IEnumerable source)
			{
				this.source = source;
			}

			// Token: 0x06001189 RID: 4489 RVA: 0x0003FE43 File Offset: 0x0003E043
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.source.GetEnumerator();
			}

			// Token: 0x0600118A RID: 4490 RVA: 0x0003FFA0 File Offset: 0x0003E1A0
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				foreach (object obj in this.source)
				{
					T item = (T)((object)obj);
					yield return item;
				}
				yield break;
			}

			// Token: 0x04000501 RID: 1281
			private IEnumerable source;
		}

		// Token: 0x020001F1 RID: 497
		internal class DataFormEntityGenericEnumerable<T> : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x0600118B RID: 4491 RVA: 0x0003FFBC File Offset: 0x0003E1BC
			internal DataFormEntityGenericEnumerable(IEnumerable source)
			{
				this.source = source;
			}

			// Token: 0x0600118C RID: 4492 RVA: 0x0003FFCB File Offset: 0x0003E1CB
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.source.GetEnumerator();
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x00040130 File Offset: 0x0003E330
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				foreach (object item in this.source)
				{
					yield return (T)((object)(item as ICustomTypeDescriptor).GetPropertyOwner(null));
				}
				yield break;
			}

			// Token: 0x04000502 RID: 1282
			private IEnumerable source;
		}
	}
}
