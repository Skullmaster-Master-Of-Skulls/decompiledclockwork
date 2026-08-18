using System;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000676 RID: 1654
	public static class ModelBinderErrorMessageProviders
	{
		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06005081 RID: 20609 RVA: 0x00115E1A File Offset: 0x0011401A
		// (set) Token: 0x06005082 RID: 20610 RVA: 0x00115E39 File Offset: 0x00114039
		public static ModelBinderErrorMessageProvider TypeConversionErrorMessageProvider
		{
			get
			{
				if (ModelBinderErrorMessageProviders._typeConversionErrorMessageProvider == null)
				{
					ModelBinderErrorMessageProviders._typeConversionErrorMessageProvider = new ModelBinderErrorMessageProvider(ModelBinderErrorMessageProviders.DefaultTypeConversionErrorMessageProvider);
				}
				return ModelBinderErrorMessageProviders._typeConversionErrorMessageProvider;
			}
			set
			{
				ModelBinderErrorMessageProviders._typeConversionErrorMessageProvider = value;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06005083 RID: 20611 RVA: 0x00115E41 File Offset: 0x00114041
		// (set) Token: 0x06005084 RID: 20612 RVA: 0x00115E60 File Offset: 0x00114060
		public static ModelBinderErrorMessageProvider ValueRequiredErrorMessageProvider
		{
			get
			{
				if (ModelBinderErrorMessageProviders._valueRequiredErrorMessageProvider == null)
				{
					ModelBinderErrorMessageProviders._valueRequiredErrorMessageProvider = new ModelBinderErrorMessageProvider(ModelBinderErrorMessageProviders.DefaultValueRequiredErrorMessageProvider);
				}
				return ModelBinderErrorMessageProviders._valueRequiredErrorMessageProvider;
			}
			set
			{
				ModelBinderErrorMessageProviders._valueRequiredErrorMessageProvider = value;
			}
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00115E68 File Offset: 0x00114068
		private static string DefaultTypeConversionErrorMessageProvider(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return ModelBinderErrorMessageProviders.GetResourceCommon(modelBindingExecutionContext, modelMetadata, incomingValue, new Func<ModelBindingExecutionContext, string>(ModelBinderErrorMessageProviders.GetValueInvalidResource));
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00115E7E File Offset: 0x0011407E
		private static string DefaultValueRequiredErrorMessageProvider(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return ModelBinderErrorMessageProviders.GetResourceCommon(modelBindingExecutionContext, modelMetadata, incomingValue, new Func<ModelBindingExecutionContext, string>(ModelBinderErrorMessageProviders.GetValueRequiredResource));
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x00115E94 File Offset: 0x00114094
		private static string GetResourceCommon(ModelBindingExecutionContext modelBindingExecutionContext, ModelMetadata modelMetadata, object incomingValue, Func<ModelBindingExecutionContext, string> resourceAccessor)
		{
			string displayName = modelMetadata.GetDisplayName();
			string format = resourceAccessor(modelBindingExecutionContext);
			return string.Format(CultureInfo.CurrentCulture, format, new object[]
			{
				incomingValue,
				displayName
			});
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x00115ECB File Offset: 0x001140CB
		private static string GetUserResourceString(ModelBindingExecutionContext modelBindingExecutionContext, string resourceName)
		{
			return ModelBinderErrorMessageProviders.GetUserResourceString(modelBindingExecutionContext, resourceName, string.Empty);
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x00115ED9 File Offset: 0x001140D9
		internal static string GetUserResourceString(ModelBindingExecutionContext modelBindingExecutionContext, string resourceName, string resourceClassKey)
		{
			if (string.IsNullOrEmpty(resourceClassKey) || modelBindingExecutionContext == null || modelBindingExecutionContext.HttpContext == null)
			{
				return null;
			}
			return modelBindingExecutionContext.HttpContext.GetGlobalResourceObject(resourceClassKey, resourceName, CultureInfo.CurrentUICulture) as string;
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x00115F07 File Offset: 0x00114107
		private static string GetValueInvalidResource(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			return ModelBinderErrorMessageProviders.GetUserResourceString(modelBindingExecutionContext, "PropertyValueInvalid") ?? SR.GetString("ModelBinderConfig_ValueInvalid");
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x00115F22 File Offset: 0x00114122
		private static string GetValueRequiredResource(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			return ModelBinderErrorMessageProviders.GetUserResourceString(modelBindingExecutionContext, "PropertyValueRequired") ?? SR.GetString("ModelBinderConfig_ValueRequired");
		}

		// Token: 0x04002AC8 RID: 10952
		private static ModelBinderErrorMessageProvider _typeConversionErrorMessageProvider;

		// Token: 0x04002AC9 RID: 10953
		private static ModelBinderErrorMessageProvider _valueRequiredErrorMessageProvider;
	}
}
