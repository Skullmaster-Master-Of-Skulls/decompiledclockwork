using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200039A RID: 922
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonsByUsernameReq : BaseMessageReq
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00009BFA File Offset: 0x00007DFA
		// (set) Token: 0x060014BC RID: 5308 RVA: 0x00009C02 File Offset: 0x00007E02
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00009C0B File Offset: 0x00007E0B
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x00009C13 File Offset: 0x00007E13
		[DataMember]
		public bool IncludeDeletedAccounts { get; set; }
	}
}
