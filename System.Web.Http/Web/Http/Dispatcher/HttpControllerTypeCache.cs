using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000115 RID: 277
	internal sealed class HttpControllerTypeCache
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x00016191 File Offset: 0x00014391
		public HttpControllerTypeCache(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
			this._cache = new Lazy<Dictionary<string, ILookup<string, Type>>>(new Func<Dictionary<string, ILookup<string, Type>>>(this.InitializeCache));
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000161C5 File Offset: 0x000143C5
		internal Dictionary<string, ILookup<string, Type>> Cache
		{
			get
			{
				return this._cache.Value;
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000161D4 File Offset: 0x000143D4
		public ICollection<Type> GetControllerTypes(string controllerName)
		{
			if (string.IsNullOrEmpty(controllerName))
			{
				throw Error.ArgumentNullOrEmpty("controllerName");
			}
			HashSet<Type> hashSet = new HashSet<Type>();
			ILookup<string, Type> lookup;
			if (this._cache.Value.TryGetValue(controllerName, out lookup))
			{
				foreach (IGrouping<string, Type> other in lookup)
				{
					hashSet.UnionWith(other);
				}
			}
			return hashSet;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000162B4 File Offset: 0x000144B4
		private Dictionary<string, ILookup<string, Type>> InitializeCache()
		{
			IAssembliesResolver assembliesResolver = this._configuration.Services.GetAssembliesResolver();
			IHttpControllerTypeResolver httpControllerTypeResolver = this._configuration.Services.GetHttpControllerTypeResolver();
			ICollection<Type> controllerTypes = httpControllerTypeResolver.GetControllerTypes(assembliesResolver);
			IEnumerable<IGrouping<string, Type>> source = controllerTypes.GroupBy((Type t) => t.Name.Substring(0, t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length), StringComparer.OrdinalIgnoreCase);
			return source.ToDictionary((IGrouping<string, Type> g) => g.Key, (IGrouping<string, Type> g) => g.ToLookup((Type t) => t.Namespace ?? string.Empty, StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x040001D7 RID: 471
		private readonly HttpConfiguration _configuration;

		// Token: 0x040001D8 RID: 472
		private readonly Lazy<Dictionary<string, ILookup<string, Type>>> _cache;
	}
}
