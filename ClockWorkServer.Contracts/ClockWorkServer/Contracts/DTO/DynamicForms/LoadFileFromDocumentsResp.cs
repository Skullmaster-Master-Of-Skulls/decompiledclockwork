using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000651 RID: 1617
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFileFromDocumentsResp
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x0000EF71 File Offset: 0x0000D171
		// (set) Token: 0x060020EF RID: 8431 RVA: 0x0000EF79 File Offset: 0x0000D179
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
