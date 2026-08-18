using System;
using System.Runtime.Serialization;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D6 RID: 214
	[DataContract(Namespace = "http://tpro.ca")]
	public class InvalidOperationFault : ExceptionFault<InvalidOperationException>
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x00002636 File Offset: 0x00000836
		public InvalidOperationFault() : this("Invalid operation")
		{
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00002645 File Offset: 0x00000845
		public InvalidOperationFault(string message) : base(message)
		{
		}
	}
}
