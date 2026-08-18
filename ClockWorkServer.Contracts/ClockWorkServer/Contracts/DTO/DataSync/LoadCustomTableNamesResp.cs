using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000724 RID: 1828
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCustomTableNamesResp
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000112BA File Offset: 0x0000F4BA
		// (set) Token: 0x0600259D RID: 9629 RVA: 0x000112C2 File Offset: 0x0000F4C2
		[DataMember]
		public string[] TableNames { get; set; }
	}
}
