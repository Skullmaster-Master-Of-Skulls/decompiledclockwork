using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000256 RID: 598
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCourseAccommodationModificationRequestItemDTO : ICloneable<StudentCourseAccommodationModificationRequestItemDTO>, ICloneable
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x000036BD File Offset: 0x000018BD
		public StudentCourseAccommodationModificationRequestItemDTO()
		{
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000062B0 File Offset: 0x000044B0
		public StudentCourseAccommodationModificationRequestItemDTO(StudentCourseAccommodationModificationRequestItemDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.StudentCourseAccommodationModificationRequestItemId = item.StudentCourseAccommodationModificationRequestItemId;
				this.RequestedAccommodationData = ((item.RequestedAccommodationData == null) ? null : item.RequestedAccommodationData.Clone());
				this.ModificationType = item.ModificationType;
				this.Note1 = item.Note1;
				this.Note2 = item.Note2;
				this.WhoEntered = ((item.WhoEntered == null) ? null : item.WhoEntered.Clone());
				this.DateEntered = item.DateEntered;
				this.Status = item.Status;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0000635A File Offset: 0x0000455A
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00006362 File Offset: 0x00004562
		[DataMember]
		public int StudentCourseAccommodationModificationRequestItemId { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0000636B File Offset: 0x0000456B
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x00006373 File Offset: 0x00004573
		[DataMember]
		public DynamicDataDTO RequestedAccommodationData { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0000637C File Offset: 0x0000457C
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00006384 File Offset: 0x00004584
		[DataMember]
		public eStudentCourseAccommodationModificationTypeDTO ModificationType { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0000638D File Offset: 0x0000458D
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00006395 File Offset: 0x00004595
		[DataMember]
		public string Note1 { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0000639E File Offset: 0x0000459E
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x000063A6 File Offset: 0x000045A6
		[DataMember]
		public string Note2 { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x000063AF File Offset: 0x000045AF
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x000063B7 File Offset: 0x000045B7
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x000063C0 File Offset: 0x000045C0
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x000063C8 File Offset: 0x000045C8
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000063D1 File Offset: 0x000045D1
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000063D9 File Offset: 0x000045D9
		[DataMember]
		public eStudentCourseAccommodationRequestStatusDTO Status { get; set; }

		// Token: 0x06000D7E RID: 3454 RVA: 0x000063E4 File Offset: 0x000045E4
		public StudentCourseAccommodationModificationRequestItemDTO Clone()
		{
			return new StudentCourseAccommodationModificationRequestItemDTO(this);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000063FC File Offset: 0x000045FC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
