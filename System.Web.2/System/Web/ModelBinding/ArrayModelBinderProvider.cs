using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200062B RID: 1579
	public sealed class ArrayModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06004ED9 RID: 20185 RVA: 0x001125E4 File Offset: 0x001107E4
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (!bindingContext.ModelMetadata.IsReadOnly && bindingContext.ModelType.IsArray && bindingContext.UnvalidatedValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				Type elementType = bindingContext.ModelType.GetElementType();
				return (IModelBinder)Activator.CreateInstance(typeof(ArrayModelBinder<>).MakeGenericType(new Type[]
				{
					elementType
				}));
			}
			return null;
		}
	}
}
