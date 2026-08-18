using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Common
{
	// Token: 0x02000841 RID: 2113
	[DataContract(Namespace = "http://tpro.ca")]
	public class FileDTO
	{
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000146FD File Offset: 0x000128FD
		// (set) Token: 0x06002B0E RID: 11022 RVA: 0x00014705 File Offset: 0x00012905
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x0001470E File Offset: 0x0001290E
		// (set) Token: 0x06002B10 RID: 11024 RVA: 0x00014716 File Offset: 0x00012916
		[DataMember]
		public byte[] FileBytes { get; set; }
	}
}
