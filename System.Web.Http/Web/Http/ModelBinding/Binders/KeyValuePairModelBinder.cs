using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000149 RID: 329
	public sealed class KeyValuePairModelBinder<TKey, TValue> : IModelBinder
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001AA8C File Offset: 0x00018C8C
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001AA94 File Offset: 0x00018C94
		internal ModelMetadataProvider MetadataProvider { private get; set; }

		// Token: 0x06000824 RID: 2084 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelMetadataProvider metadataProvider = this.MetadataProvider ?? actionContext.GetMetadataProvider();
			ModelBindingHelper.ValidateBindingContext(bindingContext, typeof(KeyValuePair<TKey, TValue>), true);
			TKey key;
			bool flag = actionContext.TryBindStrongModel(bindingContext, "key", metadataProvider, out key);
			TValue value;
			bool flag2 = actionContext.TryBindStrongModel(bindingContext, "value", metadataProvider, out value);
			if (flag && flag2)
			{
				bindingContext.Model = new KeyValuePair<TKey, TValue>(key, value);
			}
			return flag || flag2;
		}
	}
}
