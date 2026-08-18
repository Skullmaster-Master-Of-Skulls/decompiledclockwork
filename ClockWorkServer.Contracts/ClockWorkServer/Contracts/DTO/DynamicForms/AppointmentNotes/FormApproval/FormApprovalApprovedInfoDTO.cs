using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006C4 RID: 1732
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalApprovedInfoDTO
	{
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x000102CA File Offset: 0x0000E4CA
		// (set) Token: 0x06002362 RID: 9058 RVA: 0x000102D2 File Offset: 0x0000E4D2
		[DataMember]
		public PersonBaseDTO WhoApproved { get; set; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000102DB File Offset: 0x0000E4DB
		// (set) Token: 0x06002364 RID: 9060 RVA: 0x000102E3 File Offset: 0x0000E4E3
		[DataMember]
		public DateTime DateApproved { get; set; }
	}
}
