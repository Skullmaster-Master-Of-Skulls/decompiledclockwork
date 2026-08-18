using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Files
{
	// Token: 0x020005F0 RID: 1520
	[DataContract(Namespace = "http://tpro.ca")]
	public class BinaryFileDTO
	{
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0000E21F File Offset: 0x0000C41F
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x0000E227 File Offset: 0x0000C427
		[DataMember]
		public string FileName { get; set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0000E230 File Offset: 0x0000C430
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x0000E238 File Offset: 0x0000C438
		[DataMember]
		public int FileSize { get; set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0000E241 File Offset: 0x0000C441
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x0000E249 File Offset: 0x0000C449
		[DataMember]
		public byte[] ByteArray { get; set; }
	}
}
