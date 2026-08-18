using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Files.FileUpload;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload
{
	// Token: 0x020005F4 RID: 1524
	[DataContract(Namespace = "http://tpro.ca")]
	public class TempFileContextDTO
	{
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		// (set) Token: 0x06001F33 RID: 7987 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		[DataMember]
		public eTempFileUsage Usage { get; set; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0000E305 File Offset: 0x0000C505
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x0000E30D File Offset: 0x0000C50D
		[DataMember]
		public string GroupId { get; set; }
	}
}
