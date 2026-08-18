using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200068B RID: 1675
	public sealed class TypeConverterModelBinder : IModelBinder
	{
		// Token: 0x06005113 RID: 20755 RVA: 0x001175C8 File Offset: 0x001157C8
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			ValueProviderResult value = bindingContext.UnvalidatedValueProvider.GetValue(bindingContext.ModelName, !bindingContext.ValidateRequest);
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
					string text = ModelBinderErrorMessageProviders.TypeConversionErrorMessageProvider(modelBindingExecutionContext, bindingContext.ModelMetadata, value.AttemptedValue);
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
			ModelBinderUtil.ReplaceEmptyStringWithNull(bindingContext.ModelMetadata, ref model);
			bindingContext.Model = model;
			return true;
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x00117694 File Offset: 0x00115894
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
