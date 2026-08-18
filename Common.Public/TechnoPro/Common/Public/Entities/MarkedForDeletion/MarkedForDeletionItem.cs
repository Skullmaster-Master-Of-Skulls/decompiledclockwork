using System;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion
{
	// Token: 0x020002B1 RID: 689
	public class MarkedForDeletionItem : BusinessBase<string>
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0001A27A File Offset: 0x0001847A
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x0001A282 File Offset: 0x00018482
		public Guid MarkedForDeletionId { get; set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0001A28B File Offset: 0x0001848B
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x0001A293 File Offset: 0x00018493
		public string MarkedForDeletionItemId { get; set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0001A29C File Offset: 0x0001849C
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x0001A2A4 File Offset: 0x000184A4
		public eMarkedForDeletionRuleType RuleType { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0001A2AD File Offset: 0x000184AD
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x0001A2B5 File Offset: 0x000184B5
		public bool IsExemptFromDeletion { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0001A2BE File Offset: 0x000184BE
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0001A2C6 File Offset: 0x000184C6
		public int CreatedByPersonId { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0001A2CF File Offset: 0x000184CF
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0001A2D7 File Offset: 0x000184D7
		public DateTime CreatedDate { get; set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0001A2E0 File Offset: 0x000184E0
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0001A2E8 File Offset: 0x000184E8
		public DateTime? ArchivedDate { get; set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0001A2F1 File Offset: 0x000184F1
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0001A2F9 File Offset: 0x000184F9
		public DateTime? DeletedDate { get; set; }
	}
}
