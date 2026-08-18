using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000C4 RID: 196
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetInstanceNameReq : SettingsBaseMessageReq
	{
	}
}
