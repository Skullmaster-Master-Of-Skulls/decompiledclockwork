using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A83 RID: 2691
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedSeatsReq : BaseMessageReq
	{
		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x0001B55D File Offset: 0x0001975D
		// (set) Token: 0x06003857 RID: 14423 RVA: 0x0001B565 File Offset: 0x00019765
		[DataMember]
		public eTestExamSeatType ClassTestType { get; set; }
	}
}
