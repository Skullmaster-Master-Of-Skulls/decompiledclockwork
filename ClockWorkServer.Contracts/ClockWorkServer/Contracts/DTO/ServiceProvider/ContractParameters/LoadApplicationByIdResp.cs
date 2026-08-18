using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200027E RID: 638
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationByIdResp
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00007359 File Offset: 0x00005559
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00007361 File Offset: 0x00005561
		[DataMember]
		public SPApplicationDTO Application { get; set; }
	}
}
