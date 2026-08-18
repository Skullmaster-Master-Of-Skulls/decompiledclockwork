using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000145 RID: 325
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public class ModelBinderAttribute : ParameterBindingAttribute
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0001A6D0 File Offset: 0x000188D0
		public ModelBinderAttribute() : this(null)
		{
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001A6D9 File Offset: 0x000188D9
		public ModelBinderAttribute(Type binderType)
		{
			this.BinderType = binderType;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001A6E8 File Offset: 0x000188E8
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0001A6F0 File Offset: 0x000188F0
		public Type BinderType { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0001A6F9 File Offset: 0x000188F9
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0001A701 File Offset: 0x00018901
		public string Name { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001A70A File Offset: 0x0001890A
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0001A712 File Offset: 0x00018912
		public bool SuppressPrefixCheck { get; set; }

		// Token: 0x06000804 RID: 2052 RVA: 0x0001A71C File Offset: 0x0001891C
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IModelBinder modelBinder = this.GetModelBinder(configuration, parameter.ParameterType);
			IEnumerable<ValueProviderFactory> valueProviderFactories = this.GetValueProviderFactories(configuration);
			return new ModelBinderParameterBinding(parameter, modelBinder, valueProviderFactories);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A750 File Offset: 0x00018950
		public ModelBinderProvider GetModelBinderProvider(HttpConfiguration configuration)
		{
			if (this.BinderType != null)
			{
				object orInstantiate = ModelBinderAttribute.GetOrInstantiate(configuration, this.BinderType);
				if (orInstantiate != null)
				{
					ModelBinderAttribute.VerifyBinderType(orInstantiate.GetType());
					return (ModelBinderProvider)orInstantiate;
				}
			}
			IEnumerable<ModelBinderProvider> modelBinderProviders = configuration.Services.GetModelBinderProviders();
			if (modelBinderProviders.Count<ModelBinderProvider>() == 1)
			{
				return modelBinderProviders.First<ModelBinderProvider>();
			}
			return new CompositeModelBinderProvider(modelBinderProviders);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A7B4 File Offset: 0x000189B4
		public IModelBinder GetModelBinder(HttpConfiguration configuration, Type modelType)
		{
			if (this.BinderType == null)
			{
				ModelBinderProvider modelBinderProvider = this.GetModelBinderProvider(configuration);
				return modelBinderProvider.GetBinder(configuration, modelType);
			}
			object orInstantiate = ModelBinderAttribute.GetOrInstantiate(configuration, this.BinderType);
			IModelBinder modelBinder = orInstantiate as IModelBinder;
			if (modelBinder != null)
			{
				return modelBinder;
			}
			ModelBinderProvider modelBinderProvider2 = orInstantiate as ModelBinderProvider;
			if (modelBinderProvider2 != null)
			{
				return modelBinderProvider2.GetBinder(configuration, modelType);
			}
			Type typeFromHandle = typeof(IModelBinder);
			throw Error.InvalidOperation(SRResources.ValueProviderFactory_Cannot_Create, new object[]
			{
				typeFromHandle.Name,
				orInstantiate.GetType().Name,
				typeFromHandle.Name
			});
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A851 File Offset: 0x00018A51
		public virtual IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
		{
			return configuration.Services.GetValueProviderFactories();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A860 File Offset: 0x00018A60
		private static void VerifyBinderType(Type attemptedType)
		{
			Type typeFromHandle = typeof(ModelBinderProvider);
			if (!typeFromHandle.IsAssignableFrom(attemptedType))
			{
				throw Error.InvalidOperation(SRResources.ValueProviderFactory_Cannot_Create, new object[]
				{
					typeFromHandle.Name,
					attemptedType.Name,
					typeFromHandle.Name
				});
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		private static object GetOrInstantiate(HttpConfiguration configuration, Type type)
		{
			IDependencyResolver dependencyResolver = configuration.DependencyResolver;
			object service = dependencyResolver.GetService(type);
			if (service != null)
			{
				return service;
			}
			return Activator.CreateInstance(type);
		}
	}
}
