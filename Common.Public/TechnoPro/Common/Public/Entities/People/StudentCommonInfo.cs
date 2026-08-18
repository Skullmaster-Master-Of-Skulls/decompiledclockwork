using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000263 RID: 611
	public class StudentCommonInfo : BusinessBase<int>
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00018A48 File Offset: 0x00016C48
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00018A60 File Offset: 0x00016C60
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00018A68 File Offset: 0x00016C68
		public string Email { get; set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00018A71 File Offset: 0x00016C71
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00018A79 File Offset: 0x00016C79
		public bool OkToEmail { get; set; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00018A82 File Offset: 0x00016C82
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00018A8A File Offset: 0x00016C8A
		public PersonBase AssignedCounsellor { get; set; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00018A93 File Offset: 0x00016C93
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00018A9B File Offset: 0x00016C9B
		public string AssignedCounsellorTitle { get; set; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00018AA4 File Offset: 0x00016CA4
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00018AAC File Offset: 0x00016CAC
		public string AssignedCounsellorPhone { get; set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00018AB5 File Offset: 0x00016CB5
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00018ABD File Offset: 0x00016CBD
		public string AssignedCounsellorEmail { get; set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00018AC6 File Offset: 0x00016CC6
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x00018ACE File Offset: 0x00016CCE
		public string Phone { get; set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00018AD7 File Offset: 0x00016CD7
		// (set) Token: 0x06001272 RID: 4722 RVA: 0x00018ADF File Offset: 0x00016CDF
		public DateTime? DateOfBirth { get; set; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00018AE8 File Offset: 0x00016CE8
		// (set) Token: 0x06001274 RID: 4724 RVA: 0x00018AF0 File Offset: 0x00016CF0
		public eGender Gender { get; set; }
	}
}
