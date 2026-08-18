using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Services;
using System.Web.Http.Tracing;
using System.Web.Http.Validation;

namespace System.Web.Http
{
	// Token: 0x020001A7 RID: 423
	public class HttpConfiguration : IDisposable
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x00023C77 File Offset: 0x00021E77
		public HttpConfiguration() : this(new HttpRouteCollection(string.Empty))
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00023C8C File Offset: 0x00021E8C
		public HttpConfiguration(HttpRouteCollection routes)
		{
			this._properties = new ConcurrentDictionary<object, object>();
			this._messageHandlers = new Collection<DelegatingHandler>();
			this._filters = new HttpFilterCollection();
			this._dependencyResolver = EmptyResolver.Instance;
			this._initializer = new Action<HttpConfiguration>(HttpConfiguration.DefaultInitializer);
			base..ctor();
			if (routes == null)
			{
				throw Error.ArgumentNull("routes");
			}
			this._routes = routes;
			this._formatters = HttpConfiguration.DefaultFormatters(this);
			this.Services = new DefaultServices(this);
			this.ParameterBindingRules = DefaultActionValueBinder.GetDefaultParameterBinders();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00023D24 File Offset: 0x00021F24
		private HttpConfiguration(HttpConfiguration configuration, HttpControllerSettings settings)
		{
			this._properties = new ConcurrentDictionary<object, object>();
			this._messageHandlers = new Collection<DelegatingHandler>();
			this._filters = new HttpFilterCollection();
			this._dependencyResolver = EmptyResolver.Instance;
			this._initializer = new Action<HttpConfiguration>(HttpConfiguration.DefaultInitializer);
			base..ctor();
			this._routes = configuration.Routes;
			this._filters = configuration.Filters;
			this._messageHandlers = configuration.MessageHandlers;
			this._properties = configuration.Properties;
			this._dependencyResolver = configuration.DependencyResolver;
			this.IncludeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
			this.Services = (settings.IsServiceCollectionInitialized ? settings.Services : configuration.Services);
			this._formatters = (settings.IsFormatterCollectionInitialized ? settings.Formatters : configuration.Formatters);
			this.ParameterBindingRules = (settings.IsParameterBindingRuleCollectionInitialized ? settings.ParameterBindingRules : configuration.ParameterBindingRules);
			this.Initializer = configuration.Initializer;
			if (settings.IsServiceCollectionInitialized && !settings.Services.GetModelValidatorProviders().SequenceEqual(configuration.Services.GetModelValidatorProviders()))
			{
				ModelValidatorCache service = new ModelValidatorCache(new Lazy<IEnumerable<ModelValidatorProvider>>(() => this.Services.GetModelValidatorProviders()));
				settings.Services.Replace(typeof(IModelValidatorCache), service);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00023E76 File Offset: 0x00022076
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x00023E7E File Offset: 0x0002207E
		public Action<HttpConfiguration> Initializer
		{
			get
			{
				return this._initializer;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._initializer = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00023E95 File Offset: 0x00022095
		public HttpFilterCollection Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00023E9D File Offset: 0x0002209D
		public Collection<DelegatingHandler> MessageHandlers
		{
			get
			{
				return this._messageHandlers;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00023EA5 File Offset: 0x000220A5
		public HttpRouteCollection Routes
		{
			get
			{
				return this._routes;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00023EAD File Offset: 0x000220AD
		public ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00023EB5 File Offset: 0x000220B5
		public string VirtualPathRoot
		{
			get
			{
				return this._routes.VirtualPathRoot;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00023EC2 File Offset: 0x000220C2
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00023ECA File Offset: 0x000220CA
		public IDependencyResolver DependencyResolver
		{
			get
			{
				return this._dependencyResolver;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._dependencyResolver = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00023EDC File Offset: 0x000220DC
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x00023EE4 File Offset: 0x000220E4
		public ServicesContainer Services { get; internal set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00023EED File Offset: 0x000220ED
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x00023EF5 File Offset: 0x000220F5
		public ParameterBindingRulesCollection ParameterBindingRules { get; internal set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00023EFE File Offset: 0x000220FE
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x00023F06 File Offset: 0x00022106
		public IncludeErrorDetailPolicy IncludeErrorDetailPolicy { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00023F0F File Offset: 0x0002210F
		public MediaTypeFormatterCollection Formatters
		{
			get
			{
				return this._formatters;
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00023F18 File Offset: 0x00022118
		private static MediaTypeFormatterCollection DefaultFormatters(HttpConfiguration config)
		{
			return new MediaTypeFormatterCollection
			{
				new JQueryMvcFormUrlEncodedFormatter(config)
			};
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00023F38 File Offset: 0x00022138
		internal static HttpConfiguration ApplyControllerSettings(HttpControllerSettings settings, HttpConfiguration configuration)
		{
			if (!settings.IsFormatterCollectionInitialized && !settings.IsParameterBindingRuleCollectionInitialized && !settings.IsServiceCollectionInitialized)
			{
				return configuration;
			}
			HttpConfiguration httpConfiguration = new HttpConfiguration(configuration, settings);
			httpConfiguration.Initializer(httpConfiguration);
			return httpConfiguration;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00023F74 File Offset: 0x00022174
		private static void DefaultInitializer(HttpConfiguration configuration)
		{
			ModelMetadataProvider modelMetadataProvider = configuration.Services.GetModelMetadataProvider();
			IEnumerable<ModelValidatorProvider> modelValidatorProviders = configuration.Services.GetModelValidatorProviders();
			IRequiredMemberSelector requiredMemberSelector = new ModelValidationRequiredMemberSelector(modelMetadataProvider, modelValidatorProviders);
			foreach (MediaTypeFormatter mediaTypeFormatter in configuration.Formatters)
			{
				if (mediaTypeFormatter.RequiredMemberSelector == null)
				{
					mediaTypeFormatter.RequiredMemberSelector = requiredMemberSelector;
				}
			}
			ITraceManager traceManager = configuration.Services.GetTraceManager();
			traceManager.Initialize(configuration);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00024004 File Offset: 0x00022204
		public void EnsureInitialized()
		{
			if (this._initialized)
			{
				return;
			}
			this._initialized = true;
			this.Initializer(this);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00024022 File Offset: 0x00022222
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00024031 File Offset: 0x00022231
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					this._routes.Dispose();
					this.DependencyResolver.Dispose();
				}
			}
		}

		// Token: 0x0400031F RID: 799
		private readonly HttpRouteCollection _routes;

		// Token: 0x04000320 RID: 800
		private readonly ConcurrentDictionary<object, object> _properties;

		// Token: 0x04000321 RID: 801
		private readonly MediaTypeFormatterCollection _formatters;

		// Token: 0x04000322 RID: 802
		private readonly Collection<DelegatingHandler> _messageHandlers;

		// Token: 0x04000323 RID: 803
		private readonly HttpFilterCollection _filters;

		// Token: 0x04000324 RID: 804
		private IDependencyResolver _dependencyResolver;

		// Token: 0x04000325 RID: 805
		private Action<HttpConfiguration> _initializer;

		// Token: 0x04000326 RID: 806
		private bool _initialized;

		// Token: 0x04000327 RID: 807
		private bool _disposed;
	}
}
