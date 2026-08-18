using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000675 RID: 1653
	public sealed class KeyValuePairModelBinder<TKey, TValue> : IModelBinder
	{
		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x0600507D RID: 20605 RVA: 0x00115D93 File Offset: 0x00113F93
		// (set) Token: 0x0600507E RID: 20606 RVA: 0x00115DAE File Offset: 0x00113FAE
		internal ModelMetadataProvider MetadataProvider
		{
			get
			{
				if (this._metadataProvider == null)
				{
					this._metadataProvider = ModelMetadataProviders.Current;
				}
				return this._metadataProvider;
			}
			set
			{
				this._metadataProvider = value;
			}
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x00115DB8 File Offset: 0x00113FB8
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext, typeof(KeyValuePair<TKey, TValue>), true);
			TKey key;
			bool flag = KeyValuePairModelBinderUtil.TryBindStrongModel<TKey>(modelBindingExecutionContext, bindingContext, "key", this.MetadataProvider, out key);
			TValue value;
			bool flag2 = KeyValuePairModelBinderUtil.TryBindStrongModel<TValue>(modelBindingExecutionContext, bindingContext, "value", this.MetadataProvider, out value);
			if (flag && flag2)
			{
				bindingContext.Model = new KeyValuePair<TKey, TValue>(key, value);
			}
			return flag || flag2;
		}

		// Token: 0x04002AC7 RID: 10951
		private ModelMetadataProvider _metadataProvider;
	}
}
