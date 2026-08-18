using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	// Token: 0x020000CA RID: 202
	public class ModelBinderProviderCollection : Collection<IModelBinderProvider>
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public ModelBinderProviderCollection()
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public ModelBinderProviderCollection(IList<IModelBinderProvider> list) : base(list)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000EC2D File Offset: 0x0000CE2D
		internal ModelBinderProviderCollection(IList<IModelBinderProvider> list, IDependencyResolver dependencyResolver) : base(list)
		{
			this._dependencyResolver = dependencyResolver;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000EC40 File Offset: 0x0000CE40
		internal IModelBinderProvider[] CombinedItems
		{
			get
			{
				IModelBinderProvider[] array = this._combinedItems;
				if (array == null)
				{
					array = MultiServiceResolver.GetCombined<IModelBinderProvider>(base.Items, this._dependencyResolver);
					this._combinedItems = array;
				}
				return array;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000EC71 File Offset: 0x0000CE71
		protected override void ClearItems()
		{
			this._combinedItems = null;
			base.ClearItems();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000EC80 File Offset: 0x0000CE80
		protected override void InsertItem(int index, IModelBinderProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.InsertItem(index, item);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000EC9F File Offset: 0x0000CE9F
		protected override void RemoveItem(int index)
		{
			this._combinedItems = null;
			base.RemoveItem(index);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000ECAF File Offset: 0x0000CEAF
		protected override void SetItem(int index, IModelBinderProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.SetItem(index, item);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public IModelBinder GetBinder(Type modelType)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			IModelBinderProvider[] combinedItems = this.CombinedItems;
			for (int i = 0; i < combinedItems.Length; i++)
			{
				IModelBinder binder = combinedItems[i].GetBinder(modelType);
				if (binder != null)
				{
					return binder;
				}
			}
			return null;
		}

		// Token: 0x04000171 RID: 369
		private IModelBinderProvider[] _combinedItems;

		// Token: 0x04000172 RID: 370
		private IDependencyResolver _dependencyResolver;
	}
}
