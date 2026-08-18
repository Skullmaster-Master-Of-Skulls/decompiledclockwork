using System;

namespace Telerik.Licensing
{
	// Token: 0x02000438 RID: 1080
	internal class ProductUsedPayload : RequestPayload
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x0007E80D File Offset: 0x0007CA0D
		public ProductUsedPayload(Type type, string machineId, string sessionId) : base(type, sessionId)
		{
			this.InitializeProductData(base.ProductInfo);
			this.MachineId = machineId;
			base.Type = "ProductUsed";
			this.ProductType = base.ProductInfo.ProductType;
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x0007E846 File Offset: 0x0007CA46
		// (set) Token: 0x060026BD RID: 9917 RVA: 0x0007E84E File Offset: 0x0007CA4E
		public string MachineId { get; set; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0007E857 File Offset: 0x0007CA57
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x0007E85F File Offset: 0x0007CA5F
		public string ProductName { get; set; }

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0007E868 File Offset: 0x0007CA68
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x0007E870 File Offset: 0x0007CA70
		public string ProductVersion { get; set; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0007E879 File Offset: 0x0007CA79
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x0007E881 File Offset: 0x0007CA81
		public string ProductCode { get; set; }

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0007E88A File Offset: 0x0007CA8A
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x0007E892 File Offset: 0x0007CA92
		public ProductType ProductType { get; set; }

		// Token: 0x060026C6 RID: 9926 RVA: 0x0007E89B File Offset: 0x0007CA9B
		private void InitializeProductData(ProductInfo info)
		{
			this.ProductName = info.ProductName;
			this.ProductVersion = info.Version;
			this.ProductCode = "RCAJAX";
		}

		// Token: 0x040009F2 RID: 2546
		private const string EventType = "ProductUsed";
	}
}
