using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B3 RID: 947
	public class FormApprovalForAppointment
	{
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00020DB0 File Offset: 0x0001EFB0
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00020DC1 File Offset: 0x0001EFC1
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00020DC9 File Offset: 0x0001EFC9
		public int ScreenNum { get; set; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00020DD2 File Offset: 0x0001EFD2
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x00020DDA File Offset: 0x0001EFDA
		public int StudentPersonId { get; set; }

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00020DE3 File Offset: 0x0001EFE3
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x00020DEB File Offset: 0x0001EFEB
		public int AppointmentId { get; set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00020DF4 File Offset: 0x0001EFF4
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x00020DFC File Offset: 0x0001EFFC
		public DateTime DateCreated { get; set; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00020E05 File Offset: 0x0001F005
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x00020E0D File Offset: 0x0001F00D
		public IList<FormApprovalComment> Comments { get; set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00020E16 File Offset: 0x0001F016
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x00020E1E File Offset: 0x0001F01E
		public eFormApprovalState CurrentState { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00020E27 File Offset: 0x0001F027
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x00020E2F File Offset: 0x0001F02F
		public FormApprovalApprovedInfo ApprovalInfo { get; set; }
	}
}
