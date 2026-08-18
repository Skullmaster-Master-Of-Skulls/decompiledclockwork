using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000674 RID: 1652
	internal static class KeyValuePairModelBinderUtil
	{
		// Token: 0x0600507C RID: 20604 RVA: 0x00115D04 File Offset: 0x00113F04
		public static bool TryBindStrongModel<TModel>(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext parentBindingContext, string propertyName, ModelMetadataProvider metadataProvider, out TModel model)
		{
			ModelBindingContext modelBindingContext = new ModelBindingContext(parentBindingContext)
			{
				ModelMetadata = metadataProvider.GetMetadataForType(null, typeof(TModel)),
				ModelName = ModelBinderUtil.CreatePropertyModelName(parentBindingContext.ModelName, propertyName)
			};
			IModelBinder binder = parentBindingContext.ModelBinderProviders.GetBinder(modelBindingExecutionContext, modelBindingContext);
			if (binder != null && binder.BindModel(modelBindingExecutionContext, modelBindingContext))
			{
				object model2 = modelBindingContext.Model;
				model = ModelBinderUtil.CastOrDefault<TModel>(model2);
				parentBindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				return true;
			}
			model = default(TModel);
			return false;
		}
	}
}
