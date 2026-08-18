using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B5 RID: 949
	public class FormApprovalPendingItem : BusinessBase<Guid>
	{
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00020E7C File Offset: 0x0001F07C
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid FormApprovalId
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

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00020E94 File Offset: 0x0001F094
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x00020E9C File Offset: 0x0001F09C
		public int AppointmentId { get; set; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x00020EA5 File Offset: 0x0001F0A5
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00020EAD File Offset: 0x0001F0AD
		public DateTime? AppointmentDate { get; set; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00020EB6 File Offset: 0x0001F0B6
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x00020EBE File Offset: 0x0001F0BE
		public BasicPerson Student { get; set; }

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00020EC7 File Offset: 0x0001F0C7
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x00020ECF File Offset: 0x0001F0CF
		public int ScreenNum { get; set; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00020ED8 File Offset: 0x0001F0D8
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		public string ScreenTitle { get; set; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00020EE9 File Offset: 0x0001F0E9
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x00020EF1 File Offset: 0x0001F0F1
		public DateTime DateCreated { get; set; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00020EFA File Offset: 0x0001F0FA
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x00020F02 File Offset: 0x0001F102
		public eFormApprovalState CurrentState { get; set; }

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00020F0B File Offset: 0x0001F10B
		// (set) Token: 0x06001CF9 RID: 7417 RVA: 0x00020F13 File Offset: 0x0001F113
		public DateTime? LastModifiedDate { get; set; }

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00020F1C File Offset: 0x0001F11C
		// (set) Token: 0x06001CFB RID: 7419 RVA: 0x00020F24 File Offset: 0x0001F124
		public bool IsCurrentUserSupervisor { get; set; }

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00020F2D File Offset: 0x0001F12D
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x00020F35 File Offset: 0x0001F135
		public bool AppointmentIsPrivate { get; set; }

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00020F3E File Offset: 0x0001F13E
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x00020F46 File Offset: 0x0001F146
		public bool AppointmentIsLocked { get; set; }

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x00020F4F File Offset: 0x0001F14F
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x00020F57 File Offset: 0x0001F157
		public int AppointmentBookedByPersonId { get; set; }
	}
}
