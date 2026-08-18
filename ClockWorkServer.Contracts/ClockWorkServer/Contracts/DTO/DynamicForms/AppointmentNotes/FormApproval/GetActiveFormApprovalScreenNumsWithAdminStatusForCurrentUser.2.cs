using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D4 RID: 1748
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveFormApprovalScreenNumsWithAdminStatusForCurrentUserResp
	{
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x000104B7 File Offset: 0x0000E6B7
		// (set) Token: 0x060023AC RID: 9132 RVA: 0x000104BF File Offset: 0x0000E6BF
		[DataMember]
		public IDictionary<int, bool> FormApprovalScreenNumsWithAdminStatus { get; set; }
	}
}
