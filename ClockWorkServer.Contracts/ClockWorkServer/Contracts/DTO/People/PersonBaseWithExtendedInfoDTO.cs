using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A5 RID: 933
	[DataContract(Namespace = "http://tpro.ca")]
	public class PersonBaseWithExtendedInfoDTO : PersonBaseDTO
	{
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00009CFD File Offset: 0x00007EFD
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00009D05 File Offset: 0x00007F05
		[DataMember]
		public DateTime DateAdded { get; set; }
	}
}
