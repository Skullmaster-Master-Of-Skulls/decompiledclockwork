using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F6 RID: 246
	[CollectionDataContract(Namespace = "http://tpro.ca")]
	public class ClientParametersDTO : Dictionary<string, string>
	{
	}
}
