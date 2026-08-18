using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000125 RID: 293
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpActionContextExtensions
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x00017470 File Offset: 0x00015670
		public static ModelMetadataProvider GetMetadataProvider(this HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return actionContext.ControllerContext.Configuration.Services.GetModelMetadataProvider();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00017495 File Offset: 0x00015695
		public static IEnumerable<ModelValidatorProvider> GetValidatorProviders(this HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return actionContext.ControllerContext.Configuration.Services.GetModelValidatorProviders();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000174BC File Offset: 0x000156BC
		public static IEnumerable<ModelValidator> GetValidators(this HttpActionContext actionContext, ModelMetadata metadata)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IModelValidatorCache validatorCache = actionContext.GetValidatorCache();
			return actionContext.GetValidators(metadata, validatorCache);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000174E6 File Offset: 0x000156E6
		internal static IEnumerable<ModelValidator> GetValidators(this HttpActionContext actionContext, ModelMetadata metadata, IModelValidatorCache validatorCache)
		{
			if (validatorCache == null)
			{
				return metadata.GetValidators(actionContext.GetValidatorProviders());
			}
			return validatorCache.GetValidators(metadata);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00017500 File Offset: 0x00015700
		internal static IModelValidatorCache GetValidatorCache(this HttpActionContext actionContext)
		{
			HttpConfiguration configuration = actionContext.ControllerContext.Configuration;
			return configuration.Services.GetModelValidatorCache();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00017524 File Offset: 0x00015724
		public static bool TryBindStrongModel<TModel>(this HttpActionContext actionContext, ModelBindingContext parentBindingContext, string propertyName, ModelMetadataProvider metadataProvider, out TModel model)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			ModelBindingContext modelBindingContext = new ModelBindingContext(parentBindingContext)
			{
				ModelMetadata = metadataProvider.GetMetadataForType(null, typeof(TModel)),
				ModelName = ModelBindingHelper.CreatePropertyModelName(parentBindingContext.ModelName, propertyName)
			};
			if (actionContext.Bind(modelBindingContext))
			{
				object model2 = modelBindingContext.Model;
				model = ModelBindingHelper.CastOrDefault<TModel>(model2);
				parentBindingContext.ValidationNode.ChildNodes.Add(modelBindingContext.ValidationNode);
				return true;
			}
			model = default(TModel);
			return false;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000175CC File Offset: 0x000157CC
		public static bool Bind(this HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			Type modelType = bindingContext.ModelType;
			HttpConfiguration config = actionContext.ControllerContext.Configuration;
			IEnumerable<IModelBinder> binders = from provider in config.Services.GetModelBinderProviders()
			select provider.GetBinder(config, modelType);
			return actionContext.Bind(bindingContext, binders);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00017628 File Offset: 0x00015828
		public static bool Bind(this HttpActionContext actionContext, ModelBindingContext bindingContext, IEnumerable<IModelBinder> binders)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (bindingContext == null)
			{
				throw Error.ArgumentNull("bindingContext");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Type modelType = bindingContext.ModelType;
			HttpConfiguration configuration = actionContext.ControllerContext.Configuration;
			ModelBinderProvider modelBinderProvider;
			if (ModelBindingHelper.TryGetProviderFromAttributes(modelType, out modelBinderProvider))
			{
				IModelBinder binder = modelBinderProvider.GetBinder(configuration, modelType);
				if (binder != null)
				{
					return binder.BindModel(actionContext, bindingContext);
				}
			}
			foreach (IModelBinder modelBinder in binders)
			{
				if (modelBinder != null && modelBinder.BindModel(actionContext, bindingContext))
				{
					return true;
				}
			}
			return false;
		}
	}
}
