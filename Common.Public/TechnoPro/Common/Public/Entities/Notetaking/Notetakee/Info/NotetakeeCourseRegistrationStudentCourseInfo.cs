using System;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Info
{
	// Token: 0x02000289 RID: 649
	public class NotetakeeCourseRegistrationStudentCourseInfo
	{
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0001998B File Offset: 0x00017B8B
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00019993 File Offset: 0x00017B93
		public int AssignedProviderServiceProviderRequestId { get; set; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0001999C File Offset: 0x00017B9C
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x000199A4 File Offset: 0x00017BA4
		public DateTime? AssignedProviderDateAssigned { get; set; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x000199AD File Offset: 0x00017BAD
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x000199B5 File Offset: 0x00017BB5
		public int AssignedProviderId { get; set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000199BE File Offset: 0x00017BBE
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x000199C6 File Offset: 0x00017BC6
		public int SelfRegistrationRequestId { get; set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000199CF File Offset: 0x00017BCF
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x000199D7 File Offset: 0x00017BD7
		public eStudentCourseAccommodationRequestStatus SelfRegistrationRequestStatus { get; set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000199E0 File Offset: 0x00017BE0
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x000199E8 File Offset: 0x00017BE8
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x000199F1 File Offset: 0x00017BF1
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x000199F9 File Offset: 0x00017BF9
		public bool IsSelfRegRequestApproved { get; set; }
	}
}
