using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion
{
	// Token: 0x020002B2 RID: 690
	public class MarkedForDeletionJob : BusinessBase<Guid>
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0001A304 File Offset: 0x00018504
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid MarkedForDeletionJobId
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

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0001A31C File Offset: 0x0001851C
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x0001A324 File Offset: 0x00018524
		public eMarkedForDeletionType MarkedForDeletionType { get; set; }

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0001A32D File Offset: 0x0001852D
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x0001A335 File Offset: 0x00018535
		public DateTime? DateLastRun { get; set; }

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0001A33E File Offset: 0x0001853E
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x0001A346 File Offset: 0x00018546
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0001A34F File Offset: 0x0001854F
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0001A357 File Offset: 0x00018557
		public int WhoLastModifiedPersonId { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0001A360 File Offset: 0x00018560
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x0001A368 File Offset: 0x00018568
		public bool IsActive { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0001A371 File Offset: 0x00018571
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x0001A379 File Offset: 0x00018579
		public eMarkedForDeletionRuleType RuleType { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0001A382 File Offset: 0x00018582
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x0001A38A File Offset: 0x0001858A
		public string Memo { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0001A393 File Offset: 0x00018593
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x0001A39B File Offset: 0x0001859B
		public int NumDays { get; set; }

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0001A3A4 File Offset: 0x000185A4
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x0001A3AC File Offset: 0x000185AC
		public int CustomReportId { get; set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0001A3B5 File Offset: 0x000185B5
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x0001A3BD File Offset: 0x000185BD
		public DateTime? CutoffDate { get; set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0001A3C6 File Offset: 0x000185C6
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x0001A3CE File Offset: 0x000185CE
		public IDictionary<string, string> Args { get; set; }
	}
}
