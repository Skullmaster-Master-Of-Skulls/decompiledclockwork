using System;
using System.Runtime.Serialization;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults
{
	// Token: 0x020000D5 RID: 213
	[DataContract(Namespace = "http://tpro.ca")]
	public class InvalidHashCredentialsFault : GenericFault
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x00002627 File Offset: 0x00000827
		public InvalidHashCredentialsFault() : this("Invalid hash credentials.")
		{
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00002605 File Offset: 0x00000805
		public InvalidHashCredentialsFault(string message) : base(message)
		{
			base.Message = message;
		}
	}
}
