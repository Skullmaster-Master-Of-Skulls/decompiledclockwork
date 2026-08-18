using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x0200089C RID: 2204
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCasesForDisplayForStudentReq : BaseMessageReq
	{
		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000152CB File Offset: 0x000134CB
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x000152D3 File Offset: 0x000134D3
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000152DC File Offset: 0x000134DC
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000152E4 File Offset: 0x000134E4
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000152ED File Offset: 0x000134ED
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000152F5 File Offset: 0x000134F5
		[DataMember]
		public IList<int> ControlIdsForDynamicFormSummaryItems { get; set; }
	}
}
