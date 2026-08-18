using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200021B RID: 539
	internal abstract class ObjectItemAssemblyLoader
	{
		// Token: 0x06002332 RID: 9010 RVA: 0x0007CE61 File Offset: 0x0007B061
		protected ObjectItemAssemblyLoader(Assembly assembly, AssemblyCacheEntry cacheEntry, ObjectItemLoadingSessionData sessionData)
		{
			this._assembly = assembly;
			this._cacheEntry = cacheEntry;
			this._sessionData = sessionData;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0007CE7E File Offset: 0x0007B07E
		internal virtual void Load()
		{
			this.AddToAssembliesLoaded();
			this.LoadTypesFromAssembly();
			this.AddToKnownAssemblies();
			this.LoadClosureAssemblies();
		}

		// Token: 0x06002334 RID: 9012
		protected abstract void AddToAssembliesLoaded();

		// Token: 0x06002335 RID: 9013
		protected abstract void LoadTypesFromAssembly();

		// Token: 0x06002336 RID: 9014 RVA: 0x0007CE98 File Offset: 0x0007B098
		protected virtual void LoadClosureAssemblies()
		{
			ObjectItemAssemblyLoader.LoadAssemblies(this.CacheEntry.ClosureAssemblies, this.SessionData);
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void OnLevel1SessionProcessing()
		{
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void OnLevel2SessionProcessing()
		{
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0007CEB0 File Offset: 0x0007B0B0
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
					sessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AttributeAssemblyReferenced(assembly.FullName), null));
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

		// Token: 0x0600233A RID: 9018 RVA: 0x0007CFE9 File Offset: 0x0007B1E9
		internal static bool IsAttributeLoader(object loaderCookie)
		{
			return ObjectItemAssemblyLoader.IsAttributeLoader(loaderCookie as Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0007CFF6 File Offset: 0x0007B1F6
		internal static bool IsAttributeLoader(Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> loaderFactory)
		{
			return loaderFactory != null && loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0007D00F File Offset: 0x0007B20F
		internal static bool IsConventionLoader(Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> loaderFactory)
		{
			return loaderFactory != null && loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0007D028 File Offset: 0x0007B228
		protected virtual void AddToKnownAssemblies()
		{
			this._sessionData.KnownAssemblies.Add(this._assembly, new KnownAssemblyEntry(this.CacheEntry, this.SessionData.EdmItemCollection != null));
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0007D05C File Offset: 0x0007B25C
		protected static void LoadAssemblies(IEnumerable<Assembly> assemblies, ObjectItemLoadingSessionData sessionData)
		{
			foreach (Assembly assembly in assemblies)
			{
				ObjectItemAssemblyLoader objectItemAssemblyLoader = ObjectItemAssemblyLoader.CreateLoader(assembly, sessionData);
				objectItemAssemblyLoader.Load();
			}
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0007D0AC File Offset: 0x0007B2AC
		protected bool TryGetPrimitiveType(Type type, out PrimitiveType primitiveType)
		{
			return ClrProviderManifest.Instance.TryGetPrimitiveType(Nullable.GetUnderlyingType(type) ?? type, out primitiveType);
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0007D0C4 File Offset: 0x0007B2C4
		protected ObjectItemLoadingSessionData SessionData
		{
			get
			{
				return this._sessionData;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x0007D0CC File Offset: 0x0007B2CC
		protected Assembly SourceAssembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x0007D0D4 File Offset: 0x0007B2D4
		protected AssemblyCacheEntry CacheEntry
		{
			get
			{
				return this._cacheEntry;
			}
		}

		// Token: 0x04000FA7 RID: 4007
		protected const BindingFlags PropertyReflectionBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000FA8 RID: 4008
		private readonly ObjectItemLoadingSessionData _sessionData;

		// Token: 0x04000FA9 RID: 4009
		private Assembly _assembly;

		// Token: 0x04000FAA RID: 4010
		private AssemblyCacheEntry _cacheEntry;
	}
}
