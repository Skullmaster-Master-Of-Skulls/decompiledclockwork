using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000632 RID: 1586
	public sealed class CollectionModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06004EE7 RID: 20199 RVA: 0x00112764 File Offset: 0x00110964
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (bindingContext.UnvalidatedValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return CollectionModelBinderUtil.GetGenericBinder(typeof(ICollection<>), typeof(List<>), typeof(CollectionModelBinder<>), bindingContext.ModelMetadata);
			}
			return null;
		}
	}
}
