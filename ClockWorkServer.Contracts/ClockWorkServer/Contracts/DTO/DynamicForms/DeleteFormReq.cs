using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006AE RID: 1710
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteFormReq : BaseMessageReq
	{
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0000FCD7 File Offset: 0x0000DED7
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x0000FCDF File Offset: 0x0000DEDF
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
