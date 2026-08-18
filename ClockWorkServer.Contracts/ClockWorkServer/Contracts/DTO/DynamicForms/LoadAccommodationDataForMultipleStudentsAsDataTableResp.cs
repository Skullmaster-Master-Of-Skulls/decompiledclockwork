using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000667 RID: 1639
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationDataForMultipleStudentsAsDataTableResp
	{
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x0000F1F7 File Offset: 0x0000D3F7
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x0000F1FF File Offset: 0x0000D3FF
		[DataMember]
		public DataTable Table { get; set; }
	}
}
