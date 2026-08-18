using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x0200042A RID: 1066
	public class CustomDataFile : CustomDataHolder
	{
		// Token: 0x06002054 RID: 8276 RVA: 0x0002489A File Offset: 0x00022A9A
		public CustomDataFile()
		{
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000248A4 File Offset: 0x00022AA4
		public CustomDataFile(CustomDataHolder dataObj) : base(dataObj)
		{
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000248AF File Offset: 0x00022AAF
		public CustomDataFile(Guid dataInstanceId, eCustomDataPrimitiveType dataType) : base(dataInstanceId, dataType)
		{
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x000248DD File Offset: 0x00022ADD
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x000248E5 File Offset: 0x00022AE5
		public Guid? FileId { get; set; }

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000248EE File Offset: 0x00022AEE
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000248F6 File Offset: 0x00022AF6
		public string Filename { get; set; }

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x000248FF File Offset: 0x00022AFF
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x00024907 File Offset: 0x00022B07
		public long FileSize { get; set; }
	}
}
