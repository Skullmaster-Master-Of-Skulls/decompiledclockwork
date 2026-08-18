using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	// Token: 0x02000135 RID: 309
	public class ValueProviderFactoryCollection : Collection<ValueProviderFactory>
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x00015E26 File Offset: 0x00014026
		public ValueProviderFactoryCollection()
		{
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00015E2E File Offset: 0x0001402E
		public ValueProviderFactoryCollection(IList<ValueProviderFactory> list) : base(list)
		{
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00015E37 File Offset: 0x00014037
		internal ValueProviderFactoryCollection(IList<ValueProviderFactory> list, IDependencyResolver dependencyResolver) : base(list)
		{
			this._dependencyResolver = dependencyResolver;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00015E48 File Offset: 0x00014048
		internal ValueProviderFactory[] CombinedItems
		{
			get
			{
				ValueProviderFactory[] array = this._combinedItems;
				if (array == null)
				{
					array = MultiServiceResolver.GetCombined<ValueProviderFactory>(base.Items, this._dependencyResolver);
					this._combinedItems = array;
				}
				return array;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00015E7C File Offset: 0x0001407C
		public IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			ValueProviderFactory[] combinedItems = this.CombinedItems;
			List<IValueProvider> list = new List<IValueProvider>(combinedItems.Length);
			foreach (ValueProviderFactory valueProviderFactory in combinedItems)
			{
				IValueProvider valueProvider = valueProviderFactory.GetValueProvider(controllerContext);
				if (valueProvider != null)
				{
					list.Add(valueProvider);
				}
			}
			return new ValueProviderCollection(list);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00015EC6 File Offset: 0x000140C6
		protected override void ClearItems()
		{
			this._combinedItems = null;
			base.ClearItems();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override void InsertItem(int index, ValueProviderFactory item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.InsertItem(index, item);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00015EF4 File Offset: 0x000140F4
		protected override void RemoveItem(int index)
		{
			this._combinedItems = null;
			base.RemoveItem(index);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00015F04 File Offset: 0x00014104
		protected override void SetItem(int index, ValueProviderFactory item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.SetItem(index, item);
		}

		// Token: 0x0400023D RID: 573
		private ValueProviderFactory[] _combinedItems;

		// Token: 0x0400023E RID: 574
		private IDependencyResolver _dependencyResolver;
	}
}
