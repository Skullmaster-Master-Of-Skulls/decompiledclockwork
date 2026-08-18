using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D7 RID: 2519
	[DataContract(Namespace = "http://tpro.ca")]
	public class BasicAppointmentTestExamInfoDTO
	{
		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000196E9 File Offset: 0x000178E9
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x000196F1 File Offset: 0x000178F1
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000196FA File Offset: 0x000178FA
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x00019702 File Offset: 0x00017902
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x0001970B File Offset: 0x0001790B
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x00019713 File Offset: 0x00017913
		[DataMember]
		public eClassTestType ClassTestType { get; set; }

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x0001971C File Offset: 0x0001791C
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x00019724 File Offset: 0x00017924
		[DataMember]
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x0001972D File Offset: 0x0001792D
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x00019735 File Offset: 0x00017935
		[DataMember]
		public DateTime ClassEndDateTime { get; set; }

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x0001973E File Offset: 0x0001793E
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x00019746 File Offset: 0x00017946
		[DataMember]
		public string TestNote { get; set; }

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x0001974F File Offset: 0x0001794F
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x00019757 File Offset: 0x00017957
		[DataMember]
		public string StudentNote { get; set; }
	}
}
