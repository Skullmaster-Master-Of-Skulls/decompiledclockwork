using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000322 RID: 802
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateClientReportBuiltByTproReq : BaseReportMessageReq
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00008830 File Offset: 0x00006A30
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x00008838 File Offset: 0x00006A38
		[DataMember]
		public ReportDTO Report { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00008841 File Offset: 0x00006A41
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x00008849 File Offset: 0x00006A49
		[DataMember]
		public byte[] BuiltByTproSignedAndEncrypted { get; set; }
	}
}
