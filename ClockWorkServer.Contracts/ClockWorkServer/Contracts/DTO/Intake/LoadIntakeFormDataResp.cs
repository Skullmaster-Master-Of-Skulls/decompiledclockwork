using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005E5 RID: 1509
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadIntakeFormDataResp
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0000DF90 File Offset: 0x0000C190
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0000DF98 File Offset: 0x0000C198
		[DataMember]
		public IList<DynamicDataDTO> DynamicData { get; set; }
	}
}
