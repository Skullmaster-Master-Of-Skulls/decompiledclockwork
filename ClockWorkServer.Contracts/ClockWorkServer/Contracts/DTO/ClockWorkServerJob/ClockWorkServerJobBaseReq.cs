using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000842 RID: 2114
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobBaseReq : BaseMessageReq
	{
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x0001471F File Offset: 0x0001291F
		// (set) Token: 0x06002B13 RID: 11027 RVA: 0x00014727 File Offset: 0x00012927
		[DataMember]
		public eClockWorkServerInstanceName ClockWorkServerInstanceName { get; set; }
	}
}
