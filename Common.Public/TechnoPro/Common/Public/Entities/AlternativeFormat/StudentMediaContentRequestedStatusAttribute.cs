using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200057E RID: 1406
	public class StudentMediaContentRequestedStatusAttribute : Attribute
	{
		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x00032190 File Offset: 0x00030390
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x00032198 File Offset: 0x00030398
		public string Title { get; set; }

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000321A1 File Offset: 0x000303A1
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x000321A9 File Offset: 0x000303A9
		public string Action { get; set; }

		// Token: 0x06002D3E RID: 11582 RVA: 0x000321B2 File Offset: 0x000303B2
		public StudentMediaContentRequestedStatusAttribute(string title, string action)
		{
			this.Title = title;
			this.Action = action;
		}
	}
}
