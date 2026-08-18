using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B89 RID: 2953
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaContentDetailResp
	{
		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x0001E895 File Offset: 0x0001CA95
		// (set) Token: 0x06003E52 RID: 15954 RVA: 0x0001E89D File Offset: 0x0001CA9D
		[DataMember]
		public int MediaContentDetailID { get; set; }
	}
}
