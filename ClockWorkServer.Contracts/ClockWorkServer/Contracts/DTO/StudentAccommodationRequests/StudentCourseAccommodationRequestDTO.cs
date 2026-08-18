using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000257 RID: 599
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCourseAccommodationRequestDTO : ICloneable<StudentCourseAccommodationRequestDTO>, ICloneable
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x000036BD File Offset: 0x000018BD
		public StudentCourseAccommodationRequestDTO()
		{
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00006414 File Offset: 0x00004614
		public StudentCourseAccommodationRequestDTO(StudentCourseAccommodationRequestDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.StudentCourseAccommodationRequestId = item.StudentCourseAccommodationRequestId;
				this.LuCourseId = item.LuCourseId;
				LookupCourseBaseWithPrimaryInstructorDTO courseBase = item.CourseBase;
				this.CourseBase = ((courseBase != null) ? courseBase.Clone() : null);
				PersonBaseDTO student = item.Student;
				this.Student = ((student != null) ? student.Clone() : null);
				this.Status = item.Status;
				this.DateRequested = item.DateRequested;
				this.AccommodationChangesRequested = item.AccommodationChangesRequested;
				this.AdditionalAccommodationsRequested = item.AdditionalAccommodationsRequested;
				PersonBaseDTO whoEntered = item.WhoEntered;
				this.WhoEntered = ((whoEntered != null) ? whoEntered.Clone() : null);
				this.DateEntered = item.DateEntered;
				this.Note1 = item.Note1;
				this.Note2 = item.Note2;
				PersonBaseDTO assignedAdvisor = item.AssignedAdvisor;
				this.AssignedAdvisor = ((assignedAdvisor != null) ? assignedAdvisor.Clone() : null);
				IList<StudentCourseAccommodationModificationRequestItemDTO> accommodationModificationRequests = item.AccommodationModificationRequests;
				IList<StudentCourseAccommodationModificationRequestItemDTO> accommodationModificationRequests2;
				if (accommodationModificationRequests == null)
				{
					accommodationModificationRequests2 = null;
				}
				else
				{
					accommodationModificationRequests2 = (from g in accommodationModificationRequests
					select g.Clone()).ToList<StudentCourseAccommodationModificationRequestItemDTO>();
				}
				this.AccommodationModificationRequests = accommodationModificationRequests2;
				this.DateApproved = item.DateApproved;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00006559 File Offset: 0x00004759
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00006561 File Offset: 0x00004761
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0000656A File Offset: 0x0000476A
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00006572 File Offset: 0x00004772
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0000657B File Offset: 0x0000477B
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00006583 File Offset: 0x00004783
		[DataMember]
		public LookupCourseBaseWithPrimaryInstructorDTO CourseBase { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0000658C File Offset: 0x0000478C
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x00006594 File Offset: 0x00004794
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0000659D File Offset: 0x0000479D
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x000065A5 File Offset: 0x000047A5
		[DataMember]
		public eStudentCourseAccommodationRequestStatusDTO Status { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000065AE File Offset: 0x000047AE
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x000065B6 File Offset: 0x000047B6
		[DataMember]
		public DateTime? DateRequested { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000065BF File Offset: 0x000047BF
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x000065C7 File Offset: 0x000047C7
		[DataMember]
		public bool AccommodationChangesRequested { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000065D0 File Offset: 0x000047D0
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x000065D8 File Offset: 0x000047D8
		[DataMember]
		public bool AdditionalAccommodationsRequested { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x000065E1 File Offset: 0x000047E1
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x000065E9 File Offset: 0x000047E9
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000065F2 File Offset: 0x000047F2
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x000065FA File Offset: 0x000047FA
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00006603 File Offset: 0x00004803
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0000660B File Offset: 0x0000480B
		[DataMember]
		public string Note1 { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00006614 File Offset: 0x00004814
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0000661C File Offset: 0x0000481C
		[DataMember]
		public string Note2 { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00006625 File Offset: 0x00004825
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0000662D File Offset: 0x0000482D
		[DataMember]
		public IList<StudentCourseAccommodationModificationRequestItemDTO> AccommodationModificationRequests { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00006636 File Offset: 0x00004836
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0000663E File Offset: 0x0000483E
		[DataMember]
		public DateTime? DateApproved { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00006647 File Offset: 0x00004847
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0000664F File Offset: 0x0000484F
		[DataMember]
		public PersonBaseDTO AssignedAdvisor { get; set; }

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00006658 File Offset: 0x00004858
		public StudentCourseAccommodationRequestDTO Clone()
		{
			return new StudentCourseAccommodationRequestDTO(this);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00006670 File Offset: 0x00004870
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
