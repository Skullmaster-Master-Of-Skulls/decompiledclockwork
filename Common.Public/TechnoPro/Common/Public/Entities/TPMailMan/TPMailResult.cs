using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public class TPMailResult : BusinessBase<string>
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00012093 File Offset: 0x00010293
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001209B File Offset: 0x0001029B
		public eTPMailResultStatus Status { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000120A4 File Offset: 0x000102A4
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x000120AC File Offset: 0x000102AC
		public string ErrorMessage { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000120B5 File Offset: 0x000102B5
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x000120BD File Offset: 0x000102BD
		public string ErrorMessageHtml { get; set; }
	}
}
