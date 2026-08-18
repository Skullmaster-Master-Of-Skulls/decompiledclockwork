using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001DD RID: 477
	public abstract class GlobalItem : MetadataItem
	{
		// Token: 0x06002023 RID: 8227 RVA: 0x00070309 File Offset: 0x0006E509
		internal GlobalItem()
		{
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00070311 File Offset: 0x0006E511
		internal GlobalItem(MetadataItem.MetadataFlags flags) : base(flags)
		{
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0007031A File Offset: 0x0006E51A
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x00070322 File Offset: 0x0006E522
		[MetadataProperty(typeof(DataSpace), false)]
		internal DataSpace DataSpace
		{
			get
			{
				return base.GetDataSpace();
			}
			set
			{
				base.SetDataSpace(value);
			}
		}
	}
}
