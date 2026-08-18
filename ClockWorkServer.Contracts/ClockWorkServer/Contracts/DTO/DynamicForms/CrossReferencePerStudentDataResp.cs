using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200063B RID: 1595
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferencePerStudentDataResp
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
		// (set) Token: 0x06002089 RID: 8329 RVA: 0x0000ECD1 File Offset: 0x0000CED1
		[DataMember]
		public DataTable Table { get; set; }
	}
}
