using System;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000372 RID: 882
	public class CoreGroupDTOAttribute : Attribute
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x00009924 File Offset: 0x00007B24
		public CoreGroupDTOAttribute()
		{
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0000992E File Offset: 0x00007B2E
		public CoreGroupDTOAttribute(string titleSingular)
		{
			this.TitleSingular = titleSingular;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00009940 File Offset: 0x00007B40
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00009948 File Offset: 0x00007B48
		public string TitleSingular { get; set; }
	}
}
