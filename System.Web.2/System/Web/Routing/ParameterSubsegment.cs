using System;

namespace System.Web.Routing
{
	// Token: 0x02000143 RID: 323
	internal sealed class ParameterSubsegment : PathSubsegment
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x000369D8 File Offset: 0x00034BD8
		public ParameterSubsegment(string parameterName)
		{
			if (parameterName.StartsWith("*", StringComparison.Ordinal))
			{
				this.ParameterName = parameterName.Substring(1);
				this.IsCatchAll = true;
				return;
			}
			this.ParameterName = parameterName;
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00036A0A File Offset: 0x00034C0A
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00036A12 File Offset: 0x00034C12
		public bool IsCatchAll { get; private set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00036A1B File Offset: 0x00034C1B
		// (set) Token: 0x06001308 RID: 4872 RVA: 0x00036A23 File Offset: 0x00034C23
		public string ParameterName { get; private set; }
	}
}
