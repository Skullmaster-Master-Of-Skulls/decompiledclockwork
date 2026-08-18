using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B5 RID: 181
	[CollectionDataContract(Namespace = "http://tpro.ca")]
	public class MessageParameters : Dictionary<string, string>
	{
	}
}
