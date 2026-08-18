using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000DE RID: 222
	public class ProductDescriptionAttribute : Attribute
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		public string ProductTitle { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000E2C9 File Offset: 0x0000C4C9
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000E2D1 File Offset: 0x0000C4D1
		public string ProductDescription { get; set; }

		// Token: 0x0600053D RID: 1341 RVA: 0x0000E2DA File Offset: 0x0000C4DA
		public ProductDescriptionAttribute(string title, string description)
		{
			this.ProductTitle = title;
			this.ProductDescription = description;
		}
	}
}
