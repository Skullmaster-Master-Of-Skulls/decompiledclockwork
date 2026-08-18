using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000928 RID: 2344
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppCancelReasonOrGroupDTO
	{
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x00016B3C File Offset: 0x00014D3C
		// (set) Token: 0x06002F89 RID: 12169 RVA: 0x00016B44 File Offset: 0x00014D44
		[DataMember]
		public AppCancelReasonDTO AppCancelReason { get; set; }

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x00016B4D File Offset: 0x00014D4D
		// (set) Token: 0x06002F8B RID: 12171 RVA: 0x00016B55 File Offset: 0x00014D55
		[DataMember]
		public AppCancelReasonGroupDTO AppCancelReasonGroup { get; set; }
	}
}
