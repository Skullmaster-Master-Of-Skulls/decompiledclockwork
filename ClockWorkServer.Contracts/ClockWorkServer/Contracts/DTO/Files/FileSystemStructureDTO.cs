using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Files
{
	// Token: 0x020005F3 RID: 1523
	[DataContract(Namespace = "http://tpro.ca")]
	public class FileSystemStructureDTO : FileStructureDTO
	{
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0000E2C9 File Offset: 0x0000C4C9
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0000E2D1 File Offset: 0x0000C4D1
		[DataMember]
		public virtual string Filename { get; set; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0000E2DA File Offset: 0x0000C4DA
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0000E2E2 File Offset: 0x0000C4E2
		[DataMember]
		public virtual string Extension { get; set; }
	}
}
