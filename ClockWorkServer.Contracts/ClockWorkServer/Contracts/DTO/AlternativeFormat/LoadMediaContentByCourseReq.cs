using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B70 RID: 2928
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByCourseReq : BaseMessageReq
	{
		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x0001E6FD File Offset: 0x0001C8FD
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x0001E705 File Offset: 0x0001C905
		[DataMember]
		public int CourseID { get; set; }
	}
}
