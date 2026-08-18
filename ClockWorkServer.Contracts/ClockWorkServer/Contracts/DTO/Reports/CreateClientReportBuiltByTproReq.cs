using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000328 RID: 808
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClientReportBuiltByTproReq : BaseReportMessageReq
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00008896 File Offset: 0x00006A96
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x0000889E File Offset: 0x00006A9E
		[DataMember]
		public ReportDTO Report { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000088A7 File Offset: 0x00006AA7
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x000088AF File Offset: 0x00006AAF
		[DataMember]
		public byte[] BuiltByTproSignedAndEncrypted { get; set; }
	}
}
