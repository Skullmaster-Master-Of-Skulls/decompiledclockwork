using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000144 RID: 324
	public class DictionaryModelBinder<TKey, TValue> : CollectionModelBinder<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x0001A6A1 File Offset: 0x000188A1
		protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<KeyValuePair<TKey, TValue>> newCollection)
		{
			CollectionModelBinderUtil.CreateOrReplaceDictionary<TKey, TValue>(bindingContext, newCollection, () => new Dictionary<TKey, TValue>());
			return true;
		}
	}
}
