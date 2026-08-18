using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200063D RID: 1597
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferencePerAppointmentDataResp
	{
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x0000ED04 File Offset: 0x0000CF04
		[DataMember]
		public DataTable Table { get; set; }
	}
}
