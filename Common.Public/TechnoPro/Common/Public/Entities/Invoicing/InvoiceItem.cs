using System;
using TechnoPro.Common.DataStructure.Money;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000303 RID: 771
	public class InvoiceItem : BusinessBase<int>
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0001C990 File Offset: 0x0001AB90
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int InvoiceItemId
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

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x0001C9B0 File Offset: 0x0001ABB0
		public InvoicableItem Item { get; set; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0001C9B9 File Offset: 0x0001ABB9
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0001C9C1 File Offset: 0x0001ABC1
		public int Quantity { get; set; }

		// Token: 0x060017B8 RID: 6072 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		public Money Subtotal()
		{
			return (this.Item == null) ? Money.Empty : this.Item.Cost;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		public Money TotalCostWithTaxes(TaxAmount taxes)
		{
			return InvoiceItem.TotalCostWithTaxes(taxes, this.Item);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0001CA18 File Offset: 0x0001AC18
		public static Money TotalCostWithTaxes(TaxAmount taxes, InvoicableItem Item)
		{
			bool flag = Item == null;
			Money result;
			if (flag)
			{
				result = Money.Empty;
			}
			else
			{
				result = Item.CostWithTaxes(taxes);
			}
			return result;
		}
	}
}
