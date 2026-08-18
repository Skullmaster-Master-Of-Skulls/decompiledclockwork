using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms.Legacy;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy
{
	// Token: 0x020006B8 RID: 1720
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyDynamicDataRowDatasDTO
	{
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0000FFBB File Offset: 0x0000E1BB
		[DataMember]
		public IList<LegacyDynamicDataRowDataDTO> RowDatas { get; set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		[DataMember]
		public eLegacyDynamicDataType ControlValueType { get; set; }
	}
}
