using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D5 RID: 1749
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalPendingItemDTO
	{
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000104C8 File Offset: 0x0000E6C8
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x000104D0 File Offset: 0x0000E6D0
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x000104D9 File Offset: 0x0000E6D9
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x000104E1 File Offset: 0x0000E6E1
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000104EA File Offset: 0x0000E6EA
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x000104F2 File Offset: 0x0000E6F2
		[DataMember]
		public DateTime? AppointmentDate { get; set; }

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000104FB File Offset: 0x0000E6FB
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x00010503 File Offset: 0x0000E703
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x0001050C File Offset: 0x0000E70C
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x00010514 File Offset: 0x0000E714
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0001051D File Offset: 0x0000E71D
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x00010525 File Offset: 0x0000E725
		[DataMember]
		public string ScreenTitle { get; set; }

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x0001052E File Offset: 0x0000E72E
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x00010536 File Offset: 0x0000E736
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x0001053F File Offset: 0x0000E73F
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x00010547 File Offset: 0x0000E747
		[DataMember]
		public eFormApprovalState CurrentState { get; set; }

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00010550 File Offset: 0x0000E750
		// (set) Token: 0x060023BF RID: 9151 RVA: 0x00010558 File Offset: 0x0000E758
		[DataMember]
		public DateTime? LastModifiedDate { get; set; }

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x00010561 File Offset: 0x0000E761
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x00010569 File Offset: 0x0000E769
		[DataMember]
		public bool IsCurrentUserSupervisor { get; set; }

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x00010572 File Offset: 0x0000E772
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x0001057A File Offset: 0x0000E77A
		[DataMember]
		public bool AppointmentIsPrivate { get; set; }

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x00010583 File Offset: 0x0000E783
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x0001058B File Offset: 0x0000E78B
		[DataMember]
		public bool AppointmentIsLocked { get; set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x00010594 File Offset: 0x0000E794
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x0001059C File Offset: 0x0000E79C
		[DataMember]
		public int AppointmentBookedByPersonId { get; set; }
	}
}
