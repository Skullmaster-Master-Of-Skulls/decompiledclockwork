using System;
using TechnoPro.Common.DataStructure.Money;

namespace TechnoPro.Common.Public.Entities.Invoicing
{
	// Token: 0x02000301 RID: 769
	public class InvoicableItem : BusinessBase<int>
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0001C73C File Offset: 0x0001A93C
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int InvoicableItemId
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

		// Token: 0x06001797 RID: 6039 RVA: 0x0001C754 File Offset: 0x0001A954
		public InvoicableItem()
		{
			this.TaxInfo = new TaxRule();
			this.Cost = new Money(0m);
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0001C77B File Offset: 0x0001A97B
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x0001C783 File Offset: 0x0001A983
		public string Title { get; set; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0001C78C File Offset: 0x0001A98C
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x0001C794 File Offset: 0x0001A994
		public string Description { get; set; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0001C79D File Offset: 0x0001A99D
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x0001C7A5 File Offset: 0x0001A9A5
		public Money Cost { get; set; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0001C7AE File Offset: 0x0001A9AE
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x0001C7B6 File Offset: 0x0001A9B6
		public TaxRule TaxInfo { get; set; }

		// Token: 0x060017A0 RID: 6048 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		public Money CostWithTaxes(TaxAmount Taxes)
		{
			return this.TaxInfo.Calculate(this.Cost, Taxes);
		}
	}
}
