using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000340 RID: 832
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportGroupDTO
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00008DEE File Offset: 0x00006FEE
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x00008DF6 File Offset: 0x00006FF6
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00008DFF File Offset: 0x00006FFF
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x00008E07 File Offset: 0x00007007
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00008E10 File Offset: 0x00007010
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00008E18 File Offset: 0x00007018
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00008E21 File Offset: 0x00007021
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00008E29 File Offset: 0x00007029
		[DataMember]
		public int ParentGroupId { get; set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00008E32 File Offset: 0x00007032
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x00008E3A File Offset: 0x0000703A
		[DataMember]
		public bool IsTechnoProGroup { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00008E43 File Offset: 0x00007043
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00008E4B File Offset: 0x0000704B
		[DataMember]
		public int OrderNum { get; set; }
	}
}
