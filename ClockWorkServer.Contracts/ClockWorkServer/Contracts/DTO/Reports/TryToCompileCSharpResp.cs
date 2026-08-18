using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200031F RID: 799
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToCompileCSharpResp
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000087CA File Offset: 0x000069CA
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x000087D2 File Offset: 0x000069D2
		[DataMember]
		public bool CompileSucceeded { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000087DB File Offset: 0x000069DB
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x000087E3 File Offset: 0x000069E3
		[DataMember]
		public IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors { get; set; }
	}
}
