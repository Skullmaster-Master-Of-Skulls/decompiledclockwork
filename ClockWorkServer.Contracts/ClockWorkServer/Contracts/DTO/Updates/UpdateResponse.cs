using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000168 RID: 360
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateResponse
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00003F67 File Offset: 0x00002167
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00003F6F File Offset: 0x0000216F
		[DataMember]
		public FileSystemStructure File { get; set; }
	}
}
