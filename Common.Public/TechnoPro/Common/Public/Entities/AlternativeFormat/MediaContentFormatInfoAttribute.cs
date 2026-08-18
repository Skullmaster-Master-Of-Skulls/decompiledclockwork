using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200057A RID: 1402
	public class MediaContentFormatInfoAttribute : Attribute
	{
		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000320B9 File Offset: 0x000302B9
		// (set) Token: 0x06002D23 RID: 11555 RVA: 0x000320C1 File Offset: 0x000302C1
		public string Title { get; set; }

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000320CA File Offset: 0x000302CA
		// (set) Token: 0x06002D25 RID: 11557 RVA: 0x000320D2 File Offset: 0x000302D2
		public string Definition { get; set; }

		// Token: 0x06002D26 RID: 11558 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public MediaContentFormatInfoAttribute()
		{
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000320DB File Offset: 0x000302DB
		public MediaContentFormatInfoAttribute(string title, string definition)
		{
			this.Definition = definition;
			this.Title = title;
		}
	}
}
