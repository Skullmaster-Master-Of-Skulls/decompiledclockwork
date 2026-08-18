using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B0 RID: 1712
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindScreensAControlExistsOnReq : BaseMessageReq
	{
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x0000FCF9 File Offset: 0x0000DEF9
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x0000FD01 File Offset: 0x0000DF01
		[DataMember]
		public int ControlId { get; set; }
	}
}
