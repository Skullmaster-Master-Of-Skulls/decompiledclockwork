using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200068A RID: 1674
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFieldPossibleValuesReq : BaseMessageReq
	{
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x0000F800 File Offset: 0x0000DA00
		// (set) Token: 0x060021FF RID: 8703 RVA: 0x0000F808 File Offset: 0x0000DA08
		[DataMember]
		public int ControlId { get; set; }
	}
}
