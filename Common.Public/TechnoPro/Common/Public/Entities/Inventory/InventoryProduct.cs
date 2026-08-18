using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x0200030F RID: 783
	public class InventoryProduct : BusinessBase<Guid>, IDisposable
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0001D37C File Offset: 0x0001B57C
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public Guid UniqueId
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

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x0001D394 File Offset: 0x0001B594
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x0001D39C File Offset: 0x0001B59C
		public string Name { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x0001D3A5 File Offset: 0x0001B5A5
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x0001D3AD File Offset: 0x0001B5AD
		public string SerialNumber { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0001D3B6 File Offset: 0x0001B5B6
		// (set) Token: 0x06001853 RID: 6227 RVA: 0x0001D3BE File Offset: 0x0001B5BE
		public string BarCode { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0001D3C7 File Offset: 0x0001B5C7
		// (set) Token: 0x06001855 RID: 6229 RVA: 0x0001D3CF File Offset: 0x0001B5CF
		public bool IsLoaned { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		// (set) Token: 0x06001857 RID: 6231 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		public InventoryProductStatus Status { get; set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0001D3E9 File Offset: 0x0001B5E9
		// (set) Token: 0x06001859 RID: 6233 RVA: 0x0001D3F1 File Offset: 0x0001B5F1
		public string CategoryName { get; set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		// (set) Token: 0x0600185B RID: 6235 RVA: 0x0001D402 File Offset: 0x0001B602
		public string Description { get; set; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0001D40B File Offset: 0x0001B60B
		// (set) Token: 0x0600185D RID: 6237 RVA: 0x0001D413 File Offset: 0x0001B613
		public string Notes { get; set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0001D41C File Offset: 0x0001B61C
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x0001D424 File Offset: 0x0001B624
		public Image Thumbnail { get; set; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0001D42D File Offset: 0x0001B62D
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x0001D435 File Offset: 0x0001B635
		public InventoryVendorInfo Vendor { get; set; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0001D43E File Offset: 0x0001B63E
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x0001D446 File Offset: 0x0001B646
		public InventoryLocation Location { get; set; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0001D44F File Offset: 0x0001B64F
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0001D457 File Offset: 0x0001B657
		public DateTime? LocationDatetime { get; set; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0001D460 File Offset: 0x0001B660
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0001D468 File Offset: 0x0001B668
		public PersonBase InChargePerson { get; set; }

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0001D471 File Offset: 0x0001B671
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0001D479 File Offset: 0x0001B679
		public InventoryGroup Group { get; set; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0001D482 File Offset: 0x0001B682
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x0001D48A File Offset: 0x0001B68A
		public int ProductDynamicDataId { get; set; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0001D493 File Offset: 0x0001B693
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x0001D49B File Offset: 0x0001B69B
		public IList<InventoryProductAccessory> Accessories { get; set; }

		// Token: 0x0600186E RID: 6254 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		~InventoryProduct()
		{
			this.Dispose(false);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0001D4EC File Offset: 0x0001B6EC
		private void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				bool flag2 = this.Thumbnail != null;
				if (flag2)
				{
					this.Thumbnail.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001433 RID: 5171
		protected bool disposed = false;
	}
}
