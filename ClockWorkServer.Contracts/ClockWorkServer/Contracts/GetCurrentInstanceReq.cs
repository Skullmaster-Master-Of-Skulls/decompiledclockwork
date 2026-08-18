using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C2 RID: 194
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCurrentInstanceReq : SettingsBaseMessageReq
	{
	}
}
