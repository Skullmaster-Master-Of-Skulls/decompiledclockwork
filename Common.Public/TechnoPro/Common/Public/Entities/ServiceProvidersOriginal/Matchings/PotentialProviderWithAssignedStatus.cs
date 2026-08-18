using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.Matchings
{
	// Token: 0x02000209 RID: 521
	public class PotentialProviderWithAssignedStatus : BusinessBase<int, int>
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00017094 File Offset: 0x00015294
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000170AC File Offset: 0x000152AC
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public virtual int LuCourseId
		{
			get
			{
				return this.SecondId;
			}
			set
			{
				this.SecondId = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000170C4 File Offset: 0x000152C4
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x000170CC File Offset: 0x000152CC
		public LookupCourseBase Course { get; set; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000170D5 File Offset: 0x000152D5
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000170DD File Offset: 0x000152DD
		public LookupInstructor PrimaryInstructor { get; set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000170E6 File Offset: 0x000152E6
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000170EE File Offset: 0x000152EE
		public bool IsAssigned { get; set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000170F7 File Offset: 0x000152F7
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000170FF File Offset: 0x000152FF
		public int AssignedCount { get; set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00017108 File Offset: 0x00015308
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00017110 File Offset: 0x00015310
		public string FirstName { get; set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00017119 File Offset: 0x00015319
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00017121 File Offset: 0x00015321
		public string LastName { get; set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0001712A File Offset: 0x0001532A
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00017132 File Offset: 0x00015332
		public string MiddleName { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0001713B File Offset: 0x0001533B
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00017143 File Offset: 0x00015343
		public string StudentNumber { get; set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0001714C File Offset: 0x0001534C
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00017154 File Offset: 0x00015354
		public string Username { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0001715D File Offset: 0x0001535D
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00017165 File Offset: 0x00015365
		public string Email { get; set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0001716E File Offset: 0x0001536E
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00017176 File Offset: 0x00015376
		public string Phone1 { get; set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0001717F File Offset: 0x0001537F
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00017187 File Offset: 0x00015387
		public string Phone2 { get; set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00017190 File Offset: 0x00015390
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x00017198 File Offset: 0x00015398
		public string Specialization { get; set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x000171A1 File Offset: 0x000153A1
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x000171A9 File Offset: 0x000153A9
		public string Notes1 { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000171B2 File Offset: 0x000153B2
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x000171BA File Offset: 0x000153BA
		public string Notes2 { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000171C3 File Offset: 0x000153C3
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x000171CB File Offset: 0x000153CB
		public string AdditionalServices { get; set; }
	}
}
