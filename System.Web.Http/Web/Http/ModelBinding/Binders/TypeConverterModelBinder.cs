using System;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000182 RID: 386
	public sealed class TypeConverterModelBinder : IModelBinder
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x000214A0 File Offset: 0x0001F6A0
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value == null)
			{
				return false;
			}
			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
			object model;
			try
			{
				model = value.ConvertTo(bindingContext.ModelType);
			}
			catch (Exception ex)
			{
				if (TypeConverterModelBinder.IsFormatException(ex))
				{
					string text = ModelBinderConfig.TypeConversionErrorMessageProvider(actionContext, bindingContext.ModelMetadata, value.AttemptedValue);
					if (text != null)
					{
						bindingContext.ModelState.AddModelError(bindingContext.ModelName, text);
					}
				}
				else
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
				}
				return false;
			}
			ModelBindingHelper.ReplaceEmptyStringWithNull(bindingContext.ModelMetadata, ref model);
			bindingContext.Model = model;
			return true;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00021564 File Offset: 0x0001F764
		private static bool IsFormatException(Exception ex)
		{
			while (ex != null)
			{
				if (ex is FormatException)
				{
					return true;
				}
				ex = ex.InnerException;
			}
			return false;
		}
	}
}
