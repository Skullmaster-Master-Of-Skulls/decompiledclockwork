using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000639 RID: 1593
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferenceDataIntoSingleTableResp
	{
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x0000EC96 File Offset: 0x0000CE96
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x0000EC9E File Offset: 0x0000CE9E
		[DataMember]
		public DataTable Table { get; set; }
	}
}
