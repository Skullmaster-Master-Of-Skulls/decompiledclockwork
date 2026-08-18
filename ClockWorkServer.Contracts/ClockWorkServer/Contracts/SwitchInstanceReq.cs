using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C6 RID: 198
	[DataContract(Namespace = "http://tpro.ca")]
	public class SwitchInstanceReq : SettingsBaseMessageReq
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000025E5 File Offset: 0x000007E5
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000025ED File Offset: 0x000007ED
		[DataMember]
		public new string InstanceName { get; set; }
	}
}
