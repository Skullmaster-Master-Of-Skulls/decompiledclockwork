using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000223 RID: 547
	public interface IReportNode
	{
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060010A8 RID: 4264
		ReportNodeType NodeType { get; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060010A9 RID: 4265
		string Title { get; }
	}
}
