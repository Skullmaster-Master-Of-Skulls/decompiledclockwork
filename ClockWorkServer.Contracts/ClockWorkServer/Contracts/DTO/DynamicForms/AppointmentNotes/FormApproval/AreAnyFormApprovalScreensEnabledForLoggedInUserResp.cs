using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D2 RID: 1746
	[DataContract(Namespace = "http://tpro.ca")]
	public class AreAnyFormApprovalScreensEnabledForLoggedInUserResp
	{
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x000104A6 File Offset: 0x0000E6A6
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x000104AE File Offset: 0x0000E6AE
		[DataMember]
		public bool AtLeastOneScreenIsEnabledForThisUser { get; set; }
	}
}
