using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200042C RID: 1068
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerBaseByUsernameResp
	{
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x0000ABAA File Offset: 0x00008DAA
		[DataMember]
		public NotetakerBaseDTO NotetakerBase { get; set; }
	}
}
