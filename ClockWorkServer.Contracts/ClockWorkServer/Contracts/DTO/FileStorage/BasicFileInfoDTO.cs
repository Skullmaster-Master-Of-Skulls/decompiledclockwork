using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x02000600 RID: 1536
	[DataContract(Namespace = "http://tpro.ca")]
	public class BasicFileInfoDTO : BaseMessageReq
	{
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0000E48B File Offset: 0x0000C68B
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x0000E493 File Offset: 0x0000C693
		[DataMember]
		public FileIdentifierDTO FileIdentifier { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0000E49C File Offset: 0x0000C69C
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		[DataMember]
		public string FileName { get; set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0000E4AD File Offset: 0x0000C6AD
		// (set) Token: 0x06001F6C RID: 8044 RVA: 0x0000E4B5 File Offset: 0x0000C6B5
		[DataMember]
		public long Length { get; set; }
	}
}
