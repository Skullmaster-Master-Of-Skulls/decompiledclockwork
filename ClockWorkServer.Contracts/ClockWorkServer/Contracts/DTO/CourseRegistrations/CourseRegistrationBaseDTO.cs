using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200081C RID: 2076
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseRegistrationBaseDTO
	{
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x0001412E File Offset: 0x0001232E
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x00014136 File Offset: 0x00012336
		[DataMember]
		public int CoursesId { get; set; }

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x0001413F File Offset: 0x0001233F
		// (set) Token: 0x06002A4A RID: 10826 RVA: 0x00014147 File Offset: 0x00012347
		[DataMember]
		public eRegistrationStatusDTO RegistrationStatus { get; set; }

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00014150 File Offset: 0x00012350
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x00014158 File Offset: 0x00012358
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00014161 File Offset: 0x00012361
		// (set) Token: 0x06002A4E RID: 10830 RVA: 0x00014169 File Offset: 0x00012369
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x00014172 File Offset: 0x00012372
		// (set) Token: 0x06002A50 RID: 10832 RVA: 0x0001417A File Offset: 0x0001237A
		[DataMember]
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x00014183 File Offset: 0x00012383
		// (set) Token: 0x06002A52 RID: 10834 RVA: 0x0001418B File Offset: 0x0001238B
		[DataMember]
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06002A53 RID: 10835 RVA: 0x00014194 File Offset: 0x00012394
		// (set) Token: 0x06002A54 RID: 10836 RVA: 0x0001419C File Offset: 0x0001239C
		[DataMember]
		public string CourseNote { get; set; }

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000141A5 File Offset: 0x000123A5
		// (set) Token: 0x06002A56 RID: 10838 RVA: 0x000141AD File Offset: 0x000123AD
		[DataMember]
		public DateTime? DateStudentLastViewed { get; set; }

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000141B6 File Offset: 0x000123B6
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x000141BE File Offset: 0x000123BE
		[DataMember]
		public DateTime? DateInstructorLastViewed { get; set; }

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x000141C7 File Offset: 0x000123C7
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x000141CF File Offset: 0x000123CF
		[DataMember]
		public CourseRequestBaseDTO CourseAccommodationRequestBase { get; set; }
	}
}
