using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A31 RID: 2609
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTestAccommodationsReq : BaseMessageReq
	{
		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x0001A1BB File Offset: 0x000183BB
		// (set) Token: 0x060035D9 RID: 13785 RVA: 0x0001A1C3 File Offset: 0x000183C3
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x0001A1CC File Offset: 0x000183CC
		// (set) Token: 0x060035DB RID: 13787 RVA: 0x0001A1D4 File Offset: 0x000183D4
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x060035DC RID: 13788 RVA: 0x0001A1DD File Offset: 0x000183DD
		// (set) Token: 0x060035DD RID: 13789 RVA: 0x0001A1E5 File Offset: 0x000183E5
		[DataMember]
		public IList<int> ControlIdsToRemove { get; set; }

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x060035DE RID: 13790 RVA: 0x0001A1EE File Offset: 0x000183EE
		// (set) Token: 0x060035DF RID: 13791 RVA: 0x0001A1F6 File Offset: 0x000183F6
		[DataMember]
		public IList<int> ControlIdsToAdd { get; set; }
	}
}
