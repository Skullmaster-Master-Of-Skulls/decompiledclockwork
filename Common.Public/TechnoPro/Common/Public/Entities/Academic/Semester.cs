using System;

namespace TechnoPro.Common.Public.Entities.Academic
{
	// Token: 0x020005E5 RID: 1509
	public class Semester : BusinessBase<int>
	{
		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000422F0 File Offset: 0x000404F0
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SemesterId
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

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x00042308 File Offset: 0x00040508
		// (set) Token: 0x060030B5 RID: 12469 RVA: 0x00042310 File Offset: 0x00040510
		public string SemesterTitle { get; set; }

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x00042319 File Offset: 0x00040519
		// (set) Token: 0x060030B7 RID: 12471 RVA: 0x00042321 File Offset: 0x00040521
		public DateTime StartDate { get; set; }

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x0004232A File Offset: 0x0004052A
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x00042332 File Offset: 0x00040532
		public DateTime EndDate { get; set; }
	}
}
