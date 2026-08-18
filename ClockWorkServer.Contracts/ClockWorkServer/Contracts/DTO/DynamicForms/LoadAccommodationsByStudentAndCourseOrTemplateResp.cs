using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200061C RID: 1564
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationsByStudentAndCourseOrTemplateResp
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0000E6E7 File Offset: 0x0000C8E7
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x0000E6EF File Offset: 0x0000C8EF
		[DataMember]
		public IList<AccommodationDataDTO> Accommodations { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x0000E700 File Offset: 0x0000C900
		[DataMember]
		public bool IsUsingTemplateAccommodations { get; set; }
	}
}
