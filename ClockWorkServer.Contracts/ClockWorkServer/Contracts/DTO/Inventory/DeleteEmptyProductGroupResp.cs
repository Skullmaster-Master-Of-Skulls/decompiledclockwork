using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200052A RID: 1322
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteEmptyProductGroupResp
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x0000CB48 File Offset: 0x0000AD48
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x0000CB50 File Offset: 0x0000AD50
		[DataMember]
		public bool WasDeleted { get; set; }
	}
}
