using System;
using System.ComponentModel;

namespace System.Web.ModelBinding
{
	// Token: 0x0200068C RID: 1676
	public sealed class TypeConverterModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06005116 RID: 20758 RVA: 0x001176B0 File Offset: 0x001158B0
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (bindingContext.UnvalidatedValueProvider.GetValue(bindingContext.ModelName, !bindingContext.ValidateRequest) == null)
			{
				return null;
			}
			if (!TypeDescriptor.GetConverter(bindingContext.ModelType).CanConvertFrom(typeof(string)))
			{
				return null;
			}
			return new TypeConverterModelBinder();
		}
	}
}
