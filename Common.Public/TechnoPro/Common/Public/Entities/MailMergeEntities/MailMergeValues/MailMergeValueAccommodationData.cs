using System;
using TechnoPro.Common.Public.Entities.Accommodations;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002CF RID: 719
	public class MailMergeValueAccommodationData : MailMergeValueBase
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0001B290 File Offset: 0x00019490
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x0001B298 File Offset: 0x00019498
		public AccommodationData Value { get; set; }

		// Token: 0x060015C5 RID: 5573 RVA: 0x0001B2A1 File Offset: 0x000194A1
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<AccommodationData>(obj, null);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0001B2B4 File Offset: 0x000194B4
		public override object GetValue()
		{
			return this.Value;
		}
	}
}
