using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BBA RID: 3002
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveJobsByStudentReq : BaseMessageReq
	{
		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x0001F35B File Offset: 0x0001D55B
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x0001F363 File Offset: 0x0001D563
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x0001F36C File Offset: 0x0001D56C
		// (set) Token: 0x06003F71 RID: 16241 RVA: 0x0001F374 File Offset: 0x0001D574
		[DataMember]
		public int CampusId { get; set; }
	}
}
