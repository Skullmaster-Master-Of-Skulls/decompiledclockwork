using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x02000837 RID: 2103
	[DataContract(Namespace = "http://tpro.ca")]
	public class ConfidentialityAgreementBaseMessageReq : BaseMessageReq
	{
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x0001466C File Offset: 0x0001286C
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x00014674 File Offset: 0x00012874
		[DataMember]
		public eClockWorkModules Module { get; set; }
	}
}
