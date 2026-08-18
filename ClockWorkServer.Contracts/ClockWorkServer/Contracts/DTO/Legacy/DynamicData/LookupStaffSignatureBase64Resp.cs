using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004E4 RID: 1252
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupStaffSignatureBase64Resp
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0000C3F1 File Offset: 0x0000A5F1
		[DataMember]
		public string StaffSigBase64 { get; set; }
	}
}
