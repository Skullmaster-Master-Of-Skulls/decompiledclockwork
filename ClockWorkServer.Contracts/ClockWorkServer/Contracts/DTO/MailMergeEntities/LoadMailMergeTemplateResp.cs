using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A5 RID: 1189
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMailMergeTemplateResp
	{
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0000BBBD File Offset: 0x00009DBD
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
