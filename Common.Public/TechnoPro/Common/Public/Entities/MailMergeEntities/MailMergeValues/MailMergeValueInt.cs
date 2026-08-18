using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D8 RID: 728
	public class MailMergeValueInt : MailMergeValueBase
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0001B5C0 File Offset: 0x000197C0
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x0001B5C8 File Offset: 0x000197C8
		public int Value { get; set; }

		// Token: 0x060015F4 RID: 5620 RVA: 0x0001B5D1 File Offset: 0x000197D1
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<int>(obj, 0);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0001B5E4 File Offset: 0x000197E4
		public override object GetValue()
		{
			return this.Value;
		}
	}
}
