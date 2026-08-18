using System;
using System.Runtime.Serialization;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D7 RID: 215
	[DataContract(Namespace = "http://tpro.ca")]
	public class InvalidSessionIdentifierFault : GenericFault
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00002650 File Offset: 0x00000850
		public InvalidSessionIdentifierFault() : this("Invalid session id.")
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000265F File Offset: 0x0000085F
		public InvalidSessionIdentifierFault(string message) : base(message)
		{
		}
	}
}
