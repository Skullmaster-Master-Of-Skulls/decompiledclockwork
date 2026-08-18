using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D4 RID: 724
	public class MailMergeValueDateTime : MailMergeValueBase
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0001B460 File Offset: 0x00019660
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0001B468 File Offset: 0x00019668
		public DateTime Value { get; set; }

		// Token: 0x060015E0 RID: 5600 RVA: 0x0001B471 File Offset: 0x00019671
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<DateTime>(obj, DateTime.MinValue);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0001B488 File Offset: 0x00019688
		public override object GetValue()
		{
			return this.Value;
		}
	}
}
