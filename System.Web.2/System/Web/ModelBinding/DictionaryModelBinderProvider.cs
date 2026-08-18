using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x0200063C RID: 1596
	public sealed class DictionaryModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06004F1C RID: 20252 RVA: 0x00112FF0 File Offset: 0x001111F0
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (bindingContext.UnvalidatedValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return CollectionModelBinderUtil.GetGenericBinder(typeof(IDictionary<, >), typeof(Dictionary<, >), typeof(DictionaryModelBinder<, >), bindingContext.ModelMetadata);
			}
			return null;
		}
	}
}
