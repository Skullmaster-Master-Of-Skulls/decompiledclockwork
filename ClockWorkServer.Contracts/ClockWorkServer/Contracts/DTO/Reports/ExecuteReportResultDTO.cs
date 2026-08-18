using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000338 RID: 824
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReportResultDTO
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x000089C8 File Offset: 0x00006BC8
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x000089D0 File Offset: 0x00006BD0
		[DataMember]
		public IList<DataTable> OptionalTables { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x000089D9 File Offset: 0x00006BD9
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x000089E1 File Offset: 0x00006BE1
		[DataMember]
		public DataTable PrimaryTable { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x000089EA File Offset: 0x00006BEA
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x000089F2 File Offset: 0x00006BF2
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000089FB File Offset: 0x00006BFB
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00008A03 File Offset: 0x00006C03
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00008A0C File Offset: 0x00006C0C
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x00008A14 File Offset: 0x00006C14
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
