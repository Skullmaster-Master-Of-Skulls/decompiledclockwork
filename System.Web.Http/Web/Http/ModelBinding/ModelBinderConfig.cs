using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200014A RID: 330
	public static class ModelBinderConfig
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001AB18 File Offset: 0x00018D18
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0001AB28 File Offset: 0x00018D28
		public static string ResourceClassKey
		{
			get
			{
				return ModelBinderConfig._resourceClassKey ?? string.Empty;
			}
			set
			{
				ModelBinderConfig._resourceClassKey = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001AB30 File Offset: 0x00018D30
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001AB4F File Offset: 0x00018D4F
		public static ModelBinderErrorMessageProvider TypeConversionErrorMessageProvider
		{
			get
			{
				if (ModelBinderConfig._typeConversionErrorMessageProvider == null)
				{
					ModelBinderConfig._typeConversionErrorMessageProvider = new ModelBinderErrorMessageProvider(ModelBinderConfig.DefaultTypeConversionErrorMessageProvider);
				}
				return ModelBinderConfig._typeConversionErrorMessageProvider;
			}
			set
			{
				ModelBinderConfig._typeConversionErrorMessageProvider = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001AB57 File Offset: 0x00018D57
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001AB76 File Offset: 0x00018D76
		public static ModelBinderErrorMessageProvider ValueRequiredErrorMessageProvider
		{
			get
			{
				if (ModelBinderConfig._valueRequiredErrorMessageProvider == null)
				{
					ModelBinderConfig._valueRequiredErrorMessageProvider = new ModelBinderErrorMessageProvider(ModelBinderConfig.DefaultValueRequiredErrorMessageProvider);
				}
				return ModelBinderConfig._valueRequiredErrorMessageProvider;
			}
			set
			{
				ModelBinderConfig._valueRequiredErrorMessageProvider = value;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001AB7E File Offset: 0x00018D7E
		private static string DefaultTypeConversionErrorMessageProvider(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return ModelBinderConfig.GetResourceCommon(actionContext, modelMetadata, incomingValue, new Func<HttpActionContext, string>(ModelBinderConfig.GetValueInvalidResource));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001AB94 File Offset: 0x00018D94
		private static string DefaultValueRequiredErrorMessageProvider(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue)
		{
			return ModelBinderConfig.GetResourceCommon(actionContext, modelMetadata, incomingValue, new Func<HttpActionContext, string>(ModelBinderConfig.GetValueRequiredResource));
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001ABAC File Offset: 0x00018DAC
		private static string GetResourceCommon(HttpActionContext actionContext, ModelMetadata modelMetadata, object incomingValue, Func<HttpActionContext, string> resourceAccessor)
		{
			string displayName = modelMetadata.GetDisplayName();
			string format = resourceAccessor(actionContext);
			return Error.Format(format, new object[]
			{
				incomingValue,
				displayName
			});
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001ABDE File Offset: 0x00018DDE
		private static string GetUserResourceString(HttpActionContext actionContext, string resourceName)
		{
			return ModelBinderConfig.GetUserResourceString(actionContext, resourceName, ModelBinderConfig.ResourceClassKey);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001ABEC File Offset: 0x00018DEC
		internal static string GetUserResourceString(HttpActionContext actionContext, string resourceName, string resourceClassKey)
		{
			return null;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001ABEF File Offset: 0x00018DEF
		private static string GetValueInvalidResource(HttpActionContext actionContext)
		{
			return ModelBinderConfig.GetUserResourceString(actionContext, "PropertyValueInvalid") ?? SRResources.ModelBinderConfig_ValueInvalid;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001AC05 File Offset: 0x00018E05
		private static string GetValueRequiredResource(HttpActionContext actionContext)
		{
			return ModelBinderConfig.GetUserResourceString(actionContext, "PropertyValueRequired") ?? SRResources.ModelBinderConfig_ValueRequired;
		}

		// Token: 0x04000261 RID: 609
		private static string _resourceClassKey;

		// Token: 0x04000262 RID: 610
		private static ModelBinderErrorMessageProvider _typeConversionErrorMessageProvider;

		// Token: 0x04000263 RID: 611
		private static ModelBinderErrorMessageProvider _valueRequiredErrorMessageProvider;
	}
}
