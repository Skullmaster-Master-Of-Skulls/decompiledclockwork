using System;

namespace Google.Apis.Discovery
{
	// Token: 0x02000037 RID: 55
	public interface IParameter
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000119 RID: 281
		string Name { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011A RID: 282
		string Pattern { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600011B RID: 283
		bool IsRequired { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600011C RID: 284
		string DefaultValue { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011D RID: 285
		string ParameterType { get; }
	}
}
