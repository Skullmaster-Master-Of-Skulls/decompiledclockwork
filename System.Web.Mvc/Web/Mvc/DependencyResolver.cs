using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000109 RID: 265
	public class DependencyResolver
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0001367E File Offset: 0x0001187E
		public DependencyResolver()
		{
			this.InnerSetResolver(new DependencyResolver.DefaultDependencyResolver());
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00013691 File Offset: 0x00011891
		public static IDependencyResolver Current
		{
			get
			{
				return DependencyResolver._instance.InnerCurrent;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001369D File Offset: 0x0001189D
		internal static IDependencyResolver CurrentCache
		{
			get
			{
				return DependencyResolver._instance.InnerCurrentCache;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x000136A9 File Offset: 0x000118A9
		public IDependencyResolver InnerCurrent
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000136B1 File Offset: 0x000118B1
		internal IDependencyResolver InnerCurrentCache
		{
			get
			{
				return this._currentCache;
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000136B9 File Offset: 0x000118B9
		public static void SetResolver(IDependencyResolver resolver)
		{
			DependencyResolver._instance.InnerSetResolver(resolver);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000136C6 File Offset: 0x000118C6
		public static void SetResolver(object commonServiceLocator)
		{
			DependencyResolver._instance.InnerSetResolver(commonServiceLocator);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000136D3 File Offset: 0x000118D3
		public static void SetResolver(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
		{
			DependencyResolver._instance.InnerSetResolver(getService, getServices);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000136E1 File Offset: 0x000118E1
		public void InnerSetResolver(IDependencyResolver resolver)
		{
			if (resolver == null)
			{
				throw new ArgumentNullException("resolver");
			}
			this._current = resolver;
			this._currentCache = new DependencyResolver.CacheDependencyResolver(this._current);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001370C File Offset: 0x0001190C
		public void InnerSetResolver(object commonServiceLocator)
		{
			if (commonServiceLocator == null)
			{
				throw new ArgumentNullException("commonServiceLocator");
			}
			Type type = commonServiceLocator.GetType();
			MethodInfo method = type.GetMethod("GetInstance", new Type[]
			{
				typeof(Type)
			});
			MethodInfo method2 = type.GetMethod("GetAllInstances", new Type[]
			{
				typeof(Type)
			});
			if (method == null || method.ReturnType != typeof(object) || method2 == null || method2.ReturnType != typeof(IEnumerable<object>))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.DependencyResolver_DoesNotImplementICommonServiceLocator, new object[]
				{
					type.FullName
				}), "commonServiceLocator");
			}
			Func<Type, object> getService = (Func<Type, object>)Delegate.CreateDelegate(typeof(Func<Type, object>), commonServiceLocator, method);
			Func<Type, IEnumerable<object>> getServices = (Func<Type, IEnumerable<object>>)Delegate.CreateDelegate(typeof(Func<Type, IEnumerable<object>>), commonServiceLocator, method2);
			this.InnerSetResolver(new DependencyResolver.DelegateBasedDependencyResolver(getService, getServices));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00013821 File Offset: 0x00011A21
		public void InnerSetResolver(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
		{
			if (getService == null)
			{
				throw new ArgumentNullException("getService");
			}
			if (getServices == null)
			{
				throw new ArgumentNullException("getServices");
			}
			this.InnerSetResolver(new DependencyResolver.DelegateBasedDependencyResolver(getService, getServices));
		}

		// Token: 0x040001FB RID: 507
		private static DependencyResolver _instance = new DependencyResolver();

		// Token: 0x040001FC RID: 508
		private IDependencyResolver _current;

		// Token: 0x040001FD RID: 509
		private DependencyResolver.CacheDependencyResolver _currentCache;

		// Token: 0x0200010A RID: 266
		private sealed class CacheDependencyResolver : IDependencyResolver
		{
			// Token: 0x06000737 RID: 1847 RVA: 0x00013858 File Offset: 0x00011A58
			public CacheDependencyResolver(IDependencyResolver resolver)
			{
				this._resolver = resolver;
				this._getServiceDelegate = new Func<Type, object>(this._resolver.GetService);
				this._getServicesDelegate = new Func<Type, IEnumerable<object>>(this._resolver.GetServices);
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x000138B8 File Offset: 0x00011AB8
			public object GetService(Type serviceType)
			{
				return this._cache.GetOrAdd(serviceType, this._getServiceDelegate);
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x000138CC File Offset: 0x00011ACC
			public IEnumerable<object> GetServices(Type serviceType)
			{
				return this._cacheMultiple.GetOrAdd(serviceType, this._getServicesDelegate);
			}

			// Token: 0x040001FE RID: 510
			private readonly ConcurrentDictionary<Type, object> _cache = new ConcurrentDictionary<Type, object>();

			// Token: 0x040001FF RID: 511
			private readonly ConcurrentDictionary<Type, IEnumerable<object>> _cacheMultiple = new ConcurrentDictionary<Type, IEnumerable<object>>();

			// Token: 0x04000200 RID: 512
			private readonly Func<Type, object> _getServiceDelegate;

			// Token: 0x04000201 RID: 513
			private readonly Func<Type, IEnumerable<object>> _getServicesDelegate;

			// Token: 0x04000202 RID: 514
			private readonly IDependencyResolver _resolver;
		}

		// Token: 0x0200010B RID: 267
		private class DefaultDependencyResolver : IDependencyResolver
		{
			// Token: 0x0600073A RID: 1850 RVA: 0x000138E0 File Offset: 0x00011AE0
			public object GetService(Type serviceType)
			{
				if (serviceType.IsInterface || serviceType.IsAbstract)
				{
					return null;
				}
				object result;
				try
				{
					result = Activator.CreateInstance(serviceType);
				}
				catch
				{
					result = null;
				}
				return result;
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x00013920 File Offset: 0x00011B20
			public IEnumerable<object> GetServices(Type serviceType)
			{
				return Enumerable.Empty<object>();
			}
		}

		// Token: 0x0200010C RID: 268
		private class DelegateBasedDependencyResolver : IDependencyResolver
		{
			// Token: 0x0600073D RID: 1853 RVA: 0x0001392F File Offset: 0x00011B2F
			public DelegateBasedDependencyResolver(Func<Type, object> getService, Func<Type, IEnumerable<object>> getServices)
			{
				this._getService = getService;
				this._getServices = getServices;
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00013948 File Offset: 0x00011B48
			public object GetService(Type type)
			{
				object result;
				try
				{
					result = this._getService(type);
				}
				catch
				{
					result = null;
				}
				return result;
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x0001397C File Offset: 0x00011B7C
			public IEnumerable<object> GetServices(Type type)
			{
				return this._getServices(type);
			}

			// Token: 0x04000203 RID: 515
			private Func<Type, object> _getService;

			// Token: 0x04000204 RID: 516
			private Func<Type, IEnumerable<object>> _getServices;
		}
	}
}
