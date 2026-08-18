using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A51 RID: 2641
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadExamFilesByExamCheckProfAltContactPermissionsReq : BaseMessageReq
	{
		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x0001AF96 File Offset: 0x00019196
		// (set) Token: 0x06003777 RID: 14199 RVA: 0x0001AF9E File Offset: 0x0001919E
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x0001AFA7 File Offset: 0x000191A7
		// (set) Token: 0x06003779 RID: 14201 RVA: 0x0001AFAF File Offset: 0x000191AF
		[DataMember]
		public int AltContactId { get; set; }

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x0001AFB8 File Offset: 0x000191B8
		// (set) Token: 0x0600377B RID: 14203 RVA: 0x0001AFC0 File Offset: 0x000191C0
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x0600377C RID: 14204 RVA: 0x0001AFC9 File Offset: 0x000191C9
		// (set) Token: 0x0600377D RID: 14205 RVA: 0x0001AFD1 File Offset: 0x000191D1
		[DataMember]
		public bool IncludeDeletedFiles { get; set; }

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x0001AFDA File Offset: 0x000191DA
		// (set) Token: 0x0600377F RID: 14207 RVA: 0x0001AFE2 File Offset: 0x000191E2
		[DataMember]
		public bool LoadFileData { get; set; }
	}
}
