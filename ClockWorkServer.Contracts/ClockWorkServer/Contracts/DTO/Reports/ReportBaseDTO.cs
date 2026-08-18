using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200033A RID: 826
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportBaseDTO
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x00008A83 File Offset: 0x00006C83
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x00008A8B File Offset: 0x00006C8B
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x00008A94 File Offset: 0x00006C94
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x00008A9C File Offset: 0x00006C9C
		[DataMember]
		public string ReportTitle { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00008AA5 File Offset: 0x00006CA5
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x00008AAD File Offset: 0x00006CAD
		[DataMember]
		public string ReportDescription { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00008AB6 File Offset: 0x00006CB6
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00008ABE File Offset: 0x00006CBE
		[DataMember]
		public Guid ReportUniqueId { get; set; }
	}
}
