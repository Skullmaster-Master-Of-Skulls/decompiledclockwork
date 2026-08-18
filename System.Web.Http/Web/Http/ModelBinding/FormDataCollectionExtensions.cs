using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000C9 RID: 201
	public static class FormDataCollectionExtensions
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000EB48 File Offset: 0x0000CD48
		internal static string NormalizeJQueryToMvc(string key)
		{
			if (key == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = null;
			int num = 0;
			for (;;)
			{
				int num2 = key.IndexOf('[', num);
				if (num2 < 0)
				{
					break;
				}
				stringBuilder = (stringBuilder ?? new StringBuilder());
				stringBuilder.Append(key, num, num2 - num);
				int num3 = key.IndexOf(']', num2);
				if (num3 == -1)
				{
					goto Block_6;
				}
				if (num3 != num2 + 1)
				{
					if (char.IsDigit(key[num2 + 1]))
					{
						stringBuilder.Append(key, num2, num3 - num2 + 1);
					}
					else
					{
						stringBuilder.Append('.');
						stringBuilder.Append(key, num2 + 1, num3 - num2 - 1);
					}
				}
				num = num3 + 1;
				if (num >= key.Length)
				{
					goto IL_CB;
				}
			}
			if (num == 0)
			{
				return key;
			}
			stringBuilder = (stringBuilder ?? new StringBuilder());
			stringBuilder.Append(key, num, key.Length - num);
			goto IL_CB;
			Block_6:
			throw Error.Argument("key", SRResources.JQuerySyntaxMissingClosingBracket, new object[0]);
			IL_CB:
			return stringBuilder.ToString();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000EE2C File Offset: 0x0000D02C
		internal static IEnumerable<KeyValuePair<string, string>> GetJQueryNameValuePairs(this FormDataCollection formData)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			int count = 0;
			foreach (KeyValuePair<string, string> kv in formData)
			{
				FormDataCollectionExtensions.ThrowIfMaxHttpCollectionKeysExceeded(count);
				KeyValuePair<string, string> keyValuePair = kv;
				string key = FormDataCollectionExtensions.NormalizeJQueryToMvc(keyValuePair.Key);
				KeyValuePair<string, string> keyValuePair2 = kv;
				string value = keyValuePair2.Value ?? string.Empty;
				yield return new KeyValuePair<string, string>(key, value);
				count++;
			}
			yield break;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000EE4C File Offset: 0x0000D04C
		private static void ThrowIfMaxHttpCollectionKeysExceeded(int count)
		{
			if (count >= MediaTypeFormatter.MaxHttpCollectionKeys)
			{
				throw Error.InvalidOperation(SRResources.MaxHttpCollectionKeyLimitReached, new object[]
				{
					MediaTypeFormatter.MaxHttpCollectionKeys,
					typeof(MediaTypeFormatter)
				});
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000EE90 File Offset: 0x0000D090
		internal static IValueProvider GetJQueryValueProvider(this FormDataCollection formData)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			IEnumerable<KeyValuePair<string, string>> jqueryNameValuePairs = formData.GetJQueryNameValuePairs();
			return new NameValuePairsValueProvider(jqueryNameValuePairs, CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000EEBD File Offset: 0x0000D0BD
		public static T ReadAs<T>(this FormDataCollection formData)
		{
			return (T)((object)formData.ReadAs(typeof(T)));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public static T ReadAs<T>(this FormDataCollection formData, HttpActionContext actionContext)
		{
			return (T)((object)formData.ReadAs(typeof(T), string.Empty, actionContext));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		public static object ReadAs(this FormDataCollection formData, Type type)
		{
			return formData.ReadAs(type, string.Empty, null, null);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000EF01 File Offset: 0x0000D101
		public static object ReadAs(this FormDataCollection formData, Type type, HttpActionContext actionContext)
		{
			return formData.ReadAs(type, string.Empty, actionContext);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000EF10 File Offset: 0x0000D110
		public static T ReadAs<T>(this FormDataCollection formData, string modelName, IRequiredMemberSelector requiredMemberSelector, IFormatterLogger formatterLogger)
		{
			return (T)((object)formData.ReadAs(typeof(T), modelName, requiredMemberSelector, formatterLogger));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000EF2A File Offset: 0x0000D12A
		public static T ReadAs<T>(this FormDataCollection formData, string modelName, HttpActionContext actionContext)
		{
			return (T)((object)formData.ReadAs(typeof(T), modelName, actionContext));
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000EF43 File Offset: 0x0000D143
		public static object ReadAs(this FormDataCollection formData, Type type, string modelName, HttpActionContext actionContext)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return formData.ReadAsInternal(type, modelName, actionContext);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000EF7E File Offset: 0x0000D17E
		public static object ReadAs(this FormDataCollection formData, Type type, string modelName, IRequiredMemberSelector requiredMemberSelector, IFormatterLogger formatterLogger)
		{
			return formData.ReadAs(type, modelName, requiredMemberSelector, formatterLogger, null);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000EF8C File Offset: 0x0000D18C
		public static object ReadAs(this FormDataCollection formData, Type type, string modelName, IRequiredMemberSelector requiredMemberSelector, IFormatterLogger formatterLogger, HttpConfiguration config)
		{
			if (formData == null)
			{
				throw Error.ArgumentNull("formData");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object result = null;
			HttpActionContext httpActionContext = null;
			bool flag = requiredMemberSelector != null && formatterLogger != null;
			if (flag)
			{
				using (HttpConfiguration httpConfiguration = new HttpConfiguration())
				{
					config = ((config == null) ? httpConfiguration : config);
					httpConfiguration.Services = new FormDataCollectionExtensions.ServicesContainerWrapper(config, new RequiredMemberModelValidatorProvider(requiredMemberSelector));
					httpActionContext = FormDataCollectionExtensions.CreateActionContextForModelBinding(httpConfiguration);
					result = formData.ReadAs(type, modelName, httpActionContext);
					goto IL_B9;
				}
			}
			if (config == null)
			{
				HttpConfiguration httpConfiguration2;
				config = (httpConfiguration2 = new HttpConfiguration());
				try
				{
					httpActionContext = FormDataCollectionExtensions.CreateActionContextForModelBinding(config);
					result = formData.ReadAs(type, modelName, httpActionContext);
					goto IL_B9;
				}
				finally
				{
					if (httpConfiguration2 != null)
					{
						((IDisposable)httpConfiguration2).Dispose();
					}
				}
			}
			httpActionContext = FormDataCollectionExtensions.CreateActionContextForModelBinding(config);
			result = formData.ReadAs(type, modelName, httpActionContext);
			IL_B9:
			if (formatterLogger != null)
			{
				foreach (KeyValuePair<string, ModelState> keyValuePair in httpActionContext.ModelState)
				{
					foreach (ModelError modelError in keyValuePair.Value.Errors)
					{
						if (modelError.Exception != null)
						{
							formatterLogger.LogError(keyValuePair.Key, modelError.Exception);
						}
						else
						{
							formatterLogger.LogError(keyValuePair.Key, modelError.ErrorMessage);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F128 File Offset: 0x0000D328
		private static object ReadAsInternal(this FormDataCollection formData, Type type, string modelName, HttpActionContext actionContext)
		{
			IValueProvider jqueryValueProvider = formData.GetJQueryValueProvider();
			ModelBindingContext modelBindingContext = FormDataCollectionExtensions.CreateModelBindingContext(actionContext, modelName ?? string.Empty, type, jqueryValueProvider);
			ModelBinderProvider modelBinderProvider = FormDataCollectionExtensions.CreateModelBindingProvider(actionContext);
			IModelBinder binder = modelBinderProvider.GetBinder(actionContext.ControllerContext.Configuration, type);
			bool flag = binder.BindModel(actionContext, modelBindingContext);
			if (flag)
			{
				return modelBindingContext.Model;
			}
			return MediaTypeFormatter.GetDefaultValueForType(type);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000F184 File Offset: 0x0000D384
		private static ModelBinderProvider CreateModelBindingProvider(HttpActionContext actionContext)
		{
			ServicesContainer services = actionContext.ControllerContext.Configuration.Services;
			IEnumerable<ModelBinderProvider> modelBinderProviders = services.GetModelBinderProviders();
			return new CompositeModelBinderProvider(modelBinderProviders);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		private static ModelBindingContext CreateModelBindingContext(HttpActionContext actionContext, string modelName, Type type, IValueProvider vp)
		{
			ServicesContainer services = actionContext.ControllerContext.Configuration.Services;
			ModelMetadataProvider modelMetadataProvider = services.GetModelMetadataProvider();
			return new ModelBindingContext
			{
				ModelName = modelName,
				FallbackToEmptyPrefix = false,
				ModelMetadata = modelMetadataProvider.GetMetadataForType(null, type),
				ModelState = actionContext.ModelState,
				ValueProvider = vp
			};
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000F214 File Offset: 0x0000D414
		private static HttpActionContext CreateActionContextForModelBinding(HttpConfiguration config)
		{
			HttpControllerContext httpControllerContext = new HttpControllerContext
			{
				Configuration = config
			};
			httpControllerContext.ControllerDescriptor = new HttpControllerDescriptor(config);
			return new HttpActionContext
			{
				ControllerContext = httpControllerContext
			};
		}

		// Token: 0x020000CB RID: 203
		internal class ServicesContainerWrapper : ServicesContainer
		{
			// Token: 0x060004C8 RID: 1224 RVA: 0x0000F6AD File Offset: 0x0000D8AD
			public ServicesContainerWrapper(HttpConfiguration originalConfig, ModelValidatorProvider requiredMemberModelValidatorProvider)
			{
				this._originalConfig = originalConfig;
				this._requiredMemberModelValidatorProvider = requiredMemberModelValidatorProvider;
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0000F6CC File Offset: 0x0000D8CC
			public override object GetService(Type serviceType)
			{
				if (serviceType == typeof(IModelValidatorCache))
				{
					return new ModelValidatorCache(new Lazy<IEnumerable<ModelValidatorProvider>>(() => this.GetServices<ModelValidatorProvider>()));
				}
				if (serviceType == typeof(ModelValidatorProvider))
				{
					return this._requiredMemberModelValidatorProvider;
				}
				return this._originalConfig.Services.GetService(serviceType);
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x0000F734 File Offset: 0x0000D934
			public override IEnumerable<object> GetServices(Type serviceType)
			{
				if (serviceType == typeof(ModelValidatorProvider))
				{
					return new ModelValidatorProvider[]
					{
						this._requiredMemberModelValidatorProvider
					};
				}
				return this._originalConfig.Services.GetServices(serviceType);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0000F776 File Offset: 0x0000D976
			protected override List<object> GetServiceInstances(Type serviceType)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x0000F77D File Offset: 0x0000D97D
			public override bool IsSingleService(Type serviceType)
			{
				return this._originalConfig.Services.IsSingleService(serviceType);
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0000F790 File Offset: 0x0000D990
			protected override void ClearSingle(Type serviceType)
			{
				this._originalConfig.Services.Clear(serviceType);
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0000F7A3 File Offset: 0x0000D9A3
			protected override void ReplaceSingle(Type serviceType, object service)
			{
				this._originalConfig.Services.Replace(serviceType, service);
			}

			// Token: 0x04000163 RID: 355
			private HttpConfiguration _originalConfig;

			// Token: 0x04000164 RID: 356
			private ModelValidatorProvider _requiredMemberModelValidatorProvider;
		}
	}
}
