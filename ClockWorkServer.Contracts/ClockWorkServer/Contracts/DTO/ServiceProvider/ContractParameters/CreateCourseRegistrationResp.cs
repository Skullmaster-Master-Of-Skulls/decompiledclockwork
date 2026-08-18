using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000298 RID: 664
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCourseRegistrationResp
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00007579 File Offset: 0x00005779
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00007581 File Offset: 0x00005781
		[DataMember]
		public int SPProviderCourseRegistrationID { get; set; }
	}
}
