using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D5 RID: 725
	public class MailMergeValueDateTimeNullable : MailMergeValueBase
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0001B4A5 File Offset: 0x000196A5
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0001B4AD File Offset: 0x000196AD
		public DateTime? Value { get; set; }

		// Token: 0x060015E5 RID: 5605 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public override void SetValue(object obj)
		{
			bool flag = obj == null;
			if (flag)
			{
				this.Value = null;
			}
			else
			{
				bool flag2 = obj is DateTime;
				if (flag2)
				{
					this.Value = new DateTime?(base.GetValue<DateTime>(obj, DateTime.MinValue));
				}
				else
				{
					this.Value = base.GetValue<DateTime?>(obj, null);
				}
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0001B520 File Offset: 0x00019720
		public override object GetValue()
		{
			return this.Value;
		}
	}
}
