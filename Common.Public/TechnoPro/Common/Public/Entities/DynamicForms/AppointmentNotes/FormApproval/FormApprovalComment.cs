using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B1 RID: 945
	public class FormApprovalComment
	{
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00020D5B File Offset: 0x0001EF5B
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00020D63 File Offset: 0x0001EF63
		public Guid FormApprovalCommentId { get; set; }

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00020D6C File Offset: 0x0001EF6C
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x00020D74 File Offset: 0x0001EF74
		public FormApprovalCommentText Comment { get; set; }

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00020D7D File Offset: 0x0001EF7D
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00020D85 File Offset: 0x0001EF85
		public DateTime DateEntered { get; set; }

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00020D8E File Offset: 0x0001EF8E
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x00020D96 File Offset: 0x0001EF96
		public BasicPerson WhoEntered { get; set; }
	}
}
