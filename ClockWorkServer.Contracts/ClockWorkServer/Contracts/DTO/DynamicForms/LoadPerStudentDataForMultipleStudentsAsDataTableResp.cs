using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000665 RID: 1637
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPerStudentDataForMultipleStudentsAsDataTableResp
	{
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x0000F1CC File Offset: 0x0000D3CC
		[DataMember]
		public DataTable Table { get; set; }
	}
}
