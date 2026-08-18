using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000195 RID: 405
	[DataContract(Namespace = "http://tpro.ca")]
	public class TutorDTO : TutorBaseDTO
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00004457 File Offset: 0x00002657
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0000445F File Offset: 0x0000265F
		[DataMember]
		public string Specializations { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00004468 File Offset: 0x00002668
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00004470 File Offset: 0x00002670
		[DataMember]
		public string PublicNoteFromTutor { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00004479 File Offset: 0x00002679
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00004481 File Offset: 0x00002681
		[DataMember]
		public string Email { get; set; }
	}
}
