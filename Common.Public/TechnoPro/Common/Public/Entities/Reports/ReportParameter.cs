using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000229 RID: 553
	public class ReportParameter : ICloneable<ReportParameter>, ICloneable
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x0000D55A File Offset: 0x0000B75A
		public ReportParameter()
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00017DB8 File Offset: 0x00015FB8
		public ReportParameter(ReportParameter item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Name = item.Name;
				this.Value = item.Value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00017DF1 File Offset: 0x00015FF1
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x00017DF9 File Offset: 0x00015FF9
		public virtual string Name { get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00017E02 File Offset: 0x00016002
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x00017E0A File Offset: 0x0001600A
		public virtual object Value { get; set; }

		// Token: 0x06001107 RID: 4359 RVA: 0x00017E14 File Offset: 0x00016014
		public ReportParameter Clone()
		{
			return new ReportParameter(this);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00017E2C File Offset: 0x0001602C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
