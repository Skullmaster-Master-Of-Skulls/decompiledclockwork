using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000217 RID: 535
	internal sealed class KnownAssemblyEntry
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x0007CC40 File Offset: 0x0007AE40
		internal KnownAssemblyEntry(AssemblyCacheEntry cacheEntry, bool seenWithEdmItemCollection)
		{
			this._cacheEntry = cacheEntry;
			this._referencedAssembliesAreLoaded = false;
			this._seenWithEdmItemCollection = seenWithEdmItemCollection;
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x0007CC5D File Offset: 0x0007AE5D
		internal AssemblyCacheEntry CacheEntry
		{
			get
			{
				return this._cacheEntry;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0007CC65 File Offset: 0x0007AE65
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x0007CC6D File Offset: 0x0007AE6D
		public bool ReferencedAssembliesAreLoaded
		{
			get
			{
				return this._referencedAssembliesAreLoaded;
			}
			set
			{
				this._referencedAssembliesAreLoaded = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0007CC76 File Offset: 0x0007AE76
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x0007CC7E File Offset: 0x0007AE7E
		public bool SeenWithEdmItemCollection
		{
			get
			{
				return this._seenWithEdmItemCollection;
			}
			set
			{
				this._seenWithEdmItemCollection = value;
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0007CC87 File Offset: 0x0007AE87
		public bool HaveSeenInCompatibleContext(object loaderCookie, EdmItemCollection itemCollection)
		{
			return this.SeenWithEdmItemCollection || itemCollection == null || ObjectItemAssemblyLoader.IsAttributeLoader(loaderCookie);
		}

		// Token: 0x04000F9D RID: 3997
		private readonly AssemblyCacheEntry _cacheEntry;

		// Token: 0x04000F9E RID: 3998
		private bool _referencedAssembliesAreLoaded;

		// Token: 0x04000F9F RID: 3999
		private bool _seenWithEdmItemCollection;
	}
}
