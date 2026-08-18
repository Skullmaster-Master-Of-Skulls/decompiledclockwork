using System;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200051C RID: 1308
	internal sealed class ObjectItemCachedAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000EC2CB File Offset: 0x000EA4CB
		private new ImmutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (ImmutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000EC2D8 File Offset: 0x000EA4D8
		internal ObjectItemCachedAssemblyLoader(Assembly assembly, ImmutableAssemblyCacheEntry cacheEntry, ObjectItemLoadingSessionData sessionData) : base(assembly, cacheEntry, sessionData)
		{
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000EC2E3 File Offset: 0x000EA4E3
		protected override void AddToAssembliesLoaded()
		{
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000EC2E8 File Offset: 0x000EA4E8
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
