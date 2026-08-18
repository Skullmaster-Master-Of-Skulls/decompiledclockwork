using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000347 RID: 839
	[Serializable]
	public class DynamicDataBase : BusinessBase<int>
	{
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0001E460 File Offset: 0x0001C660
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int DataId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0001E478 File Offset: 0x0001C678
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x0001E480 File Offset: 0x0001C680
		public object Value { get; set; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0001E489 File Offset: 0x0001C689
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x0001E491 File Offset: 0x0001C691
		public int ValueId { get; set; }

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0001E49A File Offset: 0x0001C69A
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x0001E4A2 File Offset: 0x0001C6A2
		public int ControlId { get; set; }
	}
}
