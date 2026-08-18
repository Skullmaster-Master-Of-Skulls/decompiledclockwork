using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Money;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000302 RID: 770
	public class Invoice : BusinessBase<int>
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int InvoiceId
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

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x0001C804 File Offset: 0x0001AA04
		public DateTime DateEntered { get; set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0001C80D File Offset: 0x0001AA0D
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x0001C815 File Offset: 0x0001AA15
		public DateTime InvoiceDate { get; set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0001C81E File Offset: 0x0001AA1E
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x0001C826 File Offset: 0x0001AA26
		public PersonBase WhoEntered { get; set; }

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0001C82F File Offset: 0x0001AA2F
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x0001C837 File Offset: 0x0001AA37
		public DateTime? DateClosed { get; set; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0001C840 File Offset: 0x0001AA40
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x0001C848 File Offset: 0x0001AA48
		public IList<InvoiceItem> Items { get; set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0001C851 File Offset: 0x0001AA51
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x0001C859 File Offset: 0x0001AA59
		public IList<InvoicePayment> InvoicePayments { get; set; }

		// Token: 0x060017AF RID: 6063 RVA: 0x0001C864 File Offset: 0x0001AA64
		public Money TotalAmountDue(TaxAmount Taxes)
		{
			return Invoice.TotalAmountDue(this.DateClosed, this.Items, this.InvoicePayments, Taxes);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0001C890 File Offset: 0x0001AA90
		public static Money TotalAmountDue(DateTime? DateClosed, IList<InvoiceItem> Items, IList<InvoicePayment> InvoicePayments, TaxAmount Taxes)
		{
			bool flag = DateClosed != null;
			Money result;
			if (flag)
			{
				result = Money.Empty;
			}
			else
			{
				bool flag2 = Items != null && Items.Count > 0;
				if (flag2)
				{
					Money first = Money.Empty;
					foreach (InvoiceItem invoiceItem in Items)
					{
						first += invoiceItem.Item.CostWithTaxes(Taxes);
					}
					Money money = Money.Empty;
					bool flag3 = InvoicePayments != null;
					if (flag3)
					{
						foreach (InvoicePayment invoicePayment in InvoicePayments)
						{
							money += invoicePayment.AmountPaidToThisInvoice;
						}
					}
					result = first - money;
				}
				else
				{
					result = Money.Empty;
				}
			}
			return result;
		}
	}
}
