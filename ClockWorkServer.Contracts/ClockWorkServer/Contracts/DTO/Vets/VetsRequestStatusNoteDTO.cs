using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200011C RID: 284
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsRequestStatusNoteDTO
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x000031E4 File Offset: 0x000013E4
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x000031EC File Offset: 0x000013EC
		[DataMember]
		public int BenefitApplicationStatusDetailNotesId { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x000031F5 File Offset: 0x000013F5
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x000031FD File Offset: 0x000013FD
		[DataMember]
		public bool ForStudent { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00003206 File Offset: 0x00001406
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0000320E File Offset: 0x0000140E
		[DataMember]
		public string Note { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00003217 File Offset: 0x00001417
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0000321F File Offset: 0x0000141F
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00003228 File Offset: 0x00001428
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00003230 File Offset: 0x00001430
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }
	}
}
