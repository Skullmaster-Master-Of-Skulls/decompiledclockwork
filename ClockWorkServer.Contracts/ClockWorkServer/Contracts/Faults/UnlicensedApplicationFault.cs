using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D9 RID: 217
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnlicensedApplicationFault : ExceptionFault<UnlicensedApplicationException>
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00002684 File Offset: 0x00000884
		public UnlicensedApplicationFault() : this("Unlicensed Application.")
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00002693 File Offset: 0x00000893
		public UnlicensedApplicationFault(string message) : base(message)
		{
		}
	}
}
