using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000866 RID: 2150
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerJobStepByIdResp
	{
		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x00014BAC File Offset: 0x00012DAC
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x00014BB4 File Offset: 0x00012DB4
		[DataMember]
		public ClockWorkServerJobStepDTO ClockWorkServerJobStep { get; set; }
	}
}
