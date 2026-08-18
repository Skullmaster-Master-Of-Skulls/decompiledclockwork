using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200021C RID: 540
	public class RowFormatting
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00017802 File Offset: 0x00015A02
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0001780A File Offset: 0x00015A0A
		public string ColumnName { get; set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00017813 File Offset: 0x00015A13
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x0001781B File Offset: 0x00015A1B
		public eRowFormattingConditionType ConditionType { get; set; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00017824 File Offset: 0x00015A24
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0001782C File Offset: 0x00015A2C
		public string ConditionValue { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00017835 File Offset: 0x00015A35
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0001783D File Offset: 0x00015A3D
		public int BackColourArgB { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00017846 File Offset: 0x00015A46
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x0001784E File Offset: 0x00015A4E
		public int ForeColourArgB { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00017857 File Offset: 0x00015A57
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x0001785F File Offset: 0x00015A5F
		public bool ApplyToRow { get; set; }
	}
}
