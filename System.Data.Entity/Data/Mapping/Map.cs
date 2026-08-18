using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000234 RID: 564
	internal abstract class Map : GlobalItem
	{
		// Token: 0x06002403 RID: 9219 RVA: 0x0008272A File Offset: 0x0008092A
		protected Map() : base(MetadataItem.MetadataFlags.Readonly)
		{
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002404 RID: 9220
		internal abstract MetadataItem EdmItem { get; }
	}
}
