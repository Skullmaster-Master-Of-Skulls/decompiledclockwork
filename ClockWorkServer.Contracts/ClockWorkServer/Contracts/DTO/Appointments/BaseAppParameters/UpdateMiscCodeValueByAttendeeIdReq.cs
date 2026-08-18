using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000968 RID: 2408
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMiscCodeValueByAttendeeIdReq : BaseMessageReq
	{
		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x00017F6D File Offset: 0x0001616D
		// (set) Token: 0x0600312C RID: 12588 RVA: 0x00017F75 File Offset: 0x00016175
		[DataMember]
		public int AttendeeId { get; set; }

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x00017F7E File Offset: 0x0001617E
		// (set) Token: 0x0600312E RID: 12590 RVA: 0x00017F86 File Offset: 0x00016186
		[DataMember]
		public int MiscCodeValue { get; set; }
	}
}
