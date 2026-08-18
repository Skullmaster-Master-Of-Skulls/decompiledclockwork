using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A3 RID: 931
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDeletedAccountsReq : BaseMessageReq
	{
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00009CDB File Offset: 0x00007EDB
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x00009CE3 File Offset: 0x00007EE3
		[DataMember]
		public int[] GroupIds { get; set; }
	}
}
