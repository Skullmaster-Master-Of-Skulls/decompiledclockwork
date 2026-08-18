using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001E1 RID: 481
	[DataContract(Namespace = "http://tpro.ca")]
	public class CopyTempFilesToInstructorExamUploadAndDeleteTempFileResp
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00004FCD File Offset: 0x000031CD
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00004FD5 File Offset: 0x000031D5
		[DataMember]
		public int[] NewExamFileIds { get; set; }
	}
}
