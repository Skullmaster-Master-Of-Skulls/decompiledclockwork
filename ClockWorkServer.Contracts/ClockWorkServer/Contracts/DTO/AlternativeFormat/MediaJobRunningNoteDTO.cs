using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE0 RID: 3040
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaJobRunningNoteDTO
	{
		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0001F812 File Offset: 0x0001DA12
		// (set) Token: 0x06004023 RID: 16419 RVA: 0x0001F81A File Offset: 0x0001DA1A
		[DataMember]
		public int NoteId { get; set; }

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x0001F823 File Offset: 0x0001DA23
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x0001F82B File Offset: 0x0001DA2B
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x0001F834 File Offset: 0x0001DA34
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x0001F83C File Offset: 0x0001DA3C
		[DataMember]
		public DateTime LastModifiedDatetime { get; set; }

		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x0001F845 File Offset: 0x0001DA45
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x0001F84D File Offset: 0x0001DA4D
		[DataMember]
		public PersonBaseDTO WhoModified { get; set; }
	}
}
