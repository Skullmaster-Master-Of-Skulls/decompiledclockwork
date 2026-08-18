using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007DD RID: 2013
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupInstructorDTO : ICloneable<LookupInstructorDTO>, ICloneable
	{
		// Token: 0x06002913 RID: 10515 RVA: 0x000036BD File Offset: 0x000018BD
		public LookupInstructorDTO()
		{
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0001374C File Offset: 0x0001194C
		public LookupInstructorDTO(LookupInstructorDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ExternalId = item.ExternalId;
				this.EmployeeId = item.EmployeeId;
				this.InstructorId = item.InstructorId;
				this.Name = item.Name;
				this.Username = item.Username;
				this.Email = item.Email;
				this.Phone = item.Phone;
				this.IsPrimary = item.IsPrimary;
				this.Percentage = item.Percentage;
				this.IsExemptFromDataSync = item.IsExemptFromDataSync;
				this.IsExemptAssignmentFromDataSync = item.IsExemptAssignmentFromDataSync;
				this.CourseSpecificInfo = ((item.CourseSpecificInfo == null) ? null : item.CourseSpecificInfo.Clone());
				this.PermissionLevel = item.PermissionLevel;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x00013827 File Offset: 0x00011A27
		// (set) Token: 0x06002916 RID: 10518 RVA: 0x0001382F File Offset: 0x00011A2F
		[DataMember]
		public string ExternalId { get; set; }

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x00013838 File Offset: 0x00011A38
		// (set) Token: 0x06002918 RID: 10520 RVA: 0x00013840 File Offset: 0x00011A40
		[DataMember]
		public string EmployeeId { get; set; }

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x00013849 File Offset: 0x00011A49
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x00013851 File Offset: 0x00011A51
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x0001385A File Offset: 0x00011A5A
		// (set) Token: 0x0600291C RID: 10524 RVA: 0x00013862 File Offset: 0x00011A62
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x0001386B File Offset: 0x00011A6B
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x00013873 File Offset: 0x00011A73
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x0001387C File Offset: 0x00011A7C
		// (set) Token: 0x06002920 RID: 10528 RVA: 0x00013884 File Offset: 0x00011A84
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x0001388D File Offset: 0x00011A8D
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x00013895 File Offset: 0x00011A95
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x0001389E File Offset: 0x00011A9E
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x000138A6 File Offset: 0x00011AA6
		[DataMember]
		public bool IsPrimary { get; set; }

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000138AF File Offset: 0x00011AAF
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x000138B7 File Offset: 0x00011AB7
		[DataMember]
		public int Percentage { get; set; }

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000138C0 File Offset: 0x00011AC0
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x000138C8 File Offset: 0x00011AC8
		[DataMember]
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x000138D1 File Offset: 0x00011AD1
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x000138D9 File Offset: 0x00011AD9
		[DataMember]
		public bool IsExemptAssignmentFromDataSync { get; set; }

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000138E2 File Offset: 0x00011AE2
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x000138EA File Offset: 0x00011AEA
		[DataMember]
		public LookupInstructorCourseInfoDTO CourseSpecificInfo { get; set; }

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000138F3 File Offset: 0x00011AF3
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x000138FB File Offset: 0x00011AFB
		[DataMember]
		public ePermissionForCourseDTO PermissionLevel { get; set; }

		// Token: 0x0600292F RID: 10543 RVA: 0x00013904 File Offset: 0x00011B04
		public LookupInstructorDTO Clone()
		{
			return new LookupInstructorDTO(this);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0001391C File Offset: 0x00011B1C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
