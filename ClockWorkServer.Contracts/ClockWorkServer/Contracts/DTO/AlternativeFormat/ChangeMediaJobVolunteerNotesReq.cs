using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C01 RID: 3073
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeMediaJobVolunteerNotesReq : BaseMessageReq
	{
		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x0001FB75 File Offset: 0x0001DD75
		// (set) Token: 0x060040AA RID: 16554 RVA: 0x0001FB7D File Offset: 0x0001DD7D
		[DataMember]
		public int VolunteerId { get; set; }

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060040AB RID: 16555 RVA: 0x0001FB86 File Offset: 0x0001DD86
		// (set) Token: 0x060040AC RID: 16556 RVA: 0x0001FB8E File Offset: 0x0001DD8E
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x0001FB97 File Offset: 0x0001DD97
		// (set) Token: 0x060040AE RID: 16558 RVA: 0x0001FB9F File Offset: 0x0001DD9F
		[DataMember]
		public string Notes { get; set; }
	}
}
