using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000110 RID: 272
	internal sealed class PathParameterSubsegment : PathSubsegment
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x00015DFC File Offset: 0x00013FFC
		public PathParameterSubsegment(string parameterName)
		{
			if (parameterName.StartsWith("*", StringComparison.Ordinal))
			{
				this.ParameterName = parameterName.Substring(1);
				this.IsCatchAll = true;
				return;
			}
			this.ParameterName = parameterName;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00015E2E File Offset: 0x0001402E
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x00015E36 File Offset: 0x00014036
		public bool IsCatchAll { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00015E3F File Offset: 0x0001403F
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00015E47 File Offset: 0x00014047
		public string ParameterName { get; private set; }
	}
}
