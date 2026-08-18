using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006C7 RID: 1735
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalForAppointmentDTO
	{
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x00010341 File Offset: 0x0000E541
		// (set) Token: 0x06002373 RID: 9075 RVA: 0x00010349 File Offset: 0x0000E549
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00010352 File Offset: 0x0000E552
		// (set) Token: 0x06002375 RID: 9077 RVA: 0x0001035A File Offset: 0x0000E55A
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00010363 File Offset: 0x0000E563
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x0001036B File Offset: 0x0000E56B
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x00010374 File Offset: 0x0000E574
		// (set) Token: 0x06002379 RID: 9081 RVA: 0x0001037C File Offset: 0x0000E57C
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x00010385 File Offset: 0x0000E585
		// (set) Token: 0x0600237B RID: 9083 RVA: 0x0001038D File Offset: 0x0000E58D
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x00010396 File Offset: 0x0000E596
		// (set) Token: 0x0600237D RID: 9085 RVA: 0x0001039E File Offset: 0x0000E59E
		[DataMember]
		public IList<FormApprovalCommentDTO> Comments { get; set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000103A7 File Offset: 0x0000E5A7
		// (set) Token: 0x0600237F RID: 9087 RVA: 0x000103AF File Offset: 0x0000E5AF
		[DataMember]
		public eFormApprovalState CurrentState { get; set; }

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000103B8 File Offset: 0x0000E5B8
		// (set) Token: 0x06002381 RID: 9089 RVA: 0x000103C0 File Offset: 0x0000E5C0
		[DataMember]
		public FormApprovalApprovedInfoDTO ApprovalInfo { get; set; }
	}
}
