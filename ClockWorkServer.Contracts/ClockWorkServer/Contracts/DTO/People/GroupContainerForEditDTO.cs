using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200036C RID: 876
	[DataContract(Namespace = "http://tpro.ca")]
	public class GroupContainerForEditDTO
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0000972A File Offset: 0x0000792A
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x00009732 File Offset: 0x00007932
		[DataMember]
		public string FullDescription { get; set; }
	}
}
