using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000412 RID: 1042
	[DataContract(Namespace = "http://tpro.ca")]
	public class OnlineFormStatusDTO
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0000A88B File Offset: 0x00008A8B
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x0000A893 File Offset: 0x00008A93
		[DataMember]
		public int PeopleOnlineFormStatusId { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0000A89C File Offset: 0x00008A9C
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0000A8AD File Offset: 0x00008AAD
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0000A8B5 File Offset: 0x00008AB5
		[DataMember]
		public eOnlineFormStatusType StatusType { get; set; }
	}
}
