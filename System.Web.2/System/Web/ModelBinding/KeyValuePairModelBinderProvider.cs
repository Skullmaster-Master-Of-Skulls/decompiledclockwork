using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000673 RID: 1651
	public sealed class KeyValuePairModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x0600507A RID: 20602 RVA: 0x00115C90 File Offset: 0x00113E90
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			string prefix = ModelBinderUtil.CreatePropertyModelName(bindingContext.ModelName, "key");
			string prefix2 = ModelBinderUtil.CreatePropertyModelName(bindingContext.ModelName, "value");
			if (bindingContext.UnvalidatedValueProvider.ContainsPrefix(prefix) && bindingContext.UnvalidatedValueProvider.ContainsPrefix(prefix2))
			{
				return ModelBinderUtil.GetPossibleBinderInstance(bindingContext.ModelType, typeof(KeyValuePair<, >), typeof(KeyValuePairModelBinder<, >));
			}
			return null;
		}
	}
}
