using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000226 RID: 550
	public class ReportGroup : BusinessBase<int>
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00017CA4 File Offset: 0x00015EA4
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int GroupId
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

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00017CBC File Offset: 0x00015EBC
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x00017CC4 File Offset: 0x00015EC4
		public string Title { get; set; }

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00017CCD File Offset: 0x00015ECD
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x00017CD5 File Offset: 0x00015ED5
		public string Description { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00017CDE File Offset: 0x00015EDE
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x00017CE6 File Offset: 0x00015EE6
		public int ParentGroupId { get; set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00017CEF File Offset: 0x00015EEF
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x00017CF7 File Offset: 0x00015EF7
		public bool IsTechnoProGroup { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00017D00 File Offset: 0x00015F00
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x00017D08 File Offset: 0x00015F08
		public int OrderNum { get; set; }
	}
}
