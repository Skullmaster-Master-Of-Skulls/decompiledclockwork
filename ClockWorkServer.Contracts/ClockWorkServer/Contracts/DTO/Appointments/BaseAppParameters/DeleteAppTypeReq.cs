using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000985 RID: 2437
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAppTypeReq : BaseMessageReq
	{
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000181F3 File Offset: 0x000163F3
		// (set) Token: 0x06003195 RID: 12693 RVA: 0x000181FB File Offset: 0x000163FB
		[DataMember]
		public int AppTypeId { get; set; }

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x00018204 File Offset: 0x00016404
		// (set) Token: 0x06003197 RID: 12695 RVA: 0x0001820C File Offset: 0x0001640C
		[DataMember]
		public bool DeleteTheAppTypeInsteadOfDisabling { get; set; }

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x00018215 File Offset: 0x00016415
		// (set) Token: 0x06003199 RID: 12697 RVA: 0x0001821D File Offset: 0x0001641D
		[DataMember]
		public int AppTypeIdToReplaceWithInExistingApps { get; set; }
	}
}
