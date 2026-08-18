using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B30 RID: 2864
	[DataContract(Namespace = "http://tpro.ca")]
	public class SwapAppointmentTypeForAllAppointmentsReq : BaseMessageReq
	{
		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x0001D41F File Offset: 0x0001B61F
		// (set) Token: 0x06003C47 RID: 15431 RVA: 0x0001D427 File Offset: 0x0001B627
		[DataMember]
		public int AppTypeIdToReplace { get; set; }

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x0001D430 File Offset: 0x0001B630
		// (set) Token: 0x06003C49 RID: 15433 RVA: 0x0001D438 File Offset: 0x0001B638
		[DataMember]
		public int AppTypeIdToKeep { get; set; }
	}
}
