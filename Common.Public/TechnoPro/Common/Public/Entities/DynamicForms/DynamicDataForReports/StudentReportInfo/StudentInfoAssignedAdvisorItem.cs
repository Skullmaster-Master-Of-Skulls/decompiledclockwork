using System;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020003A7 RID: 935
	public class StudentInfoAssignedAdvisorItem : StudentInfoItemBase
	{
		// Token: 0x06001C6D RID: 7277 RVA: 0x000209D6 File Offset: 0x0001EBD6
		public StudentInfoAssignedAdvisorItem()
		{
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000209E0 File Offset: 0x0001EBE0
		public StudentInfoAssignedAdvisorItem(int studentPid, StudentCommonInfo commonInfo)
		{
			base.PersonId = studentPid;
			PersonBase personBase = (commonInfo != null) ? commonInfo.AssignedCounsellor : null;
			this.AdvisorPersonId = ((personBase != null) ? personBase.PersonId : 0);
			this.AdvisorFirstName = (((personBase != null) ? personBase.FirstName : null) ?? "");
			this.AdvisorLastName = (((personBase != null) ? personBase.LastName : null) ?? "");
			this.AdvisorName = ((personBase != null) ? personBase.GetName() : null);
			this.AdvisorPhone = (((commonInfo != null) ? commonInfo.AssignedCounsellorPhone : null) ?? "");
			this.AdvisorTitle = (((commonInfo != null) ? commonInfo.AssignedCounsellorTitle : null) ?? "");
			this.AdvisorEmail = (((commonInfo != null) ? commonInfo.AssignedCounsellorEmail : null) ?? "");
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00020ABC File Offset: 0x0001ECBC
		// (set) Token: 0x06001C70 RID: 7280 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public int AdvisorPersonId { get; set; }

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00020ACD File Offset: 0x0001ECCD
		// (set) Token: 0x06001C72 RID: 7282 RVA: 0x00020AD5 File Offset: 0x0001ECD5
		public string AdvisorFirstName { get; set; }

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00020ADE File Offset: 0x0001ECDE
		// (set) Token: 0x06001C74 RID: 7284 RVA: 0x00020AE6 File Offset: 0x0001ECE6
		public string AdvisorLastName { get; set; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00020AEF File Offset: 0x0001ECEF
		// (set) Token: 0x06001C76 RID: 7286 RVA: 0x00020AF7 File Offset: 0x0001ECF7
		public string AdvisorName { get; set; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00020B00 File Offset: 0x0001ED00
		// (set) Token: 0x06001C78 RID: 7288 RVA: 0x00020B08 File Offset: 0x0001ED08
		public string AdvisorTitle { get; set; }

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00020B11 File Offset: 0x0001ED11
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x00020B19 File Offset: 0x0001ED19
		public string AdvisorEmail { get; set; }

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00020B22 File Offset: 0x0001ED22
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x00020B2A File Offset: 0x0001ED2A
		public string AdvisorPhone { get; set; }
	}
}
