using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200022D RID: 557
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadStudentFileResp
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00005BE3 File Offset: 0x00003DE3
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00005BEB File Offset: 0x00003DEB
		[DataMember]
		public int FileId { get; set; }
	}
}
