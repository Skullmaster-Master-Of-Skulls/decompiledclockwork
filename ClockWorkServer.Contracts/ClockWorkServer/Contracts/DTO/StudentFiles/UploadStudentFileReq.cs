using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200022C RID: 556
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadStudentFileReq : BaseMessageReq
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00005BC1 File Offset: 0x00003DC1
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x00005BC9 File Offset: 0x00003DC9
		[DataMember]
		public string StudentComment { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00005BD2 File Offset: 0x00003DD2
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x00005BDA File Offset: 0x00003DDA
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
