using System;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x0200016D RID: 365
	public class BaseTemplate : BusinessBase<int>
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00012348 File Offset: 0x00010548
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TemplateId
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

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00012360 File Offset: 0x00010560
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00012368 File Offset: 0x00010568
		public string TemplateTitle { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00012371 File Offset: 0x00010571
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00012379 File Offset: 0x00010579
		public TemplateGroup Group { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00012382 File Offset: 0x00010582
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0001238A File Offset: 0x0001058A
		public eTemplateType TemplateType { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00012393 File Offset: 0x00010593
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0001239B File Offset: 0x0001059B
		public int OrderNum { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000123A4 File Offset: 0x000105A4
		public string TemplateGroupId
		{
			get
			{
				return (this.Group == null) ? null : this.Group.TemplateGroupId;
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000123CC File Offset: 0x000105CC
		public bool IsInSameGroupAs(Template otherTemplate)
		{
			bool flag = otherTemplate == null;
			return !flag && (otherTemplate.TemplateGroupId ?? "").Equals(this.TemplateGroupId ?? "", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00012410 File Offset: 0x00010610
		public bool IsInGroup(TemplateGroup group)
		{
			bool flag = group == null;
			return !flag && (group.TemplateGroupId ?? "").Equals(this.TemplateGroupId ?? "", StringComparison.OrdinalIgnoreCase);
		}
	}
}
