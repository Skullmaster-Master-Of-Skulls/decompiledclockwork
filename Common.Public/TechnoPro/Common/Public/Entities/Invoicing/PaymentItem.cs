using System;
using TechnoPro.Common.DataStructure.Money;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000306 RID: 774
	public class PaymentItem : BusinessBase<int>
	{
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0001CB68 File Offset: 0x0001AD68
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PaymentItemId
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

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0001CB80 File Offset: 0x0001AD80
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x0001CB88 File Offset: 0x0001AD88
		public Money AmountPaid { get; set; }

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0001CB91 File Offset: 0x0001AD91
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x0001CB99 File Offset: 0x0001AD99
		public ePaymentMethod PaymentMethod { get; set; }
	}
}
