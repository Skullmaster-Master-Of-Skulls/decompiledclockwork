using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000681 RID: 1665
	public sealed class MutableObjectModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060050E4 RID: 20708 RVA: 0x001170A8 File Offset: 0x001152A8
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (!bindingContext.UnvalidatedValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return null;
			}
			if (bindingContext.ModelType == typeof(ComplexModel))
			{
				return null;
			}
			return new MutableObjectModelBinder();
		}
	}
}
