using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000140 RID: 320
	public sealed class ComplexModelDtoModelBinder : IModelBinder
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x0001A518 File Offset: 0x00018718
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext, typeof(ComplexModelDto), false);
			ComplexModelDto complexModelDto = (ComplexModelDto)bindingContext.Model;
			foreach (ModelMetadata modelMetadata in complexModelDto.PropertyMetadata)
			{
				ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelMetadata = modelMetadata,
					ModelName = ModelBindingHelper.CreatePropertyModelName(bindingContext.ModelName, modelMetadata.PropertyName)
				};
				if (actionContext.Bind(modelBindingContext))
				{
					complexModelDto.Results[modelMetadata] = new ComplexModelDtoResult(modelBindingContext.Model, modelBindingContext.ValidationNode);
				}
			}
			return true;
		}
	}
}
