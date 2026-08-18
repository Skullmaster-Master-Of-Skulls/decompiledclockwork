using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D8 RID: 216
	[DataContract(Namespace = "http://tpro.ca")]
	public class PermissionDeniedFault : ExceptionFault<PermissionDeniedException>
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0000266A File Offset: 0x0000086A
		public PermissionDeniedFault() : this("Permission denied")
		{
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00002679 File Offset: 0x00000879
		public PermissionDeniedFault(string message) : base(message)
		{
		}
	}
}
