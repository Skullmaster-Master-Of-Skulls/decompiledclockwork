using System;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200021D RID: 541
	internal sealed class ObjectItemCachedAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x0007E0D5 File Offset: 0x0007C2D5
		private new ImmutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (ImmutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0007E0E2 File Offset: 0x0007C2E2
		internal ObjectItemCachedAssemblyLoader(Assembly assembly, ImmutableAssemblyCacheEntry cacheEntry, ObjectItemLoadingSessionData sessionData) : base(assembly, cacheEntry, sessionData)
		{
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected override void AddToAssembliesLoaded()
		{
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0007E0F0 File Offset: 0x0007C2F0
		protected override void LoadTypesFromAssembly()
		{
			foreach (EdmType edmType in this.CacheEntry.TypesInAssembly)
			{
				if (!base.SessionData.TypesInLoading.ContainsKey(edmType.Identity))
				{
					base.SessionData.TypesInLoading.Add(edmType.Identity, edmType);
				}
			}
		}
	}
}
