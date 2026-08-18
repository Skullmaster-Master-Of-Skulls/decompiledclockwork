using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000644 RID: 1604
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadEmailReq : BaseMessageReq
	{
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		// (set) Token: 0x060020AE RID: 8366 RVA: 0x0000EDBF File Offset: 0x0000CFBF
		[DataMember]
		public int PersonId { get; set; }
	}
}
