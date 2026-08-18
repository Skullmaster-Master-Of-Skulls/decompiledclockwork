using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000688 RID: 1672
	public class DefaultModelBinder : IModelBinder
	{
		// Token: 0x06005102 RID: 20738 RVA: 0x001173BA File Offset: 0x001155BA
		public DefaultModelBinder()
		{
			this.Providers = ModelBinderProviders.Providers;
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x001173CD File Offset: 0x001155CD
		// (set) Token: 0x06005104 RID: 20740 RVA: 0x001173D5 File Offset: 0x001155D5
		public ModelBinderProviderCollection Providers { get; private set; }

		// Token: 0x06005105 RID: 20741 RVA: 0x001173E0 File Offset: 0x001155E0
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBindingContext modelBindingContext = bindingContext;
			IModelBinder binder = this.Providers.GetBinder(modelBindingExecutionContext, bindingContext);
			if (binder == null && !string.IsNullOrEmpty(bindingContext.ModelName) && bindingContext.ModelMetadata.IsComplexType)
			{
				modelBindingContext = new ModelBindingContext(bindingContext)
				{
					ModelName = string.Empty,
					ModelMetadata = bindingContext.ModelMetadata
				};
				binder = this.Providers.GetBinder(modelBindingExecutionContext, modelBindingContext);
			}
			if (binder != null)
			{
				bool flag = binder.BindModel(modelBindingExecutionContext, modelBindingContext);
				if (flag)
				{
					modelBindingContext.ValidationNode.Validate(modelBindingExecutionContext, null);
					return true;
				}
			}
			return false;
		}
	}
}
