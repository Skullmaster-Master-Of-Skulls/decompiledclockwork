using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x02000362 RID: 866
	[DataContract(Namespace = "http://tpro.ca")]
	public class BasicPersonDTO
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x000094A7 File Offset: 0x000076A7
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x000094AF File Offset: 0x000076AF
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000094B8 File Offset: 0x000076B8
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x000094C0 File Offset: 0x000076C0
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x000094C9 File Offset: 0x000076C9
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x000094D1 File Offset: 0x000076D1
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x000094DA File Offset: 0x000076DA
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x000094E2 File Offset: 0x000076E2
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x000094EB File Offset: 0x000076EB
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x000094F3 File Offset: 0x000076F3
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
