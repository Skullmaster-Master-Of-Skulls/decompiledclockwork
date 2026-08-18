using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000965 RID: 2405
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateNoShowValueReq : BaseMessageReq
	{
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00017EE5 File Offset: 0x000160E5
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x00017EED File Offset: 0x000160ED
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x00017EF6 File Offset: 0x000160F6
		// (set) Token: 0x0600311B RID: 12571 RVA: 0x00017EFE File Offset: 0x000160FE
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x00017F07 File Offset: 0x00016107
		// (set) Token: 0x0600311D RID: 12573 RVA: 0x00017F0F File Offset: 0x0001610F
		[DataMember]
		public bool NoShowValue { get; set; }
	}
}
