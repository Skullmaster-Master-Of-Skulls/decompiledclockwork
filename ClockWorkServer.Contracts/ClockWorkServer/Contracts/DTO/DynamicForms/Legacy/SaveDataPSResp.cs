using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy
{
	// Token: 0x020006BA RID: 1722
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveDataPSResp
	{
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x0001003B File Offset: 0x0000E23B
		// (set) Token: 0x0600230C RID: 8972 RVA: 0x00010043 File Offset: 0x0000E243
		[DataMember]
		public IList<LegacySaveDataResultDTO> SaveDataResults { get; set; }
	}
}
