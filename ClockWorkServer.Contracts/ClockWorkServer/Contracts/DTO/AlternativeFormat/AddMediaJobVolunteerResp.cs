using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF0 RID: 3056
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaJobVolunteerResp
	{
		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x0001FA65 File Offset: 0x0001DC65
		// (set) Token: 0x06004079 RID: 16505 RVA: 0x0001FA6D File Offset: 0x0001DC6D
		[DataMember]
		public int VolunteerId { get; set; }
	}
}
