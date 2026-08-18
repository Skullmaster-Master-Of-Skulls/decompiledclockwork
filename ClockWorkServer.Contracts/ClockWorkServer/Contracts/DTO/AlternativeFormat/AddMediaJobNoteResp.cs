using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA5 RID: 2981
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaJobNoteResp
	{
		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x0001F1B2 File Offset: 0x0001D3B2
		// (set) Token: 0x06003F28 RID: 16168 RVA: 0x0001F1BA File Offset: 0x0001D3BA
		[DataMember]
		public int MediaJobNoteId { get; set; }
	}
}
