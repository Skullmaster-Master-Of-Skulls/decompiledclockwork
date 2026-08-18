using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200031D RID: 797
	[DataContract(Namespace = "http://tpro.ca")]
	public class CompileCSharpScript2Resp
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00008786 File Offset: 0x00006986
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0000878E File Offset: 0x0000698E
		[DataMember]
		public bool CompileSucceeded { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00008797 File Offset: 0x00006997
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x0000879F File Offset: 0x0000699F
		[DataMember]
		public IList<ReportCompileLineWarningOrErrorDTO> WarningsOrErrors { get; set; }
	}
}
