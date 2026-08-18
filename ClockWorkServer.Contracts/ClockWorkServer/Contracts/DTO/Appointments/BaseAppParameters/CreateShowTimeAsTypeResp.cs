using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000978 RID: 2424
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateShowTimeAsTypeResp
	{
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x00018116 File Offset: 0x00016316
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x0001811E File Offset: 0x0001631E
		[DataMember]
		public int AppCode { get; set; }
	}
}
