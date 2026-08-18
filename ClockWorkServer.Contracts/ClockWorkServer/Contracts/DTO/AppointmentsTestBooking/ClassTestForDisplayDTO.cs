using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009AC RID: 2476
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestForDisplayDTO
	{
		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000186A2 File Offset: 0x000168A2
		// (set) Token: 0x06003248 RID: 12872 RVA: 0x000186AA File Offset: 0x000168AA
		[DataMember]
		public virtual int ExamId { get; set; }

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000186B3 File Offset: 0x000168B3
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x000186BB File Offset: 0x000168BB
		[DataMember]
		public LookupCourseBaseWithPrimaryInstructorDTO CourseWithPrimaryInstructor { get; set; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000186C4 File Offset: 0x000168C4
		// (set) Token: 0x0600324C RID: 12876 RVA: 0x000186CC File Offset: 0x000168CC
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x000186D5 File Offset: 0x000168D5
		// (set) Token: 0x0600324E RID: 12878 RVA: 0x000186DD File Offset: 0x000168DD
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000186E6 File Offset: 0x000168E6
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x000186EE File Offset: 0x000168EE
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000186F7 File Offset: 0x000168F7
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x000186FF File Offset: 0x000168FF
		[DataMember]
		public string InstructorContactedNote { get; set; }

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x00018708 File Offset: 0x00016908
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x00018710 File Offset: 0x00016910
		[DataMember]
		public eClassTestType ExamType { get; set; }

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x00018719 File Offset: 0x00016919
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x00018721 File Offset: 0x00016921
		[DataMember]
		public string Location { get; set; }

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x0001872A File Offset: 0x0001692A
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x00018732 File Offset: 0x00016932
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x0001873B File Offset: 0x0001693B
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x00018743 File Offset: 0x00016943
		[DataMember]
		public string TestPickedUpNote { get; set; }

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x0001874C File Offset: 0x0001694C
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x00018754 File Offset: 0x00016954
		[DataMember]
		public IList<DynamicDataDTO> InstructorFormData { get; set; }
	}
}
