using System;
using System.Runtime.Serialization;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D3 RID: 211
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccountLockedOutFault : GenericFault
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x000025F6 File Offset: 0x000007F6
		public AccountLockedOutFault() : this("Account is locked out.")
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00002605 File Offset: 0x00000805
		public AccountLockedOutFault(string message) : base(message)
		{
			base.Message = message;
		}
	}
}
