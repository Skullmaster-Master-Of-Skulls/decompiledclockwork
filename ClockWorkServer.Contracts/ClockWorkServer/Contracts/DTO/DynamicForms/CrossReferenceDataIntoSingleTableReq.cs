using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000638 RID: 1592
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferenceDataIntoSingleTableReq : BaseMessageReq
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0000EC74 File Offset: 0x0000CE74
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		[DataMember]
		public DataTable TableWithData { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x0000EC85 File Offset: 0x0000CE85
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
