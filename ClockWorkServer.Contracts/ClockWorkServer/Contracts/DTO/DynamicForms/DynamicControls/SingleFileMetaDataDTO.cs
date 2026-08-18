using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicControls
{
	// Token: 0x020006C1 RID: 1729
	[DataContract(Namespace = "http://tpro.ca")]
	public class SingleFileMetaDataDTO
	{
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x00010154 File Offset: 0x0000E354
		// (set) Token: 0x06002333 RID: 9011 RVA: 0x0001015C File Offset: 0x0000E35C
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x00010165 File Offset: 0x0000E365
		// (set) Token: 0x06002335 RID: 9013 RVA: 0x0001016D File Offset: 0x0000E36D
		[DataMember]
		public string FileName { get; set; }
	}
}
