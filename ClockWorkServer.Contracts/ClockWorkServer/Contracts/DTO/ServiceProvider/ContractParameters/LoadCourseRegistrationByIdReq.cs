using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000291 RID: 657
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationByIdReq : BaseMessageReq
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00007524 File Offset: 0x00005724
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0000752C File Offset: 0x0000572C
		[DataMember]
		public int SPProviderCourseRegistrationId { get; set; }
	}
}
