using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	// Token: 0x020000C4 RID: 196
	public class FilterProviderCollection : Collection<IFilterProvider>
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000E79B File Offset: 0x0000C99B
		public FilterProviderCollection()
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000E7A3 File Offset: 0x0000C9A3
		public FilterProviderCollection(IList<IFilterProvider> providers) : base(providers)
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		internal FilterProviderCollection(IList<IFilterProvider> list, IDependencyResolver dependencyResolver) : base(list)
		{
			this._dependencyResolver = dependencyResolver;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		internal IFilterProvider[] CombinedItems
		{
			get
			{
				IFilterProvider[] array = this._combinedItems;
				if (array == null)
				{
					array = MultiServiceResolver.GetCombined<IFilterProvider>(base.Items, this._dependencyResolver);
					this._combinedItems = array;
				}
				return array;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		private static bool AllowMultiple(object filterInstance)
		{
			IMvcFilter mvcFilter = filterInstance as IMvcFilter;
			return mvcFilter == null || mvcFilter.AllowMultiple;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000E810 File Offset: 0x0000CA10
		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			IFilterProvider[] combinedItems = this.CombinedItems;
			List<Filter> list = new List<Filter>();
			foreach (IFilterProvider filterProvider in combinedItems)
			{
				foreach (Filter item in filterProvider.GetFilters(controllerContext, actionDescriptor))
				{
					list.Add(item);
				}
			}
			list.Sort(FilterProviderCollection._filterComparer);
			if (list.Count > 1)
			{
				FilterProviderCollection.RemoveDuplicates(list);
			}
			return list;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000E8BC File Offset: 0x0000CABC
		private static void RemoveDuplicates(List<Filter> filters)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			for (int i = filters.Count - 1; i >= 0; i--)
			{
				Filter filter = filters[i];
				object instance = filter.Instance;
				Type type = instance.GetType();
				if (!hashSet.Contains(type) || FilterProviderCollection.AllowMultiple(instance))
				{
					hashSet.Add(type);
				}
				else
				{
					filters.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000E91D File Offset: 0x0000CB1D
		protected override void ClearItems()
		{
			this._combinedItems = null;
			base.ClearItems();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000E92C File Offset: 0x0000CB2C
		protected override void InsertItem(int index, IFilterProvider item)
		{
			this._combinedItems = null;
			base.InsertItem(index, item);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000E93D File Offset: 0x0000CB3D
		protected override void RemoveItem(int index)
		{
			this._combinedItems = null;
			base.RemoveItem(index);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000E94D File Offset: 0x0000CB4D
		protected override void SetItem(int index, IFilterProvider item)
		{
			this._combinedItems = null;
			base.SetItem(index, item);
		}

		// Token: 0x04000165 RID: 357
		private static FilterProviderCollection.FilterComparer _filterComparer = new FilterProviderCollection.FilterComparer();

		// Token: 0x04000166 RID: 358
		private IFilterProvider[] _combinedItems;

		// Token: 0x04000167 RID: 359
		private IDependencyResolver _dependencyResolver;

		// Token: 0x020000C5 RID: 197
		private class FilterComparer : IComparer<Filter>
		{
			// Token: 0x06000531 RID: 1329 RVA: 0x0000E96C File Offset: 0x0000CB6C
			public int Compare(Filter x, Filter y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				if (x.Order < y.Order)
				{
					return -1;
				}
				if (x.Order > y.Order)
				{
					return 1;
				}
				if (x.Scope < y.Scope)
				{
					return -1;
				}
				if (x.Scope > y.Scope)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
