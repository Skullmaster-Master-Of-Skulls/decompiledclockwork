using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001DD RID: 477
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadTempFileResp
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00004F45 File Offset: 0x00003145
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x00004F4D File Offset: 0x0000314D
		[DataMember]
		public BinaryFileDTO TempFile { get; set; }
	}
}
