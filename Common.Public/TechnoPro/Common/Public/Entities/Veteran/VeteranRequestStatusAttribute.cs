using System;

namespace TechnoPro.Common.Public.Entities.Veteran
{
	// Token: 0x02000111 RID: 273
	public class VeteranRequestStatusAttribute : Attribute
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public VeteranRequestStatusAttribute()
		{
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000F3DA File Offset: 0x0000D5DA
		public VeteranRequestStatusAttribute(string displayTitle)
		{
			this.DisplayTitle = displayTitle;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public string DisplayTitle { get; set; }
	}
}
