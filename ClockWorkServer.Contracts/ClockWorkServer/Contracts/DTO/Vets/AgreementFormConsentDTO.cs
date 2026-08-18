using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x020000FE RID: 254
	[DataContract(Namespace = "http://tpro.ca")]
	public class AgreementFormConsentDTO
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00002D13 File Offset: 0x00000F13
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00002D1B File Offset: 0x00000F1B
		[DataMember]
		public DateTime DateConsentedTo { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00002D24 File Offset: 0x00000F24
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00002D2C File Offset: 0x00000F2C
		[DataMember]
		public int StudentWhoConsentedPersonId { get; set; }
	}
}
