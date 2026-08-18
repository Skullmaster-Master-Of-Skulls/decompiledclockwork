using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A5 RID: 1701
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExportFormsToXmlResp
	{
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0000FC4F File Offset: 0x0000DE4F
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0000FC57 File Offset: 0x0000DE57
		[DataMember]
		public IList<BinaryFileDTO> XmlFiles { get; set; }
	}
}
