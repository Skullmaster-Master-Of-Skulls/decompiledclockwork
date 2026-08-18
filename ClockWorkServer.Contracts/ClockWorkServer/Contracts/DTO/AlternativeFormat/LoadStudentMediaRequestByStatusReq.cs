using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C36 RID: 3126
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentMediaRequestByStatusReq : BaseReportMessageReq
	{
		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x0002004E File Offset: 0x0001E24E
		// (set) Token: 0x06004171 RID: 16753 RVA: 0x00020056 File Offset: 0x0001E256
		[DataMember]
		public MediaRequestStatus Status { get; set; }
	}
}
