using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003A4 RID: 932
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDeletedAccountsResp
	{
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00009CEC File Offset: 0x00007EEC
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x00009CF4 File Offset: 0x00007EF4
		[DataMember]
		public IList<PersonBaseDTO> UserAccounts { get; set; }
	}
}
