using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement
{
	// Token: 0x0200083D RID: 2109
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsConfidentialityAgreementSigningRequiredResp
	{
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000146CA File Offset: 0x000128CA
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x000146D2 File Offset: 0x000128D2
		[DataMember]
		public bool IsSigningRequired { get; set; }
	}
}
