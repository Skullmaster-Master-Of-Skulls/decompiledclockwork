using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000DC RID: 220
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogonSSOReq : BaseHashAuthMessageReq
	{
	}
}
