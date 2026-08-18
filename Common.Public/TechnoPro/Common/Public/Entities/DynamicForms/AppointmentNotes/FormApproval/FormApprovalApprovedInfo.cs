using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B0 RID: 944
	public class FormApprovalApprovedInfo
	{
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00020D39 File Offset: 0x0001EF39
		// (set) Token: 0x06001CBE RID: 7358 RVA: 0x00020D41 File Offset: 0x0001EF41
		public PersonBase WhoApproved { get; set; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00020D4A File Offset: 0x0001EF4A
		// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x00020D52 File Offset: 0x0001EF52
		public DateTime DateApproved { get; set; }
	}
}
