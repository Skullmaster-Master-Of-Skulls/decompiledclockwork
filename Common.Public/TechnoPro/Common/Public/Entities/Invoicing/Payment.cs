using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Money;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000305 RID: 773
	public class Payment : BusinessBase<int>
	{
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0001CA7E File Offset: 0x0001AC7E
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x0001CA86 File Offset: 0x0001AC86
		public virtual int PaymentId { get; set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0001CA8F File Offset: 0x0001AC8F
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x0001CA97 File Offset: 0x0001AC97
		public DateTime DatePaid { get; set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		// (set) Token: 0x060017C8 RID: 6088 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		public PersonBase WhoCollected { get; set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0001CAB1 File Offset: 0x0001ACB1
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x0001CAB9 File Offset: 0x0001ACB9
		public IList<PaymentItem> Payments { get; set; }

		// Token: 0x060017CB RID: 6091 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		public Money TotalPaid()
		{
			return Payment.TotalPaid(this.Payments);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public static Money TotalPaid(IList<PaymentItem> Payments)
		{
			bool flag = Payments == null || Payments.Count < 1;
			Money result;
			if (flag)
			{
				result = Money.Empty;
			}
			else
			{
				Money money = Money.Empty;
				foreach (PaymentItem paymentItem in Payments)
				{
					money += (paymentItem.AmountPaid ?? Money.Empty);
				}
				result = money;
			}
			return result;
		}
	}
}
