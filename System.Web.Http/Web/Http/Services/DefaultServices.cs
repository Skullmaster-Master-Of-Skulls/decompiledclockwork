using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.Tracing;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.Services
{
	// Token: 0x0200012C RID: 300
	public class DefaultServices : ServicesContainer
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x00018AB0 File Offset: 0x00016CB0
		protected DefaultServices()
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00018AE4 File Offset: 0x00016CE4
		private void SetSingle<T>(T instance) where T : class
		{
			this._defaultServicesSingle[typeof(T)] = instance;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00018B04 File Offset: 0x00016D04
		private void SetMultiple<T>(params T[] instances) where T : class
		{
			IEnumerable<object> collection = (IEnumerable<object>)instances;
			this._defaultServicesMulti[typeof(T)] = new List<object>(collection);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00018B3C File Offset: 0x00016D3C
		public DefaultServices(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
			this.SetSingle<IActionValueBinder>(new DefaultActionValueBinder());
			this.SetSingle<IApiExplorer>(new ApiExplorer(configuration));
			this.SetSingle<IAssembliesResolver>(new DefaultAssembliesResolver());
			this.SetSingle<IBodyModelValidator>(new DefaultBodyModelValidator());
			this.SetSingle<IContentNegotiator>(new DefaultContentNegotiator());
			this.SetSingle<IDocumentationProvider>(null);
			this.SetMultiple<IFilterProvider>(new IFilterProvider[]
			{
				new ConfigurationFilterProvider(),
				new ActionDescriptorFilterProvider()
			});
			this.SetSingle<IHostBufferPolicySelector>(null);
			this.SetSingle<IHttpActionInvoker>(new ApiControllerActionInvoker());
			this.SetSingle<IHttpActionSelector>(new ApiControllerActionSelector());
			this.SetSingle<IHttpControllerActivator>(new DefaultHttpControllerActivator());
			this.SetSingle<IHttpControllerSelector>(new DefaultHttpControllerSelector(configuration));
			this.SetSingle<IHttpControllerTypeResolver>(new DefaultHttpControllerTypeResolver());
			this.SetSingle<ITraceManager>(new TraceManager());
			this.SetSingle<ITraceWriter>(null);
			this.SetMultiple<ModelBinderProvider>(new ModelBinderProvider[]
			{
				new TypeConverterModelBinderProvider(),
				new TypeMatchModelBinderProvider(),
				new KeyValuePairModelBinderProvider(),
				new ComplexModelDtoModelBinderProvider(),
				new ArrayModelBinderProvider(),
				new DictionaryModelBinderProvider(),
				new CollectionModelBinderProvider(),
				new MutableObjectModelBinderProvider()
			});
			this.SetSingle<ModelMetadataProvider>(new DataAnnotationsModelMetadataProvider());
			this.SetMultiple<ModelValidatorProvider>(new ModelValidatorProvider[]
			{
				new DataAnnotationsModelValidatorProvider(),
				new DataMemberModelValidatorProvider()
			});
			this.SetMultiple<ValueProviderFactory>(new ValueProviderFactory[]
			{
				new QueryStringValueProviderFactory(),
				new RouteDataValueProviderFactory()
			});
			ModelValidatorCache single = new ModelValidatorCache(new Lazy<IEnumerable<ModelValidatorProvider>>(() => this.GetModelValidatorProviders()));
			this.SetSingle<IModelValidatorCache>(single);
			this.SetSingle<IExceptionHandler>(new DefaultExceptionHandler());
			this.SetMultiple<IExceptionLogger>(new IExceptionLogger[0]);
			this._serviceTypesSingle = new HashSet<Type>(this._defaultServicesSingle.Keys);
			this._serviceTypesMulti = new HashSet<Type>(this._defaultServicesMulti.Keys);
			this.ResetCache();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00018D49 File Offset: 0x00016F49
		public override bool IsSingleService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			return this._serviceTypesSingle.Contains(serviceType);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00018D6C File Offset: 0x00016F6C
		public override object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (this._lastKnownDependencyResolver != this._configuration.DependencyResolver)
			{
				this.ResetCache();
			}
			object obj;
			if (this._cacheSingle.TryGetValue(serviceType, out obj))
			{
				return obj;
			}
			if (!this._serviceTypesSingle.Contains(serviceType))
			{
				throw Error.Argument("serviceType", SRResources.DefaultServices_InvalidServiceType, new object[]
				{
					serviceType.Name
				});
			}
			object service = this._lastKnownDependencyResolver.GetService(serviceType);
			if (!this._cacheSingle.TryGetValue(serviceType, out obj))
			{
				obj = (service ?? this._defaultServicesSingle[serviceType]);
				this._cacheSingle.TryAdd(serviceType, obj);
			}
			return obj;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00018E30 File Offset: 0x00017030
		public override IEnumerable<object> GetServices(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (this._lastKnownDependencyResolver != this._configuration.DependencyResolver)
			{
				this.ResetCache();
			}
			object[] array;
			if (this._cacheMulti.TryGetValue(serviceType, out array))
			{
				return array;
			}
			if (!this._serviceTypesMulti.Contains(serviceType))
			{
				throw Error.Argument("serviceType", SRResources.DefaultServices_InvalidServiceType, new object[]
				{
					serviceType.Name
				});
			}
			IEnumerable<object> services = this._lastKnownDependencyResolver.GetServices(serviceType);
			if (!this._cacheMulti.TryGetValue(serviceType, out array))
			{
				array = (from s in services
				where s != null
				select s).Concat(this._defaultServicesMulti[serviceType]).ToArray<object>();
				this._cacheMulti.TryAdd(serviceType, array);
			}
			return array;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00018F14 File Offset: 0x00017114
		protected override List<object> GetServiceInstances(Type serviceType)
		{
			List<object> result;
			if (!this._defaultServicesMulti.TryGetValue(serviceType, out result))
			{
				throw Error.Argument("serviceType", SRResources.DefaultServices_InvalidServiceType, new object[]
				{
					serviceType.Name
				});
			}
			return result;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00018F53 File Offset: 0x00017153
		protected override void ClearSingle(Type serviceType)
		{
			this._defaultServicesSingle[serviceType] = null;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00018F62 File Offset: 0x00017162
		protected override void ReplaceSingle(Type serviceType, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			this._defaultServicesSingle[serviceType] = service;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00018F85 File Offset: 0x00017185
		private void ResetCache()
		{
			this._cacheSingle = new ConcurrentDictionary<Type, object>();
			this._cacheMulti = new ConcurrentDictionary<Type, object[]>();
			this._lastKnownDependencyResolver = this._configuration.DependencyResolver;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00018FB0 File Offset: 0x000171B0
		protected override void ResetCache(Type serviceType)
		{
			object obj;
			this._cacheSingle.TryRemove(serviceType, out obj);
			object[] array;
			this._cacheMulti.TryRemove(serviceType, out array);
		}

		// Token: 0x0400021E RID: 542
		private ConcurrentDictionary<Type, object[]> _cacheMulti = new ConcurrentDictionary<Type, object[]>();

		// Token: 0x0400021F RID: 543
		private ConcurrentDictionary<Type, object> _cacheSingle = new ConcurrentDictionary<Type, object>();

		// Token: 0x04000220 RID: 544
		private readonly HttpConfiguration _configuration;

		// Token: 0x04000221 RID: 545
		private readonly Dictionary<Type, object> _defaultServicesSingle = new Dictionary<Type, object>();

		// Token: 0x04000222 RID: 546
		private readonly Dictionary<Type, List<object>> _defaultServicesMulti = new Dictionary<Type, List<object>>();

		// Token: 0x04000223 RID: 547
		private IDependencyResolver _lastKnownDependencyResolver;

		// Token: 0x04000224 RID: 548
		private readonly HashSet<Type> _serviceTypesSingle;

		// Token: 0x04000225 RID: 549
		private readonly HashSet<Type> _serviceTypesMulti;
	}
}
