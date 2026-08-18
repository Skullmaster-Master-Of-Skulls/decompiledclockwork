using System;

namespace TechnoPro.Common.Public.Entities.OnlineForms
{
	// Token: 0x02000274 RID: 628
	public class OnlineFormStatusTypeAttribute : Attribute
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public OnlineFormStatusTypeAttribute()
		{
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000190EB File Offset: 0x000172EB
		public OnlineFormStatusTypeAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x000190FD File Offset: 0x000172FD
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00019105 File Offset: 0x00017305
		public string Title { get; set; }
	}
}
