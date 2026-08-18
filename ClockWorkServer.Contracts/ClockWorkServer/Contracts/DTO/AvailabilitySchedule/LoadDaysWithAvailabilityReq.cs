using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule
{
	// Token: 0x020008D1 RID: 2257
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDaysWithAvailabilityReq : BaseMessageReq
	{
		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x0001592D File Offset: 0x00013B2D
		// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x00015935 File Offset: 0x00013B35
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x0001593E File Offset: 0x00013B3E
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x00015946 File Offset: 0x00013B46
		[DataMember]
		public IList<int> AvailabilityGroupIds { get; set; }

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x0001594F File Offset: 0x00013B4F
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x00015957 File Offset: 0x00013B57
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x00015960 File Offset: 0x00013B60
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x00015968 File Offset: 0x00013B68
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
