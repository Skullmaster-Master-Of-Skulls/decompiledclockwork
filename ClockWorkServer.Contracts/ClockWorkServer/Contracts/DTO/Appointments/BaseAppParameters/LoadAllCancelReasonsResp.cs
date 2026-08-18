using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000942 RID: 2370
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCancelReasonsResp
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x00017BC6 File Offset: 0x00015DC6
		// (set) Token: 0x06003098 RID: 12440 RVA: 0x00017BCE File Offset: 0x00015DCE
		[DataMember]
		public IList<AppCancelReasonDTO> AppCancelReasons { get; set; }
	}
}
