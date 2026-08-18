using System;

namespace TechnoPro.Common.Public.Entities.Files.FileUpload
{
	// Token: 0x0200033C RID: 828
	public class TempFileUsageAttribute : Attribute
	{
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x0001E204 File Offset: 0x0001C404
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x0001E20C File Offset: 0x0001C40C
		public string UsageCode { get; set; }

		// Token: 0x060019CD RID: 6605 RVA: 0x0001E215 File Offset: 0x0001C415
		public TempFileUsageAttribute()
		{
			this.UsageCode = "UNK";
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0001E22B File Offset: 0x0001C42B
		public TempFileUsageAttribute(string usageCode)
		{
			this.UsageCode = usageCode;
		}
	}
}
