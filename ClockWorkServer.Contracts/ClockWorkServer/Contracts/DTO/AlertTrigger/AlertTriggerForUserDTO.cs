using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger
{
	// Token: 0x02000C75 RID: 3189
	[DataContract(Namespace = "http://tpro.ca")]
	public class AlertTriggerForUserDTO
	{
		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x00020788 File Offset: 0x0001E988
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x00020790 File Offset: 0x0001E990
		[DataMember]
		public string MessageToUser { get; set; }

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x00020799 File Offset: 0x0001E999
		// (set) Token: 0x0600427A RID: 17018 RVA: 0x000207A1 File Offset: 0x0001E9A1
		[DataMember]
		public IDictionary<string, string> Args { get; set; }

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x000207AA File Offset: 0x0001E9AA
		// (set) Token: 0x0600427C RID: 17020 RVA: 0x000207B2 File Offset: 0x0001E9B2
		[DataMember]
		public bool DontAllowAppointmentBooking { get; set; }
	}
}
