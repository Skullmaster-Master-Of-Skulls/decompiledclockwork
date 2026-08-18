using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Field
{
	// Token: 0x0200041D RID: 1053
	public class CustomListItem : BusinessBase<Guid>
	{
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x0002466C File Offset: 0x0002286C
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid ListItemId
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

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x00024684 File Offset: 0x00022884
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x0002468C File Offset: 0x0002288C
		public string ItemCaption { get; set; }

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00024695 File Offset: 0x00022895
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0002469D File Offset: 0x0002289D
		public int OrderNum { get; set; }
	}
}
