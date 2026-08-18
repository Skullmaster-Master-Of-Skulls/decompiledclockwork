using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000CD RID: 205
	public abstract class HttpActionDescriptor
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000FA16 File Offset: 0x0000DC16
		protected HttpActionDescriptor()
		{
			this._filterPipeline = new Lazy<Collection<FilterInfo>>(new Func<Collection<FilterInfo>>(this.InitializeFilterPipeline));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000FA4B File Offset: 0x0000DC4B
		protected HttpActionDescriptor(HttpControllerDescriptor controllerDescriptor) : this()
		{
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			this._controllerDescriptor = controllerDescriptor;
			this._configuration = this._controllerDescriptor.Configuration;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004DA RID: 1242
		public abstract string ActionName { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000FA79 File Offset: 0x0000DC79
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000FA81 File Offset: 0x0000DC81
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._configuration = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000FA94 File Offset: 0x0000DC94
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000FAD6 File Offset: 0x0000DCD6
		public virtual HttpActionBinding ActionBinding
		{
			get
			{
				if (this._actionBinding == null)
				{
					ServicesContainer services = this._controllerDescriptor.Configuration.Services;
					IActionValueBinder actionValueBinder = services.GetActionValueBinder();
					HttpActionBinding binding = actionValueBinder.GetBinding(this);
					this._actionBinding = binding;
				}
				return this._actionBinding;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionBinding = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		public HttpControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerDescriptor = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060004E1 RID: 1249
		public abstract Type ReturnType { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000FB02 File Offset: 0x0000DD02
		public virtual IActionResultConverter ResultConverter
		{
			get
			{
				if (this._converter == null)
				{
					this._converter = HttpActionDescriptor.GetResultConverter(this.ReturnType);
				}
				return this._converter;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000FB23 File Offset: 0x0000DD23
		public virtual Collection<HttpMethod> SupportedHttpMethods
		{
			get
			{
				return this._supportedHttpMethods;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000FB2B File Offset: 0x0000DD2B
		public virtual ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000FB33 File Offset: 0x0000DD33
		public virtual Collection<T> GetCustomAttributes<T>() where T : class
		{
			return this.GetCustomAttributes<T>(true);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public virtual Collection<T> GetCustomAttributes<T>(bool inherit) where T : class
		{
			return new Collection<T>();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000FB43 File Offset: 0x0000DD43
		public virtual Collection<IFilter> GetFilters()
		{
			return new Collection<IFilter>();
		}

		// Token: 0x060004E8 RID: 1256
		public abstract Collection<HttpParameterDescriptor> GetParameters();

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		internal static IActionResultConverter GetResultConverter(Type type)
		{
			if (type != null && type.IsGenericParameter)
			{
				throw Error.InvalidOperation(SRResources.HttpActionDescriptor_NoConverterForGenericParamterTypeExists, new object[]
				{
					type
				});
			}
			if (type == null)
			{
				return HttpActionDescriptor._voidResultConverter;
			}
			if (typeof(HttpResponseMessage).IsAssignableFrom(type))
			{
				return HttpActionDescriptor._responseMessageResultConverter;
			}
			if (typeof(IHttpActionResult).IsAssignableFrom(type))
			{
				return null;
			}
			Type instanceType = typeof(ValueResultConverter<>).MakeGenericType(new Type[]
			{
				type
			});
			return TypeActivator.Create<IActionResultConverter>(instanceType)();
		}

		// Token: 0x060004EA RID: 1258
		public abstract Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken);

		// Token: 0x060004EB RID: 1259 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		public virtual Collection<FilterInfo> GetFilterPipeline()
		{
			return this._filterPipeline.Value;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		internal FilterGrouping GetFilterGrouping()
		{
			Collection<FilterInfo> filterPipeline = this.GetFilterPipeline();
			if (this._filterGrouping == null || this._filterPipelineForGrouping != filterPipeline)
			{
				this._filterGrouping = new FilterGrouping(filterPipeline);
				this._filterPipelineForGrouping = filterPipeline;
			}
			return this._filterGrouping;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000FC40 File Offset: 0x0000DE40
		private Collection<FilterInfo> InitializeFilterPipeline()
		{
			IEnumerable<IFilterProvider> filterProviders = this._configuration.Services.GetFilterProviders();
			IEnumerable<FilterInfo> source = filterProviders.SelectMany((IFilterProvider fp) => fp.GetFilters(this._configuration, this)).OrderBy((FilterInfo f) => f, FilterInfoComparer.Instance);
			source = HttpActionDescriptor.RemoveDuplicates(source.Reverse<FilterInfo>()).Reverse<FilterInfo>();
			return new Collection<FilterInfo>(source.ToList<FilterInfo>());
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		private static IEnumerable<FilterInfo> RemoveDuplicates(IEnumerable<FilterInfo> filters)
		{
			HashSet<Type> visitedTypes = new HashSet<Type>();
			foreach (FilterInfo filter in filters)
			{
				object filterInstance = filter.Instance;
				Type filterInstanceType = filterInstance.GetType();
				if (!visitedTypes.Contains(filterInstanceType) || HttpActionDescriptor.AllowMultiple(filterInstance))
				{
					yield return filter;
					visitedTypes.Add(filterInstanceType);
				}
			}
			yield break;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		private static bool AllowMultiple(object filterInstance)
		{
			IFilter filter = filterInstance as IFilter;
			return filter == null || filter.AllowMultiple;
		}

		// Token: 0x04000168 RID: 360
		private readonly ConcurrentDictionary<object, object> _properties = new ConcurrentDictionary<object, object>();

		// Token: 0x04000169 RID: 361
		private IActionResultConverter _converter;

		// Token: 0x0400016A RID: 362
		private readonly Lazy<Collection<FilterInfo>> _filterPipeline;

		// Token: 0x0400016B RID: 363
		private FilterGrouping _filterGrouping;

		// Token: 0x0400016C RID: 364
		private Collection<FilterInfo> _filterPipelineForGrouping;

		// Token: 0x0400016D RID: 365
		private HttpConfiguration _configuration;

		// Token: 0x0400016E RID: 366
		private HttpControllerDescriptor _controllerDescriptor;

		// Token: 0x0400016F RID: 367
		private readonly Collection<HttpMethod> _supportedHttpMethods = new Collection<HttpMethod>();

		// Token: 0x04000170 RID: 368
		private HttpActionBinding _actionBinding;

		// Token: 0x04000171 RID: 369
		private static readonly ResponseMessageResultConverter _responseMessageResultConverter = new ResponseMessageResultConverter();

		// Token: 0x04000172 RID: 370
		private static readonly VoidResultConverter _voidResultConverter = new VoidResultConverter();
	}
}
