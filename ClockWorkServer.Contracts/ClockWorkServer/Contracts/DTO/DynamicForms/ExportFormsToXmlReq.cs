using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A4 RID: 1700
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportFormsToXmlReq : BaseMessageReq
	{
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x0000FC3E File Offset: 0x0000DE3E
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x0000FC46 File Offset: 0x0000DE46
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}
