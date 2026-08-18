using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C41 RID: 3137
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllStudentMediaRequestByStudentReq : BaseReportMessageReq
	{
		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x0002012B File Offset: 0x0001E32B
		// (set) Token: 0x06004196 RID: 16790 RVA: 0x00020133 File Offset: 0x0001E333
		[DataMember]
		public int StudentId { get; set; }
	}
}
