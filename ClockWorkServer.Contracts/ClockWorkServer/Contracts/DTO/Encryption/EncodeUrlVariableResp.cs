using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000611 RID: 1553
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncodeUrlVariableResp
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		[DataMember]
		public string EncodedUrlVariable { get; set; }
	}
}
