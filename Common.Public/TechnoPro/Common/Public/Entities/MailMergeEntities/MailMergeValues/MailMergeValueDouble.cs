using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D6 RID: 726
	public class MailMergeValueDouble : MailMergeValueBase
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0001B53D File Offset: 0x0001973D
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0001B545 File Offset: 0x00019745
		public double Value { get; set; }

		// Token: 0x060015EA RID: 5610 RVA: 0x0001B54E File Offset: 0x0001974E
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<double>(obj, 0.0);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0001B568 File Offset: 0x00019768
		public override object GetValue()
		{
			return this.Value;
		}
	}
}
