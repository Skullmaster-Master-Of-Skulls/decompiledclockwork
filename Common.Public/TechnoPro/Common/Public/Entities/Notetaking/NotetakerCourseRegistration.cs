using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Notetaking
{
	// Token: 0x02000280 RID: 640
	public class NotetakerCourseRegistration : BusinessBase<int>
	{
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0001955C File Offset: 0x0001775C
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderApplicationCourseId
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

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00019574 File Offset: 0x00017774
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0001957C File Offset: 0x0001777C
		public eRegistrationStatus RegistrationStatus { get; set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00019585 File Offset: 0x00017785
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x0001958D File Offset: 0x0001778D
		public NotetakerBase Student { get; set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00019596 File Offset: 0x00017796
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0001959E File Offset: 0x0001779E
		public LookupCourse Course { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000195A7 File Offset: 0x000177A7
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x000195AF File Offset: 0x000177AF
		public DateTime DateAdded { get; set; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x000195B8 File Offset: 0x000177B8
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x000195C0 File Offset: 0x000177C0
		public PersonBase WhoAdded { get; set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000195C9 File Offset: 0x000177C9
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x000195D1 File Offset: 0x000177D1
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x000195DA File Offset: 0x000177DA
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x000195E2 File Offset: 0x000177E2
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x000195EB File Offset: 0x000177EB
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x000195F3 File Offset: 0x000177F3
		public string CourseNote { get; set; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000195FC File Offset: 0x000177FC
		// (set) Token: 0x06001359 RID: 4953 RVA: 0x00019604 File Offset: 0x00017804
		public DateTime? DateStudentLastViewed { get; set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0001960D File Offset: 0x0001780D
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x00019615 File Offset: 0x00017815
		public DateTime? DateInstructorLastViewed { get; set; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0001961E File Offset: 0x0001781E
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x00019626 File Offset: 0x00017826
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0001962F File Offset: 0x0001782F
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x00019637 File Offset: 0x00017837
		public IList<int> ExemptedInstructorAssignments { get; set; }
	}
}
