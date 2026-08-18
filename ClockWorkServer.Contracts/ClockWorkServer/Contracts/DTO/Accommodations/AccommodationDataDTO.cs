using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations
{
	// Token: 0x02000C96 RID: 3222
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccommodationDataDTO
	{
		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x00024588 File Offset: 0x00022788
		// (set) Token: 0x06004339 RID: 17209 RVA: 0x00024590 File Offset: 0x00022790
		[DataMember]
		public DynamicDataDTO Data { get; set; }

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x00024599 File Offset: 0x00022799
		// (set) Token: 0x0600433B RID: 17211 RVA: 0x000245A1 File Offset: 0x000227A1
		[DataMember]
		public ExtendedAccommodationInfoDTO Detail { get; set; }
	}
}
