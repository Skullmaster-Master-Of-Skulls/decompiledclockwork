using System;
using TechnoPro.Common.DataStructure.Money;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000304 RID: 772
	public class InvoicePayment : BusinessBase<int>
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0001CA44 File Offset: 0x0001AC44
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int InvoicePaymentId
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

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0001CA64 File Offset: 0x0001AC64
		public int PaymentId { get; set; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0001CA6D File Offset: 0x0001AC6D
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0001CA75 File Offset: 0x0001AC75
		public Money AmountPaidToThisInvoice { get; set; }
	}
}
