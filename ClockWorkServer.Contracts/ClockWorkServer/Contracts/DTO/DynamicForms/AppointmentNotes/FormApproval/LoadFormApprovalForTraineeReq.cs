using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E4 RID: 1764
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormApprovalForTraineeReq : BaseMessageReq
	{
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x0001073D File Offset: 0x0000E93D
		// (set) Token: 0x06002408 RID: 9224 RVA: 0x00010745 File Offset: 0x0000E945
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x0001074E File Offset: 0x0000E94E
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x00010756 File Offset: 0x0000E956
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x0001075F File Offset: 0x0000E95F
		// (set) Token: 0x0600240C RID: 9228 RVA: 0x00010767 File Offset: 0x0000E967
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
