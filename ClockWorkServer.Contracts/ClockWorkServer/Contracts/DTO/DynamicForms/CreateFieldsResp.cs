using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200068F RID: 1679
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFieldsResp
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x0000F877 File Offset: 0x0000DA77
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x0000F87F File Offset: 0x0000DA7F
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
