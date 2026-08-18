using System;

namespace Telerik.Licensing
{
	// Token: 0x02000437 RID: 1079
	internal abstract class RequestPayload
	{
		// Token: 0x060026B1 RID: 9905 RVA: 0x0007E778 File Offset: 0x0007C978
		protected RequestPayload(Type componentType, string sessionId)
		{
			this._productInfo = ProductInfo.GetProductInfo(componentType);
			this.Source = "Licenser";
			this.TimeStamp = DateTime.UtcNow.ToString("o");
			this.SessionId = sessionId;
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0007E7C1 File Offset: 0x0007C9C1
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x0007E7C9 File Offset: 0x0007C9C9
		public string Type { get; set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0007E7D2 File Offset: 0x0007C9D2
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0007E7DA File Offset: 0x0007C9DA
		public string Source { get; set; }

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0007E7E3 File Offset: 0x0007C9E3
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x0007E7EB File Offset: 0x0007C9EB
		public string SessionId { get; set; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0007E7F4 File Offset: 0x0007C9F4
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x0007E7FC File Offset: 0x0007C9FC
		public string TimeStamp { get; set; }

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0007E805 File Offset: 0x0007CA05
		protected ProductInfo ProductInfo
		{
			get
			{
				return this._productInfo;
			}
		}

		// Token: 0x040009EC RID: 2540
		private const string SourceType = "Licenser";

		// Token: 0x040009ED RID: 2541
		private readonly ProductInfo _productInfo;
	}
}
