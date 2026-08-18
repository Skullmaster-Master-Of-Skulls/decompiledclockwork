using System;
using System.Runtime.Serialization;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D4 RID: 212
	[DataContract(Namespace = "http://tpro.ca")]
	public class InvalidCredentialsFault : GenericFault
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x00002618 File Offset: 0x00000818
		public InvalidCredentialsFault() : this("Invalid username or password.")
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00002605 File Offset: 0x00000805
		public InvalidCredentialsFault(string message) : base(message)
		{
			base.Message = message;
		}
	}
}
