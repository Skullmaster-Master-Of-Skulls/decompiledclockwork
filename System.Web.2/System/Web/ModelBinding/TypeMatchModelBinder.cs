using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200068E RID: 1678
	public sealed class TypeMatchModelBinder : IModelBinder
	{
		// Token: 0x0600511D RID: 20765 RVA: 0x001177B0 File Offset: 0x001159B0
		public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ValueProviderResult compatibleValueProviderResult = TypeMatchModelBinder.GetCompatibleValueProviderResult(bindingContext);
			if (compatibleValueProviderResult == null)
			{
				return false;
			}
			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, compatibleValueProviderResult);
			object rawValue = compatibleValueProviderResult.RawValue;
			ModelBinderUtil.ReplaceEmptyStringWithNull(bindingContext.ModelMetadata, ref rawValue);
			bindingContext.Model = rawValue;
			return true;
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x001177F8 File Offset: 0x001159F8
		internal static ValueProviderResult GetCompatibleValueProviderResult(ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			ValueProviderResult value = bindingContext.UnvalidatedValueProvider.GetValue(bindingContext.ModelName, !bindingContext.ValidateRequest);
			if (value == null)
			{
				return null;
			}
			if (!TypeHelpers.IsCompatibleObject(bindingContext.ModelType, value.RawValue))
			{
				return null;
			}
			return value;
		}
	}
}
