using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C2 RID: 962
	public abstract class MappingBase : GlobalItem
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x000A42F5 File Offset: 0x000A24F5
		internal MappingBase() : base(MetadataItem.MetadataFlags.Readonly)
		{
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000A42FE File Offset: 0x000A24FE
		internal MappingBase(MetadataItem.MetadataFlags flags) : base(flags)
		{
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600231D RID: 8989
		internal abstract MetadataItem EdmItem { get; }
	}
}
