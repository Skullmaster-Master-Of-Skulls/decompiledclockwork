using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000453 RID: 1107
	public class ClockWorkServerJobExecutingAttribute : Attribute
	{
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x000257B6 File Offset: 0x000239B6
		// (set) Token: 0x06002192 RID: 8594 RVA: 0x000257BE File Offset: 0x000239BE
		public string Title { get; set; }

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x000257C7 File Offset: 0x000239C7
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x000257CF File Offset: 0x000239CF
		public string ParametersDescription { get; set; }

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000257D8 File Offset: 0x000239D8
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x000257E0 File Offset: 0x000239E0
		public string ControlParametersType { get; set; }

		// Token: 0x06002197 RID: 8599 RVA: 0x000257E9 File Offset: 0x000239E9
		public ClockWorkServerJobExecutingAttribute(string title)
		{
			this.Title = title;
		}
	}
}
