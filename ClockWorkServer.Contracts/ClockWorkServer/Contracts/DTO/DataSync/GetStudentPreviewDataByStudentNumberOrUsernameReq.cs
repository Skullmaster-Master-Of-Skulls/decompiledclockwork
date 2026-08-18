using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000721 RID: 1825
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentPreviewDataByStudentNumberOrUsernameReq : BaseReportMessageReq
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x00011287 File Offset: 0x0000F487
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x0001128F File Offset: 0x0000F48F
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x00011298 File Offset: 0x0000F498
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x000112A0 File Offset: 0x0000F4A0
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
