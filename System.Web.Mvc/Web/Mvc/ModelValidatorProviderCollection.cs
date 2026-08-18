using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	// Token: 0x0200013B RID: 315
	public class ModelValidatorProviderCollection : Collection<ModelValidatorProvider>
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x000164E4 File Offset: 0x000146E4
		public ModelValidatorProviderCollection()
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000164EC File Offset: 0x000146EC
		public ModelValidatorProviderCollection(IList<ModelValidatorProvider> list) : base(list)
		{
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000164F5 File Offset: 0x000146F5
		internal ModelValidatorProviderCollection(IList<ModelValidatorProvider> list, IDependencyResolver dependencyResolver) : base(list)
		{
			this._dependencyResolver = dependencyResolver;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00016508 File Offset: 0x00014708
		internal ModelValidatorProvider[] CombinedItems
		{
			get
			{
				ModelValidatorProvider[] array = this._combinedItems;
				if (array == null)
				{
					array = MultiServiceResolver.GetCombined<ModelValidatorProvider>(base.Items, this._dependencyResolver);
					this._combinedItems = array;
				}
				return array;
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00016539 File Offset: 0x00014739
		protected override void ClearItems()
		{
			this._combinedItems = null;
			base.ClearItems();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00016548 File Offset: 0x00014748
		protected override void InsertItem(int index, ModelValidatorProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.InsertItem(index, item);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00016567 File Offset: 0x00014767
		protected override void RemoveItem(int index)
		{
			this._combinedItems = null;
			base.RemoveItem(index);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00016577 File Offset: 0x00014777
		protected override void SetItem(int index, ModelValidatorProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this._combinedItems = null;
			base.SetItem(index, item);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00016798 File Offset: 0x00014998
		public IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
		{
			foreach (ModelValidatorProvider provider in this.CombinedItems)
			{
				foreach (ModelValidator validator in provider.GetValidators(metadata, context))
				{
					yield return validator;
				}
			}
			yield break;
		}

		// Token: 0x04000241 RID: 577
		private ModelValidatorProvider[] _combinedItems;

		// Token: 0x04000242 RID: 578
		private IDependencyResolver _dependencyResolver;
	}
}
