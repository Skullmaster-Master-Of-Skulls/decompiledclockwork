using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x020001D6 RID: 470
	internal sealed class ControllerTypeCache
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00024E60 File Offset: 0x00023060
		internal int Count
		{
			get
			{
				int num = 0;
				foreach (ILookup<string, Type> lookup in this._cache.Values)
				{
					foreach (IGrouping<string, Type> source in lookup)
					{
						num += source.Count<Type>();
					}
				}
				return num;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00024F1C File Offset: 0x0002311C
		internal IReadOnlyList<Type> GetControllerTypes()
		{
			return new ReadOnlyCollection<Type>(this._cache.Values.SelectMany((ILookup<string, Type> lookup) => lookup.SelectMany((IGrouping<string, Type> t) => t)).ToList<Type>());
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00024FC0 File Offset: 0x000231C0
		public void EnsureInitialized(IBuildManager buildManager)
		{
			if (this._cache == null)
			{
				lock (this._lockObj)
				{
					if (this._cache == null)
					{
						List<Type> filteredTypesFromAssemblies = TypeCacheUtil.GetFilteredTypesFromAssemblies("MVC-ControllerTypeCache.xml", new Predicate<Type>(ControllerTypeCache.IsControllerType), buildManager);
						IEnumerable<IGrouping<string, Type>> source = filteredTypesFromAssemblies.GroupBy((Type t) => t.Name.Substring(0, t.Name.Length - "Controller".Length), StringComparer.OrdinalIgnoreCase);
						this._cache = source.ToDictionary((IGrouping<string, Type> g) => g.Key, (IGrouping<string, Type> g) => g.ToLookup((Type t) => t.Namespace ?? string.Empty, StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);
					}
				}
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000250A4 File Offset: 0x000232A4
		public ICollection<Type> GetControllerTypes(string controllerName, HashSet<string> namespaces)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			ILookup<string, Type> lookup;
			if (this._cache.TryGetValue(controllerName, out lookup))
			{
				if (namespaces != null)
				{
					using (HashSet<string>.Enumerator enumerator = namespaces.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string requestedNamespace = enumerator.Current;
							foreach (IGrouping<string, Type> grouping in lookup)
							{
								if (ControllerTypeCache.IsNamespaceMatch(requestedNamespace, grouping.Key))
								{
									hashSet.UnionWith(grouping);
								}
							}
						}
						return hashSet;
					}
				}
				foreach (IGrouping<string, Type> other in lookup)
				{
					hashSet.UnionWith(other);
				}
			}
			return hashSet;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00025194 File Offset: 0x00023394
		internal static bool IsControllerType(Type t)
		{
			return t != null && t.IsPublic && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) && !t.IsAbstract && typeof(IController).IsAssignableFrom(t);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000251D4 File Offset: 0x000233D4
		internal static bool IsNamespaceMatch(string requestedNamespace, string targetNamespace)
		{
			if (requestedNamespace == null)
			{
				return false;
			}
			if (requestedNamespace.Length == 0)
			{
				return true;
			}
			if (!requestedNamespace.EndsWith(".*", StringComparison.OrdinalIgnoreCase))
			{
				return string.Equals(requestedNamespace, targetNamespace, StringComparison.OrdinalIgnoreCase);
			}
			requestedNamespace = requestedNamespace.Substring(0, requestedNamespace.Length - ".*".Length);
			return targetNamespace.StartsWith(requestedNamespace, StringComparison.OrdinalIgnoreCase) && (requestedNamespace.Length == targetNamespace.Length || targetNamespace[requestedNamespace.Length] == '.');
		}

		// Token: 0x040003A7 RID: 935
		private const string TypeCacheName = "MVC-ControllerTypeCache.xml";

		// Token: 0x040003A8 RID: 936
		private volatile Dictionary<string, ILookup<string, Type>> _cache;

		// Token: 0x040003A9 RID: 937
		private object _lockObj = new object();
	}
}
