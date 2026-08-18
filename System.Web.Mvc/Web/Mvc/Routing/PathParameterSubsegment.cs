using System;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000051 RID: 81
	internal sealed class PathParameterSubsegment : PathSubsegment
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00007B14 File Offset: 0x00005D14
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007B46 File Offset: 0x00005D46
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00007B4E File Offset: 0x00005D4E
		public bool IsCatchAll { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007B57 File Offset: 0x00005D57
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00007B5F File Offset: 0x00005D5F
		public string ParameterName { get; private set; }
	}
}
