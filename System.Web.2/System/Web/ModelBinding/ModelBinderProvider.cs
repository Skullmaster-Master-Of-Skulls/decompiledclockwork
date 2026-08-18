using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000678 RID: 1656
	public abstract class ModelBinderProvider
	{
		// Token: 0x06005090 RID: 20624
		public abstract IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext);
	}
}
