using System;
using System.Runtime.Serialization;
using TechnoPro.Common.WCF;

namespace TechnoPro.ClockWorkServer.Contracts.Faults.Reports
{
	// Token: 0x020000DA RID: 218
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportGenericFault : GenericFault
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x0000269E File Offset: 0x0000089E
		public ReportGenericFault()
		{
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00002605 File Offset: 0x00000805
		public ReportGenericFault(string message) : base(message)
		{
			base.Message = message;
		}
	}
}
