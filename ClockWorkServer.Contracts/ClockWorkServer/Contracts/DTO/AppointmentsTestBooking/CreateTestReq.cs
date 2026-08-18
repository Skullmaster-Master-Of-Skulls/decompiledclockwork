using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A2F RID: 2607
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTestReq : BaseMessageReq
	{
		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x0001A166 File Offset: 0x00018366
		// (set) Token: 0x060035CD RID: 13773 RVA: 0x0001A16E File Offset: 0x0001836E
		[DataMember]
		public TestForEdit2DTO Test { get; set; }

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x0001A177 File Offset: 0x00018377
		// (set) Token: 0x060035CF RID: 13775 RVA: 0x0001A17F File Offset: 0x0001837F
		[DataMember]
		public IList<DynamicDataDTO> StudentAdditionalInfoData { get; set; }

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x0001A188 File Offset: 0x00018388
		// (set) Token: 0x060035D1 RID: 13777 RVA: 0x0001A190 File Offset: 0x00018390
		[DataMember]
		public IList<AccommodationForTestDTO> InstructorFormData { get; set; }

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x0001A199 File Offset: 0x00018399
		// (set) Token: 0x060035D3 RID: 13779 RVA: 0x0001A1A1 File Offset: 0x000183A1
		[DataMember]
		public IList<ExamFileDTO> ExamFiles { get; set; }

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x0001A1AA File Offset: 0x000183AA
		// (set) Token: 0x060035D5 RID: 13781 RVA: 0x0001A1B2 File Offset: 0x000183B2
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
