using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200068F RID: 1679
	[ModelBinderProviderOptions(FrontOfList = true)]
	public sealed class TypeMatchModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06005120 RID: 20768 RVA: 0x00117841 File Offset: 0x00115A41
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			if (TypeMatchModelBinder.GetCompatibleValueProviderResult(bindingContext) == null)
			{
				return null;
			}
			return new TypeMatchModelBinder();
		}
	}
}
