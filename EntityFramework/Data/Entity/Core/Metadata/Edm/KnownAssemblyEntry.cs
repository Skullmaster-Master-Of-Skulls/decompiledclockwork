using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000515 RID: 1301
	internal sealed class KnownAssemblyEntry
	{
		// Token: 0x06003104 RID: 12548 RVA: 0x000EAA58 File Offset: 0x000E8C58
		internal KnownAssemblyEntry(AssemblyCacheEntry cacheEntry, bool seenWithEdmItemCollection)
		{
			this._cacheEntry = cacheEntry;
			this.ReferencedAssembliesAreLoaded = false;
			this.SeenWithEdmItemCollection = seenWithEdmItemCollection;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000EAA75 File Offset: 0x000E8C75
		internal AssemblyCacheEntry CacheEntry
		{
			get
			{
				return this._cacheEntry;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000EAA7D File Offset: 0x000E8C7D
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x000EAA85 File Offset: 0x000E8C85
		public bool ReferencedAssembliesAreLoaded { get; set; }

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000EAA8E File Offset: 0x000E8C8E
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x000EAA96 File Offset: 0x000E8C96
		public bool SeenWithEdmItemCollection { get; set; }

		// Token: 0x0600310A RID: 12554 RVA: 0x000EAA9F File Offset: 0x000E8C9F
		public bool HaveSeenInCompatibleContext(object loaderCookie, EdmItemCollection itemCollection)
		{
			return this.SeenWithEdmItemCollection || itemCollection == null || ObjectItemAssemblyLoader.IsAttributeLoader(loaderCookie);
		}

		// Token: 0x04001287 RID: 4743
		private readonly AssemblyCacheEntry _cacheEntry;
	}
}
