using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020003C1 RID: 961
	public abstract class GlobalItem : MetadataItem
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x000A42D3 File Offset: 0x000A24D3
		internal GlobalItem()
		{
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000A42DB File Offset: 0x000A24DB
		internal GlobalItem(MetadataItem.MetadataFlags flags) : base(flags)
		{
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000A42E4 File Offset: 0x000A24E4
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x000A42EC File Offset: 0x000A24EC
		[MetadataProperty(typeof(DataSpace), false)]
		internal virtual DataSpace DataSpace
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
