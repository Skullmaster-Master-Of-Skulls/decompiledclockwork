using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Login;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x0200045B RID: 1115
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLoginMethodResp
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0000B015 File Offset: 0x00009215
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x0000B01D File Offset: 0x0000921D
		[DataMember]
		public eLoginMethodDTO Method { get; set; }
	}
}
