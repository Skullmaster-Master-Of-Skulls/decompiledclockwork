using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000636 RID: 1590
	public sealed class ComplexModelBinder : IModelBinder
	{
		// Token: 0x06004EFC RID: 20220 RVA: 0x00112D04 File Offset: 0x00110F04
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext, typeof(ComplexModel), false);
			ComplexModel complexModel = (ComplexModel)bindingContext.Model;
			foreach (ModelMetadata modelMetadata in complexModel.PropertyMetadata)
			{
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = modelMetadata,
					ModelName = ModelBinderUtil.CreatePropertyModelName(bindingContext.ModelName, modelMetadata.PropertyName)
				};
				IModelBinder binder = bindingContext.ModelBinderProviders.GetBinder(modelBindingExecutionContext, modelBindingContext);
				if (binder != null)
				{
					if (binder.BindModel(modelBindingExecutionContext, modelBindingContext))
					{
						complexModel.Results[modelMetadata] = new ComplexModelResult(modelBindingContext.Model, modelBindingContext.ValidationNode);
					}
					else
					{
						complexModel.Results[modelMetadata] = null;
					}
				}
			}
			return true;
		}
	}
}
