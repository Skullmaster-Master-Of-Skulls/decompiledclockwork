using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200051A RID: 1306
	internal abstract class ObjectItemAssemblyLoader
	{
		// Token: 0x06003120 RID: 12576 RVA: 0x000EAF12 File Offset: 0x000E9112
		protected ObjectItemAssemblyLoader(Assembly assembly, AssemblyCacheEntry cacheEntry, ObjectItemLoadingSessionData sessionData)
		{
			this._assembly = assembly;
			this._cacheEntry = cacheEntry;
			this._sessionData = sessionData;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000EAF2F File Offset: 0x000E912F
		internal virtual void Load()
		{
			this.AddToAssembliesLoaded();
			this.LoadTypesFromAssembly();
			this.AddToKnownAssemblies();
			this.LoadClosureAssemblies();
		}

		// Token: 0x06003122 RID: 12578
		protected abstract void AddToAssembliesLoaded();

		// Token: 0x06003123 RID: 12579
		protected abstract void LoadTypesFromAssembly();

		// Token: 0x06003124 RID: 12580 RVA: 0x000EAF49 File Offset: 0x000E9149
		protected virtual void LoadClosureAssemblies()
		{
			ObjectItemAssemblyLoader.LoadAssemblies(this.CacheEntry.ClosureAssemblies, this.SessionData);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000EAF61 File Offset: 0x000E9161
		internal virtual void OnLevel1SessionProcessing()
		{
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000EAF63 File Offset: 0x000E9163
		internal virtual void OnLevel2SessionProcessing()
		{
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000EAF68 File Offset: 0x000E9168
		internal static ObjectItemAssemblyLoader CreateLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (sessionData.KnownAssemblies.Contains(assembly, sessionData.ObjectItemAssemblyLoaderFactory, sessionData.EdmItemCollection))
			{
				return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
			}
			ImmutableAssemblyCacheEntry immutableAssemblyCacheEntry;
			if (sessionData.LockedAssemblyCache.TryGetValue(assembly, out immutableAssemblyCacheEntry))
			{
				if (sessionData.ObjectItemAssemblyLoaderFactory == null)
				{
					if (immutableAssemblyCacheEntry.TypesInAssembly.Count != 0)
					{
						sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
					}
				}
				else if (sessionData.ObjectItemAssemblyLoaderFactory != new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create))
				{
					sessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AttributeAssemblyReferenced(assembly.FullName)));
				}
				return new ObjectItemCachedAssemblyLoader(assembly, immutableAssemblyCacheEntry, sessionData);
			}
			if (sessionData.EdmItemCollection != null && sessionData.EdmItemCollection.ConventionalOcCache.TryGetConventionalOcCacheFromAssemblyCache(assembly, out immutableAssemblyCacheEntry))
			{
				sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
				return new ObjectItemCachedAssemblyLoader(assembly, immutableAssemblyCacheEntry, sessionData);
			}
			if (sessionData.ObjectItemAssemblyLoaderFactory == null)
			{
				if (ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
				{
					sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
				}
				else if (ObjectItemConventionAssemblyLoader.SessionContainsConventionParameters(sessionData))
				{
					sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
				}
			}
			if (sessionData.ObjectItemAssemblyLoaderFactory != null)
			{
				return sessionData.ObjectItemAssemblyLoaderFactory(assembly, sessionData);
			}
			return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000EB0A0 File Offset: 0x000E92A0
		internal static bool IsAttributeLoader(object loaderCookie)
		{
			return ObjectItemAssemblyLoader.IsAttributeLoader(loaderCookie as Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000EB0AD File Offset: 0x000E92AD
		internal static bool IsAttributeLoader(Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> loaderFactory)
		{
			return loaderFactory != null && loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000EB0C6 File Offset: 0x000E92C6
		internal static bool IsConventionLoader(Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> loaderFactory)
		{
			return loaderFactory != null && loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000EB0DF File Offset: 0x000E92DF
		protected virtual void AddToKnownAssemblies()
		{
			this._sessionData.KnownAssemblies.Add(this._assembly, new KnownAssemblyEntry(this.CacheEntry, this.SessionData.EdmItemCollection != null));
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000EB114 File Offset: 0x000E9314
		protected static void LoadAssemblies(IEnumerable<Assembly> assemblies, ObjectItemLoadingSessionData sessionData)
		{
			foreach (Assembly assembly in assemblies)
			{
				ObjectItemAssemblyLoader objectItemAssemblyLoader = ObjectItemAssemblyLoader.CreateLoader(assembly, sessionData);
				objectItemAssemblyLoader.Load();
			}
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000EB164 File Offset: 0x000E9364
		protected static bool TryGetPrimitiveType(Type type, out PrimitiveType primitiveType)
		{
			return ClrProviderManifest.Instance.TryGetPrimitiveType(Nullable.GetUnderlyingType(type) ?? type, out primitiveType);
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000EB17C File Offset: 0x000E937C
		protected ObjectItemLoadingSessionData SessionData
		{
			get
			{
				return this._sessionData;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000EB184 File Offset: 0x000E9384
		protected Assembly SourceAssembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000EB18C File Offset: 0x000E938C
		protected AssemblyCacheEntry CacheEntry
		{
			get
			{
				return this._cacheEntry;
			}
		}

		// Token: 0x04001295 RID: 4757
		private readonly ObjectItemLoadingSessionData _sessionData;

		// Token: 0x04001296 RID: 4758
		private readonly Assembly _assembly;

		// Token: 0x04001297 RID: 4759
		private readonly AssemblyCacheEntry _cacheEntry;
	}
}
