using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000789 RID: 1929
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(CourseRegistrationWithStudentSpecificInfoDTO))]
	public class CourseRegistrationDTO : ICloneable<CourseRegistrationDTO>, ICloneable
	{
		// Token: 0x06002793 RID: 10131 RVA: 0x000036BD File Offset: 0x000018BD
		public CourseRegistrationDTO()
		{
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0001287E File Offset: 0x00010A7E
		public CourseRegistrationDTO(CourseRegistrationDTO item)
		{
			this.CloneItem<CourseRegistrationDTO>(item);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00012890 File Offset: 0x00010A90
		public CourseRegistrationDTO(CourseRegistrationWithStudentSpecificInfoDTO item)
		{
			this.CloneItem<CourseRegistrationWithStudentSpecificInfoDTO>(item);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000128A4 File Offset: 0x00010AA4
		public void CloneItem<T>(T item) where T : CourseRegistrationDTO
		{
			bool flag = item == null;
			if (!flag)
			{
				this.CoursesId = item.CoursesId;
				this.RegistrationStatus = item.RegistrationStatus;
				this.Student = item.Student;
				this.Course = item.Course;
				this.DateAdded = item.DateAdded;
				this.WhoAdded = item.WhoAdded;
				this.DateLetterIssued = item.DateLetterIssued;
				this.DateLetterReturned = item.DateLetterReturned;
				this.CourseNote = item.CourseNote;
				this.DateStudentLastViewed = item.DateStudentLastViewed;
				this.DateInstructorLastViewed = item.DateInstructorLastViewed;
				this.IsExemptFromDataSync = item.IsExemptFromDataSync;
				this.ExemptedInstructorAssignments = item.ExemptedInstructorAssignments;
				this.CourseAccommodationRequestBase = item.CourseAccommodationRequestBase;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000129C0 File Offset: 0x00010BC0
		// (set) Token: 0x06002798 RID: 10136 RVA: 0x000129C8 File Offset: 0x00010BC8
		[DataMember]
		public int CoursesId { get; set; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x000129D1 File Offset: 0x00010BD1
		// (set) Token: 0x0600279A RID: 10138 RVA: 0x000129D9 File Offset: 0x00010BD9
		[DataMember]
		public eRegistrationStatusDTO RegistrationStatus { get; set; }

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000129E2 File Offset: 0x00010BE2
		// (set) Token: 0x0600279C RID: 10140 RVA: 0x000129EA File Offset: 0x00010BEA
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x000129F3 File Offset: 0x00010BF3
		// (set) Token: 0x0600279E RID: 10142 RVA: 0x000129FB File Offset: 0x00010BFB
		[DataMember]
		public LookupCourseDTO Course { get; set; }

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00012A04 File Offset: 0x00010C04
		// (set) Token: 0x060027A0 RID: 10144 RVA: 0x00012A0C File Offset: 0x00010C0C
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x00012A15 File Offset: 0x00010C15
		// (set) Token: 0x060027A2 RID: 10146 RVA: 0x00012A1D File Offset: 0x00010C1D
		[DataMember]
		public PersonBaseDTO WhoAdded { get; set; }

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x00012A26 File Offset: 0x00010C26
		// (set) Token: 0x060027A4 RID: 10148 RVA: 0x00012A2E File Offset: 0x00010C2E
		[DataMember]
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00012A37 File Offset: 0x00010C37
		// (set) Token: 0x060027A6 RID: 10150 RVA: 0x00012A3F File Offset: 0x00010C3F
		[DataMember]
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00012A48 File Offset: 0x00010C48
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x00012A50 File Offset: 0x00010C50
		[DataMember]
		public string CourseNote { get; set; }

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00012A59 File Offset: 0x00010C59
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x00012A61 File Offset: 0x00010C61
		[DataMember]
		public DateTime? DateStudentLastViewed { get; set; }

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00012A6A File Offset: 0x00010C6A
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x00012A72 File Offset: 0x00010C72
		[DataMember]
		public DateTime? DateInstructorLastViewed { get; set; }

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x00012A7B File Offset: 0x00010C7B
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x00012A83 File Offset: 0x00010C83
		[DataMember]
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x00012A8C File Offset: 0x00010C8C
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x00012A94 File Offset: 0x00010C94
		[DataMember]
		public IList<int> ExemptedInstructorAssignments { get; set; }

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x00012A9D File Offset: 0x00010C9D
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x00012AA5 File Offset: 0x00010CA5
		[DataMember]
		public CourseRequestBaseDTO CourseAccommodationRequestBase { get; set; }

		// Token: 0x060027B3 RID: 10163 RVA: 0x00012AB0 File Offset: 0x00010CB0
		public CourseRegistrationDTO Clone()
		{
			return new CourseRegistrationDTO(this);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x00012AC8 File Offset: 0x00010CC8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
