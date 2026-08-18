using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009EC RID: 2540
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTestReq : BaseMessageReq
	{
		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x00019C05 File Offset: 0x00017E05
		// (set) Token: 0x060034E8 RID: 13544 RVA: 0x00019C0D File Offset: 0x00017E0D
		[DataMember]
		public TestForEdit2DTO Test { get; set; }

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x00019C16 File Offset: 0x00017E16
		// (set) Token: 0x060034EA RID: 13546 RVA: 0x00019C1E File Offset: 0x00017E1E
		[DataMember]
		public IList<DynamicDataDTO> StudentAdditionalInfoData { get; set; }

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x00019C27 File Offset: 0x00017E27
		// (set) Token: 0x060034EC RID: 13548 RVA: 0x00019C2F File Offset: 0x00017E2F
		[DataMember]
		public IList<AccommodationForTestDTO> InstructorFormData { get; set; }

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x00019C38 File Offset: 0x00017E38
		// (set) Token: 0x060034EE RID: 13550 RVA: 0x00019C40 File Offset: 0x00017E40
		[DataMember]
		public IList<ExamFileDTO> ExamFiles { get; set; }

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x00019C49 File Offset: 0x00017E49
		// (set) Token: 0x060034F0 RID: 13552 RVA: 0x00019C51 File Offset: 0x00017E51
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
