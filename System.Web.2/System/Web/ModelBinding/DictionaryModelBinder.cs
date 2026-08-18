using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x0200063D RID: 1597
	public class DictionaryModelBinder<TKey, TValue> : CollectionModelBinder<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06004F1E RID: 20254 RVA: 0x00113041 File Offset: 0x00111241
		protected override bool CreateOrReplaceCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IList<KeyValuePair<TKey, TValue>> newCollection)
		{
			CollectionModelBinderUtil.CreateOrReplaceDictionary<TKey, TValue>(bindingContext, newCollection, () => new Dictionary<TKey, TValue>());
			return true;
		}
	}
}
