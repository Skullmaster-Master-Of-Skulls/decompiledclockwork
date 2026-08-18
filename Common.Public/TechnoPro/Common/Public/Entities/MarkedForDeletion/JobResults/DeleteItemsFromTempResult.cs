using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults
{
	// Token: 0x020002B5 RID: 693
	public class DeleteItemsFromTempResult
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0001A3D7 File Offset: 0x000185D7
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0001A3DF File Offset: 0x000185DF
		public IList<DeleteItemFromTempResult> Items { get; set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0001A3E8 File Offset: 0x000185E8
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public bool WasSuccessful { get; set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0001A3F9 File Offset: 0x000185F9
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0001A401 File Offset: 0x00018601
		public string ErrorMessage { get; set; }
	}
}
